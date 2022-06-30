using System.Data.SqlClient;
using System.Data;
using System;

namespace ClickServerService
{
    internal class UsersClass
    {
        private readonly MainClass objMain = new MainClass();

        public DataTable Users_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Users_GetAll", connection) { CommandType = CommandType.StoredProcedure };
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

        public DataTable Users_GetByGUID(string Card_GUID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Select * from Users where Card_GUID=@Card_GUID and Enable=1 ", connection);
                    selectCommand.Parameters.AddWithValue(nameof(Card_GUID), Card_GUID);
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

        public DataTable Users_Get(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(Users_Get), connection) { CommandType = CommandType.StoredProcedure };
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

        public int Users_Delete(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Users_Delete), connection) { CommandType = CommandType.StoredProcedure };
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

        public DataTable Users_Groups_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Users_Groups_GetAll", connection) { CommandType = CommandType.StoredProcedure };
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

        public DataTable Users_Groups_Get(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(Users_Groups_Get), connection) { CommandType = CommandType.StoredProcedure };
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

        public int Users_Groups_Delete(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Users_Groups_Delete), connection) { CommandType = CommandType.StoredProcedure };
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

        #region ' Useless '

        public DataTable Permision_GetByParentID(int ParentID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Permision_GetbyParent", connection) { CommandType = CommandType.StoredProcedure };
                    selectCommand.Parameters.AddWithValue("@ParentID", ParentID);
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

        public int Permision_insert(string Title, int ParentID, string Path)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO [Permision] ([ID] ,[Title] ,[ParentID] ,[Path]) VALUES (@ID ,@Title ,@ParentID ,@Path)", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (objMain.Max_Tbl("Permision", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@ParentID", ParentID);
                    sqlCommand.Parameters.AddWithValue("@Path", Path);
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

        public DataTable Permision_GetByPath(string Path)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Permision_GetbyPath", connection) { CommandType = CommandType.StoredProcedure };
                    selectCommand.Parameters.AddWithValue("@Path", Path);
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

        public int Users_insert(string Name, string LName, string Tel, string Address, string UserName, string Password, string RFIDcard, int Enable, string PersonnelCode, int ID_GameCenter, int ID_UsersGroup)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO [Users] ([ID] ,[Name] ,[LName] ,[Tel] ,[Address] ,[UserName] ,[Password] ,[RFIDcard] ,[Date] ,[Enable] ,[IsDeleted] ,[PersonnelCode],[ID_GameCenter],[ID_UsersGroup]) VALUES (@ID ,@Name ,@LName ,@Tel ,@Address ,@UserName ,@Password ,@RFIDcard ,@Date ,@Enable ,@IsDeleted,@PersonnelCode,@ID_GameCenter,@ID_UsersGroup)", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (objMain.Max_Tbl("Users", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@Name", Name);
                    sqlCommand.Parameters.AddWithValue("@LName", LName);
                    sqlCommand.Parameters.AddWithValue("@Tel", Tel);
                    sqlCommand.Parameters.AddWithValue("@Address", Address);
                    sqlCommand.Parameters.AddWithValue("@UserName", UserName);
                    sqlCommand.Parameters.AddWithValue("@Password", Password);
                    sqlCommand.Parameters.AddWithValue("@RFIDcard", RFIDcard);
                    sqlCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@Enable", Enable);
                    sqlCommand.Parameters.AddWithValue("@IsDeleted", 0);
                    sqlCommand.Parameters.AddWithValue("@PersonnelCode", PersonnelCode);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@ID_UsersGroup", ID_UsersGroup);
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

        public int Users_Update(int ID, string Name, string LName, string Tel, string Address, string UserName, string Password, string RFIDcard, int Enable, string PersonnelCode, int ID_GameCenter, int ID_UsersGroup)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("update [Users] set [Name]=@Name ,[LName]=@LName ,[Tel]=@Tel ,[Address]=@Address ,[UserName]=@UserName ,[Password]=@Password ,[RFIDcard]=@RFIDcard ,[Enable]=@Enable ,[PersonnelCode]=@PersonnelCode,[ID_GameCenter]=@ID_GameCenter,[ID_UsersGroup]=@ID_UsersGroup where [ID]=@ID ", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.Parameters.AddWithValue("@Name", Name);
                    sqlCommand.Parameters.AddWithValue("@LName", LName);
                    sqlCommand.Parameters.AddWithValue("@Tel", Tel);
                    sqlCommand.Parameters.AddWithValue("@Address", Address);
                    sqlCommand.Parameters.AddWithValue("@UserName", UserName);
                    sqlCommand.Parameters.AddWithValue("@Password", Password);
                    sqlCommand.Parameters.AddWithValue("@RFIDcard", RFIDcard);
                    sqlCommand.Parameters.AddWithValue("@Enable", Enable);
                    sqlCommand.Parameters.AddWithValue("@PersonnelCode", PersonnelCode);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@ID_UsersGroup", ID_UsersGroup);
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

        public int Users_Groups_insert(string Title, string PermisionList)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[Users_Groups] ([ID] ,[Title] ,[PermisionList]) VALUES (@ID ,@Title ,@PermisionList)", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (objMain.Max_Tbl("Users_Groups", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@PermisionList", PermisionList);
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

        public int Users_Groups_Update(int ID, string Title, string PermisionList)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Update [Users_Groups] set [Title]=@Title,[PermisionList]=@PermisionList Where [ID]=@ID ", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@PermisionList", PermisionList);
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
