using System.Collections.Generic;
using ClickServerService.Models;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System;

namespace ClickServerService.Improved
{
    public class ClsSender
    {
        /// <summary>
        /// Global Variables
        /// </summary>
        List<ServerConfigView> serverConfigView = new List<ServerConfigView>();

        MainClass objMain = new MainClass();
        SwiperClass objSwiper = new SwiperClass();
        CardClass objcard = new CardClass();
        UsersClass objUser = new UsersClass();
        Pattern objPattern = new Pattern();

        //string ap_IP = "";// Copy -> serverConfigView.AP_IP
        //TcpClient ap_Client;// Copy


        //int MultiRun_AP_ID = 0;
        int Main_ID_GameCenter = 1;
        int TCP_RepeatCount = 1;

        //bool chbDecreasePriceInLevel2 = false; -> serverConfigView.IsDecreasePriceInLevel2.Value

        string DispStringRecive;
        string txtRecive = "", txtSend = "";

        public ClsSender()
        {
            //MultiRun_AP_ID = 1;
            AppLoadMain();
        }

        public void AppLoadMain()
        {
            objMain.MyPrint("Send - AppLoadMain" , ConsoleColor.Blue);
            txtRecive = "";
            txtSend = "";
            int port = 1000;

            objMain.Decript_Connection_String();
            MainClass.key_Value_List = objMain.Key_Value_Get();
            objSwiper.Swiper_Update_Config_StateAll(0, objMain.ID_GameCenter_Local_Get());

            objMain.LoadGameCenterID();
            Main_ID_GameCenter = objMain.ID_GameCenter_Local_Get();

            List<ServerConfigView> byGameCenter = objMain.ServerConfig_GetByGameCenterID(objMain.ID_GameCenter_Local_Get());
            if (byGameCenter.Any())
            {
                serverConfigView = byGameCenter;
                TCP_RepeatCount = serverConfigView.FirstOrDefault().RepeatConfig.Value;

                //ap_Client = new TcpClient(serverConfigView.AP_IP, port);

                foreach (var item in serverConfigView)
                {
                    objMain.MyPrint(" ip : " + item.AP_IP);
                }
            }
            else
                objMain.MyPrint("Not find service config. Please config server service", ConsoleColor.DarkRed);
        }

        public void Start()
        {
            while (true)
            {
                Timer_SendData_Tick();
                Thread.Sleep(300);
            }
        }

        private void Timer_SendData_Tick()
        {
            DataTable storageGetForSend = objMain.ReceiveStorage_GetForSend();
            for (int index1 = 0; index1 < storageGetForSend.Rows.Count; ++index1)
            {
                string str1 = Send_Process_Main(storageGetForSend.Rows[index1]["ReciveText"].ToString());
                DispStringRecive = str1;
                if (!string.IsNullOrWhiteSpace(str1))
                {
                    string str2 = str1.Split('!')[0].ToString();
                    str1.Split('!')[1].ToString();
                    string t_SwiperName = str1.Split('!')[2].ToString();
                    string t_CardmacAddress = str1.Split('!')[3].ToString();

                    objMain.ReceiveStorage_UpdateIsProcess(storageGetForSend.Rows[index1]["ReciveText"].ToString());
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
                            //Send_Main(serverConfigView.AP_IP, Command1, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command1);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command2 = "[" + str3 + "]AT+TTIK=" + str5;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_Main(serverConfigView.AP_IP, Command2, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command2);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command3 = "[" + str3 + "]A1=" + str6;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_Main(serverConfigView.AP_IP, Command3, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command3);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command4 = "[" + str3 + "]A2=" + str7;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_Main(serverConfigView.AP_IP, Command4, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command4);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command5 = "[" + str3 + "]A3=" + str8;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_Main(serverConfigView.AP_IP, Command5, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command5);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command6 = "[" + str3 + "]A4=" + str9;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_Main(serverConfigView.AP_IP, Command6, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command6);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command7 = "[" + str3 + "]A5=" + str10;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_Main(serverConfigView.AP_IP, Command7, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command7);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command8 = "[" + str3 + "]B1=" + str11;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_Main(serverConfigView.AP_IP, Command8, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command8);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command9 = "[" + str3 + "]B2=" + str12;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_Main(serverConfigView.AP_IP, Command9, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command9);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command10 = "[" + str3 + "]B3=" + str13;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_Main(serverConfigView.AP_IP, Command10, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command10);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command11 = "[" + str3 + "]B4=" + str14;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_Main(serverConfigView.AP_IP, Command11, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command11);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command12 = "[" + str3 + "]B5=" + str15;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_Main(serverConfigView.AP_IP, Command12, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command12);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command13 = "[" + str3 + "]C1=" + str16;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_Main(serverConfigView.AP_IP, Command13, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command13);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command14 = "[" + str3 + "]C2=" + str17;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_Main(serverConfigView.AP_IP, Command14, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command14);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command15 = "[" + str3 + "]C3=" + str18;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_Main(serverConfigView.AP_IP, Command15, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command15);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command16 = "[" + str3 + "]C4=" + str19;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_Main(serverConfigView.AP_IP, Command16, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command16);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command17 = "[" + str3 + "]C5=" + str20;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_Main(serverConfigView.AP_IP, Command17, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command17);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command18 = "[" + str3 + "]D1=" + str21;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_Main(serverConfigView.AP_IP, Command18, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command18);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command19 = "[" + str3 + "]D2=" + str22;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_Main(serverConfigView.AP_IP, Command19, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command19);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command20 = "[" + str3 + "]D3=" + str23;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_Main(serverConfigView.AP_IP, Command20, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command20);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command21 = "[" + str3 + "]D4=" + str24;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_Main(serverConfigView.AP_IP, Command21, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command21);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command22 = "[" + str3 + "]D5=" + str25;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_Main(serverConfigView.AP_IP, Command22, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command22);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command23 = "[" + str3 + "]E1=" + str26;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_Main(serverConfigView.AP_IP, Command23, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command23);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command24 = "[" + str3 + "]E2=" + str27;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_Main(serverConfigView.AP_IP, Command24, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command24);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command25 = "[" + str3 + "]E3=" + str28;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_Main(serverConfigView.AP_IP, Command25, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command25);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command26 = "[" + str3 + "]E4=" + str29;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_Main(serverConfigView.AP_IP, Command26, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command26);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command27 = "[" + str3 + "]E5=" + str30;
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_Main(serverConfigView.AP_IP, Command27, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command27);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                        Thread.Sleep(millisecondsTimeout1);
                        string Command28 = "[" + str3 + "]END-DATA";
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_Main(serverConfigView.AP_IP, Command28, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                            Send_Main(Command28);
                            Thread.Sleep(millisecondsTimeout2);
                        }
                    }
                    else
                    {
                        for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                        {
                            //Send_DisplayText(str2, $"P{MultiRun_AP_ID}", t_SwiperName, t_CardmacAddress, DateTime.Now);
                            foreach (var item in serverConfigView)
                            {
                                Send_DisplayText(str2, $"P{item.AP_ID}", t_SwiperName, t_CardmacAddress, DateTime.Now);
                                Send_Main(item.AP_IP, str2, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == item.AP_ID).TCPClient);
                            }

                            Thread.Sleep(10);
                        }
                    }
                    DataTable byState = objSwiper.Swiper_GetByState();
                    for (int index2 = 0; index2 < byState.Rows.Count; ++index2)
                    {
                        string str3 = ((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString();
                        string str4 = str3.Substring(str3.Length - 5, 4);
                        string Command = "[" + MacAndTimeStamp_Create(byState.Rows[index2]["MacAddress"].ToString().ToLower()) + "]AT+CFG1=" + str4;
                        for (int index3 = 0; index3 < TCP_RepeatCount + 2; ++index3)
                        {
                            foreach (var item in serverConfigView)
                            {
                                Send_DisplayText(str2, $"P{item.AP_ID}", "", "");
                                Send_Main(item.AP_IP, Command, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == item.AP_ID).TCPClient);
                            }

                            //Send_DisplayText(str2, $"P{MultiRun_AP_ID}", "", "");
                            //Send_Main(serverConfigView.AP_IP, Command, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);

                            Thread.Sleep(100);
                        }
                    }
                    try
                    {
                        if (DispStringRecive.ToUpper().Contains("[KA=") || DispStringRecive.ToLower().Contains("selftest"))
                        {
                            DataTable stateForChangePrice = objSwiper.Swiper_GetByState_ForChangePrice();
                            for (int index2 = 0; index2 < stateForChangePrice.Rows.Count; ++index2)
                            {
                                string lower = stateForChangePrice.Rows[index2]["MacAddress"].ToString().ToLower();
                                string str3 = MacAndTimeStamp_Create(lower);
                                DataTable addressByChargeRate = objSwiper.Swiper_GetByMacAddressByChargeRate(lower.ToUpper());
                                if (addressByChargeRate.Rows.Count > 0)
                                {
                                    objSwiper.Swiper_UpdateStateByMacAddress(lower.ToUpper(), -3);
                                    string str4 = "[" + str3 + "]AT+CFG3=" + objMain.comma(addressByChargeRate.Rows[0]["PriceAdi"].ToString());
                                    string str5 = "[" + str3 + "]AT+CFG4=" + objMain.comma(addressByChargeRate.Rows[0]["PriceVije"].ToString());
                                    for (int index3 = 0; index3 < TCP_RepeatCount + 5; ++index3)
                                    {
                                        foreach (var item in serverConfigView)
                                        {
                                            Send_DisplayText(str4, $"P{item.AP_ID}", "", "");
                                            Send_Main(item.AP_IP, str4, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == item.AP_ID).TCPClient);
                                        }
                                        //Send_DisplayText(str4, $"P{MultiRun_AP_ID}", "", "");
                                        //Send_Main(serverConfigView.AP_IP, str4, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);

                                        Thread.Sleep(70);
                                    }
                                    Thread.Sleep(100);
                                    for (int index3 = 0; index3 < TCP_RepeatCount + 5; ++index3)
                                    {
                                        //Send_DisplayText(str5, $"P{MultiRun_AP_ID}", "", "");
                                        //Send_Main(serverConfigView.AP_IP, str5, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == MultiRun_AP_ID).TCPClient);
                                        foreach (var item in serverConfigView)
                                        {
                                            Send_DisplayText(str5, $"P{item.AP_ID}", "", "");
                                            Send_Main(item.AP_IP, str5, Program.tCPClientList.SingleOrDefault(i => i.AP_ID == item.AP_ID).TCPClient);
                                        }
                                        Thread.Sleep(70);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        objMain.ErrorLog(ex);
                    }
                }
            }
        }

        private void Send_DisplayText(string SendData, string TcpIPName, string t_SwiperName, string t_CardmacAddress, DateTime? sendTime = null)
        {// Copy
            if (txtRecive.Length > 10000)
            {
                txtRecive = "";
                txtSend = "";
            }
            if (sendTime == null)
                sendTime = DateTime.Now;
            string sendTimeString = sendTime.Value.ToString("hh:mm:ss:fff");
            if (!serverConfigView.FirstOrDefault().IsShowAllSend.Value)
            {
                if (SendData.Length <= 0 || txtSend.Contains(TcpIPName + "-" + SendData))
                    return;
                WriteToFile_SendRecive(TcpIPName + "-" + sendTimeString + "-" + SendData + "-" + t_SwiperName + "-" + t_CardmacAddress, 1, sendTime.Value);
                txtSend = txtSend + TcpIPName + "-" + SendData + sendTimeString + "-" + t_SwiperName + t_CardmacAddress;
            }
            else if (SendData.Length > 0)
            {
                WriteToFile_SendRecive(TcpIPName + "-" + sendTimeString + "-" + SendData + "-" + t_SwiperName + "-" + t_CardmacAddress, 1, sendTime.Value);
                txtSend = txtSend + TcpIPName + "-" + SendData + sendTimeString + "-" + t_SwiperName + t_CardmacAddress;
            }
        }

        public int Send_Main(string command)
        {// Copy 2
            try
            {
                foreach (var item in Program.tCPClientList)
                {
                    if (item.TCPClient.Connected)
                    {
                        NetworkStream stream = item.TCPClient.GetStream();
                        byte[] bytes = Encoding.ASCII.GetBytes(command);
                        stream.Write(bytes, 0, bytes.Length);

                        objMain.MyPrint($"*S*+Send :> {item.TCPClient.Client.RemoteEndPoint} -> {command}", ConsoleColor.DarkYellow, DateTime.Now);

                    }
                    else
                        return -1;
                }
                return 1;

            }
            catch
            {
                return -1;
            }
        }


        public int Send_Main(string IpAp, string Command, TcpClient client)
        {// Copy 2
            try
            {
                if (client.Connected)
                {
                    NetworkStream stream = client.GetStream();
                    byte[] bytes = Encoding.ASCII.GetBytes(Command);
                    stream.Write(bytes, 0, bytes.Length);

                    objMain.MyPrint($"*S*+Send :> {IpAp} -> {Command}", ConsoleColor.DarkYellow, DateTime.Now);

                    return 1;
                }
                else
                    return -1;
            }
            catch
            {
                return -1;
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

        public string Send_Process_Main(string ReciveText)
        {// Copy
            try
            {
                ReciveText = ReciveText.Trim();
                string str1 = "";
                string str2 = "";
                string str3_Title = "";
                string str4_Mac = "";
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
                        DataTable addressByChargeRate = objSwiper.Swiper_GetByMacAddressByChargeRate(MacAddress);
                        if (addressByChargeRate.Rows.Count > 0)
                        {
                            try
                            {
                                str3_Title = addressByChargeRate.Rows[0]["Title"].ToString();
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
                    objMain.ErrorLog(ex);
                    objMain.ErrorLogTemp("error Process Main CONFIG :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                    return "";
                }
                try
                {
                    if (ReciveText.Contains("OKCFG"))
                    {
                        string str5 = ReciveText.Substring(1, 12);
                        string MacAddress = str5.Substring(0, 2) + str5.Substring(3, 2) + str5.Substring(6, 2) + str5.Substring(9, 2);
                        string str6 = MacAndTimeStamp_Create(MacAddress);
                        DataTable addressByChargeRate = objSwiper.Swiper_GetByMacAddressByChargeRate(MacAddress.ToUpper());
                        if (addressByChargeRate.Rows.Count > 0)
                        {
                            try
                            {
                                str3_Title = addressByChargeRate.Rows[0]["Title"].ToString();
                            }
                            catch
                            {
                            }
                            int num1 = int.Parse(addressByChargeRate.Rows[0]["Config_State"].ToString());
                            if (ReciveText.Contains("OKCFG1"))
                            {
                                objSwiper.Swiper_Update_Config_State(1, MacAddress);
                                string str7 = addressByChargeRate.Rows[0]["Title"].ToString().Length > 8 ? addressByChargeRate.Rows[0]["Title"].ToString().Substring(0, 8) : addressByChargeRate.Rows[0]["Title"].ToString();
                                str1 = "[" + str6 + "]AT+CFG2=" + str7;
                                str2 = "[" + str6 + "]AT+CFG2=" + str7;
                            }
                            if (ReciveText.Contains("OKCFG2"))
                            {
                                objSwiper.Swiper_Update_Config_State(2, MacAddress);
                                if (MainClass.key_Value_List.Select("KeyName ='Enable_Charge_Rate'")[0]["Value"].ToString().ToLower() == "true")
                                {
                                    str1 = "[" + str6 + "]AT+CFG3=" + objMain.comma(addressByChargeRate.Rows[0]["PriceAdi"].ToString());
                                    str2 = "[" + str6 + "]AT+CFG3=" + objMain.comma(addressByChargeRate.Rows[0]["PriceAdi"].ToString());
                                }
                                else
                                {
                                    str1 = "[" + str6 + "]AT+CFG3=" + objMain.comma(addressByChargeRate.Rows[0]["Price1"].ToString());
                                    str2 = "[" + str6 + "]AT+CFG3=" + objMain.comma(addressByChargeRate.Rows[0]["Price1"].ToString());
                                }
                            }
                            if (ReciveText.Contains("OKCFG3"))
                            {
                                if (num1 != -3)
                                {
                                    objSwiper.Swiper_Update_Config_State(3, MacAddress);
                                    if (MainClass.key_Value_List.Select("KeyName ='Enable_Charge_Rate'")[0]["Value"].ToString().ToLower() == "true")
                                    {
                                        str1 = "[" + str6 + "]AT+CFG4=" + objMain.comma(addressByChargeRate.Rows[0]["PriceVije"].ToString());
                                        str2 = "[" + str6 + "]AT+CFG4=" + objMain.comma(addressByChargeRate.Rows[0]["PriceVije"].ToString());
                                    }
                                    else
                                    {
                                        str1 = "[" + str6 + "]AT+CFG4=" + objMain.comma(addressByChargeRate.Rows[0]["Price2"].ToString());
                                        str2 = "[" + str6 + "]AT+CFG4=" + objMain.comma(addressByChargeRate.Rows[0]["Price2"].ToString());
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
                                    objSwiper.Swiper_Update_Config_State(4, MacAddress);
                                    str1 = "[" + str6 + "]AT+CFG5=" + addressByChargeRate.Rows[0]["Delay1"].ToString() + "," + addressByChargeRate.Rows[0]["Delay2"].ToString();
                                    str2 = "[" + str6 + "]AT+CFG5=" + addressByChargeRate.Rows[0]["Delay1"].ToString() + "," + addressByChargeRate.Rows[0]["Delay2"].ToString();
                                }
                                else
                                {
                                    str1 = "[" + str6 + "]AT+ok";
                                    str2 = "[" + str6 + "]AT+ok";
                                    objSwiper.Swiper_UpdateStateByMacAddress(MacAddress.ToUpper(), 0);
                                }
                            }
                            if (ReciveText.Contains("OKCFG5"))
                            {
                                string str7 = addressByChargeRate.Rows[0]["Version"].ToString();
                                objSwiper.Swiper_Update_Config_State(5, MacAddress);
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
                                        objSwiper.Swiper_Update_Config_State(0, MacAddress);
                                    }
                                    if (str7 == "2")
                                    {
                                        int num2 = int.Parse(addressByChargeRate.Rows[0]["PulseType"].ToString());
                                        string str8 = addressByChargeRate.Rows[0]["Start_Count_Voltage"].ToString();
                                        string str9 = addressByChargeRate.Rows[0]["TicketErrorStop"].ToString();
                                        string str10 = addressByChargeRate.Rows[0]["PullUp"].ToString();
                                        objSwiper.Swiper_Update_Config_State(6, MacAddress);
                                        str1 = "[" + str6 + "]AT+CFG7=" + num2 + "," + str8 + "," + str9 + "," + str10;
                                        str2 = "[" + str6 + "]AT+CFG7=" + num2 + "," + str8 + "," + str9 + "," + str10;
                                    }
                                    if (str7 == "3")
                                    {
                                        int num2 = int.Parse(addressByChargeRate.Rows[0]["PulseType"].ToString());
                                        string str8 = addressByChargeRate.Rows[0]["Start_Count_Voltage"].ToString();
                                        string str9 = addressByChargeRate.Rows[0]["TicketErrorStop"].ToString();
                                        string str10 = addressByChargeRate.Rows[0]["PullUp"].ToString();
                                        objSwiper.Swiper_Update_Config_State(6, MacAddress);
                                        str1 = "[" + str6 + "]AT+CFG7=" + num2 + "," + str8 + "," + str9 + "," + str10;
                                        str2 = "[" + str6 + "]AT+CFG7=" + num2 + "," + str8 + "," + str9 + "," + str10;
                                    }
                                }
                                catch
                                {
                                    objSwiper.Swiper_Update_Config_State(0, MacAddress);
                                }
                            }
                            if (ReciveText.Contains("OKCFG7"))
                            {
                                str1 = "[" + str6 + "]AT+ok";
                                str2 = "[" + str6 + "]AT+ok";
                                objSwiper.Swiper_Update_Config_State(0, MacAddress);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    objMain.ErrorLog(ex);
                    objMain.ErrorLogTemp("error Process Main OKCFG :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                    return "";
                }
                string str27 = "1515";
                try
                {
                    if (ReciveText.Contains("CID"))
                    {
                        string str5 = ReciveText.Substring(1, 12);
                        string str6 = str5.Substring(0, 2) + str5.Substring(3, 2) + str5.Substring(6, 2) + str5.Substring(9, 2);
                        string str7 = MacAndTimeStamp_Create(str6);
                        DataTable addressByChargeRate = objSwiper.Swiper_GetByMacAddressByChargeRate(str6);
                        str27 = "1524";
                        if (addressByChargeRate.Rows.Count > 0)
                        {
                            int ID_Play_Type = 1;
                            try
                            {
                                str3_Title = addressByChargeRate.Rows[0]["Title"].ToString();
                            }
                            catch
                            {
                            }
                            int num1 = int.Parse(addressByChargeRate.Rows[0]["ID_Games"].ToString());
                            int.Parse(addressByChargeRate.Rows[0]["ID_Games_Class"].ToString() == "" ? "-100" : addressByChargeRate.Rows[0]["ID_Games_Class"].ToString());
                            int ID_Swiper = int.Parse(addressByChargeRate.Rows[0]["ID"].ToString());
                            str4_Mac = ReciveText.Split('=')[1].Replace("\r", "").Split(',')[0].ToUpper();
                            str27 = "1537";
                            DataTable byMacAddrress = objcard.Card_GetByMacAddrress(str4_Mac);
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
                                if (int.Parse(byMacAddrress.Rows[0]["ID_Card_Series"].ToString()) == 3 && objUser.Users_GetByGUID(byMacAddrress.Rows[0]["Card_GUID"].ToString()).Rows.Count == 0)
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
                                        Tuple<bool, int, int, int, int, bool, string> tuple = objcard.Card_CardProductTiming_Status(str8, num1);
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
                                                if (!serverConfigView.FirstOrDefault().IsDecreasePriceInLevel2.Value)
                                                {
                                                    int num8 = objcard.Card_Play_Details_Insert(str8, 0, 0, Main_ID_GameCenter, str6, num5, 0, 0, IsPersonnel, num1, ID_Swiper, ID_Play_Type, 0, 0, 0, empty);
                                                    str2 = str2 + " -L1 -" + num8;
                                                }
                                            }
                                            else if (tuple.Item5 > 0)
                                            {
                                                str27 = "B1656";
                                                flag4 = true;
                                                ID_Play_Type = 9;
                                                str2 = "[" + str7 + "]AT+PRC=" + (tuple.Item5 - 1).ToString() + "-" + str11;
                                                str1 = "[" + str7 + "]AT+PRC=" + (tuple.Item5 - 1).ToString() + " - " + str11;
                                                if (!serverConfigView.FirstOrDefault().IsDecreasePriceInLevel2.Value)
                                                {
                                                    int num8 = objcard.Card_Play_Details_Insert(str8, 0, 0, Main_ID_GameCenter, str6, num5, 0, 0, IsPersonnel, num1, ID_Swiper, ID_Play_Type, 0, 0, 0, empty);
                                                    str2 = str2 + " -L1 -" + num8;
                                                    objcard.Card_CardProductTiming_SetFreeGame(str8, tuple.Item3, tuple.Item5 - 1);
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
                                                        if (!serverConfigView.FirstOrDefault().IsDecreasePriceInLevel2.Value)
                                                        {
                                                            int num8 = objcard.Card_Play_Details_Insert(str8, 0, 0, Main_ID_GameCenter, str6, num5, 0, 0, IsPersonnel, num1, ID_Swiper, ID_Play_Type, 0, 0, 0, empty);
                                                            objcard.Card_CardProductTiming_SetChargePrice(str8, tuple.Item3, tuple.Item2 - num5);
                                                            str2 = str2 + " -L1 -" + num8;
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
                                                    if (!serverConfigView.FirstOrDefault().IsDecreasePriceInLevel2.Value)
                                                    {
                                                        int num8 = objcard.Card_Play_Details_Insert(str8, 0, 0, Main_ID_GameCenter, str6, num5, 0, 0, IsPersonnel, num1, ID_Swiper, ID_Play_Type, 0, 0, 0, empty);
                                                        str2 = str2 + " -L1 -" + num8;
                                                    }
                                                }
                                            }
                                        }
                                        str27 = "1686";
                                        if (!flag4)
                                        {
                                            int num8 = int.Parse(byMacAddrress.Rows[0]["CashPrice"].ToString());
                                            int num9 = int.Parse(byMacAddrress.Rows[0]["BonusPrice"].ToString());
                                            DataTable dataTable = objPattern.Gift_series_List_ActiveByCard_GUID(str8);
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
                                                    if (num11 > 0 && ("," + dataTable.Rows[index]["FreeDailyGames"].ToString() + ",").Contains("," + num1 + ",") && objcard.Card_Play_Details_Get_Today(str8, dataTable.Rows[index]["FreeDailyGames"].ToString()).Rows.Count < num11)
                                                    {
                                                        empty = Guid.Parse(dataTable.Rows[index]["ID"].ToString());
                                                        flag6 = true;
                                                        break;
                                                    }
                                                    if (num10 > 0 && ("," + dataTable.Rows[index]["FreeGames"].ToString() + ",").Contains("," + num1 + ","))
                                                    {
                                                        empty = Guid.Parse(dataTable.Rows[index]["ID"].ToString());
                                                        flag5 = true;
                                                        if (!serverConfigView.FirstOrDefault().IsDecreasePriceInLevel2.Value)
                                                        {
                                                            objPattern.Gift_Pattern_series_List_Update(dataTable.Rows[index]["ID"].ToString(), num10 - 1);
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
                                                if (!serverConfigView.FirstOrDefault().IsDecreasePriceInLevel2.Value)
                                                {
                                                    int num10 = objcard.Card_UpdatePriceAndBonus_PlayDetails2(str8, num8, num9, Main_ID_GameCenter, str6, num5, num8, num9, IsPersonnel, num1, ID_Swiper, ID_Play_Type, 0, 0, 0, empty);
                                                    str2 = str2 + " -L1 -" + num10;
                                                }
                                            }
                                            else
                                            {
                                                if (Pay_GiftPortion > 0)
                                                    ID_Play_Type = 8;
                                                if (num8 + num9 + Pay_GiftPortion + num6 >= num5)
                                                {
                                                    int num10 = num8 + Pay_GiftPortion + num9 + num6 - num5;
                                                    str2 = "[" + str7 + "]AT+PRC=" + objMain.comma(num10.ToString());
                                                    string str10 = num10.ToString().Length > 7 ? num10.ToString() : objMain.comma(num10.ToString());
                                                    str1 = "[" + str7 + "]AT+PRC=" + str10;
                                                    if (!serverConfigView.FirstOrDefault().IsDecreasePriceInLevel2.Value)
                                                    {
                                                        int num11 = objPattern.Gift_Pattern_Series_list_Calculate2(str8, num5);
                                                        if (num6 > 0)
                                                        {
                                                            num11 -= num6;
                                                            objcard.Card_CardProductTiming_SetChargePrice(str8, tuple.Item3, 0);
                                                        }
                                                        if (num8 >= num11)
                                                        {
                                                            int num12 = objcard.Card_UpdatePriceAndBonus_PlayDetails2(str8, num8 - num11, num9, Main_ID_GameCenter, str6, num5, num8 - num11, num9, IsPersonnel, num1, ID_Swiper, ID_Play_Type, num5, 0, Pay_GiftPortion, empty);
                                                            str2 = str2 + " -L1 -" + num12;
                                                        }
                                                        else
                                                        {
                                                            int Pay_BonusPortion = num11 - num8;
                                                            int num12 = num9 - Pay_BonusPortion;
                                                            int num13 = objcard.Card_UpdatePriceAndBonus_PlayDetails2(str8, 0, num12, Main_ID_GameCenter, str6, num5, 0, num12, IsPersonnel, num1, ID_Swiper, ID_Play_Type, num5 - Pay_BonusPortion, Pay_BonusPortion, Pay_GiftPortion, empty);
                                                            str2 = str2 + " -L1 -" + num13;
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
                    objMain.ErrorLog(ex);
                    objMain.ErrorLogTemp("error Process Main CID :exp= " + ex.Message + ",ReciveText=" + ReciveText + ",LineCode=" + str27);
                    return "";
                }
                try
                {
                    if (ReciveText.Contains("+P="))
                    {
                        string str5 = ReciveText.Substring(1, 12);
                        string str6 = str5.Substring(0, 2) + str5.Substring(3, 2) + str5.Substring(6, 2) + str5.Substring(9, 2);
                        string str7 = MacAndTimeStamp_Create(str6);
                        string MacCode = ReciveText.Split('=')[1].Replace("\r", "").Split(',')[0];
                        DataTable addressByChargeRate = objSwiper.Swiper_GetByMacAddressByChargeRate(str6);
                        int num1 = 0;
                        if (addressByChargeRate.Rows.Count > 0)
                        {
                            try
                            {
                                str3_Title = addressByChargeRate.Rows[0]["Title"].ToString() + ":" + num1 + ":";
                                str4_Mac = MacCode;
                            }
                            catch { }
                        }
                        if (serverConfigView.FirstOrDefault().IsDecreasePriceInLevel2.Value)
                        {
                            str1 = "[" + str7 + "]AT+ok";
                            str2 = "[" + str7 + "]AT+ok";
                            int ID_Play_Type = 1;
                            if (addressByChargeRate.Rows.Count > 0)
                            {
                                try
                                {
                                    str3_Title = addressByChargeRate.Rows[0]["Title"].ToString() + ":" + num1 + ":";
                                }
                                catch { }
                                int num2 = int.Parse(addressByChargeRate.Rows[0]["ID_Games"].ToString());
                                int ID_Swiper = int.Parse(addressByChargeRate.Rows[0]["ID"].ToString());
                                int.Parse(addressByChargeRate.Rows[0]["ID_Games_Class"].ToString() == "" ? "-100" : addressByChargeRate.Rows[0]["ID_Games_Class"].ToString());
                                DataTable byMacAddrress = objcard.Card_GetByMacAddrress(MacCode);
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
                                    catch { }
                                    bool.Parse(byMacAddrress.Rows[0]["AllowRegistration"].ToString());
                                    int num5 = 0;
                                    try
                                    {
                                        num5 = int.Parse(byMacAddrress.Rows[0]["ID_Card_Promotional"].ToString());
                                    }
                                    catch { }
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
                                    Tuple<bool, int, int, int, int, bool, string> tuple = objcard.Card_CardProductTiming_Status(str8, num2);
                                    if (tuple.Item1)
                                    {
                                        if (tuple.Item6)
                                        {
                                            int num8 = objcard.Card_Play_Details_Insert(str8, 0, 0, Main_ID_GameCenter, str6, num6, 0, 0, IsPersonnel, num2, ID_Swiper, ID_Play_Type, 0, 0, 0, empty);
                                            str2 = str2 + " -L2 -" + num8;
                                            flag3 = true;
                                            ID_Play_Type = 10;
                                        }
                                        else if (tuple.Item5 > 0)
                                        {
                                            flag3 = true;
                                            ID_Play_Type = 9;
                                            int num8 = objcard.Card_Play_Details_Insert(str8, 0, 0, Main_ID_GameCenter, str6, num6, 0, 0, IsPersonnel, num2, ID_Swiper, ID_Play_Type, 0, 0, 0, empty);
                                            str2 = str2 + " -L2 -" + num8;
                                            objcard.Card_CardProductTiming_SetFreeGame(str8, tuple.Item3, tuple.Item5 - 1);
                                        }
                                        else if (tuple.Item4 > 0)
                                        {
                                            if (tuple.Item2 >= num6)
                                            {
                                                flag3 = true;
                                                ID_Play_Type = 11;
                                                int num8 = objcard.Card_Play_Details_Insert(str8, 0, 0, Main_ID_GameCenter, str6, num6, 0, 0, IsPersonnel, num2, ID_Swiper, ID_Play_Type, 0, 0, 0, empty);
                                                str2 = str2 + " -L2 -" + num8;
                                                objcard.Card_CardProductTiming_SetChargePrice(str8, tuple.Item3, tuple.Item2 - num6);
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
                                            int num8 = objcard.Card_Play_Details_Insert(str8, 0, 0, Main_ID_GameCenter, str6, num6, 0, 0, IsPersonnel, num2, ID_Swiper, ID_Play_Type, 0, 0, 0, empty);
                                            str2 = str2 + " -L2 -" + num8;
                                            flag3 = true;
                                        }
                                    }
                                    if (!flag3)
                                    {
                                        int.Parse(byMacAddrress.Rows[0]["ID"].ToString());
                                        int num8 = int.Parse(byMacAddrress.Rows[0]["CashPrice"].ToString());
                                        int num9 = int.Parse(byMacAddrress.Rows[0]["BonusPrice"].ToString());
                                        DataTable dataTable = objPattern.Gift_series_List_ActiveByCard_GUID(str8);
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
                                                if (num11 > 0 && ("," + dataTable.Rows[index]["FreeDailyGames"].ToString() + ",").Contains("," + num2 + ",") && objcard.Card_Play_Details_Get_Today(str8, dataTable.Rows[index]["FreeDailyGames"].ToString()).Rows.Count < num11)
                                                {
                                                    empty = Guid.Parse(dataTable.Rows[index]["ID"].ToString());
                                                    flag5 = true;
                                                    break;
                                                }
                                                if (num10 > 0 && ("," + dataTable.Rows[index]["FreeGames"].ToString() + ",").Contains("," + num2 + ","))
                                                {
                                                    empty = Guid.Parse(dataTable.Rows[index]["ID"].ToString());
                                                    flag4 = true;
                                                    objPattern.Gift_Pattern_series_List_Update(dataTable.Rows[index]["ID"].ToString(), num10 - 1);
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
                                            int num10 = objcard.Card_UpdatePriceAndBonus_PlayDetails2(str8, num8, num9, Main_ID_GameCenter, str6, num6, num8, num9, IsPersonnel, num2, ID_Swiper, ID_Play_Type, 0, 0, 0, empty);
                                            str2 = str2 + " -L2 -" + num10;
                                        }
                                        else
                                        {
                                            if (Pay_GiftPortion > 0)
                                                ID_Play_Type = 8;
                                            if (num8 + num9 + Pay_GiftPortion + num7 >= num6)
                                            {
                                                int num10 = objPattern.Gift_Pattern_Series_list_Calculate2(str8, num6);
                                                if (num7 > 0)
                                                {
                                                    num10 -= num7;
                                                    objcard.Card_CardProductTiming_SetChargePrice(str8, tuple.Item3, 0);
                                                }
                                                if (num8 >= num10)
                                                {
                                                    int num11 = objcard.Card_UpdatePriceAndBonus_PlayDetails2(str8, num8 - num10, num9, Main_ID_GameCenter, str6, num6, num8 - num10, num9, IsPersonnel, num2, ID_Swiper, ID_Play_Type, num6, 0, Pay_GiftPortion, empty);
                                                    str2 = str2 + " -L2 -" + num11;
                                                }
                                                else
                                                {
                                                    int Pay_BonusPortion = num10 - num8;
                                                    int num11 = num9 - Pay_BonusPortion;
                                                    int num12 = objcard.Card_UpdatePriceAndBonus_PlayDetails2(str8, 0, num11, Main_ID_GameCenter, str6, num6, 0, num11, IsPersonnel, num2, ID_Swiper, ID_Play_Type, num6 - Pay_BonusPortion, Pay_BonusPortion, Pay_GiftPortion, empty);
                                                    str2 = str2 + " -L2 -" + num12;
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
                                    objMain.ErrorLogTemp("Card not Find :" + MacCode);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    objMain.ErrorLog(ex);
                    objMain.ErrorLogTemp("error Process Main +P :exp= " + ex.Message + ",ReciveText=" + ReciveText);
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
                            DataSet macAddrressFfffff = objcard.Card_GetByMacAddrressFFFFFF(str6);
                            DataTable table1 = macAddrressFfffff.Tables[0];
                            DataTable table2 = macAddrressFfffff.Tables[1];
                            if (table1.Rows.Count > 0 && table2.Rows.Count > 0)
                            {
                                str6 = table1.Rows[0]["MacAddress2"].ToString().ToLower();
                                str7 = table2.Rows[0]["MacCode"].ToString();
                            }
                        }
                        string str8 = MacAndTimeStamp_Create(str6);
                        DataTable addressByChargeRate = objSwiper.Swiper_GetByMacAddressByChargeRate(str6.ToUpper());
                        int ID_Games = -1;
                        int ID_Swiper = -1;
                        string str9 = "1";
                        if (addressByChargeRate.Rows.Count > 0)
                        {
                            try
                            {
                                str3_Title = addressByChargeRate.Rows[0]["Title"].ToString();
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
                            DataTable byMacAddrress = objcard.Card_GetByMacAddrress(str7);
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
                                    DataTable byCardGuid = objcard.Card_Play_Details_GetByCardGUID(str6, Card_GUID, Main_ID_GameCenter);
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
                                                int ID_Games_Ticket = objMain.Games_Ticket_Insert(Main_ID_GameCenter, str6, str7, Count, Status, ID_Card_Play_Details, Card_GUID, IsPersonnel, ID_Games, ID_Swiper);
                                                if (str9 == "3")
                                                {
                                                    if (IsPersonnel == 0)
                                                    {
                                                        if (objcard.Card_Ticket_History_insert(Main_ID_GameCenter, -1, Count, Card_GUID, OldCount, 6, ID_Games_Ticket) > 0)
                                                        {
                                                            if (objcard.Card_SetEtickets(Card_GUID, Count + OldCount) > 0)
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
                                                int ID_Games_Ticket = objMain.Games_Ticket_Insert(Main_ID_GameCenter, str6, str7, Count, Status, ID_Card_Play_Details, Card_GUID, IsPersonnel, ID_Games, ID_Swiper);
                                                if (IsPersonnel == 0)
                                                {
                                                    if (str9 == "2")
                                                    {
                                                        str1 = "[" + str8 + "]AT+ok";
                                                        str2 = "[" + str8 + "]AT+ok";
                                                    }
                                                    else if (str9 == "3" && objcard.Card_Ticket_History_insert(Main_ID_GameCenter, -1, Count, Card_GUID, OldCount, 6, ID_Games_Ticket) > 0)
                                                    {
                                                        if (objcard.Card_SetEtickets(Card_GUID, Count + OldCount) > 0)
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
                                            objMain.Games_Ticket_Insert(Main_ID_GameCenter, str6, str7, Count, 3, ID_Card_Play_Details, Card_GUID, IsPersonnel, ID_Games, ID_Swiper);
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
                    objMain.ErrorLog(ex);
                    objMain.ErrorLogTemp("error Process Main +T :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                    return "";
                }
                try
                {
                    if (ReciveText.Contains("Error_conf"))
                    {
                        string str5 = ReciveText.Substring(1, 12);
                        string str6 = str5.Substring(0, 2) + str5.Substring(3, 2) + str5.Substring(6, 2) + str5.Substring(9, 2);
                        //string timeStamp = TimeStamp;
                        string MacAddress = str6;
                        string str7 = MacAndTimeStamp_Create(MacAddress);
                        DataTable addressByChargeRate = objSwiper.Swiper_GetByMacAddressByChargeRate(MacAddress.ToUpper());
                        if (addressByChargeRate.Rows.Count > 0)
                        {
                            try
                            {
                                str3_Title = addressByChargeRate.Rows[0]["Title"].ToString();
                            }
                            catch
                            {
                            }
                            // str1 = "[" + str7 + "]AT+CFG1=" + timeStamp;
                            // str2 = "[" + str7 + "]AT+CFG1=" + timeStamp;
                            str1 = str2 = "[" + str7 + "]AT+CFG1=";
                            objSwiper.Swiper_Update_Config_State(0, MacAddress);
                        }
                    }
                }
                catch (Exception ex)
                {
                    objMain.ErrorLog(ex);
                    objMain.ErrorLogTemp("error Process Main Error_config :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                    return "";
                }
                try
                {
                    if (ReciveText.Contains("HID"))
                    {
                        string str5 = ReciveText.Substring(1, 12);
                        string str6 = str5.Substring(0, 2) + str5.Substring(3, 2) + str5.Substring(6, 2) + str5.Substring(9, 2);
                        //string timeStamp = TimeStamp;
                        string MacAddress = str6;
                        string str7 = MacAndTimeStamp_Create(MacAddress);
                        DataTable addressByChargeRate = objSwiper.Swiper_GetByMacAddressByChargeRate(MacAddress);
                        if (addressByChargeRate.Rows.Count > 0)
                        {
                            try
                            {
                                str3_Title = addressByChargeRate.Rows[0]["Title"].ToString();
                            }
                            catch { }
                            int.Parse(addressByChargeRate.Rows[0]["ID"].ToString());
                            str4_Mac = ReciveText.Split('=')[1].Replace("\r", "").Split(',')[0].ToUpper();
                            DataTable byMacAddrress = objcard.Card_GetByMacAddrress(str4_Mac);
                            if (byMacAddrress.Rows.Count > 0)
                            {
                                bool boolean = Convert.ToBoolean(byMacAddrress.Rows[0]["IsNonPlayGames"].ToString().ToLower() == "" ? "false" : byMacAddrress.Rows[0]["IsNonPlayGames"].ToString());
                                if (Convert.ToBoolean(byMacAddrress.Rows[0]["IsActive"].ToString()))
                                {
                                    if (!boolean)
                                    {
                                        int num1 = int.Parse(byMacAddrress.Rows[0]["ID_Card_Series"].ToString());
                                        string Card_GUID = byMacAddrress.Rows[0]["Card_GUID"].ToString();

                                        if (objcard.Card_CardProductTiming_Get(Card_GUID).Rows.Count <= 0)
                                        {
                                            int num3 = int.Parse(byMacAddrress.Rows[0]["CashPrice"].ToString());
                                            int num4 = int.Parse(byMacAddrress.Rows[0]["BonusPrice"].ToString());
                                            string str8 = "0";
                                            DataTable dataTable = new DataTable();
                                            if (byMacAddrress.Rows.Count > 0)
                                            {
                                                str8 = byMacAddrress.Rows[0]["Etickets"].ToString();
                                                dataTable = objcard.Card_Play_Details_GetByCardGUID(byMacAddrress.Rows[0]["Card_GUID"].ToString());
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
                                                    str11 = dateTime.Hour.ToString() + ":" + dateTime.Minute;
                                                    str12 = objMain.comma(dataTable.Rows[0]["Price"].ToString());
                                                    str13 = objMain.comma(dataTable.Rows[0]["PriceKol"].ToString());
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
                                                    str16 = dateTime.Hour.ToString() + ":" + dateTime.Minute;
                                                    str17 = objMain.comma(dataTable.Rows[1]["Price"].ToString());
                                                    str18 = objMain.comma(dataTable.Rows[1]["PriceKol"].ToString());
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
                                                    str21 = dateTime.Hour.ToString() + ":" + dateTime.Minute;
                                                    str22 = objMain.comma(dataTable.Rows[2]["Price"].ToString());
                                                    str23 = objMain.comma(dataTable.Rows[2]["PriceKol"].ToString());
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
                                                    str26 = dateTime.Hour.ToString() + ":" + dateTime.Minute;
                                                    str28 = objMain.comma(dataTable.Rows[3]["Price"].ToString());
                                                    str29 = objMain.comma(dataTable.Rows[3]["PriceKol"].ToString());
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
                                                    str32 = dateTime.Hour.ToString() + ":" + dateTime.Minute;
                                                    str33 = objMain.comma(dataTable.Rows[4]["Price"].ToString());
                                                    str34 = objMain.comma(dataTable.Rows[4]["PriceKol"].ToString());
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
                    objMain.ErrorLog(ex);
                    objMain.ErrorLogTemp("error Process Main HID :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                    return "";
                }
                try
                {
                    if (ReciveText.Contains("OK_"))
                    {
                        string str5 = ReciveText.Substring(1, 12);
                        string MacAddress = str5.Substring(0, 2) + str5.Substring(3, 2) + str5.Substring(6, 2) + str5.Substring(9, 2);
                        string str6 = MacAndTimeStamp_Create(MacAddress);
                        if (objSwiper.Swiper_GetByMacAddressByChargeRate(MacAddress.ToUpper()).Rows.Count > 0)
                        {
                            str4_Mac = ReciveText.Split('=')[1].Replace("\r", "").Split(',')[0].ToUpper();
                            string str7 = "0";
                            DataTable byMacAddrress = objcard.Card_GetByMacAddrress(str4_Mac);
                            DataTable dataTable = new DataTable();
                            if (byMacAddrress.Rows.Count > 0)
                            {
                                str7 = byMacAddrress.Rows[0]["Etickets"].ToString();
                                dataTable = objcard.Card_Play_Details_GetByCardGUID(byMacAddrress.Rows[0]["Card_GUID"].ToString());
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
                    objMain.ErrorLog(ex);
                    objMain.ErrorLogTemp("error Process Main OKCFG_HID :exp= " + ex.Message + ",ReciveText=" + ReciveText);
                    return "";
                }
                return str1 + "!" + str2 + "!" + str3_Title + "!" + str4_Mac.ToUpper();
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
                objMain.ErrorLogTemp("error Process Main :" + ex.Message);
                return "";
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
            catch { }
        }
    }
}
