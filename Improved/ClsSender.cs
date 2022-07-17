using System.Collections.Generic;
using ClickServerService.Models;
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
        #region ' Variables '

        List<ServerConfigView> serverConfigView = new List<ServerConfigView>();

        readonly Pattern clsPattern = new Pattern();
        readonly MainClass clsMain = new MainClass();
        readonly CardClass clsCard = new CardClass();
        readonly UsersClass clsUser = new UsersClass();
        readonly SwiperClass clsSwiper = new SwiperClass();

        int main_ID_GameCenter = 1, tcp_RepeatCount = 1;

        string txtSend = "";

        #endregion

        public void AppLoadMain()
        {
            try
            {
                clsMain.MyPrint("Send - AppLoadMain", ConsoleColor.Blue);
                txtSend = "";
                clsMain.LoadGameCenterID();
                main_ID_GameCenter = clsMain.ID_GameCenter_Local_Get();

                clsSwiper.Swiper_Update_Config_StateByGameCenterID(main_ID_GameCenter, 0);


                List<ServerConfigView> byGameCenter = clsMain.ServerConfig_GetByGameCenterID(main_ID_GameCenter);
                if (byGameCenter.Any())
                {
                    serverConfigView = byGameCenter;
                    tcp_RepeatCount = serverConfigView.FirstOrDefault().RepeatConfig.Value;

                    foreach (var item in serverConfigView)
                    {
                        clsMain.MyPrint(" ip : " + item.AP_IP);
                    }
                }
                else
                    clsMain.MyPrint("Not find service config. Please config server service", ConsoleColor.DarkRed);
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
            }
        }

        public void Start()
        {
            try
            {
                AppLoadMain();
                while (true)
                {
                    try
                    {
                        Timer_SendData_Tick();
                        Thread.Sleep(300);
                    }
                    catch (Exception ex)
                    {
                        clsMain.ErrorLog(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
            }
        }

        private void Timer_SendData_Tick()
        {
            try
            {
                DataTable storageGetForSend = clsMain.ReceiveStorage_GetForSend();
                for (int index1 = 0; index1 < storageGetForSend.Rows.Count; ++index1)
                {
                    string readDataProcessMain = Send_Process_Main(storageGetForSend.Rows[index1]["ReciveText"].ToString());
                    if (!string.IsNullOrWhiteSpace(readDataProcessMain))
                    {
                        string str2 = readDataProcessMain.Split('!')[0].ToString();
                        readDataProcessMain.Split('!')[1].ToString();
                        string t_SwiperName = readDataProcessMain.Split('!')[2].ToString();
                        string t_CardmacAddress = readDataProcessMain.Split('!')[3].ToString();

                        clsMain.ReceiveStorage_UpdateIsProcess(storageGetForSend.Rows[index1]["ReciveText"].ToString());
                        int tcpRepeatCount = tcp_RepeatCount;
                        if (str2.Contains("AT+PRC"))
                            tcpRepeatCount *= 2;
                        if (str2.Contains("HIDShow$"))
                        {
                            string[] strArray = str2.Split('$');

                            int msTimeOut_BetweenSend = 15, msTimeOut_BetweenCommand = 25;

                            string Command1 = "[" + strArray[1] + "]AT+TPRC=" + strArray[2];
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command1);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                            Thread.Sleep(msTimeOut_BetweenCommand);
                            string Command2 = "[" + strArray[1] + "]AT+TTIK=" + strArray[3];
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command2);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                            Thread.Sleep(msTimeOut_BetweenCommand);
                            string Command3 = "[" + strArray[1] + "]A1=" + strArray[4];
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command3);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                            Thread.Sleep(msTimeOut_BetweenCommand);
                            string Command4 = "[" + strArray[1] + "]A2=" + strArray[5];
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command4);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                            Thread.Sleep(msTimeOut_BetweenCommand);
                            string Command5 = "[" + strArray[1] + "]A3=" + strArray[6];
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command5);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                            Thread.Sleep(msTimeOut_BetweenCommand);
                            string Command6 = "[" + strArray[1] + "]A4=" + strArray[7];
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command6);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                            Thread.Sleep(msTimeOut_BetweenCommand);
                            string Command7 = "[" + strArray[1] + "]A5=" + strArray[8];
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command7);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                            Thread.Sleep(msTimeOut_BetweenCommand);
                            string Command8 = "[" + strArray[1] + "]B1=" + strArray[9];
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command8);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                            Thread.Sleep(msTimeOut_BetweenCommand);
                            string Command9 = "[" + strArray[1] + "]B2=" + strArray[10];
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command9);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                            Thread.Sleep(msTimeOut_BetweenCommand);
                            string Command10 = "[" + strArray[1] + "]B3=" + strArray[11];
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command10);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                            Thread.Sleep(msTimeOut_BetweenCommand);
                            string Command11 = "[" + strArray[1] + "]B4=" + strArray[12];
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command11);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                            Thread.Sleep(msTimeOut_BetweenCommand);
                            string Command12 = "[" + strArray[1] + "]B5=" + strArray[13];
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command12);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                            Thread.Sleep(msTimeOut_BetweenCommand);
                            string Command13 = "[" + strArray[1] + "]C1=" + strArray[14];
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command13);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                            Thread.Sleep(msTimeOut_BetweenCommand);
                            string Command14 = "[" + strArray[1] + "]C2=" + strArray[15];
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command14);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                            Thread.Sleep(msTimeOut_BetweenCommand);
                            string Command15 = "[" + strArray[1] + "]C3=" + strArray[16];
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command15);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                            Thread.Sleep(msTimeOut_BetweenCommand);
                            string Command16 = "[" + strArray[1] + "]C4=" + strArray[17];
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command16);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                            Thread.Sleep(msTimeOut_BetweenCommand);
                            string Command17 = "[" + strArray[1] + "]C5=" + strArray[18];
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command17);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                            Thread.Sleep(msTimeOut_BetweenCommand);
                            string Command18 = "[" + strArray[1] + "]D1=" + strArray[19];
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command18);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                            Thread.Sleep(msTimeOut_BetweenCommand);
                            string Command19 = "[" + strArray[1] + "]D2=" + strArray[20];
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command19);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                            Thread.Sleep(msTimeOut_BetweenCommand);
                            string Command20 = "[" + strArray[1] + "]D3=" + strArray[21];
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command20);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                            Thread.Sleep(msTimeOut_BetweenCommand);
                            string Command21 = "[" + strArray[1] + "]D4=" + strArray[22];
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command21);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                            Thread.Sleep(msTimeOut_BetweenCommand);
                            string Command22 = "[" + strArray[1] + "]D5=" + strArray[23];
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command22);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                            Thread.Sleep(msTimeOut_BetweenCommand);
                            string Command23 = "[" + strArray[1] + "]E1=" + strArray[24];
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command23);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                            Thread.Sleep(msTimeOut_BetweenCommand);
                            string Command24 = "[" + strArray[1] + "]E2=" + strArray[25];
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command24);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                            Thread.Sleep(msTimeOut_BetweenCommand);
                            string Command25 = "[" + strArray[1] + "]E3=" + strArray[26];
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command25);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                            Thread.Sleep(msTimeOut_BetweenCommand);
                            string Command26 = "[" + strArray[1] + "]E4=" + strArray[27];
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command26);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                            Thread.Sleep(msTimeOut_BetweenCommand);
                            string Command27 = "[" + strArray[1] + "]E5=" + strArray[28];
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command27);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                            Thread.Sleep(msTimeOut_BetweenCommand);
                            string Command28 = "[" + strArray[1] + "]END-DATA";
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                Send_Main(Command28);
                                Thread.Sleep(msTimeOut_BetweenSend);
                            }
                        }
                        else
                        {
                            for (int index2 = 0; index2 < tcpRepeatCount; ++index2)
                            {
                                foreach (var item in serverConfigView)
                                {
                                    Send_DisplayText(str2, $"P{item.AP_ID}", t_SwiperName, t_CardmacAddress, DateTime.Now);
                                    Send_Main(item.AP_IP, str2, ClsStarter.tCPClientList.FirstOrDefault(i => i.AP_ID == item.AP_ID).TCPClient);
                                }
                                Thread.Sleep(10);
                            }
                        }
                        Swiper byState = clsSwiper.Swiper_GetByState();
                        if (byState != null)
                        {
                            string str3 = ((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString();
                            string str4 = str3.Substring(str3.Length - 5, 4);
                            string Command = "[" + MacAndTimeStamp_Create(byState.MacAddress.ToLower()) + "]AT+CFG1=" + str4;
                            for (int index3 = 0; index3 < tcp_RepeatCount + 2; ++index3)
                            {
                                foreach (var item in serverConfigView)
                                {
                                    Send_DisplayText(str2, $"P{item.AP_ID}", "", "");
                                    Send_Main(item.AP_IP, Command, ClsStarter.tCPClientList.FirstOrDefault(i => i.AP_ID == item.AP_ID).TCPClient);
                                    Thread.Sleep(10);
                                }
                                Thread.Sleep(100);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
            }
        }

        private void Send_DisplayText(string SendData, string TcpIPName, string t_SwiperName, string t_CardmacAddress, DateTime? sendTime = null)
        {
            try
            {
                if (txtSend.Length > 10000)
                {
                    txtSend = "";
                }
                if (sendTime == null)
                    sendTime = DateTime.Now;
                string sendTimeString = sendTime.Value.ToString("HH:mm:ss:fff");
                if (!serverConfigView.FirstOrDefault().IsShowAllSend.Value)
                {
                    if (SendData.Length <= 0 || txtSend.Contains(TcpIPName + "-" + SendData))
                        return;
                    WriteToFile_SendLog(TcpIPName + "-" + sendTimeString + "-" + SendData + "-" + t_SwiperName + "-" + t_CardmacAddress, sendTime.Value);
                    txtSend += TcpIPName + "-" + SendData + sendTimeString + "-" + t_SwiperName + t_CardmacAddress;
                }
                else if (SendData.Length > 0)
                {
                    WriteToFile_SendLog(TcpIPName + "-" + sendTimeString + "-" + SendData + "-" + t_SwiperName + "-" + t_CardmacAddress, sendTime.Value);
                    txtSend += TcpIPName + "-" + SendData + sendTimeString + "-" + t_SwiperName + t_CardmacAddress;
                }
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
            }
        }

        public void Send_Main(string command)
        {
            try
            {
                foreach (var item in ClsStarter.tCPClientList)
                {
                    try
                    {
                        bool isAllowedToSend = false;
                        var swiper = SwiperClass.Swipers.FirstOrDefault(mc => mc.MacAddress.ToUpper() == clsSwiper.GetMacSwiper(command).ToUpper());
                        if (swiper != null)
                        {
                            if (swiper.ID_Swiper_Segment != null)
                                isAllowedToSend = ClsStarter.accessPoints.Where(ap => ap.AP_ID == item.AP_ID).FirstOrDefault().ListSwiperSegmentIDs.Contains(swiper.ID_Swiper_Segment.Value.ToString());
                        }
                        if (isAllowedToSend)
                        {
                            if (item.TCPClient.Connected)
                            {
                                NetworkStream stream = item.TCPClient.GetStream();
                                byte[] bytes = Encoding.ASCII.GetBytes(command);
                                stream.Write(bytes, 0, bytes.Length);
                                clsMain.MyPrint($"+S%Send%{item.TCPClient.Client.RemoteEndPoint}%{command}", ConsoleColor.DarkYellow, DateTime.Now);
                            }
                        }
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
            }
        }

        public void Send_Main(string ipAp, string command, TcpClient client)
        {
            try
            {
                bool isAllowedToSend = false;
                var swiper = SwiperClass.Swipers.FirstOrDefault(mc => mc.MacAddress.ToUpper() == clsSwiper.GetMacSwiper(command).ToUpper());
                if (swiper != null)
                {
                    if (swiper.ID_Swiper_Segment != null)
                    {
                        int id_ap = ClsStarter.accessPoints.Where(i => i.AP_IP == ipAp).FirstOrDefault().AP_ID;
                        isAllowedToSend = ClsStarter.accessPoints.Where(ap => ap.AP_ID == id_ap).FirstOrDefault().ListSwiperSegmentIDs.Contains(swiper.ID_Swiper_Segment.Value.ToString());
                    }
                }
                if (isAllowedToSend)
                {
                    if (client.Connected)
                    {
                        NetworkStream stream = client.GetStream();
                        byte[] bytes = Encoding.ASCII.GetBytes(command);
                        stream.Write(bytes, 0, bytes.Length);
                        clsMain.MyPrint($"+S%Send%{ipAp}%{command}", ConsoleColor.DarkYellow, DateTime.Now);
                    }
                }
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
            }
        }

        public string MacAndTimeStamp_Create(string macAddress)
        {
            try
            {
                string str1 = ((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString();
                string str2 = str1.Substring(str1.Length - 5, 4);
                char ch1 = macAddress[0];
                string str3 = ch1.ToString();
                ch1 = macAddress[1];
                string str4 = ch1.ToString();
                string str5 = str3 + str4 + str2[0].ToString();
                char ch2 = macAddress[2];
                string str6 = ch2.ToString();
                ch2 = macAddress[3];
                string str7 = ch2.ToString();
                string str8 = str5 + str6 + str7 + str2[1].ToString();
                char ch3 = macAddress[4];
                string str9 = ch3.ToString();
                ch3 = macAddress[5];
                string str10 = ch3.ToString();
                string str11 = str8 + str9 + str10 + str2[2].ToString();
                char ch4 = macAddress[6];
                string str12 = ch4.ToString();
                ch4 = macAddress[7];
                string str13 = ch4.ToString();
                return str11 + str12 + str13 + str2[3].ToString();
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
                return "";
            }
        }

        public string Send_Process_Main(string receiveText)
        {
            try
            {
                string strCommandOne = "", strCommandTwo = "", str3_Title = "", str4_Mac = "";
                receiveText = receiveText.Trim();

                if (receiveText.Contains("CONFIG"))
                {
                    try
                    {
                        string str5 = receiveText.Substring(1, 12);
                        string macAddress = str5.Substring(0, 2) + str5.Substring(3, 2) + str5.Substring(6, 2) + str5.Substring(9, 2);
                        string str7 = ((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString();
                        string str8 = str7.Substring(str7.Length - 5, 4);
                        string str9 = macAddress[0].ToString() + macAddress[1].ToString();
                        char ch = str8[0];
                        string str10 = ch.ToString();
                        string str11 = str9 + str10;
                        ch = macAddress[2];
                        string str12 = ch.ToString();
                        ch = macAddress[3];
                        string str13 = ch.ToString();
                        string str14 = str11 + str12 + str13;
                        ch = str8[1];
                        string str15 = ch.ToString();
                        string str16 = str14 + str15;
                        ch = macAddress[4];
                        string str17 = ch.ToString();
                        ch = macAddress[5];
                        string str18 = ch.ToString();
                        string str19 = str16 + str17 + str18;
                        ch = str8[2];
                        string str20 = ch.ToString();
                        string str21 = str19 + str20;
                        ch = macAddress[6];
                        string str22 = ch.ToString();
                        ch = macAddress[7];
                        string str23 = ch.ToString();
                        string str24 = str21 + str22 + str23;
                        ch = str8[3];
                        string str25 = ch.ToString();
                        string macStamp = str24 + str25;
                        Swipers_ChargeRate addressByChargeRate = SwiperClass.Swipers_ChargeRate.FirstOrDefault(mac => mac.MacAddress.ToUpper() == macAddress.ToUpper());
                        if (addressByChargeRate != null)
                        {
                            str3_Title = addressByChargeRate.Title;
                            strCommandOne = "[" + macStamp + "]AT+CFG1=" + str8;
                            strCommandTwo = "[" + macStamp + "]AT+CFG1=" + str8;
                        }
                    }
                    catch (Exception ex)
                    {
                        clsMain.ErrorLog(ex);
                        clsMain.ErrorLogTemp($"Error Process Main CONFIG :exp= {ex.Message},ReceiveText={receiveText}");
                        return "";
                    }
                }
                else if (receiveText.Contains("OKCFG"))
                {
                    try
                    {
                        string str5 = receiveText.Substring(1, 12);
                        string macAddress = str5.Substring(0, 2) + str5.Substring(3, 2) + str5.Substring(6, 2) + str5.Substring(9, 2);
                        string macStamp = MacAndTimeStamp_Create(macAddress);
                        Swipers_ChargeRate addressByChargeRate = SwiperClass.Swipers_ChargeRate.FirstOrDefault(mac => mac.MacAddress == macAddress.ToUpper());
                        if (addressByChargeRate != null)
                        {
                            str3_Title = addressByChargeRate.Title;
                            int num1 = addressByChargeRate.Config_State.Value;

                            if (receiveText.Contains("OKCFG1"))
                            {
                                clsSwiper.Swiper_Update_Config_State(1, macAddress);
                                string ttle = addressByChargeRate.Title.Length > 8 ? addressByChargeRate.Title.Substring(0, 8) : addressByChargeRate.Title;
                                strCommandOne = "[" + macStamp + "]AT+CFG2=" + ttle;
                                strCommandTwo = "[" + macStamp + "]AT+CFG2=" + ttle;
                            }
                            else if (receiveText.Contains("OKCFG2"))
                            {
                                clsSwiper.Swiper_Update_Config_State(2, macAddress);
                                if (MainClass.key_Value_List.Select("KeyName ='Enable_Charge_Rate'")[0]["Value"].ToString().ToLower() == "true")
                                {
                                    strCommandOne = "[" + macStamp + "]AT+CFG3=" + clsMain.Comma(addressByChargeRate.PriceAdi);
                                    strCommandTwo = "[" + macStamp + "]AT+CFG3=" + clsMain.Comma(addressByChargeRate.PriceAdi);
                                }
                                else
                                {
                                    strCommandOne = "[" + macStamp + "]AT+CFG3=" + clsMain.Comma(addressByChargeRate.Price1.Value.ToString());
                                    strCommandTwo = "[" + macStamp + "]AT+CFG3=" + clsMain.Comma(addressByChargeRate.Price1.Value.ToString());
                                }
                            }
                            else if (receiveText.Contains("OKCFG3"))
                            {
                                if (num1 != -3)
                                {
                                    clsSwiper.Swiper_Update_Config_State(3, macAddress);
                                    if (MainClass.key_Value_List.Select("KeyName ='Enable_Charge_Rate'")[0]["Value"].ToString().ToLower() == "true")
                                    {
                                        strCommandOne = "[" + macStamp + "]AT+CFG4=" + clsMain.Comma(addressByChargeRate.PriceVije);
                                        strCommandTwo = "[" + macStamp + "]AT+CFG4=" + clsMain.Comma(addressByChargeRate.PriceVije);
                                    }
                                    else
                                    {
                                        strCommandOne = "[" + macStamp + "]AT+CFG4=" + clsMain.Comma(addressByChargeRate.Price2.Value.ToString());
                                        strCommandTwo = "[" + macStamp + "]AT+CFG4=" + clsMain.Comma(addressByChargeRate.Price2.Value.ToString());
                                    }
                                }
                                else
                                {
                                    strCommandOne = "[" + macStamp + "]AT+ok";
                                    strCommandTwo = "[" + macStamp + "]AT+ok";
                                }
                            }
                            else if (receiveText.Contains("OKCFG4"))
                            {
                                if (num1 != -3)
                                {
                                    clsSwiper.Swiper_Update_Config_State(4, macAddress);
                                    strCommandOne = "[" + macStamp + "]AT+CFG5=" + addressByChargeRate.Delay1 + "," + addressByChargeRate.Delay2;
                                    strCommandTwo = "[" + macStamp + "]AT+CFG5=" + addressByChargeRate.Delay1 + "," + addressByChargeRate.Delay2;
                                }
                                else
                                {
                                    strCommandOne = "[" + macStamp + "]AT+ok";
                                    strCommandTwo = "[" + macStamp + "]AT+ok";
                                    clsSwiper.Swiper_UpdateStateByMacAddress(macAddress.ToUpper(), 0);
                                }
                            }
                            else if (receiveText.Contains("OKCFG5"))
                            {
                                string strVersn = addressByChargeRate.Version;
                                clsSwiper.Swiper_Update_Config_State(5, macAddress);
                                string str8 = "";
                                if (Convert.ToInt32(addressByChargeRate.Pulse) < 100)
                                    str8 = addressByChargeRate.Pulse;
                                else
                                {
                                    switch (addressByChargeRate.Pulse)
                                    {
                                        case "100": str8 = "01"; break;
                                        case "200": str8 = "02"; break;
                                        case "300": str8 = "03"; break;
                                        case "400": str8 = "04"; break;
                                        case "500": str8 = "05"; break;
                                        case "600": str8 = "06"; break;
                                        case "700": str8 = "07"; break;
                                        case "800": str8 = "08"; break;
                                        case "900": str8 = "09"; break;
                                        default: str8 = "00"; break;
                                    }
                                }
                                bool flag1_State = false, flag2_ClassStatus = false, flag3_GroupsStatus = false, flag4_SegmentStatus = false;
                                try
                                {
                                    flag1_State = Convert.ToBoolean(addressByChargeRate.State);
                                    flag2_ClassStatus = Convert.ToBoolean(addressByChargeRate.ClassStatus);
                                    flag3_GroupsStatus = Convert.ToBoolean(addressByChargeRate.GroupsStatus);
                                    flag4_SegmentStatus = Convert.ToBoolean(addressByChargeRate.SegmentStatus);
                                }
                                catch { }
                                string str9 = "0";
                                if (flag1_State & flag2_ClassStatus & flag3_GroupsStatus & flag4_SegmentStatus)
                                    str9 = strVersn;
                                strCommandOne = "[" + macStamp + "]AT+CFG6=" + str8 + "," + addressByChargeRate.RepeatCount + "," + str9;
                                strCommandTwo = "[" + macStamp + "]AT+CFG6=" + str8 + "," + addressByChargeRate.RepeatCount + "," + str9;
                            }
                            else if (receiveText.Contains("OKCFG6"))
                            {
                                try
                                {
                                    string strVersn = addressByChargeRate.Version;
                                    if (strVersn == "1")
                                        clsSwiper.Swiper_Update_Config_State(0, macAddress);
                                    else if (strVersn == "2" || strVersn == "3")
                                    {
                                        int pulseTyp = Convert.ToInt32(addressByChargeRate.PulseType);
                                        string str8 = addressByChargeRate.Start_Count_Voltage;
                                        string str9 = addressByChargeRate.TicketErrorStop;
                                        string str10 = addressByChargeRate.PullUp;
                                        clsSwiper.Swiper_Update_Config_State(6, macAddress);
                                        strCommandOne = "[" + macStamp + "]AT+CFG7=" + pulseTyp + "," + str8 + "," + str9 + "," + str10;
                                        strCommandTwo = "[" + macStamp + "]AT+CFG7=" + pulseTyp + "," + str8 + "," + str9 + "," + str10;
                                    }
                                }
                                catch
                                {
                                    clsSwiper.Swiper_Update_Config_State(0, macAddress);
                                }
                            }
                            else if (receiveText.Contains("OKCFG7"))
                            {
                                strCommandOne = "[" + macStamp + "]AT+ok";
                                strCommandTwo = "[" + macStamp + "]AT+ok";
                                clsSwiper.Swiper_Update_Config_State(0, macAddress);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        clsMain.ErrorLog(ex);
                        clsMain.ErrorLogTemp($"Error Process Main OKCFG :exp= {ex.Message},ReceiveText={receiveText}");
                        return "";
                    }
                }
                else if (receiveText.Contains("CID"))
                {
                    string str27_Error = "1515";
                    try
                    {
                        string str5 = receiveText.Substring(1, 12);
                        string macAddress = str5.Substring(0, 2) + str5.Substring(3, 2) + str5.Substring(6, 2) + str5.Substring(9, 2);
                        string macStamp = MacAndTimeStamp_Create(macAddress);
                        Swipers_ChargeRate addressByChargeRate = SwiperClass.Swipers_ChargeRate.FirstOrDefault(mac => mac.MacAddress == macAddress.ToUpper());
                        str27_Error = "1524";
                        if (addressByChargeRate != null)
                        {
                            int ID_Play_Type = 1;
                            str3_Title = addressByChargeRate.Title;
                            int gamesID = Convert.ToInt32(addressByChargeRate.ID_Games);

                            int swiperID = Convert.ToInt32(addressByChargeRate.ID);
                            str4_Mac = receiveText.Split('=')[1].Replace("\r", "").Split(',')[0].ToUpper();
                            str27_Error = "1537";
                            DataTable byMacAddrress = clsCard.Card_GetByMacAddrress(str4_Mac);
                            if (byMacAddrress.Rows.Count > 0)
                            {
                                str27_Error = "1543";
                                Guid empty = Guid.Empty;
                                int num2_CardStatusID = Convert.ToInt32(byMacAddrress.Rows[0]["ID_Card_Status"]);
                                str27_Error = "1546";
                                bool flag1 = false;
                                try
                                {
                                    flag1 = Convert.ToBoolean(byMacAddrress.Rows[0]["IsNonTicket"]);
                                }
                                catch { }
                                str27_Error = "1548";
                                bool.Parse(byMacAddrress.Rows[0]["AllowRegistration"].ToString());
                                int num3 = 0;
                                try
                                {
                                    num3 = int.Parse(byMacAddrress.Rows[0]["ID_Card_Promotional"].ToString());
                                }
                                catch { }
                                str27_Error = "1556";
                                bool boolean = Convert.ToBoolean(byMacAddrress.Rows[0]["IsNonPlayGames"].ToString().ToLower() == "" ? "false" : byMacAddrress.Rows[0]["IsNonPlayGames"].ToString());
                                str27_Error = "1558";
                                bool flag2 = Convert.ToBoolean(byMacAddrress.Rows[0]["IsActive"].ToString());
                                str27_Error = "1560";
                                if (int.Parse(byMacAddrress.Rows[0]["ID_Card_Series"].ToString()) == 3 && clsUser.Users_GetByGUID(byMacAddrress.Rows[0]["Card_GUID"].ToString()).Rows.Count == 0)
                                    flag2 = false;
                                str27_Error = "1569";
                                if (flag2)
                                {
                                    if (!boolean)
                                    {
                                        str27_Error = "B1580";
                                        int num4 = int.Parse(byMacAddrress.Rows[0]["ID_Card_Series"].ToString());
                                        string str8 = byMacAddrress.Rows[0]["Card_GUID"].ToString();
                                        string str9 = byMacAddrress.Rows[0]["ID_Club_Member_Type"].ToString();
                                        bool flag3 = !(str9 == "") && !(str9 == "0");
                                        str27_Error = "B1590";
                                        int num5_price;
                                        if (MainClass.key_Value_List.Select("KeyName ='Enable_Charge_Rate'")[0]["Value"].ToString().ToLower() == "true")
                                        {
                                            num5_price = (num2_CardStatusID != 2) ? Convert.ToInt32(addressByChargeRate.PriceAdi) : Convert.ToInt32(addressByChargeRate.PriceVije);
                                            if (flag3)
                                                num5_price = Convert.ToInt32(addressByChargeRate.PriceVije);
                                        }
                                        else
                                        {
                                            num5_price = (num2_CardStatusID != 2) ? Convert.ToInt32(addressByChargeRate.Price1) : Convert.ToInt32(addressByChargeRate.Price2);
                                            if (flag3)
                                                num5_price = Convert.ToInt32(addressByChargeRate.Price2);
                                        }
                                        str27_Error = "B1622";
                                        int IsPersonnel = 0;
                                        if (num4 == 3)
                                        {
                                            ID_Play_Type = 4;
                                            IsPersonnel = 1;
                                        }
                                        str27_Error = "B1629";
                                        bool flag4 = false;
                                        int num6 = 0;
                                        Tuple<bool, int, int, int, int, bool, string> tuple = clsCard.Card_CardProductTiming_Status(str8, gamesID);
                                        str27_Error = "B1633";
                                        int num7;
                                        if (tuple.Item1)
                                        {
                                            string str10 = tuple.Item7;
                                            string str11 = str10.Split(',')[0];
                                            string str12 = str10.Split(',')[1];
                                            string s = str10.Split(',')[2];
                                            str27_Error = "B1640";
                                            if (tuple.Item6)
                                            {
                                                str27_Error = "B1643";
                                                flag4 = true;
                                                ID_Play_Type = 10;
                                                strCommandOne = "[" + macStamp + "]AT+PRC=" + (int.Parse(s) - 1).ToString() + "-" + str12;
                                                strCommandTwo = "[" + macStamp + "]AT+PRC=" + (int.Parse(s) - 1).ToString() + "-" + str12;
                                                if (!serverConfigView.FirstOrDefault().IsDecreasePriceInLevel2.Value)
                                                {
                                                    int num8 = clsCard.Card_Play_Details_Insert(str8, 0, 0, main_ID_GameCenter, macAddress, num5_price, 0, 0, IsPersonnel, gamesID, swiperID, ID_Play_Type, 0, 0, 0, empty);
                                                    strCommandTwo = strCommandTwo + " -L1 -" + num8;
                                                }
                                            }
                                            else if (tuple.Item5 > 0)
                                            {
                                                str27_Error = "B1656";
                                                flag4 = true;
                                                ID_Play_Type = 9;
                                                strCommandOne = "[" + macStamp + "]AT+PRC=" + (tuple.Item5 - 1).ToString() + " - " + str11;
                                                strCommandTwo = "[" + macStamp + "]AT+PRC=" + (tuple.Item5 - 1).ToString() + "-" + str11;
                                                if (!serverConfigView.FirstOrDefault().IsDecreasePriceInLevel2.Value)
                                                {
                                                    int num8 = clsCard.Card_Play_Details_Insert(str8, 0, 0, main_ID_GameCenter, macAddress, num5_price, 0, 0, IsPersonnel, gamesID, swiperID, ID_Play_Type, 0, 0, 0, empty);
                                                    strCommandTwo = strCommandTwo + " -L1 -" + num8;
                                                    clsCard.Card_CardProductTiming_SetFreeGame(str8, tuple.Item3, tuple.Item5 - 1);
                                                }
                                            }
                                            else
                                            {
                                                str27_Error = "B1670";
                                                if (tuple.Item4 > 0)
                                                {
                                                    ID_Play_Type = 11;
                                                    if (tuple.Item2 >= num5_price)
                                                    {
                                                        str27_Error = "B1677";
                                                        flag4 = true;
                                                        strCommandTwo = "[" + macStamp + "]AT+PRC=" + clsMain.Comma((tuple.Item2 - num5_price).ToString());
                                                        string str13 = macStamp;
                                                        MainClass clsMain1 = clsMain;
                                                        num7 = tuple.Item2 - num5_price;
                                                        string str14 = num7.ToString();
                                                        string str15 = clsMain1.Comma(str14);
                                                        strCommandOne = "[" + str13 + "]AT+PRC=" + str15;
                                                        if (!serverConfigView.FirstOrDefault().IsDecreasePriceInLevel2.Value)
                                                        {
                                                            int num8 = clsCard.Card_Play_Details_Insert(str8, 0, 0, main_ID_GameCenter, macAddress, num5_price, 0, 0, IsPersonnel, gamesID, swiperID, ID_Play_Type, 0, 0, 0, empty);
                                                            clsCard.Card_CardProductTiming_SetChargePrice(str8, tuple.Item3, tuple.Item2 - num5_price);
                                                            strCommandTwo = strCommandTwo + " -L1 -" + num8;
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
                                                    str27_Error = "B1697";
                                                    flag4 = true;
                                                    ID_Play_Type = 5;
                                                    strCommandOne = "[" + macStamp + "]AT+PRC=--";
                                                    strCommandTwo = "[" + macStamp + "]AT+PRC=--";
                                                    if (!serverConfigView.FirstOrDefault().IsDecreasePriceInLevel2.Value)
                                                    {
                                                        int num8 = clsCard.Card_Play_Details_Insert(str8, 0, 0, main_ID_GameCenter, macAddress, num5_price, 0, 0, IsPersonnel, gamesID, swiperID, ID_Play_Type, 0, 0, 0, empty);
                                                        strCommandTwo = strCommandTwo + " -L1 -" + num8;
                                                    }
                                                }
                                            }
                                        }
                                        str27_Error = "1686";
                                        if (!flag4)
                                        {
                                            int num8 = int.Parse(byMacAddrress.Rows[0]["CashPrice"].ToString());
                                            int num9 = int.Parse(byMacAddrress.Rows[0]["BonusPrice"].ToString());
                                            DataTable dataTable = clsPattern.Gift_series_List_ActiveByCard_GUID(str8);
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
                                                    if (num11 > 0 && ("," + dataTable.Rows[index]["FreeDailyGames"].ToString() + ",").Contains("," + gamesID + ",") && clsCard.Card_Play_Details_Get_Today(str8, dataTable.Rows[index]["FreeDailyGames"].ToString()).Rows.Count < num11)
                                                    {
                                                        empty = Guid.Parse(dataTable.Rows[index]["ID"].ToString());
                                                        flag6 = true;
                                                        break;
                                                    }
                                                    if (num10 > 0 && ("," + dataTable.Rows[index]["FreeGames"].ToString() + ",").Contains("," + gamesID + ","))
                                                    {
                                                        empty = Guid.Parse(dataTable.Rows[index]["ID"].ToString());
                                                        flag5 = true;
                                                        if (!serverConfigView.FirstOrDefault().IsDecreasePriceInLevel2.Value)
                                                        {
                                                            clsPattern.Gift_Pattern_series_List_Update(dataTable.Rows[index]["ID"].ToString(), num10 - 1);
                                                            break;
                                                        }
                                                        break;
                                                    }
                                                }
                                            }
                                            str27_Error = "1736";
                                            if (flag5)
                                            {
                                                ID_Play_Type = 2;
                                                num5_price = 0;
                                            }
                                            if (flag6)
                                            {
                                                ID_Play_Type = 3;
                                                num5_price = 0;
                                            }
                                            if (num3 > 0)
                                                ID_Play_Type = 7;
                                            if (flag6 | flag5)
                                            {
                                                strCommandOne = "[" + macStamp + "]AT+PRC=---";
                                                strCommandTwo = "[" + macStamp + "]AT+PRC=---";
                                                if (!serverConfigView.FirstOrDefault().IsDecreasePriceInLevel2.Value)
                                                {
                                                    int num10 = clsCard.Card_UpdatePriceAndBonus_PlayDetails2(str8, num8, num9, main_ID_GameCenter, macAddress, num5_price, num8, num9, IsPersonnel, gamesID, swiperID, ID_Play_Type, 0, 0, 0, empty);
                                                    strCommandTwo = strCommandTwo + " -L1 -" + num10;
                                                }
                                            }
                                            else
                                            {
                                                if (Pay_GiftPortion > 0)
                                                    ID_Play_Type = 8;
                                                if (num8 + num9 + Pay_GiftPortion + num6 >= num5_price)
                                                {
                                                    int num10 = num8 + Pay_GiftPortion + num9 + num6 - num5_price;
                                                    strCommandTwo = "[" + macStamp + "]AT+PRC=" + clsMain.Comma(num10.ToString());
                                                    string str10 = num10.ToString().Length > 7 ? num10.ToString() : clsMain.Comma(num10.ToString());
                                                    strCommandOne = "[" + macStamp + "]AT+PRC=" + str10;
                                                    if (!serverConfigView.FirstOrDefault().IsDecreasePriceInLevel2.Value)
                                                    {
                                                        int num11 = clsPattern.Gift_Pattern_Series_list_Calculate2(str8, num5_price);
                                                        if (num6 > 0)
                                                        {
                                                            num11 -= num6;
                                                            clsCard.Card_CardProductTiming_SetChargePrice(str8, tuple.Item3, 0);
                                                        }
                                                        if (num8 >= num11)
                                                        {
                                                            int num12 = clsCard.Card_UpdatePriceAndBonus_PlayDetails2(str8, num8 - num11, num9, main_ID_GameCenter, macAddress, num5_price, num8 - num11, num9, IsPersonnel, gamesID, swiperID, ID_Play_Type, num5_price, 0, Pay_GiftPortion, empty);
                                                            strCommandTwo = strCommandTwo + " -L1 -" + num12;
                                                        }
                                                        else
                                                        {
                                                            int Pay_BonusPortion = num11 - num8;
                                                            int num12 = num9 - Pay_BonusPortion;
                                                            int num13 = clsCard.Card_UpdatePriceAndBonus_PlayDetails2(str8, 0, num12, main_ID_GameCenter, macAddress, num5_price, 0, num12, IsPersonnel, gamesID, swiperID, ID_Play_Type, num5_price - Pay_BonusPortion, Pay_BonusPortion, Pay_GiftPortion, empty);
                                                            strCommandTwo = strCommandTwo + " -L1 -" + num13;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    string str10 = macStamp;
                                                    MainClass clsMain1 = clsMain;
                                                    num7 = num8 + num9 + num6;
                                                    string str11 = num7.ToString();
                                                    string str12 = clsMain1.Comma(str11);
                                                    strCommandTwo = "[" + str10 + "]AT+NRC=" + str12;
                                                    string str13 = macStamp;
                                                    MainClass clsMain2 = clsMain;
                                                    num7 = num8 + num9 + num6;
                                                    string str14 = num7.ToString();
                                                    string str15 = clsMain2.Comma(str14);
                                                    strCommandOne = "[" + str13 + "]AT+NRC=" + str15;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        strCommandOne = "[" + macStamp + "]AT+DISABLE";
                                        strCommandTwo = "[" + macStamp + "]AT+DISABLE";
                                    }
                                }
                                else
                                {
                                    strCommandOne = "[" + macStamp + "]AT+DISABLE";
                                    strCommandTwo = "[" + macStamp + "]AT+DISABLE";
                                }
                            }
                            else
                            {
                                strCommandOne = "[" + macStamp + "]AT+INVALID";
                                strCommandTwo = "[" + macStamp + "]AT+INVALID";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        clsMain.ErrorLog(ex);
                        clsMain.ErrorLogTemp($"Error Process Main CID :exp= {ex.Message},ReceiveText={receiveText},LineCode={str27_Error}");
                        return "";
                    }
                }
                else if (receiveText.Contains("+P="))
                {
                    try
                    {
                        string str5 = receiveText.Substring(1, 12);
                        string macAddress = str5.Substring(0, 2) + str5.Substring(3, 2) + str5.Substring(6, 2) + str5.Substring(9, 2);
                        string macStamp = MacAndTimeStamp_Create(macAddress);
                        string MacCode = receiveText.Split('=')[1].Replace("\r", "").Split(',')[0];
                        Swipers_ChargeRate addressByChargeRate = SwiperClass.Swipers_ChargeRate.FirstOrDefault(mac => mac.MacAddress == macAddress.ToUpper());
                        int num1 = 0;
                        if (addressByChargeRate != null)
                        {
                            str3_Title = addressByChargeRate.Title + ":" + num1 + ":";
                            str4_Mac = MacCode;
                        }
                        if (serverConfigView.FirstOrDefault().IsDecreasePriceInLevel2.Value)
                        {
                            strCommandOne = "[" + macStamp + "]AT+ok";
                            strCommandTwo = "[" + macStamp + "]AT+ok";
                            int ID_Play_Type = 1;
                            if (addressByChargeRate != null)
                            {
                                str3_Title = addressByChargeRate.Title + ":" + num1 + ":";
                                int num2 = Convert.ToInt32(addressByChargeRate.ID_Games);
                                int ID_Swiper = Convert.ToInt32(addressByChargeRate.ID);
                                DataTable byMacAddrress = clsCard.Card_GetByMacAddrress(MacCode);
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
                                    int num6_Price;
                                    if (MainClass.key_Value_List.Select("KeyName ='Enable_Charge_Rate'")[0]["Value"].ToString().ToLower() == "true")
                                    {
                                        num6_Price = (num3 != 2) ? Convert.ToInt32(addressByChargeRate.PriceAdi) : Convert.ToInt32(addressByChargeRate.PriceVije);
                                        if (flag2)
                                            num6_Price = Convert.ToInt32(addressByChargeRate.PriceVije);
                                    }
                                    else
                                    {
                                        num6_Price = (num3 != 2) ? Convert.ToInt32(addressByChargeRate.Price1) : Convert.ToInt32(addressByChargeRate.Price2);
                                        if (flag2)
                                            num6_Price = Convert.ToInt32(addressByChargeRate.Price2);
                                    }
                                    int IsPersonnel = 0;
                                    if (num4 == 3)
                                    {
                                        ID_Play_Type = 4;
                                        IsPersonnel = 1;
                                    }
                                    bool flag3 = false;
                                    int num7 = 0;
                                    Tuple<bool, int, int, int, int, bool, string> tuple = clsCard.Card_CardProductTiming_Status(str8, num2);
                                    if (tuple.Item1)
                                    {
                                        if (tuple.Item6)
                                        {
                                            int num8 = clsCard.Card_Play_Details_Insert(str8, 0, 0, main_ID_GameCenter, macAddress, num6_Price, 0, 0, IsPersonnel, num2, ID_Swiper, ID_Play_Type, 0, 0, 0, empty);
                                            strCommandTwo = strCommandTwo + " -L2 -" + num8;
                                            flag3 = true;
                                            ID_Play_Type = 10;
                                        }
                                        else if (tuple.Item5 > 0)
                                        {
                                            flag3 = true;
                                            ID_Play_Type = 9;
                                            int num8 = clsCard.Card_Play_Details_Insert(str8, 0, 0, main_ID_GameCenter, macAddress, num6_Price, 0, 0, IsPersonnel, num2, ID_Swiper, ID_Play_Type, 0, 0, 0, empty);
                                            strCommandTwo = strCommandTwo + " -L2 -" + num8;
                                            clsCard.Card_CardProductTiming_SetFreeGame(str8, tuple.Item3, tuple.Item5 - 1);
                                        }
                                        else if (tuple.Item4 > 0)
                                        {
                                            if (tuple.Item2 >= num6_Price)
                                            {
                                                flag3 = true;
                                                ID_Play_Type = 11;
                                                int num8 = clsCard.Card_Play_Details_Insert(str8, 0, 0, main_ID_GameCenter, macAddress, num6_Price, 0, 0, IsPersonnel, num2, ID_Swiper, ID_Play_Type, 0, 0, 0, empty);
                                                strCommandTwo = strCommandTwo + " -L2 -" + num8;
                                                clsCard.Card_CardProductTiming_SetChargePrice(str8, tuple.Item3, tuple.Item2 - num6_Price);
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
                                            int num8 = clsCard.Card_Play_Details_Insert(str8, 0, 0, main_ID_GameCenter, macAddress, num6_Price, 0, 0, IsPersonnel, num2, ID_Swiper, ID_Play_Type, 0, 0, 0, empty);
                                            strCommandTwo = strCommandTwo + " -L2 -" + num8;
                                            flag3 = true;
                                        }
                                    }
                                    if (!flag3)
                                    {
                                        int.Parse(byMacAddrress.Rows[0]["ID"].ToString());
                                        int num8 = int.Parse(byMacAddrress.Rows[0]["CashPrice"].ToString());
                                        int num9 = int.Parse(byMacAddrress.Rows[0]["BonusPrice"].ToString());
                                        DataTable dataTable = clsPattern.Gift_series_List_ActiveByCard_GUID(str8);
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
                                                if (num11 > 0 && ("," + dataTable.Rows[index]["FreeDailyGames"].ToString() + ",").Contains("," + num2 + ",") && clsCard.Card_Play_Details_Get_Today(str8, dataTable.Rows[index]["FreeDailyGames"].ToString()).Rows.Count < num11)
                                                {
                                                    empty = Guid.Parse(dataTable.Rows[index]["ID"].ToString());
                                                    flag5 = true;
                                                    break;
                                                }
                                                if (num10 > 0 && ("," + dataTable.Rows[index]["FreeGames"].ToString() + ",").Contains("," + num2 + ","))
                                                {
                                                    empty = Guid.Parse(dataTable.Rows[index]["ID"].ToString());
                                                    flag4 = true;
                                                    clsPattern.Gift_Pattern_series_List_Update(dataTable.Rows[index]["ID"].ToString(), num10 - 1);
                                                    break;
                                                }
                                            }
                                        }
                                        if (flag4)
                                        {
                                            ID_Play_Type = 2;
                                            num6_Price = 0;
                                        }
                                        if (flag5)
                                        {
                                            ID_Play_Type = 3;
                                            num6_Price = 0;
                                        }
                                        if (num5 > 0)
                                            ID_Play_Type = 7;
                                        if (flag5 | flag4)
                                        {
                                            int num10 = clsCard.Card_UpdatePriceAndBonus_PlayDetails2(str8, num8, num9, main_ID_GameCenter, macAddress, num6_Price, num8, num9, IsPersonnel, num2, ID_Swiper, ID_Play_Type, 0, 0, 0, empty);
                                            strCommandTwo = strCommandTwo + " -L2 -" + num10;
                                        }
                                        else
                                        {
                                            if (Pay_GiftPortion > 0)
                                                ID_Play_Type = 8;
                                            if (num8 + num9 + Pay_GiftPortion + num7 >= num6_Price)
                                            {
                                                int num10 = clsPattern.Gift_Pattern_Series_list_Calculate2(str8, num6_Price);
                                                if (num7 > 0)
                                                {
                                                    num10 -= num7;
                                                    clsCard.Card_CardProductTiming_SetChargePrice(str8, tuple.Item3, 0);
                                                }
                                                if (num8 >= num10)
                                                {
                                                    int num11 = clsCard.Card_UpdatePriceAndBonus_PlayDetails2(str8, num8 - num10, num9, main_ID_GameCenter, macAddress, num6_Price, num8 - num10, num9, IsPersonnel, num2, ID_Swiper, ID_Play_Type, num6_Price, 0, Pay_GiftPortion, empty);
                                                    strCommandTwo = strCommandTwo + " -L2 -" + num11;
                                                }
                                                else
                                                {
                                                    int Pay_BonusPortion = num10 - num8;
                                                    int num11 = num9 - Pay_BonusPortion;
                                                    int num12 = clsCard.Card_UpdatePriceAndBonus_PlayDetails2(str8, 0, num11, main_ID_GameCenter, macAddress, num6_Price, 0, num11, IsPersonnel, num2, ID_Swiper, ID_Play_Type, num6_Price - Pay_BonusPortion, Pay_BonusPortion, Pay_GiftPortion, empty);
                                                    strCommandTwo = strCommandTwo + " -L2 -" + num12;
                                                }
                                            }
                                            else
                                            {
                                                string str10 = macStamp;
                                                MainClass clsMain1 = clsMain;
                                                int num10 = num8 + num9 + num7;
                                                string str11 = num10.ToString();
                                                string str12 = clsMain1.Comma(str11);
                                                strCommandTwo = "[" + str10 + "]AT+NRC=" + str12;
                                                string str13 = macStamp;
                                                MainClass clsMain2 = clsMain;
                                                num10 = num8 + num9 + num7;
                                                string str14 = num10.ToString();
                                                string str15 = clsMain2.Comma(str14);
                                                strCommandOne = "[" + str13 + "]AT+NRC=" + str15;
                                            }
                                        }
                                    }
                                }
                                else
                                    clsMain.ErrorLogTemp($"Card not Find :{MacCode}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        clsMain.ErrorLog(ex);
                        clsMain.ErrorLogTemp($"Error Process Main +P :exp= {ex.Message},ReceiveText={receiveText}");
                        return "";
                    }
                }
                else if (receiveText.Contains("+T="))
                {
                    try
                    {
                        bool flag1 = false;
                        string str5 = receiveText.Substring(1, 12);
                        string macAddress = str5.Substring(0, 2) + str5.Substring(3, 2) + str5.Substring(6, 2) + str5.Substring(9, 2);
                        string str7 = receiveText.Split('=')[1].Replace("\r", "").Split(',')[0];
                        if (str7.ToLower() == "ffffffff")
                        {
                            DataSet macAddrressFfffff = clsCard.Card_GetByMacAddrressFFFFFF(macAddress);
                            DataTable table1 = macAddrressFfffff.Tables[0];
                            DataTable table2 = macAddrressFfffff.Tables[1];
                            if (table1.Rows.Count > 0 && table2.Rows.Count > 0)
                            {
                                macAddress = table1.Rows[0]["MacAddress2"].ToString().ToLower();
                                str7 = table2.Rows[0]["MacCode"].ToString();
                            }
                        }
                        string macStamp = MacAndTimeStamp_Create(macAddress);
                        Swipers_ChargeRate addressByChargeRate = SwiperClass.Swipers_ChargeRate.FirstOrDefault(mac => mac.MacAddress == macAddress.ToUpper());
                        int ID_Games = -1;
                        int ID_Swiper = -1;
                        string str9_Vrsn = "1";
                        if (addressByChargeRate != null)
                        {
                            try
                            {
                                str3_Title = addressByChargeRate.Title;
                                ID_Games = Convert.ToInt32(addressByChargeRate.ID_Games);
                                ID_Swiper = Convert.ToInt32(addressByChargeRate.ID);
                                str9_Vrsn = addressByChargeRate.Version;
                            }
                            catch { }
                        }
                        if (receiveText.Contains("SendOk"))
                        {
                            strCommandOne = "[" + macStamp + "]AT+TRC=0";
                            strCommandTwo = "[" + macStamp + "]AT+TRC=0";
                        }
                        else
                        {
                            DataTable dataTable = new DataTable();
                            DataTable byMacAddrress = clsCard.Card_GetByMacAddrress(str7);
                            int ID_Card_Play_Details = -1;
                            int IsPersonnel = 0;
                            string str10 = receiveText.Split('=')[1].ToString();
                            int Count = 0;
                            string str11 = "E";
                            try
                            {
                                Count = int.Parse(str10.Split(',')[1].ToString());
                                str11 = str10.Split(',')[2].ToString();
                            }
                            catch { }
                            if (byMacAddrress.Rows.Count > 0)
                            {
                                bool flag2 = bool.Parse(byMacAddrress.Rows[0]["IsNonTicket"].ToString());
                                bool.Parse(byMacAddrress.Rows[0]["AllowRegistration"].ToString());
                                if (flag2)
                                {
                                    strCommandOne = "[" + macStamp + "]AT+TRC=0";
                                    strCommandTwo = "[" + macStamp + "]AT+TRC=0";
                                }
                                else
                                {
                                    string Card_GUID = byMacAddrress.Rows[0]["Card_GUID"].ToString();
                                    int OldCount = int.Parse(byMacAddrress.Rows[0]["Etickets"].ToString());
                                    if (int.Parse(byMacAddrress.Rows[0]["ID_Card_Series"].ToString()) == 3)
                                        IsPersonnel = 1;
                                    DataTable byCardGuid = clsCard.Card_Play_Details_GetByCardGUID(macAddress, Card_GUID, main_ID_GameCenter);
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
                                                int ID_Games_Ticket = clsMain.Games_Ticket_Insert(main_ID_GameCenter, macAddress, str7, Count, Status, ID_Card_Play_Details, Card_GUID, IsPersonnel, ID_Games, ID_Swiper);
                                                if (str9_Vrsn == "3")
                                                {
                                                    if (IsPersonnel == 0)
                                                    {
                                                        if (clsCard.Card_Ticket_History_insert(main_ID_GameCenter, -1, Count, Card_GUID, OldCount, 6, ID_Games_Ticket) > 0)
                                                        {
                                                            if (clsCard.Card_SetEtickets(Card_GUID, Count + OldCount) > 0)
                                                            {
                                                                int num = Count + OldCount;
                                                                strCommandOne = "[" + macStamp + "]AT+TRC=" + num.ToString();
                                                                strCommandTwo = "[" + macStamp + "]AT+TRC=" + num.ToString();
                                                            }
                                                            else
                                                            {
                                                                strCommandOne = "[" + macStamp + "]AT+TRC=0";
                                                                strCommandTwo = "[" + macStamp + "]AT+TRC=0";
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        strCommandOne = "[" + macStamp + "]AT+TRC=0";
                                                        strCommandTwo = "[" + macStamp + "]AT+TRC=0";
                                                    }
                                                }
                                            }
                                            if (str11.ToUpper() == "O")
                                            {
                                                int Status = !flag3 ? 2 : 3;
                                                int ID_Games_Ticket = clsMain.Games_Ticket_Insert(main_ID_GameCenter, macAddress, str7, Count, Status, ID_Card_Play_Details, Card_GUID, IsPersonnel, ID_Games, ID_Swiper);
                                                if (IsPersonnel == 0)
                                                {
                                                    if (str9_Vrsn == "2")
                                                    {
                                                        strCommandOne = "[" + macStamp + "]AT+ok";
                                                        strCommandTwo = "[" + macStamp + "]AT+ok";
                                                    }
                                                    else if (str9_Vrsn == "3" && clsCard.Card_Ticket_History_insert(main_ID_GameCenter, -1, Count, Card_GUID, OldCount, 6, ID_Games_Ticket) > 0)
                                                    {
                                                        if (clsCard.Card_SetEtickets(Card_GUID, Count + OldCount) > 0)
                                                        {
                                                            int num = Count + OldCount;
                                                            strCommandOne = "[" + macStamp + "]AT+TRC=" + num.ToString();
                                                            strCommandTwo = "[" + macStamp + "]AT+TRC=" + num.ToString();
                                                        }
                                                        else
                                                        {
                                                            strCommandOne = "[" + macStamp + "]AT+TRC=0";
                                                            strCommandTwo = "[" + macStamp + "]AT+TRC=0";
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    strCommandOne = "[" + macStamp + "]AT+TRC=0";
                                                    strCommandTwo = "[" + macStamp + "]AT+TRC=0";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            clsMain.Games_Ticket_Insert(main_ID_GameCenter, macAddress, str7, Count, 3, ID_Card_Play_Details, Card_GUID, IsPersonnel, ID_Games, ID_Swiper);
                                            strCommandOne = "[" + macStamp + "]AT+TRC=0";
                                            strCommandTwo = "[" + macStamp + "]AT+TRC=0";
                                        }
                                    }
                                    else
                                    {
                                        strCommandOne = "[" + macStamp + "]AT+TRC=" + OldCount.ToString();
                                        strCommandTwo = "[" + macStamp + "]AT+TRC=" + OldCount.ToString();
                                    }
                                }
                            }
                            else
                            {
                                strCommandOne = "[" + macStamp + "]AT+TRC=0";
                                strCommandTwo = "[" + macStamp + "]AT+TRC=0";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        clsMain.ErrorLog(ex);
                        clsMain.ErrorLogTemp($"Error Process Main +T :exp= {ex.Message},ReceiveText={receiveText}");
                        return "";
                    }
                }
                else if (receiveText.Contains("Error_conf"))
                {
                    try
                    {
                        string str5 = receiveText.Substring(1, 12);
                        string str6 = str5.Substring(0, 2) + str5.Substring(3, 2) + str5.Substring(6, 2) + str5.Substring(9, 2);
                        string macAddress = str6;
                        string macStamp = MacAndTimeStamp_Create(macAddress);
                        Swipers_ChargeRate addressByChargeRate = SwiperClass.Swipers_ChargeRate.FirstOrDefault(mac => mac.MacAddress == macAddress.ToUpper());
                        if (addressByChargeRate != null)
                        {
                            str3_Title = addressByChargeRate.Title;
                            strCommandOne = strCommandTwo = "[" + macStamp + "]AT+CFG1=";
                            clsSwiper.Swiper_Update_Config_State(0, macAddress);
                        }
                    }
                    catch (Exception ex)
                    {
                        clsMain.ErrorLog(ex);
                        clsMain.ErrorLogTemp($"Error Process Main Error_config :exp= {ex.Message},ReceiveText={receiveText}");
                        return "";
                    }
                }
                else if (receiveText.Contains("HID"))
                {
                    try
                    {
                        string str5 = receiveText.Substring(1, 12);
                        string macAddress = str5.Substring(0, 2) + str5.Substring(3, 2) + str5.Substring(6, 2) + str5.Substring(9, 2); ;
                        string macStamp = MacAndTimeStamp_Create(macAddress);
                        Swipers_ChargeRate addressByChargeRate = SwiperClass.Swipers_ChargeRate.FirstOrDefault(mac => mac.MacAddress == macAddress.ToUpper());
                        if (addressByChargeRate != null)
                        {
                            str3_Title = addressByChargeRate.Title;
                            str4_Mac = receiveText.Split('=')[1].Replace("\r", "").Split(',')[0].ToUpper();
                            DataTable byMacAddrress = clsCard.Card_GetByMacAddrress(str4_Mac);
                            if (byMacAddrress.Rows.Count > 0)
                            {
                                bool boolean = Convert.ToBoolean(byMacAddrress.Rows[0]["IsNonPlayGames"].ToString().ToLower() == "" ? "false" : byMacAddrress.Rows[0]["IsNonPlayGames"].ToString());
                                if (Convert.ToBoolean(byMacAddrress.Rows[0]["IsActive"].ToString()))
                                {
                                    if (!boolean)
                                    {
                                        int num1 = int.Parse(byMacAddrress.Rows[0]["ID_Card_Series"].ToString());
                                        string Card_GUID = byMacAddrress.Rows[0]["Card_GUID"].ToString();

                                        if (clsCard.Card_CardProductTiming_Get(Card_GUID).Rows.Count <= 0)
                                        {
                                            int num3 = int.Parse(byMacAddrress.Rows[0]["CashPrice"].ToString());
                                            int num4 = int.Parse(byMacAddrress.Rows[0]["BonusPrice"].ToString());
                                            string str8 = "0";
                                            DataTable dataTable = new DataTable();
                                            if (byMacAddrress.Rows.Count > 0)
                                            {
                                                str8 = byMacAddrress.Rows[0]["Etickets"].ToString();
                                                dataTable = clsCard.Card_Play_Details_GetByCardGUID(byMacAddrress.Rows[0]["Card_GUID"].ToString());
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
                                                    str12 = clsMain.Comma(dataTable.Rows[0]["Price"].ToString());
                                                    str13 = clsMain.Comma(dataTable.Rows[0]["PriceKol"].ToString());
                                                }
                                                catch { }
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
                                                    str17 = clsMain.Comma(dataTable.Rows[1]["Price"].ToString());
                                                    str18 = clsMain.Comma(dataTable.Rows[1]["PriceKol"].ToString());
                                                }
                                                catch { }
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
                                                    str22 = clsMain.Comma(dataTable.Rows[2]["Price"].ToString());
                                                    str23 = clsMain.Comma(dataTable.Rows[2]["PriceKol"].ToString());
                                                }
                                                catch { }
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
                                                    str28 = clsMain.Comma(dataTable.Rows[3]["Price"].ToString());
                                                    str29 = clsMain.Comma(dataTable.Rows[3]["PriceKol"].ToString());
                                                }
                                                catch { }
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
                                                    str33 = clsMain.Comma(dataTable.Rows[4]["Price"].ToString());
                                                    str34 = clsMain.Comma(dataTable.Rows[4]["PriceKol"].ToString());
                                                }
                                                catch { }
                                            }
                                            string str36 = "HIDShow$" + macStamp + "$" + (num3 + num4).ToString() + "$" + str8 + "$" + str9 + "$" + str10 + "$" + str11 + "$" + str12 + "$" + str13 + "$" + str14 + "$" + str15 + "$" + str16 + "$" + str17 + "$" + str18 + "$" + str19 + "$" + str20 + "$" + str21 + "$" + str22 + "$" + str23 + "$" + str24 + "$" + str25 + "$" + str26 + "$" + str28 + "$" + str29 + "$" + str30 + "$" + str31 + "$" + str32 + "$" + str33 + "$" + str34;
                                            strCommandOne = str36;
                                            strCommandTwo = str36;
                                        }
                                    }
                                    else
                                    {
                                        strCommandOne = "[" + macStamp + "]AT+DISABLE";
                                        strCommandTwo = "[" + macStamp + "]AT+DISABLE";
                                    }
                                }
                                else
                                {
                                    strCommandOne = "[" + macStamp + "]AT+DISABLE";
                                    strCommandTwo = "[" + macStamp + "]AT+DISABLE";
                                }
                            }
                            else
                            {
                                strCommandOne = "[" + macStamp + "]AT+INVALID";
                                strCommandTwo = "[" + macStamp + "]AT+INVALID";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        clsMain.ErrorLog(ex);
                        clsMain.ErrorLogTemp($"Error Process Main HID :exp= {ex.Message},ReceiveText={receiveText}");
                        return "";
                    }
                }
                else if (receiveText.Contains("OK_"))
                {
                    try
                    {
                        string str5 = receiveText.Substring(1, 12);
                        string macAddress = str5.Substring(0, 2) + str5.Substring(3, 2) + str5.Substring(6, 2) + str5.Substring(9, 2);
                        string macStamp = MacAndTimeStamp_Create(macAddress);

                        Swipers_ChargeRate addressByChargeRate = SwiperClass.Swipers_ChargeRate.FirstOrDefault(mac => mac.MacAddress == macAddress.ToUpper());
                        if (addressByChargeRate != null)
                        {
                            str4_Mac = receiveText.Split('=')[1].Replace("\r", "").Split(',')[0].ToUpper();
                            string str7 = "0";
                            DataTable byMacAddrress = clsCard.Card_GetByMacAddrress(str4_Mac);
                            DataTable dataTable = new DataTable();
                            if (byMacAddrress.Rows.Count > 0)
                            {
                                str7 = byMacAddrress.Rows[0]["Etickets"].ToString();
                                dataTable = clsCard.Card_Play_Details_GetByCardGUID(byMacAddrress.Rows[0]["Card_GUID"].ToString());
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
                            if (receiveText.Contains("OK_TP"))
                            {
                                strCommandOne = "[" + macStamp + "]AT+TTIK=" + str7;
                                strCommandTwo = "[" + macStamp + "]AT+TTIK=" + str7;
                            }
                            if (receiveText.Contains("OK_TT") && str8.Length > 0)
                            {
                                strCommandOne = "[" + macStamp + "]A1=" + str8;
                                strCommandTwo = "[" + macStamp + "]A1=" + str8;
                            }
                            if (receiveText.Contains("OK_A1"))
                            {
                                strCommandOne = "[" + macStamp + "]A2=" + str9;
                                strCommandTwo = "[" + macStamp + "]A2=" + str9;
                            }
                            if (receiveText.Contains("OK_A2"))
                            {
                                strCommandOne = "[" + macStamp + "]A3=" + str10;
                                strCommandTwo = "[" + macStamp + "]A3=" + str10;
                            }
                            if (receiveText.Contains("OK_A3"))
                            {
                                strCommandOne = "[" + macStamp + "]A4=" + str11;
                                strCommandTwo = "[" + macStamp + "]A4=" + str11;
                            }
                            if (receiveText.Contains("OK_A4") && str12.Length > 0)
                            {
                                strCommandOne = "[" + macStamp + "]B1=" + str12;
                                strCommandTwo = "[" + macStamp + "]B1=" + str12;
                            }
                            if (receiveText.Contains("OK_B1"))
                            {
                                strCommandOne = "[" + macStamp + "]B2=" + str13;
                                strCommandTwo = "[" + macStamp + "]B2=" + str13;
                            }
                            if (receiveText.Contains("OK_B2"))
                            {
                                strCommandOne = "[" + macStamp + "]B3=" + str14;
                                strCommandTwo = "[" + macStamp + "]B3=" + str14;
                            }
                            if (receiveText.Contains("OK_B3"))
                            {
                                strCommandOne = "[" + macStamp + "]B4=" + str15;
                                strCommandTwo = "[" + macStamp + "]B4=" + str15;
                            }
                            if (receiveText.Contains("OK_B4") && str16.Length > 0)
                            {
                                strCommandOne = "[" + macStamp + "]C1=" + str16;
                                strCommandTwo = "[" + macStamp + "]C1=" + str16;
                            }
                            if (receiveText.Contains("OK_C1"))
                            {
                                strCommandOne = "[" + macStamp + "]C2=" + str17;
                                strCommandTwo = "[" + macStamp + "]C2=" + str17;
                            }
                            if (receiveText.Contains("OK_C2"))
                            {
                                strCommandOne = "[" + macStamp + "]C3=" + str18;
                                strCommandTwo = "[" + macStamp + "]C3=" + str18;
                            }
                            if (receiveText.Contains("OK_C3"))
                            {
                                strCommandOne = "[" + macStamp + "]C4=" + str19;
                                strCommandTwo = "[" + macStamp + "]C4=" + str19;
                            }
                            if (receiveText.Contains("OK_C4") && str20.Length > 0)
                            {
                                strCommandOne = "[" + macStamp + "]D1=" + str20;
                                strCommandTwo = "[" + macStamp + "]D1=" + str20;
                            }
                            if (receiveText.Contains("OK_D1"))
                            {
                                strCommandOne = "[" + macStamp + "]D2=" + str21;
                                strCommandTwo = "[" + macStamp + "]D2=" + str21;
                            }
                            if (receiveText.Contains("OK_D2"))
                            {
                                strCommandOne = "[" + macStamp + "]D3=" + str22;
                                strCommandTwo = "[" + macStamp + "]D3=" + str22;
                            }
                            if (receiveText.Contains("OK_D3"))
                            {
                                strCommandOne = "[" + macStamp + "]D4=" + str23;
                                strCommandTwo = "[" + macStamp + "]D4=" + str23;
                            }
                            if (receiveText.Contains("OK_D4") && str24.Length > 0)
                            {
                                strCommandOne = "[" + macStamp + "]E1=" + str24;
                                strCommandTwo = "[" + macStamp + "]E1=" + str24;
                            }
                            if (receiveText.Contains("OK_E1"))
                            {
                                strCommandOne = "[" + macStamp + "]E2=" + str25;
                                strCommandTwo = "[" + macStamp + "]E2=" + str25;
                            }
                            if (receiveText.Contains("OK_E2"))
                            {
                                strCommandOne = "[" + macStamp + "]E3=" + str26;
                                strCommandTwo = "[" + macStamp + "]E3=" + str26;
                            }
                            if (receiveText.Contains("OK_E3"))
                            {
                                strCommandOne = "[" + macStamp + "]E4=" + str28;
                                strCommandTwo = "[" + macStamp + "]E4=" + str28;
                            }
                            Thread.Sleep(1);
                        }
                    }
                    catch (Exception ex)
                    {
                        clsMain.ErrorLog(ex);
                        clsMain.ErrorLogTemp($"Error Process Main OKCFG_HID :exp= {ex.Message},ReceiveText={receiveText}");
                        return "";
                    }
                }

                return strCommandOne + "!" + strCommandTwo + "!" + str3_Title + "!" + str4_Mac.ToUpper();
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
                clsMain.ErrorLogTemp($"Error Process Main :{ex.Message}");
                return "";
            }
        }

        public void ManualChargeRate()
        {
            try
            {
                List<Swiper> stateForChangePrice = clsSwiper.Swiper_GetByState_ForChangePrice();
                if (stateForChangePrice != null)
                    for (int index2 = 0; index2 < stateForChangePrice.Count(); ++index2)
                    {
                        string macAddress = stateForChangePrice[index2].MacAddress;
                        string macStamp = MacAndTimeStamp_Create(macAddress);

                        Swipers_ChargeRate addressByChargeRate = SwiperClass.Swipers_ChargeRate.FirstOrDefault(mac => mac.MacAddress == macAddress.ToUpper());
                        if (addressByChargeRate != null)
                        {
                            clsSwiper.Swiper_UpdateStateByMacAddress(macAddress.ToUpper(), -3);
                            string str4 = "[" + macStamp + "]AT+CFG3=" + clsMain.Comma(addressByChargeRate.PriceAdi);
                            string str5 = "[" + macStamp + "]AT+CFG4=" + clsMain.Comma(addressByChargeRate.PriceVije);
                            for (int index3 = 0; index3 < tcp_RepeatCount + 5; ++index3)
                            {
                                foreach (var item in serverConfigView)
                                {
                                    Send_DisplayText(str4, $"P{item.AP_ID}", "", "");
                                    Send_Main(item.AP_IP, str4, ClsStarter.tCPClientList.FirstOrDefault(i => i.AP_ID == item.AP_ID).TCPClient);
                                }
                                Thread.Sleep(70);
                            }
                            Thread.Sleep(100);
                            for (int index3 = 0; index3 < tcp_RepeatCount + 5; ++index3)
                            {
                                foreach (var item in serverConfigView)
                                {
                                    Send_DisplayText(str5, $"P{item.AP_ID}", "", "");
                                    Send_Main(item.AP_IP, str5, ClsStarter.tCPClientList.FirstOrDefault(i => i.AP_ID == item.AP_ID).TCPClient);
                                }
                                Thread.Sleep(70);
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
            }
        }

        /// <summary>
        /// Insert in Database if Not Write in File
        /// </summary>
        public void WriteToFile_SendLog(string message, DateTime dtSR)
        {
            try
            {
                int retValue = clsMain.Server_SendMessage_Insert(message, dtSR);
                if (retValue != 1)
                {
                    string pathFileSendReceive = AppDomain.CurrentDomain.BaseDirectory + "\\ServiceLogs";
                    if (!Directory.Exists(pathFileSendReceive))
                        Directory.CreateDirectory(pathFileSendReceive);

                    pathFileSendReceive += $"\\ServiceLog_Send{DateTime.Now:yyyy-MM-dd}.txt";

                    File.AppendAllText(pathFileSendReceive, message + "\n", Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
            }
        }
    }
}