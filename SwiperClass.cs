﻿// Decompiled with JetBrains decompiler
// Type: ClickServerService.SwiperClass
// Assembly: ClickServerService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6BDFD2F8-7BA8-4B8A-8EC1-401DFA893333
// Assembly location: C:\Users\Win10\Desktop\ClickServerService.exe

using System;
using System.Data;
using System.Data.SqlClient;

namespace ClickServerService
{
    internal class SwiperClass
    {
        private MainClass objmain = new MainClass();

        public int Swiper_Segment_insert(string Title, int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[Swiper_Segment]\r\n           ([ID]\r\n           ,[ID_GameCenter]\r\n           ,[Title],[IsDeleted])\r\n           VALUES\r\n           (@ID\r\n           ,@ID_GameCenter\r\n           ,@Title,0)", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (object)(this.objmain.Max_Tbl("Swiper_Segment", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter ", (object)ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@Title", (object)Title);
                    sqlCommand.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.objmain.ErrorLog(ex);
                return -1;
            }
        }

        public int Swiper_Segment_Update(int ID, int ID_GameCenter, string Title)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("update [Swiper_Segment] set \r\n           [Title]=@Title where  [ID]=@ID and  [ID_GameCenter]=@ID_GameCenter ", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (object)ID);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter ", (object)ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@Title", (object)Title);
                    sqlCommand.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.objmain.ErrorLog(ex);
                return -1;
            }
        }

        public DataTable Swiper_Segment_GetAll(int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("select * from Swiper_Segment where  [ID_GameCenter]=@ID_GameCenter and IsDeleted=0 ", connection);
                    selectCommand.Parameters.AddWithValue("@ID_GameCenter ", (object)ID_GameCenter);
                    new SqlDataAdapter(selectCommand).Fill(dataTable);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                this.objmain.ErrorLog(ex);
                return dataTable;
            }
        }

        public DataTable Swiper_Segment_Get(int ID, int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("select * from Swiper_Segment where [ID]=@ID and [ID_GameCenter]=@ID_GameCenter and IsDeleted=0 ", connection);
                    selectCommand.Parameters.AddWithValue("@ID", (object)ID);
                    selectCommand.Parameters.AddWithValue("@ID_GameCenter ", (object)ID_GameCenter);
                    new SqlDataAdapter(selectCommand).Fill(dataTable);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                this.objmain.ErrorLog(ex);
                return dataTable;
            }
        }

        public int Swiper_Segment_Delete(int ID, int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("update  Swiper_Segment set IsDeleted=1 where [ID]=@ID and [ID_GameCenter]=@ID_GameCenter  ", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (object)ID);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter ", (object)ID_GameCenter);
                    sqlCommand.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.objmain.ErrorLog(ex);
                return -1;
            }
        }

        public int Swiper_insert(
          int ID_GameCenter,
          string Title,
          string MacAddress,
          int ID_Games,
          bool State,
          string Dec,
          DateTime DateStart,
          int Price1,
          int Price2,
          int Delay1,
          int Delay2)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Swiper_Insert", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (object)(this.objmain.Max_Tbl("Swiper", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter ", (object)ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@Title", (object)Title);
                    sqlCommand.Parameters.AddWithValue("@MacAddress", (object)MacAddress);
                    sqlCommand.Parameters.AddWithValue("@ID_Games", (object)ID_Games);
                    sqlCommand.Parameters.AddWithValue("@State", (object)State);
                    sqlCommand.Parameters.AddWithValue("@Dec", (object)Dec);
                    sqlCommand.Parameters.AddWithValue("@DateStart", (object)DateStart);
                    sqlCommand.Parameters.AddWithValue("@Price1", (object)Price1);
                    sqlCommand.Parameters.AddWithValue("@Price2", (object)Price2);
                    sqlCommand.Parameters.AddWithValue("@Delay1", (object)Delay1);
                    sqlCommand.Parameters.AddWithValue("@Delay2", (object)Delay2);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.objmain.ErrorLog(ex);
                return -1;
            }
        }

        public int Swiper_Update(
          int ID,
          int ID_GameCenter,
          string Title,
          string MacAddress,
          int ID_Games,
          bool State,
          string Dec,
          DateTime DateStart,
          int Price1,
          int Price2,
          int Delay1,
          int Delay2)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Swiper_Update), connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (object)ID);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter ", (object)ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@Title", (object)Title);
                    sqlCommand.Parameters.AddWithValue("@MacAddress", (object)MacAddress);
                    sqlCommand.Parameters.AddWithValue("@ID_Games", (object)ID_Games);
                    sqlCommand.Parameters.AddWithValue("@State", (object)State);
                    sqlCommand.Parameters.AddWithValue("@Dec", (object)Dec);
                    sqlCommand.Parameters.AddWithValue("@DateStart", (object)DateStart);
                    sqlCommand.Parameters.AddWithValue("@Price1", (object)Price1);
                    sqlCommand.Parameters.AddWithValue("@Price2", (object)Price2);
                    sqlCommand.Parameters.AddWithValue("@Delay1", (object)Delay1);
                    sqlCommand.Parameters.AddWithValue("@Delay2", (object)Delay2);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.objmain.ErrorLog(ex);
                return -1;
            }
        }

        public DataTable Swiper_GetAll(int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(Swiper_GetAll), connection);
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    selectCommand.Parameters.AddWithValue("@ID_GameCenter ", (object)ID_GameCenter);
                    new SqlDataAdapter(selectCommand).Fill(dataTable);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                this.objmain.ErrorLog(ex);
                return dataTable;
            }
        }

        public DataTable Swiper_Get(int ID, int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(Swiper_Get), connection);
                    selectCommand.Parameters.AddWithValue("@ID", (object)ID);
                    selectCommand.Parameters.AddWithValue("@ID_GameCenter ", (object)ID_GameCenter);
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    new SqlDataAdapter(selectCommand).Fill(dataTable);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                this.objmain.ErrorLog(ex);
                return dataTable;
            }
        }

        public DataTable Swiper_GetByMacAddress(string MacAddress)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Swiper_Get_ByMacAddress", connection);
                    selectCommand.Parameters.AddWithValue("@MacAddress", (object)MacAddress);
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    new SqlDataAdapter(selectCommand).Fill(dataTable);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                this.objmain.ErrorLog(ex);
                return dataTable;
            }
        }

        public int RetDayOfWeek()
        {
            int num1 = this.Days_Special_Check();
            int num2;
            if (num1 == -1)
            {
                switch (DateTime.Now.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        num2 = 1;
                        break;
                    case DayOfWeek.Monday:
                        num2 = 2;
                        break;
                    case DayOfWeek.Tuesday:
                        num2 = 3;
                        break;
                    case DayOfWeek.Wednesday:
                        num2 = 4;
                        break;
                    case DayOfWeek.Thursday:
                        num2 = 5;
                        break;
                    case DayOfWeek.Friday:
                        num2 = 6;
                        break;
                    case DayOfWeek.Saturday:
                        num2 = 0;
                        break;
                    default:
                        num2 = 0;
                        break;
                }
            }
            else
                num2 = num1;
            return num2;
        }

        public int Days_Special_Check()
        {
            int num = -1;
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    new SqlDataAdapter(new SqlCommand("select * from [dbo].[Days_Special]\r\nwhere cast( [DaysDate] as date) = CAST((select GETDATE()) as Date)", connection)).Fill(dataTable);
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
                this.objmain.ErrorLog(ex);
                return -1;
            }
        }

        public DataTable Swiper_GetByMacAddressByChargeRate(string MacAddress)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    var x = RetDayOfWeek();
                    var g = this.objmain.ID_GameCenter_Local_Get();
                    var d = DateTime.Now.ToString("HH:mm").Split(':')[0];
                    SqlCommand selectCommand = new SqlCommand("Swiper_Get_ByMacAddress_ByChargeRate_ByGameCenter", connection);
                    selectCommand.Parameters.AddWithValue("@MacAddress", (object)MacAddress);
                    selectCommand.Parameters.AddWithValue("@ID_Days", (object)this.RetDayOfWeek());
                    selectCommand.Parameters.AddWithValue("@ID_GameCenter", (object)this.objmain.ID_GameCenter_Local_Get());
                    selectCommand.Parameters.AddWithValue("@hourTime", (object)DateTime.Now.ToString("HH:mm").Split(':')[0]);
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    new SqlDataAdapter(selectCommand).Fill(dataTable);
                    connection.Close();
                    connection.Dispose();
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                this.objmain.ErrorLog(ex);
                return dataTable;
            }
        }

        public DataTable Swiper_UpdateStateByMacAddress(string MacAddress, int ConfigState)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("update Swiper set Config_State=@ConfigState where  Swiper.MacAddress=@MacAddress and Swiper.ID_GameCenter=@ID_GameCenter", connection);
                    sqlCommand.Parameters.AddWithValue("@MacAddress", (object)MacAddress);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", (object)this.objmain.ID_GameCenter_Local_Get());
                    sqlCommand.Parameters.AddWithValue("@ConfigState", (object)ConfigState);
                    sqlCommand.ExecuteNonQuery();
                    connection.Close();
                    connection.Dispose();
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                this.objmain.ErrorLog(ex);
                return dataTable;
            }
        }

        public DataTable Swiper_GetByState()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("SELECT        TOP (1) Swiper.ID, Swiper.ID_GameCenter, Swiper.Title, Swiper.MacAddress, Swiper.ID_Games, Swiper.State, Swiper.Dec, Swiper.DateStart, Swiper.Price1, Swiper.Price2, Swiper.Delay1, Swiper.Delay2, Swiper.Pulse, \r\n                         Swiper.Config_State, Swiper.RepeatCount, Swiper.IsDeleted, Swiper.PulseType, Swiper.Start_Count_Voltage, Swiper.Version, Swiper.TicketErrorStop, Swiper.PullUp, Swiper.ID_Swiper_Segment, Games.IsRetired\r\n                    FROM            Swiper INNER JOIN\r\n                         Games ON Swiper.ID_Games = Games.ID\r\n                    WHERE(Swiper.ID_GameCenter = @ID_GameCenter)\r\n                    AND(Swiper.IsDeleted = 0) AND(Swiper.Config_State = -1) and(Games.IsRetired = 0)\r\n                    ORDER BY Swiper.ID", connection);
                    selectCommand.Parameters.AddWithValue("@ID_GameCenter", (object)this.objmain.ID_GameCenter_Local_Get());
                    new SqlDataAdapter(selectCommand).Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                        this.Swiper_GetByStateUpdate(int.Parse(dataTable.Rows[0]["ID"].ToString()), int.Parse(dataTable.Rows[0]["ID_GameCenter"].ToString()));
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                this.objmain.ErrorLog(ex);
                return dataTable;
            }
        }

        public DataTable Swiper_GetByState_ForChangePrice()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("SELECT        TOP (1) Swiper.ID, Swiper.ID_GameCenter, Swiper.Title, Swiper.MacAddress, Swiper.ID_Games, Swiper.State, Swiper.Dec, Swiper.DateStart, Swiper.Price1, Swiper.Price2, Swiper.Delay1, Swiper.Delay2, Swiper.Pulse, \r\n                         Swiper.Config_State, Swiper.RepeatCount, Swiper.IsDeleted, Swiper.PulseType, Swiper.Start_Count_Voltage, Swiper.Version, Swiper.TicketErrorStop, Swiper.PullUp, Swiper.ID_Swiper_Segment, Games.IsRetired\r\n                    FROM            Swiper INNER JOIN\r\n                         Games ON Swiper.ID_Games = Games.ID\r\n                    WHERE(Swiper.ID_GameCenter = @ID_GameCenter)\r\n                    AND(Swiper.IsDeleted = 0) AND(Swiper.Config_State = -2) and(Games.IsRetired = 0) and (Swiper.MacAddress<>'')\r\n                    ORDER BY Swiper.ID", connection);
                    selectCommand.Parameters.AddWithValue("@ID_GameCenter", (object)this.objmain.ID_GameCenter_Local_Get());
                    new SqlDataAdapter(selectCommand).Fill(dataTable);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                this.objmain.ErrorLog(ex);
                return dataTable;
            }
        }

        public DataTable Swiper_GetByStateUpdate(int ID, int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("update  Swiper set  Config_State=0 where ID_GameCenter=@ID_GameCenter and ID=@ID and Config_State=-1", connection);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", (object)ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@ID", (object)ID);
                    sqlCommand.ExecuteNonQuery();
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                this.objmain.ErrorLog(ex);
                return dataTable;
            }
        }

        public DataTable Swiper_StateUpdateToNotReciveForChargeRate()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("update  Swiper set  Config_State=-2 where ID_GameCenter=@ID_GameCenter   and Config_State=-3", connection);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", (object)this.objmain.ID_GameCenter_Local_Get());
                    sqlCommand.ExecuteNonQuery();
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                this.objmain.ErrorLog(ex);
                return dataTable;
            }
        }

        public int Swiper_Update_Config_StateByGameCenterID(int ID_GameCenter, int Config_State)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("update Swiper set Config_State=@Config_State where ID_GameCenter=@ID_GameCenter", connection);
                    sqlCommand.Parameters.AddWithValue("@Config_State", (object)Config_State);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter ", (object)ID_GameCenter);
                    sqlCommand.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.objmain.ErrorLog(ex);
                return -1;
            }
        }

        public int Swiper_Update_Config_StateAll(int Config_State, int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("update Swiper set Config_State=@Config_State where ID_GameCenter=@ID_GameCenter", connection);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", (object)ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@Config_State", (object)Config_State);
                    sqlCommand.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.objmain.ErrorLog(ex);
                return -1;
            }
        }

        public int Swiper_Update_Config_State(int Config_State, string MacAddress)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Swiper_Update_Config_State), connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Config_State", (object)Config_State);
                    sqlCommand.Parameters.AddWithValue("@MacAddress ", (object)MacAddress);
                    sqlCommand.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.objmain.ErrorLog(ex);
                return -1;
            }
        }

        public int Swiper_Delete(int ID, int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Swiper_Delete), connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ID", (object)ID);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter ", (object)ID_GameCenter);
                    sqlCommand.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.objmain.ErrorLog(ex);
                return -1;
            }
        }
    }
}