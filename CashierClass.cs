using System.Data.SqlClient;
using System.Data;
using System;

namespace ClickServerService
{
    internal class CashierClass
    {
        private readonly MainClass clsMain = new MainClass();

        public DataTable Orders_Get(int id, int id_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Select * from [dbo].[Orders] where ID=@ID and ID_GameCenter=@ID_GameCenter", connection);
                    selectCommand.Parameters.AddWithValue("@ID", id);
                    selectCommand.Parameters.AddWithValue("@ID_GameCenter", id_GameCenter);
                    new SqlDataAdapter(selectCommand).Fill(dataTable);
                    connection.Close();
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
                return dataTable;
            }
        }

        #region ' Useless '

        public DataTable CashierDesk_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("CashierDesk_GetAll", connection) { CommandType = CommandType.StoredProcedure };
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


        public int CashierDesk_Update(int ID, string Title, bool Enable, string UsersIDAccess, int GameCenterID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("CashierDesk_Update", connection) { CommandType = CommandType.StoredProcedure };
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@Enable", Enable);
                    sqlCommand.Parameters.AddWithValue("@UsersIDAccess", UsersIDAccess);
                    sqlCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@GameCenterID", GameCenterID);
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

        public DataTable CashierDesk_Get(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("CashierDesk_Get", connection) { CommandType = CommandType.StoredProcedure };
                    selectCommand.Parameters.AddWithValue("@ID", ID);
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

        public int CashierDesk_Delete(int ID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("CashierDesk_Delete", connection) { CommandType = CommandType.StoredProcedure };
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
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

        public int CashierDesk_insert(string Title, bool Enable, string UsersIDAccess, int GameCenterID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("CashierDesk_Insert", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@ID", (clsMain.Max_Tbl("CashierDesk", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@Enable", Enable);
                    sqlCommand.Parameters.AddWithValue("@UsersIDAccess", UsersIDAccess);
                    sqlCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@GameCenterID", GameCenterID);
                    sqlCommand.Parameters.AddWithValue("@IsDeleted", 0);
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

        public int Orders_insert(int ID_GameCenter, int ID_User, int ID_CashierDesk, int ID_Card, string CardMacAddress, int SumPrice, int SumBonus, int SumFreeGame, int SumFreeDailyGame, int ID_Coupon, int CashPrice, int PosPrice)
        {
            try
            {
                int num = clsMain.Max_Tbl("Orders", "ID") + 1;
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[Orders]             ([ID]             ,[ID_GameCenter]             ,[ID_User]             ,[Date]             ,[ID_CashierDesk]             ,[ID_Card]             ,[CardMacAddress]             ,[SumPrice]             ,[SumBonus]             ,[SumFreeGame]             ,[SumFreeDailyGame]             ,[ID_Coupon]             ,[CashPrice]             ,[PosPrice]             ,[IsDeleted]           )       VALUES             (@ID             ,@ID_GameCenter             ,@ID_User             ,@Date             ,@ID_CashierDesk             ,@ID_Card             ,@CardMacAddress             ,@SumPrice             ,@SumBonus             ,@SumFreeGame             ,@SumFreeDailyGame             ,@ID_Coupon             ,@CashPrice             ,@PosPrice             ,@IsDeleted)", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", num);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@ID_User", ID_User);
                    sqlCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@ID_CashierDesk", ID_CashierDesk);
                    sqlCommand.Parameters.AddWithValue("@ID_Card", ID_Card);
                    sqlCommand.Parameters.AddWithValue("@CardMacAddress", CardMacAddress);
                    sqlCommand.Parameters.AddWithValue("@SumPrice", SumPrice);
                    sqlCommand.Parameters.AddWithValue("@SumBonus", SumBonus);
                    sqlCommand.Parameters.AddWithValue("@SumFreeGame", SumFreeGame);
                    sqlCommand.Parameters.AddWithValue("@SumFreeDailyGame", SumFreeDailyGame);
                    sqlCommand.Parameters.AddWithValue("@ID_Coupon", ID_Coupon);
                    sqlCommand.Parameters.AddWithValue("@CashPrice", CashPrice);
                    sqlCommand.Parameters.AddWithValue("@PosPrice", PosPrice);
                    sqlCommand.Parameters.AddWithValue("@IsDeleted", 0);
                    sqlCommand.ExecuteNonQuery();
                }
                return num;
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
                return -1;
            }
        }

        public int Order_Details_insert(int ID_GameCenter, int ID_Order, int ID_OrderItemType, int ID_OrderDetail, int Count, int Price, int Bonus, int ID_Coupon)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(" INSERT INTO [dbo].[Order_Details]             ([ID]             ,[ID_GameCenter]             ,[ID_Order]             ,[ID_OrderItemType]             ,[ID_OrderDetail]             ,[Count]             ,[Price]             ,[Bonus]             ,[ID_Coupon])       VALUES             (@ID             ,@ID_GameCenter             ,@ID_Order             ,@ID_OrderItemType             ,@ID_OrderDetail             ,@Count             ,@Price             ,@Bonus             ,@ID_Coupon)", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (clsMain.Max_Tbl("Order_Details", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@ID_Order", ID_Order);
                    sqlCommand.Parameters.AddWithValue("@ID_OrderItemType", ID_OrderItemType);
                    sqlCommand.Parameters.AddWithValue("@ID_OrderDetail", ID_OrderDetail);
                    sqlCommand.Parameters.AddWithValue("@Count", Count);
                    sqlCommand.Parameters.AddWithValue("@Price", Price);
                    sqlCommand.Parameters.AddWithValue("@Bonus", Bonus);
                    sqlCommand.Parameters.AddWithValue("@ID_Coupon", ID_Coupon);
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
