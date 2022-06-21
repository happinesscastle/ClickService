// Decompiled with JetBrains decompiler
// Type: ClickServerService.ClsClickService
// Assembly: ClickServerService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6BDFD2F8-7BA8-4B8A-8EC1-401DFA893333
// Assembly location: C:\Users\Win10\Desktop\ClickServerService.exe

using ClickServerService.ClassCode;
using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ClickServerService
{
    public class ClsClickService //: ServiceBase
    {
        public static int Main_ID_GameCenter = 1;
        private SwiperClass objSwiper = new SwiperClass();
        private MainClass objMain = new MainClass();
        private GamesClass objGames = new GamesClass();
        private CardClass objcard = new CardClass();
        private UsersClass objUser = new UsersClass();
        private Pattern objPattern = new Pattern();
        private Club objClub = new Club();
        public static int SwiperID;
        public static int GameCenterID;
        private static string textback;
        private static string MyReport;
        private static string TimeStamp;
        private DataTable TempP1_DT = new DataTable();
        public static SerialPort spTX;
        public static SerialPort spRX;
        public static bool _checkBoxShowAll = false;
        public static int TCP_CountValidateReceivedData = 1;
        public static string IpAp1 = "";
        public static string IpAp2 = "";
        public static bool TCPIpState_Main1 = false;
        public static bool TCPIpState_Main2 = false;

        public static Thread TCp_IP_Thread_1;
        public static Thread TCp_IP_Thread_2;

        public static int cblValidateReceivedData;
        public static bool chbAP1 = false;
        public static bool chbAP2 = false;
        public static string txtAp1_IP = "0.0.0.0";
        public static string txtAp2_IP = "0.0.0.0";
        public static string txtServerIp = "";
        public static int cblRepeatConfig = 2;
        public static bool chbShowAllRecive = false;
        public static string txtRecive = "";
        public static bool chbShowAllSend = false;
        public static string txtSend = "";
        public static bool chbDecreasePriceInLevel2 = false;
        public static bool timerChargeRate_State = false;
        public static bool flagConnectToSQL = false;

        private System.Timers.Timer timer = new System.Timers.Timer();
        private System.Timers.Timer timerChargeRate = new System.Timers.Timer();
        private System.Timers.Timer timerChargeRate_SetNonRecive = new System.Timers.Timer();
        private System.Timers.Timer Timer_SendData = new System.Timers.Timer();
        private System.Timers.Timer Timer_Create_Repair_CheckList = new System.Timers.Timer();

        private static string DispStringSend;
        private static string DispStringRecive;
        private static string DispStringSplit;
        public static int TCP_RepeatCount = 1;
        private static DateTime DispStringReciveTime;
        private static DateTime DispStringSendTime;
        public static string SwiperName_ForShow = "";
        public static string CardMacAddress_ForShow = "";
        public static TcpClient clientAp1;
        private static string DispStringSend_T2 = "";
        private static string DispStringRecive_T2 = "";
        private static string DispStringSplit_T2 = "";
        private static DateTime DispStringReciveTime_T2;
        private static DateTime DispStringSendTime_T2;
        public static string SwiperName_ForShow_T2 = "";
        public static string CardMacAddress_ForShow_T2 = "";
        public static TcpClient clientAp2;
        private IContainer components = (IContainer)null;

         public ClsClickService()
         {
            //this.InitializeComponent();

            Start();
         }

        public void Start()
        {
            //CheckForIllegalCrossThreadCalls = true;
            Console.WriteLine("Start Run");
            TempP1_DT.Clear();
            TempP1_DT.Columns.Clear();
            TempP1_DT.Rows.Clear();
            TempP1_DT.Columns.Add("Txt");
            WriteToFile("Service is started at " + (object)DateTime.Now);

            timer.Elapsed += new ElapsedEventHandler(this.OnElapsedTime);
            timer.Interval = 3000.0;
            timer.Enabled = true;



            timerChargeRate.Elapsed += new ElapsedEventHandler(this.timerChargeRate_Tick);
            timerChargeRate.Interval = 100.0;
            timerChargeRate.Enabled = false;



            timerChargeRate_SetNonRecive.Elapsed += new ElapsedEventHandler(this.timerChargeRate_SetNonRecive_Tick);
            timerChargeRate_SetNonRecive.Interval = 300000.0;
            timerChargeRate_SetNonRecive.Enabled = true;



            Timer_SendData.Elapsed += new ElapsedEventHandler(this.Timer_SendData_Tick);
            Timer_SendData.Interval = 1000.0;
            Timer_SendData.Enabled = true;

            objMain._pbNCStatus();
            AppLoadMain();

            Timer_Create_Repair_CheckList.Elapsed += new ElapsedEventHandler(this.Timer_Create_Repair_CheckList_Tick);
            Timer_Create_Repair_CheckList.Interval = 1800000.0;
            Timer_Create_Repair_CheckList.Enabled = true;

            var a = Task.Run(() => ForBeConteneud());
            a.Wait();
        }

        void ForBeConteneud()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(" : ");
                Console.ForegroundColor = ConsoleColor.White;
                var r = Console.ReadKey();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(" -_-> " + r.Key);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private void Timer_Create_Repair_CheckList_Tick(object sender, EventArgs e)
        {
            this.Create_Repair_CheckList();
        }

        private void Create_Repair_CheckList()
        {
            new Thread(new ThreadStart(new RepairClass().Create_Repair_Today_CheckList)).Start();
        }

        private void timerChargeRate_SetNonRecive_Tick(object sender, EventArgs e)
        {
            this.objSwiper.Swiper_StateUpdateToNotReciveForChargeRate();
        }

        private void timerChargeRate_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Minute != 0)
                return;
            this.objGames.Charge_Rate_GetAll(this.objMain.ID_GameCenter_Local_Get());
            this.timerChargeRate.Interval = 60000.0;
        }

        public void AppLoadMain()
        {
            try
            {
                flagConnectToSQL = false;
                cblValidateReceivedData = 2;
                chbAP1 = false;
                chbAP2 = false;
                txtAp1_IP = "0.0.0.0";
                txtAp2_IP = "0.0.0.0";
                txtServerIp = "0.0.0.0";
                cblRepeatConfig = 2;
                chbShowAllRecive = true;
                txtRecive = "";
                chbShowAllSend = true;
                txtSend = "";
                chbDecreasePriceInLevel2 = false;
                this.objMain.Decript_Connection_String();
                MainClass.key_Value_List = this.objMain.Key_Value_Get();
                this.objSwiper.Swiper_Update_Config_StateAll(0, this.objMain.ID_GameCenter_Local_Get());
                Console.WriteLine("!this.objMain.licence_Check()");
                if (!this.objMain.licence_Check())
                {
                    this.WriteToFile(DateTime.Now.ToString() + ":1:Licence ERROR ");
                    this.timer.Enabled = false;
                    this.Timer_SendData.Enabled = false;
                    //this.Dispose();
                }
                else
                {
                    this.timer.Enabled = true;
                    this.Timer_SendData.Enabled = true;
                    this.objMain.LoadGameCenterID();
                    Main_ID_GameCenter = this.objMain.ID_GameCenter_Local_Get();
                    DataTable byGameCenter = this.objMain.ServerConfig_GetByGameCenter(this.objMain.ID_GameCenter_Local_Get());
                    Console.WriteLine("byGameCenter.Rows.Count : " + byGameCenter.Rows.Count.ToString());
                    if (byGameCenter.Rows.Count > 0)
                    {
                        txtServerIp = byGameCenter.Rows[0]["ServerIP"].ToString();
                        chbAP1 = Convert.ToBoolean(byGameCenter.Rows[0]["IsEnableAP1"].ToString());
                        Console.WriteLine("chbAP1 Fill : " + chbAP1.ToString());
                        txtAp1_IP = byGameCenter.Rows[0]["AP1_IP"].ToString();
                        chbAP2 = Convert.ToBoolean(byGameCenter.Rows[0]["IsEnableAP2"].ToString());
                        txtAp2_IP = byGameCenter.Rows[0]["AP2_IP"].ToString();
                        cblRepeatConfig = int.Parse(byGameCenter.Rows[0]["RepeatConfig"].ToString());
                        cblValidateReceivedData = int.Parse(byGameCenter.Rows[0]["ValidateReceivedData"].ToString());
                        chbDecreasePriceInLevel2 = Convert.ToBoolean(byGameCenter.Rows[0]["IsDecreasePriceInLevel2"].ToString());
                        chbShowAllRecive = Convert.ToBoolean(byGameCenter.Rows[0]["IsShowAllRecive"].ToString());
                        _checkBoxShowAll = Convert.ToBoolean(byGameCenter.Rows[0]["IsShowAllRecive"].ToString());
                        chbShowAllSend = Convert.ToBoolean(byGameCenter.Rows[0]["IsShowAllSend"].ToString());
                        this.WriteToFile("txtServerIp:" + txtServerIp + ",chbAP1:" + chbAP1.ToString() + ",txtAp1_IP:" + txtAp1_IP + ",chbAP2:" + chbAP2.ToString() + ",txtAp2_IP:" + txtAp2_IP + ",cblRepeatConfig:" + (object)cblRepeatConfig + "cblValidateReceivedData:" + (object)cblValidateReceivedData + ",chbDecreasePriceInLevel2:" + chbDecreasePriceInLevel2.ToString() + ",chbShowAllRecive:" + chbShowAllRecive.ToString() + ",chbShowAllSend:" + chbShowAllSend.ToString());
                    }
                    else
                        this.WriteToFile("Not find service config. Please config server service");

                    this.timerChargeRate.Enabled = MainClass.key_Value_List.Select("KeyName ='Enable_Charge_Rate'")[0]["Value"].ToString().ToLower() == "true";
                    flagConnectToSQL = true;
                    try
                    {
                        TCp_IP_Thread_1.Interrupt();
                        TCp_IP_Thread_1.Abort();
                    }
                    catch (Exception ex)
                    {
                    }
                    try
                    {
                        TCp_IP_Thread_2.Interrupt();
                        TCp_IP_Thread_2.Abort();
                    }
                    catch (Exception ex)
                    {
                    }
                    try
                    {
                        txtServerIp = GetLocalIPAddress();
                    }
                    catch
                    {
                        txtServerIp = "";
                    }
                    TCP_RepeatCount = cblRepeatConfig;
                    TCP_CountValidateReceivedData = cblValidateReceivedData;
                    this.objSwiper.Swiper_Update_Config_StateByGameCenterID(this.objMain.ID_GameCenter_Local_Get(), 0);
                    TCP_CountValidateReceivedData = cblValidateReceivedData;
                    IpAp1 = "";
                    IpAp2 = "";
                    if (chbAP1)
                    {
                        try
                        {
                            IpAp1 = txtAp1_IP;
                            TCp_IP_Thread_1 = new Thread(new ThreadStart(this.Recive_StartTcpIp_1));
                            TCp_IP_Thread_1.Start();
                        }
                        catch
                        {
                        }
                    }
                    if (chbAP2)
                    {
                        try
                        {
                            IpAp2 = txtAp2_IP;
                            TCp_IP_Thread_2 = new Thread(new ThreadStart(this.Recive_StartTcpIp_2));
                            TCp_IP_Thread_2.Start();
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.objMain.ErrorLog(ex);
            }
        }

        public static string GetLocalIPAddress()
        {
            foreach (IPAddress address in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                    return address.ToString();
            }
            throw new Exception("Local IP Address Not Found!");
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Timer : OnElapsedTime");
            Console.WriteLine("chbAP1 : "+ chbAP1.ToString());
            if (chbAP1)
            {
                try
                {
                    if (this.Send_Main(IpAp1, "check", clientAp1) == 1)
                    {
                        this.objMain.ServerConfig_SetAp1Status(this.objMain.ID_GameCenter_Local_Get(), true);
                    }
                    else
                    {
                        Console.WriteLine(DateTime.Now.ToString() + ": Ap1 is Disconnect.");
                        this.WriteToFile(DateTime.Now.ToString() + ": Ap1 is Disconnect.");
                        this.objMain.ServerConfig_SetAp1Status(this.objMain.ID_GameCenter_Local_Get(), false);
                        try
                        {
                            Console.WriteLine("TCp_IP_Thread_1.Abort()");
                            TCp_IP_Thread_1.Interrupt();
                            TCp_IP_Thread_1.Abort();
                        }
                        catch (Exception ex)
                        {
                            this.WriteToFile("Ap1 NoStart :" + (object)ex);
                        }
                        try
                        {
                            clientAp1.Close();
                        }
                        catch (Exception ex)
                        {
                            this.WriteToFile(DateTime.Now.ToString() + ":Ap1 Close -" + ex.Message);
                        }
                        IpAp1 = txtAp1_IP;
                        Console.WriteLine("Start TCp_IP_Thread_1");
                        TCp_IP_Thread_1 = new Thread(new ThreadStart(this.Recive_StartTcpIp_1));
                        TCp_IP_Thread_1.Start();
                    }
                }
                catch (Exception ex)
                {
                    this.WriteToFile(DateTime.Now.ToString() + ": Ap1 :" + ex.Message);
                }
            }
            if (chbAP2)
            {
                try
                {
                    if (this.Send_Main(IpAp2, "check", clientAp2) == 1)
                    {
                        this.objMain.ServerConfig_SetAp2Status(this.objMain.ID_GameCenter_Local_Get(), true);
                    }
                    else
                    {
                        this.WriteToFile(DateTime.Now.ToString() + ": Ap2 is Disconnect.");
                        this.objMain.ServerConfig_SetAp2Status(this.objMain.ID_GameCenter_Local_Get(), false);
                        try
                        {
                            TCp_IP_Thread_2.Interrupt();
                            TCp_IP_Thread_2.Abort();
                        }
                        catch (Exception ex)
                        {
                            this.WriteToFile("Ap2 NoStart :" + (object)ex);
                        }
                        try
                        {
                            clientAp2.Close();
                        }
                        catch (Exception ex)
                        {
                            this.WriteToFile(DateTime.Now.ToString() + ":Ap2 Close -" + ex.Message);
                        }
                        IpAp2 = txtAp2_IP;
                        TCp_IP_Thread_2 = new Thread(new ThreadStart(this.Recive_StartTcpIp_2));
                        TCp_IP_Thread_2.Start();
                    }
                }
                catch (Exception ex)
                {
                    this.WriteToFile(DateTime.Now.ToString() + ": Ap2 :" + ex.Message);
                }
            }
            if (!flagConnectToSQL)
                this.AppLoadMain();
            if (!flagConnectToSQL)
                return;
            try
            {
                DataTable byGameCenter = this.objMain.ServerConfig_GetByGameCenter(this.objMain.ID_GameCenter_Local_Get());
                if (byGameCenter.Rows.Count > 0 && Convert.ToBoolean(byGameCenter.Rows[0]["IsRestart"].ToString()))
                    this.AppLoadMain();
            }
            catch (Exception ex)
            {
                this.objMain.ErrorLog(ex);
            }
        }

        public void WriteToFile(string Message)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("WriteToFile : " + Message);
                Console.ForegroundColor = ConsoleColor.White;
                string path1 = AppDomain.CurrentDomain.BaseDirectory + "\\ServiceLogs";
                if (!Directory.Exists(path1))
                    Directory.CreateDirectory(path1);
                string path2 = AppDomain.CurrentDomain.BaseDirectory + "\\ServiceLogs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
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
            catch
            {
            }
        }

        public void WriteToFile_SendRecive(string Message, int SendOrRecive, DateTime dtSR)
        {
            string path1 = AppDomain.CurrentDomain.BaseDirectory + "\\ServiceLogs";
            if (!Directory.Exists(path1))
                Directory.CreateDirectory(path1);
            string path2 = "";
            switch (SendOrRecive)
            {
                case 1:
                    if (this.objMain.Server_SendMessage_Insert(Message, dtSR) != 1)
                    {
                        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        DateTime dateTime = DateTime.Now;
                        dateTime = dateTime.Date;
                        string str = dateTime.ToShortDateString().Replace('/', '_');
                        path2 = baseDirectory + "\\ServiceLogs\\ServiceLog_Send" + str + ".txt";
                        break;
                    }
                    break;
                case 2:
                    if (this.objMain.Server_ReciveMessage_Insert(Message, dtSR) != 1)
                    {
                        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        DateTime dateTime = DateTime.Now;
                        dateTime = dateTime.Date;
                        string str = dateTime.ToShortDateString().Replace('/', '_');
                        path2 = baseDirectory + "\\ServiceLogs\\ServiceLog_Recive" + str + ".txt";
                    }
                    break;
            }
            if (!(path2 != ""))
                return;
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

        public string MacAndTimeStamp_Create(string MacAddress)
        {
            string str1 = ((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString();
            string str2 = str1.Substring(str1.Length - 5, 4);
            char ch1 = MacAddress[0];
            string str3 = ch1.ToString();
            ch1 = MacAddress[1];
            string str4 = ch1.ToString();
            string str5 = str3 + str4 + str2[0].ToString();
            char ch2 = MacAddress[2];
            string str6 = ch2.ToString();
            ch2 = MacAddress[3];
            string str7 = ch2.ToString();
            string str8 = str5 + str6 + str7 + str2[1].ToString();
            char ch3 = MacAddress[4];
            string str9 = ch3.ToString();
            ch3 = MacAddress[5];
            string str10 = ch3.ToString();
            string str11 = str8 + str9 + str10 + str2[2].ToString();
            char ch4 = MacAddress[6];
            string str12 = ch4.ToString();
            ch4 = MacAddress[7];
            string str13 = ch4.ToString();
            return str11 + str12 + str13 + str2[3].ToString();
        }

        public void Recive_StartTcpIp_1()
        {
            DispStringRecive = "";
            DispStringSplit = "";
            try
            {
                int port = 1000;
                byte[] numArray = new byte[256];
                string str1 = (string)null;
                while (true)
                {
                    try
                    {
                        clientAp1 = new TcpClient();
                    }
                    catch
                    {
                        this.WriteToFile("Co_StartTcpIp_1");
                        clientAp1.Close();
                    }
                    clientAp1.Connect(IpAp1, port);
                    Console.WriteLine($"clientAp1.Connect (IP : {IpAp1} , Port: {port})");
                    TCPIpState_Main1 = true;
                    str1 = (string)null;
                    NetworkStream stream = clientAp1.GetStream();
                    try
                    {
                        int count;
                        while ((uint)(count = stream.Read(numArray, 0, numArray.Length)) > 0U)
                        {
                            DispStringRecive = "";
                            DispStringSplit = "";
                            try
                            {
                                string str2 = Encoding.ASCII.GetString(numArray, 0, count).Replace("\n", "");
                                Console.WriteLine(str2);
                                DispStringRecive += str2;
                                try
                                {
                                    string[] strArray = DispStringRecive.Split('[');
                                    for (int index = 0; index < strArray.Length; ++index)
                                    {
                                        DispStringSplit = strArray[index].Trim();
                                        if (DispStringSplit.Length > 0)
                                        {
                                            DispStringSplit = "[" + strArray[index].Trim();
                                            DispStringReciveTime = DateTime.Now;
                                            if (this.Process_ReciveData(DispStringSplit, 1) == "true")
                                                this.Recive_DisplayText(DispStringSplit, "P1");
                                            else if (_checkBoxShowAll)
                                                this.Recive_DisplayText(DispStringSplit, "P1");
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    this.objMain.ErrorLog(ex);
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch (SocketException ex)
            {
            }
            finally
            {
            }
        }

        public void Recive_StartTcpIp_2()
        {
            DispStringRecive_T2 = "";
            DispStringSplit_T2 = "";
            try
            {
                int port = 1000;
                byte[] numArray = new byte[256];
                string str1 = (string)null;
                while (true)
                {
                    try
                    {
                        clientAp2 = new TcpClient();
                    }
                    catch
                    {
                        this.WriteToFile("Co_StartTcpIp_2");
                        clientAp2.Close();
                    }
                    clientAp2.Connect(IpAp2, port);
                    TCPIpState_Main2 = true;
                    str1 = (string)null;
                    NetworkStream stream = clientAp2.GetStream();
                    try
                    {
                        int count;
                        while ((uint)(count = stream.Read(numArray, 0, numArray.Length)) > 0U)
                        {
                            DispStringRecive_T2 = "";
                            DispStringSplit_T2 = "";
                            try
                            {
                                string str2 = Encoding.ASCII.GetString(numArray, 0, count).Replace("\n", "");
                                DispStringRecive_T2 += str2;
                                try
                                {
                                    string[] strArray = DispStringRecive_T2.Split('[');
                                    for (int index = 0; index < strArray.Length; ++index)
                                    {
                                        DispStringSplit_T2 = strArray[index].Trim();
                                        if (DispStringSplit_T2.Length > 0)
                                        {
                                            DispStringSplit_T2 = "[" + strArray[index].Trim();
                                            DispStringReciveTime_T2 = DateTime.Now;
                                            if (this.Process_ReciveData(DispStringSplit_T2, 2) == "true")
                                                this.Recive_DisplayText(DispStringSplit_T2, "P2");
                                            else if (_checkBoxShowAll)
                                                this.Recive_DisplayText(DispStringSplit_T2, "P2");
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    this.objMain.ErrorLog(ex);
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch (SocketException ex)
            {
            }
            finally
            {
            }
        }

        public string Process_ReciveData(string ReciveText, int P)
        {
            bool flag = false;
            try
            {
                ReciveText = ReciveText.Trim();
                try
                {
                    if (ReciveText.Contains("CONFIG"))
                    {
                        if (new Regex("^[[][0-9a-fA-F]{12}[]][+]CONFIG$", RegexOptions.IgnoreCase).Match(ReciveText).Success)
                        {
                            this.ReceiveStorage_Insert(ReciveText, P);
                            flag = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.objMain.ErrorLog(ex);
                    this.objMain.ErrorLogTemp("error Process_ReciveData CONFIG :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                }
                try
                {
                    if (ReciveText.Contains("OKCFG") && !flag)
                    {
                        if (new Regex("^[[][0-9a-fA-F]{12}[]]OKCFG[0-9]{1}$", RegexOptions.IgnoreCase).Match(ReciveText).Success)
                        {
                            this.ReceiveStorage_Insert(ReciveText, P);
                            flag = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.objMain.ErrorLog(ex);
                    this.objMain.ErrorLogTemp("error Process_ReciveData OKCFG :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                }
                try
                {
                    if (ReciveText.Contains("CID") && !flag)
                    {
                        if (new Regex("^[[][0-9a-f]{12}[]][+]CID[=][0-9a-fA-F]{8}[,][0-9]{1}$", RegexOptions.IgnoreCase).Match(ReciveText).Success)
                        {
                            this.ReceiveStorage_Insert(ReciveText, P);
                            flag = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.objMain.ErrorLog(ex);
                    this.objMain.ErrorLogTemp("error Process_ReciveData CID :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                }
                try
                {
                    if (ReciveText.Contains("+P=") && !flag)
                    {
                        if (new Regex("^[[][0-9a-f]{12}[]][+]P[=][0-9a-fA-F]{8}[,][0-9]{1}$", RegexOptions.IgnoreCase).Match(ReciveText).Success)
                        {
                            this.ReceiveStorage_Insert(ReciveText, P);
                            flag = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.objMain.ErrorLog(ex);
                    this.objMain.ErrorLogTemp("error Process_ReciveData +P :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                }
                try
                {
                    if (ReciveText.Contains("+T=") && !flag)
                    {
                        if (new Regex("^[[][0-9a-f]{12}[]][+]T[=][0-9a-fA-F]{8}[,][0-9]{4}[,][A-Z]{1}$", RegexOptions.IgnoreCase).Match(ReciveText).Success)
                        {
                            this.ReceiveStorage_Insert(ReciveText, P);
                            flag = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.objMain.ErrorLog(ex);
                    this.objMain.ErrorLogTemp("error Process_ReciveData +T :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                }
                try
                {
                    if (ReciveText.Contains("Error_conf") && !flag)
                    {
                        this.ReceiveStorage_Insert(ReciveText, P);
                        flag = true;
                    }
                }
                catch (Exception ex)
                {
                    this.objMain.ErrorLog(ex);
                    this.objMain.ErrorLogTemp("error Process_ReciveData Error_config :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                }
                try
                {
                    if (ReciveText.Contains("HID") && !flag)
                    {
                        new Regex("^[[][0-9a-f]{12}[]][+]HID[=][0-9a-fA-F]{8}[,][0-9]{1}$", RegexOptions.IgnoreCase).Match(ReciveText);
                        this.ReceiveStorage_Insert(ReciveText, P);
                        flag = true;
                    }
                }
                catch (Exception ex)
                {
                    this.objMain.ErrorLog(ex);
                    this.objMain.ErrorLogTemp("error Process_ReciveData HID :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                }
                try
                {
                    if (ReciveText.Contains("OK_") && !flag)
                    {
                        new Regex("^[[][0-9a-fA-F]{12}[]]OKCFG_[0-9a-fA-F]$", RegexOptions.IgnoreCase).Match(ReciveText);
                        this.ReceiveStorage_Insert(ReciveText, P);
                        flag = true;
                    }
                }
                catch (Exception ex)
                {
                    this.objMain.ErrorLog(ex);
                    this.objMain.ErrorLogTemp("error Process_ReciveData OKCFG_HID :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                }
                return flag.ToString().ToLower();
            }
            catch (Exception ex)
            {
                this.objMain.ErrorLog(ex);
                this.objMain.ErrorLogTemp("error Process_ReciveData :" + ex.Message);
                return flag.ToString().ToLower();
            }
        }

        public bool ReceiveStorage_Insert(string ReciveText, int P)
        {
            try
            {
                this.objMain.ReceiveStorage_insert(ReciveText, P);
                if (ReciveText.Contains("OKCFG1"))
                    this.objMain.ReceiveStorage_insert(ReciveText, P);
                return true;
            }
            catch (Exception ex)
            {
                this.objMain.ErrorLogTemp("ReceiveStorage_Insert:exp:" + ex.Message + "ReciveText:" + ReciveText);
                return false;
            }
        }

        private void Recive_DisplayText(string ReciveData, string TcpIPName)
        {
            if (txtRecive.Length > 10000)
            {
                txtRecive = "";
                txtSend = "";
            }
            if (!chbShowAllRecive)
            {
                if (txtRecive.Contains(ReciveData))
                    return;
                this.WriteToFile_SendRecive(TcpIPName + "-" + (object)DispStringReciveTime.Hour + ":" + (object)DispStringReciveTime.Minute + ":" + (object)DispStringReciveTime.Second + ":" + (object)DispStringReciveTime.Millisecond + "-" + ReciveData, 2, DispStringReciveTime);
                txtRecive = txtRecive + TcpIPName + "-" + (object)DispStringReciveTime.Hour + ":" + (object)DispStringReciveTime.Minute + ":" + (object)DispStringReciveTime.Second + ":" + (object)DispStringReciveTime.Millisecond + "-" + ReciveData;
            }
            else
            {
                this.WriteToFile_SendRecive(TcpIPName + "-" + (object)DispStringReciveTime.Hour + ":" + (object)DispStringReciveTime.Minute + ":" + (object)DispStringReciveTime.Second + ":" + (object)DispStringReciveTime.Millisecond + "-" + ReciveData, 2, DispStringReciveTime);
                txtRecive = txtRecive + TcpIPName + "-" + (object)DispStringReciveTime.Hour + ":" + (object)DispStringReciveTime.Minute + ":" + (object)DispStringReciveTime.Second + ":" + (object)DispStringReciveTime.Millisecond + "-" + ReciveData;
            }
        }

        private void Timer_SendData_Tick(object sender, EventArgs e)
        {
            DataTable storageGetForSend = this.objMain.ReceiveStorage_GetForSend();
            for (int index1 = 0; index1 < storageGetForSend.Rows.Count; ++index1)
            {
                string str1 = this.Send_Process_Main(storageGetForSend.Rows[index1]["ReciveText"].ToString());
                if (!(str1 == ""))
                {
                    string str2 = str1.Split('!')[0].ToString();
                    str1.Split('!')[1].ToString();
                    string t_SwiperName = str1.Split('!')[2].ToString();
                    string t_CardmacAddress = str1.Split('!')[3].ToString();
                    this.objMain.ReceiveStorage_UpdateIsProcess(storageGetForSend.Rows[index1]["ReciveText"].ToString());
                    int tcpRepeatCount = TCP_RepeatCount;
                    if (str2.Contains("AT+PRC"))
                        tcpRepeatCount *= 2;
                    if (str2.Contains("HIDShow$"))
                    {
                        string[] strArray = str2.Split('$');
                        string str3 = strArray[1].ToString();
                        string str4 = strArray[2].ToString();
                        string str5 = strArray[3].ToString();
                        string str6 = strArray[4].ToString();
                        string str7 = strArray[5].ToString();
                        string str8 = strArray[6].ToString();
                        string str9 = strArray[7].ToString();
                        string str10 = strArray[8].ToString();
                        string str11 = strArray[9].ToString();
                        string str12 = strArray[10].ToString();
                        string str13 = strArray[11].ToString();
                        string str14 = strArray[12].ToString();
                        string str15 = strArray[13].ToString();
                        string str16 = strArray[14].ToString();
                        string str17 = strArray[15].ToString();
                        string str18 = strArray[16].ToString();
                        string str19 = strArray[17].ToString();
                        string str20 = strArray[18].ToString();
                        string str21 = strArray[19].ToString();
                        string str22 = strArray[20].ToString();
                        string str23 = strArray[21].ToString();
                        string str24 = strArray[22].ToString();
                        string str25 = strArray[23].ToString();
                        string str26 = strArray[24].ToString();
                        string str27 = strArray[25].ToString();
                        string str28 = strArray[26].ToString();
                        string str29 = strArray[27].ToString();
                        string str30 = strArray[28].ToString();
                        int millisecondsTimeout1 = 25;
                        int millisecondsTimeout2 = 15;
                        string Command1 = "[" + str3 + "]AT+TPRC=" + str4;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command1, clientAp1);
                            this.Send_Main(IpAp2, Command1, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command2 = "[" + str3 + "]AT+TTIK=" + str5;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command2, clientAp1);
                            this.Send_Main(IpAp2, Command2, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command3 = "[" + str3 + "]A1=" + str6;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command3, clientAp1);
                            this.Send_Main(IpAp2, Command3, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command4 = "[" + str3 + "]A2=" + str7;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command4, clientAp1);
                            this.Send_Main(IpAp2, Command4, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command5 = "[" + str3 + "]A3=" + str8;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command5, clientAp1);
                            this.Send_Main(IpAp2, Command5, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command6 = "[" + str3 + "]A4=" + str9;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command6, clientAp1);
                            this.Send_Main(IpAp2, Command6, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command7 = "[" + str3 + "]A5=" + str10;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command7, clientAp1);
                            this.Send_Main(IpAp2, Command7, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command8 = "[" + str3 + "]B1=" + str11;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command8, clientAp1);
                            this.Send_Main(IpAp2, Command8, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command9 = "[" + str3 + "]B2=" + str12;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command9, clientAp1);
                            this.Send_Main(IpAp2, Command9, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command10 = "[" + str3 + "]B3=" + str13;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command10, clientAp1);
                            this.Send_Main(IpAp2, Command10, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command11 = "[" + str3 + "]B4=" + str14;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command11, clientAp1);
                            this.Send_Main(IpAp2, Command11, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command12 = "[" + str3 + "]B5=" + str15;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command12, clientAp1);
                            this.Send_Main(IpAp2, Command12, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command13 = "[" + str3 + "]C1=" + str16;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command13, clientAp1);
                            this.Send_Main(IpAp2, Command13, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command14 = "[" + str3 + "]C2=" + str17;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command14, clientAp1);
                            this.Send_Main(IpAp2, Command14, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command15 = "[" + str3 + "]C3=" + str18;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command15, clientAp1);
                            this.Send_Main(IpAp2, Command15, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command16 = "[" + str3 + "]C4=" + str19;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command16, clientAp1);
                            this.Send_Main(IpAp2, Command16, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command17 = "[" + str3 + "]C5=" + str20;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command17, clientAp1);
                            this.Send_Main(IpAp2, Command17, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command18 = "[" + str3 + "]D1=" + str21;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command18, clientAp1);
                            this.Send_Main(IpAp2, Command18, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command19 = "[" + str3 + "]D2=" + str22;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command19, clientAp1);
                            this.Send_Main(IpAp2, Command19, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command20 = "[" + str3 + "]D3=" + str23;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command20, clientAp1);
                            this.Send_Main(IpAp2, Command20, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command21 = "[" + str3 + "]D4=" + str24;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command21, clientAp1);
                            this.Send_Main(IpAp2, Command21, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command22 = "[" + str3 + "]D5=" + str25;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command22, clientAp1);
                            this.Send_Main(IpAp2, Command22, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command23 = "[" + str3 + "]E1=" + str26;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command23, clientAp1);
                            this.Send_Main(IpAp2, Command23, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command24 = "[" + str3 + "]E2=" + str27;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command24, clientAp1);
                            this.Send_Main(IpAp2, Command24, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command25 = "[" + str3 + "]E3=" + str28;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command25, clientAp1);
                            this.Send_Main(IpAp2, Command25, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command26 = "[" + str3 + "]E4=" + str29;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command26, clientAp1);
                            this.Send_Main(IpAp2, Command26, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command27 = "[" + str3 + "]E5=" + str30;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command27, clientAp1);
                            this.Send_Main(IpAp2, Command27, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command28 = "[" + str3 + "]END-DATA";
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            this.Send_Main(IpAp1, Command28, clientAp1);
                            this.Send_Main(IpAp2, Command28, clientAp2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                    }
                    else
                    {
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            DispStringSendTime = DateTime.Now;
                            this.Send_DisplayText(str2, "P1", t_SwiperName, t_CardmacAddress);
                            this.Send_Main(IpAp1, str2, clientAp1);
                            Thread.Sleep(10);
                            DispStringSendTime = DateTime.Now;
                            this.Send_DisplayText(str2, "P2", t_SwiperName, t_CardmacAddress);
                            this.Send_Main(IpAp2, str2, clientAp2);
                            Thread.Sleep(5);
                        }
                    }
                    DataTable byState = this.objSwiper.Swiper_GetByState();
                    for (int index2 = 0; index2 < byState.Rows.Count; ++index2)
                    {
                        string str3 = ((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString();
                        string str4 = str3.Substring(str3.Length - 5, 4);
                        string Command = "[" + this.MacAndTimeStamp_Create(byState.Rows[index2]["MacAddress"].ToString().ToLower()) + "]AT+CFG1=" + str4;
                        for (int index3 = 0; index3 < TCP_RepeatCount + 2; ++index3)
                        {
                            this.Send_DisplayText(str2, "P1", "", "");
                            this.Send_Main(IpAp1, Command, clientAp1);
                            Thread.Sleep(100);
                            this.Send_DisplayText(str2, "P2", "", "");
                            this.Send_Main(IpAp2, Command, clientAp2);
                        }
                    }
                    try
                    {
                        if (DispStringRecive.ToUpper().Contains("[KA=") || DispStringRecive.ToLower().Contains("selftest"))
                        {
                            DataTable stateForChangePrice = this.objSwiper.Swiper_GetByState_ForChangePrice();
                            for (int index2 = 0; index2 < stateForChangePrice.Rows.Count; ++index2)
                            {
                                string lower = stateForChangePrice.Rows[index2]["MacAddress"].ToString().ToLower();
                                string str3 = this.MacAndTimeStamp_Create(lower);
                                DataTable addressByChargeRate = this.objSwiper.Swiper_GetByMacAddressByChargeRate(lower.ToUpper());
                                if (addressByChargeRate.Rows.Count > 0)
                                {
                                    this.objSwiper.Swiper_UpdateStateByMacAddress(lower.ToUpper(), -3);
                                    string str4 = "[" + str3 + "]AT+CFG3=" + this.objMain.comma(addressByChargeRate.Rows[0]["PriceAdi"].ToString());
                                    string str5 = "[" + str3 + "]AT+CFG4=" + this.objMain.comma(addressByChargeRate.Rows[0]["PriceVije"].ToString());
                                    for (int index3 = 0; index3 < TCP_RepeatCount + 5; ++index3)
                                    {
                                        this.Send_DisplayText(str4, "P1", "", "");
                                        this.Send_Main(IpAp1, str4, clientAp1);
                                        Thread.Sleep(70);
                                        this.Send_DisplayText(str4, "P2", "", "");
                                        this.Send_Main(IpAp2, str4, clientAp2);
                                    }
                                    Thread.Sleep(100);
                                    for (int index3 = 0; index3 < TCP_RepeatCount + 5; ++index3)
                                    {
                                        this.Send_DisplayText(str5, "P1", "", "");
                                        this.Send_Main(IpAp1, str5, clientAp1);
                                        Thread.Sleep(70);
                                        this.Send_DisplayText(str5, "P2", "", "");
                                        this.Send_Main(IpAp2, str5, clientAp2);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        this.objMain.ErrorLog(ex);
                    }
                }
            }
        }

        public string Send_Process_Main(string ReciveText)
        {
            try
            {
                ReciveText = ReciveText.Trim();
                string str1 = "";
                string str2 = "";
                string str3 = "";
                string str4 = "";
                try
                {
                    if (ReciveText.Contains("CONFIG"))
                    {
                        string str5 = ReciveText.Substring(1, 12);
                        string str6 = str5.Substring(0, 2) + str5.Substring(3, 2) + str5.Substring(6, 2) + str5.Substring(9, 2);
                        string str7 = ((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString();
                        string str8 = str7.Substring(str7.Length - 5, 4);
                        string MacAddress = str6;
                        string str9 = MacAddress[0].ToString() + MacAddress[1].ToString();
                        char ch = str8[0];
                        string str10 = ch.ToString();
                        string str11 = str9 + str10;
                        ch = MacAddress[2];
                        string str12 = ch.ToString();
                        ch = MacAddress[3];
                        string str13 = ch.ToString();
                        string str14 = str11 + str12 + str13;
                        ch = str8[1];
                        string str15 = ch.ToString();
                        string str16 = str14 + str15;
                        ch = MacAddress[4];
                        string str17 = ch.ToString();
                        ch = MacAddress[5];
                        string str18 = ch.ToString();
                        string str19 = str16 + str17 + str18;
                        ch = str8[2];
                        string str20 = ch.ToString();
                        string str21 = str19 + str20;
                        ch = MacAddress[6];
                        string str22 = ch.ToString();
                        ch = MacAddress[7];
                        string str23 = ch.ToString();
                        string str24 = str21 + str22 + str23;
                        ch = str8[3];
                        string str25 = ch.ToString();
                        string str26 = str24 + str25;
                        DataTable addressByChargeRate = this.objSwiper.Swiper_GetByMacAddressByChargeRate(MacAddress);
                        if (addressByChargeRate.Rows.Count > 0)
                        {
                            try
                            {
                                str3 = addressByChargeRate.Rows[0]["Title"].ToString();
                            }
                            catch
                            {
                            }
                            int num = 0;
                            num = !(addressByChargeRate.Rows[0]["Config_State"].ToString() == "") && addressByChargeRate.Rows[0]["Config_State"] != null ? int.Parse(addressByChargeRate.Rows[0]["Config_State"].ToString()) : 0;
                            if (true)
                            {
                                str1 = "[" + str26 + "]AT+CFG1=" + str8;
                                str2 = "[" + str26 + "]AT+CFG1=" + str8;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.objMain.ErrorLog(ex);
                    this.objMain.ErrorLogTemp("error Process Main CONFIG :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                    return "";
                }
                try
                {
                    if (ReciveText.Contains("OKCFG"))
                    {
                        string str5 = ReciveText.Substring(1, 12);
                        string MacAddress = str5.Substring(0, 2) + str5.Substring(3, 2) + str5.Substring(6, 2) + str5.Substring(9, 2);
                        string str6 = this.MacAndTimeStamp_Create(MacAddress);
                        DataTable addressByChargeRate = this.objSwiper.Swiper_GetByMacAddressByChargeRate(MacAddress.ToUpper());
                        if (addressByChargeRate.Rows.Count > 0)
                        {
                            try
                            {
                                str3 = addressByChargeRate.Rows[0]["Title"].ToString();
                            }
                            catch
                            {
                            }
                            int num1 = int.Parse(addressByChargeRate.Rows[0]["Config_State"].ToString());
                            if (ReciveText.Contains("OKCFG1"))
                            {
                                this.objSwiper.Swiper_Update_Config_State(1, MacAddress);
                                MyReport = "موفقیت در ثبت تایم استمپ /r/n";
                                string str7 = addressByChargeRate.Rows[0]["Title"].ToString().Length > 8 ? addressByChargeRate.Rows[0]["Title"].ToString().Substring(0, 8) : addressByChargeRate.Rows[0]["Title"].ToString();
                                str1 = "[" + str6 + "]AT+CFG2=" + str7;
                                str2 = "[" + str6 + "]AT+CFG2=" + str7;
                            }
                            if (ReciveText.Contains("OKCFG2"))
                            {
                                MyReport = "موفقیت در ثبت نام دستگاه /r/n";
                                this.objSwiper.Swiper_Update_Config_State(2, MacAddress);
                                if (MainClass.key_Value_List.Select("KeyName ='Enable_Charge_Rate'")[0]["Value"].ToString().ToLower() == "true")
                                {
                                    str1 = "[" + str6 + "]AT+CFG3=" + this.objMain.comma(addressByChargeRate.Rows[0]["PriceAdi"].ToString());
                                    str2 = "[" + str6 + "]AT+CFG3=" + this.objMain.comma(addressByChargeRate.Rows[0]["PriceAdi"].ToString());
                                }
                                else
                                {
                                    str1 = "[" + str6 + "]AT+CFG3=" + this.objMain.comma(addressByChargeRate.Rows[0]["Price1"].ToString());
                                    str2 = "[" + str6 + "]AT+CFG3=" + this.objMain.comma(addressByChargeRate.Rows[0]["Price1"].ToString());
                                }
                            }
                            if (ReciveText.Contains("OKCFG3"))
                            {
                                if (num1 != -3)
                                {
                                    MyReport = "موفقیت در ثبت نام قیمت عادی /r/n";
                                    this.objSwiper.Swiper_Update_Config_State(3, MacAddress);
                                    if (MainClass.key_Value_List.Select("KeyName ='Enable_Charge_Rate'")[0]["Value"].ToString().ToLower() == "true")
                                    {
                                        str1 = "[" + str6 + "]AT+CFG4=" + this.objMain.comma(addressByChargeRate.Rows[0]["PriceVije"].ToString());
                                        str2 = "[" + str6 + "]AT+CFG4=" + this.objMain.comma(addressByChargeRate.Rows[0]["PriceVije"].ToString());
                                    }
                                    else
                                    {
                                        str1 = "[" + str6 + "]AT+CFG4=" + this.objMain.comma(addressByChargeRate.Rows[0]["Price2"].ToString());
                                        str2 = "[" + str6 + "]AT+CFG4=" + this.objMain.comma(addressByChargeRate.Rows[0]["Price2"].ToString());
                                    }
                                }
                                else
                                {
                                    str1 = "[" + str6 + "]AT+ok";
                                    str2 = "[" + str6 + "]AT+ok";
                                }
                            }
                            if (ReciveText.Contains("OKCFG4"))
                            {
                                if (num1 != -3)
                                {
                                    MyReport = "موفقیت در ثبت نام قیمت ویژه /r/n";
                                    this.objSwiper.Swiper_Update_Config_State(4, MacAddress);
                                    str1 = "[" + str6 + "]AT+CFG5=" + addressByChargeRate.Rows[0]["Delay1"].ToString() + "," + addressByChargeRate.Rows[0]["Delay2"].ToString();
                                    str2 = "[" + str6 + "]AT+CFG5=" + addressByChargeRate.Rows[0]["Delay1"].ToString() + "," + addressByChargeRate.Rows[0]["Delay2"].ToString();
                                }
                                else
                                {
                                    str1 = "[" + str6 + "]AT+ok";
                                    str2 = "[" + str6 + "]AT+ok";
                                    this.objSwiper.Swiper_UpdateStateByMacAddress(MacAddress.ToUpper(), 0);
                                }
                            }
                            if (ReciveText.Contains("OKCFG5"))
                            {
                                string str7 = addressByChargeRate.Rows[0]["Version"].ToString();
                                MyReport = "موفقیت در ثبت نام Delay و TimeOut /r/n";
                                this.objSwiper.Swiper_Update_Config_State(5, MacAddress);
                                string str8;
                                if (int.Parse(addressByChargeRate.Rows[0]["Pulse"].ToString()) < 100)
                                {
                                    str8 = addressByChargeRate.Rows[0]["Pulse"].ToString();
                                }
                                else
                                {
                                    switch (addressByChargeRate.Rows[0]["Pulse"].ToString())
                                    {
                                        case "100":
                                            str8 = "01";
                                            break;
                                        case "200":
                                            str8 = "02";
                                            break;
                                        case "300":
                                            str8 = "03";
                                            break;
                                        case "400":
                                            str8 = "04";
                                            break;
                                        case "500":
                                            str8 = "05";
                                            break;
                                        case "600":
                                            str8 = "06";
                                            break;
                                        case "700":
                                            str8 = "07";
                                            break;
                                        case "800":
                                            str8 = "08";
                                            break;
                                        case "900":
                                            str8 = "09";
                                            break;
                                        default:
                                            str8 = "00";
                                            break;
                                    }
                                }
                                bool flag1 = false;
                                bool flag2 = false;
                                bool flag3 = false;
                                bool flag4 = false;
                                try
                                {
                                    flag1 = Convert.ToBoolean(addressByChargeRate.Rows[0]["State"].ToString());
                                    flag2 = Convert.ToBoolean(addressByChargeRate.Rows[0]["ClassStatus"].ToString());
                                    flag3 = Convert.ToBoolean(addressByChargeRate.Rows[0]["GroupsStatus"].ToString());
                                    flag4 = Convert.ToBoolean(addressByChargeRate.Rows[0]["SegmentStatus"].ToString());
                                }
                                catch
                                {
                                }
                                string str9 = "0";
                                if (flag1 & flag2 & flag3 & flag4)
                                    str9 = str7;
                                str1 = "[" + str6 + "]AT+CFG6=" + str8 + "," + addressByChargeRate.Rows[0]["RepeatCount"].ToString() + "," + str9;
                                str2 = "[" + str6 + "]AT+CFG6=" + str8 + "," + addressByChargeRate.Rows[0]["RepeatCount"].ToString() + "," + str9;
                            }
                            if (ReciveText.Contains("OKCFG6"))
                            {
                                try
                                {
                                    string str7 = addressByChargeRate.Rows[0]["Version"].ToString();
                                    if (str7 == "1")
                                    {
                                        this.objSwiper.Swiper_Update_Config_State(0, MacAddress);
                                        MyReport = "تنظیمات پایه با موفقیت انجام یافت";
                                    }
                                    if (str7 == "2")
                                    {
                                        int num2 = int.Parse(addressByChargeRate.Rows[0]["PulseType"].ToString());
                                        string str8 = addressByChargeRate.Rows[0]["Start_Count_Voltage"].ToString();
                                        string str9 = addressByChargeRate.Rows[0]["TicketErrorStop"].ToString();
                                        string str10 = addressByChargeRate.Rows[0]["PullUp"].ToString();
                                        this.objSwiper.Swiper_Update_Config_State(6, MacAddress);
                                        str1 = "[" + str6 + "]AT+CFG7=" + (object)num2 + "," + str8 + "," + str9 + "," + str10;
                                        str2 = "[" + str6 + "]AT+CFG7=" + (object)num2 + "," + str8 + "," + str9 + "," + str10;
                                    }
                                    if (str7 == "3")
                                    {
                                        int num2 = int.Parse(addressByChargeRate.Rows[0]["PulseType"].ToString());
                                        string str8 = addressByChargeRate.Rows[0]["Start_Count_Voltage"].ToString();
                                        string str9 = addressByChargeRate.Rows[0]["TicketErrorStop"].ToString();
                                        string str10 = addressByChargeRate.Rows[0]["PullUp"].ToString();
                                        this.objSwiper.Swiper_Update_Config_State(6, MacAddress);
                                        str1 = "[" + str6 + "]AT+CFG7=" + (object)num2 + "," + str8 + "," + str9 + "," + str10;
                                        str2 = "[" + str6 + "]AT+CFG7=" + (object)num2 + "," + str8 + "," + str9 + "," + str10;
                                    }
                                }
                                catch
                                {
                                    this.objSwiper.Swiper_Update_Config_State(0, MacAddress);
                                    MyReport = "تنظیمات پایه با موفقیت انجام یافت";
                                }
                            }
                            if (ReciveText.Contains("OKCFG7"))
                            {
                                str1 = "[" + str6 + "]AT+ok";
                                str2 = "[" + str6 + "]AT+ok";
                                this.objSwiper.Swiper_Update_Config_State(0, MacAddress);
                                MyReport = "تنظیمات پایه با موفقیت انجام یافت";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.objMain.ErrorLog(ex);
                    this.objMain.ErrorLogTemp("error Process Main OKCFG :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                    return "";
                }
                string str27 = "1515";
                try
                {
                    if (ReciveText.Contains("CID"))
                    {
                        string str5 = ReciveText.Substring(1, 12);
                        string str6 = str5.Substring(0, 2) + str5.Substring(3, 2) + str5.Substring(6, 2) + str5.Substring(9, 2);
                        string str7 = this.MacAndTimeStamp_Create(str6);
                        DataTable addressByChargeRate = this.objSwiper.Swiper_GetByMacAddressByChargeRate(str6);
                        str27 = "1524";
                        if (addressByChargeRate.Rows.Count > 0)
                        {
                            int ID_Play_Type = 1;
                            try
                            {
                                str3 = addressByChargeRate.Rows[0]["Title"].ToString();
                            }
                            catch
                            {
                            }
                            int num1 = int.Parse(addressByChargeRate.Rows[0]["ID_Games"].ToString());
                            int.Parse(addressByChargeRate.Rows[0]["ID_Games_Class"].ToString() == "" ? "-100" : addressByChargeRate.Rows[0]["ID_Games_Class"].ToString());
                            int ID_Swiper = int.Parse(addressByChargeRate.Rows[0]["ID"].ToString());
                            string upper = ReciveText.Split('=')[1].Replace("\r", "").Split(',')[0].ToUpper();
                            str4 = upper;
                            str27 = "1537";
                            DataTable byMacAddrress = this.objcard.Card_GetByMacAddrress(upper);
                            if (byMacAddrress.Rows.Count > 0)
                            {
                                str27 = "1543";
                                Guid empty = Guid.Empty;
                                int num2 = int.Parse(byMacAddrress.Rows[0]["ID_Card_Status"].ToString());
                                str27 = "1546";
                                bool flag1 = false;
                                try
                                {
                                    flag1 = bool.Parse(byMacAddrress.Rows[0]["IsNonTicket"].ToString());
                                }
                                catch
                                {
                                }
                                str27 = "1548";
                                bool.Parse(byMacAddrress.Rows[0]["AllowRegistration"].ToString());
                                int num3 = 0;
                                try
                                {
                                    num3 = int.Parse(byMacAddrress.Rows[0]["ID_Card_Promotional"].ToString());
                                }
                                catch
                                {
                                }
                                str27 = "1556";
                                bool boolean = Convert.ToBoolean(byMacAddrress.Rows[0]["IsNonPlayGames"].ToString().ToLower() == "" ? "false" : byMacAddrress.Rows[0]["IsNonPlayGames"].ToString());
                                str27 = "1558";
                                bool flag2 = Convert.ToBoolean(byMacAddrress.Rows[0]["IsActive"].ToString());
                                str27 = "1560";
                                if (int.Parse(byMacAddrress.Rows[0]["ID_Card_Series"].ToString()) == 3 && this.objUser.Users_GetByGUID(byMacAddrress.Rows[0]["Card_GUID"].ToString()).Rows.Count == 0)
                                    flag2 = false;
                                str27 = "1569";
                                if (flag2)
                                {
                                    if (!boolean)
                                    {
                                        str27 = "B1580";
                                        int num4 = int.Parse(byMacAddrress.Rows[0]["ID_Card_Series"].ToString());
                                        string str8 = byMacAddrress.Rows[0]["Card_GUID"].ToString();
                                        string str9 = byMacAddrress.Rows[0]["ID_Club_Member_Type"].ToString();
                                        bool flag3 = !(str9 == "") && !(str9 == "0");
                                        str27 = "B1590";
                                        int num5;
                                        if (MainClass.key_Value_List.Select("KeyName ='Enable_Charge_Rate'")[0]["Value"].ToString().ToLower() == "true")
                                        {
                                            num5 = num2 != 2 ? int.Parse(addressByChargeRate.Rows[0]["PriceAdi"].ToString()) : int.Parse(addressByChargeRate.Rows[0]["PriceVije"].ToString());
                                            if (flag3)
                                                num5 = int.Parse(addressByChargeRate.Rows[0]["PriceVije"].ToString());
                                        }
                                        else
                                        {
                                            num5 = num2 != 2 ? int.Parse(addressByChargeRate.Rows[0]["Price1"].ToString()) : int.Parse(addressByChargeRate.Rows[0]["Price2"].ToString());
                                            if (flag3)
                                                num5 = int.Parse(addressByChargeRate.Rows[0]["Price2"].ToString());
                                        }
                                        str27 = "B1622";
                                        int IsPersonnel = 0;
                                        if (num4 == 3)
                                        {
                                            ID_Play_Type = 4;
                                            IsPersonnel = 1;
                                        }
                                        str27 = "B1629";
                                        bool flag4 = false;
                                        int num6 = 0;
                                        Tuple<bool, int, int, int, int, bool, string> tuple = this.objcard.Card_CardProductTiming_Status(str8, num1);
                                        str27 = "B1633";
                                        int num7;
                                        if (tuple.Item1)
                                        {
                                            string str10 = tuple.Item7;
                                            string str11 = str10.Split(',')[0];
                                            string str12 = str10.Split(',')[1];
                                            string s = str10.Split(',')[2];
                                            str27 = "B1640";
                                            if (tuple.Item6)
                                            {
                                                str27 = "B1643";
                                                flag4 = true;
                                                ID_Play_Type = 10;
                                                str2 = "[" + str7 + "]AT+PRC=" + (int.Parse(s) - 1).ToString() + "-" + str12;
                                                str1 = "[" + str7 + "]AT+PRC=" + (int.Parse(s) - 1).ToString() + "-" + str12;
                                                if (!chbDecreasePriceInLevel2)
                                                {
                                                    int num8 = this.objcard.Card_Play_Details_Insert(str8, 0, 0, Main_ID_GameCenter, str6, num5, 0, 0, IsPersonnel, num1, ID_Swiper, ID_Play_Type, 0, 0, 0, empty);
                                                    str2 = str2 + " -L1 -" + (object)num8;
                                                }
                                            }
                                            else if (tuple.Item5 > 0)
                                            {
                                                str27 = "B1656";
                                                flag4 = true;
                                                ID_Play_Type = 9;
                                                str2 = "[" + str7 + "]AT+PRC=" + (tuple.Item5 - 1).ToString() + "-" + str11;
                                                str1 = "[" + str7 + "]AT+PRC=" + (tuple.Item5 - 1).ToString() + " - " + str11;
                                                if (!chbDecreasePriceInLevel2)
                                                {
                                                    int num8 = this.objcard.Card_Play_Details_Insert(str8, 0, 0, Main_ID_GameCenter, str6, num5, 0, 0, IsPersonnel, num1, ID_Swiper, ID_Play_Type, 0, 0, 0, empty);
                                                    str2 = str2 + " -L1 -" + (object)num8;
                                                    this.objcard.Card_CardProductTiming_SetFreeGame(str8, tuple.Item3, tuple.Item5 - 1);
                                                }
                                            }
                                            else
                                            {
                                                str27 = "B1670";
                                                if (tuple.Item4 > 0)
                                                {
                                                    ID_Play_Type = 11;
                                                    if (tuple.Item2 >= num5)
                                                    {
                                                        str27 = "B1677";
                                                        flag4 = true;
                                                        str2 = "[" + str7 + "]AT+PRC=" + this.objMain.comma((tuple.Item2 - num5).ToString());
                                                        string str13 = str7;
                                                        MainClass objMain = this.objMain;
                                                        num7 = tuple.Item2 - num5;
                                                        string str14 = num7.ToString();
                                                        string str15 = objMain.comma(str14);
                                                        str1 = "[" + str13 + "]AT+PRC=" + str15;
                                                        if (!chbDecreasePriceInLevel2)
                                                        {
                                                            int num8 = this.objcard.Card_Play_Details_Insert(str8, 0, 0, Main_ID_GameCenter, str6, num5, 0, 0, IsPersonnel, num1, ID_Swiper, ID_Play_Type, 0, 0, 0, empty);
                                                            this.objcard.Card_CardProductTiming_SetChargePrice(str8, tuple.Item3, tuple.Item2 - num5);
                                                            str2 = str2 + " -L1 -" + (object)num8;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        num6 = tuple.Item2;
                                                        flag4 = false;
                                                    }
                                                }
                                                else
                                                {
                                                    str27 = "B1697";
                                                    flag4 = true;
                                                    ID_Play_Type = 5;
                                                    str2 = "[" + str7 + "]AT+PRC=--";
                                                    str1 = "[" + str7 + "]AT+PRC=--";
                                                    if (!chbDecreasePriceInLevel2)
                                                    {
                                                        int num8 = this.objcard.Card_Play_Details_Insert(str8, 0, 0, Main_ID_GameCenter, str6, num5, 0, 0, IsPersonnel, num1, ID_Swiper, ID_Play_Type, 0, 0, 0, empty);
                                                        str2 = str2 + " -L1 -" + (object)num8;
                                                    }
                                                }
                                            }
                                        }
                                        str27 = "1686";
                                        if (!flag4)
                                        {
                                            int num8 = int.Parse(byMacAddrress.Rows[0]["CashPrice"].ToString());
                                            int num9 = int.Parse(byMacAddrress.Rows[0]["BonusPrice"].ToString());
                                            DataTable dataTable = this.objPattern.Gift_series_List_ActiveByCard_GUID(str8);
                                            int Pay_GiftPortion = 0;
                                            bool flag5 = false;
                                            bool flag6 = false;
                                            if (dataTable.Rows.Count > 0)
                                            {
                                                for (int index = 0; index < dataTable.Rows.Count; num7 = index++)
                                                {
                                                    if (int.Parse(dataTable.Rows[index]["Real_Charge"].ToString()) > 0)
                                                        empty = Guid.Parse(dataTable.Rows[index]["ID"].ToString());
                                                    Pay_GiftPortion += int.Parse(dataTable.Rows[index]["Real_Charge"].ToString());
                                                    int num10 = int.Parse(dataTable.Rows[index]["Real_FreegameCount"].ToString());
                                                    int num11 = int.Parse(dataTable.Rows[index]["Real_FreeDailyGamesCount"].ToString());
                                                    if (num11 > 0 && ("," + dataTable.Rows[index]["FreeDailyGames"].ToString() + ",").Contains("," + (object)num1 + ",") && this.objcard.Card_Play_Details_Get_Today(str8, dataTable.Rows[index]["FreeDailyGames"].ToString()).Rows.Count < num11)
                                                    {
                                                        empty = Guid.Parse(dataTable.Rows[index]["ID"].ToString());
                                                        flag6 = true;
                                                        break;
                                                    }
                                                    if (num10 > 0 && ("," + dataTable.Rows[index]["FreeGames"].ToString() + ",").Contains("," + (object)num1 + ","))
                                                    {
                                                        empty = Guid.Parse(dataTable.Rows[index]["ID"].ToString());
                                                        flag5 = true;
                                                        if (!chbDecreasePriceInLevel2)
                                                        {
                                                            this.objPattern.Gift_Pattern_series_List_Update(dataTable.Rows[index]["ID"].ToString(), num10 - 1);
                                                            break;
                                                        }
                                                        break;
                                                    }
                                                }
                                            }
                                            str27 = "1736";
                                            if (flag5)
                                            {
                                                ID_Play_Type = 2;
                                                num5 = 0;
                                            }
                                            if (flag6)
                                            {
                                                ID_Play_Type = 3;
                                                num5 = 0;
                                            }
                                            if (num3 > 0)
                                                ID_Play_Type = 7;
                                            if (flag6 | flag5)
                                            {
                                                str2 = "[" + str7 + "]AT+PRC=---";
                                                str1 = "[" + str7 + "]AT+PRC=---";
                                                if (!chbDecreasePriceInLevel2)
                                                {
                                                    int num10 = this.objcard.Card_UpdatePriceAndBonus_PlayDetails2(str8, num8, num9, Main_ID_GameCenter, str6, num5, num8, num9, IsPersonnel, num1, ID_Swiper, ID_Play_Type, 0, 0, 0, empty);
                                                    str2 = str2 + " -L1 -" + (object)num10;
                                                }
                                            }
                                            else
                                            {
                                                if (Pay_GiftPortion > 0)
                                                    ID_Play_Type = 8;
                                                if (num8 + num9 + Pay_GiftPortion + num6 >= num5)
                                                {
                                                    int num10 = num8 + Pay_GiftPortion + num9 + num6 - num5;
                                                    str2 = "[" + str7 + "]AT+PRC=" + this.objMain.comma(num10.ToString());
                                                    string str10 = num10.ToString().Length > 7 ? num10.ToString() : this.objMain.comma(num10.ToString());
                                                    str1 = "[" + str7 + "]AT+PRC=" + str10;
                                                    if (!chbDecreasePriceInLevel2)
                                                    {
                                                        int num11 = this.objPattern.Gift_Pattern_Series_list_Calculate2(str8, num5);
                                                        if (num6 > 0)
                                                        {
                                                            num11 -= num6;
                                                            this.objcard.Card_CardProductTiming_SetChargePrice(str8, tuple.Item3, 0);
                                                        }
                                                        if (num8 >= num11)
                                                        {
                                                            int num12 = this.objcard.Card_UpdatePriceAndBonus_PlayDetails2(str8, num8 - num11, num9, Main_ID_GameCenter, str6, num5, num8 - num11, num9, IsPersonnel, num1, ID_Swiper, ID_Play_Type, num5, 0, Pay_GiftPortion, empty);
                                                            str2 = str2 + " -L1 -" + (object)num12;
                                                        }
                                                        else
                                                        {
                                                            int Pay_BonusPortion = num11 - num8;
                                                            int num12 = num9 - Pay_BonusPortion;
                                                            int num13 = this.objcard.Card_UpdatePriceAndBonus_PlayDetails2(str8, 0, num12, Main_ID_GameCenter, str6, num5, 0, num12, IsPersonnel, num1, ID_Swiper, ID_Play_Type, num5 - Pay_BonusPortion, Pay_BonusPortion, Pay_GiftPortion, empty);
                                                            str2 = str2 + " -L1 -" + (object)num13;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    string str10 = str7;
                                                    MainClass objMain1 = this.objMain;
                                                    num7 = num8 + num9 + num6;
                                                    string str11 = num7.ToString();
                                                    string str12 = objMain1.comma(str11);
                                                    str2 = "[" + str10 + "]AT+NRC=" + str12;
                                                    string str13 = str7;
                                                    MainClass objMain2 = this.objMain;
                                                    num7 = num8 + num9 + num6;
                                                    string str14 = num7.ToString();
                                                    string str15 = objMain2.comma(str14);
                                                    str1 = "[" + str13 + "]AT+NRC=" + str15;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        str2 = "[" + str7 + "]AT+DISABLE";
                                        str1 = "[" + str7 + "]AT+DISABLE";
                                    }
                                }
                                else
                                {
                                    str2 = "[" + str7 + "]AT+DISABLE";
                                    str1 = "[" + str7 + "]AT+DISABLE";
                                }
                            }
                            else
                            {
                                str2 = "[" + str7 + "]AT+INVALID";
                                str1 = "[" + str7 + "]AT+INVALID";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.objMain.ErrorLog(ex);
                    this.objMain.ErrorLogTemp("error Process Main CID :exp= " + ex.Message + ",ReciveText=" + ReciveText + ",LineCode=" + str27);
                    return "";
                }
                try
                {
                    if (ReciveText.Contains("+P="))
                    {
                        string str5 = ReciveText.Substring(1, 12);
                        string str6 = str5.Substring(0, 2) + str5.Substring(3, 2) + str5.Substring(6, 2) + str5.Substring(9, 2);
                        string str7 = this.MacAndTimeStamp_Create(str6);
                        string MacCode = ReciveText.Split('=')[1].Replace("\r", "").Split(',')[0];
                        DataTable addressByChargeRate = this.objSwiper.Swiper_GetByMacAddressByChargeRate(str6);
                        int num1 = 0;
                        if (addressByChargeRate.Rows.Count > 0)
                        {
                            try
                            {
                                str3 = addressByChargeRate.Rows[0]["Title"].ToString() + ":" + (object)num1 + ":";
                                str4 = MacCode;
                            }
                            catch
                            {
                            }
                        }
                        if (chbDecreasePriceInLevel2)
                        {
                            str1 = "[" + str7 + "]AT+ok";
                            str2 = "[" + str7 + "]AT+ok";
                            int ID_Play_Type = 1;
                            if (addressByChargeRate.Rows.Count > 0)
                            {
                                try
                                {
                                    str3 = addressByChargeRate.Rows[0]["Title"].ToString() + ":" + (object)num1 + ":";
                                }
                                catch
                                {
                                }
                                int num2 = int.Parse(addressByChargeRate.Rows[0]["ID_Games"].ToString());
                                int ID_Swiper = int.Parse(addressByChargeRate.Rows[0]["ID"].ToString());
                                int.Parse(addressByChargeRate.Rows[0]["ID_Games_Class"].ToString() == "" ? "-100" : addressByChargeRate.Rows[0]["ID_Games_Class"].ToString());
                                DataTable byMacAddrress = this.objcard.Card_GetByMacAddrress(MacCode);
                                if (byMacAddrress.Rows.Count > 0)
                                {
                                    Guid empty = Guid.Empty;
                                    int num3 = int.Parse(byMacAddrress.Rows[0]["ID_Card_Status"].ToString());
                                    int num4 = int.Parse(byMacAddrress.Rows[0]["ID_Card_Series"].ToString());
                                    bool flag1 = false;
                                    try
                                    {
                                        flag1 = bool.Parse(byMacAddrress.Rows[0]["IsNonTicket"].ToString());
                                    }
                                    catch
                                    {
                                    }
                                    bool.Parse(byMacAddrress.Rows[0]["AllowRegistration"].ToString());
                                    int num5 = 0;
                                    try
                                    {
                                        num5 = int.Parse(byMacAddrress.Rows[0]["ID_Card_Promotional"].ToString());
                                    }
                                    catch
                                    {
                                    }
                                    string str8 = byMacAddrress.Rows[0]["Card_GUID"].ToString();
                                    string str9 = byMacAddrress.Rows[0]["ID_Club_Member_Type"].ToString();
                                    bool flag2 = !(str9 == "") && !(str9 == "0");
                                    int num6;
                                    if (MainClass.key_Value_List.Select("KeyName ='Enable_Charge_Rate'")[0]["Value"].ToString().ToLower() == "true")
                                    {
                                        num6 = num3 != 2 ? int.Parse(addressByChargeRate.Rows[0]["PriceAdi"].ToString()) : int.Parse(addressByChargeRate.Rows[0]["PriceVije"].ToString());
                                        if (flag2)
                                            num6 = int.Parse(addressByChargeRate.Rows[0]["PriceVije"].ToString());
                                    }
                                    else
                                    {
                                        num6 = num3 != 2 ? int.Parse(addressByChargeRate.Rows[0]["Price1"].ToString()) : int.Parse(addressByChargeRate.Rows[0]["Price2"].ToString());
                                        if (flag2)
                                            num6 = int.Parse(addressByChargeRate.Rows[0]["Price2"].ToString());
                                    }
                                    int IsPersonnel = 0;
                                    if (num4 == 3)
                                    {
                                        ID_Play_Type = 4;
                                        IsPersonnel = 1;
                                    }
                                    bool flag3 = false;
                                    int num7 = 0;
                                    Tuple<bool, int, int, int, int, bool, string> tuple = this.objcard.Card_CardProductTiming_Status(str8, num2);
                                    if (tuple.Item1)
                                    {
                                        if (tuple.Item6)
                                        {
                                            int num8 = this.objcard.Card_Play_Details_Insert(str8, 0, 0, Main_ID_GameCenter, str6, num6, 0, 0, IsPersonnel, num2, ID_Swiper, ID_Play_Type, 0, 0, 0, empty);
                                            str2 = str2 + " -L2 -" + (object)num8;
                                            flag3 = true;
                                            ID_Play_Type = 10;
                                        }
                                        else if (tuple.Item5 > 0)
                                        {
                                            flag3 = true;
                                            ID_Play_Type = 9;
                                            int num8 = this.objcard.Card_Play_Details_Insert(str8, 0, 0, Main_ID_GameCenter, str6, num6, 0, 0, IsPersonnel, num2, ID_Swiper, ID_Play_Type, 0, 0, 0, empty);
                                            str2 = str2 + " -L2 -" + (object)num8;
                                            this.objcard.Card_CardProductTiming_SetFreeGame(str8, tuple.Item3, tuple.Item5 - 1);
                                        }
                                        else if (tuple.Item4 > 0)
                                        {
                                            if (tuple.Item2 >= num6)
                                            {
                                                flag3 = true;
                                                ID_Play_Type = 11;
                                                int num8 = this.objcard.Card_Play_Details_Insert(str8, 0, 0, Main_ID_GameCenter, str6, num6, 0, 0, IsPersonnel, num2, ID_Swiper, ID_Play_Type, 0, 0, 0, empty);
                                                str2 = str2 + " -L2 -" + (object)num8;
                                                this.objcard.Card_CardProductTiming_SetChargePrice(str8, tuple.Item3, tuple.Item2 - num6);
                                            }
                                            else
                                            {
                                                num7 = tuple.Item2;
                                                flag3 = false;
                                            }
                                        }
                                        else
                                        {
                                            ID_Play_Type = 5;
                                            int num8 = this.objcard.Card_Play_Details_Insert(str8, 0, 0, Main_ID_GameCenter, str6, num6, 0, 0, IsPersonnel, num2, ID_Swiper, ID_Play_Type, 0, 0, 0, empty);
                                            str2 = str2 + " -L2 -" + (object)num8;
                                            flag3 = true;
                                        }
                                    }
                                    if (!flag3)
                                    {
                                        int.Parse(byMacAddrress.Rows[0]["ID"].ToString());
                                        int num8 = int.Parse(byMacAddrress.Rows[0]["CashPrice"].ToString());
                                        int num9 = int.Parse(byMacAddrress.Rows[0]["BonusPrice"].ToString());
                                        DataTable dataTable = this.objPattern.Gift_series_List_ActiveByCard_GUID(str8);
                                        int Pay_GiftPortion = 0;
                                        bool flag4 = false;
                                        bool flag5 = false;
                                        if (dataTable.Rows.Count > 0)
                                        {
                                            for (int index = 0; index < dataTable.Rows.Count; ++index)
                                            {
                                                if (int.Parse(dataTable.Rows[index]["Real_Charge"].ToString()) > 0)
                                                    empty = Guid.Parse(dataTable.Rows[index]["ID"].ToString());
                                                Pay_GiftPortion += int.Parse(dataTable.Rows[index]["Real_Charge"].ToString());
                                                int num10 = int.Parse(dataTable.Rows[index]["Real_FreegameCount"].ToString());
                                                int num11 = int.Parse(dataTable.Rows[index]["Real_FreeDailyGamesCount"].ToString());
                                                if (num11 > 0 && ("," + dataTable.Rows[index]["FreeDailyGames"].ToString() + ",").Contains("," + (object)num2 + ",") && this.objcard.Card_Play_Details_Get_Today(str8, dataTable.Rows[index]["FreeDailyGames"].ToString()).Rows.Count < num11)
                                                {
                                                    empty = Guid.Parse(dataTable.Rows[index]["ID"].ToString());
                                                    flag5 = true;
                                                    break;
                                                }
                                                if (num10 > 0 && ("," + dataTable.Rows[index]["FreeGames"].ToString() + ",").Contains("," + (object)num2 + ","))
                                                {
                                                    empty = Guid.Parse(dataTable.Rows[index]["ID"].ToString());
                                                    flag4 = true;
                                                    this.objPattern.Gift_Pattern_series_List_Update(dataTable.Rows[index]["ID"].ToString(), num10 - 1);
                                                    break;
                                                }
                                            }
                                        }
                                        if (flag4)
                                        {
                                            ID_Play_Type = 2;
                                            num6 = 0;
                                        }
                                        if (flag5)
                                        {
                                            ID_Play_Type = 3;
                                            num6 = 0;
                                        }
                                        if (num5 > 0)
                                            ID_Play_Type = 7;
                                        if (flag5 | flag4)
                                        {
                                            int num10 = this.objcard.Card_UpdatePriceAndBonus_PlayDetails2(str8, num8, num9, Main_ID_GameCenter, str6, num6, num8, num9, IsPersonnel, num2, ID_Swiper, ID_Play_Type, 0, 0, 0, empty);
                                            str2 = str2 + " -L2 -" + (object)num10;
                                        }
                                        else
                                        {
                                            if (Pay_GiftPortion > 0)
                                                ID_Play_Type = 8;
                                            if (num8 + num9 + Pay_GiftPortion + num7 >= num6)
                                            {
                                                int num10 = this.objPattern.Gift_Pattern_Series_list_Calculate2(str8, num6);
                                                if (num7 > 0)
                                                {
                                                    num10 -= num7;
                                                    this.objcard.Card_CardProductTiming_SetChargePrice(str8, tuple.Item3, 0);
                                                }
                                                if (num8 >= num10)
                                                {
                                                    int num11 = this.objcard.Card_UpdatePriceAndBonus_PlayDetails2(str8, num8 - num10, num9, Main_ID_GameCenter, str6, num6, num8 - num10, num9, IsPersonnel, num2, ID_Swiper, ID_Play_Type, num6, 0, Pay_GiftPortion, empty);
                                                    str2 = str2 + " -L2 -" + (object)num11;
                                                }
                                                else
                                                {
                                                    int Pay_BonusPortion = num10 - num8;
                                                    int num11 = num9 - Pay_BonusPortion;
                                                    int num12 = this.objcard.Card_UpdatePriceAndBonus_PlayDetails2(str8, 0, num11, Main_ID_GameCenter, str6, num6, 0, num11, IsPersonnel, num2, ID_Swiper, ID_Play_Type, num6 - Pay_BonusPortion, Pay_BonusPortion, Pay_GiftPortion, empty);
                                                    str2 = str2 + " -L2 -" + (object)num12;
                                                }
                                            }
                                            else
                                            {
                                                string str10 = str7;
                                                MainClass objMain1 = this.objMain;
                                                int num10 = num8 + num9 + num7;
                                                string str11 = num10.ToString();
                                                string str12 = objMain1.comma(str11);
                                                str2 = "[" + str10 + "]AT+NRC=" + str12;
                                                string str13 = str7;
                                                MainClass objMain2 = this.objMain;
                                                num10 = num8 + num9 + num7;
                                                string str14 = num10.ToString();
                                                string str15 = objMain2.comma(str14);
                                                str1 = "[" + str13 + "]AT+NRC=" + str15;
                                            }
                                        }
                                    }
                                }
                                else
                                    this.objMain.ErrorLogTemp("Card not Find :" + MacCode);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.objMain.ErrorLog(ex);
                    this.objMain.ErrorLogTemp("error Process Main +P :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                    return "";
                }
                try
                {
                    if (ReciveText.Contains("+T="))
                    {
                        bool flag1 = false;
                        string str5 = ReciveText.Substring(1, 12);
                        string str6 = str5.Substring(0, 2) + str5.Substring(3, 2) + str5.Substring(6, 2) + str5.Substring(9, 2);
                        string str7 = ReciveText.Split('=')[1].Replace("\r", "").Split(',')[0];
                        if (str7.ToLower() == "ffffffff")
                        {
                            DataSet macAddrressFfffff = this.objcard.Card_GetByMacAddrressFFFFFF(str6);
                            DataTable table1 = macAddrressFfffff.Tables[0];
                            DataTable table2 = macAddrressFfffff.Tables[1];
                            if (table1.Rows.Count > 0 && table2.Rows.Count > 0)
                            {
                                str6 = table1.Rows[0]["MacAddress2"].ToString().ToLower();
                                str7 = table2.Rows[0]["MacCode"].ToString();
                            }
                        }
                        string str8 = this.MacAndTimeStamp_Create(str6);
                        DataTable addressByChargeRate = this.objSwiper.Swiper_GetByMacAddressByChargeRate(str6.ToUpper());
                        int ID_Games = -1;
                        int ID_Swiper = -1;
                        string str9 = "1";
                        if (addressByChargeRate.Rows.Count > 0)
                        {
                            try
                            {
                                str3 = addressByChargeRate.Rows[0]["Title"].ToString();
                                ID_Games = int.Parse(addressByChargeRate.Rows[0]["ID_Games"].ToString());
                                ID_Swiper = int.Parse(addressByChargeRate.Rows[0]["ID"].ToString());
                                str9 = addressByChargeRate.Rows[0]["Version"].ToString();
                            }
                            catch
                            {
                            }
                        }
                        if (ReciveText.Contains("SendOk"))
                        {
                            str1 = "[" + str8 + "]AT+TRC=0";
                            str2 = "[" + str8 + "]AT+TRC=0";
                        }
                        else
                        {
                            DataTable dataTable = new DataTable();
                            DataTable byMacAddrress = this.objcard.Card_GetByMacAddrress(str7);
                            int ID_Card_Play_Details = -1;
                            int IsPersonnel = 0;
                            string str10 = ReciveText.Split('=')[1].ToString();
                            int Count = 0;
                            string str11 = "E";
                            try
                            {
                                Count = int.Parse(str10.Split(',')[1].ToString());
                                str11 = str10.Split(',')[2].ToString();
                            }
                            catch
                            {
                            }
                            if (byMacAddrress.Rows.Count > 0)
                            {
                                bool flag2 = bool.Parse(byMacAddrress.Rows[0]["IsNonTicket"].ToString());
                                bool.Parse(byMacAddrress.Rows[0]["AllowRegistration"].ToString());
                                if (flag2)
                                {
                                    str1 = "[" + str8 + "]AT+TRC=0";
                                    str2 = "[" + str8 + "]AT+TRC=0";
                                }
                                else
                                {
                                    string Card_GUID = byMacAddrress.Rows[0]["Card_GUID"].ToString();
                                    int OldCount = int.Parse(byMacAddrress.Rows[0]["Etickets"].ToString());
                                    if (int.Parse(byMacAddrress.Rows[0]["ID_Card_Series"].ToString()) == 3)
                                        IsPersonnel = 1;
                                    DataTable byCardGuid = this.objcard.Card_Play_Details_GetByCardGUID(str6, Card_GUID, Main_ID_GameCenter);
                                    if (byCardGuid.Rows.Count > 0)
                                        ID_Card_Play_Details = int.Parse(byCardGuid.Rows[0]["ID"].ToString());
                                    if (!flag1)
                                    {
                                        bool flag3 = false;
                                        if (ID_Card_Play_Details > 0)
                                        {
                                            if (str11.ToUpper() == "E")
                                            {
                                                int Status = !flag3 ? 1 : 3;
                                                int ID_Games_Ticket = this.objMain.Games_Ticket_Insert(Main_ID_GameCenter, str6, str7, Count, Status, ID_Card_Play_Details, Card_GUID, IsPersonnel, ID_Games, ID_Swiper);
                                                if (str9 == "3")
                                                {
                                                    if (IsPersonnel == 0)
                                                    {
                                                        if (this.objcard.Card_Ticket_History_insert(Main_ID_GameCenter, -1, Count, Card_GUID, OldCount, 6, ID_Games_Ticket) > 0)
                                                        {
                                                            if (this.objcard.Card_SetEtickets(Card_GUID, Count + OldCount) > 0)
                                                            {
                                                                int num = Count + OldCount;
                                                                str1 = "[" + str8 + "]AT+TRC=" + num.ToString();
                                                                str2 = "[" + str8 + "]AT+TRC=" + num.ToString();
                                                            }
                                                            else
                                                            {
                                                                str1 = "[" + str8 + "]AT+TRC=0";
                                                                str2 = "[" + str8 + "]AT+TRC=0";
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        str1 = "[" + str8 + "]AT+TRC=0";
                                                        str2 = "[" + str8 + "]AT+TRC=0";
                                                    }
                                                }
                                            }
                                            if (str11.ToUpper() == "O")
                                            {
                                                int Status = !flag3 ? 2 : 3;
                                                int ID_Games_Ticket = this.objMain.Games_Ticket_Insert(Main_ID_GameCenter, str6, str7, Count, Status, ID_Card_Play_Details, Card_GUID, IsPersonnel, ID_Games, ID_Swiper);
                                                if (IsPersonnel == 0)
                                                {
                                                    if (str9 == "2")
                                                    {
                                                        str1 = "[" + str8 + "]AT+ok";
                                                        str2 = "[" + str8 + "]AT+ok";
                                                    }
                                                    else if (str9 == "3" && this.objcard.Card_Ticket_History_insert(Main_ID_GameCenter, -1, Count, Card_GUID, OldCount, 6, ID_Games_Ticket) > 0)
                                                    {
                                                        if (this.objcard.Card_SetEtickets(Card_GUID, Count + OldCount) > 0)
                                                        {
                                                            int num = Count + OldCount;
                                                            str1 = "[" + str8 + "]AT+TRC=" + num.ToString();
                                                            str2 = "[" + str8 + "]AT+TRC=" + num.ToString();
                                                        }
                                                        else
                                                        {
                                                            str1 = "[" + str8 + "]AT+TRC=0";
                                                            str2 = "[" + str8 + "]AT+TRC=0";
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    str1 = "[" + str8 + "]AT+TRC=0";
                                                    str2 = "[" + str8 + "]AT+TRC=0";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            this.objMain.Games_Ticket_Insert(Main_ID_GameCenter, str6, str7, Count, 3, ID_Card_Play_Details, Card_GUID, IsPersonnel, ID_Games, ID_Swiper);
                                            str1 = "[" + str8 + "]AT+TRC=0";
                                            str2 = "[" + str8 + "]AT+TRC=0";
                                        }
                                    }
                                    else
                                    {
                                        str1 = "[" + str8 + "]AT+TRC=" + OldCount.ToString();
                                        str2 = "[" + str8 + "]AT+TRC=" + OldCount.ToString();
                                    }
                                }
                            }
                            else
                            {
                                str1 = "[" + str8 + "]AT+TRC=0";
                                str2 = "[" + str8 + "]AT+TRC=0";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.objMain.ErrorLog(ex);
                    this.objMain.ErrorLogTemp("error Process Main +T :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                    return "";
                }
                try
                {
                    if (ReciveText.Contains("Error_conf"))
                    {
                        string str5 = ReciveText.Substring(1, 12);
                        string str6 = str5.Substring(0, 2) + str5.Substring(3, 2) + str5.Substring(6, 2) + str5.Substring(9, 2);
                        string timeStamp = TimeStamp;
                        string MacAddress = str6;
                        string str7 = this.MacAndTimeStamp_Create(MacAddress);
                        DataTable addressByChargeRate = this.objSwiper.Swiper_GetByMacAddressByChargeRate(MacAddress.ToUpper());
                        if (addressByChargeRate.Rows.Count > 0)
                        {
                            try
                            {
                                str3 = addressByChargeRate.Rows[0]["Title"].ToString();
                            }
                            catch
                            {
                            }
                            str1 = "[" + str7 + "]AT+CFG1=" + timeStamp;
                            str2 = "[" + str7 + "]AT+CFG1=" + timeStamp;
                            this.objSwiper.Swiper_Update_Config_State(0, MacAddress);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.objMain.ErrorLog(ex);
                    this.objMain.ErrorLogTemp("error Process Main Error_config :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                    return "";
                }
                try
                {
                    if (ReciveText.Contains("HID"))
                    {
                        string str5 = ReciveText.Substring(1, 12);
                        string str6 = str5.Substring(0, 2) + str5.Substring(3, 2) + str5.Substring(6, 2) + str5.Substring(9, 2);
                        string timeStamp = TimeStamp;
                        string MacAddress = str6;
                        string str7 = this.MacAndTimeStamp_Create(MacAddress);
                        DataTable addressByChargeRate = this.objSwiper.Swiper_GetByMacAddressByChargeRate(MacAddress);
                        if (addressByChargeRate.Rows.Count > 0)
                        {
                            try
                            {
                                str3 = addressByChargeRate.Rows[0]["Title"].ToString();
                            }
                            catch
                            {
                            }
                            int.Parse(addressByChargeRate.Rows[0]["ID"].ToString());
                            string upper = ReciveText.Split('=')[1].Replace("\r", "").Split(',')[0].ToUpper();
                            str4 = upper;
                            DataTable byMacAddrress = this.objcard.Card_GetByMacAddrress(upper);
                            if (byMacAddrress.Rows.Count > 0)
                            {
                                bool boolean = Convert.ToBoolean(byMacAddrress.Rows[0]["IsNonPlayGames"].ToString().ToLower() == "" ? "false" : byMacAddrress.Rows[0]["IsNonPlayGames"].ToString());
                                if (Convert.ToBoolean(byMacAddrress.Rows[0]["IsActive"].ToString()))
                                {
                                    if (!boolean)
                                    {
                                        int num1 = int.Parse(byMacAddrress.Rows[0]["ID_Card_Series"].ToString());
                                        string Card_GUID = byMacAddrress.Rows[0]["Card_GUID"].ToString();
                                        int num2 = 0;
                                        if (num1 == 3)
                                            num2 = 1;
                                        if (this.objcard.Card_CardProductTiming_Get(Card_GUID).Rows.Count <= 0)
                                        {
                                            int num3 = int.Parse(byMacAddrress.Rows[0]["CashPrice"].ToString());
                                            int num4 = int.Parse(byMacAddrress.Rows[0]["BonusPrice"].ToString());
                                            string str8 = "0";
                                            DataTable dataTable = new DataTable();
                                            if (byMacAddrress.Rows.Count > 0)
                                            {
                                                str8 = byMacAddrress.Rows[0]["Etickets"].ToString();
                                                dataTable = this.objcard.Card_Play_Details_GetByCardGUID(byMacAddrress.Rows[0]["Card_GUID"].ToString());
                                            }
                                            string str9 = "";
                                            string str10 = "";
                                            string str11 = "";
                                            string str12 = "";
                                            string str13 = "";
                                            string str14 = "";
                                            string str15 = "";
                                            string str16 = "";
                                            string str17 = "";
                                            string str18 = "";
                                            string str19 = "";
                                            string str20 = "";
                                            string str21 = "";
                                            string str22 = "";
                                            string str23 = "";
                                            string str24 = "";
                                            string str25 = "";
                                            string str26 = "";
                                            string str28 = "";
                                            string str29 = "";
                                            string str30 = "";
                                            string str31 = "";
                                            string str32 = "";
                                            string str33 = "";
                                            string str34 = "";
                                            if (dataTable.Rows.Count > 0)
                                            {
                                                try
                                                {
                                                    string str35 = dataTable.Rows[0]["GameTitle"].ToString().Replace(" ", "");
                                                    str9 = str35.Length > 9 ? str35.Substring(0, 9) : str35;
                                                    str10 = dataTable.Rows[0]["Date"].ToString().Substring(2, 10);
                                                    DateTime dateTime = Convert.ToDateTime(dataTable.Rows[0]["MiladiDate"].ToString());
                                                    str11 = dateTime.Hour.ToString() + ":" + (object)dateTime.Minute;
                                                    str12 = this.objMain.comma(dataTable.Rows[0]["Price"].ToString());
                                                    str13 = this.objMain.comma(dataTable.Rows[0]["PriceKol"].ToString());
                                                }
                                                catch
                                                {
                                                }
                                            }
                                            if (dataTable.Rows.Count > 1)
                                            {
                                                try
                                                {
                                                    string str35 = dataTable.Rows[1]["GameTitle"].ToString().Replace(" ", "");
                                                    str14 = str35.Length > 9 ? str35.Substring(0, 9) : str35;
                                                    str15 = dataTable.Rows[1]["Date"].ToString().Substring(2, 10);
                                                    DateTime dateTime = Convert.ToDateTime(dataTable.Rows[1]["MiladiDate"].ToString());
                                                    str16 = dateTime.Hour.ToString() + ":" + (object)dateTime.Minute;
                                                    str17 = this.objMain.comma(dataTable.Rows[1]["Price"].ToString());
                                                    str18 = this.objMain.comma(dataTable.Rows[1]["PriceKol"].ToString());
                                                }
                                                catch
                                                {
                                                }
                                            }
                                            if (dataTable.Rows.Count > 2)
                                            {
                                                try
                                                {
                                                    string str35 = dataTable.Rows[2]["GameTitle"].ToString().Replace(" ", "");
                                                    str19 = str35.Length > 9 ? str35.Substring(0, 9) : str35;
                                                    str20 = dataTable.Rows[2]["Date"].ToString().Substring(2, 10);
                                                    DateTime dateTime = Convert.ToDateTime(dataTable.Rows[2]["MiladiDate"].ToString());
                                                    str21 = dateTime.Hour.ToString() + ":" + (object)dateTime.Minute;
                                                    str22 = this.objMain.comma(dataTable.Rows[2]["Price"].ToString());
                                                    str23 = this.objMain.comma(dataTable.Rows[2]["PriceKol"].ToString());
                                                }
                                                catch
                                                {
                                                }
                                            }
                                            if (dataTable.Rows.Count > 3)
                                            {
                                                try
                                                {
                                                    string str35 = dataTable.Rows[3]["GameTitle"].ToString().Replace(" ", "");
                                                    str24 = str35.Length > 9 ? str35.Substring(0, 9) : str35;
                                                    str25 = dataTable.Rows[3]["Date"].ToString().Substring(2, 10);
                                                    DateTime dateTime = Convert.ToDateTime(dataTable.Rows[3]["MiladiDate"].ToString());
                                                    str26 = dateTime.Hour.ToString() + ":" + (object)dateTime.Minute;
                                                    str28 = this.objMain.comma(dataTable.Rows[3]["Price"].ToString());
                                                    str29 = this.objMain.comma(dataTable.Rows[3]["PriceKol"].ToString());
                                                }
                                                catch
                                                {
                                                }
                                            }
                                            if (dataTable.Rows.Count > 4)
                                            {
                                                try
                                                {
                                                    string str35 = dataTable.Rows[4]["GameTitle"].ToString().Replace(" ", "");
                                                    str30 = str35.Length > 9 ? str35.Substring(0, 9) : str35;
                                                    str31 = dataTable.Rows[4]["Date"].ToString().Substring(2, 10);
                                                    DateTime dateTime = Convert.ToDateTime(dataTable.Rows[4]["MiladiDate"].ToString());
                                                    str32 = dateTime.Hour.ToString() + ":" + (object)dateTime.Minute;
                                                    str33 = this.objMain.comma(dataTable.Rows[4]["Price"].ToString());
                                                    str34 = this.objMain.comma(dataTable.Rows[4]["PriceKol"].ToString());
                                                }
                                                catch
                                                {
                                                }
                                            }
                                            string str36 = "HIDShow$" + str7 + "$" + (num3 + num4).ToString() + "$" + str8 + "$" + str9 + "$" + str10 + "$" + str11 + "$" + str12 + "$" + str13 + "$" + str14 + "$" + str15 + "$" + str16 + "$" + str17 + "$" + str18 + "$" + str19 + "$" + str20 + "$" + str21 + "$" + str22 + "$" + str23 + "$" + str24 + "$" + str25 + "$" + str26 + "$" + str28 + "$" + str29 + "$" + str30 + "$" + str31 + "$" + str32 + "$" + str33 + "$" + str34;
                                            str2 = str36;
                                            str1 = str36;
                                        }
                                    }
                                    else
                                    {
                                        str2 = "[" + str7 + "]AT+DISABLE";
                                        str1 = "[" + str7 + "]AT+DISABLE";
                                    }
                                }
                                else
                                {
                                    str2 = "[" + str7 + "]AT+DISABLE";
                                    str1 = "[" + str7 + "]AT+DISABLE";
                                }
                            }
                            else
                            {
                                str2 = "[" + str7 + "]AT+INVALID";
                                str1 = "[" + str7 + "]AT+INVALID";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.objMain.ErrorLog(ex);
                    this.objMain.ErrorLogTemp("error Process Main HID :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                    return "";
                }
                try
                {
                    if (ReciveText.Contains("OK_"))
                    {
                        string str5 = ReciveText.Substring(1, 12);
                        string MacAddress = str5.Substring(0, 2) + str5.Substring(3, 2) + str5.Substring(6, 2) + str5.Substring(9, 2);
                        string str6 = this.MacAndTimeStamp_Create(MacAddress);
                        if (this.objSwiper.Swiper_GetByMacAddressByChargeRate(MacAddress.ToUpper()).Rows.Count > 0)
                        {
                            string upper = ReciveText.Split('=')[1].Replace("\r", "").Split(',')[0].ToUpper();
                            str4 = upper;
                            string str7 = "0";
                            DataTable byMacAddrress = this.objcard.Card_GetByMacAddrress(upper);
                            DataTable dataTable = new DataTable();
                            if (byMacAddrress.Rows.Count > 0)
                            {
                                str7 = byMacAddrress.Rows[0]["Etickets"].ToString();
                                dataTable = this.objcard.Card_Play_Details_GetByCardGUID(byMacAddrress.Rows[0]["Card_GUID"].ToString());
                            }
                            string str8 = "";
                            string str9 = "";
                            string str10 = "";
                            string str11 = "";
                            string str12 = "";
                            string str13 = "";
                            string str14 = "";
                            string str15 = "";
                            string str16 = "";
                            string str17 = "";
                            string str18 = "";
                            string str19 = "";
                            string str20 = "";
                            string str21 = "";
                            string str22 = "";
                            string str23 = "";
                            string str24 = "";
                            string str25 = "";
                            string str26 = "";
                            string str28 = "";
                            if (dataTable.Rows.Count > 0)
                            {
                                string str29 = dataTable.Rows[0]["GameTitle"].ToString().Replace(" ", "");
                                str8 = str29.Length > 13 ? str29.Substring(0, 13) : str29;
                                str9 = dataTable.Rows[0]["Date"].ToString().Substring(0, 10);
                                str10 = dataTable.Rows[0]["Price"].ToString();
                                str11 = dataTable.Rows[0]["PriceKol"].ToString();
                            }
                            if (dataTable.Rows.Count > 1)
                            {
                                string str29 = dataTable.Rows[1]["GameTitle"].ToString().Replace(" ", "");
                                str12 = str29.Length > 13 ? str29.Substring(0, 13) : str29;
                                str13 = dataTable.Rows[1]["Date"].ToString().Substring(0, 10);
                                str14 = dataTable.Rows[1]["Price"].ToString();
                                str15 = dataTable.Rows[1]["PriceKol"].ToString();
                            }
                            if (dataTable.Rows.Count > 2)
                            {
                                string str29 = dataTable.Rows[2]["GameTitle"].ToString().Replace(" ", "");
                                str16 = str29.Length > 13 ? str29.Substring(0, 13) : str29;
                                str17 = dataTable.Rows[2]["Date"].ToString().Substring(0, 10);
                                str18 = dataTable.Rows[2]["Price"].ToString();
                                str19 = dataTable.Rows[2]["PriceKol"].ToString();
                            }
                            if (dataTable.Rows.Count > 3)
                            {
                                string str29 = dataTable.Rows[3]["GameTitle"].ToString().Replace(" ", "");
                                str20 = str29.Length > 13 ? str29.Substring(0, 13) : str29;
                                str21 = dataTable.Rows[3]["Date"].ToString().Substring(0, 10);
                                str22 = dataTable.Rows[3]["Price"].ToString();
                                str23 = dataTable.Rows[3]["PriceKol"].ToString();
                            }
                            if (dataTable.Rows.Count > 4)
                            {
                                string str29 = dataTable.Rows[4]["GameTitle"].ToString().Replace(" ", "");
                                str24 = str29.Length > 13 ? str29.Substring(0, 13) : str29;
                                str25 = dataTable.Rows[4]["Date"].ToString().Substring(0, 10);
                                str26 = dataTable.Rows[4]["Price"].ToString();
                                str28 = dataTable.Rows[4]["PriceKol"].ToString();
                            }
                            if (ReciveText.Contains("OK_TP"))
                            {
                                str1 = "[" + str6 + "]AT+TTIK=" + str7;
                                str2 = "[" + str6 + "]AT+TTIK=" + str7;
                            }
                            if (ReciveText.Contains("OK_TT") && str8.Length > 0)
                            {
                                str1 = "[" + str6 + "]A1=" + str8;
                                str2 = "[" + str6 + "]A1=" + str8;
                            }
                            if (ReciveText.Contains("OK_A1"))
                            {
                                str1 = "[" + str6 + "]A2=" + str9;
                                str2 = "[" + str6 + "]A2=" + str9;
                            }
                            if (ReciveText.Contains("OK_A2"))
                            {
                                str1 = "[" + str6 + "]A3=" + str10;
                                str2 = "[" + str6 + "]A3=" + str10;
                            }
                            if (ReciveText.Contains("OK_A3"))
                            {
                                str1 = "[" + str6 + "]A4=" + str11;
                                str2 = "[" + str6 + "]A4=" + str11;
                            }
                            if (ReciveText.Contains("OK_A4") && str12.Length > 0)
                            {
                                str1 = "[" + str6 + "]B1=" + str12;
                                str2 = "[" + str6 + "]B1=" + str12;
                            }
                            if (ReciveText.Contains("OK_B1"))
                            {
                                str1 = "[" + str6 + "]B2=" + str13;
                                str2 = "[" + str6 + "]B2=" + str13;
                            }
                            if (ReciveText.Contains("OK_B2"))
                            {
                                str1 = "[" + str6 + "]B3=" + str14;
                                str2 = "[" + str6 + "]B3=" + str14;
                            }
                            if (ReciveText.Contains("OK_B3"))
                            {
                                str1 = "[" + str6 + "]B4=" + str15;
                                str2 = "[" + str6 + "]B4=" + str15;
                            }
                            if (ReciveText.Contains("OK_B4") && str16.Length > 0)
                            {
                                str1 = "[" + str6 + "]C1=" + str16;
                                str2 = "[" + str6 + "]C1=" + str16;
                            }
                            if (ReciveText.Contains("OK_C1"))
                            {
                                str1 = "[" + str6 + "]C2=" + str17;
                                str2 = "[" + str6 + "]C2=" + str17;
                            }
                            if (ReciveText.Contains("OK_C2"))
                            {
                                str1 = "[" + str6 + "]C3=" + str18;
                                str2 = "[" + str6 + "]C3=" + str18;
                            }
                            if (ReciveText.Contains("OK_C3"))
                            {
                                str1 = "[" + str6 + "]C4=" + str19;
                                str2 = "[" + str6 + "]C4=" + str19;
                            }
                            if (ReciveText.Contains("OK_C4") && str20.Length > 0)
                            {
                                str1 = "[" + str6 + "]D1=" + str20;
                                str2 = "[" + str6 + "]D1=" + str20;
                            }
                            if (ReciveText.Contains("OK_D1"))
                            {
                                str1 = "[" + str6 + "]D2=" + str21;
                                str2 = "[" + str6 + "]D2=" + str21;
                            }
                            if (ReciveText.Contains("OK_D2"))
                            {
                                str1 = "[" + str6 + "]D3=" + str22;
                                str2 = "[" + str6 + "]D3=" + str22;
                            }
                            if (ReciveText.Contains("OK_D3"))
                            {
                                str1 = "[" + str6 + "]D4=" + str23;
                                str2 = "[" + str6 + "]D4=" + str23;
                            }
                            if (ReciveText.Contains("OK_D4") && str24.Length > 0)
                            {
                                str1 = "[" + str6 + "]E1=" + str24;
                                str2 = "[" + str6 + "]E1=" + str24;
                            }
                            if (ReciveText.Contains("OK_E1"))
                            {
                                str1 = "[" + str6 + "]E2=" + str25;
                                str2 = "[" + str6 + "]E2=" + str25;
                            }
                            if (ReciveText.Contains("OK_E2"))
                            {
                                str1 = "[" + str6 + "]E3=" + str26;
                                str2 = "[" + str6 + "]E3=" + str26;
                            }
                            if (ReciveText.Contains("OK_E3"))
                            {
                                str1 = "[" + str6 + "]E4=" + str28;
                                str2 = "[" + str6 + "]E4=" + str28;
                            }
                            Thread.Sleep(1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.objMain.ErrorLog(ex);
                    this.objMain.ErrorLogTemp("error Process Main OKCFG_HID :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                    return "";
                }
                return str1 + "!" + str2 + "!" + str3 + "!" + str4.ToUpper();
            }
            catch (Exception ex)
            {
                this.objMain.ErrorLog(ex);
                this.objMain.ErrorLogTemp("error Process Main :" + ex.Message);
                return "";
            }
        }

        public int Send_Main(string IpAp, string Command, TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] bytes = Encoding.ASCII.GetBytes(Command);
                stream.Write(bytes, 0, bytes.Length);
                return 1;
            }
            catch
            {
                return -1;
            }
        }

        private void Send_DisplayText(
          string SendData,
          string TcpIPName,
          string t_SwiperName,
          string t_CardmacAddress)
        {
            if (txtRecive.Length > 10000)
            {
                txtRecive = "";
                txtSend = "";
            }
            if (!chbShowAllSend)
            {
                if (SendData.Length <= 0 || txtSend.Contains(TcpIPName + "-" + SendData))
                    return;
                this.WriteToFile_SendRecive(TcpIPName + "-" + (object)DispStringSendTime.Hour + ":" + (object)DispStringSendTime.Minute + ":" + (object)DispStringSendTime.Second + ":" + (object)DispStringSendTime.Millisecond + "-" + SendData + "-" + t_SwiperName + "-" + t_CardmacAddress, 1, DispStringSendTime);
                txtSend = txtSend + TcpIPName + "-" + SendData + (object)DispStringSendTime.Hour + ":" + (object)DispStringSendTime.Minute + ":" + (object)DispStringSendTime.Second + ":" + (object)DispStringSendTime.Millisecond + "-" + t_SwiperName + t_CardmacAddress;
            }
            else if (SendData.Length > 0)
            {
                this.WriteToFile_SendRecive(TcpIPName + "-" + (object)DispStringSendTime.Hour + ":" + (object)DispStringSendTime.Minute + ":" + (object)DispStringSendTime.Second + ":" + (object)DispStringSendTime.Millisecond + "-" + SendData + "-" + t_SwiperName + "-" + t_CardmacAddress, 1, DispStringSendTime);
                txtSend = txtSend + TcpIPName + "-" + SendData + (object)DispStringSendTime.Hour + ":" + (object)DispStringSendTime.Minute + ":" + (object)DispStringSendTime.Second + ":" + (object)DispStringSendTime.Millisecond + "-" + t_SwiperName + t_CardmacAddress;
            }
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && this.components != null)
        //        this.components.Dispose();
        //    base.Dispose(disposing);
        //}

        // private void InitializeComponent()
        // {
        // this.components = (IContainer)new Container();
        // this.ServiceName = "Service1";
        // }

        public delegate void DelegateStandardPattern();
    }
}
