using System.Text.RegularExpressions;
using System.Collections.Generic;
using ClickServerService.Models;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using Dapper;
using System;

namespace ClickServerService
{
    internal class SwiperClass
    {
        private readonly MainClass clsMain = new MainClass();
        public static List<Swiper> Swipers = new List<Swiper>();
        public static List<Swipers_ChargeRate> Swipers_ChargeRate = new List<Swipers_ChargeRate>();

        public int RetDayOfWeek()
        {
            try
            {
                int dayCheck = Days_Special_Check();
                if (dayCheck == -1)
                {
                    switch (DateTime.Now.DayOfWeek)
                    {
                        case DayOfWeek.Sunday: return 1;
                        case DayOfWeek.Monday: return 2;
                        case DayOfWeek.Tuesday: return 3;
                        case DayOfWeek.Wednesday: return 4;
                        case DayOfWeek.Thursday: return 5;
                        case DayOfWeek.Friday: return 6;
                        case DayOfWeek.Saturday: return 0;
                        default: return 0;
                    }
                }
                else
                    return dayCheck;
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
                return 0;
            }
        }

        public int Days_Special_Check()
        {
            int num = -1;
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    new SqlDataAdapter(new SqlCommand("select * from [dbo].[Days_Special] where cast( [DaysDate] as date) = CAST((select GETDATE()) as Date)", connection)).Fill(dataTable);
                    connection.Close();
                    connection.Dispose();
                }
                if (dataTable.Rows.Count > 0)
                {
                    switch (int.Parse(dataTable.Rows[0]["ID_SpecialDays_Type"].ToString()))
                    {
                        case 1: num = 7; break;
                        case 2: num = 8; break;
                        case 3: num = 9; break;
                    }
                }
                return num;
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
                return -1;
            }
        }

        public void Swiper_UpdateStateByMacAddress(string macAddress, int configState)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Update Swiper set Config_State=@ConfigState where Swiper.MacAddress=@MacAddress and Swiper.ID_GameCenter=@ID_GameCenter", connection);
                    sqlCommand.Parameters.AddWithValue("@MacAddress", macAddress);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", clsMain.ID_GameCenter_Local_Get());
                    sqlCommand.Parameters.AddWithValue("@ConfigState", configState);
                    sqlCommand.ExecuteNonQuery();
                    connection.Close();
                    connection.Dispose();
                    Swipers.Where(mac => mac.MacAddress == macAddress).Where(gameCenterID => gameCenterID.ID_GameCenter == clsMain.ID_GameCenter_Local_Get()).FirstOrDefault().Config_State = configState;
                }
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
            }
        }

        public Swiper Swiper_GetByState()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    string query = @"SELECT TOP (1) Swiper.ID, Swiper.Title, Swiper.MacAddress, Swiper.ID_GameCenter FROM Swiper INNER JOIN Games ON Swiper.ID_Games = Games.ID WHERE(Swiper.ID_GameCenter = @ID_GameCenter) AND(Swiper.IsDeleted = 0) AND(Swiper.Config_State = -1) and(Games.IsRetired = 0) ORDER BY Swiper.ID";
                    var temp = (List<Swiper>)connection.Query<Swiper>(query, new { ID_GameCenter = clsMain.ID_GameCenter_Local_Get() });
                    if (temp.Any())
                    {
                        Swiper_GetByStateUpdate(Convert.ToInt32(temp.FirstOrDefault().ID), Convert.ToInt32(temp.FirstOrDefault().ID_GameCenter));
                        return temp.FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
            }
            return null;
        }

        public List<Swiper> Swiper_GetByState_ForChangePrice()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    string query = @"SELECT Swiper.ID, Swiper.Title , Swiper.MacAddress FROM Swiper INNER JOIN Games ON Swiper.ID_Games = Games.ID WHERE(Swiper.ID_GameCenter = @ID_GameCenter) AND(Swiper.IsDeleted = 0) AND(Swiper.Config_State = -2) and(Games.IsRetired = 0) and (Swiper.MacAddress<>'') ORDER BY Swiper.ID";
                    var temp = (List<Swiper>)connection.Query<Swiper>(query, new { ID_GameCenter = clsMain.ID_GameCenter_Local_Get() });
                    if (temp.Any())
                        return temp;
                }
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
            }
            return null;
        }

        public void Swiper_GetByStateUpdate(int id, int id_GameCenter)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Update Swiper set  Config_State=0 where ID_GameCenter=@ID_GameCenter and ID=@ID and Config_State=-1", connection);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", id_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@ID", id);
                    sqlCommand.ExecuteNonQuery();
                    Swipers.Where(i => i.ID == id).Where(gcID => gcID.ID_GameCenter == id_GameCenter).Where(conState => conState.Config_State == -1).FirstOrDefault().Config_State = 0;
                }
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
            }
        }

        public void Swiper_UpdateStateToNotReceiveForChargeRate()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Update Swiper Set Config_State=-2 Where ID_GameCenter=@ID_GameCenter And Config_State=-3", connection);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", clsMain.ID_GameCenter_Local_Get());
                    sqlCommand.ExecuteNonQuery();

                    foreach (var item in Swipers.Where(conState => conState.Config_State == -2).Where(gcID => gcID.ID_GameCenter == clsMain.ID_GameCenter_Local_Get()))
                    {
                        item.Config_State = -2;
                    }
                }
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
            }
        }

        public void Swiper_Update_Config_StateByGameCenterID(int id_GameCenter, int config_State)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Update Swiper set Config_State=@Config_State where ID_GameCenter=@ID_GameCenter", connection);
                    sqlCommand.Parameters.AddWithValue("@Config_State", config_State);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter ", id_GameCenter);
                    sqlCommand.ExecuteNonQuery();
                    foreach (var item in Swipers.Where(gcID => gcID.ID_GameCenter == id_GameCenter))
                    {
                        item.Config_State = config_State;
                    }
                }
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
            }
        }

        public void Swiper_Update_Config_State(int config_State, string macAddress)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Swiper_Update_Config_State", connection) { CommandType = CommandType.StoredProcedure };
                    sqlCommand.Parameters.AddWithValue("@Config_State", config_State);
                    sqlCommand.Parameters.AddWithValue("@MacAddress ", macAddress);
                    sqlCommand.ExecuteNonQuery();
                    Swipers.FirstOrDefault(mac => mac.MacAddress == macAddress.ToUpper()).Config_State = config_State;
                }
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
            }
        }

        #region ' S.E.M '

        /// <summary>
        /// Get Mac From Command
        /// </summary>
        public string GetMacSwiper(string command)
        {
            try
            {
                if (command != null)
                    if (!string.IsNullOrWhiteSpace(command))
                    {
                        if (command.Contains("[") && command.Contains("]"))
                        {
                            int aIndex = command.IndexOf('[') + 1;
                            int zIndex = command.IndexOf(']');
                            string macIP = command.Substring(aIndex, zIndex - aIndex);

                            if (!Regex.IsMatch(macIP, "^[0-9]*$"))
                                return macIP.Substring(0, 2) + macIP.Substring(3, 2) + macIP.Substring(6, 2) + macIP.Substring(9, 2);
                        }
                    }
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
            }
            return "";
        }

        /// <summary>
        /// Get All Swiper From Database 
        /// </summary>
        public List<Swiper> GetAllSwiper()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {

                    string query = @"Select * From Swiper Where ID_GameCenter = @ID_GameCenter";
                    var temp = (List<Swiper>)connection.Query<Swiper>(query, new { ID_GameCenter = clsMain.ID_GameCenter_Local_Get() });
                    if (temp.Any())
                    {
                        foreach (var item in temp)
                        {
                            item.MacAddress = item.MacAddress.ToUpper();
                        }
                        return temp;
                    }
                }
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
            }
            return null;
        }

        /// <summary>
        /// Get Swipers By ByChargeRate
        /// </summary>
        public List<Swipers_ChargeRate> Swipers_GetByMacAddressByChargeRate()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    var param = new DynamicParameters();
                    param.Add("@MacAddress", "GetAll_NoMacFilter");
                    param.Add("@ID_Days", RetDayOfWeek());
                    param.Add("@ID_GameCenter", clsMain.ID_GameCenter_Local_Get());
                    param.Add("@hourTime", DateTime.Now.ToString("HH:mm").Split(':')[0]);
                    var res = connection.QueryMultiple("Swiper_Get_ByMacAddress_ByChargeRate_ByGameCenter", param, commandType: CommandType.StoredProcedure);
                    List<Swipers_ChargeRate> temp = res.Read<Swipers_ChargeRate>().ToList();
                    if (temp.Any())
                    {
                        foreach (var item in temp)
                        {
                            if (item.State == null)
                                item.State = false;
                            if (item.Config_State == null)
                                item.Config_State = 0;
                            item.MacAddress = item.MacAddress.ToUpper();
                        }
                        return temp;
                    }
                }
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
            }
            return null;
        }

        #endregion

        //

        #region ' Useless '

        public DataTable Swiper_GetByMacAddressByChargeRate(string macAddress)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Swiper_Get_ByMacAddress_ByChargeRate_ByGameCenter", connection) { CommandType = CommandType.StoredProcedure };
                    selectCommand.Parameters.AddWithValue("@MacAddress", macAddress.ToUpper());
                    selectCommand.Parameters.AddWithValue("@ID_Days", RetDayOfWeek());
                    selectCommand.Parameters.AddWithValue("@ID_GameCenter", clsMain.ID_GameCenter_Local_Get());
                    selectCommand.Parameters.AddWithValue("@hourTime", DateTime.Now.ToString("HH:mm").Split(':')[0]);
                    new SqlDataAdapter(selectCommand).Fill(dataTable);
                    connection.Close();
                    connection.Dispose();
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
                return dataTable;
            }
        }

        public void Swiper_Update_Config_StateAll(int Config_State, int ID_GameCenter)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Update Swiper set Config_State=@Config_State where ID_GameCenter=@ID_GameCenter", connection);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@Config_State", Config_State);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
            }
        }

        public DataTable Swiper_Get(int ID, int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Swiper_Get", connection) { CommandType = CommandType.StoredProcedure };
                    selectCommand.Parameters.AddWithValue("@ID", ID);
                    selectCommand.Parameters.AddWithValue("@ID_GameCenter ", ID_GameCenter);
                    new SqlDataAdapter(selectCommand).Fill(dataTable);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
                return dataTable;
            }
        }

        public string GetSwiperSegmentByMac(string mac, int gameCenterID)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(mac))
                    return "";
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    string query = $"select ID_Swiper_Segment From Swiper Where ID_GameCenter = {gameCenterID} And MacAddress = N'{mac}'";
                    var temp = connection.ExecuteScalar(query);
                    if (temp != null && (!string.IsNullOrWhiteSpace(temp.ToString())))
                        return temp.ToString();
                }
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
            }
            return "";
        }

        public DataTable Swiper_GetAll(int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Swiper_GetAll", connection) { CommandType = CommandType.StoredProcedure };
                    selectCommand.Parameters.AddWithValue("@ID_GameCenter ", ID_GameCenter);
                    new SqlDataAdapter(selectCommand).Fill(dataTable);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
                return dataTable;
            }
        }

        public int Swiper_Update(int ID, int ID_GameCenter, string Title, string MacAddress, int ID_Games, bool State, string Dec, DateTime DateStart, int Price1, int Price2, int Delay1, int Delay2)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Swiper_Update", connection) { CommandType = CommandType.StoredProcedure };
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter ", ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@MacAddress", MacAddress);
                    sqlCommand.Parameters.AddWithValue("@ID_Games", ID_Games);
                    sqlCommand.Parameters.AddWithValue("@State", State);
                    sqlCommand.Parameters.AddWithValue("@Dec", Dec);
                    sqlCommand.Parameters.AddWithValue("@DateStart", DateStart);
                    sqlCommand.Parameters.AddWithValue("@Price1", Price1);
                    sqlCommand.Parameters.AddWithValue("@Price2", Price2);
                    sqlCommand.Parameters.AddWithValue("@Delay1", Delay1);
                    sqlCommand.Parameters.AddWithValue("@Delay2", Delay2);
                    sqlCommand.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
                return -1;
            }
        }

        public int Swiper_Segment_insert(string Title, int ID_GameCenter)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[Swiper_Segment] ([ID] ,[ID_GameCenter] ,[Title],[IsDeleted]) VALUES (@ID ,@ID_GameCenter ,@Title,0)", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (clsMain.Max_Tbl("Swiper_Segment", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter ", ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
                return -1;
            }
        }

        public int Swiper_Segment_Update(int ID, int ID_GameCenter, string Title)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("update [Swiper_Segment] set [Title]=@Title where  [ID]=@ID and  [ID_GameCenter]=@ID_GameCenter ", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter ", ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
                return -1;
            }
        }

        public DataTable Swiper_Segment_GetAll(int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("select * from Swiper_Segment where  [ID_GameCenter]=@ID_GameCenter and IsDeleted=0 ", connection);
                    selectCommand.Parameters.AddWithValue("@ID_GameCenter ", ID_GameCenter);
                    new SqlDataAdapter(selectCommand).Fill(dataTable);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
                return dataTable;
            }
        }

        public DataTable Swiper_Segment_Get(int ID, int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("select * from Swiper_Segment where [ID]=@ID and [ID_GameCenter]=@ID_GameCenter and IsDeleted=0 ", connection);
                    selectCommand.Parameters.AddWithValue("@ID", ID);
                    selectCommand.Parameters.AddWithValue("@ID_GameCenter ", ID_GameCenter);
                    new SqlDataAdapter(selectCommand).Fill(dataTable);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
                return dataTable;
            }
        }

        public int Swiper_Segment_Delete(int ID, int ID_GameCenter)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("update  Swiper_Segment set IsDeleted=1 where [ID]=@ID and [ID_GameCenter]=@ID_GameCenter  ", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter ", ID_GameCenter);
                    sqlCommand.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
                return -1;
            }
        }

        public int Swiper_insert(int ID_GameCenter, string Title, string MacAddress, int ID_Games, bool State, string Dec, DateTime DateStart, int Price1, int Price2, int Delay1, int Delay2)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Swiper_Insert", connection) { CommandType = CommandType.StoredProcedure };
                    sqlCommand.Parameters.AddWithValue("@ID", (clsMain.Max_Tbl("Swiper", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter ", ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@MacAddress", MacAddress);
                    sqlCommand.Parameters.AddWithValue("@ID_Games", ID_Games);
                    sqlCommand.Parameters.AddWithValue("@State", State);
                    sqlCommand.Parameters.AddWithValue("@Dec", Dec);
                    sqlCommand.Parameters.AddWithValue("@DateStart", DateStart);
                    sqlCommand.Parameters.AddWithValue("@Price1", Price1);
                    sqlCommand.Parameters.AddWithValue("@Price2", Price2);
                    sqlCommand.Parameters.AddWithValue("@Delay1", Delay1);
                    sqlCommand.Parameters.AddWithValue("@Delay2", Delay2);
                    sqlCommand.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
                return -1;
            }
        }

        public DataTable Swiper_GetByMacAddress(string MacAddress)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Swiper_Get_ByMacAddress", connection) { CommandType = CommandType.StoredProcedure };
                    selectCommand.Parameters.AddWithValue("@MacAddress", MacAddress);
                    new SqlDataAdapter(selectCommand).Fill(dataTable);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
                return dataTable;
            }
        }

        public int Swiper_Delete(int ID, int ID_GameCenter)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Swiper_Delete", connection) { CommandType = CommandType.StoredProcedure };
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter ", ID_GameCenter);
                    sqlCommand.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
                return -1;
            }
        }

        #endregion
    }
}
