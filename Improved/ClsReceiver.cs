using System.Text.RegularExpressions;
using System.Collections.Generic;
using ClickServerService.Models;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Linq;
using System.Net;
using System.IO;
using System;

namespace ClickServerService.Improved
{
    public class ClsReceiver
    {
        /// <summary>
        /// Global Variables
        /// </summary>
        ServerConfigView serverConfigView = new ServerConfigView();

        readonly MainClass objMain = new MainClass();
        readonly SwiperClass objSwiper = new SwiperClass();

        Thread receiveThread;

        bool _checkBoxShowAll = false;
        bool chbShowAllRecive = false;
        bool flagConnectToSQL = false;
        readonly int multiRun_AP_ID = 0;

        string dispStringRecive = "", dispStringSplit = "", txtRecive = "";

        public ClsReceiver(int apID)
        {
            try
            {
                multiRun_AP_ID = apID;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
            }
        }

        public void Start()
        {
            try
            {
                AppLoadMain();
                while (true)
                {
                    OnElapsedTime();
                    Thread.Sleep(3000);
                }
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
            }
        }

        public void AppLoadMain()
        {
            try
            {
                //objMain.MyPrint("Receive - AppLoadMain", ConsoleColor.Blue);
                flagConnectToSQL = false;
                serverConfigView.ValidateReceivedData = 2;
                serverConfigView.AP_IsEnable = false;
                serverConfigView.ServerIP = "0.0.0.0";
                serverConfigView.RepeatConfig = 2;
                chbShowAllRecive = true;
                txtRecive = "";
                objSwiper.Swiper_Update_Config_StateAll(0, objMain.ID_GameCenter_Local_Get());

                if (!objMain.Licence_Check())
                {
                    WriteToFile(DateTime.Now.ToString() + ":1:Licence ERROR ");
                }
                else
                {
                    objMain.LoadGameCenterID();

                    List<ServerConfigView> byGameCenter = objMain.ServerConfig_GetByGameCenterID(objMain.ID_GameCenter_Local_Get(), multiRun_AP_ID);
                    if (byGameCenter.Any())
                    {
                        serverConfigView = byGameCenter.FirstOrDefault();
                        chbShowAllRecive = serverConfigView.IsShowAllRecive.Value;
                        _checkBoxShowAll = serverConfigView.IsShowAllRecive.Value;
                        WriteToFile("txtServerIp:" + serverConfigView.ServerIP + $",chbAP{multiRun_AP_ID}:" + serverConfigView.AP_IsEnable.ToString() + $",txtAp{multiRun_AP_ID}_IP:" + serverConfigView.AP_IP + ",cblRepeatConfig:" + (object)serverConfigView.RepeatConfig + "cblValidateReceivedData:" + (object)serverConfigView.ValidateReceivedData + ",chbDecreasePriceInLevel2:" + serverConfigView.IsDecreasePriceInLevel2.ToString() + ",chbShowAllRecive:" + chbShowAllRecive.ToString() + ",chbShowAllSend:" + serverConfigView.IsShowAllSend.ToString());
                    }
                    else
                        WriteToFile("Not find service config. Please config server service");

                    flagConnectToSQL = true;
                    try
                    {
                        if (receiveThread != null)
                        {
                            receiveThread.Interrupt();
                            receiveThread.Abort();
                        }
                    }
                    catch { }
                    try
                    {
                        serverConfigView.ServerIP = GetLocalIPAddress();
                    }
                    catch
                    {
                        serverConfigView.ServerIP = "";
                    }
                    objSwiper.Swiper_Update_Config_StateByGameCenterID(objMain.ID_GameCenter_Local_Get(), 0);
                    if (serverConfigView.AP_IsEnable)
                    {
                        try
                        {
                            receiveThread = new Thread(new ThreadStart(Receive_TCP));
                            receiveThread.Start();
                        }
                        catch { }
                    }
                }
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
            }
        }

        public int Send_Main(string ipAp, string command, TcpClient client)
        {
            try
            {
                if (client != null && client.Connected)
                {
                    NetworkStream stream = client.GetStream();
                    byte[] bytes = Encoding.ASCII.GetBytes(command);
                    stream.Write(bytes, 0, bytes.Length);
                    objMain.MyPrint($"+R%Send%{ipAp}%{command}", ConsoleColor.Yellow, DateTime.Now);
                    return 1;
                }
                else
                    return -1;
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                return -1;
            }
        }

        private void OnElapsedTime()
        {
            if (serverConfigView.AP_IsEnable)
            {
                try
                {
                    if ((Send_Main(serverConfigView.AP_IP, "check", ClsStarter.tCPClientList.SingleOrDefault(i => i.AP_ID == multiRun_AP_ID).TCPClient) == 1) && receiveThread.IsAlive)
                    {
                        objMain.ServerConfig_SetAp1Status(objMain.ID_GameCenter_Local_Get(), true, multiRun_AP_ID);
                    }
                    else
                    {
                        WriteToFile(DateTime.Now.ToString() + $": Ap{multiRun_AP_ID} is Disconnect.");
                        objMain.ServerConfig_SetAp1Status(objMain.ID_GameCenter_Local_Get(), false, multiRun_AP_ID);
                        try
                        {
                            objMain.MyPrint($"R%TCp_IP_Thread_{multiRun_AP_ID}.Abort()", ConsoleColor.DarkMagenta);
                            if (receiveThread != null)
                            {
                                receiveThread.Interrupt();
                                receiveThread.Abort();
                            }
                        }
                        catch (Exception ex)
                        {
                            objMain.MyPrint($"Ap{multiRun_AP_ID} NoStart :" + ex, ConsoleColor.Red);
                            objMain.ErrorLog(ex);
                        }
                        objMain.MyPrint($"R%Start TCp_IP_Thread_{multiRun_AP_ID}");
                        receiveThread = new Thread(new ThreadStart(Receive_TCP));
                        receiveThread.Start();
                    }
                }
                catch (Exception ex)
                {
                    WriteToFile(DateTime.Now.ToString() + $": Ap{multiRun_AP_ID} :" + ex.Message);
                    objMain.ErrorLog(ex);
                }
            }
            if (!flagConnectToSQL)
            {
                AppLoadMain();
                return;
            }
            try
            {
                List<ServerConfigView> byGameCenter = objMain.ServerConfig_GetByGameCenterID(objMain.ID_GameCenter_Local_Get(), multiRun_AP_ID);
                if (byGameCenter.Any() && byGameCenter.FirstOrDefault().IsRestart.Value)
                    AppLoadMain();
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
            }
        }

        public void Receive_TCP()
        {
            dispStringRecive = "";
            dispStringSplit = "";
            try
            {
                byte[] numArray = new byte[256];
                while (true)
                {
                    try
                    {
                        objMain.MyPrint($"R%clientAp{multiRun_AP_ID}.Connect (IP : {serverConfigView.AP_IP} , Port: {serverConfigView.AP_Port})", ConsoleColor.Magenta);
                        NetworkStream stream = null;
                        if (!ClsStarter.tCPClientList.SingleOrDefault(i => i.AP_ID == multiRun_AP_ID).TCPClient.Connected)
                        {
                            ClsStarter.tCPClientList.SingleOrDefault(i => i.AP_ID == multiRun_AP_ID).TCPClient.Close();
                            ClsStarter.tCPClientList.SingleOrDefault(i => i.AP_ID == multiRun_AP_ID).TCPClient = new TcpClient();
                            ClsStarter.tCPClientList.SingleOrDefault(i => i.AP_ID == multiRun_AP_ID).TCPClient.Connect(serverConfigView.AP_IP, serverConfigView.AP_Port);
                        }
                        stream = ClsStarter.tCPClientList.SingleOrDefault(i => i.AP_ID == multiRun_AP_ID).TCPClient.GetStream();
                        try
                        {
                            int count;
                            while ((count = stream.Read(numArray, 0, numArray.Length)) > 0)
                            {
                                dispStringRecive = "";
                                dispStringSplit = "";
                                try
                                {
                                    string str2 = Encoding.ASCII.GetString(numArray, 0, count).Replace("\n", "");

                                    objMain.MyPrint("+R%Recive%" + serverConfigView.AP_IP + "%" + str2, ConsoleColor.Cyan, DateTime.Now);

                                    dispStringRecive += str2;
                                    try
                                    {
                                        string[] strArray = dispStringRecive.Split('[');
                                        for (int index = 0; index < strArray.Length; ++index)
                                        {
                                            dispStringSplit = strArray[index].Trim();
                                            if (dispStringSplit.Length > 0)
                                            {
                                                dispStringSplit = "[" + strArray[index].Trim();
                                                DateTime tempReciveTime = DateTime.Now;
                                                if (Recive_ProcessData(dispStringSplit, multiRun_AP_ID) == "true")
                                                    Recive_DisplayText(dispStringSplit, $"P{multiRun_AP_ID}", tempReciveTime);
                                                else if (_checkBoxShowAll)
                                                    Recive_DisplayText(dispStringSplit, $"P{multiRun_AP_ID}", tempReciveTime);
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
                                    objMain.ErrorLog(ex);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            objMain.ErrorLog(ex);
                        }
                    }
                    catch //(Exception ex)
                    {
                       // objMain.ErrorLog(ex);
                    }
                }
            }
            catch (SocketException ex)
            {
                objMain.ErrorLog(ex);
            }
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
            try
            {
                if (txtRecive.Length > 10000)
                    txtRecive = "";

                string reciveTimeStr = reciveTime.ToString("HH:mm:ss:fff");
                if (!chbShowAllRecive)
                {
                    if (txtRecive.Contains(reciveData))
                        return;
                    WriteToFile_SendRecive($"{tcpIpName}-{reciveTimeStr}-{reciveData}", 2, reciveTime);
                    txtRecive += $"{tcpIpName}-{reciveTimeStr}-{reciveData}";
                }
                else
                {
                    WriteToFile_SendRecive($"{tcpIpName}-{reciveTimeStr}-{reciveData}", 2, reciveTime);
                    txtRecive += $"{tcpIpName}-{reciveTimeStr}-{reciveData}";
                }
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
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
                objMain.MyPrint("R%WriteToFile : " + message, ConsoleColor.Green);

                string pathFileLog = AppDomain.CurrentDomain.BaseDirectory + "\\ServiceLogs";
                if (!Directory.Exists(pathFileLog))
                    Directory.CreateDirectory(pathFileLog);

                pathFileLog = $"{AppDomain.CurrentDomain.BaseDirectory}\\ServiceLogs\\ServiceLog_{DateTime.Now:yyyy-MM-dd}.txt";
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
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
            }
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
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
            }
        }

        #endregion
    }
}