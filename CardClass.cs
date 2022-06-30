using ClickServerService.ClassCode;
using System.Data.SqlClient;
using System.Data;
using System;

namespace ClickServerService
{
    internal class CardClass
    {
        private readonly MainClass objMain = new MainClass();
        private readonly Club objClub = new Club();
        private readonly CashierClass objCashier = new CashierClass();

        public DataTable Card_Status_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Card_Status_GetAll", connection) { CommandType = CommandType.StoredProcedure };
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

        public int Card_Status_Update(int ID, string Title, int ReswipeDelay, int ConsecutiveSwipes, bool DisplayBalance, bool Is_Party, bool Is_TimePlay, bool Is_Staff, bool AllowAllCategory, bool DisAllowAllCategory, string DisallowList)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Card_Status_Update), connection) { CommandType = CommandType.StoredProcedure };
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@ReswipeDelay", ReswipeDelay);
                    sqlCommand.Parameters.AddWithValue("@ConsecutiveSwipes", ConsecutiveSwipes);
                    sqlCommand.Parameters.AddWithValue("@DisplayBalance", DisplayBalance);
                    sqlCommand.Parameters.AddWithValue("@Is_Party", Is_Party);
                    sqlCommand.Parameters.AddWithValue("@Is_TimePlay", Is_TimePlay);
                    sqlCommand.Parameters.AddWithValue("@Is_Staff", Is_Staff);
                    sqlCommand.Parameters.AddWithValue("@AllowAllCategory", AllowAllCategory);
                    sqlCommand.Parameters.AddWithValue("@DisAllowAllCategory", DisAllowAllCategory);
                    sqlCommand.Parameters.AddWithValue("@DisallowList", DisallowList);
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

        public DataTable Card_Status_Get(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(Card_Status_Get), connection) { CommandType = CommandType.StoredProcedure };
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

        public int Card_Status_Delete(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Card_Status_Delete), connection) { CommandType = CommandType.StoredProcedure };
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

        public int Card_Series_Update(int ID, int ID_GameCenter, string CardPrefix, string BarcodeFrom, string BarcodeTo, DateTime PurgeDate)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Card_Series_Update), connection)  {   CommandType = CommandType.StoredProcedure };
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@CardPrefix", CardPrefix);
                    sqlCommand.Parameters.AddWithValue("@BarcodeFrom", BarcodeFrom);
                    sqlCommand.Parameters.AddWithValue("@BarcodeTo", BarcodeTo);
                    sqlCommand.Parameters.AddWithValue("@PurgeDate", PurgeDate);
                    sqlCommand.Parameters.AddWithValue("@Date", DateTime.Now);
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

        public DataTable Card_Series_GetAllByGameCenter(int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(Card_Series_GetAllByGameCenter), connection) { CommandType = CommandType.StoredProcedure };
                    selectCommand.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
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

        public DataTable Card_Series_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Card_Series_GetAll", connection) { CommandType = CommandType.StoredProcedure };
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

        public DataTable Card_Series_Get(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(Card_Series_Get), connection) { CommandType = CommandType.StoredProcedure };
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

        public int Card_Series_Delete(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Card_Series_Delete), connection) { CommandType = CommandType.StoredProcedure };
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

        public DataTable Card_Get(int ID_Card_Series)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(Card_Get), connection) { CommandType = CommandType.StoredProcedure };
                    selectCommand.Parameters.AddWithValue("@ID_Card_Series", ID_Card_Series);
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

        public DataTable Card_GetByMacAddrress(string MacCode)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Card_GetByMacAddrress_Service", connection) { CommandType = CommandType.StoredProcedure };
                    selectCommand.Parameters.AddWithValue("@MacCode", MacCode.ToUpper());
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

        public DataSet Card_GetByMacAddrressFFFFFF(string MacCode)
        {
            DataSet dataSet = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Card_GetByMacAddrress_FFFFFFF_Service", connection) { CommandType = CommandType.StoredProcedure };
                    selectCommand.Parameters.AddWithValue("@MacCode", MacCode.ToUpper());
                    new SqlDataAdapter(selectCommand).Fill(dataSet);
                    connection.Close();
                    connection.Dispose();
                }
                return dataSet;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return dataSet;
            }
        }

        public int Card_UpdatePriceAndBonus_PlayDetails(string GuID, int Card_Cash_Price, int Card_Bonus_Price, int ID_GameCenter, string SwiperMacAddress, int GamePrice, int SumPrice, int Bonus, int IsPersonnel, int ID_Games, int ID_Swiper, int ID_Play_Type)
        {
            DataTable dataTable = new DataTable();
            try
            {
                int num = objMain.Max_Tbl("Card_Play_Details", "ID") + 1;
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand(nameof(Card_UpdatePriceAndBonus_PlayDetails), connection) { CommandType = CommandType.StoredProcedure };
                    com.Parameters.AddWithValue("@ID", num);
                    com.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
                    com.Parameters.AddWithValue("@SwiperMacAddress", SwiperMacAddress);
                    com.Parameters.AddWithValue("@Card_GUID", GuID);
                    com.Parameters.AddWithValue("@Date", DateTime.Now);
                    com.Parameters.AddWithValue("@Price", GamePrice);
                    com.Parameters.AddWithValue("@SumPrice", SumPrice);
                    com.Parameters.AddWithValue("@Bonus", Bonus);
                    com.Parameters.AddWithValue("@IsPersonnel", IsPersonnel);
                    com.Parameters.AddWithValue("@ID_Games", ID_Games);
                    com.Parameters.AddWithValue("@ID_Swiper", ID_Swiper);
                    com.Parameters.AddWithValue("@CashPrice", Card_Cash_Price);
                    com.Parameters.AddWithValue("@BonusPrice", Card_Bonus_Price);
                    com.Parameters.AddWithValue("@ID_Play_Type", ID_Play_Type);
                    com.ExecuteNonQuery();
                    objMain.Synchronize_Insert(com);
                }
                return num;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
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
                    using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                    {
                        connection.Open();
                        SqlCommand selectCommand = new SqlCommand("Card_GetByCard_GUID_2", connection) { CommandType = CommandType.StoredProcedure };
                        selectCommand.Parameters.AddWithValue("@Card_GUID", Card_GUID);
                        new SqlDataAdapter(selectCommand).Fill(dataTable);
                        connection.Close();
                    }
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                objMain.ErrorLogTemp(Card_GUID);
                return dataTable;
            }
        }

        public int Card_UpdatePriceAndBonus_PlayDetails2(string GuID, int Card_Cash_Price, int Card_Bonus_Price, int ID_GameCenter, string SwiperMacAddress, int GamePrice, int SumPrice, int Bonus, int IsPersonnel, int ID_Games, int ID_Swiper, int ID_Play_Type, int Pay_CashPortion, int Pay_BonusPortion, int Pay_GiftPortion, Guid ID_ActiveGiftForExtraBonus)
        {
            DataTable dataTable = new DataTable();
            try
            {
                int num = objMain.Max_Tbl("Card_Play_Details", "ID") + 1;
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand(nameof(Card_UpdatePriceAndBonus_PlayDetails2), connection) { CommandType = CommandType.StoredProcedure };
                    com.Parameters.AddWithValue("@ID", num);
                    com.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
                    com.Parameters.AddWithValue("@SwiperMacAddress", SwiperMacAddress);
                    com.Parameters.AddWithValue("@Card_GUID", GuID);
                    com.Parameters.AddWithValue("@Date", DateTime.Now);
                    com.Parameters.AddWithValue("@Price", GamePrice);
                    com.Parameters.AddWithValue("@SumPrice", SumPrice);
                    com.Parameters.AddWithValue("@Bonus", Bonus);
                    com.Parameters.AddWithValue("@IsPersonnel", IsPersonnel);
                    com.Parameters.AddWithValue("@ID_Games", ID_Games);
                    com.Parameters.AddWithValue("@ID_Swiper", ID_Swiper);
                    com.Parameters.AddWithValue("@CashPrice", Card_Cash_Price);
                    com.Parameters.AddWithValue("@BonusPrice", Card_Bonus_Price);
                    com.Parameters.AddWithValue("@ID_Play_Type", ID_Play_Type);
                    com.Parameters.AddWithValue("@Pay_CashPortion", Pay_CashPortion);
                    com.Parameters.AddWithValue("@Pay_BonusPortion", Pay_BonusPortion);
                    com.Parameters.AddWithValue("@Pay_GiftPortion", Pay_GiftPortion);
                    com.Parameters.AddWithValue("@ID_Gift_Pattern_series_List", ID_ActiveGiftForExtraBonus);
                    com.ExecuteNonQuery();
                    connection.Close();
                    connection.Dispose();
                    objMain.Synchronize_Insert(com);
                    DataTable byCardGuid = Card_GetByCard_GUID(GuID);
                    int Point_Old = 0;
                    try
                    {
                        Point_Old = int.Parse(byCardGuid.Rows[0]["Club_Point"].ToString());
                    }
                    catch
                    {
                    }
                    objClub.Club_Point_Process(GuID, 1, 0, Point_Old, ID_Games);
                }
                return num;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                objMain.ErrorLogTemp(Pay_CashPortion.ToString() + ":" + Pay_BonusPortion.ToString());
                return -1;
            }
        }

        public int Card_Play_Details_Insert(string GuID, int Card_Cash_Price, int Card_Bonus_Price, int ID_GameCenter, string SwiperMacAddress, int GamePrice, int SumPrice, int Bonus, int IsPersonnel, int ID_Games, int ID_Swiper, int ID_Play_Type, int Pay_CashPortion, int Pay_BonusPortion, int Pay_GiftPortion, Guid ID_ActiveGiftForExtraBonus)
        {
            try
            {
                int num = objMain.Max_Tbl("Card_Play_Details", "ID") + 1;
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand("Update Card set LAST_UPDATE=@Date where Card_GUID=@Card_GUID    INSERT INTO[dbo].[Card_Play_Details]            ([ID]            ,[ID_GameCenter]            ,[SwiperMacAddress]            ,[Card_GUID]            ,[Date]            ,[Price],[SumPrice],[Bonus],[IsPersonnel],[ID_Games],[ID_Swiper]             ,[IsCancel],[ID_Play_Type],[Pay_CashPortion],[Pay_BonusPortion],            [Pay_GiftPortion],[ID_Gift_Pattern_series_List])      VALUES            (@ID            , @ID_GameCenter            , @SwiperMacAddress            , @Card_GUID            , @Date  , @Price, @SumPrice, @Bonus, @IsPersonnel, @ID_Games, @ID_Swiper , 0, @ID_Play_Type, @Pay_CashPortion, @Pay_BonusPortion, @Pay_GiftPortion, @ID_Gift_Pattern_series_List)", connection);
                    com.Parameters.AddWithValue("@ID", num);
                    com.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
                    com.Parameters.AddWithValue("@SwiperMacAddress", SwiperMacAddress);
                    com.Parameters.AddWithValue("@Card_GUID", GuID);
                    com.Parameters.AddWithValue("@Date", DateTime.Now);
                    com.Parameters.AddWithValue("@Price", GamePrice);
                    com.Parameters.AddWithValue("@SumPrice", SumPrice);
                    com.Parameters.AddWithValue("@Bonus", Bonus);
                    com.Parameters.AddWithValue("@IsPersonnel", IsPersonnel);
                    com.Parameters.AddWithValue("@ID_Games", ID_Games);
                    com.Parameters.AddWithValue("@ID_Swiper", ID_Swiper);
                    com.Parameters.AddWithValue("@CashPrice", Card_Cash_Price);
                    com.Parameters.AddWithValue("@BonusPrice", Card_Bonus_Price);
                    com.Parameters.AddWithValue("@ID_Play_Type", ID_Play_Type);
                    com.Parameters.AddWithValue("@Pay_CashPortion", Pay_CashPortion);
                    com.Parameters.AddWithValue("@Pay_BonusPortion", Pay_BonusPortion);
                    com.Parameters.AddWithValue("@Pay_GiftPortion", Pay_GiftPortion);
                    com.Parameters.AddWithValue("@ID_Gift_Pattern_series_List", ID_ActiveGiftForExtraBonus);
                    com.ExecuteNonQuery();
                    connection.Close();
                    connection.Dispose();
                    objMain.Synchronize_Insert(com);
                    DataTable byCardGuid = Card_GetByCard_GUID(GuID);
                    int Point_Old = 0;
                    try
                    {
                        Point_Old = int.Parse(byCardGuid.Rows[0]["Club_Point"].ToString());
                    }
                    catch
                    {
                    }
                    objClub.Club_Point_Process(GuID, 1, 0, Point_Old, ID_Games);
                }
                return num;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                objMain.ErrorLogTemp(Pay_CashPortion.ToString() + ":" + Pay_BonusPortion.ToString());
                return -1;
            }
        }

        public DataTable Card_Play_Details_GetByCardGUID(string SwiperMacAddress, string Card_GUID, int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("select top(1) * from  Card_Play_Details where ( Card_GUID=@Card_GUID) and (SwiperMacAddress=@SwiperMacAddress) and (ID_GameCenter=@ID_GameCenter) and (Card_Play_Details.Date between  @FromDate and @ToDate ) order by ID desc ", connection);
                    selectCommand.Parameters.AddWithValue("@Card_GUID", Card_GUID);
                    selectCommand.Parameters.AddWithValue("@SwiperMacAddress", SwiperMacAddress);
                    selectCommand.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
                    int num = -10;
                    try
                    {
                        num = int.Parse(MainClass.key_Value_List.Select("KeyName ='TimeOfCheck_Card_Play_Details_ForTicket'")[0]["Value"].ToString());
                    }
                    catch { }
                    selectCommand.Parameters.AddWithValue("@FromDate", DateTime.Now.AddMinutes((double)num));
                    selectCommand.Parameters.AddWithValue("@ToDate", DateTime.Now);
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

        public DataTable Card_Play_Details_GetByCardGUID(string Card_GUID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("SELECT        TOP (5) Card_Play_Details.ID, Card_Play_Details.ID_GameCenter, Card_Play_Details.SwiperMacAddress, Card_Play_Details.ID_Card,dbo.MiladiTOShamsi( Card_Play_Details.Date) as Date,Card_Play_Details.Date as MiladiDate, Card_Play_Details.Price,(isnull( Card_Play_Details.SumPrice,0) +isnull( Card_Play_Details.Bonus,0)) as PriceKol ,                            Card_Play_Details.Bonus, Card_Play_Details.Card_GUID, Card_Play_Details.IsPersonnel, Card_Play_Details.ID_Games, Card_Play_Details.ID_Swiper, Card_Play_Details.IsCancel, Games.Title AS GameTitle  FROM            Card_Play_Details INNER JOIN                           Games ON Card_Play_Details.ID_Games = Games.ID AND Card_Play_Details.ID_GameCenter = Games.ID_GameCenter  WHERE(Card_Play_Details.Card_GUID = @Card_GUID) ORDER BY Card_Play_Details.ID DESC", connection);
                    selectCommand.Parameters.AddWithValue("@Card_GUID", Card_GUID);
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

        public int Card_Type_Update(int ID, string Title, int ID_Card_Status, int MaxAllowedBalance, int MinBalanceForReissue, int MinBalanceForRegistration, int MinCashForRegistration, int NumberOfDaysKeep, int FreeDailyGames, int MinSpendPrice, int MinSpendDays)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Card_Type_Update), connection) { CommandType = CommandType.StoredProcedure };
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.Parameters.AddWithValue("@Title ", Title);
                    sqlCommand.Parameters.AddWithValue("@ID_Card_Status", ID_Card_Status);
                    sqlCommand.Parameters.AddWithValue("@MaxAllowedBalance", MaxAllowedBalance);
                    sqlCommand.Parameters.AddWithValue("@MinBalanceForReissue", MinBalanceForReissue);
                    sqlCommand.Parameters.AddWithValue("@MinBalanceForRegistration", MinBalanceForRegistration);
                    sqlCommand.Parameters.AddWithValue("@MinCashForRegistration", MinCashForRegistration);
                    sqlCommand.Parameters.AddWithValue("@NumberOfDaysKeep", NumberOfDaysKeep);
                    sqlCommand.Parameters.AddWithValue("@FreeDailyGames", FreeDailyGames);
                    sqlCommand.Parameters.AddWithValue("@MinSpendPrice", MinSpendPrice);
                    sqlCommand.Parameters.AddWithValue("@MinSpendDays", MinSpendDays);
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

        public DataTable Card_Type_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Card_Type_GetAll", connection) { CommandType = CommandType.StoredProcedure };
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

        public DataTable Card_Type_Get(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(Card_Type_Get), connection) { CommandType = CommandType.StoredProcedure };
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

        public int Card_Type_Delete(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Card_Type_Delete), connection) { CommandType = CommandType.StoredProcedure };
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

        public int CardProduct_Update(int ID, string Title, int ID_GameCenter, bool IsActive, bool AllowedNewCard, bool AllowedOldCard, string TextColor, string BackColor, int Priority, string AllowedCardTypeIds, int TenderedType, int TenderedAmount, int FreeGame, int DailyFreeGame, int DelayFreeGame, bool AllowChildCards, int MaxChildCards, int Price1, int Price2, int Price3, int Price4, int Price1MaxPeople, int Price2MaxPeople, int Price3MaxPeople, int Price4MaxPeople, int Bonus1, int Bonus2, int Bonus3, int Bonus4, int PageLayout, int ID_CardProduct_Group)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(CardProduct_Update), connection) { CommandType = CommandType.StoredProcedure };
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@IsActive", IsActive);
                    sqlCommand.Parameters.AddWithValue("@AllowedNewCard", AllowedNewCard);
                    sqlCommand.Parameters.AddWithValue("@AllowedOldCard", AllowedOldCard);
                    sqlCommand.Parameters.AddWithValue("@TextColor", TextColor);
                    sqlCommand.Parameters.AddWithValue("@BackColor", BackColor);
                    sqlCommand.Parameters.AddWithValue("@Priority", Priority);
                    sqlCommand.Parameters.AddWithValue("@AllowedCardTypeIds", AllowedCardTypeIds);
                    sqlCommand.Parameters.AddWithValue("@TenderedType", TenderedType);
                    sqlCommand.Parameters.AddWithValue("@TenderedAmount", TenderedAmount);
                    sqlCommand.Parameters.AddWithValue("@FreeGame", FreeGame);
                    sqlCommand.Parameters.AddWithValue("@DailyFreeGame", DailyFreeGame);
                    sqlCommand.Parameters.AddWithValue("@DelayFreeGame", DelayFreeGame);
                    sqlCommand.Parameters.AddWithValue("@AllowChildCards", AllowChildCards);
                    sqlCommand.Parameters.AddWithValue("@MaxChildCards", MaxChildCards);
                    sqlCommand.Parameters.AddWithValue("@Price1", Price1);
                    sqlCommand.Parameters.AddWithValue("@Price2", Price2);
                    sqlCommand.Parameters.AddWithValue("@Price3", Price3);
                    sqlCommand.Parameters.AddWithValue("@Price4", Price4);
                    sqlCommand.Parameters.AddWithValue("@Price1MaxPeople", Price1MaxPeople);
                    sqlCommand.Parameters.AddWithValue("@Price2MaxPeople", Price2MaxPeople);
                    sqlCommand.Parameters.AddWithValue("@Price3MaxPeople", Price3MaxPeople);
                    sqlCommand.Parameters.AddWithValue("@Price4MaxPeople", Price4MaxPeople);
                    sqlCommand.Parameters.AddWithValue("@Bonus1", Bonus1);
                    sqlCommand.Parameters.AddWithValue("@Bonus2", Bonus2);
                    sqlCommand.Parameters.AddWithValue("@Bonus3", Bonus3);
                    sqlCommand.Parameters.AddWithValue("@Bonus4", Bonus4);
                    sqlCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@PageLayout", PageLayout);
                    sqlCommand.Parameters.AddWithValue("@ID_CardProduct_Group", ID_CardProduct_Group);
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

        public DataTable CardProduct_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("CardProduct_GetAll", connection) { CommandType = CommandType.StoredProcedure };
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

        public DataTable CardProduct_Get(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(CardProduct_Get), connection) { CommandType = CommandType.StoredProcedure };
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

        public DataTable CardProduct_GetByGroup(int ID_CardProduct_Group)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(CardProduct_GetByGroup), connection) { CommandType = CommandType.StoredProcedure };
                    selectCommand.Parameters.AddWithValue("@ID_CardProduct_Group", ID_CardProduct_Group);
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

        public int CardProduct_Delete(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(CardProduct_Delete), connection) { CommandType = CommandType.StoredProcedure };
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

        public DataTable Card_CardProductTiming_Get(string Card_GUID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("select * from Card_CardProductTiming where Card_GUID=@Card_GUID order by ID desc", connection);
                    selectCommand.Parameters.AddWithValue("@Card_GUID", Card_GUID);
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

        public Tuple<bool, int, int, int, int, bool, string> Card_CardProductTiming_Status(string Card_GUID, int GameID)
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
            try
            {
                DataTable dataTable1 = Card_CardProductTiming_Get(Card_GUID);
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
                    DateTime Start_AfterDate = Convert.ToDateTime(dataTable1.Rows[0]["Start_AfterDate"].ToString());
                    DataTable dataTable2 = objCashier.Orders_Get(int.Parse(dataTable1.Rows[0]["ID_Order"].ToString()), int.Parse(dataTable1.Rows[0]["ID_GameCenter"].ToString()));
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
                    DataTable dataTable3 = CardProduct_Get(int.Parse(dataTable1.Rows[0]["ID_CardProduct"].ToString()));
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
                        DataTable onle5Today = Card_Play_Details_GetOnle5Today(Card_GUID, Start_AfterDate);
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
                    int dayOfWeek = (int)now1.DayOfWeek;
                    if (str2.Contains(dayOfWeek.ToString()))
                        num14 = 1;
                    for (int index = 0; index < strArray.Length; ++index)
                    {
                        if (int.Parse(strArray[index]) == GameID)
                            num15 = 1;
                    }
                    DataTable onle5 = Card_Play_Details_GetOnle5(Card_GUID, Start_AfterDate);
                    if (num17 > 0)
                        num13 = num17 <= onle5.Rows.Count ? 0 : 1;
                    else if (num17 == 0)
                        num13 = 1;
                    if (num13 == 1)
                    {
                        if (num18 > 0)
                        {
                            int count = Card_Play_Details_GetByID_Play_Type5(Card_GUID, GameID, Start_AfterDate).Rows.Count;
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
                            num8 = !(now1 > dateTime2.AddMinutes((double)num16)) ? 0 : 1;
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
                                        num10 = !(dateTime5.AddMinutes((double)num23) >= now1) ? 0 : 1;
                                    }
                                    else
                                        num10 = 1;
                                }
                                if (boolean1)
                                    num10 = !(Start_AfterDate.AddMinutes((double)num23) >= now1) ? 0 : 1;
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
                                        num24 = !(dateTime5.AddMinutes((double)num23) >= now1) ? 0 : 1;
                                    }
                                    else
                                        num24 = 1;
                                }
                                num11 = !boolean1 ? 0 : (!(Start_AfterDate.AddMinutes((double)num23) >= now1) ? 0 : 1);
                            }
                            if (boolean7)
                                num11 = !(dateTime4 > now1) ? 0 : 1;
                            if (!boolean7 && !boolean6)
                                num11 = 1;
                        }
                    }
                    DateTime now2 = DateTime.Now;
                    string str5 = now2.ToString("HH:mm").Split(':')[0].ToString() + now2.ToString("HH:mm").Split(':')[1].ToString();

                    if (num8 * num15 * num12 * num10 * num11 * num9 * num13 * num14 == 1)
                        flag1 = true;
                }
                else
                    flag1 = false;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
            }
            string str6 = num4.ToString() + "," + num6 + "," + num7;
            return Tuple.Create<bool, int, int, int, int, bool, string>(flag1, num3, num2, num1, num5, flag2, str6);
        }

        public int Card_CardProductTiming_SetChargePrice(string Card_GUID, int IDCard_CardProductTiming, int TimingChargePrice_Real)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand("update [dbo].[Card_CardProductTiming] set [TimingChargePrice_Real]=@TimingChargePrice_Real where [Card_GUID]=@Card_GUID and ID=@ID ", connection);
                    com.Parameters.AddWithValue("@TimingChargePrice_Real", TimingChargePrice_Real);
                    com.Parameters.AddWithValue("@Card_GUID", Card_GUID);
                    com.Parameters.AddWithValue("@ID", IDCard_CardProductTiming);
                    com.ExecuteNonQuery();
                    connection.Close();
                    connection.Dispose();
                    objMain.Synchronize_Insert(com);
                }
                return 1;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return -1;
            }
        }

        public int Card_CardProductTiming_SetFreeGame(string Card_GUID, int IDCard_CardProductTiming, int FreeGame_Real)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand("update [dbo].[Card_CardProductTiming] set [FreeGame_Real]=@FreeGame_Real where [Card_GUID]=@Card_GUID and ID=@ID ", connection);
                    com.Parameters.AddWithValue("@FreeGame_Real", FreeGame_Real);
                    com.Parameters.AddWithValue("@Card_GUID", Card_GUID);
                    com.Parameters.AddWithValue("@ID", IDCard_CardProductTiming);
                    com.ExecuteNonQuery();
                    connection.Close();
                    connection.Dispose();
                    objMain.Synchronize_Insert(com);
                }
                return 1;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return -1;
            }
        }

        public DataTable Card_Play_Details_GetOnle5(string Card_GUID, DateTime Start_AfterDate)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("select * from Card_Play_Details where Card_GUID=@Card_GUID and ID_Play_Type in(5,9,10,11) and IsCancel=0 and Date>=@Start_AfterDate order by Date ASC ", connection);
                    selectCommand.Parameters.AddWithValue("@Card_GUID", Card_GUID);
                    selectCommand.Parameters.AddWithValue("@Start_AfterDate", Start_AfterDate);
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

        public DataTable Card_Play_Details_GetOnle5Today(string Card_GUID, DateTime Start_AfterDate)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("select * from Card_Play_Details where Card_GUID=@Card_GUID and ID_Play_Type in(5,9,10,11) and IsCancel=0 and Date>=@Start_AfterDate and  cast(Date as date)=cast(@DateNow as date) order by Date ASC ", connection);
                    selectCommand.Parameters.AddWithValue("@Card_GUID", Card_GUID);
                    selectCommand.Parameters.AddWithValue("@Start_AfterDate", Start_AfterDate);
                    selectCommand.Parameters.AddWithValue("@DateNow", DateTime.Now);
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

        public DataTable Card_Play_Details_GetByID_Play_Type5(string Card_GUID, int ID_Games, DateTime Start_AfterDate)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("select * from Card_Play_Details where Card_GUID=@Card_GUID and ID_Games=@ID_Games and IsCancel=0 and ID_Play_Type in(5,9,10,11) and Date>=@Start_AfterDate order by Date ASC ", connection);
                    selectCommand.Parameters.AddWithValue("@Card_GUID", Card_GUID);
                    selectCommand.Parameters.AddWithValue("@ID_Games", ID_Games);
                    selectCommand.Parameters.AddWithValue("@Start_AfterDate", Start_AfterDate);
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

        public DataTable Card_Play_Details_Get_Today(string Card_GUID, string ID_Games)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("select * from Card_Play_Details where  Card_GUID=@Card_GUID and ID_Games in (" + ID_Games + ") and IsCancel=0 and cast( Date as date)=cast(@FromDate as date) and  ID_Play_Type=3  order by Date ASC", connection);
                    selectCommand.Parameters.AddWithValue("@Card_GUID", Card_GUID);
                    selectCommand.Parameters.AddWithValue("@FromDate", DateTime.Now);
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

        public int Card_Ticket_History_insert(int ID_GameCenter, int ID_Cashier_Session, int Count, string Card_GUID, int OldCount, int ID_Card_Ticket_History_Type, int ID_Games_Ticket)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand("INSERT INTO [dbo].[Card_Ticket_History] ([ID] ,[ID_GameCenter] ,[ID_Cashier_Session] ,[Date] ,[Count] ,[Card_GUID],[OldCount],[ID_Card_Ticket_History_Type],[ID_Games_Ticket]) VALUES (@ID ,@ID_GameCenter ,@ID_Cashier_Session ,@Date ,@Count ,@Card_GUID,@OldCount,@ID_Card_Ticket_History_Type,@ID_Games_Ticket)", connection);
                    com.Parameters.AddWithValue("@ID", (objMain.Max_Tbl("Card_Ticket_History", "ID") + 1));
                    com.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
                    com.Parameters.AddWithValue("@ID_Cashier_Session", ID_Cashier_Session);
                    com.Parameters.AddWithValue("@Date", DateTime.Now);
                    com.Parameters.AddWithValue("@Count", Count);
                    com.Parameters.AddWithValue("@Card_GUID", Card_GUID);
                    com.Parameters.AddWithValue("@OldCount", OldCount);
                    com.Parameters.AddWithValue("@ID_Card_Ticket_History_Type", ID_Card_Ticket_History_Type);
                    com.Parameters.AddWithValue("@ID_Games_Ticket", ID_Games_Ticket);
                    com.ExecuteNonQuery();
                    objMain.Synchronize_Insert(com);
                }
                return 1;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return -1;
            }
        }

        public int Card_SetEtickets(string Card_GUID, int Etickets)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand("update Card set Etickets=@Etickets ,LAST_UPDATE=@Date where Card_GUID=@Card_GUID ", connection);
                    com.Parameters.AddWithValue("@Etickets", Etickets);
                    com.Parameters.AddWithValue("@Card_GUID", Card_GUID);
                    com.Parameters.AddWithValue("@Date", DateTime.Now);
                    com.ExecuteNonQuery();
                    objMain.Synchronize_Insert(com);
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

        public int Card_Status_insert(string Title, int ReswipeDelay, int ConsecutiveSwipes, bool DisplayBalance, bool Is_Party, bool Is_TimePlay, bool Is_Staff, bool AllowAllCategory, bool DisAllowAllCategory, string DisallowList)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Card_Status_Insert", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@ID", (objMain.Max_Tbl("Card_Status", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@IsDeleted", 0);
                    sqlCommand.Parameters.AddWithValue("@ReswipeDelay", ReswipeDelay);
                    sqlCommand.Parameters.AddWithValue("@ConsecutiveSwipes", ConsecutiveSwipes);
                    sqlCommand.Parameters.AddWithValue("@DisplayBalance", DisplayBalance);
                    sqlCommand.Parameters.AddWithValue("@Is_Party", Is_Party);
                    sqlCommand.Parameters.AddWithValue("@Is_TimePlay", Is_TimePlay);
                    sqlCommand.Parameters.AddWithValue("@Is_Staff", Is_Staff);
                    sqlCommand.Parameters.AddWithValue("@AllowAllCategory", AllowAllCategory);
                    sqlCommand.Parameters.AddWithValue("@DisAllowAllCategory", DisAllowAllCategory);
                    sqlCommand.Parameters.AddWithValue("@DisallowList", DisallowList);
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

        public int Card_Series_insert(int ID_GameCenter, string CardPrefix, string BarcodeFrom, string BarcodeTo, DateTime PurgeDate)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Card_Series_Insert", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@ID", (objMain.Max_Tbl("Card_Series", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@CardPrefix", CardPrefix);
                    sqlCommand.Parameters.AddWithValue("@BarcodeFrom", BarcodeFrom);
                    sqlCommand.Parameters.AddWithValue("@BarcodeTo", BarcodeTo);
                    sqlCommand.Parameters.AddWithValue("@PurgeDate", PurgeDate);
                    sqlCommand.Parameters.AddWithValue("@Date", DateTime.Now);
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

        public DataTable Card_Series_Get_byPrefix(string CardPrefix)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Card_Series_Get_byCardPrefix", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    selectCommand.Parameters.AddWithValue("@CardPrefix", CardPrefix);
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

        public int Card_insert(string MacCode, int ID_Card_Series, string CodeSeries)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Card_Insert", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@ID", (objMain.Max_Tbl("Card", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@MacCode", MacCode);
                    sqlCommand.Parameters.AddWithValue("@ID_Card_Series", ID_Card_Series);
                    sqlCommand.Parameters.AddWithValue("@CodeSeries", CodeSeries);
                    sqlCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@IsActive", 1);
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

        public int Card_Return(string Card_GUID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand(" update Card set Is_Closed=1  where Card_GUID=@Card_GUID ", connection);
                    com.Parameters.AddWithValue("@Card_GUID", Card_GUID);
                    com.CommandType = CommandType.Text;
                    com.ExecuteNonQuery();
                    objMain.Synchronize_Insert(com);
                }
                return 1;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return -1;
            }
        }

        public DataTable Card_UpdatePrice(string MacCode, string GuID, int Price)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand("update   Card set CashPrice=@CashPrice  where Is_Closed=0 and MacCode=@MacCode and Card_GUID=@Card_GUID", connection);
                    com.Parameters.AddWithValue("@CashPrice", Price);
                    com.Parameters.AddWithValue("@MacCode", MacCode);
                    com.Parameters.AddWithValue("@Card_GUID", GuID);
                    com.ExecuteNonQuery();
                    objMain.Synchronize_Insert(com);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return dataTable;
            }
        }

        public DataTable Card_UpdateBonus(string MacCode, string Card_GUID, int BonusPrice)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand("update   Card set BonusPrice=@BonusPrice  where Is_Closed=0 and MacCode=@MacCode and Card_GUID=@Card_GUID", connection);
                    com.Parameters.AddWithValue("@BonusPrice", BonusPrice);
                    com.Parameters.AddWithValue("@MacCode", MacCode);
                    com.Parameters.AddWithValue("@Card_GUID", Card_GUID);
                    com.ExecuteNonQuery();
                    objMain.Synchronize_Insert(com);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return dataTable;
            }
        }

        public DataTable Card_UpdatePriceAndBonus(string MacCode, string GuID, int Price, int BonusPrice)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand("update   Card set CashPrice=@CashPrice,BonusPrice=@BonusPrice  where  MacCode=@MacCode and Card_GUID=@Card_GUID", connection);
                    com.Parameters.AddWithValue("@CashPrice", Price);
                    com.Parameters.AddWithValue("@BonusPrice", BonusPrice);
                    com.Parameters.AddWithValue("@MacCode", MacCode);
                    com.Parameters.AddWithValue("@Card_GUID", GuID);
                    com.ExecuteNonQuery();
                    objMain.Synchronize_Insert(com);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return dataTable;
            }
        }

        public int Card_PlayDetails_Insert_Temp(string GuID, int ID_GameCenter, string SwiperMacAddress, int GamePrice, int SumPrice, int Bonus, int IsPersonnel, int ID_Games, int ID_Swiper, int Pay_CashPortion, int Pay_BonusPortion)
        {
            try
            {
                int num = objMain.Max_Tbl("Card_Play_Details", "ID") + 1;
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand("INSERT INTO [dbo].[Card_Play_Details] ([ID] ,[ID_GameCenter] ,[SwiperMacAddress] ,[Date] ,[Price] ,[SumPrice] ,[Bonus] ,[Card_GUID] ,[IsPersonnel] ,[ID_Games] ,[ID_Swiper] ,[IsCancel] ,[ID_Play_Type] ,[Pay_CashPortion] ,[Pay_BonusPortion]) VALUES (@ID ,@ID_GameCenter ,@SwiperMacAddress ,@Date ,@Price ,@SumPrice ,@Bonus ,@Card_GUID ,@IsPersonnel ,@ID_Games ,@ID_Swiper ,@IsCancel ,@ID_Play_Type ,@Pay_CashPortion ,@Pay_BonusPortion)", connection);
                    com.Parameters.AddWithValue("@ID", num);
                    com.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
                    com.Parameters.AddWithValue("@SwiperMacAddress", SwiperMacAddress);
                    com.Parameters.AddWithValue("@Date", DateTime.Now);
                    com.Parameters.AddWithValue("@Price", GamePrice);
                    com.Parameters.AddWithValue("@SumPrice", SumPrice);
                    com.Parameters.AddWithValue("@Bonus", Bonus);
                    com.Parameters.AddWithValue("@Card_GUID", GuID);
                    com.Parameters.AddWithValue("@IsPersonnel", IsPersonnel);
                    com.Parameters.AddWithValue("@ID_Games", ID_Games);
                    com.Parameters.AddWithValue("@ID_Swiper", ID_Swiper);
                    com.Parameters.AddWithValue("@IsCancel", 0);
                    com.Parameters.AddWithValue("@ID_Play_Type", 6);
                    com.Parameters.AddWithValue("@Pay_CashPortion", Pay_CashPortion);
                    com.Parameters.AddWithValue("@Pay_BonusPortion", Pay_BonusPortion);
                    com.ExecuteNonQuery();
                    objMain.Synchronize_Insert(com);
                }
                return num;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return -1;
            }
        }

        public int Card_Play_Details_insert(int ID_GameCenter, string SwiperMacAddress, string GuID_Card, int Price, int SumPrice, int Bonus, int IsPersonnel, int ID_Games, int ID_Swiper)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand("Card_Play_Details_Insert2", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    com.Parameters.AddWithValue("@ID", (objMain.Max_Tbl("Card_Play_Details", "ID") + 1));
                    com.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
                    com.Parameters.AddWithValue("@SwiperMacAddress", SwiperMacAddress);
                    com.Parameters.AddWithValue("@Card_GUID", GuID_Card);
                    com.Parameters.AddWithValue("@Date", DateTime.Now);
                    com.Parameters.AddWithValue("@Price", Price);
                    com.Parameters.AddWithValue("@SumPrice", SumPrice);
                    com.Parameters.AddWithValue("@Bonus", Bonus);
                    com.Parameters.AddWithValue("@IsPersonnel", IsPersonnel);
                    com.Parameters.AddWithValue("@ID_Games", ID_Games);
                    com.Parameters.AddWithValue("@ID_Swiper", ID_Swiper);
                    com.ExecuteNonQuery();
                    objMain.Synchronize_Insert(com);
                }
                return 1;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return -1;
            }
        }

        public int Card_Type_insert(string Title, int ID_Card_Status, int MaxAllowedBalance, int MinBalanceForReissue, int MinBalanceForRegistration, int MinCashForRegistration, int NumberOfDaysKeep, int FreeDailyGames, int MinSpendPrice, int MinSpendDays)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Card_Type_Insert", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@ID", (objMain.Max_Tbl("Card_Type", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@Title ", Title);
                    sqlCommand.Parameters.AddWithValue("@ID_Card_Status", ID_Card_Status);
                    sqlCommand.Parameters.AddWithValue("@MaxAllowedBalance", MaxAllowedBalance);
                    sqlCommand.Parameters.AddWithValue("@MinBalanceForReissue", MinBalanceForReissue);
                    sqlCommand.Parameters.AddWithValue("@MinBalanceForRegistration", MinBalanceForRegistration);
                    sqlCommand.Parameters.AddWithValue("@MinCashForRegistration", MinCashForRegistration);
                    sqlCommand.Parameters.AddWithValue("@NumberOfDaysKeep", NumberOfDaysKeep);
                    sqlCommand.Parameters.AddWithValue("@FreeDailyGames", FreeDailyGames);
                    sqlCommand.Parameters.AddWithValue("@MinSpendPrice", MinSpendPrice);
                    sqlCommand.Parameters.AddWithValue("@MinSpendDays", MinSpendDays);
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

        public int CardProduct_insert(string Title, int ID_GameCenter, bool IsActive, bool AllowedNewCard, bool AllowedOldCard, string TextColor, string BackColor, int Priority, string AllowedCardTypeIds, int TenderedType, int TenderedAmount, int FreeGame, int DailyFreeGame, int DelayFreeGame, bool AllowChildCards, int MaxChildCards, int Price1, int Price2, int Price3, int Price4, int Price1MaxPeople, int Price2MaxPeople, int Price3MaxPeople, int Price4MaxPeople, int Bonus1, int Bonus2, int Bonus3, int Bonus4, int PageLayout, int ID_CardProduct_Group)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("CardProduct_Insert", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@ID", (objMain.Max_Tbl("CardProduct", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@IsActive", IsActive);
                    sqlCommand.Parameters.AddWithValue("@AllowedNewCard", AllowedNewCard);
                    sqlCommand.Parameters.AddWithValue("@AllowedOldCard", AllowedOldCard);
                    sqlCommand.Parameters.AddWithValue("@TextColor", TextColor);
                    sqlCommand.Parameters.AddWithValue("@BackColor", BackColor);
                    sqlCommand.Parameters.AddWithValue("@Priority", Priority);
                    sqlCommand.Parameters.AddWithValue("@AllowedCardTypeIds", AllowedCardTypeIds);
                    sqlCommand.Parameters.AddWithValue("@TenderedType", TenderedType);
                    sqlCommand.Parameters.AddWithValue("@TenderedAmount", TenderedAmount);
                    sqlCommand.Parameters.AddWithValue("@FreeGame", FreeGame);
                    sqlCommand.Parameters.AddWithValue("@DailyFreeGame", DailyFreeGame);
                    sqlCommand.Parameters.AddWithValue("@DelayFreeGame", DelayFreeGame);
                    sqlCommand.Parameters.AddWithValue("@AllowChildCards", AllowChildCards);
                    sqlCommand.Parameters.AddWithValue("@MaxChildCards", MaxChildCards);
                    sqlCommand.Parameters.AddWithValue("@Price1", Price1);
                    sqlCommand.Parameters.AddWithValue("@Price2", Price2);
                    sqlCommand.Parameters.AddWithValue("@Price3", Price3);
                    sqlCommand.Parameters.AddWithValue("@Price4", Price4);
                    sqlCommand.Parameters.AddWithValue("@Price1MaxPeople", Price1MaxPeople);
                    sqlCommand.Parameters.AddWithValue("@Price2MaxPeople", Price2MaxPeople);
                    sqlCommand.Parameters.AddWithValue("@Price3MaxPeople", Price3MaxPeople);
                    sqlCommand.Parameters.AddWithValue("@Price4MaxPeople", Price4MaxPeople);
                    sqlCommand.Parameters.AddWithValue("@Bonus1", Bonus1);
                    sqlCommand.Parameters.AddWithValue("@Bonus2", Bonus2);
                    sqlCommand.Parameters.AddWithValue("@Bonus3", Bonus3);
                    sqlCommand.Parameters.AddWithValue("@Bonus4", Bonus4);
                    sqlCommand.Parameters.AddWithValue("@IsDeleted", 0);
                    sqlCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@PageLayout", PageLayout);
                    sqlCommand.Parameters.AddWithValue("@ID_CardProduct_Group", ID_CardProduct_Group);
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

        public DataTable CardProduct_Group_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    new SqlDataAdapter(new SqlCommand("select * from CardProduct_Group ", connection)).Fill(dataTable);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return dataTable;
            }
        }

        public DataTable Card_Play_Details_Get(string Card_GUID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("select * from Card_Play_Details where Card_GUID=@Card_GUID order by Date ASC ", connection);
                    selectCommand.Parameters.AddWithValue("@Card_GUID", Card_GUID);
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

        public DataTable Card_Play_Details_Get(string Card_GUID, int ID_Games)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("select * from Card_Play_Details where Card_GUID=@Card_GUID and ID_Games=@ID_Games and IsCancel=0 order by Date ASC ", connection);
                    selectCommand.Parameters.AddWithValue("@Card_GUID", Card_GUID);
                    selectCommand.Parameters.AddWithValue("@ID_Games", ID_Games);
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

        public DataTable Card_Play_Details_Get(string Card_GUID, string ID_Games)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("select * from Card_Play_Details where Card_GUID=@Card_GUID and ID_Games in (" + ID_Games + ") and IsCancel=0 order by Date ASC ", connection);
                    selectCommand.Parameters.AddWithValue("@Card_GUID", Card_GUID);
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

        public DataTable Card_Play_Details_GetByCLassID(string Card_GUID, int ID_Games_Class)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("SELECT        Card_Play_Details.ID, Card_Play_Details.ID_GameCenter, Card_Play_Details.SwiperMacAddress, Card_Play_Details.ID_Card, Card_Play_Details.Date, Card_Play_Details.Price, Card_Play_Details.SumPrice,                            Card_Play_Details.Bonus, Card_Play_Details.Card_GUID, Card_Play_Details.IsPersonnel, Card_Play_Details.ID_Games, Card_Play_Details.ID_Swiper, Card_Play_Details.IsCancel, Card_Play_Details.IsCancel_Des,                           Card_Play_Details.IsCancel_Date, Card_Play_Details.IsCancel_User, Card_Play_Details.IsCancel_CashierSession, Games.ID_Games_Class  FROM            Card_Play_Details INNER JOIN                           Games ON Card_Play_Details.ID_Games = Games.ID  WHERE(Card_Play_Details.Card_GUID = @Card_GUID) AND (Games.ID_Games_Class = @ID_Games_Class) and(Card_Play_Details.IsCancel=0)  ", connection);
                    selectCommand.Parameters.AddWithValue("@Card_GUID", Card_GUID);
                    selectCommand.Parameters.AddWithValue("@ID_Games_Class", ID_Games_Class);
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

        public DataTable Card_Play_Details_GetByCLassIDByID_Play_Type(string Card_GUID, int ID_Games_Class, int ID_Play_Type)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("SELECT        Card_Play_Details.ID, Card_Play_Details.ID_GameCenter, Card_Play_Details.SwiperMacAddress, Card_Play_Details.ID_Card, Card_Play_Details.Date, Card_Play_Details.Price, Card_Play_Details.SumPrice,                            Card_Play_Details.Bonus, Card_Play_Details.Card_GUID, Card_Play_Details.IsPersonnel, Card_Play_Details.ID_Games, Card_Play_Details.ID_Swiper, Card_Play_Details.IsCancel, Card_Play_Details.IsCancel_Des,                           Card_Play_Details.IsCancel_Date, Card_Play_Details.IsCancel_User, Card_Play_Details.IsCancel_CashierSession, Games.ID_Games_Class  FROM            Card_Play_Details INNER JOIN                           Games ON Card_Play_Details.ID_Games = Games.ID  WHERE(Card_Play_Details.Card_GUID = @Card_GUID) AND (Games.ID_Games_Class = @ID_Games_Class) and(Card_Play_Details.IsCancel=0) and (Card_Play_Details.ID_Play_Type=@ID_Play_Type)  ", connection);
                    selectCommand.Parameters.AddWithValue("@Card_GUID", Card_GUID);
                    selectCommand.Parameters.AddWithValue("@ID_Games_Class", ID_Games_Class);
                    selectCommand.Parameters.AddWithValue("@ID_Play_Type", ID_Play_Type);
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
