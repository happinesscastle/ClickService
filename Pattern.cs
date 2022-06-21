// Decompiled with JetBrains decompiler
// Type: ClickServerService.Pattern
// Assembly: ClickServerService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6BDFD2F8-7BA8-4B8A-8EC1-401DFA893333
// Assembly location: C:\Users\Win10\Desktop\ClickServerService.exe

using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace ClickServerService
{
  internal class Pattern
  {
    private MainClass objmain = new MainClass();
    private UsersClass objUser = new UsersClass();

    public DataTable Gift_list_GetAll()
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.objmain.DBPath()))
        {
          sqlConnection.Open();
          SqlCommand selectCommand = new SqlCommand();
          selectCommand.CommandText = nameof (Gift_list_GetAll);
          selectCommand.Connection = sqlConnection;
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

    public DataTable Gift_Pattern_Series_list_GetAll(Guid ID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.objmain.DBPath()))
        {
          sqlConnection.Open();
          SqlCommand selectCommand = new SqlCommand();
          selectCommand.CommandText = nameof (Gift_Pattern_Series_list_GetAll);
          selectCommand.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = (object) ID;
          selectCommand.Connection = sqlConnection;
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

    public DataTable Gift_list_GetByID(Guid ID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.objmain.DBPath()))
        {
          sqlConnection.Open();
          SqlCommand selectCommand = new SqlCommand();
          selectCommand.CommandText = "Select * from Gift_Pattern where ID=@ID";
          selectCommand.Connection = sqlConnection;
          selectCommand.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = (object) ID;
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

    public DataTable Gift_Series_List_Search(string Code, Guid ID_Pattern)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.objmain.DBPath()))
        {
          sqlConnection.Open();
          SqlCommand selectCommand = new SqlCommand();
          selectCommand.CommandText = "Select * from Gift_Pattern_series_List where Gift_Pattern_series_List.Code=@Code and ID_Pattern_Series=@ID_Pattern";
          selectCommand.Connection = sqlConnection;
          selectCommand.Parameters.Add("@Code", SqlDbType.NVarChar).Value = (object) Code;
          selectCommand.Parameters.Add("@ID_Pattern", SqlDbType.NVarChar).Value = (object) ID_Pattern;
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

    public DataTable Gamecenter_GetAll_Pattern(string List)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.objmain.DBPath()))
        {
          sqlConnection.Open();
          SqlCommand selectCommand = new SqlCommand();
          selectCommand.CommandText = nameof (Gamecenter_GetAll_Pattern);
          selectCommand.Connection = sqlConnection;
          selectCommand.CommandType = CommandType.StoredProcedure;
          selectCommand.Parameters.Add("@list", SqlDbType.NVarChar).Value = (object) List;
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

    public DataTable Gift_Series_Details(Guid ID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.objmain.DBPath()))
        {
          sqlConnection.Open();
          SqlCommand selectCommand = new SqlCommand();
          selectCommand.CommandText = "Select * from Gift_Pattern_series_List where ID_Pattern_Series=@ID";
          selectCommand.Connection = sqlConnection;
          selectCommand.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = (object) ID;
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

    public bool Gift_Series_Check_SeriesCOdeExist(int ID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.objmain.DBPath()))
        {
          sqlConnection.Open();
          SqlCommand selectCommand = new SqlCommand();
          selectCommand.CommandText = nameof (Gift_Series_Check_SeriesCOdeExist);
          selectCommand.Connection = sqlConnection;
          selectCommand.Parameters.Add("@ID", SqlDbType.Int).Value = (object) ID;
          selectCommand.CommandType = CommandType.StoredProcedure;
          new SqlDataAdapter(selectCommand).Fill(dataTable);
        }
        return dataTable.Rows.Count != 0;
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return false;
      }
    }

    public DataSet Gift_GUID_Check_Validation(DataTable dtGuidList)
    {
      DataSet dataSet = new DataSet();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.objmain.DBPath()))
        {
          sqlConnection.Open();
          SqlCommand selectCommand = new SqlCommand();
          selectCommand.CommandText = nameof (Gift_GUID_Check_Validation);
          selectCommand.Connection = sqlConnection;
          selectCommand.CommandType = CommandType.StoredProcedure;
          selectCommand.Parameters.Add("@list", SqlDbType.Structured).Value = (object) dtGuidList;
          new SqlDataAdapter(selectCommand).Fill(dataSet);
        }
        return dataSet;
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return dataSet;
      }
    }

    public DataTable Gift_Pattern_GetBYID(string ID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.objmain.DBPath()))
        {
          sqlConnection.Open();
          SqlCommand selectCommand = new SqlCommand();
          selectCommand.CommandText = "Select * from Gift_Pattern where ID=@ID";
          selectCommand.Connection = sqlConnection;
          selectCommand.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = (object) Guid.Parse(ID);
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

    public int Gift_Series_list_Add(
      string Code,
      string Card_GUID,
      string Mobile,
      int charge,
      string FreeGames,
      int FreegameCount,
      string FreeDailyGames,
      int FreeDailyGamesCount,
      string CustomerName,
      string ID_Pattern_Series,
      DateTime StartDate,
      DateTime EndDate,
      int ExtraCharge,
      int ValidationType,
      int ValidationDay,
      DateTime ValidationDate,
      string GameCenterIDs)
    {
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand com = new SqlCommand("insert into Gift_Pattern_series_List(Code,Card_GUID,Status,Mobile,Charge,FreeGames,FreegameCount\r\n                    ,FreeDailyGames,FreeDailyGamesCount,Customer_Name,IsActive,ID,ID_Pattern_Series,StartDate,EndDate,ExtraCharge,ValidationType,\r\n                    ValidationDay,ValidationDate,GameCenterIDs,Real_Charge,Real_FreegameCount,Real_FreeDailyGamesCount)\r\n                    Values(@Code,@Card_GUID,0,@Mobile,@charge,@FreeGames,@FreegameCount,@FreeDailyGames,@FreeDailyGamesCount,@CustomerName,\r\n                    1,@ID,@ID_Pattern_Series,@StartDate,@EndDate,@ExtraCharge,@ValidationType,@ValidationDay,@ValidationDate,@GameCenterIDs,\r\n                    @charge,@FreegameCount,@FreeDailyGamesCount)", connection);
          com.Parameters.Add("@Code", SqlDbType.NVarChar).Value = (object) Code;
          if (Card_GUID != string.Empty)
            com.Parameters.Add("@Card_GUID", SqlDbType.NVarChar).Value = (object) Guid.Parse(Card_GUID);
          else
            com.Parameters.Add("@Card_GUID", SqlDbType.NVarChar).Value = (object) DBNull.Value;
          com.Parameters.Add("@Mobile", SqlDbType.NVarChar).Value = (object) Mobile;
          com.Parameters.Add("@charge", SqlDbType.Int).Value = (object) charge;
          com.Parameters.Add("@FreeGames", SqlDbType.NVarChar).Value = (object) FreeGames;
          com.Parameters.Add("@FreegameCount", SqlDbType.Int).Value = (object) FreegameCount;
          com.Parameters.Add("@FreeDailyGames", SqlDbType.NVarChar).Value = (object) FreeDailyGames;
          com.Parameters.Add("@FreeDailyGamesCount", SqlDbType.Int).Value = (object) FreeDailyGamesCount;
          com.Parameters.Add("@CustomerName", SqlDbType.NVarChar).Value = (object) CustomerName;
          com.Parameters.Add("@ID_Pattern_Series", SqlDbType.UniqueIdentifier).Value = (object) Guid.Parse(ID_Pattern_Series);
          com.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = (object) StartDate;
          com.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = (object) EndDate;
          com.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = (object) Guid.NewGuid();
          com.Parameters.Add("@ExtraCharge", SqlDbType.Int).Value = (object) ExtraCharge;
          com.Parameters.Add("@ValidationType", SqlDbType.Int).Value = (object) ValidationType;
          com.Parameters.Add("@ValidationDay", SqlDbType.Int).Value = (object) ValidationDay;
          com.Parameters.Add("@ValidationDate", SqlDbType.DateTime).Value = (object) ValidationDate;
          com.Parameters.Add("@GameCenterIDs", SqlDbType.NVarChar).Value = (object) GameCenterIDs;
          com.ExecuteNonQuery();
          this.objmain.Synchronize_Insert(com);
          return 1;
        }
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return -1;
      }
    }

    public int Gift_Series_Add(
      int ID_Pattern,
      string Title,
      int count,
      DateTime StartDate,
      DateTime Enddate,
      bool Special,
      int UserID,
      DataTable dtList)
    {
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand com = new SqlCommand(nameof (Gift_Series_Add), connection);
          com.Parameters.Add("@ID_Pattern", SqlDbType.Int).Value = (object) ID_Pattern;
          com.Parameters.Add("@Title", SqlDbType.NVarChar).Value = (object) Title;
          com.Parameters.Add("@Count", SqlDbType.Int).Value = (object) count;
          com.Parameters.Add("@SatrtDate", SqlDbType.Date).Value = (object) StartDate;
          com.Parameters.Add("@EndDate", SqlDbType.Date).Value = (object) Enddate;
          com.Parameters.Add("@Special", SqlDbType.Bit).Value = (object) Special;
          com.Parameters.Add("@userID", SqlDbType.Int).Value = (object) UserID;
          com.Parameters.Add("@List", SqlDbType.Structured).Value = (object) dtList;
          com.CommandType = CommandType.StoredProcedure;
          com.ExecuteNonQuery();
          this.objmain.Synchronize_Insert(com);
          return 1;
        }
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return -1;
      }
    }

    public int Gift_series_Delete(Guid ID)
    {
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand com = new SqlCommand("update Gift_Pattern_series Set IsDelete=1 where ID=@ID\r\n                    update Gift_Pattern_series_List Set IsActive = 0 where ID_Pattern_Series = @ID", connection);
          com.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = (object) ID;
          com.ExecuteNonQuery();
          this.objmain.Synchronize_Insert(com);
          return 1;
        }
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return -1;
      }
    }

    public int Gift_series_List_ChangeState(Guid ID, bool IsActive)
    {
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          IsActive = !IsActive;
          connection.Open();
          SqlCommand com = new SqlCommand("Update Gift_Pattern_series_List Set IsActive=@IsActive where ID=@ID", connection);
          com.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = (object) ID;
          com.Parameters.Add("@IsActive", SqlDbType.Bit).Value = (object) IsActive;
          com.ExecuteNonQuery();
          this.objmain.Synchronize_Insert(com);
          return 1;
        }
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return -1;
      }
    }

    public int Gift_Pattern_Add(
      Guid EditID,
      string Gamecenter,
      string Name,
      int CHarge,
      string FreeGames,
      string freeDailyGames,
      int FreeGamesCount,
      int freeDailyGamesCount,
      int Extracharge)
    {
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand com = new SqlCommand(nameof (Gift_Pattern_Add), connection);
          com.Parameters.Add("@EditedID", SqlDbType.UniqueIdentifier).Value = (object) EditID;
          com.Parameters.Add("@Gamecenters", SqlDbType.NVarChar).Value = (object) Gamecenter;
          com.Parameters.Add("@Name", SqlDbType.NVarChar).Value = (object) Name;
          com.Parameters.Add("@Charge", SqlDbType.Int).Value = (object) CHarge;
          com.Parameters.Add("@FreeGames", SqlDbType.NVarChar).Value = (object) FreeGames;
          com.Parameters.Add("@freeDailyGames", SqlDbType.NVarChar).Value = (object) freeDailyGames;
          com.Parameters.Add("@FreeGamesCount", SqlDbType.Int).Value = (object) FreeGamesCount;
          com.Parameters.Add("@freeDailyGamesCount", SqlDbType.Int).Value = (object) freeDailyGamesCount;
          com.Parameters.Add("@Extracharge", SqlDbType.Int).Value = (object) Extracharge;
          com.CommandType = CommandType.StoredProcedure;
          com.ExecuteNonQuery();
          this.objmain.Synchronize_Insert(com);
          return 1;
        }
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return -1;
      }
    }

    public DataTable Gift_series_List_ActiveByCard_GUID(string Card_GUID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("select * from Gift_Pattern_series_List where Card_GUID=@Card_GUID  and cast(ValidationDate as date) >=  cast( @ValidationDate  as date)   and IsActive=1  ", connection);
          selectCommand.Parameters.AddWithValue("@Card_GUID", (object) Card_GUID);
          selectCommand.Parameters.AddWithValue("@ValidationDate", (object) DateTime.Now);
          new SqlDataAdapter(selectCommand).Fill(dataTable);
          connection.Close();
          connection.Dispose();
          return dataTable;
        }
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return dataTable;
      }
    }

    public int Gift_Pattern_Delete(Guid ID)
    {
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand com = new SqlCommand("Update Gift_Pattern Set IsDelete=1 where ID=@ID", connection);
          com.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = (object) ID;
          com.ExecuteNonQuery();
          this.objmain.Synchronize_Insert(com);
          return 1;
        }
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return -1;
      }
    }

    public int Gift_Pattern_series_List_Update(string ID, int Real_FreegameCount)
    {
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand com = new SqlCommand("Update Gift_Pattern_series_List Set Real_FreegameCount=@Real_FreegameCount where ID=@ID", connection);
          com.Parameters.AddWithValue("@ID", (object) ID);
          com.Parameters.AddWithValue("@Real_FreegameCount", (object) Real_FreegameCount);
          com.ExecuteNonQuery();
          this.objmain.Synchronize_Insert(com);
          connection.Close();
          connection.Dispose();
        }
        return 1;
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return -1;
      }
    }

    public int Gift_Pattern_Series_list_Calculate(string Card_GUID, int GamePrice)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand(nameof (Gift_Pattern_Series_list_Calculate), connection);
          sqlCommand.Parameters.AddWithValue("@Card_GUID", (object) Card_GUID);
          sqlCommand.Parameters.AddWithValue("@GamePrice", (object) GamePrice);
          sqlCommand.CommandType = CommandType.StoredProcedure;
          new SqlDataAdapter(sqlCommand).Fill(dataTable);
          connection.Close();
          connection.Dispose();
          this.objmain.Synchronize_Insert(sqlCommand);
        }
        return int.Parse(dataTable.Rows[0][0].ToString());
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return -1;
      }
    }

    public int Gift_Pattern_Series_list_Calculate2(string Card_GUID, int GamePrice)
    {
      int num1 = GamePrice;
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand(nameof (Gift_Pattern_Series_list_Calculate2), connection);
          selectCommand.Parameters.AddWithValue("@Card_GUID", (object) Card_GUID);
          selectCommand.Parameters.AddWithValue("@DateNow", (object) DateTime.Now);
          selectCommand.CommandType = CommandType.StoredProcedure;
          new SqlDataAdapter(selectCommand).Fill(dataTable);
          connection.Close();
          connection.Dispose();
          if (dataTable.Rows.Count > 0)
          {
            for (int index = 0; index < dataTable.Rows.Count; ++index)
            {
              if (num1 > 0)
              {
                int num2 = int.Parse(dataTable.Rows[index]["Real_Charge"].ToString());
                if (num2 <= num1)
                {
                  this.Gift_Pattern_Series_list_Update(Guid.Parse(dataTable.Rows[index]["ID"].ToString()), 0);
                  num1 -= num2;
                }
                else
                {
                  this.Gift_Pattern_Series_list_Update(Guid.Parse(dataTable.Rows[index]["ID"].ToString()), num2 - num1);
                  num1 = 0;
                }
              }
            }
          }
        }
        return num1;
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return -1;
      }
    }

    public int Gift_Pattern_Series_list_Update(Guid ID, int Real_Charge)
    {
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand com = new SqlCommand(" update Gift_Pattern_series_List set Real_Charge=@Real_Charge where ID=@ID ", connection);
          com.Parameters.AddWithValue("@ID", (object) ID);
          com.Parameters.AddWithValue("@Real_Charge", (object) Real_Charge);
          com.ExecuteNonQuery();
          connection.Close();
          connection.Dispose();
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

    public DataTable Gift_series_List_CheckCode(string Code)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("select * from Gift_Pattern_series_List where Code=@Code ", connection);
          selectCommand.Parameters.Add("@Code", SqlDbType.NVarChar).Value = (object) Code;
          new SqlDataAdapter(selectCommand).Fill(dataTable);
          return dataTable;
        }
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return dataTable;
      }
    }

    public DataTable Gift_Series_DetailsBy_Card_GUID(string Card_GUID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection sqlConnection = new SqlConnection(this.objmain.DBPath()))
        {
          sqlConnection.Open();
          SqlCommand selectCommand = new SqlCommand();
          selectCommand.CommandText = "SELECT        Gift_Pattern_series_List.ID, Gift_Pattern_series_List.ID_Pattern_Series, Gift_Pattern_series_List.Code, Gift_Pattern_series_List.Card_GUID, Gift_Pattern_series_List.Mobile, Gift_Pattern_series_List.Charge, \r\n                         Gift_Pattern_series_List.FreeGames, Gift_Pattern_series_List.FreegameCount, Gift_Pattern_series_List.FreeDailyGames, Gift_Pattern_series_List.FreeDailyGamesCount, Gift_Pattern_series_List.Customer_Name, \r\n                         Gift_Pattern_series_List.StartDate, Gift_Pattern_series_List.EndDate, Gift_Pattern_series_List.ExtraCharge, Gift_Pattern_series_List.ValidationType, Gift_Pattern_series_List.ValidationDay, \r\n                       dbo.MiladiTOShamsi(Gift_Pattern_series_List.ValidationDate) as ValidationDate, Gift_Pattern_series_List.Status, Gift_Pattern_series_List.IsActive,dbo.MiladiTOShamsi(Gift_Pattern_series_List.StartActiveDate) as StartActiveDate, Gift_Pattern_series_List.GameCenterIDs, \r\n                         Gift_Pattern_series.Title AS SeriTitle,Gift_Pattern_series_List.Real_Charge\r\nFROM            Gift_Pattern_series_List INNER JOIN\r\n                         Gift_Pattern_series ON Gift_Pattern_series_List.ID_Pattern_Series = Gift_Pattern_series.ID\r\nWHERE(Gift_Pattern_series_List.Card_GUID = @Card_GUID)";
          selectCommand.Connection = sqlConnection;
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

    public DataTable ReadFromExcelFile(string Path, string Sheet)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Path + ";Extended Properties='Excel 8.0;HDR=Yes;'"))
        {
          connection.Open();
          OleDbCommand oleDbCommand = new OleDbCommand("select * from [" + Sheet + "]", connection);
          new OleDbDataAdapter()
          {
            SelectCommand = oleDbCommand
          }.Fill(dataTable);
          return dataTable;
        }
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return dataTable;
      }
    }
  }
}
