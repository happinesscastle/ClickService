using System.Data.SqlClient;
using System.Data;
using System;

namespace ClickServerService
{
    internal class ProductTemplateClass
    {
        #region ' Useless '

        private readonly MainClass objMain = new MainClass();

        public DataTable ProductTemplate_Group_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    new SqlDataAdapter(new SqlCommand("select * from ProductTemplate_Group where IsDeleted=0", connection)).Fill(dataTable);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return dataTable;
            }
        }

        public int ProductTemplate_Group_insert(string title)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[ProductTemplate_Group] ([ID] ,[Title] ,[IsDeleted]) VALUES (@ID ,@Title ,@IsDeleted)", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (objMain.Max_Tbl("ProductTemplate_Group", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@Title", title);
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

        public int ProductTemplate_Group_Update(int id, string title)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("update [ProductTemplate_Group] set [Title]=@Title where [ID]=@ID ", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", id);
                    sqlCommand.Parameters.AddWithValue("@Title", title);
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

        public DataTable ProductTemplate_Group_Get(int id)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("select * from ProductTemplate_Group where IsDeleted=0 and ID=@ID", connection);
                    selectCommand.Parameters.AddWithValue("@ID", id);
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

        public int ProductTemplate_Group_Delete(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("update ProductTemplate_Group set IsDeleted=1 where ID=@ID ", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", id);
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

        public DataTable ProductTemplate_SubGroup_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    new SqlDataAdapter(new SqlCommand("select * from ProductTemplate_SubGroup where IsDeleted=0", connection)).Fill(dataTable);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return dataTable;
            }
        }

        public int ProductTemplate_SubGroup_insert(string title, int id_ProductTemplate_Group)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[ProductTemplate_SubGroup] ([ID] ,[Title] ,[ID_ProductTemplate_Group] ,[IsDeleted]) VALUES (@ID ,@Title ,@ID_ProductTemplate_Group ,@IsDeleted)", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (objMain.Max_Tbl("ProductTemplate_SubGroup", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@Title", title);
                    sqlCommand.Parameters.AddWithValue("@ID_ProductTemplate_Group", id_ProductTemplate_Group);
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

        public int ProductTemplate_SubGroup_Update(int id, string title, int id_ProductTemplate_Group)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("update [ProductTemplate_SubGroup] set [Title]=@Title ,[ID_ProductTemplate_Group]=@ID_ProductTemplate_Group where [ID]=@ID ", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", id);
                    sqlCommand.Parameters.AddWithValue("@Title", title);
                    sqlCommand.Parameters.AddWithValue("@ID_ProductTemplate_Group", id_ProductTemplate_Group);
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

        public DataTable ProductTemplate_SubGroup_Get(int id)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("select * from ProductTemplate_SubGroup where IsDeleted=0 and ID=@ID", connection);
                    selectCommand.Parameters.AddWithValue("@ID", id);
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

        public DataTable ProductTemplate_SubGroup_GetByGroup(int ID_ProductTemplate_Group)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("select * from ProductTemplate_SubGroup where IsDeleted=0 and ID_ProductTemplate_Group=@ID_ProductTemplate_Group", connection);
                    selectCommand.Parameters.AddWithValue("@ID_ProductTemplate_Group", ID_ProductTemplate_Group);
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

        public int ProductTemplate_SubGroup_Delete(int ID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("update ProductTemplate_SubGroup set IsDeleted=1 where ID=@ID ", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
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

        public DataTable ProductTemplate_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    new SqlDataAdapter(new SqlCommand("select * from ProductTemplate where IsDeleted=0", connection)).Fill(dataTable);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return dataTable;
            }
        }

        public int ProductTemplate_insert(string Title, string TextColor, string BackColor, int ProductType, string ProductBarcode, int ProductID, string Priority, int ID_ProductTemplate_SubGroup)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO  [ProductTemplate] ([ID] ,[Title] ,[TextColor] ,[BackColor] ,[ProductType] ,[ProductBarcode] ,[ProductID] ,[Priority] ,[ID_ProductTemplate_SubGroup] ,[IsDeleted]) VALUES (@ID ,@Title ,@TextColor ,@BackColor ,@ProductType ,@ProductBarcode ,@ProductID ,@Priority ,@ID_ProductTemplate_SubGroup ,@IsDeleted)", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (objMain.Max_Tbl("ProductTemplate", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@TextColor", TextColor);
                    sqlCommand.Parameters.AddWithValue("@BackColor", BackColor);
                    sqlCommand.Parameters.AddWithValue("@ProductType", ProductType);
                    sqlCommand.Parameters.AddWithValue("@ProductBarcode", ProductBarcode);
                    sqlCommand.Parameters.AddWithValue("@ProductID", ProductID);
                    sqlCommand.Parameters.AddWithValue("@Priority", Priority);
                    sqlCommand.Parameters.AddWithValue("@ID_ProductTemplate_SubGroup", ID_ProductTemplate_SubGroup);
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

        public int ProductTemplate_Update(int ID, string Title, string TextColor, string BackColor, int ProductType, string ProductBarcode, int ProductID, string Priority, int ID_ProductTemplate_SubGroup)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("update [ProductTemplate] set [Title]=@Title ,[TextColor]=@TextColor ,[BackColor]=@BackColor ,[ProductType]=@ProductType ,[ProductBarcode]=@ProductBarcode ,[ProductID]=@ProductID ,[Priority]=@Priority ,[ID_ProductTemplate_SubGroup]=@ID_ProductTemplate_SubGroup where [ID]=@ID ", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@TextColor", TextColor);
                    sqlCommand.Parameters.AddWithValue("@BackColor", BackColor);
                    sqlCommand.Parameters.AddWithValue("@ProductType", ProductType);
                    sqlCommand.Parameters.AddWithValue("@ProductBarcode", ProductBarcode);
                    sqlCommand.Parameters.AddWithValue("@ProductID", ProductID);
                    sqlCommand.Parameters.AddWithValue("@Priority", Priority);
                    sqlCommand.Parameters.AddWithValue("@ID_ProductTemplate_SubGroup", ID_ProductTemplate_SubGroup);
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

        public DataTable ProductTemplate_Get(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("select * from ProductTemplate where IsDeleted=0 and [ID]=@ID", connection);
                    selectCommand.Parameters.AddWithValue("@ID", ID);
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

        public DataTable ProductTemplate_GetBySubGroup(int ID_ProductTemplate_SubGroup)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("select * from ProductTemplate where IsDeleted=0 and [ID_ProductTemplate_SubGroup]=@ID_ProductTemplate_SubGroup", connection);
                    selectCommand.Parameters.AddWithValue("@ID_ProductTemplate_SubGroup", ID_ProductTemplate_SubGroup);
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

        #endregion
    }
}
