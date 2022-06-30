using System.Data.SqlClient;
using System.Data;
using System;

namespace ClickServerService.ClassCode
{
    internal class Club
    {
        private readonly MainClass objMain = new MainClass();
        private readonly UsersClass objUser = new UsersClass();

        public bool Club_CheckIn_Process(string Card_GUID)
        {
            bool flag = false;
            DataTable dataTable = new DataTable();
            try
            {
                Guid.Parse(Card_GUID);
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(Club_CheckIn_Process), connection) { CommandType = CommandType.StoredProcedure };
                    selectCommand.Parameters.AddWithValue("@Card_GUID", Card_GUID);
                    new SqlDataAdapter(selectCommand).Fill(dataTable);
                }
                if (dataTable.Rows.Count > 0)
                {
                    string str = dataTable.Rows[0][0].ToString();
                    if (str.Split(',')[0].ToString() == "1")
                        Club_CheckIn_UpdateEnd(str.Split(',')[2].ToString());
                    if (str.Split(',')[1].ToString() == "1")
                        Club_CheckIn_Insert(Card_GUID, 1, -1);
                    if (str.Split(',')[3].ToString() == "1")
                        flag = true;
                }
                return flag;
            }
            catch
            {
                return false;
            }
        }

        public int Club_CheckIn_UpdateEnd(string ID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand("Update [Club_CheckIn] set [Date_End]=@Date where ID=@ID", connection);
                    com.Parameters.AddWithValue("@ID", ID);
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

        public int Club_CheckIn_Insert(string Card_GUID, int ID_Club_Type, int ID_User)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand("Insert into [dbo].[Club_CheckIn] (ID,Card_GUID,Date_Start,IsEnd,Points,ID_Club_Type,ID_User) values(@ID,@Card_GUID,@Date_Start,@IsEnd,@Points,@ID_Club_Type,@ID_User) ", connection);
                    com.Parameters.AddWithValue("@ID", Guid.NewGuid());
                    com.Parameters.AddWithValue("@Card_GUID", Card_GUID);
                    com.Parameters.AddWithValue("@Date_Start", DateTime.Now);
                    com.Parameters.AddWithValue("@IsEnd", 0);
                    com.Parameters.AddWithValue("@Points", 0);
                    com.Parameters.AddWithValue("@ID_Club_Type", ID_Club_Type);
                    com.Parameters.AddWithValue("@ID_User", ID_User);
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

        public int Club_Point_Process_UpdateCardPoint(string Card_GUID, int Point, int ID_Club_Member_Type)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(objMain.DBPath());
                sqlConnection.Open();
                SqlCommand com = new SqlCommand("Card_Update_Point", sqlConnection) { CommandType = CommandType.StoredProcedure };
                com.Parameters.Add("@Card_GUID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(Card_GUID);
                com.Parameters.Add("@Point", SqlDbType.Int).Value = Point;
                com.Parameters.Add("@ID_Club_Member_Type", SqlDbType.Int).Value = ID_Club_Member_Type;
                com.ExecuteNonQuery();
                sqlConnection.Close();
                objMain.Synchronize_Insert(com);
                return 1;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return -1;
            }
        }

        public void Club_Point_Process(string Card_GUID, int ID_Club_Point_Type, int Price, int Point_Old, int ID_Game)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(Club_Point_Process), connection) { CommandType = CommandType.StoredProcedure };
                    selectCommand.Parameters.AddWithValue("@Card_GUID", Card_GUID);
                    selectCommand.Parameters.AddWithValue("@ID_Club_Point_Type", ID_Club_Point_Type);
                    selectCommand.Parameters.AddWithValue("@Price", Price);
                    selectCommand.Parameters.AddWithValue("@ID_Game", ID_Game);
                    new SqlDataAdapter(selectCommand).Fill(dataTable);
                }
                if (dataTable.Rows.Count <= 0)
                    return;
                int Point = int.Parse(dataTable.Rows[0]["Point"].ToString());
                if ((uint)Point > 0U)
                {
                    string Description = dataTable.Rows[0]["Descr"].ToString();
                    int ID_Club_Member_Type = 0;
                    try
                    {
                        ID_Club_Member_Type = int.Parse(dataTable.Rows[0]["ID_Club_Member_Type"].ToString());
                    }
                    catch { }
                    string ID_Club_Campaign = dataTable.Rows[0]["ID_Club_Campaign"].ToString();
                    Club_Point_History_Insert(objMain.ID_GameCenter_Local_Get(), ID_Club_Campaign, ID_Club_Member_Type, Card_GUID, Point, Point_Old, Description);
                    Club_Point_Process_UpdateCardPoint(Card_GUID, Point + Point_Old, ID_Club_Member_Type);
                }
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
            }
        }

        public int Club_Point_History_Insert(int ID_GameCenter, string ID_Club_Campaign, int ID_Club_Member_Type, string Card_GUID, int Point, int Point_Old, string Description)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand("INSERT INTO [dbo].[Club_Point_History] ([ID] ,[ID_GameCenter] ,[ID_Club_Campaign] ,[ID_Club_Member_Type] ,[Card_GUID] ,[Date] ,[Point] ,[Point_Old] ,[Description]) VALUES (@ID ,@ID_GameCenter ,@ID_Club_Campaign ,@ID_Club_Member_Type ,@Card_GUID ,@Date ,@Point ,@Point_Old ,@Description)", connection);
                    com.Parameters.AddWithValue("@ID", Guid.NewGuid());
                    com.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter);
                    com.Parameters.AddWithValue("@ID_Club_Campaign", ID_Club_Campaign);
                    com.Parameters.AddWithValue("@ID_Club_Member_Type", ID_Club_Member_Type);
                    com.Parameters.AddWithValue("@Card_GUID", Card_GUID);
                    com.Parameters.AddWithValue("@Date", DateTime.Now);
                    com.Parameters.AddWithValue("@Point", Point);
                    com.Parameters.AddWithValue("@Point_Old", Point_Old);
                    com.Parameters.AddWithValue("@Description", Description);
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

        public DataTable Club_Point_History_Get(string Card_GUID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(objMain.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("SELECT Club_Point_History.ID, Club_Point_History.ID_GameCenter, Club_Point_History.ID_Club_Campaign, Club_Point_History.ID_Club_Member_Type, Club_Point_History.Card_GUID, dbo.MiladiTOShamsi(Club_Point_History.Date) AS Date, Club_Point_History.Point, Club_Point_History.Point_Old, Club_Point_History.Description, Club_Campaign.Title AS CampaignTitle, Club_Member_Type.Title AS MemberTypeTitle, GameCenter.Title AS GameCenterTitle FROM Club_Point_History INNER JOIN Club_Campaign ON Club_Point_History.ID_Club_Campaign = Club_Campaign.ID INNER JOIN GameCenter ON Club_Point_History.ID_GameCenter = GameCenter.ID LEFT OUTER JOIN Club_Member_Type ON Club_Point_History.ID_Club_Member_Type = Club_Member_Type.ID WHERE (Club_Point_History.Card_GUID = @Card_GUID) ORDER BY Date DESC", connection);
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

        #endregion
    }
}
