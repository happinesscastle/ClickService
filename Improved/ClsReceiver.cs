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

        #region ' Variables '

        ServerConfigView serverConfigView = new ServerConfigView();

        readonly MainClass objMain = new MainClass();
        readonly SwiperClass objSwiper = new SwiperClass();

        Thread receiveThread;

        bool checkBoxShowAll = false, chbShowAllReceive = false, flagConnectToSQL = false;

        readonly int multiRun_AP_ID = 0;

        string dispStringReceive = "", dispStringSplit = "", txtReceive = "";

        #endregion

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
                chbShowAllReceive = true;
                txtReceive = "";
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
                        chbShowAllReceive = serverConfigView.IsShowAllReceive.Value;
                        checkBoxShowAll = serverConfigView.IsShowAllReceive.Value;
                        WriteToFile("txtServerIp:" + serverConfigView.ServerIP + $",chbAP{multiRun_AP_ID}:" + serverConfigView.AP_IsEnable.ToString() + $",txtAp{multiRun_AP_ID}_IP:" + serverConfigView.AP_IP + ",cblRepeatConfig:" + (object)serverConfigView.RepeatConfig + "cblValidateReceivedData:" + (object)serverConfigView.ValidateReceivedData + ",chbDecreasePriceInLevel2:" + serverConfigView.IsDecreasePriceInLevel2.ToString() + ",chbShowAllReceive:" + chbShowAllReceive.ToString() + ",chbShowAllSend:" + serverConfigView.IsShowAllSend.ToString());
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
                        objMain.ServerConfig_SetApStatus(objMain.ID_GameCenter_Local_Get(), true, multiRun_AP_ID);
                    }
                    else
                    {
                        WriteToFile(DateTime.Now.ToString() + $": Ap{multiRun_AP_ID} is Disconnect.");
                        objMain.ServerConfig_SetApStatus(objMain.ID_GameCenter_Local_Get(), false, multiRun_AP_ID);
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
            dispStringReceive = "";
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
                                dispStringReceive = "";
                                dispStringSplit = "";
                                try
                                {
                                    string readData = Encoding.ASCII.GetString(numArray, 0, count).Replace("\n", "");

                                    objMain.MyPrint("+R%Receive%" + serverConfigView.AP_IP + "%" + readData, ConsoleColor.Cyan, DateTime.Now);

                                    dispStringReceive += readData;
                                    try
                                    {
                                        string[] strArray = dispStringReceive.Split('[');
                                        for (int index = 0; index < strArray.Length; ++index)
                                        {
                                            dispStringSplit = strArray[index].Trim();
                                            if (dispStringSplit.Length > 0)
                                            {
                                                dispStringSplit = "[" + strArray[index].Trim();
                                                DateTime tempReceiveTime = DateTime.Now;
                                                if (Receive_ProcessData(dispStringSplit, multiRun_AP_ID) == "true")
                                                    Receive_DisplayText(dispStringSplit, $"P{multiRun_AP_ID}", tempReceiveTime);
                                                else if (checkBoxShowAll)
                                                    Receive_DisplayText(dispStringSplit, $"P{multiRun_AP_ID}", tempReceiveTime);
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
                    catch { }
                }
            }
            catch (SocketException ex)
            {
                objMain.ErrorLog(ex);
            }
        }

        public string Receive_ProcessData(string receiveText, int p)
        {
            bool flag = false;
            try
            {
                receiveText = receiveText.Trim();
                try
                {
                    if (receiveText.Contains("CONFIG"))
                    {
                        if (new Regex("^[[][0-9a-fA-F]{12}[]][+]CONFIG$", RegexOptions.IgnoreCase).Match(receiveText).Success)
                        {
                            ReceiveStorage_Insert(receiveText, p);
                            flag = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    objMain.ErrorLog(ex);
                    objMain.ErrorLogTemp($"error Process_ReceiveData CONFIG :exp= {ex.Message},ReceiveText={receiveText}");
                }
                try
                {
                    if (receiveText.Contains("OKCFG") && !flag)
                    {
                        if (new Regex("^[[][0-9a-fA-F]{12}[]]OKCFG[0-9]{1}$", RegexOptions.IgnoreCase).Match(receiveText).Success)
                        {
                            ReceiveStorage_Insert(receiveText, p);
                            flag = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    objMain.ErrorLog(ex);
                    objMain.ErrorLogTemp($"error Process_ReceiveData OKCFG :exp= {ex.Message},ReceiveText={receiveText}");
                }
                try
                {
                    if (receiveText.Contains("CID") && !flag)
                    {
                        if (new Regex("^[[][0-9a-f]{12}[]][+]CID[=][0-9a-fA-F]{8}[,][0-9]{1}$", RegexOptions.IgnoreCase).Match(receiveText).Success)
                        {
                            ReceiveStorage_Insert(receiveText, p);
                            flag = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    objMain.ErrorLog(ex);
                    objMain.ErrorLogTemp($"error Process_ReceiveData CID :exp= {ex.Message},ReceiveText={receiveText}");
                }
                try
                {
                    if (receiveText.Contains("+P=") && !flag)
                    {
                        if (new Regex("^[[][0-9a-f]{12}[]][+]P[=][0-9a-fA-F]{8}[,][0-9]{1}$", RegexOptions.IgnoreCase).Match(receiveText).Success)
                        {
                            ReceiveStorage_Insert(receiveText, p);
                            flag = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    objMain.ErrorLog(ex);
                    objMain.ErrorLogTemp($"error Process_ReceiveData +P :exp= {ex.Message},ReceiveText={receiveText}");
                }
                try
                {
                    if (receiveText.Contains("+T=") && !flag)
                    {
                        if (new Regex("^[[][0-9a-f]{12}[]][+]T[=][0-9a-fA-F]{8}[,][0-9]{4}[,][A-Z]{1}$", RegexOptions.IgnoreCase).Match(receiveText).Success)
                        {
                            ReceiveStorage_Insert(receiveText, p);
                            flag = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    objMain.ErrorLog(ex);
                    objMain.ErrorLogTemp($"error Process_ReceiveData +T :exp= {ex.Message},ReceiveText={receiveText}");
                }
                try
                {
                    if (receiveText.Contains("Error_conf") && !flag)
                    {
                        ReceiveStorage_Insert(receiveText, p);
                        flag = true;
                    }
                }
                catch (Exception ex)
                {
                    objMain.ErrorLog(ex);
                    objMain.ErrorLogTemp($"error Process_ReceiveData Error_config :exp= {ex.Message},ReceiveText={receiveText}");
                }
                try
                {
                    if (receiveText.Contains("HID") && !flag)
                    {
                        new Regex("^[[][0-9a-f]{12}[]][+]HID[=][0-9a-fA-F]{8}[,][0-9]{1}$", RegexOptions.IgnoreCase).Match(receiveText);
                        ReceiveStorage_Insert(receiveText, p);
                        flag = true;
                    }
                }
                catch (Exception ex)
                {
                    objMain.ErrorLog(ex);
                    objMain.ErrorLogTemp($"error Process_ReceiveData HID :exp= {ex.Message},ReceiveText={receiveText}");
                }
                try
                {
                    if (receiveText.Contains("OK_") && !flag)
                    {
                        new Regex("^[[][0-9a-fA-F]{12}[]]OKCFG_[0-9a-fA-F]$", RegexOptions.IgnoreCase).Match(receiveText);
                        ReceiveStorage_Insert(receiveText, p);
                        flag = true;
                    }
                }
                catch (Exception ex)
                {
                    objMain.ErrorLog(ex);
                    objMain.ErrorLogTemp($"error Process_ReceiveData OKCFG_HID :exp= {ex.Message},ReceiveText={receiveText}");
                }
                return flag.ToString().ToLower();
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                objMain.ErrorLogTemp($"error Process_ReceiveData :{ex.Message}");
                return flag.ToString().ToLower();
            }
        }

        private void Receive_DisplayText(string receiveData, string tcpIpName, DateTime receiveTime)
        {
            try
            {
                if (txtReceive.Length > 10000)
                    txtReceive = "";

                string receiveTimeStr = receiveTime.ToString("HH:mm:ss:fff");
                if (!chbShowAllReceive)
                {
                    if (txtReceive.Contains(receiveData))
                        return;
                    WriteToFile_ReceiveLog($"{tcpIpName}-{receiveTimeStr}-{receiveData}", receiveTime);
                    txtReceive += $"{tcpIpName}-{receiveTimeStr}-{receiveData}";
                }
                else
                {
                    WriteToFile_ReceiveLog($"{tcpIpName}-{receiveTimeStr}-{receiveData}", receiveTime);
                    txtReceive += $"{tcpIpName}-{receiveTimeStr}-{receiveData}";
                }
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
            }
        }

        public bool ReceiveStorage_Insert(string receiveText, int p)
        {
            try
            {
                objMain.ReceiveStorage_insert(receiveText, p);
                if (receiveText.Contains("OKCFG1"))
                    objMain.ReceiveStorage_insert(receiveText, p);
                return true;
            }
            catch (Exception ex)
            {
                objMain.ErrorLogTemp($"ReceiveStorage_Insert:exp:{ex.Message}ReceiveText:{receiveText}");
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

                pathFileLog += $"\\ServiceLog_AP{multiRun_AP_ID}_{DateTime.Now:yyyy-MM-dd}.txt";

                File.AppendAllText(pathFileLog, message, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
            }
        }

        /// <summary>
        /// Insert in Database if Not Write in File
        /// </summary>
        public void WriteToFile_ReceiveLog(string message, DateTime dtSR)
        {
            try
            {
                int retValue = objMain.Server_ReceiveMessage_Insert(message, dtSR);

                if (retValue != 1)
                {
                    string pathFileReceive = AppDomain.CurrentDomain.BaseDirectory + "\\ServiceLogs";
                    if (!Directory.Exists(pathFileReceive))
                        Directory.CreateDirectory(pathFileReceive);

                    pathFileReceive += $"\\ServiceLog_Receive_AP{multiRun_AP_ID}_{DateTime.Now:yyyy-MM-dd}.txt";
                    File.WriteAllText(pathFileReceive, message, Encoding.UTF8);
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