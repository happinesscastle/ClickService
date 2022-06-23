using ClickServerService.ClassCode;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ClickServerService
{
  internal class CardClass
  {
    private MainClass objmain = new MainClass();
    private Club objClub = new Club();
    private CashierClass objCashier = new CashierClass();

    public DataTable Card_Status_Get()
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("Card_Status_GetAll", connection);
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

    public int Card_Status_insert(
      string Title,
      int ReswipeDelay,
      int ConsecutiveSwipes,
      bool DisplayBalance,
      bool Is_Party,
      bool Is_TimePlay,
      bool Is_Staff,
      bool AllowAllCategory,
      bool DisAllowAllCategory,
      string DisallowList)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand("Card_Status_Insert", connection);
          sqlCommand.Parameters.AddWithValue("@ID", (object) (this.objmain.Max_Tbl("Card_Status", "ID") + 1));
          sqlCommand.Parameters.AddWithValue("@Title", (object) Title);
          sqlCommand.Parameters.AddWithValue("@IsDeleted", (object) 0);
          sqlCommand.Parameters.AddWithValue("@ReswipeDelay", (object) ReswipeDelay);
          sqlCommand.Parameters.AddWithValue("@ConsecutiveSwipes", (object) ConsecutiveSwipes);
          sqlCommand.Parameters.AddWithValue("@DisplayBalance", (object) DisplayBalance);
          sqlCommand.Parameters.AddWithValue("@Is_Party", (object) Is_Party);
          sqlCommand.Parameters.AddWithValue("@Is_TimePlay", (object) Is_TimePlay);
          sqlCommand.Parameters.AddWithValue("@Is_Staff", (object) Is_Staff);
          sqlCommand.Parameters.AddWithValue("@AllowAllCategory", (object) AllowAllCategory);
          sqlCommand.Parameters.AddWithValue("@DisAllowAllCategory", (object) DisAllowAllCategory);
          sqlCommand.Parameters.AddWithValue("@DisallowList", (object) DisallowList);
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

    public int Card_Status_Update(
      int ID,
      string Title,
      int ReswipeDelay,
      int ConsecutiveSwipes,
      bool DisplayBalance,
      bool Is_Party,
      bool Is_TimePlay,
      bool Is_Staff,
      bool AllowAllCategory,
      bool DisAllowAllCategory,
      string DisallowList)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand(nameof (Card_Status_Update), connection);
          sqlCommand.Parameters.AddWithValue("@ID", (object) ID);
          sqlCommand.Parameters.AddWithValue("@Title", (object) Title);
          sqlCommand.Parameters.AddWithValue("@ReswipeDelay", (object) ReswipeDelay);
          sqlCommand.Parameters.AddWithValue("@ConsecutiveSwipes", (object) ConsecutiveSwipes);
          sqlCommand.Parameters.AddWithValue("@DisplayBalance", (object) DisplayBalance);
          sqlCommand.Parameters.AddWithValue("@Is_Party", (object) Is_Party);
          sqlCommand.Parameters.AddWithValue("@Is_TimePlay", (object) Is_TimePlay);
          sqlCommand.Parameters.AddWithValue("@Is_Staff", (object) Is_Staff);
          sqlCommand.Parameters.AddWithValue("@AllowAllCategory", (object) AllowAllCategory);
          sqlCommand.Parameters.AddWithValue("@DisAllowAllCategory", (object) DisAllowAllCategory);
          sqlCommand.Parameters.AddWithValue("@DisallowList", (object) DisallowList);
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

    public DataTable Card_Status_Get(int ID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand(nameof (Card_Status_Get), connection);
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

    public int Card_Status_Delete(int ID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand(nameof (Card_Status_Delete), connection);
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

    public int Card_Series_insert(
      int ID_GameCenter,
      string CardPrefix,
      string BarcodeFrom,
      string BarcodeTo,
      DateTime PurgeDate)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand("Card_Series_Insert", connection);
          sqlCommand.Parameters.AddWithValue("@ID", (object) (this.objmain.Max_Tbl("Card_Series", "ID") + 1));
          sqlCommand.Parameters.AddWithValue("@ID_GameCenter", (object) ID_GameCenter);
          sqlCommand.Parameters.AddWithValue("@CardPrefix", (object) CardPrefix);
          sqlCommand.Parameters.AddWithValue("@BarcodeFrom", (object) BarcodeFrom);
          sqlCommand.Parameters.AddWithValue("@BarcodeTo", (object) BarcodeTo);
          sqlCommand.Parameters.AddWithValue("@PurgeDate", (object) PurgeDate);
          sqlCommand.Parameters.AddWithValue("@Date", (object) DateTime.Now);
          sqlCommand.Parameters.AddWithValue("@IsDeleted", (object) 0);
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

    public int Card_Series_Update(
      int ID,
      int ID_GameCenter,
      string CardPrefix,
      string BarcodeFrom,
      string BarcodeTo,
      DateTime PurgeDate)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand(nameof (Card_Series_Update), connection);
          sqlCommand.Parameters.AddWithValue("@ID", (object) ID);
          sqlCommand.Parameters.AddWithValue("@ID_GameCenter", (object) ID_GameCenter);
          sqlCommand.Parameters.AddWithValue("@CardPrefix", (object) CardPrefix);
          sqlCommand.Parameters.AddWithValue("@BarcodeFrom", (object) BarcodeFrom);
          sqlCommand.Parameters.AddWithValue("@BarcodeTo", (object) BarcodeTo);
          sqlCommand.Parameters.AddWithValue("@PurgeDate", (object) PurgeDate);
          sqlCommand.Parameters.AddWithValue("@Date", (object) DateTime.Now);
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

    public DataTable Card_Series_GetAllByGameCenter(int ID_GameCenter)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand(nameof (Card_Series_GetAllByGameCenter), connection);
          selectCommand.Parameters.AddWithValue("@ID_GameCenter", (object) ID_GameCenter);
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

    public DataTable Card_Series_Get()
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("Card_Series_GetAll", connection);
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

    public DataTable Card_Series_Get_byPrefix(string CardPrefix)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("Card_Series_Get_byCardPrefix", connection);
          selectCommand.Parameters.AddWithValue("@CardPrefix", (object) CardPrefix);
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

    public DataTable Card_Series_Get(int ID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand(nameof (Card_Series_Get), connection);
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

    public int Card_Series_Delete(int ID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand(nameof (Card_Series_Delete), connection);
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

    public int Card_insert(string MacCode, int ID_Card_Series, string CodeSeries)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand("Card_Insert", connection);
          sqlCommand.Parameters.AddWithValue("@ID", (object) (this.objmain.Max_Tbl("Card", "ID") + 1));
          sqlCommand.Parameters.AddWithValue("@MacCode", (object) MacCode);
          sqlCommand.Parameters.AddWithValue("@ID_Card_Series", (object) ID_Card_Series);
          sqlCommand.Parameters.AddWithValue("@CodeSeries", (object) CodeSeries);
          sqlCommand.Parameters.AddWithValue("@Date", (object) DateTime.Now);
          sqlCommand.Parameters.AddWithValue("@IsActive", (object) 1);
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

    public DataTable Card_Get(int ID_Card_Series)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand(nameof (Card_Get), connection);
          selectCommand.CommandType = CommandType.StoredProcedure;
          selectCommand.Parameters.AddWithValue("@ID_Card_Series", (object) ID_Card_Series);
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

    public DataTable Card_GetByMacAddrress(string MacCode)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("Card_GetByMacAddrress_Service", connection);
          selectCommand.CommandType = CommandType.StoredProcedure;
          selectCommand.Parameters.AddWithValue("@MacCode", (object) MacCode.ToUpper());
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

    public DataSet Card_GetByMacAddrressFFFFFF(string MacCode)
    {
      DataSet dataSet = new DataSet();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("Card_GetByMacAddrress_FFFFFFF_Service", connection);
          selectCommand.CommandType = CommandType.StoredProcedure;
          selectCommand.Parameters.AddWithValue("@MacCode", (object) MacCode.ToUpper());
          new SqlDataAdapter(selectCommand).Fill(dataSet);
          connection.Close();
          connection.Dispose();
        }
        return dataSet;
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return dataSet;
      }
    }

    public int Card_Return(string Card_GUID)
    {
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand com = new SqlCommand(" update Card set Is_Closed=1  where Card_GUID=@Card_GUID ", connection);
          com.Parameters.AddWithValue("@Card_GUID", (object) Card_GUID);
          com.CommandType = CommandType.Text;
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

    public DataTable Card_UpdatePrice(string MacCode, string GuID, int Price)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand com = new SqlCommand("update   Card set CashPrice=@CashPrice  where Is_Closed=0 and MacCode=@MacCode and Card_GUID=@Card_GUID", connection);
          com.Parameters.AddWithValue("@CashPrice", (object) Price);
          com.Parameters.AddWithValue("@MacCode", (object) MacCode);
          com.Parameters.AddWithValue("@Card_GUID", (object) GuID);
          com.ExecuteNonQuery();
          this.objmain.Synchronize_Insert(com);
        }
        return dataTable;
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return dataTable;
      }
    }

    public DataTable Card_UpdateBonus(string MacCode, string Card_GUID, int BonusPrice)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand com = new SqlCommand("update   Card set BonusPrice=@BonusPrice  where Is_Closed=0 and MacCode=@MacCode and Card_GUID=@Card_GUID", connection);
          com.Parameters.AddWithValue("@BonusPrice", (object) BonusPrice);
          com.Parameters.AddWithValue("@MacCode", (object) MacCode);
          com.Parameters.AddWithValue("@Card_GUID", (object) Card_GUID);
          com.ExecuteNonQuery();
          this.objmain.Synchronize_Insert(com);
        }
        return dataTable;
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return dataTable;
      }
    }

    public DataTable Card_UpdatePriceAndBonus(
      string MacCode,
      string GuID,
      int Price,
      int BonusPrice)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand com = new SqlCommand("update   Card set CashPrice=@CashPrice,BonusPrice=@BonusPrice  where  MacCode=@MacCode and Card_GUID=@Card_GUID", connection);
          com.Parameters.AddWithValue("@CashPrice", (object) Price);
          com.Parameters.AddWithValue("@BonusPrice", (object) BonusPrice);
          com.Parameters.AddWithValue("@MacCode", (object) MacCode);
          com.Parameters.AddWithValue("@Card_GUID", (object) GuID);
          com.ExecuteNonQuery();
          this.objmain.Synchronize_Insert(com);
        }
        return dataTable;
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return dataTable;
      }
    }

    public int Card_UpdatePriceAndBonus_PlayDetails(
      string GuID,
      int Card_Cash_Price,
      int Card_Bonus_Price,
      int ID_GameCenter,
      string SwiperMacAddress,
      int GamePrice,
      int SumPrice,
      int Bonus,
      int IsPersonnel,
      int ID_Games,
      int ID_Swiper,
      int ID_Play_Type)
    {
      DataTable dataTable = new DataTable();
      try
      {
        int num = this.objmain.Max_Tbl("Card_Play_Details", "ID") + 1;
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand com = new SqlCommand(nameof (Card_UpdatePriceAndBonus_PlayDetails), connection);
          com.Parameters.AddWithValue("@ID", (object) num);
          com.Parameters.AddWithValue("@ID_GameCenter", (object) ID_GameCenter);
          com.Parameters.AddWithValue("@SwiperMacAddress", (object) SwiperMacAddress);
          com.Parameters.AddWithValue("@Card_GUID", (object) GuID);
          com.Parameters.AddWithValue("@Date", (object) DateTime.Now);
          com.Parameters.AddWithValue("@Price", (object) GamePrice);
          com.Parameters.AddWithValue("@SumPrice", (object) SumPrice);
          com.Parameters.AddWithValue("@Bonus", (object) Bonus);
          com.Parameters.AddWithValue("@IsPersonnel", (object) IsPersonnel);
          com.Parameters.AddWithValue("@ID_Games", (object) ID_Games);
          com.Parameters.AddWithValue("@ID_Swiper", (object) ID_Swiper);
          com.Parameters.AddWithValue("@CashPrice", (object) Card_Cash_Price);
          com.Parameters.AddWithValue("@BonusPrice", (object) Card_Bonus_Price);
          com.Parameters.AddWithValue("@ID_Play_Type", (object) ID_Play_Type);
          com.CommandType = CommandType.StoredProcedure;
          com.ExecuteNonQuery();
          this.objmain.Synchronize_Insert(com);
        }
        return num;
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return -1;
      }
    }

    public DataTable Card_GetByCard_GUID(string Card_GUID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        if (Card_GUID.Length > 5)
        {
          using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
          {
            connection.Open();
            SqlCommand selectCommand = new SqlCommand("Card_GetByCard_GUID_2", connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@Card_GUID", (object) Card_GUID);
            new SqlDataAdapter(selectCommand).Fill(dataTable);
            connection.Close();
          }
        }
        return dataTable;
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        this.objmain.ErrorLogTemp(Card_GUID);
        return dataTable;
      }
    }

    public int Card_UpdatePriceAndBonus_PlayDetails2(
      string GuID,
      int Card_Cash_Price,
      int Card_Bonus_Price,
      int ID_GameCenter,
      string SwiperMacAddress,
      int GamePrice,
      int SumPrice,
      int Bonus,
      int IsPersonnel,
      int ID_Games,
      int ID_Swiper,
      int ID_Play_Type,
      int Pay_CashPortion,
      int Pay_BonusPortion,
      int Pay_GiftPortion,
      Guid ID_ActiveGiftForExtraBonus)
    {
      DataTable dataTable = new DataTable();
      try
      {
        int num = this.objmain.Max_Tbl("Card_Play_Details", "ID") + 1;
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand com = new SqlCommand(nameof (Card_UpdatePriceAndBonus_PlayDetails2), connection);
          com.Parameters.AddWithValue("@ID", (object) num);
          com.Parameters.AddWithValue("@ID_GameCenter", (object) ID_GameCenter);
          com.Parameters.AddWithValue("@SwiperMacAddress", (object) SwiperMacAddress);
          com.Parameters.AddWithValue("@Card_GUID", (object) GuID);
          com.Parameters.AddWithValue("@Date", (object) DateTime.Now);
          com.Parameters.AddWithValue("@Price", (object) GamePrice);
          com.Parameters.AddWithValue("@SumPrice", (object) SumPrice);
          com.Parameters.AddWithValue("@Bonus", (object) Bonus);
          com.Parameters.AddWithValue("@IsPersonnel", (object) IsPersonnel);
          com.Parameters.AddWithValue("@ID_Games", (object) ID_Games);
          com.Parameters.AddWithValue("@ID_Swiper", (object) ID_Swiper);
          com.Parameters.AddWithValue("@CashPrice", (object) Card_Cash_Price);
          com.Parameters.AddWithValue("@BonusPrice", (object) Card_Bonus_Price);
          com.Parameters.AddWithValue("@ID_Play_Type", (object) ID_Play_Type);
          com.Parameters.AddWithValue("@Pay_CashPortion", (object) Pay_CashPortion);
          com.Parameters.AddWithValue("@Pay_BonusPortion", (object) Pay_BonusPortion);
          com.Parameters.AddWithValue("@Pay_GiftPortion", (object) Pay_GiftPortion);
          com.Parameters.AddWithValue("@ID_Gift_Pattern_series_List", (object) ID_ActiveGiftForExtraBonus);
          com.CommandType = CommandType.StoredProcedure;
          com.ExecuteNonQuery();
          connection.Close();
          connection.Dispose();
          this.objmain.Synchronize_Insert(com);
          DataTable byCardGuid = this.Card_GetByCard_GUID(GuID);
          int Point_Old = 0;
          try
          {
            Point_Old = int.Parse(byCardGuid.Rows[0]["Club_Point"].ToString());
          }
          catch
          {
          }
          this.objClub.Club_Point_Process(GuID, 1, 0, Point_Old, ID_Games);
        }
        return num;
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        this.objmain.ErrorLogTemp(Pay_CashPortion.ToString() + ":" + Pay_BonusPortion.ToString());
        return -1;
      }
    }

    public int Card_Play_Details_Insert(
      string GuID,
      int Card_Cash_Price,
      int Card_Bonus_Price,
      int ID_GameCenter,
      string SwiperMacAddress,
      int GamePrice,
      int SumPrice,
      int Bonus,
      int IsPersonnel,
      int ID_Games,
      int ID_Swiper,
      int ID_Play_Type,
      int Pay_CashPortion,
      int Pay_BonusPortion,
      int Pay_GiftPortion,
      Guid ID_ActiveGiftForExtraBonus)
    {
      DataTable dataTable = new DataTable();
      try
      {
        int num = this.objmain.Max_Tbl("Card_Play_Details", "ID") + 1;
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand com = new SqlCommand("\nupdate Card set LAST_UPDATE=@Date where Card_GUID=@Card_GUID\r\n\r\nINSERT INTO[dbo].[Card_Play_Details]\n           ([ID]\n           ,[ID_GameCenter]\n           ,[SwiperMacAddress]\n           ,[Card_GUID]\n           ,[Date]\n           ,[Price],[SumPrice],[Bonus],[IsPersonnel],[ID_Games],[ID_Swiper]\r\n           ,[IsCancel],[ID_Play_Type],[Pay_CashPortion],[Pay_BonusPortion],\n           [Pay_GiftPortion],[ID_Gift_Pattern_series_List])\n     VALUES\n           (@ID\n           , @ID_GameCenter\n           , @SwiperMacAddress\n           , @Card_GUID\n           , @Date\n           , @Price, @SumPrice, @Bonus, @IsPersonnel, @ID_Games, @ID_Swiper\r\n           , 0, @ID_Play_Type, @Pay_CashPortion, @Pay_BonusPortion,\r\n           @Pay_GiftPortion, @ID_Gift_Pattern_series_List)", connection);
          com.Parameters.AddWithValue("@ID", (object) num);
          com.Parameters.AddWithValue("@ID_GameCenter", (object) ID_GameCenter);
          com.Parameters.AddWithValue("@SwiperMacAddress", (object) SwiperMacAddress);
          com.Parameters.AddWithValue("@Card_GUID", (object) GuID);
          com.Parameters.AddWithValue("@Date", (object) DateTime.Now);
          com.Parameters.AddWithValue("@Price", (object) GamePrice);
          com.Parameters.AddWithValue("@SumPrice", (object) SumPrice);
          com.Parameters.AddWithValue("@Bonus", (object) Bonus);
          com.Parameters.AddWithValue("@IsPersonnel", (object) IsPersonnel);
          com.Parameters.AddWithValue("@ID_Games", (object) ID_Games);
          com.Parameters.AddWithValue("@ID_Swiper", (object) ID_Swiper);
          com.Parameters.AddWithValue("@CashPrice", (object) Card_Cash_Price);
          com.Parameters.AddWithValue("@BonusPrice", (object) Card_Bonus_Price);
          com.Parameters.AddWithValue("@ID_Play_Type", (object) ID_Play_Type);
          com.Parameters.AddWithValue("@Pay_CashPortion", (object) Pay_CashPortion);
          com.Parameters.AddWithValue("@Pay_BonusPortion", (object) Pay_BonusPortion);
          com.Parameters.AddWithValue("@Pay_GiftPortion", (object) Pay_GiftPortion);
          com.Parameters.AddWithValue("@ID_Gift_Pattern_series_List", (object) ID_ActiveGiftForExtraBonus);
          com.ExecuteNonQuery();
          connection.Close();
          connection.Dispose();
          this.objmain.Synchronize_Insert(com);
          DataTable byCardGuid = this.Card_GetByCard_GUID(GuID);
          int Point_Old = 0;
          try
          {
            Point_Old = int.Parse(byCardGuid.Rows[0]["Club_Point"].ToString());
          }
          catch
          {
          }
          this.objClub.Club_Point_Process(GuID, 1, 0, Point_Old, ID_Games);
        }
        return num;
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        this.objmain.ErrorLogTemp(Pay_CashPortion.ToString() + ":" + Pay_BonusPortion.ToString());
        return -1;
      }
    }

    public int Card_PlayDetails_Insert_Temp(
      string GuID,
      int Card_Cash_Price,
      int Card_Bonus_Price,
      int ID_GameCenter,
      string SwiperMacAddress,
      int GamePrice,
      int SumPrice,
      int Bonus,
      int IsPersonnel,
      int ID_Games,
      int ID_Swiper,
      int ID_Play_Type,
      int Pay_CashPortion,
      int Pay_BonusPortion)
    {
      DataTable dataTable = new DataTable();
      try
      {
        int num = this.objmain.Max_Tbl("Card_Play_Details", "ID") + 1;
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand com = new SqlCommand("INSERT INTO [dbo].[Card_Play_Details]\r\n           ([ID]\r\n           ,[ID_GameCenter]\r\n           ,[SwiperMacAddress]\r\n           ,[Date]\r\n           ,[Price]\r\n           ,[SumPrice]\r\n           ,[Bonus]\r\n           ,[Card_GUID]\r\n           ,[IsPersonnel]\r\n           ,[ID_Games]\r\n           ,[ID_Swiper]\r\n           ,[IsCancel]\r\n           ,[ID_Play_Type]\r\n           ,[Pay_CashPortion]\r\n           ,[Pay_BonusPortion])\r\n     VALUES\r\n           (@ID\r\n           ,@ID_GameCenter\r\n           ,@SwiperMacAddress\r\n           ,@Date\r\n           ,@Price\r\n           ,@SumPrice\r\n           ,@Bonus\r\n           ,@Card_GUID\r\n           ,@IsPersonnel\r\n           ,@ID_Games\r\n           ,@ID_Swiper\r\n           ,@IsCancel\r\n           ,@ID_Play_Type\r\n           ,@Pay_CashPortion\r\n           ,@Pay_BonusPortion)", connection);
          com.Parameters.AddWithValue("@ID", (object) num);
          com.Parameters.AddWithValue("@ID_GameCenter", (object) ID_GameCenter);
          com.Parameters.AddWithValue("@SwiperMacAddress", (object) SwiperMacAddress);
          com.Parameters.AddWithValue("@Date", (object) DateTime.Now);
          com.Parameters.AddWithValue("@Price", (object) GamePrice);
          com.Parameters.AddWithValue("@SumPrice", (object) SumPrice);
          com.Parameters.AddWithValue("@Bonus", (object) Bonus);
          com.Parameters.AddWithValue("@Card_GUID", (object) GuID);
          com.Parameters.AddWithValue("@IsPersonnel", (object) IsPersonnel);
          com.Parameters.AddWithValue("@ID_Games", (object) ID_Games);
          com.Parameters.AddWithValue("@ID_Swiper", (object) ID_Swiper);
          com.Parameters.AddWithValue("@IsCancel", (object) 0);
          com.Parameters.AddWithValue("@ID_Play_Type", (object) 6);
          com.Parameters.AddWithValue("@Pay_CashPortion", (object) Pay_CashPortion);
          com.Parameters.AddWithValue("@Pay_BonusPortion", (object) Pay_BonusPortion);
          com.ExecuteNonQuery();
          this.objmain.Synchronize_Insert(com);
        }
        return num;
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return -1;
      }
    }

    public int Card_Play_Details_insert(
      int ID_GameCenter,
      string SwiperMacAddress,
      string GuID_Card,
      int Price,
      int SumPrice,
      int Bonus,
      int IsPersonnel,
      int ID_Games,
      int ID_Swiper)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand com = new SqlCommand("Card_Play_Details_Insert2", connection);
          com.Parameters.AddWithValue("@ID", (object) (this.objmain.Max_Tbl("Card_Play_Details", "ID") + 1));
          com.Parameters.AddWithValue("@ID_GameCenter", (object) ID_GameCenter);
          com.Parameters.AddWithValue("@SwiperMacAddress", (object) SwiperMacAddress);
          com.Parameters.AddWithValue("@Card_GUID", (object) GuID_Card);
          com.Parameters.AddWithValue("@Date", (object) DateTime.Now);
          com.Parameters.AddWithValue("@Price", (object) Price);
          com.Parameters.AddWithValue("@SumPrice", (object) SumPrice);
          com.Parameters.AddWithValue("@Bonus", (object) Bonus);
          com.Parameters.AddWithValue("@IsPersonnel", (object) IsPersonnel);
          com.Parameters.AddWithValue("@ID_Games", (object) ID_Games);
          com.Parameters.AddWithValue("@ID_Swiper", (object) ID_Swiper);
          com.CommandType = CommandType.StoredProcedure;
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

    public DataTable Card_Play_Details_GetByCardGUID(
      string SwiperMacAddress,
      string Card_GUID,
      int ID_GameCenter)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("select top(1) * from  Card_Play_Details where ( Card_GUID=@Card_GUID) and (SwiperMacAddress=@SwiperMacAddress) and (ID_GameCenter=@ID_GameCenter) and (Card_Play_Details.Date between  @FromDate and @ToDate ) order by ID desc ", connection);
          selectCommand.Parameters.AddWithValue("@Card_GUID", (object) Card_GUID);
          selectCommand.Parameters.AddWithValue("@SwiperMacAddress", (object) SwiperMacAddress);
          selectCommand.Parameters.AddWithValue("@ID_GameCenter", (object) ID_GameCenter);
          int num = -10;
          try
          {
            num = int.Parse(MainClass.key_Value_List.Select("KeyName ='TimeOfCheck_Card_Play_Details_ForTicket'")[0]["Value"].ToString());
          }
          catch
          {
          }
          selectCommand.Parameters.AddWithValue("@FromDate", (object) DateTime.Now.AddMinutes((double) num));
          selectCommand.Parameters.AddWithValue("@ToDate", (object) DateTime.Now);
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

    public DataTable Card_Play_Details_GetByCardGUID(string Card_GUID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("SELECT        TOP (5) Card_Play_Details.ID, Card_Play_Details.ID_GameCenter, Card_Play_Details.SwiperMacAddress, Card_Play_Details.ID_Card,dbo.MiladiTOShamsi( Card_Play_Details.Date) as Date,Card_Play_Details.Date as MiladiDate, Card_Play_Details.Price,(isnull( Card_Play_Details.SumPrice,0) +isnull( Card_Play_Details.Bonus,0)) as PriceKol , \r\n                         Card_Play_Details.Bonus, Card_Play_Details.Card_GUID, Card_Play_Details.IsPersonnel, Card_Play_Details.ID_Games, Card_Play_Details.ID_Swiper, Card_Play_Details.IsCancel, Games.Title AS GameTitle\r\nFROM            Card_Play_Details INNER JOIN\r\n                         Games ON Card_Play_Details.ID_Games = Games.ID AND Card_Play_Details.ID_GameCenter = Games.ID_GameCenter\r\nWHERE(Card_Play_Details.Card_GUID = @Card_GUID) ORDER BY Card_Play_Details.ID DESC", connection);
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

    public int Card_Type_insert(
      string Title,
      int ID_Card_Status,
      int MaxAllowedBalance,
      int MinBalanceForReissue,
      int MinBalanceForRegistration,
      int MinCashForRegistration,
      int NumberOfDaysKeep,
      int FreeDailyGames,
      int MinSpendPrice,
      int MinSpendDays)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand("Card_Type_Insert", connection);
          sqlCommand.Parameters.AddWithValue("@ID", (object) (this.objmain.Max_Tbl("Card_Type", "ID") + 1));
          sqlCommand.Parameters.AddWithValue("@Title ", (object) Title);
          sqlCommand.Parameters.AddWithValue("@ID_Card_Status", (object) ID_Card_Status);
          sqlCommand.Parameters.AddWithValue("@MaxAllowedBalance", (object) MaxAllowedBalance);
          sqlCommand.Parameters.AddWithValue("@MinBalanceForReissue", (object) MinBalanceForReissue);
          sqlCommand.Parameters.AddWithValue("@MinBalanceForRegistration", (object) MinBalanceForRegistration);
          sqlCommand.Parameters.AddWithValue("@MinCashForRegistration", (object) MinCashForRegistration);
          sqlCommand.Parameters.AddWithValue("@NumberOfDaysKeep", (object) NumberOfDaysKeep);
          sqlCommand.Parameters.AddWithValue("@FreeDailyGames", (object) FreeDailyGames);
          sqlCommand.Parameters.AddWithValue("@MinSpendPrice", (object) MinSpendPrice);
          sqlCommand.Parameters.AddWithValue("@MinSpendDays", (object) MinSpendDays);
          sqlCommand.Parameters.AddWithValue("@IsDeleted", (object) 0);
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

    public int Card_Type_Update(
      int ID,
      string Title,
      int ID_Card_Status,
      int MaxAllowedBalance,
      int MinBalanceForReissue,
      int MinBalanceForRegistration,
      int MinCashForRegistration,
      int NumberOfDaysKeep,
      int FreeDailyGames,
      int MinSpendPrice,
      int MinSpendDays)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand(nameof (Card_Type_Update), connection);
          sqlCommand.Parameters.AddWithValue("@ID", (object) ID);
          sqlCommand.Parameters.AddWithValue("@Title ", (object) Title);
          sqlCommand.Parameters.AddWithValue("@ID_Card_Status", (object) ID_Card_Status);
          sqlCommand.Parameters.AddWithValue("@MaxAllowedBalance", (object) MaxAllowedBalance);
          sqlCommand.Parameters.AddWithValue("@MinBalanceForReissue", (object) MinBalanceForReissue);
          sqlCommand.Parameters.AddWithValue("@MinBalanceForRegistration", (object) MinBalanceForRegistration);
          sqlCommand.Parameters.AddWithValue("@MinCashForRegistration", (object) MinCashForRegistration);
          sqlCommand.Parameters.AddWithValue("@NumberOfDaysKeep", (object) NumberOfDaysKeep);
          sqlCommand.Parameters.AddWithValue("@FreeDailyGames", (object) FreeDailyGames);
          sqlCommand.Parameters.AddWithValue("@MinSpendPrice", (object) MinSpendPrice);
          sqlCommand.Parameters.AddWithValue("@MinSpendDays", (object) MinSpendDays);
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

    public DataTable Card_Type_Get()
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("Card_Type_GetAll", connection);
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

    public DataTable Card_Type_Get(int ID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand(nameof (Card_Type_Get), connection);
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

    public int Card_Type_Delete(int ID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand(nameof (Card_Type_Delete), connection);
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

    public int CardProduct_insert(
      string Title,
      int ID_GameCenter,
      bool IsActive,
      bool AllowedNewCard,
      bool AllowedOldCard,
      string TextColor,
      string BackColor,
      int Priority,
      string AllowedCardTypeIds,
      int TenderedType,
      int TenderedAmount,
      int FreeGame,
      int DailyFreeGame,
      int DelayFreeGame,
      bool AllowChildCards,
      int MaxChildCards,
      int Price1,
      int Price2,
      int Price3,
      int Price4,
      int Price1MaxPeople,
      int Price2MaxPeople,
      int Price3MaxPeople,
      int Price4MaxPeople,
      int Bonus1,
      int Bonus2,
      int Bonus3,
      int Bonus4,
      int PageLayout,
      int ID_CardProduct_Group)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand("CardProduct_Insert", connection);
          sqlCommand.Parameters.AddWithValue("@ID", (object) (this.objmain.Max_Tbl("CardProduct", "ID") + 1));
          sqlCommand.Parameters.AddWithValue("@Title", (object) Title);
          sqlCommand.Parameters.AddWithValue("@ID_GameCenter", (object) ID_GameCenter);
          sqlCommand.Parameters.AddWithValue("@IsActive", (object) IsActive);
          sqlCommand.Parameters.AddWithValue("@AllowedNewCard", (object) AllowedNewCard);
          sqlCommand.Parameters.AddWithValue("@AllowedOldCard", (object) AllowedOldCard);
          sqlCommand.Parameters.AddWithValue("@TextColor", (object) TextColor);
          sqlCommand.Parameters.AddWithValue("@BackColor", (object) BackColor);
          sqlCommand.Parameters.AddWithValue("@Priority", (object) Priority);
          sqlCommand.Parameters.AddWithValue("@AllowedCardTypeIds", (object) AllowedCardTypeIds);
          sqlCommand.Parameters.AddWithValue("@TenderedType", (object) TenderedType);
          sqlCommand.Parameters.AddWithValue("@TenderedAmount", (object) TenderedAmount);
          sqlCommand.Parameters.AddWithValue("@FreeGame", (object) FreeGame);
          sqlCommand.Parameters.AddWithValue("@DailyFreeGame", (object) DailyFreeGame);
          sqlCommand.Parameters.AddWithValue("@DelayFreeGame", (object) DelayFreeGame);
          sqlCommand.Parameters.AddWithValue("@AllowChildCards", (object) AllowChildCards);
          sqlCommand.Parameters.AddWithValue("@MaxChildCards", (object) MaxChildCards);
          sqlCommand.Parameters.AddWithValue("@Price1", (object) Price1);
          sqlCommand.Parameters.AddWithValue("@Price2", (object) Price2);
          sqlCommand.Parameters.AddWithValue("@Price3", (object) Price3);
          sqlCommand.Parameters.AddWithValue("@Price4", (object) Price4);
          sqlCommand.Parameters.AddWithValue("@Price1MaxPeople", (object) Price1MaxPeople);
          sqlCommand.Parameters.AddWithValue("@Price2MaxPeople", (object) Price2MaxPeople);
          sqlCommand.Parameters.AddWithValue("@Price3MaxPeople", (object) Price3MaxPeople);
          sqlCommand.Parameters.AddWithValue("@Price4MaxPeople", (object) Price4MaxPeople);
          sqlCommand.Parameters.AddWithValue("@Bonus1", (object) Bonus1);
          sqlCommand.Parameters.AddWithValue("@Bonus2", (object) Bonus2);
          sqlCommand.Parameters.AddWithValue("@Bonus3", (object) Bonus3);
          sqlCommand.Parameters.AddWithValue("@Bonus4", (object) Bonus4);
          sqlCommand.Parameters.AddWithValue("@IsDeleted", (object) 0);
          sqlCommand.Parameters.AddWithValue("@Date", (object) DateTime.Now);
          sqlCommand.Parameters.AddWithValue("@PageLayout", (object) PageLayout);
          sqlCommand.Parameters.AddWithValue("@ID_CardProduct_Group", (object) ID_CardProduct_Group);
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

    public int CardProduct_Update(
      int ID,
      string Title,
      int ID_GameCenter,
      bool IsActive,
      bool AllowedNewCard,
      bool AllowedOldCard,
      string TextColor,
      string BackColor,
      int Priority,
      string AllowedCardTypeIds,
      int TenderedType,
      int TenderedAmount,
      int FreeGame,
      int DailyFreeGame,
      int DelayFreeGame,
      bool AllowChildCards,
      int MaxChildCards,
      int Price1,
      int Price2,
      int Price3,
      int Price4,
      int Price1MaxPeople,
      int Price2MaxPeople,
      int Price3MaxPeople,
      int Price4MaxPeople,
      int Bonus1,
      int Bonus2,
      int Bonus3,
      int Bonus4,
      int PageLayout,
      int ID_CardProduct_Group)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand(nameof (CardProduct_Update), connection);
          sqlCommand.Parameters.AddWithValue("@ID", (object) ID);
          sqlCommand.Parameters.AddWithValue("@Title", (object) Title);
          sqlCommand.Parameters.AddWithValue("@ID_GameCenter", (object) ID_GameCenter);
          sqlCommand.Parameters.AddWithValue("@IsActive", (object) IsActive);
          sqlCommand.Parameters.AddWithValue("@AllowedNewCard", (object) AllowedNewCard);
          sqlCommand.Parameters.AddWithValue("@AllowedOldCard", (object) AllowedOldCard);
          sqlCommand.Parameters.AddWithValue("@TextColor", (object) TextColor);
          sqlCommand.Parameters.AddWithValue("@BackColor", (object) BackColor);
          sqlCommand.Parameters.AddWithValue("@Priority", (object) Priority);
          sqlCommand.Parameters.AddWithValue("@AllowedCardTypeIds", (object) AllowedCardTypeIds);
          sqlCommand.Parameters.AddWithValue("@TenderedType", (object) TenderedType);
          sqlCommand.Parameters.AddWithValue("@TenderedAmount", (object) TenderedAmount);
          sqlCommand.Parameters.AddWithValue("@FreeGame", (object) FreeGame);
          sqlCommand.Parameters.AddWithValue("@DailyFreeGame", (object) DailyFreeGame);
          sqlCommand.Parameters.AddWithValue("@DelayFreeGame", (object) DelayFreeGame);
          sqlCommand.Parameters.AddWithValue("@AllowChildCards", (object) AllowChildCards);
          sqlCommand.Parameters.AddWithValue("@MaxChildCards", (object) MaxChildCards);
          sqlCommand.Parameters.AddWithValue("@Price1", (object) Price1);
          sqlCommand.Parameters.AddWithValue("@Price2", (object) Price2);
          sqlCommand.Parameters.AddWithValue("@Price3", (object) Price3);
          sqlCommand.Parameters.AddWithValue("@Price4", (object) Price4);
          sqlCommand.Parameters.AddWithValue("@Price1MaxPeople", (object) Price1MaxPeople);
          sqlCommand.Parameters.AddWithValue("@Price2MaxPeople", (object) Price2MaxPeople);
          sqlCommand.Parameters.AddWithValue("@Price3MaxPeople", (object) Price3MaxPeople);
          sqlCommand.Parameters.AddWithValue("@Price4MaxPeople", (object) Price4MaxPeople);
          sqlCommand.Parameters.AddWithValue("@Bonus1", (object) Bonus1);
          sqlCommand.Parameters.AddWithValue("@Bonus2", (object) Bonus2);
          sqlCommand.Parameters.AddWithValue("@Bonus3", (object) Bonus3);
          sqlCommand.Parameters.AddWithValue("@Bonus4", (object) Bonus4);
          sqlCommand.Parameters.AddWithValue("@Date", (object) DateTime.Now);
          sqlCommand.Parameters.AddWithValue("@PageLayout", (object) PageLayout);
          sqlCommand.Parameters.AddWithValue("@ID_CardProduct_Group", (object) ID_CardProduct_Group);
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

    public DataTable CardProduct_Get()
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("CardProduct_GetAll", connection);
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

    public DataTable CardProduct_Get(int ID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand(nameof (CardProduct_Get), connection);
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

    public DataTable CardProduct_GetByGroup(int ID_CardProduct_Group)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand(nameof (CardProduct_GetByGroup), connection);
          selectCommand.Parameters.AddWithValue("@ID_CardProduct_Group", (object) ID_CardProduct_Group);
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

    public int CardProduct_Delete(int ID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand(nameof (CardProduct_Delete), connection);
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

    public DataTable CardProduct_Group_Get()
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          new SqlDataAdapter(new SqlCommand("select * from CardProduct_Group ", connection)).Fill(dataTable);
        }
        return dataTable;
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
        return dataTable;
      }
    }

    public DataTable Card_CardProductTiming_Get(string Card_GUID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("select * from Card_CardProductTiming where Card_GUID=@Card_GUID order by ID desc", connection);
          selectCommand.Parameters.AddWithValue("@Card_GUID", (object) Card_GUID);
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

    public Tuple<bool, int, int, int, int, bool, string> Card_CardProductTiming_Status(
      string Card_GUID,
      int GameID)
    {
      bool flag1 = false;
      int num1 = 0;
      int num2 = -1;
      int num3 = 0;
      int num4 = 0;
      bool flag2 = false;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      DateTime now1 = DateTime.Now;
      string str1 = "";
      try
      {
        DataTable dataTable1 = this.Card_CardProductTiming_Get(Card_GUID);
        if (dataTable1.Rows.Count > 0)
        {
          int num8 = 0;
          int num9 = 0;
          int num10 = 0;
          int num11 = 0;
          int num12 = 0;
          int num13 = 0;
          int num14 = 0;
          int num15 = 0;
          DateTime dateTime1 = new DateTime();
          DateTime Start_AfterDate = Convert.ToDateTime(dataTable1.Rows[0]["Start_AfterDate"].ToString());
          DataTable dataTable2 = this.objCashier.Orders_Get(int.Parse(dataTable1.Rows[0]["ID_Order"].ToString()), int.Parse(dataTable1.Rows[0]["ID_GameCenter"].ToString()));
          if (dataTable2.Rows.Count > 0)
            Start_AfterDate = !(Convert.ToDateTime(dataTable1.Rows[0]["Start_AfterDate"].ToString()) <= Convert.ToDateTime(dataTable2.Rows[0]["Date"].ToString())) ? Convert.ToDateTime(dataTable1.Rows[0]["Start_AfterDate"].ToString()) : Convert.ToDateTime(dataTable2.Rows[0]["Date"].ToString());
          num2 = int.Parse(dataTable1.Rows[0]["ID"].ToString());
          try
          {
            num1 = int.Parse(dataTable1.Rows[0]["TimingChargePrice"].ToString());
            num3 = int.Parse(dataTable1.Rows[0]["TimingChargePrice_Real"].ToString());
          }
          catch
          {
          }
          DataTable dataTable3 = this.CardProduct_Get(int.Parse(dataTable1.Rows[0]["ID_CardProduct"].ToString()));
          try
          {
            num4 = int.Parse(dataTable1.Rows[0]["FreeGame"].ToString());
            num5 = int.Parse(dataTable1.Rows[0]["FreeGame_Real"].ToString());
            num6 = int.Parse(dataTable3.Rows[0]["DailyFreeGame"].ToString());
          }
          catch
          {
          }
          if (num6 > 0)
          {
            DataTable onle5Today = this.Card_Play_Details_GetOnle5Today(Card_GUID, Start_AfterDate);
            if (onle5Today.Rows.Count < num6)
              flag2 = true;
            num7 = num6 - onle5Today.Rows.Count;
          }
          int num16 = int.Parse(dataTable3.Rows[0]["DelayPlayTimingCard"].ToString());
          dataTable3.Rows[0]["AllowGames_ClassIDs"].ToString().Split(',');
          string[] strArray = dataTable3.Rows[0]["AllowGamesIDs"].ToString().Split(',');
          int num17 = dataTable3.Rows[0]["AllowGamesPlayCountAll"].ToString() == "" ? 0 : int.Parse(dataTable3.Rows[0]["AllowGamesPlayCountAll"].ToString());
          int num18 = dataTable3.Rows[0]["AllowGamesPlayCount"].ToString() == "" ? 0 : int.Parse(dataTable3.Rows[0]["AllowGamesPlayCount"].ToString());
          string str2 = dataTable3.Rows[0]["AllowDayList"].ToString();
          int dayOfWeek = (int) now1.DayOfWeek;
          if (str2.Contains(dayOfWeek.ToString()))
            num14 = 1;
          for (int index = 0; index < strArray.Length; ++index)
          {
            if (int.Parse(strArray[index]) == GameID)
              num15 = 1;
          }
          DataTable onle5 = this.Card_Play_Details_GetOnle5(Card_GUID, Start_AfterDate);
          if (num17 > 0)
            num13 = num17 <= onle5.Rows.Count ? 0 : 1;
          else if (num17 == 0)
            num13 = 1;
          if (num13 == 1)
          {
            if (num18 > 0)
            {
              int count = this.Card_Play_Details_GetByID_Play_Type5(Card_GUID, GameID, Start_AfterDate).Rows.Count;
              num13 = num18 <= count ? 0 : 1;
            }
            else if (num18 == 0)
              num13 = 1;
          }
          string s1 = "0";
          string s2 = "0";
          try
          {
            string s3 = now1.ToString("HH:mm").Split(':')[0].ToString() + now1.ToString("HH:mm").Split(':')[1].ToString();
            string str3 = dataTable1.Rows[dataTable1.Rows.Count - 1]["Time_Start"].ToString();
            s1 = str3.Split(':')[0].ToString() + str3.Split(':')[1].ToString();
            string str4 = dataTable1.Rows[dataTable1.Rows.Count - 1]["Time_End"].ToString();
            s2 = str4.Split(':')[0].ToString() + str4.Split(':')[1].ToString();
            if (int.Parse(s1) <= int.Parse(s3) && int.Parse(s2) >= int.Parse(s3))
              num12 = 1;
          }
          catch
          {
          }
          if (num15 == 1 && num12 == 1 && num13 == 1)
          {
            if (onle5.Rows.Count > 0)
            {
              DateTime dateTime2 = Convert.ToDateTime(onle5.Rows[onle5.Rows.Count - 1]["Date"]);
              num8 = !(now1 > dateTime2.AddMinutes((double) num16)) ? 0 : 1;
            }
            else
              num8 = 1;
            if (num8 == 1)
            {
              bool boolean1 = Convert.ToBoolean(dataTable1.Rows[0]["Start_IsQuick"].ToString());
              bool boolean2 = Convert.ToBoolean(dataTable1.Rows[0]["Start_AfterFirstUse"].ToString());
              Convert.ToBoolean(dataTable1.Rows[0]["Start_AfterFirstUseGroup"].ToString());
              bool boolean3 = Convert.ToBoolean(dataTable1.Rows[0]["Start_IsAfterDate"].ToString());
              DateTime dateTime2 = Convert.ToDateTime(dataTable1.Rows[0]["Start_AfterDate"].ToString());
              bool boolean4 = Convert.ToBoolean(dataTable1.Rows[0]["End_IsAfter"].ToString());
              bool boolean5 = Convert.ToBoolean(dataTable1.Rows[0]["End_IsAfter_Date"].ToString());
              int num19 = int.Parse(dataTable1.Rows[0]["End_After"].ToString());
              int num20 = int.Parse(dataTable1.Rows[0]["End_After_ID_TimeType"].ToString());
              DateTime dateTime3 = Convert.ToDateTime(dataTable1.Rows[0]["End_After_Date"].ToString());
              bool boolean6 = Convert.ToBoolean(dataTable1.Rows[0]["Exp_IsAfter"].ToString());
              bool boolean7 = Convert.ToBoolean(dataTable1.Rows[0]["Exp_IsAfter_Date"].ToString());
              int num21 = int.Parse(dataTable1.Rows[0]["Exp_After"].ToString());
              int num22 = int.Parse(dataTable1.Rows[0]["Exp_After_ID_TimeType"].ToString());
              DateTime dateTime4 = Convert.ToDateTime(dataTable1.Rows[0]["Exp_After_Date"].ToString());
              if (boolean2 | boolean1)
                num9 = 1;
              if (boolean3)
                num9 = !(dateTime2 <= now1) ? 0 : 1;
              DateTime dateTime5;
              if (boolean4)
              {
                int num23 = 0;
                switch (num20)
                {
                  case 1:
                    num23 = num19 * 24 * 60;
                    break;
                  case 2:
                    num23 = num19 * 60;
                    break;
                  case 3:
                    num23 = num19;
                    break;
                }
                if (boolean2)
                {
                  if (onle5.Rows.Count > 0)
                  {
                    dateTime5 = Convert.ToDateTime(onle5.Rows[0]["Date"].ToString());
                    num10 = !(dateTime5.AddMinutes((double) num23) >= now1) ? 0 : 1;
                  }
                  else
                    num10 = 1;
                }
                if (boolean1)
                  num10 = !(Start_AfterDate.AddMinutes((double) num23) >= now1) ? 0 : 1;
              }
              if (boolean5)
                num10 = !(dateTime3 >= now1) ? 0 : 1;
              if (!boolean5 & !boolean4)
                num10 = 1;
              if (boolean6)
              {
                int num23 = 0;
                switch (num22)
                {
                  case 1:
                    num23 = num21 * 24 * 60;
                    break;
                  case 2:
                    num23 = num21 * 60;
                    break;
                  case 3:
                    num23 = num21;
                    break;
                }
                int num24;
                if (boolean2)
                {
                  if (onle5.Rows.Count > 0)
                  {
                    dateTime5 = Convert.ToDateTime(onle5.Rows[0]["Date"].ToString());
                    num24 = !(dateTime5.AddMinutes((double) num23) >= now1) ? 0 : 1;
                  }
                  else
                    num24 = 1;
                }
                num11 = !boolean1 ? 0 : (!(Start_AfterDate.AddMinutes((double) num23) >= now1) ? 0 : 1);
              }
              if (boolean7)
                num11 = !(dateTime4 > now1) ? 0 : 1;
              if (!boolean7 && !boolean6)
                num11 = 1;
            }
          }
          DateTime now2 = DateTime.Now;
          string str5 = now2.ToString("HH:mm").Split(':')[0].ToString() + now2.ToString("HH:mm").Split(':')[1].ToString();
          str1 = "ppServer:" + (object) num8 + "," + (object) num15 + "," + (object) num12 + "," + (object) num10 + "," + (object) num11 + "," + (object) num9 + "," + (object) num13 + "," + str5 + ",TimeStartOk:" + s1 + ",TimeEndOk:" + s2 + ",FlagAllowDay:" + (object) num14 + ",AllowDayList:" + str2 + ",DayIndexNow:" + dayOfWeek.ToString();
          if (num8 * num15 * num12 * num10 * num11 * num9 * num13 * num14 == 1)
            flag1 = true;
        }
        else
          flag1 = false;
      }
      catch (Exception ex)
      {
        this.objmain.ErrorLog(ex);
      }
      string str6 = num4.ToString() + "," + (object) num6 + "," + (object) num7;
      return Tuple.Create<bool, int, int, int, int, bool, string>(flag1, num3, num2, num1, num5, flag2, str6);
    }

    public int Card_CardProductTiming_SetChargePrice(
      string Card_GUID,
      int IDCard_CardProductTiming,
      int TimingChargePrice_Real)
    {
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand com = new SqlCommand("update [dbo].[Card_CardProductTiming] set [TimingChargePrice_Real]=@TimingChargePrice_Real where [Card_GUID]=@Card_GUID and ID=@ID ", connection);
          com.Parameters.AddWithValue("@TimingChargePrice_Real", (object) TimingChargePrice_Real);
          com.Parameters.AddWithValue("@Card_GUID", (object) Card_GUID);
          com.Parameters.AddWithValue("@ID", (object) IDCard_CardProductTiming);
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

    public int Card_CardProductTiming_SetFreeGame(
      string Card_GUID,
      int IDCard_CardProductTiming,
      int FreeGame_Real)
    {
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand com = new SqlCommand("update [dbo].[Card_CardProductTiming] set [FreeGame_Real]=@FreeGame_Real where [Card_GUID]=@Card_GUID and ID=@ID ", connection);
          com.Parameters.AddWithValue("@FreeGame_Real", (object) FreeGame_Real);
          com.Parameters.AddWithValue("@Card_GUID", (object) Card_GUID);
          com.Parameters.AddWithValue("@ID", (object) IDCard_CardProductTiming);
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

    public DataTable Card_Play_Details_Get(string Card_GUID)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("select * from Card_Play_Details where Card_GUID=@Card_GUID order by Date ASC ", connection);
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

    public DataTable Card_Play_Details_GetOnle5(string Card_GUID, DateTime Start_AfterDate)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("select * from Card_Play_Details where Card_GUID=@Card_GUID and ID_Play_Type in(5,9,10,11) and IsCancel=0 and Date>=@Start_AfterDate order by Date ASC ", connection);
          selectCommand.Parameters.AddWithValue("@Card_GUID", (object) Card_GUID);
          selectCommand.Parameters.AddWithValue("@Start_AfterDate", (object) Start_AfterDate);
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

    public DataTable Card_Play_Details_GetOnle5Today(
      string Card_GUID,
      DateTime Start_AfterDate)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("select * from Card_Play_Details where Card_GUID=@Card_GUID and ID_Play_Type in(5,9,10,11) and IsCancel=0 and Date>=@Start_AfterDate and  cast(Date as date)=cast(@DateNow as date) order by Date ASC ", connection);
          selectCommand.Parameters.AddWithValue("@Card_GUID", (object) Card_GUID);
          selectCommand.Parameters.AddWithValue("@Start_AfterDate", (object) Start_AfterDate);
          selectCommand.Parameters.AddWithValue("@DateNow", (object) DateTime.Now);
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

    public DataTable Card_Play_Details_Get(string Card_GUID, int ID_Games)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("select * from Card_Play_Details where Card_GUID=@Card_GUID and ID_Games=@ID_Games and IsCancel=0 order by Date ASC ", connection);
          selectCommand.Parameters.AddWithValue("@Card_GUID", (object) Card_GUID);
          selectCommand.Parameters.AddWithValue("@ID_Games", (object) ID_Games);
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

    public DataTable Card_Play_Details_GetByID_Play_Type5(
      string Card_GUID,
      int ID_Games,
      DateTime Start_AfterDate)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("select * from Card_Play_Details where Card_GUID=@Card_GUID and ID_Games=@ID_Games and IsCancel=0 and ID_Play_Type in(5,9,10,11) and Date>=@Start_AfterDate order by Date ASC ", connection);
          selectCommand.Parameters.AddWithValue("@Card_GUID", (object) Card_GUID);
          selectCommand.Parameters.AddWithValue("@ID_Games", (object) ID_Games);
          selectCommand.Parameters.AddWithValue("@Start_AfterDate", (object) Start_AfterDate);
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

    public DataTable Card_Play_Details_Get(string Card_GUID, string ID_Games)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("select * from Card_Play_Details where Card_GUID=@Card_GUID and ID_Games in (" + ID_Games + ") and IsCancel=0 order by Date ASC ", connection);
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

    public DataTable Card_Play_Details_Get_Today(string Card_GUID, string ID_Games)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("select * from Card_Play_Details where  Card_GUID=@Card_GUID and ID_Games in (" + ID_Games + ") and IsCancel=0 and cast( Date as date)=cast(@FromDate as date) and  ID_Play_Type=3  order by Date ASC", connection);
          selectCommand.Parameters.AddWithValue("@Card_GUID", (object) Card_GUID);
          selectCommand.Parameters.AddWithValue("@FromDate", (object) DateTime.Now);
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

    public DataTable Card_Play_Details_GetByCLassID(string Card_GUID, int ID_Games_Class)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("SELECT        Card_Play_Details.ID, Card_Play_Details.ID_GameCenter, Card_Play_Details.SwiperMacAddress, Card_Play_Details.ID_Card, Card_Play_Details.Date, Card_Play_Details.Price, Card_Play_Details.SumPrice, \r\n                         Card_Play_Details.Bonus, Card_Play_Details.Card_GUID, Card_Play_Details.IsPersonnel, Card_Play_Details.ID_Games, Card_Play_Details.ID_Swiper, Card_Play_Details.IsCancel, Card_Play_Details.IsCancel_Des,\r\n                         Card_Play_Details.IsCancel_Date, Card_Play_Details.IsCancel_User, Card_Play_Details.IsCancel_CashierSession, Games.ID_Games_Class\r\nFROM            Card_Play_Details INNER JOIN\r\n                         Games ON Card_Play_Details.ID_Games = Games.ID\r\nWHERE(Card_Play_Details.Card_GUID = @Card_GUID) AND (Games.ID_Games_Class = @ID_Games_Class) and(Card_Play_Details.IsCancel=0)  ", connection);
          selectCommand.Parameters.AddWithValue("@Card_GUID", (object) Card_GUID);
          selectCommand.Parameters.AddWithValue("@ID_Games_Class", (object) ID_Games_Class);
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

    public DataTable Card_Play_Details_GetByCLassIDByID_Play_Type(
      string Card_GUID,
      int ID_Games_Class,
      int ID_Play_Type)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand selectCommand = new SqlCommand("SELECT        Card_Play_Details.ID, Card_Play_Details.ID_GameCenter, Card_Play_Details.SwiperMacAddress, Card_Play_Details.ID_Card, Card_Play_Details.Date, Card_Play_Details.Price, Card_Play_Details.SumPrice, \r\n                         Card_Play_Details.Bonus, Card_Play_Details.Card_GUID, Card_Play_Details.IsPersonnel, Card_Play_Details.ID_Games, Card_Play_Details.ID_Swiper, Card_Play_Details.IsCancel, Card_Play_Details.IsCancel_Des,\r\n                         Card_Play_Details.IsCancel_Date, Card_Play_Details.IsCancel_User, Card_Play_Details.IsCancel_CashierSession, Games.ID_Games_Class\r\nFROM            Card_Play_Details INNER JOIN\r\n                         Games ON Card_Play_Details.ID_Games = Games.ID\r\nWHERE(Card_Play_Details.Card_GUID = @Card_GUID) AND (Games.ID_Games_Class = @ID_Games_Class) and(Card_Play_Details.IsCancel=0) and (Card_Play_Details.ID_Play_Type=@ID_Play_Type)  ", connection);
          selectCommand.Parameters.AddWithValue("@Card_GUID", (object) Card_GUID);
          selectCommand.Parameters.AddWithValue("@ID_Games_Class", (object) ID_Games_Class);
          selectCommand.Parameters.AddWithValue("@ID_Play_Type", (object) ID_Play_Type);
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

    public int Card_Ticket_History_insert(
      int ID_GameCenter,
      int ID_Cashier_Session,
      int Count,
      string Card_GUID,
      int OldCount,
      int ID_Card_Ticket_History_Type,
      int ID_Games_Ticket)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand com = new SqlCommand("\r\n                     INSERT INTO [dbo].[Card_Ticket_History]\r\n                     ([ID]\r\n                     ,[ID_GameCenter]\r\n                     ,[ID_Cashier_Session]\r\n                     ,[Date]\r\n                     ,[Count]\r\n                     ,[Card_GUID],[OldCount],[ID_Card_Ticket_History_Type],[ID_Games_Ticket])\r\n                     VALUES\r\n                     (@ID\r\n                     ,@ID_GameCenter\r\n                     ,@ID_Cashier_Session\r\n                     ,@Date\r\n                     ,@Count\r\n                     ,@Card_GUID,@OldCount,@ID_Card_Ticket_History_Type,@ID_Games_Ticket)", connection);
          com.Parameters.AddWithValue("@ID", (object) (this.objmain.Max_Tbl("Card_Ticket_History", "ID") + 1));
          com.Parameters.AddWithValue("@ID_GameCenter", (object) ID_GameCenter);
          com.Parameters.AddWithValue("@ID_Cashier_Session", (object) ID_Cashier_Session);
          com.Parameters.AddWithValue("@Date", (object) DateTime.Now);
          com.Parameters.AddWithValue("@Count", (object) Count);
          com.Parameters.AddWithValue("@Card_GUID", (object) Card_GUID);
          com.Parameters.AddWithValue("@OldCount", (object) OldCount);
          com.Parameters.AddWithValue("@ID_Card_Ticket_History_Type", (object) ID_Card_Ticket_History_Type);
          com.Parameters.AddWithValue("@ID_Games_Ticket", (object) ID_Games_Ticket);
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

    public int Card_SetEtickets(string Card_GUID, int Etickets)
    {
      DataTable dataTable = new DataTable();
      try
      {
        using (SqlConnection connection = new SqlConnection(this.objmain.DBPath()))
        {
          connection.Open();
          SqlCommand com = new SqlCommand("update Card set Etickets=@Etickets ,LAST_UPDATE=@Date where Card_GUID=@Card_GUID ", connection);
          com.Parameters.AddWithValue("@Etickets", (object) Etickets);
          com.Parameters.AddWithValue("@Card_GUID", (object) Card_GUID);
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
  }
}
