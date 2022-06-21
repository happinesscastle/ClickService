// Decompiled with JetBrains decompiler
// Type: ClickServerService.MainClass
// Assembly: ClickServerService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6BDFD2F8-7BA8-4B8A-8EC1-401DFA893333
// Assembly location: C:\Users\Win10\Desktop\ClickServerService.exe

using RCG;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace ClickServerService
{
    internal class MainClass
    {
        private PersianCalendar p = new PersianCalendar();
        private static readonly string PasswordHash = "P@@Sw0rd";
        private static readonly string SaltKey = "srqw1363277$";
        private static readonly string VIKey = "@ZerooneGamecenter";
        public static string DBPATH = string.Empty;
        public static string Local_IP = "";
        private static int ID_GameCenter_Local = 1;
        public static string RetVal = "";
        public static DataTable key_Value_List;
        public static string licence_RequestCode = "";
        public static string licence_IsStock = "0";
        public static string licence_IsSync = "0";
        public static string licence_IsRepair = "0";
        public static string licence_IsGift = "0";
        public static string licence_CardCount = "1";
        public static string licence_IsTimingPlace = "0";
        public static string licence_TimingPlaceCount = "0";
        public static string licence_CashDeskCount = "0";

        public string Encrypt(string plainText)
        {
            byte[] bytes1 = Encoding.UTF8.GetBytes(plainText);
            byte[] bytes2 = new Rfc2898DeriveBytes(MainClass.PasswordHash, Encoding.ASCII.GetBytes(MainClass.SaltKey)).GetBytes(32);
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            rijndaelManaged.Mode = CipherMode.CBC;
            rijndaelManaged.Padding = PaddingMode.Zeros;
            ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor(bytes2, Encoding.ASCII.GetBytes(MainClass.VIKey));
            byte[] array;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(bytes1, 0, bytes1.Length);
                    cryptoStream.FlushFinalBlock();
                    array = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(array);
        }

        public string Decrypt(string encryptedText)
        {
            if (encryptedText == "5KispYBrnaxwJECGK5H03m+6LfTRSNheyX+byYknSM5V0Q8hvBdDDzepBL8HSXWE0aSZY7hI8ebSf4ZdeIjZtO+OSeXPhepIqGPjZ07Pl7vSnBqqL4Me1Kf88dplK9HcWefVjxJEh1/4iU1/Foptvw2hIDA/5O8AIGR5zr9tTBKaWV5yT8tZPhIpiVvVmYTm")
            {
                //return "Data Source=sb.happinesscastle.ir,8080;Initial Catalog=ClickEMS2;User ID=sa;Password=M1cr0l@b";
                return @"Data Source=SEM\SEM;Initial Catalog=GameCenter;Integrated Security=True";
            }
            return null;

            //byte[] buffer = Convert.FromBase64String(encryptedText);
            //byte[] bytes = new Rfc2898DeriveBytes(MainClass.PasswordHash, Encoding.ASCII.GetBytes(MainClass.SaltKey)).GetBytes(32);
            //RijndaelManaged rijndaelManaged = new RijndaelManaged();
            //rijndaelManaged.Mode = CipherMode.CBC;
            //rijndaelManaged.Padding = PaddingMode.None;
            //ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor(bytes, Encoding.ASCII.GetBytes(MainClass.VIKey));
            //MemoryStream memoryStream = new MemoryStream(buffer);
            //CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read);
            //byte[] numArray = new byte[buffer.Length];
            //int count = cryptoStream.Read(numArray, 0, numArray.Length);
            //memoryStream.Close();
            //cryptoStream.Close();
            //return Encoding.UTF8.GetString(numArray, 0, count).TrimEnd("\0".ToCharArray());



        }

        public string DBPath()
        {
            return MainClass.DBPATH;
        }

        public void Decript_Connection_String()
        {
            //string path1 = AppDomain.CurrentDomain.BaseDirectory + "\\db01946.txt";
            //if (System.IO.File.Exists(path1))
            //{
            //  MainClass.DBPATH = System.IO.File.ReadAllText(path1);
            //}
            //else
            //{
            //  string path2 = AppDomain.CurrentDomain.BaseDirectory + "\\db.txt";
            //  if (System.IO.File.Exists(path2))
            //    MainClass.DBPATH = this.Decrypt(System.IO.File.ReadAllText(path2));
            //  else
            //    System.IO.File.Create(path2);
            //}

            MainClass.DBPATH = Decrypt("5KispYBrnaxwJECGK5H03m+6LfTRSNheyX+byYknSM5V0Q8hvBdDDzepBL8HSXWE0aSZY7hI8ebSf4ZdeIjZtO+OSeXPhepIqGPjZ07Pl7vSnBqqL4Me1Kf88dplK9HcWefVjxJEh1/4iU1/Foptvw2hIDA/5O8AIGR5zr9tTBKaWV5yT8tZPhIpiVvVmYTm");
        }

        public string XmlParameters_Read(string ParamName)
        {
            string filename = AppDomain.CurrentDomain.BaseDirectory + "\\Parameters.xml";
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filename);
            return xmlDocument.SelectNodes("/root/" + ParamName)[0].InnerText;
        }

        public void XmlParameters_Change(string ParamName, string Value)
        {
            string filename = AppDomain.CurrentDomain.BaseDirectory + "\\Parameters.xml";
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filename);
            xmlDocument.SelectNodes("/root/" + ParamName)[0].InnerText = Value;
            xmlDocument.Save(filename);
        }

        public void ID_GameCenter_Local_Set(int ID) => MainClass.ID_GameCenter_Local = ID;

        public int ID_GameCenter_Local_Get()
        {
            return MainClass.ID_GameCenter_Local;
        }

        public void LoadGameCenterID()
        {
            MainClass.ID_GameCenter_Local = int.Parse(this.GameCenter_GetLocal(true).Rows[0]["ID"].ToString());
        }

        public string comma(string str) => string.Format("{0:n0}", (object)int.Parse(str == "" ? "0" : str));

        public string getShamsiForDataTextBox(DateTime dateT) => this.p.GetYear(dateT).ToString() + "/" + (object)int.Parse(this.p.GetMonth(dateT).ToString()) + "/" + this.p.GetDayOfMonth(dateT).ToString();

        public string getShamsiAndTime(DateTime dateT) => this.p.GetYear(dateT).ToString() + "/" + (object)int.Parse(this.p.GetMonth(dateT).ToString()) + "/" + this.p.GetDayOfMonth(dateT).ToString() + " " + (object)dateT.Hour + ":" + (object)dateT.Minute + ":" + (object)dateT.Second;

        public void Cashier_Pay_Set(int Cash, int Pos) => MainClass.RetVal = Cash.ToString() + "," + Pos.ToString();

        public string Cashier_Pay_Get() => MainClass.RetVal;

        public void ErrorLog(Exception exp)
        {
            try
            {
                int num = 0;
                try
                {
                    num = Convert.ToInt32(exp.StackTrace.Substring(exp.StackTrace.LastIndexOf(' ')));
                }
                catch
                {
                }
                string str1 = "";
                try
                {
                    str1 = exp.StackTrace.ToString();
                }
                catch
                {
                }
                try
                {
                    using (SqlConnection connection = new SqlConnection(this.DBPath()))
                    {
                        connection.Open();
                        SqlCommand com = new SqlCommand("\r\n                            INSERT INTO [dbo].[SystemLog]\r\n                            ([ID]\r\n                            ,[Date]\r\n                            ,[ID_GameCenter]\r\n                            ,[ID_Users]\r\n                            ,[IP]\r\n                            ,[AppType]\r\n                            ,[AppVersion]\r\n                            ,[LineCode]\r\n                            ,[Message]\r\n                            ,[Trace])\r\n                            VALUES\r\n                            (@ID\r\n                            ,@Date\r\n                            ,@ID_GameCenter\r\n                            ,@ID_Users\r\n                            ,@IP\r\n                            ,@AppType\r\n                            ,@AppVersion\r\n                            ,@LineCode\r\n                            ,@Message\r\n                            ,@Trace)", connection);
                        com.Parameters.AddWithValue("@ID", (object)(this.Max_Tbl("SystemLog", "ID") + 1));
                        com.Parameters.AddWithValue("@Date", (object)DateTime.Now);
                        com.Parameters.AddWithValue("@ID_GameCenter", (object)this.ID_GameCenter_Local_Get());
                        com.Parameters.AddWithValue("@ID_Users", (object)-1);
                        com.Parameters.AddWithValue("@IP", (object)MainClass.Local_IP);
                        com.Parameters.AddWithValue("@AppType", (object)2);
                        string str2 = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                        com.Parameters.AddWithValue("@AppVersion", (object)str2);
                        com.Parameters.AddWithValue("@LineCode", (object)num.ToString());
                        com.Parameters.AddWithValue("@Message", (object)exp.Message.ToString());
                        com.Parameters.AddWithValue("@Trace", (object)str1);
                        com.ExecuteNonQuery();
                        this.Synchronize_Insert(com);
                    }
                }
                catch (Exception ex)
                {
                    this.WriteToFileError("Date: " + DateTime.Now.ToString() + " - Line: " + num.ToString() + " - MSG: " + exp.Message.ToString() + " - Trace: " + str1 + " " + Environment.NewLine);
                }
            }
            catch
            {
            }
        }

        public void _pbNCStatus()
        {
            try
            {
                Dns.GetHostEntry(Dns.GetHostName());
                string empty = string.Empty;
                foreach (IPAddress hostAddress in Dns.GetHostAddresses(Dns.GetHostName()))
                {
                    if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                        empty = hostAddress.ToString();
                }
                Local_IP = empty;
            }
            catch
            {
            }
        }

        public void ErrorLogTemp(string P)
        {
            try
            {
                this.WriteToFileError("Date: " + DateTime.Now.ToString() + " - P: " + P);
            }
            catch
            {
            }
        }

        public void WriteToFileError(string Message)
        {
            string path1 = AppDomain.CurrentDomain.BaseDirectory + "\\ServiceLogs";
            if (!Directory.Exists(path1))
                Directory.CreateDirectory(path1);
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.Date;
            string str = dateTime.ToShortDateString().Replace('/', '_');
            string path2 = baseDirectory + "\\ServiceLogs\\ErrorLog_" + str + ".txt";
            if (!System.IO.File.Exists(path2))
            {
                using (StreamWriter text = System.IO.File.CreateText(path2))
                    text.WriteLine(Message);
            }
            else
            {
                using (StreamWriter streamWriter = System.IO.File.AppendText(path2))
                    streamWriter.WriteLine(Message);
            }
        }

        public int Max_Tbl(string tblName, string FildName)
        {
            try
            {
                DataTable dataTable = new DataTable();
                using (SqlConnection connection = new SqlConnection(this.DBPath()))
                {
                    connection.Open();
                    new SqlDataAdapter(new SqlCommand("select isnull( max(" + FildName + "),0) from " + tblName, connection)).Fill(dataTable);
                    connection.Close();
                    connection.Dispose();
                }
                return dataTable.Rows.Count > 0 ? int.Parse(dataTable.Rows[0][0].ToString()) : 0;
            }
            catch
            {
                return 0;
            }
        }

        public DataTable GameCenter_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = this.DBPath();
                connection.Open();
                SqlCommand selectCommand = new SqlCommand("GameCenter_GetAll", connection);
                selectCommand.CommandType = CommandType.StoredProcedure;
                new SqlDataAdapter(selectCommand).Fill(dataTable);
                connection.Close();
                return dataTable;
            }
            catch (Exception ex)
            {
                this.ErrorLog(ex);
                return dataTable;
            }
        }

        public DataTable GameCenter_GetLocal(bool IsLocal)
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = this.DBPath();
                connection.Open();
                SqlCommand selectCommand = new SqlCommand("select * from GameCenter where IsLocal=@IsLocal ", connection);
                selectCommand.Parameters.Add("@IsLocal", SqlDbType.Bit).Value = (object)IsLocal;
                new SqlDataAdapter(selectCommand).Fill(dataTable);
                connection.Close();
                return dataTable;
            }
            catch (Exception ex)
            {
                this.ErrorLog(ex);
                return dataTable;
            }
        }

        public DataTable AccessPoint_Get(string RXoTX, int ID_GameCenter)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("select * from AccessPoint where ID_GameCenter=@ID_GameCenter and Title=@Title", connection);
                    selectCommand.Parameters.AddWithValue("@ID_GameCenter", (object)ID_GameCenter);
                    selectCommand.Parameters.AddWithValue("@Title", (object)RXoTX);
                    new SqlDataAdapter(selectCommand).Fill(dataTable);
                    connection.Close();
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                return dataTable;
            }
        }

        public DataTable AccessPoint_Update(string RXoTX, int ID_GameCenter, string PortName)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("update AccessPoint set PortName=@PortName where ID_GameCenter=@ID_GameCenter and Title=@Title", connection);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", (object)ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@Title", (object)RXoTX);
                    sqlCommand.Parameters.AddWithValue("@PortName", (object)PortName);
                    sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                return dataTable;
            }
        }

        public void Synchronize_Insert(SqlCommand com)
        {
            if (MainClass.licence_IsSync == "0")
                return;
            string commandText = com.CommandText;
            string CommandTypeB = com.CommandType.ToString();
            int count = com.Parameters.Count;
            ArrayList arrayList1 = new ArrayList();
            ArrayList arrayList2 = new ArrayList();
            string Parameters = "";
            for (int index = 0; index < count; ++index)
            {
                Parameters = Parameters + com.Parameters[index].ParameterName + ",";
                arrayList2.Add(com.Parameters[index].Value);
            }
            if (Parameters.Length > 0)
                Parameters = Parameters.Remove(Parameters.Length - 1, 1);
            this.SynchronizeTable_Insert(this.ID_GameCenter_Local_Get(), DateTime.Now, commandText, CommandTypeB, Parameters, arrayList2.Count >= 1 ? arrayList2[0].ToString() : "", arrayList2.Count >= 2 ? arrayList2[1].ToString() : "", arrayList2.Count >= 3 ? arrayList2[2].ToString() : "", arrayList2.Count >= 4 ? arrayList2[3].ToString() : "", arrayList2.Count >= 5 ? arrayList2[4].ToString() : "", arrayList2.Count >= 6 ? arrayList2[5].ToString() : "", arrayList2.Count >= 7 ? arrayList2[6].ToString() : "", arrayList2.Count >= 8 ? arrayList2[7].ToString() : "", arrayList2.Count >= 9 ? arrayList2[8].ToString() : "", arrayList2.Count >= 10 ? arrayList2[9].ToString() : "", arrayList2.Count >= 11 ? arrayList2[10].ToString() : "", arrayList2.Count >= 12 ? arrayList2[11].ToString() : "", arrayList2.Count >= 13 ? arrayList2[12].ToString() : "", arrayList2.Count >= 14 ? arrayList2[13].ToString() : "", arrayList2.Count >= 15 ? arrayList2[14].ToString() : "", arrayList2.Count >= 16 ? arrayList2[15].ToString() : "", arrayList2.Count >= 17 ? arrayList2[16].ToString() : "", arrayList2.Count >= 18 ? arrayList2[17].ToString() : "", arrayList2.Count >= 19 ? arrayList2[18].ToString() : "", arrayList2.Count >= 20 ? arrayList2[19].ToString() : "", arrayList2.Count >= 21 ? arrayList2[20].ToString() : "", arrayList2.Count >= 22 ? arrayList2[21].ToString() : "", arrayList2.Count >= 23 ? arrayList2[22].ToString() : "", arrayList2.Count >= 24 ? arrayList2[23].ToString() : "", arrayList2.Count >= 25 ? arrayList2[24].ToString() : "", arrayList2.Count >= 26 ? arrayList2[25].ToString() : "", arrayList2.Count >= 27 ? arrayList2[26].ToString() : "", arrayList2.Count >= 28 ? arrayList2[27].ToString() : "", arrayList2.Count >= 29 ? arrayList2[28].ToString() : "", arrayList2.Count >= 30 ? arrayList2[29].ToString() : "", arrayList2.Count >= 31 ? arrayList2[30].ToString() : "", arrayList2.Count >= 32 ? arrayList2[31].ToString() : "", arrayList2.Count >= 33 ? arrayList2[32].ToString() : "", arrayList2.Count >= 34 ? arrayList2[33].ToString() : "", arrayList2.Count >= 35 ? arrayList2[34].ToString() : "", arrayList2.Count >= 36 ? arrayList2[35].ToString() : "", arrayList2.Count >= 37 ? arrayList2[36].ToString() : "", arrayList2.Count >= 38 ? arrayList2[37].ToString() : "", arrayList2.Count >= 39 ? arrayList2[38].ToString() : "", arrayList2.Count >= 40 ? arrayList2[39].ToString() : "", arrayList2.Count >= 41 ? arrayList2[40].ToString() : "", arrayList2.Count >= 42 ? arrayList2[41].ToString() : "", arrayList2.Count >= 43 ? arrayList2[42].ToString() : "", arrayList2.Count >= 44 ? arrayList2[43].ToString() : "", arrayList2.Count >= 45 ? arrayList2[44].ToString() : "", arrayList2.Count >= 46 ? arrayList2[45].ToString() : "", arrayList2.Count >= 47 ? arrayList2[46].ToString() : "", arrayList2.Count >= 48 ? arrayList2[47].ToString() : "", arrayList2.Count >= 49 ? arrayList2[48].ToString() : "", arrayList2.Count >= 50 ? arrayList2[49].ToString() : "", arrayList2.Count >= 51 ? arrayList2[50].ToString() : "", arrayList2.Count >= 52 ? arrayList2[51].ToString() : "", arrayList2.Count >= 53 ? arrayList2[52].ToString() : "", arrayList2.Count >= 54 ? arrayList2[53].ToString() : "", arrayList2.Count >= 55 ? arrayList2[54].ToString() : "", arrayList2.Count >= 56 ? arrayList2[55].ToString() : "", arrayList2.Count >= 57 ? arrayList2[56].ToString() : "", arrayList2.Count >= 58 ? arrayList2[57].ToString() : "", arrayList2.Count >= 59 ? arrayList2[58].ToString() : "", arrayList2.Count >= 60 ? arrayList2[59].ToString() : "", arrayList2.Count >= 61 ? arrayList2[60].ToString() : "", arrayList2.Count >= 62 ? arrayList2[61].ToString() : "", arrayList2.Count >= 63 ? arrayList2[62].ToString() : "", arrayList2.Count >= 64 ? arrayList2[63].ToString() : "", arrayList2.Count >= 65 ? arrayList2[64].ToString() : "", arrayList2.Count >= 66 ? arrayList2[65].ToString() : "", arrayList2.Count >= 67 ? arrayList2[66].ToString() : "", arrayList2.Count >= 68 ? arrayList2[67].ToString() : "", arrayList2.Count >= 69 ? arrayList2[68].ToString() : "", arrayList2.Count >= 70 ? arrayList2[69].ToString() : "", arrayList2.Count >= 71 ? arrayList2[70].ToString() : "", arrayList2.Count >= 72 ? arrayList2[71].ToString() : "", arrayList2.Count >= 73 ? arrayList2[72].ToString() : "", arrayList2.Count >= 74 ? arrayList2[73].ToString() : "", arrayList2.Count >= 75 ? arrayList2[74].ToString() : "", arrayList2.Count >= 76 ? arrayList2[75].ToString() : "", arrayList2.Count >= 77 ? arrayList2[76].ToString() : "", arrayList2.Count >= 78 ? arrayList2[77].ToString() : "", arrayList2.Count >= 79 ? arrayList2[78].ToString() : "", arrayList2.Count >= 80 ? arrayList2[79].ToString() : "", arrayList2.Count >= 81 ? arrayList2[80].ToString() : "", arrayList2.Count >= 82 ? arrayList2[81].ToString() : "", arrayList2.Count >= 83 ? arrayList2[82].ToString() : "", arrayList2.Count >= 84 ? arrayList2[83].ToString() : "", arrayList2.Count >= 85 ? arrayList2[84].ToString() : "", arrayList2.Count >= 86 ? arrayList2[85].ToString() : "", arrayList2.Count >= 87 ? arrayList2[86].ToString() : "", arrayList2.Count >= 88 ? arrayList2[87].ToString() : "", arrayList2.Count >= 89 ? arrayList2[88].ToString() : "", arrayList2.Count >= 90 ? arrayList2[89].ToString() : "", arrayList2.Count >= 91 ? arrayList2[90].ToString() : "", arrayList2.Count >= 92 ? arrayList2[91].ToString() : "", arrayList2.Count >= 93 ? arrayList2[92].ToString() : "", arrayList2.Count >= 94 ? arrayList2[93].ToString() : "", arrayList2.Count >= 95 ? arrayList2[94].ToString() : "", arrayList2.Count >= 96 ? arrayList2[95].ToString() : "", arrayList2.Count >= 97 ? arrayList2[96].ToString() : "", arrayList2.Count >= 98 ? arrayList2[97].ToString() : "", arrayList2.Count >= 99 ? arrayList2[98].ToString() : "", arrayList2.Count >= 100 ? arrayList2[99].ToString() : "");
        }

        public int SynchronizeTable_Insert(
          int ID_GameCenter,
          DateTime Date,
          string CommandText,
          string CommandTypeB,
          string Parameters,
          string PV1,
          string PV2,
          string PV3,
          string PV4,
          string PV5,
          string PV6,
          string PV7,
          string PV8,
          string PV9,
          string PV10,
          string PV11,
          string PV12,
          string PV13,
          string PV14,
          string PV15,
          string PV16,
          string PV17,
          string PV18,
          string PV19,
          string PV20,
          string PV21,
          string PV22,
          string PV23,
          string PV24,
          string PV25,
          string PV26,
          string PV27,
          string PV28,
          string PV29,
          string PV30,
          string PV31,
          string PV32,
          string PV33,
          string PV34,
          string PV35,
          string PV36,
          string PV37,
          string PV38,
          string PV39,
          string PV40,
          string PV41,
          string PV42,
          string PV43,
          string PV44,
          string PV45,
          string PV46,
          string PV47,
          string PV48,
          string PV49,
          string PV50,
          string PV51,
          string PV52,
          string PV53,
          string PV54,
          string PV55,
          string PV56,
          string PV57,
          string PV58,
          string PV59,
          string PV60,
          string PV61,
          string PV62,
          string PV63,
          string PV64,
          string PV65,
          string PV66,
          string PV67,
          string PV68,
          string PV69,
          string PV70,
          string PV71,
          string PV72,
          string PV73,
          string PV74,
          string PV75,
          string PV76,
          string PV77,
          string PV78,
          string PV79,
          string PV80,
          string PV81,
          string PV82,
          string PV83,
          string PV84,
          string PV85,
          string PV86,
          string PV87,
          string PV88,
          string PV89,
          string PV90,
          string PV91,
          string PV92,
          string PV93,
          string PV94,
          string PV95,
          string PV96,
          string PV97,
          string PV98,
          string PV99,
          string PV100)
        {
            try
            {
                int num = 0;
                using (SqlConnection connection = new SqlConnection(this.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(SynchronizeTable_Insert), connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Clear();
                    num = this.Max_Tbl("SynchronizeTable", "ID") + 1;
                    sqlCommand.Parameters.AddWithValue("@ID", (object)num);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", (object)ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@Date", (object)Date);
                    sqlCommand.Parameters.AddWithValue("@IsSynchronize", (object)0);
                    sqlCommand.Parameters.AddWithValue("@CommandText", (object)CommandText);
                    sqlCommand.Parameters.AddWithValue("@CommandType", (object)CommandTypeB);
                    sqlCommand.Parameters.AddWithValue("@Parameters", (object)Parameters);
                    sqlCommand.Parameters.AddWithValue("@PV1", (object)PV1);
                    sqlCommand.Parameters.AddWithValue("@PV2", (object)PV2);
                    sqlCommand.Parameters.AddWithValue("@PV3", (object)PV3);
                    sqlCommand.Parameters.AddWithValue("@PV4", (object)PV4);
                    sqlCommand.Parameters.AddWithValue("@PV5", (object)PV5);
                    sqlCommand.Parameters.AddWithValue("@PV6", (object)PV6);
                    sqlCommand.Parameters.AddWithValue("@PV7", (object)PV7);
                    sqlCommand.Parameters.AddWithValue("@PV8", (object)PV8);
                    sqlCommand.Parameters.AddWithValue("@PV9", (object)PV9);
                    sqlCommand.Parameters.AddWithValue("@PV10", (object)PV10);
                    sqlCommand.Parameters.AddWithValue("@PV11", (object)PV11);
                    sqlCommand.Parameters.AddWithValue("@PV12", (object)PV12);
                    sqlCommand.Parameters.AddWithValue("@PV13", (object)PV13);
                    sqlCommand.Parameters.AddWithValue("@PV14", (object)PV14);
                    sqlCommand.Parameters.AddWithValue("@PV15", (object)PV15);
                    sqlCommand.Parameters.AddWithValue("@PV16", (object)PV16);
                    sqlCommand.Parameters.AddWithValue("@PV17", (object)PV17);
                    sqlCommand.Parameters.AddWithValue("@PV18", (object)PV18);
                    sqlCommand.Parameters.AddWithValue("@PV19", (object)PV19);
                    sqlCommand.Parameters.AddWithValue("@PV20", (object)PV20);
                    sqlCommand.Parameters.AddWithValue("@PV21", (object)PV21);
                    sqlCommand.Parameters.AddWithValue("@PV22", (object)PV22);
                    sqlCommand.Parameters.AddWithValue("@PV23", (object)PV23);
                    sqlCommand.Parameters.AddWithValue("@PV24", (object)PV24);
                    sqlCommand.Parameters.AddWithValue("@PV25", (object)PV25);
                    sqlCommand.Parameters.AddWithValue("@PV26", (object)PV26);
                    sqlCommand.Parameters.AddWithValue("@PV27", (object)PV27);
                    sqlCommand.Parameters.AddWithValue("@PV28", (object)PV28);
                    sqlCommand.Parameters.AddWithValue("@PV29", (object)PV29);
                    sqlCommand.Parameters.AddWithValue("@PV30", (object)PV30);
                    sqlCommand.Parameters.AddWithValue("@PV31", (object)PV31);
                    sqlCommand.Parameters.AddWithValue("@PV32", (object)PV32);
                    sqlCommand.Parameters.AddWithValue("@PV33", (object)PV33);
                    sqlCommand.Parameters.AddWithValue("@PV34", (object)PV34);
                    sqlCommand.Parameters.AddWithValue("@PV35", (object)PV35);
                    sqlCommand.Parameters.AddWithValue("@PV36", (object)PV36);
                    sqlCommand.Parameters.AddWithValue("@PV37", (object)PV37);
                    sqlCommand.Parameters.AddWithValue("@PV38", (object)PV38);
                    sqlCommand.Parameters.AddWithValue("@PV39", (object)PV39);
                    sqlCommand.Parameters.AddWithValue("@PV40", (object)PV40);
                    sqlCommand.Parameters.AddWithValue("@PV41", (object)PV41);
                    sqlCommand.Parameters.AddWithValue("@PV42", (object)PV42);
                    sqlCommand.Parameters.AddWithValue("@PV43", (object)PV43);
                    sqlCommand.Parameters.AddWithValue("@PV44", (object)PV44);
                    sqlCommand.Parameters.AddWithValue("@PV45", (object)PV45);
                    sqlCommand.Parameters.AddWithValue("@PV46", (object)PV46);
                    sqlCommand.Parameters.AddWithValue("@PV47", (object)PV47);
                    sqlCommand.Parameters.AddWithValue("@PV48", (object)PV48);
                    sqlCommand.Parameters.AddWithValue("@PV49", (object)PV49);
                    sqlCommand.Parameters.AddWithValue("@PV50", (object)PV50);
                    sqlCommand.Parameters.AddWithValue("@PV51", (object)PV51);
                    sqlCommand.Parameters.AddWithValue("@PV52", (object)PV52);
                    sqlCommand.Parameters.AddWithValue("@PV53", (object)PV53);
                    sqlCommand.Parameters.AddWithValue("@PV54", (object)PV54);
                    sqlCommand.Parameters.AddWithValue("@PV55", (object)PV55);
                    sqlCommand.Parameters.AddWithValue("@PV56", (object)PV56);
                    sqlCommand.Parameters.AddWithValue("@PV57", (object)PV57);
                    sqlCommand.Parameters.AddWithValue("@PV58", (object)PV58);
                    sqlCommand.Parameters.AddWithValue("@PV59", (object)PV59);
                    sqlCommand.Parameters.AddWithValue("@PV60", (object)PV60);
                    sqlCommand.Parameters.AddWithValue("@PV61", (object)PV61);
                    sqlCommand.Parameters.AddWithValue("@PV62", (object)PV62);
                    sqlCommand.Parameters.AddWithValue("@PV63", (object)PV63);
                    sqlCommand.Parameters.AddWithValue("@PV64", (object)PV64);
                    sqlCommand.Parameters.AddWithValue("@PV65", (object)PV65);
                    sqlCommand.Parameters.AddWithValue("@PV66", (object)PV66);
                    sqlCommand.Parameters.AddWithValue("@PV67", (object)PV67);
                    sqlCommand.Parameters.AddWithValue("@PV68", (object)PV68);
                    sqlCommand.Parameters.AddWithValue("@PV69", (object)PV69);
                    sqlCommand.Parameters.AddWithValue("@PV70", (object)PV70);
                    sqlCommand.Parameters.AddWithValue("@PV71", (object)PV71);
                    sqlCommand.Parameters.AddWithValue("@PV72", (object)PV72);
                    sqlCommand.Parameters.AddWithValue("@PV73", (object)PV73);
                    sqlCommand.Parameters.AddWithValue("@PV74", (object)PV74);
                    sqlCommand.Parameters.AddWithValue("@PV75", (object)PV75);
                    sqlCommand.Parameters.AddWithValue("@PV76", (object)PV76);
                    sqlCommand.Parameters.AddWithValue("@PV77", (object)PV77);
                    sqlCommand.Parameters.AddWithValue("@PV78", (object)PV78);
                    sqlCommand.Parameters.AddWithValue("@PV79", (object)PV79);
                    sqlCommand.Parameters.AddWithValue("@PV80", (object)PV80);
                    sqlCommand.Parameters.AddWithValue("@PV81", (object)PV81);
                    sqlCommand.Parameters.AddWithValue("@PV82", (object)PV82);
                    sqlCommand.Parameters.AddWithValue("@PV83", (object)PV83);
                    sqlCommand.Parameters.AddWithValue("@PV84", (object)PV84);
                    sqlCommand.Parameters.AddWithValue("@PV85", (object)PV85);
                    sqlCommand.Parameters.AddWithValue("@PV86", (object)PV86);
                    sqlCommand.Parameters.AddWithValue("@PV87", (object)PV87);
                    sqlCommand.Parameters.AddWithValue("@PV88", (object)PV88);
                    sqlCommand.Parameters.AddWithValue("@PV89", (object)PV89);
                    sqlCommand.Parameters.AddWithValue("@PV90", (object)PV90);
                    sqlCommand.Parameters.AddWithValue("@PV91", (object)PV91);
                    sqlCommand.Parameters.AddWithValue("@PV92", (object)PV92);
                    sqlCommand.Parameters.AddWithValue("@PV93", (object)PV93);
                    sqlCommand.Parameters.AddWithValue("@PV94", (object)PV94);
                    sqlCommand.Parameters.AddWithValue("@PV95", (object)PV95);
                    sqlCommand.Parameters.AddWithValue("@PV96", (object)PV96);
                    sqlCommand.Parameters.AddWithValue("@PV97", (object)PV97);
                    sqlCommand.Parameters.AddWithValue("@PV98", (object)PV98);
                    sqlCommand.Parameters.AddWithValue("@PV99", (object)PV99);
                    sqlCommand.Parameters.AddWithValue("@PV100", (object)PV100);
                    sqlCommand.ExecuteNonQuery();
                    connection.Close();
                    connection.Dispose();
                }
                return num;
            }
            catch (Exception ex)
            {
                this.ErrorLog(ex);
                return -1;
            }
        }

        public string Start_Synchronize()
        {
            ArrayList arrayList1 = new ArrayList();
            ArrayList arrayList2 = new ArrayList();
            DataTable dataTable = new DataTable();
            int num = 1;
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = this.DBPath();
                connection.Open();
                new SqlDataAdapter(new SqlCommand("select top(50) * from Synch_IN where IsSynchronize=0  order by Date asc,ID asc ", connection)).Fill(dataTable);
                connection.Close();
                SqlConnection sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = this.DBPath();
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                for (int index = 0; index < dataTable.Rows.Count; ++index)
                {
                    try
                    {
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = dataTable.Rows[index]["CommandText"].ToString();
                        sqlCommand.CommandType = dataTable.Rows[index]["CommandType"].ToString() == "Text" ? CommandType.Text : CommandType.StoredProcedure;
                        string[] strArray = dataTable.Rows[index]["Parameters"].ToString().Split(',');
                        if ((uint)strArray.Length > 0U)
                            sqlCommand.Parameters.AddWithValue(strArray[0], (object)dataTable.Rows[index]["PV1"].ToString());
                        if (strArray.Length > 1)
                            sqlCommand.Parameters.AddWithValue(strArray[1], (object)dataTable.Rows[index]["PV2"].ToString());
                        if (strArray.Length > 2)
                            sqlCommand.Parameters.AddWithValue(strArray[2], (object)dataTable.Rows[index]["PV3"].ToString());
                        if (strArray.Length > 3)
                            sqlCommand.Parameters.AddWithValue(strArray[3], (object)dataTable.Rows[index]["PV4"].ToString());
                        if (strArray.Length > 4)
                            sqlCommand.Parameters.AddWithValue(strArray[4], (object)dataTable.Rows[index]["PV5"].ToString());
                        if (strArray.Length > 5)
                            sqlCommand.Parameters.AddWithValue(strArray[5], (object)dataTable.Rows[index]["PV6"].ToString());
                        if (strArray.Length > 6)
                            sqlCommand.Parameters.AddWithValue(strArray[6], (object)dataTable.Rows[index]["PV7"].ToString());
                        if (strArray.Length > 7)
                            sqlCommand.Parameters.AddWithValue(strArray[7], (object)dataTable.Rows[index]["PV8"].ToString());
                        if (strArray.Length > 8)
                            sqlCommand.Parameters.AddWithValue(strArray[8], (object)dataTable.Rows[index]["PV9"].ToString());
                        if (strArray.Length > 9)
                            sqlCommand.Parameters.AddWithValue(strArray[9], (object)dataTable.Rows[index]["PV10"].ToString());
                        if (strArray.Length > 10)
                            sqlCommand.Parameters.AddWithValue(strArray[10], (object)dataTable.Rows[index]["PV11"].ToString());
                        if (strArray.Length > 11)
                            sqlCommand.Parameters.AddWithValue(strArray[11], (object)dataTable.Rows[index]["PV12"].ToString());
                        if (strArray.Length > 12)
                            sqlCommand.Parameters.AddWithValue(strArray[12], (object)dataTable.Rows[index]["PV13"].ToString());
                        if (strArray.Length > 13)
                            sqlCommand.Parameters.AddWithValue(strArray[13], (object)dataTable.Rows[index]["PV14"].ToString());
                        if (strArray.Length > 14)
                            sqlCommand.Parameters.AddWithValue(strArray[14], (object)dataTable.Rows[index]["PV15"].ToString());
                        if (strArray.Length > 15)
                            sqlCommand.Parameters.AddWithValue(strArray[15], (object)dataTable.Rows[index]["PV16"].ToString());
                        if (strArray.Length > 16)
                            sqlCommand.Parameters.AddWithValue(strArray[16], (object)dataTable.Rows[index]["PV17"].ToString());
                        if (strArray.Length > 17)
                            sqlCommand.Parameters.AddWithValue(strArray[17], (object)dataTable.Rows[index]["PV18"].ToString());
                        if (strArray.Length > 18)
                            sqlCommand.Parameters.AddWithValue(strArray[18], (object)dataTable.Rows[index]["PV19"].ToString());
                        if (strArray.Length > 19)
                            sqlCommand.Parameters.AddWithValue(strArray[19], (object)dataTable.Rows[index]["PV20"].ToString());
                        if (strArray.Length > 20)
                            sqlCommand.Parameters.AddWithValue(strArray[20], (object)dataTable.Rows[index]["PV21"].ToString());
                        if (strArray.Length > 21)
                            sqlCommand.Parameters.AddWithValue(strArray[21], (object)dataTable.Rows[index]["PV22"].ToString());
                        if (strArray.Length > 22)
                            sqlCommand.Parameters.AddWithValue(strArray[22], (object)dataTable.Rows[index]["PV23"].ToString());
                        if (strArray.Length > 23)
                            sqlCommand.Parameters.AddWithValue(strArray[23], (object)dataTable.Rows[index]["PV24"].ToString());
                        if (strArray.Length > 24)
                            sqlCommand.Parameters.AddWithValue(strArray[24], (object)dataTable.Rows[index]["PV25"].ToString());
                        if (strArray.Length > 25)
                            sqlCommand.Parameters.AddWithValue(strArray[25], (object)dataTable.Rows[index]["PV26"].ToString());
                        if (strArray.Length > 26)
                            sqlCommand.Parameters.AddWithValue(strArray[26], (object)dataTable.Rows[index]["PV27"].ToString());
                        if (strArray.Length > 27)
                            sqlCommand.Parameters.AddWithValue(strArray[27], (object)dataTable.Rows[index]["PV28"].ToString());
                        if (strArray.Length > 28)
                            sqlCommand.Parameters.AddWithValue(strArray[28], (object)dataTable.Rows[index]["PV29"].ToString());
                        if (strArray.Length > 29)
                            sqlCommand.Parameters.AddWithValue(strArray[29], (object)dataTable.Rows[index]["PV30"].ToString());
                        if (strArray.Length > 30)
                            sqlCommand.Parameters.AddWithValue(strArray[30], (object)dataTable.Rows[index]["PV31"].ToString());
                        if (strArray.Length > 31)
                            sqlCommand.Parameters.AddWithValue(strArray[31], (object)dataTable.Rows[index]["PV32"].ToString());
                        if (strArray.Length > 32)
                            sqlCommand.Parameters.AddWithValue(strArray[32], (object)dataTable.Rows[index]["PV33"].ToString());
                        if (strArray.Length > 33)
                            sqlCommand.Parameters.AddWithValue(strArray[33], (object)dataTable.Rows[index]["PV34"].ToString());
                        if (strArray.Length > 34)
                            sqlCommand.Parameters.AddWithValue(strArray[34], (object)dataTable.Rows[index]["PV35"].ToString());
                        if (strArray.Length > 35)
                            sqlCommand.Parameters.AddWithValue(strArray[35], (object)dataTable.Rows[index]["PV36"].ToString());
                        if (strArray.Length > 36)
                            sqlCommand.Parameters.AddWithValue(strArray[36], (object)dataTable.Rows[index]["PV37"].ToString());
                        if (strArray.Length > 37)
                            sqlCommand.Parameters.AddWithValue(strArray[37], (object)dataTable.Rows[index]["PV38"].ToString());
                        if (strArray.Length > 38)
                            sqlCommand.Parameters.AddWithValue(strArray[38], (object)dataTable.Rows[index]["PV39"].ToString());
                        if (strArray.Length > 39)
                            sqlCommand.Parameters.AddWithValue(strArray[39], (object)dataTable.Rows[index]["PV40"].ToString());
                        if (strArray.Length > 40)
                            sqlCommand.Parameters.AddWithValue(strArray[40], (object)dataTable.Rows[index]["PV41"].ToString());
                        if (strArray.Length > 41)
                            sqlCommand.Parameters.AddWithValue(strArray[41], (object)dataTable.Rows[index]["PV42"].ToString());
                        if (strArray.Length > 42)
                            sqlCommand.Parameters.AddWithValue(strArray[42], (object)dataTable.Rows[index]["PV43"].ToString());
                        if (strArray.Length > 43)
                            sqlCommand.Parameters.AddWithValue(strArray[43], (object)dataTable.Rows[index]["PV44"].ToString());
                        if (strArray.Length > 44)
                            sqlCommand.Parameters.AddWithValue(strArray[44], (object)dataTable.Rows[index]["PV45"].ToString());
                        if (strArray.Length > 45)
                            sqlCommand.Parameters.AddWithValue(strArray[45], (object)dataTable.Rows[index]["PV46"].ToString());
                        if (strArray.Length > 46)
                            sqlCommand.Parameters.AddWithValue(strArray[46], (object)dataTable.Rows[index]["PV47"].ToString());
                        if (strArray.Length > 47)
                            sqlCommand.Parameters.AddWithValue(strArray[47], (object)dataTable.Rows[index]["PV48"].ToString());
                        if (strArray.Length > 48)
                            sqlCommand.Parameters.AddWithValue(strArray[48], (object)dataTable.Rows[index]["PV49"].ToString());
                        if (strArray.Length > 49)
                            sqlCommand.Parameters.AddWithValue(strArray[49], (object)dataTable.Rows[index]["PV50"].ToString());
                        if (strArray.Length > 50)
                            sqlCommand.Parameters.AddWithValue(strArray[50], (object)dataTable.Rows[index]["PV51"].ToString());
                        if (strArray.Length > 51)
                            sqlCommand.Parameters.AddWithValue(strArray[51], (object)dataTable.Rows[index]["PV52"].ToString());
                        if (strArray.Length > 52)
                            sqlCommand.Parameters.AddWithValue(strArray[52], (object)dataTable.Rows[index]["PV53"].ToString());
                        if (strArray.Length > 53)
                            sqlCommand.Parameters.AddWithValue(strArray[53], (object)dataTable.Rows[index]["PV54"].ToString());
                        if (strArray.Length > 54)
                            sqlCommand.Parameters.AddWithValue(strArray[54], (object)dataTable.Rows[index]["PV55"].ToString());
                        if (strArray.Length > 55)
                            sqlCommand.Parameters.AddWithValue(strArray[55], (object)dataTable.Rows[index]["PV56"].ToString());
                        if (strArray.Length > 56)
                            sqlCommand.Parameters.AddWithValue(strArray[56], (object)dataTable.Rows[index]["PV57"].ToString());
                        if (strArray.Length > 57)
                            sqlCommand.Parameters.AddWithValue(strArray[57], (object)dataTable.Rows[index]["PV58"].ToString());
                        if (strArray.Length > 58)
                            sqlCommand.Parameters.AddWithValue(strArray[58], (object)dataTable.Rows[index]["PV59"].ToString());
                        if (strArray.Length > 59)
                            sqlCommand.Parameters.AddWithValue(strArray[59], (object)dataTable.Rows[index]["PV60"].ToString());
                        sqlCommand.ExecuteNonQuery();
                        arrayList1.Add((object)dataTable.Rows[index]["ID"].ToString());
                        this.SynchronizeTable_Set_Synced_New(int.Parse(dataTable.Rows[index]["ID"].ToString()), int.Parse(dataTable.Rows[index]["ID_GameCenter"].ToString()));
                    }
                    catch (Exception ex)
                    {
                        num = 0;
                        arrayList2.Add((object)dataTable.Rows[index]["ID"].ToString());
                        this.SynchronizeTable_Set_NoSynced_New(int.Parse(dataTable.Rows[index]["ID"].ToString()), int.Parse(dataTable.Rows[index]["ID_GameCenter"].ToString()), ex.Message);
                    }
                }
                sqlConnection.Close();
                return num.ToString() + "," + (object)arrayList2.Count + "," + (object)arrayList1.Count;
            }
            catch (Exception ex)
            {
                this.ErrorLog(ex);
                return "-1," + (object)arrayList2.Count + "," + (object)arrayList1.Count;
            }
        }

        public void SynchronizeTable_Set_Synced(ArrayList SynchedList)
        {
            string str = "";
            for (int index = 0; index < SynchedList.Count; ++index)
                str = str + SynchedList[index] + ",";
            if (str.Length > 0)
                str = str.Remove(str.Length - 1, 1);
            try
            {
                using (SqlConnection connection = new SqlConnection(this.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("update Synch_IN set IsSynchronize=1 ,DateSync=@DateSync where ID in (" + str + ")", connection);
                    sqlCommand.Parameters.AddWithValue("@DateSync", (object)DateTime.Now);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                this.ErrorLog(ex);
            }
        }

        public void SynchronizeTable_Set_NoSynced_New(int ID, int ID_GameCenter, string Message)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("update Synch_IN set SynchronizeType=10,Message_Date=@Message_Date,Message=@Message where ID=@ID and ID_GameCenter=@ID_GameCenter ", connection);
                    sqlCommand.Parameters.AddWithValue("@Message_Date", (object)DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", (object)ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@ID", (object)ID);
                    sqlCommand.Parameters.AddWithValue("@Message", (object)Message);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                this.ErrorLog(ex);
            }
        }

        public void SynchronizeTable_Set_Synced_New(int ID, int ID_GameCenter)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("update Synch_IN set IsSynchronize=1 ,DateSync=@DateSync where ID=@ID and ID_GameCenter=@ID_GameCenter ", connection);
                    sqlCommand.Parameters.AddWithValue("@DateSync", (object)DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", (object)ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@ID", (object)ID);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                this.ErrorLog(ex);
            }
        }

        public int SynchronizeTable_Set_NoSynced_IsSynced(int ID_GameCenter)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("update Synch_IN set IsSynchronize=1  where SynchronizeType=10 and ID_GameCenter=@ID_GameCenter ", connection);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", (object)ID_GameCenter);
                    sqlCommand.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.ErrorLog(ex);
                return -1;
            }
        }

        public int SynchronizeTable_NotSetIsSynchronize_Insert(string IDsList)
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = this.DBPath();
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[SynchronizeTable_NotSetIsSynchronize]\r\n           ([ID]\r\n           ,[IDsList]\r\n           ,[Date],[IsSet])\r\n     VALUES\r\n           (@ID\r\n           ,@IDsList\r\n           ,@Date,@IsSet)", connection);
                sqlCommand.Parameters.AddWithValue("@ID", (object)(this.Max_Tbl("SynchronizeTable_NotSetIsSynchronize", "ID") + 1));
                sqlCommand.Parameters.AddWithValue("@IDsList", (object)IDsList);
                sqlCommand.Parameters.AddWithValue("@Date", (object)DateTime.Now);
                sqlCommand.Parameters.AddWithValue("@IsSet", (object)0);
                sqlCommand.ExecuteNonQuery();
                connection.Close();
                return 1;
            }
            catch (Exception ex)
            {
                this.ErrorLog(ex);
                return -1;
            }
        }

        public string SynchronizeTable_NotSetIsSynchronize_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = this.DBPath();
                connection.Open();
                new SqlDataAdapter(new SqlCommand("select * from SynchronizeTable_NotSetIsSynchronize where IsSet=0 ", connection)).Fill(dataTable);
                connection.Close();
                string str = "";
                if (dataTable.Rows.Count > 0)
                {
                    for (int index = 0; index < dataTable.Rows.Count; ++index)
                    {
                        if (dataTable.Rows[index]["IDsList"].ToString() != "")
                            str = str + dataTable.Rows[index]["IDsList"].ToString() + ",";
                    }
                }
                return str;
            }
            catch (Exception ex)
            {
                this.ErrorLog(ex);
                return "";
            }
        }

        public int SynchronizeTable_NotSetIsSynchronize_IsSet()
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = this.DBPath();
                connection.Open();
                new SqlCommand("update   [dbo].[SynchronizeTable_NotSetIsSynchronize] set IsSet=1 ", connection).ExecuteNonQuery();
                connection.Close();
                return 1;
            }
            catch (Exception ex)
            {
                this.ErrorLog(ex);
                return -1;
            }
        }

        public int Games_Ticket_Insert(
          int ID_GameCenter,
          string SwiperMacAddress,
          string Card_MacAddress,
          int Count,
          int Status,
          int ID_Card_Play_Details,
          string Card_GUID,
          int IsPersonnel,
          int ID_Games,
          int ID_Swiper)
        {
            try
            {
                int num = this.Max_Tbl("Games_Ticket", "ID") + 1;
                using (SqlConnection connection = new SqlConnection(this.DBPath()))
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand("INSERT INTO [dbo].[Games_Ticket]\r\n                    ([ID]\r\n                    ,[ID_GameCenter]\r\n                    ,[ID_Card_Play_Details]\r\n                    ,[Card_GUID]\r\n                    ,[SwiperMacAddress]\r\n                    ,[Card_MacAddress]\r\n                    ,[Count]\r\n                    ,[Date]\r\n                    ,[Status],[IsPersonnel],[ID_Games],[ID_Swiper])\r\n                     VALUES\r\n                    (@ID\r\n                    ,@ID_GameCenter\r\n                    ,@ID_Card_Play_Details\r\n                    ,@Card_GUID\r\n                    ,@SwiperMacAddress\r\n                    ,@Card_MacAddress\r\n                    ,@Count\r\n                    ,@Date\r\n                    ,@Status,@IsPersonnel,@ID_Games,@ID_Swiper)", connection);
                    com.Parameters.AddWithValue("@ID", (object)num);
                    com.Parameters.AddWithValue("@ID_GameCenter", (object)ID_GameCenter);
                    com.Parameters.AddWithValue("@ID_Card_Play_Details", (object)ID_Card_Play_Details);
                    try
                    {
                        Guid.Parse(Card_GUID);
                        com.Parameters.AddWithValue("@Card_GUID", (object)Card_GUID);
                    }
                    catch
                    {
                        com.Parameters.AddWithValue("@Card_GUID", (object)"00000000-0000-0000-0000-000000000000");
                    }
                    com.Parameters.AddWithValue("@SwiperMacAddress", (object)SwiperMacAddress);
                    com.Parameters.AddWithValue("@Card_MacAddress", (object)Card_MacAddress);
                    com.Parameters.AddWithValue("@Count", (object)Count);
                    com.Parameters.AddWithValue("@Date", (object)DateTime.Now);
                    com.Parameters.AddWithValue("@Status", (object)Status);
                    com.Parameters.AddWithValue("@IsPersonnel", (object)IsPersonnel);
                    com.Parameters.AddWithValue("@ID_Games", (object)ID_Games);
                    com.Parameters.AddWithValue("@ID_Swiper", (object)ID_Swiper);
                    com.ExecuteNonQuery();
                    this.Synchronize_Insert(com);
                }
                return num;
            }
            catch (Exception ex)
            {
                this.ErrorLog(ex);
                return -1;
            }
        }

        public DataTable Key_Value_Get()
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = this.DBPath();
                connection.Open();
                new SqlDataAdapter(new SqlCommand("select * from Key_Value", connection)).Fill(dataTable);
                connection.Close();
                return dataTable;
            }
            catch (Exception ex)
            {
                this.ErrorLog(ex);
                return dataTable;
            }
        }

        public DataTable Key_Value_GetByID(int ID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = this.DBPath();
                connection.Open();
                SqlCommand selectCommand = new SqlCommand("select * from Key_Value where ID=@ID", connection);
                selectCommand.Parameters.AddWithValue("@ID", (object)ID);
                new SqlDataAdapter(selectCommand).Fill(dataTable);
                connection.Close();
                return dataTable;
            }
            catch (Exception ex)
            {
                this.ErrorLog(ex);
                return dataTable;
            }
        }

        public int Server_ReciveMessage_Insert(string message, DateTime DateSR)
        {
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = this.DBPath();
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("\r\n\r\n\r\nINSERT INTO [dbo].[Server_ReciveMessage]\r\n           ([ID]\r\n           ,[message]\r\n           ,[Date])\r\n     VALUES\r\n           (@ID\r\n           ,@message\r\n           ,@Date)", connection);
                sqlCommand.Parameters.AddWithValue("@ID", (object)(this.Max_Tbl("Server_ReciveMessage", "ID") + 1));
                sqlCommand.Parameters.AddWithValue("@message", (object)message);
                DateTime dateTime = DateTime.Now;
                try
                {
                    dateTime = Convert.ToDateTime(DateSR);
                }
                catch
                {
                }
                sqlCommand.Parameters.AddWithValue("@Date", (object)dateTime);
                sqlCommand.ExecuteNonQuery();
                connection.Close();
                return 1;
            }
            catch (Exception ex)
            {
                this.ErrorLog(ex);
                return -1;
            }
        }

        public int Server_SendMessage_Insert(string message, DateTime DateSR)
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = this.DBPath();
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("\r\n\r\n\r\nINSERT INTO [dbo].[Server_SendMessage]\r\n           ([ID]\r\n           ,[message]\r\n           ,[Date])\r\n     VALUES\r\n           (@ID\r\n           ,@message\r\n           ,@Date)", connection);
                sqlCommand.Parameters.AddWithValue("@ID", (object)(this.Max_Tbl("Server_SendMessage", "ID") + 1));
                sqlCommand.Parameters.AddWithValue("@message", (object)message);
                sqlCommand.Parameters.AddWithValue("@Date", (object)DateSR);
                sqlCommand.ExecuteNonQuery();
                connection.Close();
                return 1;
            }
            catch (Exception ex)
            {
                this.ErrorLog(ex);
                return -1;
            }
        }

        public DataTable ServerConfig_GetByGameCenter(int ID_GameCenter, int multiRun_AP_ID = 0)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.DBPath()))
                {
                    connection.Open();
                    string query = @"Select * From ServerConfigView Where ID_GameCenter = @ID_GameCenter And AP_ID = @MultiRun_AP_ID;
                                     Update ServerConfig set IsRestart = 0  Where ID_GameCenter = @ID_GameCenter";
                    SqlCommand selectCommand = new SqlCommand(query, connection);
                    selectCommand.Parameters.AddWithValue("@ID_GameCenter", (object)ID_GameCenter);
                    selectCommand.Parameters.AddWithValue("@MultiRun_AP_ID", multiRun_AP_ID);
                    new SqlDataAdapter(selectCommand).Fill(dataTable);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                this.ErrorLog(ex);
                return dataTable;
            }
        }

        public int ServerConfig_SetAp1Status(int ID_GameCenter, bool Status, int multiRun_AP_ID = 0)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Update Access_Point Set AP_Status = @Status Where ID_GameCenter = @ID_GameCenter And AP_ID = @MultiRun_AP_ID", connection);
                    sqlCommand.Parameters.AddWithValue("@MultiRun_AP_ID", multiRun_AP_ID);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", (object)ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@Status", (object)Status);
                    sqlCommand.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.ErrorLog(ex);
                return -1;
            }
        }

        public int ServerConfig_SetAp2Status(int ID_GameCenter, bool Status)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(" update ServerConfig set AP2_Status=@Status  where [ID_GameCenter]=@ID_GameCenter ", connection);
                    sqlCommand.Parameters.AddWithValue("@ID_GameCenter", (object)ID_GameCenter);
                    sqlCommand.Parameters.AddWithValue("@Status", (object)Status);
                    sqlCommand.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.ErrorLog(ex);
                return -1;
            }
        }

        public bool licence_Check()
        {
            bool flag;
            try
            {
                //RequestCode requestCode1 = new RequestCode();
                //string requestCode2 = requestCode1.GetRequestCode();
                //string str1 = MainClass.key_Value_List.Select("KeyName ='OrderCode'")[0]["Value"].ToString();
                //string str2 = MainClass.key_Value_List.Select("KeyName ='LicenceCode'")[0]["Value"].ToString();
                //DataTable dataTable = requestCode1.DeEncryptofOptions(str1, str2);
                //if (dataTable.Rows.Count > 0)
                //{
                //    string str3 = dataTable.Rows[0][0].ToString();
                //    string str4 = dataTable.Rows[0][1].ToString().Replace(" ", string.Empty);
                //    string str5 = dataTable.Rows[0][2].ToString();
                //    if (Convert.ToDateTime(str3) < DateTime.Now)
                //        flag = false;
                //    else if (requestCode2 != str5)
                //    {
                //        flag = false;
                //    }
                //    else
                //    {
                //        string[] strArray = str4.Split(':');
                //        for (int index = 0; index < strArray.Length; ++index)
                //        {
                //            int num = int.Parse(strArray[index].Split('*')[0]);
                //            string str6 = strArray[index].Split('*')[1];
                //            switch (num)
                //            {
                //                case 1:
                //                    MainClass.licence_CashDeskCount = str6;
                //                    break;
                //                case 2:
                //                    MainClass.licence_IsTimingPlace = str6;
                //                    break;
                //                case 3:
                //                    MainClass.licence_TimingPlaceCount = str6;
                //                    break;
                //                case 4:
                //                    MainClass.licence_IsStock = str6;
                //                    break;
                //                case 5:
                //                    MainClass.licence_IsRepair = str6;
                //                    break;
                //                case 6:
                //                    MainClass.licence_IsSync = str6;
                //                    break;
                //                case 7:
                //                    MainClass.licence_IsGift = str6;
                //                    break;
                //                case 8:
                //                    MainClass.licence_CardCount = str6;
                //                    break;
                //            }
                //        }
                //        if (MainClass.licence_CardCount == "-1")
                //            MainClass.licence_CardCount = "1000000000";
                //        flag = true;
                //    }
                //}
                //else
                //    flag = false;
                flag = true;
            }
            catch (Exception ex)
            {
                this.ErrorLog(ex);
                flag = false;
            }

            return flag;
        }

        public int ReceiveStorage_insert(string ReciveText, int P)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(ReceiveStorage_insert), connection);
                    sqlCommand.Parameters.AddWithValue("@ReciveText", (object)ReciveText);
                    sqlCommand.Parameters.AddWithValue("@P", (object)P);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.ErrorLog(ex);
                return -1;
            }
        }

        public int ReceiveStorage_getReciveText(string ReciveText)
        {
            try
            {
                int num = 0;
                using (SqlConnection connection = new SqlConnection(this.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(ReceiveStorage_getReciveText), connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ReciveText", (object)ReciveText);
                    SqlParameter sqlParameter = sqlCommand.Parameters.Add("@RetState", SqlDbType.Int);
                    sqlParameter.Direction = ParameterDirection.Output;
                    sqlCommand.ExecuteNonQuery();
                    num = int.Parse(sqlParameter.Value.ToString());
                    connection.Close();
                }
                return num;
            }
            catch (Exception ex)
            {
                this.ErrorLog(ex);
                return -1;
            }
        }

        public int ReceiveStorage_UpdateIsProcess(string ReciveText)
        {
            try
            {
                int num = 0;
                using (SqlConnection connection = new SqlConnection(this.DBPath()))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nameof(ReceiveStorage_UpdateIsProcess), connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ReciveText", (object)ReciveText);
                    sqlCommand.Parameters.AddWithValue("@Processed_Date", (object)DateTime.Now);
                    sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                return num;
            }
            catch (Exception ex)
            {
                this.ErrorLog(ex);
                return -1;
            }
        }

        public DataTable ReceiveStorage_GetForSend()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(this.DBPath()))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand(nameof(ReceiveStorage_GetForSend), connection);
                    selectCommand.CommandType = CommandType.StoredProcedure;
                    new SqlDataAdapter(selectCommand).Fill(dataTable);
                    connection.Close();
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                this.ErrorLog(ex);
                return dataTable;
            }
        }

        #region SEM

        /// <summary>
        /// Get All AccessPoints in Table(Access_Point) Where ID_GameCenter
        /// </summary>
        public DataTable GetAccessPoints()
        {
            DataTable dataTable = new DataTable();
            try
            {
                //using (SqlConnection connection = new SqlConnection(this.DBPath()))
                using (SqlConnection connection = new SqlConnection(@"Data Source=SEM\SEM;Initial Catalog=GameCenter;Integrated Security=True"))
                {
                    connection.Open();
                    SqlCommand selectCommand = new SqlCommand("Select * From Access_Point Where ID_GameCenter = @ID_GameCenter", connection);
                    selectCommand.Parameters.AddWithValue("@ID_GameCenter", ID_GameCenter_Local_Get());
                    new SqlDataAdapter(selectCommand).Fill(dataTable);
                    connection.Close();
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                return dataTable;
            }
        }

        #endregion

        public class Retm
        {
            private int state;
            private int ContOk;
            private int notOk;
        }
    }
}
