// Decompiled with JetBrains decompiler
// Type: ClickServerService.ClassCode.RepairClass
// Assembly: ClickServerService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6BDFD2F8-7BA8-4B8A-8EC1-401DFA893333
// Assembly location: C:\Users\Win10\Desktop\ClickServerService.exe

using System;
using System.Data;
using System.Data.SqlClient;

namespace ClickServerService.ClassCode
{
  internal class RepairClass
  {
    private MainClass objmain = new MainClass();

    public void Create_Repair_Today_CheckList()
    {
      DataTable dataTable1 = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          SqlCommand selectCommand = new SqlCommand("Select * from Games_CheckList where CAST(Date as date)=CAST((Select GETDATE()) as date) and ID_Gamecenter=@ID_Gamecenter", connection);
          selectCommand.Parameters.Add("@ID_Gamecenter", SqlDbType.Int).Value = (object) this.objmain.ID_GameCenter_Local_Get();
          new SqlDataAdapter(selectCommand).Fill(dataTable1);
        }
        if (dataTable1.Rows.Count != 0)
          return;
        DataSet dataSet = new DataSet();
        DataTable dataTable2 = new DataTable();
        DataTable dataTable3 = new DataTable();
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          SqlCommand selectCommand = new SqlCommand("SELECT   distinct     Games_Details.ID, Games_Details.ID_GameCenter, Games_Details.ID_Games, \r\nGames_Details.Title, Game_Visit_Items.Daily,\r\n Game_Visit_Items.Weekly, Game_Visit_Items.Periodic, Games_Details.WeekDayIndex,\r\n                         Games_Details.Day,1 as ID_CheckList_Type\r\nFROM            Games_Details INNER JOIN\r\n                         Game_Visit_Items ON Games_Details.ID = Game_Visit_Items.ID_Games_Details\r\n                          AND Games_Details.ID_GameCenter = Game_Visit_Items.ID_GameCenter\r\n LEFT OUTER JOIN\r\n                         Games ON Games_Details.ID_Games = Games.ID\r\n                          where  Game_Visit_Items.Daily = 1 and Games_Details.ID_GameCenter=@ID_Gamecenter and ISNULL( Games.IsRetired,0)=0 and ISNULL( Games.IsDeleted,0)=0\r\n\r\n\r\nSELECT   distinct     Games_Details.ID, Games_Details.ID_GameCenter, Games_Details.ID_Games, \r\nGames_Details.Title, Game_Visit_Items.Daily,\r\n Game_Visit_Items.Weekly, Game_Visit_Items.Periodic, Games_Details.WeekDayIndex,\r\n                         Games_Details.Day,2 as ID_CheckList_Type\r\nFROM            Games_Details INNER JOIN\r\n                         Game_Visit_Items ON Games_Details.ID = Game_Visit_Items.ID_Games_Details\r\n                          AND Games_Details.ID_GameCenter = Game_Visit_Items.ID_GameCenter\r\n LEFT OUTER JOIN\r\n                         Games ON Games_Details.ID_Games = Games.ID\r\n                          where  Game_Visit_Items.Weekly = 1 and Games_Details.ID_GameCenter=@ID_Gamecenter and Games_Details.WeekDayIndex= (SELECT datepart(WEEKDAY,(select GETDATE()))) and ISNULL( Games.IsRetired,0)=0 and ISNULL( Games.IsDeleted,0)=0\r\n\r\n\r\nSELECT   distinct     Games_Details.ID, Games_Details.ID_GameCenter, Games_Details.ID_Games, \r\nGames_Details.Title, Game_Visit_Items.Daily,\r\n Game_Visit_Items.Weekly, Game_Visit_Items.Periodic, Games_Details.WeekDayIndex,\r\n                         Games_Details.Day,3 as ID_CheckList_Type\r\nFROM            Games_Details INNER JOIN\r\n                         Game_Visit_Items ON Games_Details.ID = Game_Visit_Items.ID_Games_Details\r\n                          AND Games_Details.ID_GameCenter = Game_Visit_Items.ID_GameCenter\r\n LEFT OUTER JOIN\r\n                         Games ON Games_Details.ID_Games = Games.ID\r\n                          where  Game_Visit_Items.Periodic = 1 and Games_Details.ID_GameCenter=@ID_Gamecenter  and Games_Details.period = 1 and  datepart (day,Games_Details.StartDate)= (SELECT datepart(DAY,(select GETDATE()))) and ISNULL( Games.IsRetired,0)=0 and ISNULL( Games.IsDeleted,0)=0 \r\n\r\n\r\n\r\nSELECT   distinct     Games_Details.ID, Games_Details.ID_GameCenter, Games_Details.ID_Games,\r\nGames_Details.Title, Game_Visit_Items.Daily,\r\n Game_Visit_Items.Weekly, Game_Visit_Items.Periodic, Games_Details.WeekDayIndex,\r\n                         Games_Details.Day, 3 as ID_CheckList_Type\r\nFROM            Games_Details INNER JOIN\r\n                         Game_Visit_Items ON Games_Details.ID = Game_Visit_Items.ID_Games_Details\r\n                          AND Games_Details.ID_GameCenter = Game_Visit_Items.ID_GameCenter\r\n LEFT OUTER JOIN\r\n                         Games ON Games_Details.ID_Games = Games.ID\r\n                          where  Game_Visit_Items.Periodic = 1 and Games_Details.ID_GameCenter = 1 and Games_Details.period = 3\r\n                           and\r\n                            (DATEDIFF(day, Games_Details.StartDate, (select GETDATE())) % 90) = 0\r\n                             and ISNULL(Games.IsRetired, 0)= 0 and ISNULL(Games.IsDeleted, 0)= 0\r\n", connection);
          selectCommand.Parameters.Add("@ID_Gamecenter", SqlDbType.Int).Value = (object) this.objmain.ID_GameCenter_Local_Get();
          new SqlDataAdapter(selectCommand).Fill(dataSet);
          if (dataSet.Tables[0].Rows.Count > 0)
            dataTable2.Merge(dataSet.Tables[0]);
          if (dataSet.Tables[1].Rows.Count > 0)
            dataTable2.Merge(dataSet.Tables[1]);
          if (dataSet.Tables[2].Rows.Count > 0)
            dataTable2.Merge(dataSet.Tables[2]);
          if (dataSet.Tables[3].Rows.Count > 0)
            dataTable2.Merge(dataSet.Tables[3]);
        }
        for (int index = 0; index < dataTable2.Rows.Count; ++index)
        {
          if (true)
          {
            using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
            {
              try
              {
                SqlCommand com = new SqlCommand("insert into Games_CheckList\r\n(ID,ID_Game,ID_Gamecenter,Date,State,IsActive,ID_Games_Details,ID_CheckList_Type,IsChecked,IsConfirm) \r\n                            VALUES(@ID,-1, @ID_GameCenter, @Date,4,1,@ID_Games_Details,@ID_CheckList_Type,0,0)", connection);
                connection.Open();
                com.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = (object) Guid.NewGuid();
                com.Parameters.Add("@ID_GameCenter", SqlDbType.Int).Value = (object) dataTable2.Rows[index]["ID_GameCenter"].ToString();
                com.Parameters.Add("@Date", SqlDbType.DateTime).Value = (object) DateTime.Now;
                com.Parameters.Add("@ID_Games_Details", SqlDbType.Int).Value = (object) dataTable2.Rows[index]["ID"].ToString();
                com.Parameters.Add("@ID_CheckList_Type", SqlDbType.Int).Value = (object) dataTable2.Rows[index]["ID_CheckList_Type"].ToString();
                com.ExecuteNonQuery();
                this.objmain.Synchronize_Insert(com);
              }
              catch (Exception ex)
              {
                this.objmain.ErrorLog(ex);
              }
              finally
              {
                connection.Close();
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
      }
    }
  }
}
