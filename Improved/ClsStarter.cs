using ClickServerService.ClassCode;
using System.Collections.Generic;
using ClickServerService.Models;
using System.Threading.Tasks;
using System.ServiceProcess;
using System.ComponentModel;
using System.Net.Sockets;
using System.Threading;
using System.Timers;
using System.Linq;
using System;

namespace ClickServerService.Improved
{
    public class ClsStarter : ServiceBase
    {
        #region ' Variables '

        public static List<Access_Point> accessPoints = null;
        public static List<string> debugDataList = new List<string>();
        public static List<MyTCPClient> tCPClientList = new List<MyTCPClient>();

        public static int ServerBufferLength = 1000;

        readonly MainClass clsMain = new MainClass();
        readonly ClsSender clsSender = new ClsSender();
        readonly GamesClass clsGames = new GamesClass();
        readonly SwiperClass clsSwiper = new SwiperClass();

        readonly System.Timers.Timer TimerChargeRate = new System.Timers.Timer();
        readonly System.Timers.Timer Timer_Create_Repair_CheckList = new System.Timers.Timer();
        readonly System.Timers.Timer TimerChargeRate_SetNonReceive = new System.Timers.Timer();

        #endregion

        #region ' Service '

        private IContainer components = null;

        public ClsStarter()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            ServiceName = "ClickServerService";
        }

        #endregion

        protected override void OnStart(string[] args)
        {
            try
            {
                if (!clsMain.Licence_Check())
                {
                    clsMain.MyPrint(" :1:Licence ERROR ", ConsoleColor.Red);
                    Dispose();
                }
                else
                    AppLoadMain();
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
            }
        }

        public void AppLoadMain()
        {
            try
            {
                clsMain.MyPrint("Starter - AppLoadMain", ConsoleColor.Blue);
                accessPoints = clsMain.GetAccessPoints();
                SwiperClass.Swipers = clsSwiper.GetAllSwiper();
                SwiperClass.Swipers_ChargeRate = clsSwiper.Swipers_GetByMacAddressByChargeRate();
                Thread.Sleep(0);
                if (accessPoints.Any())
                {
                    MainClass.key_Value_List = clsMain.Key_Value_Get();

                    foreach (var item in accessPoints)
                    {
                        try
                        {
                            TcpClient tcp = new TcpClient();
                            tCPClientList.Add(new MyTCPClient(item.AP_ID, tcp));
                            try
                            {
                                tCPClientList.FirstOrDefault(i => i.AP_ID == item.AP_ID).TCPClient.Connect(item.AP_IP, item.AP_Port);
                            }
                            catch
                            {
                                clsMain.MyPrint("Not Connect " + item.AP_IP, ConsoleColor.Red);
                            }
                            Thread.Sleep(0);
                            Task.Run(() => new ClsReceiver(item.AP_ID).Start());
                            Thread.Sleep(0);
                            clsMain.MyPrint("*AccessPoints : " + item.AP_ID.ToString(), ConsoleColor.White);
                        }
                        catch { }
                    }
                    Task.Run(() => new ClsSender().Start());
                    //
                    Task.Run(() => new ClsSocketServer().Start());
                }

                #region ' Initialization Timers '

                TimerChargeRate.Elapsed += new ElapsedEventHandler(TimerChargeRate_Tick);
                TimerChargeRate.Interval = 10000.0;
                TimerChargeRate.Enabled = false;

                TimerChargeRate_SetNonReceive.Elapsed += new ElapsedEventHandler(TimerChargeRate_SetNonReceive_Tick);
                TimerChargeRate_SetNonReceive.Interval = 300000.0;// 5 Min
                TimerChargeRate_SetNonReceive.Enabled = true;

                Timer_Create_Repair_CheckList.Elapsed += new ElapsedEventHandler(Timer_Create_Repair_CheckList_Tick);
                Timer_Create_Repair_CheckList.Interval = 1800000.0;// 30 Min
                Timer_Create_Repair_CheckList.Enabled = true;

                #endregion

                TimerChargeRate.Enabled = MainClass.key_Value_List.Select("KeyName ='Enable_Charge_Rate'")[0]["Value"].ToString().ToLower() == "true";

            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
            }
        }

        #region ' Timers Tick '

        private void TimerChargeRate_Tick(object sender, EventArgs e)
        {
            try
            {
                if (DateTime.Now.Minute != 0)
                    return;
                TimerChargeRate.Interval = 60000.0;// 1 Min
                clsGames.Charge_Rate_GetAll(clsMain.ID_GameCenter_Local_Get());
                clsSender.ManualChargeRate();
                SwiperClass.Swipers = clsSwiper.GetAllSwiper();
                SwiperClass.Swipers_ChargeRate = clsSwiper.Swipers_GetByMacAddressByChargeRate();
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
            }
        }

        private void Timer_Create_Repair_CheckList_Tick(object sender, EventArgs e)
        {
            try
            {
                new Thread(new ThreadStart(new RepairClass().Create_Repair_Today_CheckList)).Start();
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
            }
        }

        private void TimerChargeRate_SetNonReceive_Tick(object sender, EventArgs e)
        {
            try
            {
                clsSwiper.Swiper_UpdateStateToNotReceiveForChargeRate();
            }
            catch (Exception ex)
            {
                clsMain.ErrorLog(ex);
            }
        }

        #endregion
    }
}
