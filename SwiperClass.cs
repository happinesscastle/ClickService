﻿using System.Text.RegularExpressions;
using System.Collections.Generic;
using ClickServerService.Models;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using System;
using System.Linq;

namespace ClickServerService
{
    internal class SwiperClass
    {
        private readonly MainClass objMain = new MainClass();
        public static List<Swiper> Swipers = new List<Swiper>();
        public static List<Swipers_Get_ByChargeRate_ByGameCenter> Swipers_ChargeRate = new List<Swipers_Get_ByChargeRate_ByGameCenter>();

        public int Swiper_Update(int ID, int ID_GameCenter, string Title, string MacAddress, int ID_Games, bool State, string Dec, DateTime DateStart, int Price1, int Price2, int Delay1, int Delay2)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Swiper_Update), connection) { CommandType = CommandType.StoredProcedure };
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
                objMain.ErrorLog(ex);
                return -1;
            }
        }

        public DataTable Swiper_GetAll(int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(Swiper_GetAll), connection) { CommandType = CommandType.StoredProcedure };
                    selectCommand.Parameters.AddWithValue("@ID_GameCenter ", ID_GameCenter);
                    new SqlDataAdapter(selectCommand).Fill(dataTable);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return dataTable;
            }
        }

        public DataTable Swiper_Get(int ID, int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(Swiper_Get), connection) { CommandType = CommandType.StoredProcedure };
                    selectCommand.Parameters.AddWithValue("@ID", ID);
                    selectCommand.Parameters.AddWithValue("@ID_GameCenter ", ID_GameCenter);
                    new SqlDataAdapter(selectCommand).Fill(dataTable);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return dataTable;
            }
        }

        public int RetDayOfWeek()
        {
            try
            {
                int dayCheck = Days_Special_Check();
                int dayInt;
                if (dayCheck == -1)
                {
                    switch (DateTime.Now.DayOfWeek)
                    {
                        case DayOfWeek.Sunday:
                            dayInt = 1; break;
                        case DayOfWeek.Monday:
                            dayInt = 2; break;
                        case DayOfWeek.Tuesday:
                            dayInt = 3; break;
                        case DayOfWeek.Wednesday:
                            dayInt = 4; break;
                        case DayOfWeek.Thursday:
                            dayInt = 5; break;
                        case DayOfWeek.Friday:
                            dayInt = 6; break;
                        case DayOfWeek.Saturday:
                            dayInt = 0; break;
                        default:
                            dayInt = 0; break;
                    }
                }
                else
                    dayInt = dayCheck;
                return dayInt;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return 0;
            }
        }

        public int Days_Special_Check()
        {
            int num = -1;
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
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
                        case 1:
                            num = 7;
                            break;
                        case 2:
                            num = 8;
                            break;
                        case 3:
                            num = 9;
                            break;
                    }
                }
                return num;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return -1;
            }
        }

        public DataTable Swiper_GetByMacAddressByChargeRate(string MacAddress)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Swiper_Get_ByMacAddress_ByChargeRate_ByGameCenter", connection) { CommandType = CommandType.StoredProcedure };
                    selectCommand.Parameters.AddWithValue("@MacAddress", MacAddress.ToUpper());
                    selectCommand.Parameters.AddWithValue("@ID_Days", RetDayOfWeek());
                    selectCommand.Parameters.AddWithValue("@ID_GameCenter", objMain.ID_GameCenter_Local_Get());
                    selectCommand.Parameters.AddWithValue("@hourTime", DateTime.Now.ToString("HH:mm").Split(':')[0]);
                    new SqlDataAdapter(selectCommand).Fill(dataTable);
                    connection.Close();
                    connection.Dispose();
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return dataTable;
            }
        }

        public DataTable Swiper_UpdateStateByMacAddress(string MacAddress, int ConfigState)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Update Swiper set Config_State=@ConfigState where  Swiper.MacAddress=@MacAddress and Swiper.ID_GameCenter=@ID_GameCenter", connection);
                    sqlCommand.Parameters.AddWithValue("@MacAddress", MacAddress);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", objMain.ID_GameCenter_Local_Get());
                    sqlCommand.Parameters.AddWithValue("@ConfigState", ConfigState);
                    sqlCommand.ExecuteNonQuery();
                    connection.Close();
                    connection.Dispose();
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return dataTable;
            }
        }

        public DataTable Swiper_GetByState()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("SELECT TOP (1) Swiper.ID, Swiper.ID_GameCenter, Swiper.Title, Swiper.MacAddress, Swiper.ID_Games, Swiper.State, Swiper.Dec, Swiper.DateStart, Swiper.Price1, Swiper.Price2, Swiper.Delay1, Swiper.Delay2, Swiper.Pulse, Swiper.Config_State, Swiper.RepeatCount, Swiper.IsDeleted, Swiper.PulseType, Swiper.Start_Count_Voltage, Swiper.Version, Swiper.TicketErrorStop, Swiper.PullUp, Swiper.ID_Swiper_Segment, Games.IsRetired FROM Swiper INNER JOIN Games ON Swiper.ID_Games = Games.ID WHERE(Swiper.ID_GameCenter = @ID_GameCenter) AND(Swiper.IsDeleted = 0) AND(Swiper.Config_State = -1) and(Games.IsRetired = 0) ORDER BY Swiper.ID", connection);
                    selectCommand.Parameters.AddWithValue("@ID_GameCenter", objMain.ID_GameCenter_Local_Get());
                    new SqlDataAdapter(selectCommand).Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                        Swiper_GetByStateUpdate(int.Parse(dataTable.Rows[0]["ID"].ToString()), int.Parse(dataTable.Rows[0]["ID_GameCenter"].ToString()));
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return dataTable;
            }
        }

        public DataTable Swiper_GetByState_ForChangePrice(bool isTopOne = true)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    string topOne = "";
                    if (isTopOne)
                        topOne = " Top(1) ";
                    SqlCommand selectCommand = new SqlCommand($"SELECT {topOne} Swiper.ID, Swiper.ID_GameCenter, Swiper.Title, Swiper.MacAddress, Swiper.ID_Games, Swiper.State, Swiper.Dec, Swiper.DateStart, Swiper.Price1, Swiper.Price2, Swiper.Delay1, Swiper.Delay2, Swiper.Pulse, Swiper.Config_State, Swiper.RepeatCount, Swiper.IsDeleted, Swiper.PulseType, Swiper.Start_Count_Voltage, Swiper.Version, Swiper.TicketErrorStop, Swiper.PullUp, Swiper.ID_Swiper_Segment, Games.IsRetired FROM Swiper INNER JOIN Games ON Swiper.ID_Games = Games.ID WHERE(Swiper.ID_GameCenter = @ID_GameCenter) AND(Swiper.IsDeleted = 0) AND(Swiper.Config_State = -2) and(Games.IsRetired = 0) and (Swiper.MacAddress<>'') ORDER BY Swiper.ID", connection);
                    selectCommand.Parameters.AddWithValue("@ID_GameCenter", objMain.ID_GameCenter_Local_Get());
                    new SqlDataAdapter(selectCommand).Fill(dataTable);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return dataTable;
            }
        }

        public DataTable Swiper_GetByStateUpdate(int ID, int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("update  Swiper set  Config_State=0 where ID_GameCenter=@ID_GameCenter and ID=@ID and Config_State=-1", connection);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.ExecuteNonQuery();
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return dataTable;
            }
        }

        public DataTable Swiper_StateUpdateToNotReceiveForChargeRate()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("update  Swiper set  Config_State=-2 where ID_GameCenter=@ID_GameCenter   and Config_State=-3", connection);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", objMain.ID_GameCenter_Local_Get());
                    sqlCommand.ExecuteNonQuery();
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return dataTable;
            }
        }

        public int Swiper_Update_Config_StateByGameCenterID(int ID_GameCenter, int Config_State)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("update Swiper set Config_State=@Config_State where ID_GameCenter=@ID_GameCenter", connection);
                    sqlCommand.Parameters.AddWithValue("@Config_State", Config_State);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter ", ID_GameCenter);
                    sqlCommand.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return -1;
            }
        }

        public int Swiper_Update_Config_StateAll(int Config_State, int ID_GameCenter)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("update Swiper set Config_State=@Config_State where ID_GameCenter=@ID_GameCenter", connection);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@Config_State", Config_State);
                    sqlCommand.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return -1;
            }
        }

        public int Swiper_Update_Config_State(int Config_State, string MacAddress)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Swiper_Update_Config_State), connection) { CommandType = CommandType.StoredProcedure };
                    sqlCommand.Parameters.AddWithValue("@Config_State", Config_State);
                    sqlCommand.Parameters.AddWithValue("@MacAddress ", MacAddress);
                    sqlCommand.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return -1;
            }
        }

        public int Swiper_Delete(int ID, int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Swiper_Delete), connection) { CommandType = CommandType.StoredProcedure };
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter ", ID_GameCenter);
                    sqlCommand.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return -1;
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
                            {
                                string tempMac = macIP.Substring(0, 2) + macIP.Substring(3, 2) + macIP.Substring(6, 2) + macIP.Substring(9, 2);
                                return tempMac;
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
            }
            return "";
        }

        public string GetSwiperSegmentByMac(string mac, int gameCenterID)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(mac))
                    return "";
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    string query = $"select ID_Swiper_Segment From Swiper Where ID_GameCenter = {gameCenterID} And MacAddress = N'{mac}'";
                    var temp = connection.ExecuteScalar(query);
                    if (temp != null && (!string.IsNullOrWhiteSpace(temp.ToString())))
                        return temp.ToString();
                }
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
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
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {

                    string query = @"Select * From Swiper ID_GameCenter = @ID_GameCenter";
                    var temp = (List<Swiper>)connection.Query<Swiper>(query, new { ID_GameCenter = objMain.ID_GameCenter_Local_Get() });
                    if (temp.Any())
                        return temp;
                }
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
            }
            return null;
        }

        /// <summary>
        /// Get Swipers By ByChargeRate
        /// </summary>
        public List<Swipers_Get_ByChargeRate_ByGameCenter> Swipers_GetByChargeRate()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    var param = new DynamicParameters();
                    param.Add("@ID_Days", RetDayOfWeek());
                    param.Add("@ID_GameCenter", objMain.ID_GameCenter_Local_Get());
                    param.Add("@hourTime", DateTime.Now.ToString("HH:mm").Split(':')[0]);
                    var res = connection.QueryMultiple("Swipers_Get_ByChargeRate_ByGameCenter", param, commandType: CommandType.StoredProcedure);
                    List<Swipers_Get_ByChargeRate_ByGameCenter> temp = res.Read<Swipers_Get_ByChargeRate_ByGameCenter>().ToList();
                    if (temp.Any())
                        return temp;
                }
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
            }
            return null;
        }

        #endregion

        //

        #region ' Useless '

        public int Swiper_Segment_insert(string Title, int ID_GameCenter)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[Swiper_Segment] ([ID] ,[ID_GameCenter] ,[Title],[IsDeleted]) VALUES (@ID ,@ID_GameCenter ,@Title,0)", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (objMain.Max_Tbl("Swiper_Segment", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter ", ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return -1;
            }
        }

        public int Swiper_Segment_Update(int ID, int ID_GameCenter, string Title)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
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
                objMain.ErrorLog(ex);
                return -1;
            }
        }

        public DataTable Swiper_Segment_GetAll(int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
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
                objMain.ErrorLog(ex);
                return dataTable;
            }
        }

        public DataTable Swiper_Segment_Get(int ID, int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
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
                objMain.ErrorLog(ex);
                return dataTable;
            }
        }

        public int Swiper_Segment_Delete(int ID, int ID_GameCenter)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
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
                objMain.ErrorLog(ex);
                return -1;
            }
        }

        public int Swiper_insert(int ID_GameCenter, string Title, string MacAddress, int ID_Games, bool State, string Dec, DateTime DateStart, int Price1, int Price2, int Delay1, int Delay2)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Swiper_Insert", connection) { CommandType = CommandType.StoredProcedure };
                    sqlCommand.Parameters.AddWithValue("@ID", (objMain.Max_Tbl("Swiper", "ID") + 1));
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
                objMain.ErrorLog(ex);
                return -1;
            }
        }

        public DataTable Swiper_GetByMacAddress(string MacAddress)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
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
                objMain.ErrorLog(ex);
                return dataTable;
            }
        }
    }

    #endregion
}
