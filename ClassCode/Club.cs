// Decompiled with JetBrains decompiler
// Type: ClickServerService.ClassCode.Club
// Assembly: ClickServerService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6BDFD2F8-7BA8-4B8A-8EC1-401DFA893333
// Assembly location: C:\Users\Win10\Desktop\ClickServerService.exe

using System;
using System.Data;
using System.Data.SqlClient;

namespace ClickServerService.ClassCode
{
  internal class Club
  {
    private MainClass objmain = new MainClass();
    private UsersClass objUser = new UsersClass();

    public bool Club_CheckIn_Process(string Card_GUID)
    {
      bool flag = false;
      DataTable dataTable = new DataTable();
      try
      {
        Guid.Parse(Card_GUID);
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand(nameof (Club_CheckIn_Process), connection);
          selectCommand.Parameters.AddWithValue("@Card_GUID", (object) Card_GUID);
          selectCommand.CommandType = CommandType.StoredProcedure;
          new SqlDataAdapter(selectCommand).Fill(dataTable);
        }
        if (dataTable.Rows.Count > 0)
        {
          string str = dataTable.Rows[0][0].ToString();
          if (str.Split(',')[0].ToString() == "1")
            this.Club_CheckIn_UpdateEnd(str.Split(',')[2].ToString());
          if (str.Split(',')[1].ToString() == "1")
            this.Club_CheckIn_Insert(Card_GUID, 1, -1);
          if (str.Split(',')[3].ToString() == "1")
            flag = true;
        }
        return flag;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public int Club_CheckIn_UpdateEnd(string ID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand com = new SqlCommand("update [Club_CheckIn] set [Date_End]=@Date   where  ID=@ID", connection);
          com.Parameters.AddWithValue("@ID", (object) ID);
          com.Parameters.AddWithValue("@Date", (object) DateTime.Now);
          com.ExecuteNonQuery();
          this.objmain.Synchronize_Insert(com);
        }
        return 1;
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return -1;
      }
    }

    public int Club_CheckIn_Insert(string Card_GUID, int ID_Club_Type, int ID_User)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand com = new SqlCommand("\r\ninsert into [dbo].[Club_CheckIn] (ID,Card_GUID,Date_Start,IsEnd,Points,ID_Club_Type,ID_User) values(@ID,@Card_GUID,@Date_Start,@IsEnd,@Points,@ID_Club_Type,@ID_User) ", connection);
          com.Parameters.AddWithValue("@ID", (object) Guid.NewGuid());
          com.Parameters.AddWithValue("@Card_GUID", (object) Card_GUID);
          com.Parameters.AddWithValue("@Date_Start", (object) DateTime.Now);
          com.Parameters.AddWithValue("@IsEnd", (object) 0);
          com.Parameters.AddWithValue("@Points", (object) 0);
          com.Parameters.AddWithValue("@ID_Club_Type", (object) ID_Club_Type);
          com.Parameters.AddWithValue("@ID_User", (object) ID_User);
          com.ExecuteNonQuery();
          this.objmain.Synchronize_Insert(com);
        }
        return 1;
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return -1;
      }
    }

    public int Club_Point_Process_UpdateCardPoint(
      string Card_GUID,
      int Point,
      int ID_Club_Member_Type)
    {
      try
      {
        SqlConnection sqlConnection = new SqlConnection();
        sqlConnection.ConnectionString = this.objmain.DBPath();
        sqlConnection.Open();
        SqlCommand com = new SqlCommand();
        com.Connection = sqlConnection;
        com.CommandText = "Card_Update_Point";
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@Card_GUID", SqlDbType.UniqueIdentifier).Value = (object) Guid.Parse(Card_GUID);
        com.Parameters.Add("@Point", SqlDbType.Int).Value = (object) Point;
        com.Parameters.Add("@ID_Club_Member_Type", SqlDbType.Int).Value = (object) ID_Club_Member_Type;
        com.ExecuteNonQuery();
        sqlConnection.Close();
        this.objmain.Synchronize_Insert(com);
        return 1;
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return -1;
      }
    }

    public void Club_Point_Process(
      string Card_GUID,
      int ID_Club_Point_Type,
      int Price,
      int Point_Old,
      int ID_Game)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand(nameof (Club_Point_Process), connection);
          selectCommand.CommandType = CommandType.StoredProcedure;
          selectCommand.Parameters.AddWithValue("@Card_GUID", (object) Card_GUID);
          selectCommand.Parameters.AddWithValue("@ID_Club_Point_Type", (object) ID_Club_Point_Type);
          selectCommand.Parameters.AddWithValue("@Price", (object) Price);
          selectCommand.Parameters.AddWithValue("@ID_Game", (object) ID_Game);
          new SqlDataAdapter(selectCommand).Fill(dataTable);
        }
        if (dataTable.Rows.Count <= 0)
          return;
        int Point = int.Parse(dataTable.Rows[0]["Point"].ToString());
        if ((uint) Point > 0U)
        {
          string Description = dataTable.Rows[0]["Descr"].ToString();
          int ID_Club_Member_Type = 0;
          try
          {
            ID_Club_Member_Type = int.Parse(dataTable.Rows[0]["ID_Club_Member_Type"].ToString());
          }
          catch
          {
          }
          string ID_Club_Campaign = dataTable.Rows[0]["ID_Club_Campaign"].ToString();
          this.Club_Point_History_Insert(this.objmain.ID_GameCenter_Local_Get(), ID_Club_Campaign, ID_Club_Member_Type, Card_GUID, Point, Point_Old, Description);
          this.Club_Point_Process_UpdateCardPoint(Card_GUID, Point + Point_Old, ID_Club_Member_Type);
        }
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
      }
    }

    public DataTable Club_Point_History_Get(string Card_GUID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("SELECT        Club_Point_History.ID, Club_Point_History.ID_GameCenter, Club_Point_History.ID_Club_Campaign, Club_Point_History.ID_Club_Member_Type, Club_Point_History.Card_GUID, dbo.MiladiTOShamsi(Club_Point_History.Date) \r\n                         AS Date, Club_Point_History.Point, Club_Point_History.Point_Old, Club_Point_History.Description, Club_Campaign.Title AS CampaignTitle, Club_Member_Type.Title AS MemberTypeTitle, \r\n                         GameCenter.Title AS GameCenterTitle\r\nFROM            Club_Point_History INNER JOIN\r\n                         Club_Campaign ON Club_Point_History.ID_Club_Campaign = Club_Campaign.ID INNER JOIN\r\n                         GameCenter ON Club_Point_History.ID_GameCenter = GameCenter.ID LEFT OUTER JOIN\r\n                         Club_Member_Type ON Club_Point_History.ID_Club_Member_Type = Club_Member_Type.ID\r\nWHERE        (Club_Point_History.Card_GUID = @Card_GUID)\r\nORDER BY Date DESC", connection);
          selectCommand.Parameters.AddWithValue("@Card_GUID", (object) Card_GUID);
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

    public int Club_Point_History_Insert(
      int ID_GameCenter,
      string ID_Club_Campaign,
      int ID_Club_Member_Type,
      string Card_GUID,
      int Point,
      int Point_Old,
      string Description)
    {
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand com = new SqlCommand("INSERT INTO [dbo].[Club_Point_History]\r\n           ([ID]\r\n           ,[ID_GameCenter]\r\n           ,[ID_Club_Campaign]\r\n           ,[ID_Club_Member_Type]\r\n           ,[Card_GUID]\r\n           ,[Date]\r\n           ,[Point]\r\n           ,[Point_Old]\r\n           ,[Description])\r\n     VALUES\r\n           (@ID\r\n           ,@ID_GameCenter\r\n           ,@ID_Club_Campaign\r\n           ,@ID_Club_Member_Type\r\n           ,@Card_GUID\r\n           ,@Date\r\n           ,@Point\r\n           ,@Point_Old\r\n           ,@Description)", connection);
          com.Parameters.AddWithValue("@ID", (object) Guid.NewGuid());
          com.Parameters.AddWithValue("@ID_GameCenter", (object) ID_GameCenter);
          com.Parameters.AddWithValue("@ID_Club_Campaign", (object) ID_Club_Campaign);
          com.Parameters.AddWithValue("@ID_Club_Member_Type", (object) ID_Club_Member_Type);
          com.Parameters.AddWithValue("@Card_GUID", (object) Card_GUID);
          com.Parameters.AddWithValue("@Date", (object) DateTime.Now);
          com.Parameters.AddWithValue("@Point", (object) Point);
          com.Parameters.AddWithValue("@Point_Old", (object) Point_Old);
          com.Parameters.AddWithValue("@Description", (object) Description);
          com.ExecuteNonQuery();
          this.objmain.Synchronize_Insert(com);
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
