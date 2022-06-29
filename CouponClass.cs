using System.Data.SqlClient;
using System.Data;
using System;

namespace ClickServerService
{
    internal class CouponClass
    {
        private readonly MainClass objMain = new MainClass();

        public DataTable Coupon_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Coupon_GetAll", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
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

        public int Coupon_Update(int ID, int ID_GameCenter, string Title, string Barcode, string Serial, bool IsPercent, int Amount, bool AmountIsLock, bool ForItem, bool ForAll, string Users_GroupsIds, bool EnableDateAlways, DateTime EnableDateFrom, DateTime EnableDateTo, bool EnableTimeAlways, string EnableTimeFrom, string EnableTimeTo, bool EnableEveryDay, string EnableDays, bool ForAllProduct, bool ForAllStockProduct, bool ForAllCardProduct, bool ForSelectedProduct, string SelectedProductList)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Coupon_Update), connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@Barcode", Barcode);
                    sqlCommand.Parameters.AddWithValue("@Serial", Serial);
                    sqlCommand.Parameters.AddWithValue("@IsPercent", IsPercent);
                    sqlCommand.Parameters.AddWithValue("@Amount", Amount);
                    sqlCommand.Parameters.AddWithValue("@AmountIsLock", AmountIsLock);
                    sqlCommand.Parameters.AddWithValue("@ForItem", ForItem);
                    sqlCommand.Parameters.AddWithValue("@ForAll", ForAll);
                    sqlCommand.Parameters.AddWithValue("@Users_GroupsIds", Users_GroupsIds);
                    sqlCommand.Parameters.AddWithValue("@EnableDateAlways", EnableDateAlways);
                    sqlCommand.Parameters.AddWithValue("@EnableDateFrom", EnableDateFrom);
                    sqlCommand.Parameters.AddWithValue("@EnableDateTo", EnableDateTo);
                    sqlCommand.Parameters.AddWithValue("@EnableTimeAlways", EnableTimeAlways);
                    sqlCommand.Parameters.AddWithValue("@EnableTimeFrom", EnableTimeFrom);
                    sqlCommand.Parameters.AddWithValue("@EnableTimeTo", EnableTimeTo);
                    sqlCommand.Parameters.AddWithValue("@EnableEveryDay", EnableEveryDay);
                    sqlCommand.Parameters.AddWithValue("@EnableDays", EnableDays);
                    sqlCommand.Parameters.AddWithValue("@ForAllProduct", ForAllProduct);
                    sqlCommand.Parameters.AddWithValue("@ForAllStockProduct", ForAllStockProduct);
                    sqlCommand.Parameters.AddWithValue("@ForAllCardProduct", ForAllCardProduct);
                    sqlCommand.Parameters.AddWithValue("@ForSelectedProduct", ForSelectedProduct);
                    sqlCommand.Parameters.AddWithValue("@SelectedProductList", SelectedProductList);
                    sqlCommand.Parameters.AddWithValue("@CreateDate", DateTime.Now);
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

        public DataTable Coupon_Get(int ID, int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(Coupon_Get), connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    selectCommand.Parameters.AddWithValue("@ID", ID);
                    selectCommand.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
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

        public int Coupon_Delete(int ID, int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Coupon_Delete), connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
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

        #region ' Useless '

        public int Coupon_insert(int ID_GameCenter, string Title, string Barcode, string Serial, bool IsPercent, int Amount, bool AmountIsLock, bool ForItem, bool ForAll, string Users_GroupsIds, bool EnableDateAlways, DateTime EnableDateFrom, DateTime EnableDateTo, bool EnableTimeAlways, string EnableTimeFrom, string EnableTimeTo, bool EnableEveryDay, string EnableDays, bool ForAllProduct, bool ForAllStockProduct, bool ForAllCardProduct, bool ForSelectedProduct, string SelectedProductList)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Coupon_Insert", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@ID", (objMain.Max_Tbl("Coupon", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@Barcode", Barcode);
                    sqlCommand.Parameters.AddWithValue("@Serial", Serial);
                    sqlCommand.Parameters.AddWithValue("@IsPercent", IsPercent);
                    sqlCommand.Parameters.AddWithValue("@Amount", Amount);
                    sqlCommand.Parameters.AddWithValue("@AmountIsLock", AmountIsLock);
                    sqlCommand.Parameters.AddWithValue("@ForItem", ForItem);
                    sqlCommand.Parameters.AddWithValue("@ForAll", ForAll);
                    sqlCommand.Parameters.AddWithValue("@Users_GroupsIds", Users_GroupsIds);
                    sqlCommand.Parameters.AddWithValue("@EnableDateAlways", EnableDateAlways);
                    sqlCommand.Parameters.AddWithValue("@EnableDateFrom", EnableDateFrom);
                    sqlCommand.Parameters.AddWithValue("@EnableDateTo", EnableDateTo);
                    sqlCommand.Parameters.AddWithValue("@EnableTimeAlways", EnableTimeAlways);
                    sqlCommand.Parameters.AddWithValue("@EnableTimeFrom", EnableTimeFrom);
                    sqlCommand.Parameters.AddWithValue("@EnableTimeTo", EnableTimeTo);
                    sqlCommand.Parameters.AddWithValue("@EnableEveryDay", EnableEveryDay);
                    sqlCommand.Parameters.AddWithValue("@EnableDays", EnableDays);
                    sqlCommand.Parameters.AddWithValue("@ForAllProduct", ForAllProduct);
                    sqlCommand.Parameters.AddWithValue("@ForAllStockProduct", ForAllStockProduct);
                    sqlCommand.Parameters.AddWithValue("@ForAllCardProduct", ForAllCardProduct);
                    sqlCommand.Parameters.AddWithValue("@ForSelectedProduct", ForSelectedProduct);
                    sqlCommand.Parameters.AddWithValue("@SelectedProductList", SelectedProductList);
                    sqlCommand.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@IsDeleted", 0);
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

        #endregion

    }
}
