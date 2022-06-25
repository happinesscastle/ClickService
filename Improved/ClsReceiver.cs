using ClickServerService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Threading;
using System.Net;

namespace ClickServerService.Improved
{
    public class ClsReceiver
    {
        /// <summary>
        /// Global Variables
        /// </summary>
        ServerConfigView serverConfigView = new ServerConfigView();

        public TcpClient ap_Client;

        MainClass objMain = new MainClass();
        SwiperClass objSwiper = new SwiperClass();

        Thread receiveThread;

        bool _checkBoxShowAll = false;
        bool chbShowAllRecive = false;
        bool flagConnectToSQL = false;

        int MultiRun_AP_ID = 0;
        int Main_ID_GameCenter = 1;

        string DispStringRecive = "", DispStringSplit = "";
        string txtRecive = "";

        public ClsReceiver(int apID)
        {
            MultiRun_AP_ID = apID;

            AppLoadMain();
            //Thread.Sleep(10);
           
        }

        public void Start()
        {

            while (true)
            {
                //Task tsk = Task.Run(() => OnElapsedTime());
                OnElapsedTime();
                Thread.Sleep(3000);
                //  tsk.Wait(TimeSpan.FromSeconds(3));

            }
        }

        public void AppLoadMain()
        {// Copy 2
            try
            {
                flagConnectToSQL = false;

                //cblValidateReceivedData = 2;
                serverConfigView.ValidateReceivedData = 2;

                //chbAP1 = false;
                serverConfigView.AP_IsEnable = false;

                //txtAp1_IP = "0.0.0.0";
                //   serverConfigView.AP_IP = "0.0.0.0";

                //txtServerIp = "0.0.0.0";
                serverConfigView.ServerIP = "0.0.0.0";

                //cblRepeatConfig = 2;
                serverConfigView.RepeatConfig = 2;

                chbShowAllRecive = true;
                txtRecive = "";


                objMain.Decript_Connection_String();
                MainClass.key_Value_List = objMain.Key_Value_Get();
                objSwiper.Swiper_Update_Config_StateAll(0, objMain.ID_GameCenter_Local_Get());

                if (!objMain.licence_Check())
                {
                    WriteToFile(DateTime.Now.ToString() + ":1:Licence ERROR ");
                    //  timer.Enabled = false;
                    //  Timer_SendData.Enabled = false;
                    //Dispose();
                }
                else
                {
                    //   timer.Enabled = true;
                    //   Timer_SendData.Enabled = true;
                    objMain.LoadGameCenterID();
                    Main_ID_GameCenter = objMain.ID_GameCenter_Local_Get();

                    List<ServerConfigView> byGameCenter = objMain.ServerConfig_GetByGameCenterID(objMain.ID_GameCenter_Local_Get(), MultiRun_AP_ID);
                    if (byGameCenter.Any())
                    {
                        serverConfigView = byGameCenter.FirstOrDefault();
                        //txtServerIp = byGameCenter.Rows[0]["ServerIP"].ToString();
                        //chbAP1 = Convert.ToBoolean(byGameCenter.Rows[0]["AP_IsEnable"].ToString());
                        //Console.WriteLine($"chbAP{MultiRun_AP_ID} Fill : " + chbAP1.ToString());
                        //txtAp1_IP = byGameCenter.Rows[0]["AP_IP"].ToString();
                        //cblRepeatConfig = int.Parse(byGameCenter.Rows[0]["RepeatConfig"].ToString());
                        //cblValidateReceivedData = int.Parse(byGameCenter.Rows[0]["ValidateReceivedData"].ToString());
                        //chbDecreasePriceInLevel2 = Convert.ToBoolean(byGameCenter.Rows[0]["IsDecreasePriceInLevel2"].ToString());
                        //chbShowAllRecive = Convert.ToBoolean(byGameCenter.Rows[0]["IsShowAllRecive"].ToString());
                        //_checkBoxShowAll = Convert.ToBoolean(byGameCenter.Rows[0]["IsShowAllRecive"].ToString());
                        //chbShowAllSend = Convert.ToBoolean(byGameCenter.Rows[0]["IsShowAllSend"].ToString());
                        //WriteToFile("txtServerIp:" + txtServerIp + $",chbAP{MultiRun_AP_ID}:" + chbAP1.ToString() + $",txtAp{MultiRun_AP_ID}_IP:" + txtAp1_IP + ",cblRepeatConfig:" + (object)cblRepeatConfig + "cblValidateReceivedData:" + (object)cblValidateReceivedData + ",chbDecreasePriceInLevel2:" + chbDecreasePriceInLevel2.ToString() + ",chbShowAllRecive:" + chbShowAllRecive.ToString() + ",chbShowAllSend:" + chbShowAllSend.ToString());
                        WriteToFile("txtServerIp:" + serverConfigView.ServerIP + $",chbAP{MultiRun_AP_ID}:" + serverConfigView.AP_IsEnable.ToString() + $",txtAp{MultiRun_AP_ID}_IP:" + serverConfigView.AP_IP + ",cblRepeatConfig:" + (object)serverConfigView.RepeatConfig + "cblValidateReceivedData:" + (object)serverConfigView.ValidateReceivedData + ",chbDecreasePriceInLevel2:" + serverConfigView.IsDecreasePriceInLevel2.ToString() + ",chbShowAllRecive:" + chbShowAllRecive.ToString() + ",chbShowAllSend:" + serverConfigView.IsShowAllSend.ToString());
                    }
                    else
                        WriteToFile("Not find service config. Please config server service");

                    //  timerChargeRate.Enabled = MainClass.key_Value_List.Select("KeyName ='Enable_Charge_Rate'")[0]["Value"].ToString().ToLower() == "true";
                    flagConnectToSQL = true;
                    try
                    {
                        if (receiveThread != null)
                        {
                            receiveThread.Interrupt();
                            receiveThread.Abort();
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    try
                    {
                        serverConfigView.ServerIP = GetLocalIPAddress();
                        //txtServerIp = GetLocalIPAddress();
                    }
                    catch
                    {
                        //txtServerIp = "";
                        serverConfigView.ServerIP = "";
                    }
                    //TCP_RepeatCount = cblRepeatConfig;
                    //TCP_RepeatCount = serverConfigView.RepeatConfig;

                    //TCP_CountValidateReceivedData = cblValidateReceivedData;
                    objSwiper.Swiper_Update_Config_StateByGameCenterID(objMain.ID_GameCenter_Local_Get(), 0);
                    //TCP_CountValidateReceivedData = cblValidateReceivedData;
                    //if (chbAP1)
                    if (serverConfigView.AP_IsEnable.Value)
                    {
                        try
                        {
                            //ap_IP = txtAp1_IP;
                            ////ap_IP = serverConfigView.AP_IP;
                            receiveThread = new Thread(new ThreadStart(Receive_TCP));
                            receiveThread.Start();
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
            }
        }

        public int Send_Main(string IpAp, string Command, TcpClient client)
        {
            try
            {
                if (client!= null && client.Connected)
                {
                    NetworkStream stream = client.GetStream();
                    byte[] bytes = Encoding.ASCII.GetBytes(Command);
                    stream.Write(bytes, 0, bytes.Length);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"*R*+Send :> {IpAp} -> {Command}");
                    Console.ForegroundColor = ConsoleColor.White;
                    return 1;
                }
                else
                    return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }


        //private void OnElapsedTime(object source, ElapsedEventArgs e)
        private void OnElapsedTime()
        {// Copy
            //if (chbAP1)
            if (serverConfigView.AP_IsEnable.Value)
            {
                try
                {
                    if ((Send_Main(serverConfigView.AP_IP, "check", ap_Client) == 1) && receiveThread.IsAlive)
                    {
                        objMain.ServerConfig_SetAp1Status(objMain.ID_GameCenter_Local_Get(), true, MultiRun_AP_ID);
                    }
                    else
                    {
                        WriteToFile(DateTime.Now.ToString() + $": Ap{MultiRun_AP_ID} is Disconnect.");
                        objMain.ServerConfig_SetAp1Status(objMain.ID_GameCenter_Local_Get(), false, MultiRun_AP_ID);
                        try
                        {
                            Console.WriteLine($"*R*TCp_IP_Thread_{MultiRun_AP_ID}.Abort()");
                            receiveThread.Interrupt();
                            receiveThread.Abort();

                        }
                        catch (Exception ex)
                        {
                            WriteToFile($"Ap{MultiRun_AP_ID} NoStart :" + (object)ex);
                        }
                        try
                        {
                            ap_Client.Close();
                        }
                        catch (Exception ex)
                        {
                            WriteToFile(DateTime.Now.ToString() + $":Ap{MultiRun_AP_ID} Close -" + ex.Message);
                        }
                        //ap_IP = txtAp1_IP;
                        ////ap_IP = serverConfigView.AP_IP;
                        Console.WriteLine($"*R*Start TCp_IP_Thread_{MultiRun_AP_ID}");
                        receiveThread = new Thread(new ThreadStart(Receive_TCP));
                        receiveThread.Start();
                    }
                }
                catch (Exception ex)
                {
                    WriteToFile(DateTime.Now.ToString() + $": Ap{MultiRun_AP_ID} :" + ex.Message);
                }
            }
            if (!flagConnectToSQL)
            {
                AppLoadMain();
                return;
            }
            try
            {
                List<ServerConfigView> byGameCenter = objMain.ServerConfig_GetByGameCenterID(objMain.ID_GameCenter_Local_Get(), MultiRun_AP_ID);
                if (byGameCenter.Any() && byGameCenter.FirstOrDefault().IsRestart.Value)
                    AppLoadMain();
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
            }
        }

        public void Receive_TCP()
        {// Copy
            DispStringRecive = "";
            DispStringSplit = "";
            try
            {
                int port = 1000;
                byte[] numArray = new byte[256];
                while (true)
                {
                    try
                    {
                        ap_Client = new TcpClient();
                    }
                    catch
                    {
                        WriteToFile($"Co_StartTcpIp_{MultiRun_AP_ID}");
                        ap_Client.Close();
                    }

                    ap_Client.Connect(serverConfigView.AP_IP, port);
                    // Thread.Sleep(1);
                    Console.WriteLine($"*R*clientAp{MultiRun_AP_ID}.Connect (IP : {serverConfigView.AP_IP} , Port: {port})");

                    NetworkStream stream = ap_Client.GetStream();

                    try
                    {
                        int count;
                        while ((count = stream.Read(numArray, 0, numArray.Length)) > 0)
                        {
                            DispStringRecive = "";
                            DispStringSplit = "";
                            try
                            {
                                string str2 = Encoding.ASCII.GetString(numArray, 0, count).Replace("\n", "");
                                Thread.Sleep(1);
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("*R*+Recive :<" + serverConfigView.AP_IP + " : <- " + str2);
                                Console.ForegroundColor = ConsoleColor.White;

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
                                            DateTime tempReciveTime = DateTime.Now;
                                            if (Recive_ProcessData(DispStringSplit, MultiRun_AP_ID) == "true")
                                                Recive_DisplayText(DispStringSplit, $"P{MultiRun_AP_ID}", tempReciveTime);
                                            else if (_checkBoxShowAll)
                                                Recive_DisplayText(DispStringSplit, $"P{MultiRun_AP_ID}", tempReciveTime);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    objMain.ErrorLog(ex);
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    catch (Exception ex) { }

                }
            }
            catch (SocketException ex) { }
        }

        public string Recive_ProcessData(string ReciveText, int P)
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
                            ReceiveStorage_Insert(ReciveText, P);
                            flag = true;

                        }
                    }
                }
                catch (Exception ex)
                {
                    objMain.ErrorLog(ex);
                    objMain.ErrorLogTemp("error Process_ReciveData CONFIG :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                }
                try
                {
                    if (ReciveText.Contains("OKCFG") && !flag)
                    {
                        if (new Regex("^[[][0-9a-fA-F]{12}[]]OKCFG[0-9]{1}$", RegexOptions.IgnoreCase).Match(ReciveText).Success)
                        {
                            ReceiveStorage_Insert(ReciveText, P);
                            flag = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    objMain.ErrorLog(ex);
                    objMain.ErrorLogTemp("error Process_ReciveData OKCFG :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                }
                try
                {
                    if (ReciveText.Contains("CID") && !flag)
                    {
                        if (new Regex("^[[][0-9a-f]{12}[]][+]CID[=][0-9a-fA-F]{8}[,][0-9]{1}$", RegexOptions.IgnoreCase).Match(ReciveText).Success)
                        {
                            ReceiveStorage_Insert(ReciveText, P);
                            flag = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    objMain.ErrorLog(ex);
                    objMain.ErrorLogTemp("error Process_ReciveData CID :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                }
                try
                {
                    if (ReciveText.Contains("+P=") && !flag)
                    {
                        if (new Regex("^[[][0-9a-f]{12}[]][+]P[=][0-9a-fA-F]{8}[,][0-9]{1}$", RegexOptions.IgnoreCase).Match(ReciveText).Success)
                        {
                            ReceiveStorage_Insert(ReciveText, P);
                            flag = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    objMain.ErrorLog(ex);
                    objMain.ErrorLogTemp("error Process_ReciveData +P :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                }
                try
                {
                    if (ReciveText.Contains("+T=") && !flag)
                    {
                        if (new Regex("^[[][0-9a-f]{12}[]][+]T[=][0-9a-fA-F]{8}[,][0-9]{4}[,][A-Z]{1}$", RegexOptions.IgnoreCase).Match(ReciveText).Success)
                        {
                            ReceiveStorage_Insert(ReciveText, P);
                            flag = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    objMain.ErrorLog(ex);
                    objMain.ErrorLogTemp("error Process_ReciveData +T :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                }
                try
                {
                    if (ReciveText.Contains("Error_conf") && !flag)
                    {
                        ReceiveStorage_Insert(ReciveText, P);
                        flag = true;
                    }
                }
                catch (Exception ex)
                {
                    objMain.ErrorLog(ex);
                    objMain.ErrorLogTemp("error Process_ReciveData Error_config :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                }
                try
                {
                    if (ReciveText.Contains("HID") && !flag)
                    {
                        new Regex("^[[][0-9a-f]{12}[]][+]HID[=][0-9a-fA-F]{8}[,][0-9]{1}$", RegexOptions.IgnoreCase).Match(ReciveText);
                        ReceiveStorage_Insert(ReciveText, P);
                        flag = true;
                    }
                }
                catch (Exception ex)
                {
                    objMain.ErrorLog(ex);
                    objMain.ErrorLogTemp("error Process_ReciveData HID :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                }
                try
                {
                    if (ReciveText.Contains("OK_") && !flag)
                    {
                        new Regex("^[[][0-9a-fA-F]{12}[]]OKCFG_[0-9a-fA-F]$", RegexOptions.IgnoreCase).Match(ReciveText);
                        ReceiveStorage_Insert(ReciveText, P);
                        flag = true;
                    }
                }
                catch (Exception ex)
                {
                    objMain.ErrorLog(ex);
                    objMain.ErrorLogTemp("error Process_ReciveData OKCFG_HID :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                }
                return flag.ToString().ToLower();
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                objMain.ErrorLogTemp("error Process_ReciveData :" + ex.Message);
                return flag.ToString().ToLower();
            }
        }

        private void Recive_DisplayText(string reciveData, string tcpIpName, DateTime reciveTime)
        {
            if (txtRecive.Length > 10000)
                txtRecive = "";

            string reciveTimeStr = reciveTime.ToString("hh:mm:ss:fff");
            if (!chbShowAllRecive)
            {
                if (txtRecive.Contains(reciveData))
                    return;
                WriteToFile_SendRecive($"{tcpIpName}-{reciveTimeStr}-{reciveData}", 2, reciveTime);
                txtRecive += $"{tcpIpName}-{reciveTimeStr}-{reciveData}";
                Console.WriteLine($"*R*{tcpIpName}-{reciveTimeStr}-{reciveData}", 2, reciveTime);
            }
            else
            {
                WriteToFile_SendRecive($"{tcpIpName}-{reciveTimeStr}-{reciveData}", 2, reciveTime);
                txtRecive += $"{tcpIpName}-{reciveTimeStr}-{reciveData}";
                Console.WriteLine($"*R*{tcpIpName}-{reciveTimeStr}-{reciveData}", 2, reciveTime);
            }
        }

        public bool ReceiveStorage_Insert(string ReciveText, int P)
        {
            try
            {
                objMain.ReceiveStorage_insert(ReciveText, P);
                if (ReciveText.Contains("OKCFG1"))
                    objMain.ReceiveStorage_insert(ReciveText, P);
                return true;
            }
            catch (Exception ex)
            {
                objMain.ErrorLogTemp("ReceiveStorage_Insert:exp:" + ex.Message + "ReciveText:" + ReciveText);
                return false;
            }
        }

        public string GetLocalIPAddress()
        {
            foreach (IPAddress address in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                    return address.ToString();
            }
            throw new Exception("Local IP Address Not Found!");
        }

        #region ' Write To File '

        public void WriteToFile(string message)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("*R*WriteToFile : " + message);
                Console.ForegroundColor = ConsoleColor.White;

                string pathFileLog = AppDomain.CurrentDomain.BaseDirectory + "\\ServiceLogs";
                if (!Directory.Exists(pathFileLog))
                    Directory.CreateDirectory(pathFileLog);

                pathFileLog = $"{AppDomain.CurrentDomain.BaseDirectory}\\ServiceLogs\\ServiceLog_{DateTime.Now.Date.ToShortDateString().Replace('/', '_')}.txt";
                if (File.Exists(pathFileLog))
                {
                    using (StreamWriter streamWriter = File.AppendText(pathFileLog))
                        streamWriter.WriteLine(message);
                }
                else
                {
                    using (StreamWriter text = File.CreateText(pathFileLog))
                        text.WriteLine(message);
                }
            }
            catch { }
        }

        /// <summary>
        /// Insert in Database if Not Write in File
        /// </summary>
        /// <param name="message"></param>
        /// <param name="sendOrRecive"> 1-> Send * 2-> Recive</param>
        /// <param name="dtSR"></param>
        public void WriteToFile_SendRecive(string message, int sendOrRecive, DateTime dtSR)
        {
            try
            {
                string pathFileSendRecive = AppDomain.CurrentDomain.BaseDirectory + "\\ServiceLogs";
                if (!Directory.Exists(pathFileSendRecive))
                    Directory.CreateDirectory(pathFileSendRecive);

                pathFileSendRecive = "";
                string tempTime = DateTime.Now.ToString("yyyy-MM-dd");

                if (sendOrRecive == 1)
                {
                    if (objMain.Server_SendMessage_Insert(message, dtSR) != 1)
                        pathFileSendRecive = $"\\ServiceLog_Send{tempTime}.txt";
                }
                else if (sendOrRecive == 2)
                {
                    if (objMain.Server_ReciveMessage_Insert(message, dtSR) != 1)
                        pathFileSendRecive = $"\\ServiceLog_Recive{tempTime}.txt";
                }

                if (string.IsNullOrWhiteSpace(pathFileSendRecive))
                    return;
                pathFileSendRecive = AppDomain.CurrentDomain.BaseDirectory + pathFileSendRecive;
                if (File.Exists(pathFileSendRecive))
                {
                    using (StreamWriter streamWriter = File.AppendText(pathFileSendRecive))
                        streamWriter.WriteLine(message);
                }
                else
                {
                    using (StreamWriter text = File.CreateText(pathFileSendRecive))
                        text.WriteLine(message);
                }
            }
            catch { }
        }

        #endregion
    }
}
