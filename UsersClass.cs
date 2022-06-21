// Decompiled with JetBrains decompiler
// Type: ClickServerService.UsersClass
// Assembly: ClickServerService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6BDFD2F8-7BA8-4B8A-8EC1-401DFA893333
// Assembly location: C:\Users\Win10\Desktop\ClickServerService.exe

using System;
using System.Data;
using System.Data.SqlClient;

namespace ClickServerService
{
  internal class UsersClass
  {
    private MainClass objmain = new MainClass();

    public DataTable Permision_GetByParentID(int ParentID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("Permision_GetbyParent", connection);
          selectCommand.Parameters.AddWithValue("@ParentID", (object) ParentID);
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

    public int Permision_insert(string Title, int ParentID, string Path)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand("\r\n                        INSERT INTO [Permision]\r\n                       ([ID]\r\n                       ,[Title]\r\n                       ,[ParentID]\r\n                       ,[Path])\r\n                         VALUES\r\n                       (@ID\r\n                       ,@Title\r\n                       ,@ParentID\r\n                       ,@Path)", connection);
          sqlCommand.Parameters.AddWithValue("@ID", (object) (this.objmain.Max_Tbl("Permision", "ID") + 1));
          sqlCommand.Parameters.AddWithValue("@Title", (object) Title);
          sqlCommand.Parameters.AddWithValue("@ParentID", (object) ParentID);
          sqlCommand.Parameters.AddWithValue("@Path", (object) Path);
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

    public DataTable Permision_GetByPath(string Path)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("Permision_GetbyPath", connection);
          selectCommand.Parameters.AddWithValue("@Path", (object) Path);
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

    public int Users_insert(
      string Name,
      string LName,
      string Tel,
      string Address,
      string UserName,
      string Password,
      string RFIDcard,
      int Enable,
      string PersonnelCode,
      int ID_GameCenter,
      int ID_UsersGroup)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand("\r\nINSERT INTO [Users]\r\n           ([ID]\r\n           ,[Name]\r\n           ,[LName]\r\n           ,[Tel]\r\n           ,[Address]\r\n           ,[UserName]\r\n           ,[Password]\r\n           ,[RFIDcard]\r\n           ,[Date]\r\n           ,[Enable]\r\n           ,[IsDeleted]\r\n           ,[PersonnelCode],[ID_GameCenter],[ID_UsersGroup])\r\n     VALUES\r\n           (@ID\r\n           ,@Name\r\n           ,@LName\r\n           ,@Tel\r\n           ,@Address\r\n           ,@UserName\r\n           ,@Password\r\n           ,@RFIDcard\r\n           ,@Date\r\n           ,@Enable\r\n           ,@IsDeleted,@PersonnelCode,@ID_GameCenter,@ID_UsersGroup)", connection);
          sqlCommand.Parameters.AddWithValue("@ID", (object) (this.objmain.Max_Tbl("Users", "ID") + 1));
          sqlCommand.Parameters.AddWithValue("@Name", (object) Name);
          sqlCommand.Parameters.AddWithValue("@LName", (object) LName);
          sqlCommand.Parameters.AddWithValue("@Tel", (object) Tel);
          sqlCommand.Parameters.AddWithValue("@Address", (object) Address);
          sqlCommand.Parameters.AddWithValue("@UserName", (object) UserName);
          sqlCommand.Parameters.AddWithValue("@Password", (object) Password);
          sqlCommand.Parameters.AddWithValue("@RFIDcard", (object) RFIDcard);
          sqlCommand.Parameters.AddWithValue("@Date", (object) DateTime.Now);
          sqlCommand.Parameters.AddWithValue("@Enable", (object) Enable);
          sqlCommand.Parameters.AddWithValue("@IsDeleted", (object) 0);
          sqlCommand.Parameters.AddWithValue("@PersonnelCode", (object) PersonnelCode);
          sqlCommand.Parameters.AddWithValue("@ID_GameCenter", (object) ID_GameCenter);
          sqlCommand.Parameters.AddWithValue("@ID_UsersGroup", (object) ID_UsersGroup);
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

    public int Users_Update(
      int ID,
      string Name,
      string LName,
      string Tel,
      string Address,
      string UserName,
      string Password,
      string RFIDcard,
      int Enable,
      string PersonnelCode,
      int ID_GameCenter,
      int ID_UsersGroup)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand("\r\nupdate  [Users] set\r\n           [Name]=@Name\r\n           ,[LName]=@LName\r\n           ,[Tel]=@Tel\r\n           ,[Address]=@Address\r\n           ,[UserName]=@UserName\r\n           ,[Password]=@Password\r\n           ,[RFIDcard]=@RFIDcard\r\n\r\n           ,[Enable]=@Enable\r\n    \r\n           ,[PersonnelCode]=@PersonnelCode,[ID_GameCenter]=@ID_GameCenter,[ID_UsersGroup]=@ID_UsersGroup where [ID]=@ID ", connection);
          sqlCommand.Parameters.AddWithValue("@ID", (object) ID);
          sqlCommand.Parameters.AddWithValue("@Name", (object) Name);
          sqlCommand.Parameters.AddWithValue("@LName", (object) LName);
          sqlCommand.Parameters.AddWithValue("@Tel", (object) Tel);
          sqlCommand.Parameters.AddWithValue("@Address", (object) Address);
          sqlCommand.Parameters.AddWithValue("@UserName", (object) UserName);
          sqlCommand.Parameters.AddWithValue("@Password", (object) Password);
          sqlCommand.Parameters.AddWithValue("@RFIDcard", (object) RFIDcard);
          sqlCommand.Parameters.AddWithValue("@Enable", (object) Enable);
          sqlCommand.Parameters.AddWithValue("@PersonnelCode", (object) PersonnelCode);
          sqlCommand.Parameters.AddWithValue("@ID_GameCenter", (object) ID_GameCenter);
          sqlCommand.Parameters.AddWithValue("@ID_UsersGroup", (object) ID_UsersGroup);
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

    public DataTable Users_Get()
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("Users_GetAll", connection);
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

    public DataTable Users_GetByGUID(string Card_GUID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("select * from Users where Card_GUID=@Card_GUID and Enable=1 ", connection);
          selectCommand.Parameters.AddWithValue(nameof (Card_GUID), (object) Card_GUID);
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

    public DataTable Users_Get(int ID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand(nameof (Users_Get), connection);
          selectCommand.Parameters.AddWithValue("@ID", (object) ID);
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

    public int Users_Delete(int ID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand(nameof (Users_Delete), connection);
          sqlCommand.CommandType = CommandType.StoredProcedure;
          sqlCommand.Parameters.AddWithValue("@ID", (object) ID);
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

    public int Users_Groups_insert(string Title, string PermisionList)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand("\r\nINSERT INTO [dbo].[Users_Groups]\r\n           ([ID]\r\n           ,[Title]\r\n           ,[PermisionList])\r\n     VALUES\r\n           (@ID\r\n           ,@Title\r\n           ,@PermisionList)", connection);
          sqlCommand.Parameters.AddWithValue("@ID", (object) (this.objmain.Max_Tbl("Users_Groups", "ID") + 1));
          sqlCommand.Parameters.AddWithValue("@Title", (object) Title);
          sqlCommand.Parameters.AddWithValue("@PermisionList", (object) PermisionList);
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

    public int Users_Groups_Update(int ID, string Title, string PermisionList)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand("\r\n        update  [Users_Groups] set [Title]=@Title,[PermisionList]=@PermisionList\r\n        where [ID]=@ID ", connection);
          sqlCommand.Parameters.AddWithValue("@ID", (object) ID);
          sqlCommand.Parameters.AddWithValue("@Title", (object) Title);
          sqlCommand.Parameters.AddWithValue("@PermisionList", (object) PermisionList);
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

    public DataTable Users_Groups_Get()
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("Users_Groups_GetAll", connection);
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

    public DataTable Users_Groups_Get(int ID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand(nameof (Users_Groups_Get), connection);
          selectCommand.Parameters.AddWithValue("@ID", (object) ID);
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

    public int Users_Groups_Delete(int ID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand(nameof (Users_Groups_Delete), connection);
          sqlCommand.CommandType = CommandType.StoredProcedure;
          sqlCommand.Parameters.AddWithValue("@ID", (object) ID);
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
