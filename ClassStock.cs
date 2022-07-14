using System.Data.SqlClient;
using System.Data;
using System;

namespace ClickServerService
{
    internal class ClassStock
    {
        #region ' Useless '

        private readonly MainClass clsMain = new MainClass();

        public int Stock_Product_Group_Update(int ID, string Title)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Stock_Product_Group_Update", connection) { CommandType = CommandType.StoredProcedure };
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
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

        public DataTable Stock_Product_Group_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_Product_Group_GetAll", connection) { CommandType = CommandType.StoredProcedure };
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

        public DataTable Stock_Product_Group_Get(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_Product_Group_Get", connection) { CommandType = CommandType.StoredProcedure };
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

        public int Stock_Product_Group_Delete(int ID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Stock_Product_Group_Delete", connection) { CommandType = CommandType.StoredProcedure };
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

        public DataTable Stock_Product_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_Product_GetAll", connection) { CommandType = CommandType.StoredProcedure };
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

        public DataTable Stock_Product_Get(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_Product_Get", connection) { CommandType = CommandType.StoredProcedure };
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

        public DataTable Stock_Product_GetByCode(string Code)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_Product_GetByCode", connection) { CommandType = CommandType.StoredProcedure };
                    selectCommand.Parameters.AddWithValue("@Code", Code);
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

        public int Stock_Product_Delete(int ID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Stock_Product_Delete", connection) { CommandType = CommandType.StoredProcedure };
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

        public int Stock_Update(int ID, int ID_GameCenter, string Code, string Title, string Tel, string Address, string Des)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Stock_Update", connection) { CommandType = CommandType.StoredProcedure };
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@Code", Code);
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@Tel", Tel);
                    sqlCommand.Parameters.AddWithValue("@Address", Address);
                    sqlCommand.Parameters.AddWithValue("@Des", Des);
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

        public DataTable Stock_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_GetAll", connection) { CommandType = CommandType.StoredProcedure };
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

        public DataTable Stock_Get(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_Get", connection) { CommandType = CommandType.StoredProcedure };
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

        public int Stock_Delete(int ID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Stock_Delete", connection) { CommandType = CommandType.StoredProcedure };
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

        public int Stock_Supplier_Update(int ID, string Code, string Name, string LName, string Tel, string Mobile, string RegistrationCompanyID, string NationalCompanyID, string NationalCode, string Address, int Is_Company, int ID_Sex, string AliasName, string CompanyName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Stock_Supplier_Update", connection) { CommandType = CommandType.StoredProcedure };
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.Parameters.AddWithValue("@Code", Code);
                    sqlCommand.Parameters.AddWithValue("@Name", Name);
                    sqlCommand.Parameters.AddWithValue("@LName", LName);
                    sqlCommand.Parameters.AddWithValue("@Tel", Tel);
                    sqlCommand.Parameters.AddWithValue("@Mobile", Mobile);
                    sqlCommand.Parameters.AddWithValue("@RegistrationCompanyID", RegistrationCompanyID);
                    sqlCommand.Parameters.AddWithValue("@NationalCompanyID", NationalCompanyID);
                    sqlCommand.Parameters.AddWithValue("@NationalCode", NationalCode);
                    sqlCommand.Parameters.AddWithValue("@Address", Address);
                    sqlCommand.Parameters.AddWithValue("@Is_Company", Is_Company);
                    sqlCommand.Parameters.AddWithValue("@ID_Sex", ID_Sex);
                    sqlCommand.Parameters.AddWithValue("@AliasName", AliasName);
                    sqlCommand.Parameters.AddWithValue("@CompanyName", CompanyName);
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

        public DataTable Stock_Supplier_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_Supplier_GetAll", connection) { CommandType = CommandType.StoredProcedure };
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

        public DataTable Stock_Supplier_Get(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_Supplier_Get", connection) { CommandType = CommandType.StoredProcedure };
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

        public int Stock_Supplier_Delete(int ID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Stock_Supplier_Delete", connection) { CommandType = CommandType.StoredProcedure };
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

        public int Stock_Process_insert(int ID_Stock_Product, int ID_Stock_Supplier, int ID_Stock, int Count, DateTime Date, int FactorID, int Serial, string Des, int ID_Stock_Process_Type, int PriceBuy, int PriceSale)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Stock_Process_insert", connection) { CommandType = CommandType.StoredProcedure };
                    sqlCommand.Parameters.AddWithValue("@ID", (clsMain.Max_Tbl("Stock_Process", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@ID_Stock_Product", ID_Stock_Product);
                    sqlCommand.Parameters.AddWithValue("@ID_Stock_Supplier", ID_Stock_Supplier);
                    sqlCommand.Parameters.AddWithValue("@ID_Stock", ID_Stock);
                    sqlCommand.Parameters.AddWithValue("@Count", Count);
                    sqlCommand.Parameters.AddWithValue("@Date", Date);
                    sqlCommand.Parameters.AddWithValue("@FactorID", FactorID);
                    sqlCommand.Parameters.AddWithValue("@Serial", Serial);
                    sqlCommand.Parameters.AddWithValue("@Des", Des);
                    sqlCommand.Parameters.AddWithValue("@ID_Stock_Process_Type", ID_Stock_Process_Type);
                    sqlCommand.Parameters.AddWithValue("@IsDeleted", 0);
                    sqlCommand.Parameters.AddWithValue("@PriceBuy", PriceBuy);
                    sqlCommand.Parameters.AddWithValue("@PriceSale", PriceSale);
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

        public DataTable Stock_Process_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_Process_GetAll", connection) { CommandType = CommandType.StoredProcedure };
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

        public DataTable Stock_Process_Get(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_Process_Get", connection) { CommandType = CommandType.StoredProcedure };
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

        public int Stock_Process_Delete(int ID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Stock_Process_Delete", connection) { CommandType = CommandType.StoredProcedure };
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

        public int Stock_Product_Group_insert(string Title)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Stock_Product_Group_Insert", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@ID", (clsMain.Max_Tbl("Stock_Product_Group", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
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

        public int Stock_Product_insert(int ID_Product_Group, string Code, string Title, int MinCount, int MaxCount, int PriceBuy, int PriceSale, int Tax, int Complications, string Des, string ShortTitle, int ID_Unit, int TicketValue, int IS_Stocked, int Enable, int IsRedemption, int IsRetail, string SupplierIDs)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Stock_Product_Insert", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@ID", (clsMain.Max_Tbl("Stock_Product", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@ID_Product_Group", ID_Product_Group);
                    sqlCommand.Parameters.AddWithValue("@Code", Code);
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@MinCount", MinCount);
                    sqlCommand.Parameters.AddWithValue("@MaxCount", MaxCount);
                    sqlCommand.Parameters.AddWithValue("@PriceBuy", PriceBuy);
                    sqlCommand.Parameters.AddWithValue("@PriceSale", PriceSale);
                    sqlCommand.Parameters.AddWithValue("@Tax", Tax);
                    sqlCommand.Parameters.AddWithValue("@Complications", Complications);
                    sqlCommand.Parameters.AddWithValue("@Des", Des);
                    sqlCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@IsDeleted", 0);
                    sqlCommand.Parameters.AddWithValue("@ShortTitle", ShortTitle);
                    sqlCommand.Parameters.AddWithValue("@ID_Unit", ID_Unit);
                    sqlCommand.Parameters.AddWithValue("@TicketValue", TicketValue);
                    sqlCommand.Parameters.AddWithValue("@IS_Stocked", IS_Stocked);
                    sqlCommand.Parameters.AddWithValue("@Enable", Enable);
                    sqlCommand.Parameters.AddWithValue("@IsRedemption", IsRedemption);
                    sqlCommand.Parameters.AddWithValue("@IsRetail", IsRetail);
                    sqlCommand.Parameters.AddWithValue("@SupplierIDs", SupplierIDs);
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

        public int Stock_Product_Update(int ID, int ID_Product_Group, string Code, string Title, int MinCount, int MaxCount, int PriceBuy, int PriceSale, int Tax, int Complications, string Des, string ShortTitle, int ID_Unit, int TicketValue, int IS_Stocked, int Enable, int IsRedemption, int IsRetail, string SupplierIDs)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("stock_Product_Update", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.Parameters.AddWithValue("@ID_Product_Group", ID_Product_Group);
                    sqlCommand.Parameters.AddWithValue("@Code", Code);
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@MinCount", MinCount);
                    sqlCommand.Parameters.AddWithValue("@MaxCount", MaxCount);
                    sqlCommand.Parameters.AddWithValue("@PriceBuy", PriceBuy);
                    sqlCommand.Parameters.AddWithValue("@PriceSale", PriceSale);
                    sqlCommand.Parameters.AddWithValue("@Tax", Tax);
                    sqlCommand.Parameters.AddWithValue("@Complications", Complications);
                    sqlCommand.Parameters.AddWithValue("@Des", Des);
                    sqlCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@ShortTitle", ShortTitle);
                    sqlCommand.Parameters.AddWithValue("@ID_Unit", ID_Unit);
                    sqlCommand.Parameters.AddWithValue("@TicketValue", TicketValue);
                    sqlCommand.Parameters.AddWithValue("@IS_Stocked", IS_Stocked);
                    sqlCommand.Parameters.AddWithValue("@Enable", Enable);
                    sqlCommand.Parameters.AddWithValue("@IsRedemption", IsRedemption);
                    sqlCommand.Parameters.AddWithValue("@IsRetail", IsRetail);
                    sqlCommand.Parameters.AddWithValue("@SupplierIDs", SupplierIDs);
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

        public int Stock_insert(int ID_GameCenter, string Code, string Title, string Tel, string Address, string Des)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Stock_Insert", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@ID", (clsMain.Max_Tbl("Stock", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@Code", Code);
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@Tel", Tel);
                    sqlCommand.Parameters.AddWithValue("@Address", Address);
                    sqlCommand.Parameters.AddWithValue("@IsDeleted", 0);
                    sqlCommand.Parameters.AddWithValue("@Des", Des);
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

        public int Stock_Supplier_insert(string Code, string Name, string LName, string Tel, string Mobile, string RegistrationCompanyID, string NationalCompanyID, string NationalCode, string Address, int Is_Company, int ID_Sex, string AliasName, string CompanyName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Stock_Supplier_Insert", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@ID", (clsMain.Max_Tbl("Stock_Supplier", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@Code", Code);
                    sqlCommand.Parameters.AddWithValue("@Name", Name);
                    sqlCommand.Parameters.AddWithValue("@LName", LName);
                    sqlCommand.Parameters.AddWithValue("@Tel", Tel);
                    sqlCommand.Parameters.AddWithValue("@Mobile", Mobile);
                    sqlCommand.Parameters.AddWithValue("@RegistrationCompanyID", RegistrationCompanyID);
                    sqlCommand.Parameters.AddWithValue("@NationalCompanyID", NationalCompanyID);
                    sqlCommand.Parameters.AddWithValue("@NationalCode", NationalCode);
                    sqlCommand.Parameters.AddWithValue("@Address", Address);
                    sqlCommand.Parameters.AddWithValue("@IsDeleted", 0);
                    sqlCommand.Parameters.AddWithValue("@Is_Company", Is_Company);
                    sqlCommand.Parameters.AddWithValue("@ID_Sex", ID_Sex);
                    sqlCommand.Parameters.AddWithValue("@AliasName", AliasName);
                    sqlCommand.Parameters.AddWithValue("@CompanyName", CompanyName);
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

        public int Stock_Process_Update(int ID, int ID_Stock_Product, int ID_Stock_Supplier, int ID_Stock, int Count, DateTime Date, int FactorID, int Serial, string Des, int ID_Stock_Process_Type, int PriceBuy, int PriceSale)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Stock_Process_update", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.Parameters.AddWithValue("@ID_Stock_Product", ID_Stock_Product);
                    sqlCommand.Parameters.AddWithValue("@ID_Stock_Supplier", ID_Stock_Supplier);
                    sqlCommand.Parameters.AddWithValue("@ID_Stock", ID_Stock);
                    sqlCommand.Parameters.AddWithValue("@Count", Count);
                    sqlCommand.Parameters.AddWithValue("@Date", Date);
                    sqlCommand.Parameters.AddWithValue("@FactorID", FactorID);
                    sqlCommand.Parameters.AddWithValue("@Serial", Serial);
                    sqlCommand.Parameters.AddWithValue("@Des", Des);
                    sqlCommand.Parameters.AddWithValue("@ID_Stock_Process_Type", ID_Stock_Process_Type);
                    sqlCommand.Parameters.AddWithValue("@PriceBuy", PriceBuy);
                    sqlCommand.Parameters.AddWithValue("@PriceSale", PriceSale);
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

        public DataTable Stock_Process_GetByProsessType(int ProsessTypeID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_Process_Get_By_ProsessType", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    selectCommand.Parameters.AddWithValue("@ID_Stock_Process_Type", ProsessTypeID);
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

        public DataTable Stock_Process_GetByProsessType_ByFactorID(int ProsessTypeID, int FactorID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_Process_Get_By_ProsessType_FactorID", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    selectCommand.Parameters.AddWithValue("@ID_Stock_Process_Type", ProsessTypeID);
                    selectCommand.Parameters.AddWithValue("@FactorID", FactorID);
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

        public DataTable Stock_Process_GetByProsessType_OrderByFactorID(int ProsessTypeID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_Process_Get_By_ProsessType_OrderByFactorID", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    selectCommand.Parameters.AddWithValue("@ID_Stock_Process_Type", ProsessTypeID);
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

        public DataTable Stock_Process_GetByProsessTypes(string ProsessTypeIDs)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_Process_Get_By_ProsessTypes", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    selectCommand.Parameters.AddWithValue("@ID_Stock_Process_Types", ProsessTypeIDs);
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

        public DataTable Stock_Process_CalculateByProsessTypeID(int ProductID, int ProsessTypeID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("select * from Stock_Process where ID_Stock_Product=@ID_Stock_Product and ID_Stock_Process_Type=@ID_Stock_Process_Type and IsDeleted=0", connection);
                    selectCommand.Parameters.AddWithValue("@ID_Stock_Product", ProductID);
                    selectCommand.Parameters.AddWithValue("@ID_Stock_Process_Type", ProsessTypeID);
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

        public int Stock_Process_Transfer_insert(int ID_From_Stock, int ID_To_Stock, DateTime Date, int ID_Users, int ID_Stock_Product, int Count, int FactorID, int Serial, string Des)
        {
            try
            {
                int num = clsMain.Max_Tbl("Stock_Process_Transfer", "ID") + 1;
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Stock_Process_Transfer_Insert", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@ID", num);
                    sqlCommand.Parameters.AddWithValue("@ID_From_Stock", ID_From_Stock);
                    sqlCommand.Parameters.AddWithValue("@ID_To_Stock", ID_To_Stock);
                    sqlCommand.Parameters.AddWithValue("@Date", Date);
                    sqlCommand.Parameters.AddWithValue("@ID_Users", ID_Users);
                    sqlCommand.Parameters.AddWithValue("@ID_Stock_Product", ID_Stock_Product);
                    sqlCommand.Parameters.AddWithValue("@Count", Count);
                    sqlCommand.Parameters.AddWithValue("@IsDeleted", 0);
                    sqlCommand.Parameters.AddWithValue("@FactorID", FactorID);
                    sqlCommand.Parameters.AddWithValue("@Serial", Serial);
                    sqlCommand.Parameters.AddWithValue("@Des", Des);
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

        public DataTable Stock_Process_Transfer_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_Process_Transfer_GetAll", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
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

        public DataTable Stock_Product_Unit_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_Product_Unit_GetAll", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
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

        #endregion
    }
}
