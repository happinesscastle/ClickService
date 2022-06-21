// Decompiled with JetBrains decompiler
// Type: ClickServerService.ProductTemplateClass
// Assembly: ClickServerService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6BDFD2F8-7BA8-4B8A-8EC1-401DFA893333
// Assembly location: C:\Users\Win10\Desktop\ClickServerService.exe

using System;
using System.Data;
using System.Data.SqlClient;

namespace ClickServerService
{
  internal class ProductTemplateClass
  {
    private MainClass objmain = new MainClass();

    public DataTable ProductTemplate_Group_Get()
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          new SqlDataAdapter(new SqlCommand("select * from ProductTemplate_Group where IsDeleted=0", connection)).Fill(dataTable);
        }
        return dataTable;
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return dataTable;
      }
    }

    public int ProductTemplate_Group_insert(string Title)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[ProductTemplate_Group]\r\n           ([ID]\r\n           ,[Title]\r\n           ,[IsDeleted])\r\n     VALUES\r\n           (@ID\r\n           ,@Title\r\n           ,@IsDeleted)", connection);
          sqlCommand.Parameters.AddWithValue("@ID", (object) (this.objmain.Max_Tbl("ProductTemplate_Group", "ID") + 1));
          sqlCommand.Parameters.AddWithValue("@Title", (object) Title);
          sqlCommand.Parameters.AddWithValue("@IsDeleted", (object) 0);
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

    public int ProductTemplate_Group_Update(int ID, string Title)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand("update [ProductTemplate_Group] set [Title]=@Title where [ID]=@ID ", connection);
          sqlCommand.Parameters.AddWithValue("@ID", (object) ID);
          sqlCommand.Parameters.AddWithValue("@Title", (object) Title);
          sqlCommand.Parameters.AddWithValue("@IsDeleted", (object) 0);
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

    public DataTable ProductTemplate_Group_Get(int ID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("select * from ProductTemplate_Group where IsDeleted=0 and ID=@ID", connection);
          selectCommand.Parameters.AddWithValue("@ID", (object) ID);
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

    public int ProductTemplate_Group_Delete(int ID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand("update ProductTemplate_Group set IsDeleted=1 where ID=@ID ", connection);
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

    public DataTable ProductTemplate_SubGroup_Get()
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          new SqlDataAdapter(new SqlCommand("select * from ProductTemplate_SubGroup where IsDeleted=0", connection)).Fill(dataTable);
        }
        return dataTable;
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return dataTable;
      }
    }

    public int ProductTemplate_SubGroup_insert(string Title, int ID_ProductTemplate_Group)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[ProductTemplate_SubGroup]\r\n           ([ID]\r\n           ,[Title]\r\n        ,[ID_ProductTemplate_Group]\r\n           ,[IsDeleted])\r\n     VALUES\r\n           (@ID\r\n           ,@Title\r\n            ,@ID_ProductTemplate_Group\r\n           ,@IsDeleted)", connection);
          sqlCommand.Parameters.AddWithValue("@ID", (object) (this.objmain.Max_Tbl("ProductTemplate_SubGroup", "ID") + 1));
          sqlCommand.Parameters.AddWithValue("@Title", (object) Title);
          sqlCommand.Parameters.AddWithValue("@ID_ProductTemplate_Group", (object) ID_ProductTemplate_Group);
          sqlCommand.Parameters.AddWithValue("@IsDeleted", (object) 0);
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

    public int ProductTemplate_SubGroup_Update(int ID, string Title, int ID_ProductTemplate_Group)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand("update [ProductTemplate_SubGroup] set [Title]=@Title ,[ID_ProductTemplate_Group]=@ID_ProductTemplate_Group where [ID]=@ID ", connection);
          sqlCommand.Parameters.AddWithValue("@ID", (object) ID);
          sqlCommand.Parameters.AddWithValue("@Title", (object) Title);
          sqlCommand.Parameters.AddWithValue("@ID_ProductTemplate_Group", (object) ID_ProductTemplate_Group);
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

    public DataTable ProductTemplate_SubGroup_Get(int ID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("select * from ProductTemplate_SubGroup where IsDeleted=0 and ID=@ID", connection);
          selectCommand.Parameters.AddWithValue("@ID", (object) ID);
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

    public DataTable ProductTemplate_SubGroup_GetByGroup(int ID_ProductTemplate_Group)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("select * from ProductTemplate_SubGroup where IsDeleted=0 and ID_ProductTemplate_Group=@ID_ProductTemplate_Group", connection);
          selectCommand.Parameters.AddWithValue("@ID_ProductTemplate_Group", (object) ID_ProductTemplate_Group);
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

    public int ProductTemplate_SubGroup_Delete(int ID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand("update ProductTemplate_SubGroup set IsDeleted=1 where ID=@ID ", connection);
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

    public DataTable ProductTemplate_Get()
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          new SqlDataAdapter(new SqlCommand("select * from ProductTemplate where IsDeleted=0", connection)).Fill(dataTable);
        }
        return dataTable;
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return dataTable;
      }
    }

    public int ProductTemplate_insert(
      string Title,
      string TextColor,
      string BackColor,
      int ProductType,
      string ProductBarcode,
      int ProductID,
      string Priority,
      int ID_ProductTemplate_SubGroup)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand("INSERT INTO  [ProductTemplate]\r\n           ([ID]\r\n           ,[Title]\r\n           ,[TextColor]\r\n           ,[BackColor]\r\n           ,[ProductType]\r\n           ,[ProductBarcode]\r\n           ,[ProductID]\r\n           ,[Priority]\r\n           ,[ID_ProductTemplate_SubGroup]\r\n           ,[IsDeleted])\r\n     VALUES\r\n           (@ID\r\n           ,@Title\r\n           ,@TextColor\r\n           ,@BackColor\r\n           ,@ProductType\r\n           ,@ProductBarcode\r\n           ,@ProductID\r\n           ,@Priority\r\n           ,@ID_ProductTemplate_SubGroup\r\n           ,@IsDeleted)", connection);
          sqlCommand.Parameters.AddWithValue("@ID", (object) (this.objmain.Max_Tbl("ProductTemplate", "ID") + 1));
          sqlCommand.Parameters.AddWithValue("@Title", (object) Title);
          sqlCommand.Parameters.AddWithValue("@TextColor", (object) TextColor);
          sqlCommand.Parameters.AddWithValue("@BackColor", (object) BackColor);
          sqlCommand.Parameters.AddWithValue("@ProductType", (object) ProductType);
          sqlCommand.Parameters.AddWithValue("@ProductBarcode", (object) ProductBarcode);
          sqlCommand.Parameters.AddWithValue("@ProductID", (object) ProductID);
          sqlCommand.Parameters.AddWithValue("@Priority", (object) Priority);
          sqlCommand.Parameters.AddWithValue("@ID_ProductTemplate_SubGroup", (object) ID_ProductTemplate_SubGroup);
          sqlCommand.Parameters.AddWithValue("@IsDeleted", (object) 0);
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

    public int ProductTemplate_Update(
      int ID,
      string Title,
      string TextColor,
      string BackColor,
      int ProductType,
      string ProductBarcode,
      int ProductID,
      string Priority,
      int ID_ProductTemplate_SubGroup)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand("update [ProductTemplate] set \r\n           [Title]=@Title\r\n           ,[TextColor]=@TextColor\r\n           ,[BackColor]=@BackColor\r\n           ,[ProductType]=@ProductType\r\n           ,[ProductBarcode]=@ProductBarcode\r\n           ,[ProductID]=@ProductID\r\n           ,[Priority]=@Priority\r\n           ,[ID_ProductTemplate_SubGroup]=@ID_ProductTemplate_SubGroup\r\n             where [ID]=@ID ", connection);
          sqlCommand.Parameters.AddWithValue("@ID", (object) ID);
          sqlCommand.Parameters.AddWithValue("@Title", (object) Title);
          sqlCommand.Parameters.AddWithValue("@TextColor", (object) TextColor);
          sqlCommand.Parameters.AddWithValue("@BackColor", (object) BackColor);
          sqlCommand.Parameters.AddWithValue("@ProductType", (object) ProductType);
          sqlCommand.Parameters.AddWithValue("@ProductBarcode", (object) ProductBarcode);
          sqlCommand.Parameters.AddWithValue("@ProductID", (object) ProductID);
          sqlCommand.Parameters.AddWithValue("@Priority", (object) Priority);
          sqlCommand.Parameters.AddWithValue("@ID_ProductTemplate_SubGroup", (object) ID_ProductTemplate_SubGroup);
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

    public DataTable ProductTemplate_Get(int ID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("select * from ProductTemplate where IsDeleted=0 and [ID]=@ID", connection);
          selectCommand.Parameters.AddWithValue("@ID", (object) ID);
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

    public DataTable ProductTemplate_GetBySubGroup(int ID_ProductTemplate_SubGroup)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("select * from ProductTemplate where IsDeleted=0 and [ID_ProductTemplate_SubGroup]=@ID_ProductTemplate_SubGroup", connection);
          selectCommand.Parameters.AddWithValue("@ID_ProductTemplate_SubGroup", (object) ID_ProductTemplate_SubGroup);
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
