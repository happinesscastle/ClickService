// Decompiled with JetBrains decompiler
// Type: ClickServerService.CashierClass
// Assembly: ClickServerService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6BDFD2F8-7BA8-4B8A-8EC1-401DFA893333
// Assembly location: C:\Users\Win10\Desktop\ClickServerService.exe

using System;
using System.Data;
using System.Data.SqlClient;

namespace ClickServerService
{
    internal class CashierClass
    {
        private MainClass objmain = new MainClass();

        public DataTable CashierDesk_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("CashierDesk_GetAll", connection);
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

        public int CashierDesk_insert(
          string Title,
          bool Enable,
          string UsersIDAccess,
          int GameCenterID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("CashierDesk_Insert", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (object)(this.objmain.Max_Tbl("CashierDesk", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@Title", (object)Title);
                    sqlCommand.Parameters.AddWithValue("@Enable", (object)Enable);
                    sqlCommand.Parameters.AddWithValue("@UsersIDAccess", (object)UsersIDAccess);
                    sqlCommand.Parameters.AddWithValue("@Date", (object)DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@GameCenterID", (object)GameCenterID);
                    sqlCommand.Parameters.AddWithValue("@IsDeleted", (object)0);
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

        public int CashierDesk_Update(
          int ID,
          string Title,
          bool Enable,
          string UsersIDAccess,
          int GameCenterID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(CashierDesk_Update), connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (object)ID);
                    sqlCommand.Parameters.AddWithValue("@Title", (object)Title);
                    sqlCommand.Parameters.AddWithValue("@Enable", (object)Enable);
                    sqlCommand.Parameters.AddWithValue("@UsersIDAccess", (object)UsersIDAccess);
                    sqlCommand.Parameters.AddWithValue("@Date", (object)DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@GameCenterID", (object)GameCenterID);
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

        public DataTable CashierDesk_Get(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(CashierDesk_Get), connection);
                    selectCommand.Parameters.AddWithValue("@ID", (object)ID);
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

        public int CashierDesk_Delete(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(CashierDesk_Delete), connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ID", (object)ID);
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

        public int Orders_insert(
          int ID_GameCenter,
          int ID_User,
          int ID_CashierDesk,
          int ID_Card,
          string CardMacAddress,
          int SumPrice,
          int SumBonus,
          int SumFreeGame,
          int SumFreeDailyGame,
          int ID_Coupon,
          int CashPrice,
          int PosPrice)
        {
            DataTable dataTable = new DataTable();
            try
            {
                int num = this.objmain.Max_Tbl("Orders", "ID") + 1;
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[Orders]\r\n           ([ID]\r\n           ,[ID_GameCenter]\r\n           ,[ID_User]\r\n           ,[Date]\r\n           ,[ID_CashierDesk]\r\n           ,[ID_Card]\r\n           ,[CardMacAddress]\r\n           ,[SumPrice]\r\n           ,[SumBonus]\r\n           ,[SumFreeGame]\r\n           ,[SumFreeDailyGame]\r\n           ,[ID_Coupon]\r\n           ,[CashPrice]\r\n           ,[PosPrice]\r\n           ,[IsDeleted]\r\n         )\r\n     VALUES\r\n           (@ID\r\n           ,@ID_GameCenter\r\n           ,@ID_User\r\n           ,@Date\r\n           ,@ID_CashierDesk\r\n           ,@ID_Card\r\n           ,@CardMacAddress\r\n           ,@SumPrice\r\n           ,@SumBonus\r\n           ,@SumFreeGame\r\n           ,@SumFreeDailyGame\r\n           ,@ID_Coupon\r\n           ,@CashPrice\r\n           ,@PosPrice\r\n           ,@IsDeleted)", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (object)num);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", (object)ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@ID_User", (object)ID_User);
                    sqlCommand.Parameters.AddWithValue("@Date", (object)DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@ID_CashierDesk", (object)ID_CashierDesk);
                    sqlCommand.Parameters.AddWithValue("@ID_Card", (object)ID_Card);
                    sqlCommand.Parameters.AddWithValue("@CardMacAddress", (object)CardMacAddress);
                    sqlCommand.Parameters.AddWithValue("@SumPrice", (object)SumPrice);
                    sqlCommand.Parameters.AddWithValue("@SumBonus", (object)SumBonus);
                    sqlCommand.Parameters.AddWithValue("@SumFreeGame", (object)SumFreeGame);
                    sqlCommand.Parameters.AddWithValue("@SumFreeDailyGame", (object)SumFreeDailyGame);
                    sqlCommand.Parameters.AddWithValue("@ID_Coupon", (object)ID_Coupon);
                    sqlCommand.Parameters.AddWithValue("@CashPrice", (object)CashPrice);
                    sqlCommand.Parameters.AddWithValue("@PosPrice", (object)PosPrice);
                    sqlCommand.Parameters.AddWithValue("@IsDeleted", (object)0);
                    sqlCommand.ExecuteNonQuery();
                }
                return num;
            }
            catch (Exception ex)
            {
                this.objmain.ErrorLog(ex);
                return -1;
            }
        }

        public DataTable Orders_Get(int ID, int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("select * from [dbo].[Orders] where ID=@ID and ID_GameCenter=@ID_GameCenter", connection);
                    selectCommand.Parameters.AddWithValue("@ID", (object)ID);
                    selectCommand.Parameters.AddWithValue("@ID_GameCenter", (object)ID_GameCenter);
                    new SqlDataAdapter(selectCommand).Fill(dataTable);
                    connection.Close();
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                this.objmain.ErrorLog(ex);
                return dataTable;
            }
        }

        public int Order_Details_insert(
          int ID_GameCenter,
          int ID_Order,
          int ID_OrderItemType,
          int ID_OrderDetail,
          int Count,
          int Price,
          int Bonus,
          int ID_Coupon)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(" INSERT INTO [dbo].[Order_Details]\r\n           ([ID]\r\n           ,[ID_GameCenter]\r\n           ,[ID_Order]\r\n           ,[ID_OrderItemType]\r\n           ,[ID_OrderDetail]\r\n           ,[Count]\r\n           ,[Price]\r\n           ,[Bonus]\r\n           ,[ID_Coupon])\r\n     VALUES\r\n           (@ID\r\n           ,@ID_GameCenter\r\n           ,@ID_Order\r\n           ,@ID_OrderItemType\r\n           ,@ID_OrderDetail\r\n           ,@Count\r\n           ,@Price\r\n           ,@Bonus\r\n           ,@ID_Coupon)", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (object)(this.objmain.Max_Tbl("Order_Details", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", (object)ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@ID_Order", (object)ID_Order);
                    sqlCommand.Parameters.AddWithValue("@ID_OrderItemType", (object)ID_OrderItemType);
                    sqlCommand.Parameters.AddWithValue("@ID_OrderDetail", (object)ID_OrderDetail);
                    sqlCommand.Parameters.AddWithValue("@Count", (object)Count);
                    sqlCommand.Parameters.AddWithValue("@Price", (object)Price);
                    sqlCommand.Parameters.AddWithValue("@Bonus", (object)Bonus);
                    sqlCommand.Parameters.AddWithValue("@ID_Coupon", (object)ID_Coupon);
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
