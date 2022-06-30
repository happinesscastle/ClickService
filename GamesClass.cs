using System.Data.SqlClient;
using System.Data;
using System;

namespace ClickServerService
{
    internal class GamesClass
    {
        private readonly SwiperClass objSwiper = new SwiperClass();
        private readonly MainClass objMain = new MainClass();

        public int Games_Groups_Update(int ID, string Title)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Games_Groups_Update), connection) { CommandType = CommandType.StoredProcedure };
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
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

        public DataTable Games_Groups_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Games_Groups_GetAll", connection) { CommandType = CommandType.StoredProcedure };
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

        public DataTable Games_Groups_Get(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(Games_Groups_Get), connection) { CommandType = CommandType.StoredProcedure };
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

        public int Games_Groups_Delete(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Games_Groups_Delete), connection) { CommandType = CommandType.StoredProcedure };
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

        public int Games_Type_Update(int ID, string Title)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Games_Type_Update), connection) { CommandType = CommandType.StoredProcedure };
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
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

        public DataTable Games_Type_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Games_Type_GetAll", connection) { CommandType = CommandType.StoredProcedure };
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

        public DataTable Games_Type_Get(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(Games_Type_Get), connection) { CommandType = CommandType.StoredProcedure };
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

        public int Games_Type_Delete(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Games_Type_Delete), connection) { CommandType = CommandType.StoredProcedure };
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

        public int Games_Class_Update(int ID, string Title, string Des, int ID_Games_Group, int ID_Games_Type, int IsStock, int IsFreeGames, int IsFreeDayliGames, int IsBonusAllowed)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Games_Class_Update), connection) { CommandType = CommandType.StoredProcedure };
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@Des", Des);
                    sqlCommand.Parameters.AddWithValue("@ID_Games_Group", ID_Games_Group);
                    sqlCommand.Parameters.AddWithValue("@ID_Games_Type", ID_Games_Type);
                    sqlCommand.Parameters.AddWithValue("@IsStock", IsStock);
                    sqlCommand.Parameters.AddWithValue("@IsFreeGames", IsFreeGames);
                    sqlCommand.Parameters.AddWithValue("@IsFreeDayliGames", IsFreeDayliGames);
                    sqlCommand.Parameters.AddWithValue("@IsBonusAllowed", IsBonusAllowed);
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

        public DataTable Games_Class_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Games_Class_GetAll", connection) { CommandType = CommandType.StoredProcedure };
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

        public DataTable Games_Class_Get(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(Games_Class_Get), connection) { CommandType = CommandType.StoredProcedure };
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

        public int Games_Class_Delete(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Games_Class_Delete), connection) { CommandType = CommandType.StoredProcedure };
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

        public int ChargeGroup_Update(int ID, string Title)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(ChargeGroup_Update), connection) { CommandType = CommandType.StoredProcedure };
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
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

        public DataTable ChargeGroup_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("ChargeGroup_GetAll", connection) { CommandType = CommandType.StoredProcedure };
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

        public DataTable ChargeGroup_Get(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(ChargeGroup_Get), connection) { CommandType = CommandType.StoredProcedure };
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

        public int ChargeGroup_Delete(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(ChargeGroup_Delete), connection) { CommandType = CommandType.StoredProcedure };
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

        public DataTable Charge_Rate_Get(int ID_ChargeGroup, int ID_Days, int ID_Card_Status)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(Charge_Rate_Get), connection) { CommandType = CommandType.StoredProcedure };
                    selectCommand.Parameters.AddWithValue("@ID_ChargeGroup", ID_ChargeGroup);
                    selectCommand.Parameters.AddWithValue("@ID_Days", ID_Days);
                    selectCommand.Parameters.AddWithValue("@ID_Card_Status", ID_Card_Status);
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

        public DataTable Charge_Rate_GetAll(int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    DateTime now = DateTime.Now;
                    DateTime dateTime = now.AddHours(-1.0);
                    string str1;
                    if (dateTime.ToString("HH:mm").Split(':')[0].ToString().Length != 1)
                        str1 = dateTime.ToString("HH:mm").Split(':')[0].ToString();
                    else
                        str1 = "0" + dateTime.ToString("HH:mm").Split(':')[0].ToString();
                    string str2 = str1;
                    string str3;
                    if (now.ToString("HH:mm").Split(':')[0].ToString().Length != 1)
                        str3 = now.ToString("HH:mm").Split(':')[0].ToString();
                    else
                        str3 = "0" + now.ToString("HH:mm").Split(':')[0].ToString();
                    string str4 = str3;
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("update Swiper set Config_State=-2 where ID_GameCenter = @ID_GameCenter and ID in ( SELECT Swiper.ID FROM Charge_Rate INNER JOIN Games ON Charge_Rate.ID_ChargeGroup = Games.ID_ChargeGroup INNER JOIN Swiper ON Games.ID = Swiper.ID_Games WHERE(Charge_Rate.ID_Days = @ID_Days) and (Games.ID_GameCenter = @ID_GameCenter) and " + (" [" + str2 + ":00] <> [" + str4 + ":00] ") + " )", connection);
                    sqlCommand.Parameters.AddWithValue("@ID_Days", objSwiper.RetDayOfWeek());
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
                    sqlCommand.ExecuteNonQuery();
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return dataTable;
            }
        }

        public int Days_Special_Update(int ID, string Title, DateTime DaysDate, int ID_SpecialDays_Type)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Days_Special_Update), connection) { CommandType = CommandType.StoredProcedure };
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@DaysDate", DaysDate);
                    sqlCommand.Parameters.AddWithValue("@ID_SpecialDays_Type", ID_SpecialDays_Type);
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

        public DataTable Days_Special_Get(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(Days_Special_Get), connection) { CommandType = CommandType.StoredProcedure };
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

        public int Days_Special_Delete(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Days_Special_Delete), connection) { CommandType = CommandType.StoredProcedure };
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

        public int Games_Update(int ID, int ID_GameCenter, string Code, string Title, string Des, int ID_Game_Type, int ID_ChargeGroup, int ID_Games_Groups, int ID_Games_Class, string CountOfTicket, int IsRetired, int IsETicket, int IsPaperTicket, int IsStock, int IsFreeGames, int IsFreeDailyGames, int IsBonus)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Games_Update), connection) { CommandType = CommandType.StoredProcedure };
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@Code", Code);
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@Des", Des);
                    sqlCommand.Parameters.AddWithValue("@ID_Game_Type", ID_Game_Type);
                    sqlCommand.Parameters.AddWithValue("@ID_ChargeGroup", ID_ChargeGroup);
                    sqlCommand.Parameters.AddWithValue("@ID_Games_Groups", ID_Games_Groups);
                    sqlCommand.Parameters.AddWithValue("@ID_Games_Class", ID_Games_Class);
                    sqlCommand.Parameters.AddWithValue("@CountOfTicket", CountOfTicket);
                    sqlCommand.Parameters.AddWithValue("@IsRetired", IsRetired);
                    sqlCommand.Parameters.AddWithValue("@IsETicket", IsETicket);
                    sqlCommand.Parameters.AddWithValue("@IsPaperTicket", IsPaperTicket);
                    sqlCommand.Parameters.AddWithValue("@IsStock", IsStock);
                    sqlCommand.Parameters.AddWithValue("@IsFreeGames", IsFreeGames);
                    sqlCommand.Parameters.AddWithValue("@IsFreeDailyGames", IsFreeDailyGames);
                    sqlCommand.Parameters.AddWithValue("@IsBonus", IsBonus);
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

        public DataTable Games_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Games_GetAll", connection) { CommandType = CommandType.StoredProcedure };
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

        public DataTable Games_Get(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(Games_Get), connection) { CommandType = CommandType.StoredProcedure };
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

        public int Games_Delete(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Games_Delete), connection) { CommandType = CommandType.StoredProcedure };
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

        public int Bonuses_Update(int ID, string Title, bool IsAccumulativeBalance, bool IsRunningAccumulativeBalance, bool IsAmountTendered, int FromPrice, int ToPrice, int BonusType, int Bonus, int FreeGame, int FreeGameDaily, int ProductID, int WhenID, string CardStatusIDs, bool IsReportToCashier, bool IsPromptToRegister, string Message, int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Bonuses_Update), connection) { CommandType = CommandType.StoredProcedure };
                    sqlCommand.Parameters.AddWithValue("@ID", ID);
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@IsAccumulativeBalance", IsAccumulativeBalance);
                    sqlCommand.Parameters.AddWithValue("@IsRunningAccumulativeBalance", IsRunningAccumulativeBalance);
                    sqlCommand.Parameters.AddWithValue("@IsAmountTendered", IsAmountTendered);
                    sqlCommand.Parameters.AddWithValue("@FromPrice", FromPrice);
                    sqlCommand.Parameters.AddWithValue("@ToPrice", ToPrice);
                    sqlCommand.Parameters.AddWithValue("@BonusType", BonusType);
                    sqlCommand.Parameters.AddWithValue("@Bonus", Bonus);
                    sqlCommand.Parameters.AddWithValue("@FreeGame", FreeGame);
                    sqlCommand.Parameters.AddWithValue("@FreeGameDaily", FreeGameDaily);
                    sqlCommand.Parameters.AddWithValue("@ProductID", ProductID);
                    sqlCommand.Parameters.AddWithValue("@WhenID", WhenID);
                    sqlCommand.Parameters.AddWithValue("@CardStatusIDs", CardStatusIDs);
                    sqlCommand.Parameters.AddWithValue("@IsReportToCashier", IsReportToCashier);
                    sqlCommand.Parameters.AddWithValue("@IsPromptToRegister", IsPromptToRegister);
                    sqlCommand.Parameters.AddWithValue("@Message", Message);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
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

        public DataTable Bonuses_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Bonuses_GetAll", connection) { CommandType = CommandType.StoredProcedure };
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

        public DataTable Bonuses_Get(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(Bonuses_Get), connection) { CommandType = CommandType.StoredProcedure };
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

        public int Bonuses_Delete(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(Bonuses_Delete), connection) { CommandType = CommandType.StoredProcedure };
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

        public int Games_Groups_insert(string Title)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Games_Groups_Insert", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (objMain.Max_Tbl("Games_Groups", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@IsDeleted", 0);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
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

        public int Games_Type_insert(string Title)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Games_Type_Insert", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (objMain.Max_Tbl("Games_Type", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@IsDeleted", 0);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
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

        public int Games_Class_insert(string Title, string Des, int ID_Games_Group, int ID_Games_Type, int IsStock, int IsFreeGames, int IsFreeDayliGames, int IsBonusAllowed)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Games_Class_Insert", connection);
                    sqlCommand.Parameters.AddWithValue("@ID", (objMain.Max_Tbl("Games_Class", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@Des", Des);
                    sqlCommand.Parameters.AddWithValue("@ID_Games_Group", ID_Games_Group);
                    sqlCommand.Parameters.AddWithValue("@ID_Games_Type", ID_Games_Type);
                    sqlCommand.Parameters.AddWithValue("@IsStock", IsStock);
                    sqlCommand.Parameters.AddWithValue("@IsFreeGames", IsFreeGames);
                    sqlCommand.Parameters.AddWithValue("@IsFreeDayliGames", IsFreeDayliGames);
                    sqlCommand.Parameters.AddWithValue("@IsBonusAllowed", IsBonusAllowed);
                    sqlCommand.Parameters.AddWithValue("@IsDeleted", 0);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
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

        public DataTable Games_Class_GetByGroup(int ID_Games_Group)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Games_Groups_GetByGroup", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    selectCommand.Parameters.AddWithValue("@ID_Games_Group", ID_Games_Group);
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

        public int ChargeGroup_insert(string Title)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("ChargeGroup_Insert", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@ID", (objMain.Max_Tbl("ChargeGroup", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
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

        public int Days_Special_insert(string Title, DateTime DaysDate, int ID_SpecialDays_Type)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Days_Special_Insert", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@ID", (objMain.Max_Tbl("Days_Special", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@DaysDate", DaysDate);
                    sqlCommand.Parameters.AddWithValue("@ID_SpecialDays_Type", ID_SpecialDays_Type);
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

        public DataTable Days_Special_GetByType(int ID_SpecialDays_Type)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Days_Special_GetByID_SpecialDays_Type", connection);
                    selectCommand.Parameters.AddWithValue("@ID_SpecialDays_Type", ID_SpecialDays_Type);
                    selectCommand.CommandType = CommandType.StoredProcedure;
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

        public int Games_insert(int ID_GameCenter, string Code, string Title, string Des, int ID_Game_Type, int ID_ChargeGroup, int ID_Games_Groups, int ID_Games_Class, string CountOfTicket, int IsRetired, int IsETicket, int IsPaperTicket, int IsStock, int IsFreeGames, int IsFreeDailyGames, int IsBonus)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Games_Insert", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@ID", (objMain.Max_Tbl("Games", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@Code", Code);
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@Des", Des);
                    sqlCommand.Parameters.AddWithValue("@ID_Game_Type", ID_Game_Type);
                    sqlCommand.Parameters.AddWithValue("@ID_ChargeGroup", ID_ChargeGroup);
                    sqlCommand.Parameters.AddWithValue("@ID_Games_Groups", ID_Games_Groups);
                    sqlCommand.Parameters.AddWithValue("@ID_Games_Class", ID_Games_Class);
                    sqlCommand.Parameters.AddWithValue("@CountOfTicket", CountOfTicket);
                    sqlCommand.Parameters.AddWithValue("@IsRetired", IsRetired);
                    sqlCommand.Parameters.AddWithValue("@IsETicket", IsETicket);
                    sqlCommand.Parameters.AddWithValue("@IsPaperTicket", IsPaperTicket);
                    sqlCommand.Parameters.AddWithValue("@IsStock", IsStock);
                    sqlCommand.Parameters.AddWithValue("@IsFreeGames", IsFreeGames);
                    sqlCommand.Parameters.AddWithValue("@IsFreeDailyGames", IsFreeDailyGames);
                    sqlCommand.Parameters.AddWithValue("@IsBonus", IsBonus);
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

        public DataTable Games_GetByChargeGroup(int ID_ChargeGroup)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Games_Get_byChargeGroup", connection);
                    selectCommand.Parameters.AddWithValue("@ID_ChargeGroup", ID_ChargeGroup);
                    selectCommand.CommandType = CommandType.StoredProcedure;
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

        public DataTable Games_GetByCatagoryID()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Games_Get_byChargeGroup", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
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

        public DataTable Games_GetBy_ID_GameCenter(int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("SELECT Games.ID, Games.ID_GameCenter, Games.Code, Games.Title, Games.Des, Games.ID_Game_Type, Games.ID_ChargeGroup, Games.ID_Games_Groups, Games.ID_Games_Class, Games.CountOfTicket, Games.IsRetired,                            Games.IsETicket, Games.IsPaperTicket, Games.IsStock, Games.IsFreeGames, Games.IsFreeDailyGames, Games.IsBonus, Games.IsDeleted, Swiper.MacAddress  FROM Games INNER JOIN Swiper ON Games.ID_GameCenter = Swiper.ID_GameCenter AND Games.ID = Swiper.ID_Games where Games.ID_GameCenter=@ID_GameCenter and Games.IsDeleted=0 ", connection);
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

        public int Bonuses_insert(string Title, bool IsAccumulativeBalance, bool IsRunningAccumulativeBalance, bool IsAmountTendered, int FromPrice, int ToPrice, int BonusType, int Bonus, int FreeGame, int FreeGameDaily, int ProductID, int WhenID, string CardStatusIDs, bool IsReportToCashier, bool IsPromptToRegister, string Message, int ID_GameCenter)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Bonuses_Insert", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@ID", (objMain.Max_Tbl("Bonuses", "ID") + 1));
                    sqlCommand.Parameters.AddWithValue("@Title", Title);
                    sqlCommand.Parameters.AddWithValue("@IsAccumulativeBalance", IsAccumulativeBalance);
                    sqlCommand.Parameters.AddWithValue("@IsRunningAccumulativeBalance", IsRunningAccumulativeBalance);
                    sqlCommand.Parameters.AddWithValue("@IsAmountTendered", IsAmountTendered);
                    sqlCommand.Parameters.AddWithValue("@FromPrice", FromPrice);
                    sqlCommand.Parameters.AddWithValue("@ToPrice", ToPrice);
                    sqlCommand.Parameters.AddWithValue("@BonusType", BonusType);
                    sqlCommand.Parameters.AddWithValue("@Bonus", Bonus);
                    sqlCommand.Parameters.AddWithValue("@FreeGame", FreeGame);
                    sqlCommand.Parameters.AddWithValue("@FreeGameDaily", FreeGameDaily);
                    sqlCommand.Parameters.AddWithValue("@ProductID", ProductID);
                    sqlCommand.Parameters.AddWithValue("@WhenID", WhenID);
                    sqlCommand.Parameters.AddWithValue("@CardStatusIDs", CardStatusIDs);
                    sqlCommand.Parameters.AddWithValue("@IsReportToCashier", IsReportToCashier);
                    sqlCommand.Parameters.AddWithValue("@IsPromptToRegister", IsPromptToRegister);
                    sqlCommand.Parameters.AddWithValue("@Message", Message);
                    sqlCommand.Parameters.AddWithValue("@IsDeleted", 0);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
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
