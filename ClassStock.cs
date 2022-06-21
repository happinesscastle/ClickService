// Decompiled with JetBrains decompiler
// Type: ClickServerService.ClassStock
// Assembly: ClickServerService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6BDFD2F8-7BA8-4B8A-8EC1-401DFA893333
// Assembly location: C:\Users\Win10\Desktop\ClickServerService.exe

using System;
using System.Data;
using System.Data.SqlClient;

namespace ClickServerService
{
    internal class ClassStock
    {
        private MainClass objmain = new MainClass();

        public int Stock_Product_Group_insert(string Title)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Stock_Product_Group_Insert", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (object)(this.objmain.Max_Tbl("Stock_Product_Group", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@Title", (object)Title);
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

        public int Stock_Product_Group_Update(int ID, string Title)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Stock_Product_Group_Update), connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (object)ID);
                    sqlCommand.Parameters.AddWithValue("@Title", (object)Title);
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

        public DataTable Stock_Product_Group_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_Product_Group_GetAll", connection);
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

        public DataTable Stock_Product_Group_Get(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(Stock_Product_Group_Get), connection);
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

        public int Stock_Product_Group_Delete(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Stock_Product_Group_Delete), connection);
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

        public int Stock_Product_insert(
          int ID_Product_Group,
          string Code,
          string Title,
          int MinCount,
          int MaxCount,
          int PriceBuy,
          int PriceSale,
          int Tax,
          int Complications,
          string Des,
          string ShortTitle,
          int ID_Unit,
          int TicketValue,
          int IS_Stocked,
          int Enable,
          int IsRedemption,
          int IsRetail,
          string SupplierIDs)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Stock_Product_Insert", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (object)(this.objmain.Max_Tbl("Stock_Product", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@ID_Product_Group", (object)ID_Product_Group);
                    sqlCommand.Parameters.AddWithValue("@Code", (object)Code);
                    sqlCommand.Parameters.AddWithValue("@Title", (object)Title);
                    sqlCommand.Parameters.AddWithValue("@MinCount", (object)MinCount);
                    sqlCommand.Parameters.AddWithValue("@MaxCount", (object)MaxCount);
                    sqlCommand.Parameters.AddWithValue("@PriceBuy", (object)PriceBuy);
                    sqlCommand.Parameters.AddWithValue("@PriceSale", (object)PriceSale);
                    sqlCommand.Parameters.AddWithValue("@Tax", (object)Tax);
                    sqlCommand.Parameters.AddWithValue("@Complications", (object)Complications);
                    sqlCommand.Parameters.AddWithValue("@Des", (object)Des);
                    sqlCommand.Parameters.AddWithValue("@Date", (object)DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@IsDeleted", (object)0);
                    sqlCommand.Parameters.AddWithValue("@ShortTitle", (object)ShortTitle);
                    sqlCommand.Parameters.AddWithValue("@ID_Unit", (object)ID_Unit);
                    sqlCommand.Parameters.AddWithValue("@TicketValue", (object)TicketValue);
                    sqlCommand.Parameters.AddWithValue("@IS_Stocked", (object)IS_Stocked);
                    sqlCommand.Parameters.AddWithValue("@Enable", (object)Enable);
                    sqlCommand.Parameters.AddWithValue("@IsRedemption", (object)IsRedemption);
                    sqlCommand.Parameters.AddWithValue("@IsRetail", (object)IsRetail);
                    sqlCommand.Parameters.AddWithValue("@SupplierIDs", (object)SupplierIDs);
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

        public int Stock_Product_Update(
          int ID,
          int ID_Product_Group,
          string Code,
          string Title,
          int MinCount,
          int MaxCount,
          int PriceBuy,
          int PriceSale,
          int Tax,
          int Complications,
          string Des,
          string ShortTitle,
          int ID_Unit,
          int TicketValue,
          int IS_Stocked,
          int Enable,
          int IsRedemption,
          int IsRetail,
          string SupplierIDs)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("stock_Product_Update", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (object)ID);
                    sqlCommand.Parameters.AddWithValue("@ID_Product_Group", (object)ID_Product_Group);
                    sqlCommand.Parameters.AddWithValue("@Code", (object)Code);
                    sqlCommand.Parameters.AddWithValue("@Title", (object)Title);
                    sqlCommand.Parameters.AddWithValue("@MinCount", (object)MinCount);
                    sqlCommand.Parameters.AddWithValue("@MaxCount", (object)MaxCount);
                    sqlCommand.Parameters.AddWithValue("@PriceBuy", (object)PriceBuy);
                    sqlCommand.Parameters.AddWithValue("@PriceSale", (object)PriceSale);
                    sqlCommand.Parameters.AddWithValue("@Tax", (object)Tax);
                    sqlCommand.Parameters.AddWithValue("@Complications", (object)Complications);
                    sqlCommand.Parameters.AddWithValue("@Des", (object)Des);
                    sqlCommand.Parameters.AddWithValue("@Date", (object)DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@ShortTitle", (object)ShortTitle);
                    sqlCommand.Parameters.AddWithValue("@ID_Unit", (object)ID_Unit);
                    sqlCommand.Parameters.AddWithValue("@TicketValue", (object)TicketValue);
                    sqlCommand.Parameters.AddWithValue("@IS_Stocked", (object)IS_Stocked);
                    sqlCommand.Parameters.AddWithValue("@Enable", (object)Enable);
                    sqlCommand.Parameters.AddWithValue("@IsRedemption", (object)IsRedemption);
                    sqlCommand.Parameters.AddWithValue("@IsRetail", (object)IsRetail);
                    sqlCommand.Parameters.AddWithValue("@SupplierIDs", (object)SupplierIDs);
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

        public DataTable Stock_Product_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_Product_GetAll", connection);
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

        public DataTable Stock_Product_Get(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(Stock_Product_Get), connection);
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

        public DataTable Stock_Product_GetByCode(string Code)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(Stock_Product_GetByCode), connection);
                    selectCommand.Parameters.AddWithValue("@Code", (object)Code);
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

        public int Stock_Product_Delete(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Stock_Product_Delete), connection);
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

        public int Stock_insert(
          int ID_GameCenter,
          string Code,
          string Title,
          string Tel,
          string Address,
          string Des)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Stock_Insert", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (object)(this.objmain.Max_Tbl("Stock", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", (object)ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@Code", (object)Code);
                    sqlCommand.Parameters.AddWithValue("@Title", (object)Title);
                    sqlCommand.Parameters.AddWithValue("@Tel", (object)Tel);
                    sqlCommand.Parameters.AddWithValue("@Address", (object)Address);
                    sqlCommand.Parameters.AddWithValue("@IsDeleted", (object)0);
                    sqlCommand.Parameters.AddWithValue("@Des", (object)Des);
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

        public int Stock_Update(
          int ID,
          int ID_GameCenter,
          string Code,
          string Title,
          string Tel,
          string Address,
          string Des)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Stock_Update), connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (object)ID);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", (object)ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@Code", (object)Code);
                    sqlCommand.Parameters.AddWithValue("@Title", (object)Title);
                    sqlCommand.Parameters.AddWithValue("@Tel", (object)Tel);
                    sqlCommand.Parameters.AddWithValue("@Address", (object)Address);
                    sqlCommand.Parameters.AddWithValue("@Des", (object)Des);
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

        public DataTable Stock_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_GetAll", connection);
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

        public DataTable Stock_Get(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(Stock_Get), connection);
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

        public int Stock_Delete(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Stock_Delete), connection);
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

        public int Stock_Supplier_insert(
          string Code,
          string Name,
          string LName,
          string Tel,
          string Mobile,
          string RegistrationCompanyID,
          string NationalCompanyID,
          string NationalCode,
          string Address,
          int Is_Company,
          int ID_Sex,
          string AliasName,
          string CompanyName)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Stock_Supplier_Insert", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (object)(this.objmain.Max_Tbl("Stock_Supplier", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@Code", (object)Code);
                    sqlCommand.Parameters.AddWithValue("@Name", (object)Name);
                    sqlCommand.Parameters.AddWithValue("@LName", (object)LName);
                    sqlCommand.Parameters.AddWithValue("@Tel", (object)Tel);
                    sqlCommand.Parameters.AddWithValue("@Mobile", (object)Mobile);
                    sqlCommand.Parameters.AddWithValue("@RegistrationCompanyID", (object)RegistrationCompanyID);
                    sqlCommand.Parameters.AddWithValue("@NationalCompanyID", (object)NationalCompanyID);
                    sqlCommand.Parameters.AddWithValue("@NationalCode", (object)NationalCode);
                    sqlCommand.Parameters.AddWithValue("@Address", (object)Address);
                    sqlCommand.Parameters.AddWithValue("@IsDeleted", (object)0);
                    sqlCommand.Parameters.AddWithValue("@Is_Company", (object)Is_Company);
                    sqlCommand.Parameters.AddWithValue("@ID_Sex", (object)ID_Sex);
                    sqlCommand.Parameters.AddWithValue("@AliasName", (object)AliasName);
                    sqlCommand.Parameters.AddWithValue("@CompanyName", (object)CompanyName);
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

        public int Stock_Supplier_Update(
          int ID,
          string Code,
          string Name,
          string LName,
          string Tel,
          string Mobile,
          string RegistrationCompanyID,
          string NationalCompanyID,
          string NationalCode,
          string Address,
          int Is_Company,
          int ID_Sex,
          string AliasName,
          string CompanyName)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Stock_Supplier_Update), connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (object)ID);
                    sqlCommand.Parameters.AddWithValue("@Code", (object)Code);
                    sqlCommand.Parameters.AddWithValue("@Name", (object)Name);
                    sqlCommand.Parameters.AddWithValue("@LName", (object)LName);
                    sqlCommand.Parameters.AddWithValue("@Tel", (object)Tel);
                    sqlCommand.Parameters.AddWithValue("@Mobile", (object)Mobile);
                    sqlCommand.Parameters.AddWithValue("@RegistrationCompanyID", (object)RegistrationCompanyID);
                    sqlCommand.Parameters.AddWithValue("@NationalCompanyID", (object)NationalCompanyID);
                    sqlCommand.Parameters.AddWithValue("@NationalCode", (object)NationalCode);
                    sqlCommand.Parameters.AddWithValue("@Address", (object)Address);
                    sqlCommand.Parameters.AddWithValue("@Is_Company", (object)Is_Company);
                    sqlCommand.Parameters.AddWithValue("@ID_Sex", (object)ID_Sex);
                    sqlCommand.Parameters.AddWithValue("@AliasName", (object)AliasName);
                    sqlCommand.Parameters.AddWithValue("@CompanyName", (object)CompanyName);
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

        public DataTable Stock_Supplier_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_Supplier_GetAll", connection);
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

        public DataTable Stock_Supplier_Get(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(Stock_Supplier_Get), connection);
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

        public int Stock_Supplier_Delete(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Stock_Supplier_Delete), connection);
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

        public int Stock_Process_insert(
          int ID_Stock_Product,
          int ID_Stock_Supplier,
          int ID_Stock,
          int Count,
          DateTime Date,
          int FactorID,
          int Serial,
          string Des,
          int ID_Stock_Process_Type,
          int PriceBuy,
          int PriceSale)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Stock_Process_insert), connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (object)(this.objmain.Max_Tbl("Stock_Process", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@ID_Stock_Product", (object)ID_Stock_Product);
                    sqlCommand.Parameters.AddWithValue("@ID_Stock_Supplier", (object)ID_Stock_Supplier);
                    sqlCommand.Parameters.AddWithValue("@ID_Stock", (object)ID_Stock);
                    sqlCommand.Parameters.AddWithValue("@Count", (object)Count);
                    sqlCommand.Parameters.AddWithValue("@Date", (object)Date);
                    sqlCommand.Parameters.AddWithValue("@FactorID", (object)FactorID);
                    sqlCommand.Parameters.AddWithValue("@Serial", (object)Serial);
                    sqlCommand.Parameters.AddWithValue("@Des", (object)Des);
                    sqlCommand.Parameters.AddWithValue("@ID_Stock_Process_Type", (object)ID_Stock_Process_Type);
                    sqlCommand.Parameters.AddWithValue("@IsDeleted", (object)0);
                    sqlCommand.Parameters.AddWithValue("@PriceBuy", (object)PriceBuy);
                    sqlCommand.Parameters.AddWithValue("@PriceSale", (object)PriceSale);
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

        public int Stock_Process_Update(
          int ID,
          int ID_Stock_Product,
          int ID_Stock_Supplier,
          int ID_Stock,
          int Count,
          DateTime Date,
          int FactorID,
          int Serial,
          string Des,
          int ID_Stock_Process_Type,
          int PriceBuy,
          int PriceSale)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Stock_Process_update", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (object)ID);
                    sqlCommand.Parameters.AddWithValue("@ID_Stock_Product", (object)ID_Stock_Product);
                    sqlCommand.Parameters.AddWithValue("@ID_Stock_Supplier", (object)ID_Stock_Supplier);
                    sqlCommand.Parameters.AddWithValue("@ID_Stock", (object)ID_Stock);
                    sqlCommand.Parameters.AddWithValue("@Count", (object)Count);
                    sqlCommand.Parameters.AddWithValue("@Date", (object)Date);
                    sqlCommand.Parameters.AddWithValue("@FactorID", (object)FactorID);
                    sqlCommand.Parameters.AddWithValue("@Serial", (object)Serial);
                    sqlCommand.Parameters.AddWithValue("@Des", (object)Des);
                    sqlCommand.Parameters.AddWithValue("@ID_Stock_Process_Type", (object)ID_Stock_Process_Type);
                    sqlCommand.Parameters.AddWithValue("@PriceBuy", (object)PriceBuy);
                    sqlCommand.Parameters.AddWithValue("@PriceSale", (object)PriceSale);
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

        public DataTable Stock_Process_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_Process_GetAll", connection);
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

        public DataTable Stock_Process_Get(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(Stock_Process_Get), connection);
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

        public DataTable Stock_Process_GetByProsessType(int ProsessTypeID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_Process_Get_By_ProsessType", connection);
                    selectCommand.Parameters.AddWithValue("@ID_Stock_Process_Type", (object)ProsessTypeID);
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

        public DataTable Stock_Process_GetByProsessType_ByFactorID(
          int ProsessTypeID,
          int FactorID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_Process_Get_By_ProsessType_FactorID", connection);
                    selectCommand.Parameters.AddWithValue("@ID_Stock_Process_Type", (object)ProsessTypeID);
                    selectCommand.Parameters.AddWithValue("@FactorID", (object)FactorID);
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

        public DataTable Stock_Process_GetByProsessType_OrderByFactorID(int ProsessTypeID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_Process_Get_By_ProsessType_OrderByFactorID", connection);
                    selectCommand.Parameters.AddWithValue("@ID_Stock_Process_Type", (object)ProsessTypeID);
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

        public DataTable Stock_Process_GetByProsessTypes(string ProsessTypeIDs)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_Process_Get_By_ProsessTypes", connection);
                    selectCommand.Parameters.AddWithValue("@ID_Stock_Process_Types", (object)ProsessTypeIDs);
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

        public int Stock_Process_Delete(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Stock_Process_Delete), connection);
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

        public DataTable Stock_Process_CalculateByProsessTypeID(
          int ProductID,
          int ProsessTypeID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("select * from Stock_Process where ID_Stock_Product=@ID_Stock_Product and ID_Stock_Process_Type=@ID_Stock_Process_Type and IsDeleted=0", connection);
                    selectCommand.Parameters.AddWithValue("@ID_Stock_Product", (object)ProductID);
                    selectCommand.Parameters.AddWithValue("@ID_Stock_Process_Type", (object)ProsessTypeID);
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

        public int Stock_Process_Transfer_insert(
          int ID_From_Stock,
          int ID_To_Stock,
          DateTime Date,
          int ID_Users,
          int ID_Stock_Product,
          int Count,
          int FactorID,
          int Serial,
          string Des)
        {
            DataTable dataTable = new DataTable();
            try
            {
                int num = this.objmain.Max_Tbl("Stock_Process_Transfer", "ID") + 1;
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Stock_Process_Transfer_Insert", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (object)num);
                    sqlCommand.Parameters.AddWithValue("@ID_From_Stock", (object)ID_From_Stock);
                    sqlCommand.Parameters.AddWithValue("@ID_To_Stock", (object)ID_To_Stock);
                    sqlCommand.Parameters.AddWithValue("@Date", (object)Date);
                    sqlCommand.Parameters.AddWithValue("@ID_Users", (object)ID_Users);
                    sqlCommand.Parameters.AddWithValue("@ID_Stock_Product", (object)ID_Stock_Product);
                    sqlCommand.Parameters.AddWithValue("@Count", (object)Count);
                    sqlCommand.Parameters.AddWithValue("@IsDeleted", (object)0);
                    sqlCommand.Parameters.AddWithValue("@FactorID", (object)FactorID);
                    sqlCommand.Parameters.AddWithValue("@Serial", (object)Serial);
                    sqlCommand.Parameters.AddWithValue("@Des", (object)Des);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
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

        public DataTable Stock_Process_Transfer_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_Process_Transfer_GetAll", connection);
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

        public DataTable Stock_Product_Unit_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Stock_Product_Unit_GetAll", connection);
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
    }
}
