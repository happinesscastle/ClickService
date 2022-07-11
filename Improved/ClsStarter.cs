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
        public static List<Access_Point> accessPoints = null;
        public static List<string> debugDataList = new List<string>();
        public static List<MyTCPClient> tCPClientList = new List<MyTCPClient>();

        public static int ServerBufferLength = 1000;

        readonly MainClass objMain = new MainClass();
        readonly ClsSender clsSender = new ClsSender();
        readonly GamesClass objGames = new GamesClass();
        readonly SwiperClass objSwiper = new SwiperClass();

        readonly System.Timers.Timer TimerChargeRate = new System.Timers.Timer();
        readonly System.Timers.Timer Timer_Create_Repair_CheckList = new System.Timers.Timer();
        readonly System.Timers.Timer TimerChargeRate_SetNonRecive = new System.Timers.Timer();

        private IContainer components = null;

        public ClsStarter()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                if (!objMain.Licence_Check())
                {
                    objMain.MyPrint(" :1:Licence ERROR ", ConsoleColor.Red);
                    Dispose();
                }
                else
                    AppLoadMain();
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
                objMain.MyPrint("Starter - AppLoadMain", ConsoleColor.Blue);

                accessPoints = objMain.GetAccessPoints();
                Thread.Sleep(0);
                if (accessPoints.Any())
                {
                    foreach (var item in accessPoints)
                    {
                        TcpClient tcp = new TcpClient();
                        tCPClientList.Add(new MyTCPClient(item.AP_ID, tcp));
                        try
                        {
                            tCPClientList.SingleOrDefault(i => i.AP_ID == item.AP_ID).TCPClient.Connect(item.AP_IP, item.AP_Port);
                        }
                        catch
                        {
                            objMain.MyPrint("Not Connect " + item.AP_IP, ConsoleColor.Red);
                        }
                        Thread.Sleep(0);
                        Task.Run(() => new ClsReceiver(item.AP_ID).Start());
                        Thread.Sleep(0);
                        objMain.MyPrint("*AccessPoints : " + item.AP_ID.ToString(), ConsoleColor.White);
                    }
                    Task.Run(() => new ClsSender().Start());
                    //
                    Task.Run(() => new ClsSocketServer().Start());
                }

                #region ' Timers '

                TimerChargeRate.Elapsed += new ElapsedEventHandler(TimerChargeRate_Tick);
                TimerChargeRate.Interval = 10000.0;
                TimerChargeRate.Enabled = false;

                TimerChargeRate_SetNonRecive.Elapsed += new ElapsedEventHandler(TimerChargeRate_SetNonRecive_Tick);
                TimerChargeRate_SetNonRecive.Interval = 300000.0;// 5 Min
                TimerChargeRate_SetNonRecive.Enabled = true;

                Timer_Create_Repair_CheckList.Elapsed += new ElapsedEventHandler(Timer_Create_Repair_CheckList_Tick);
                Timer_Create_Repair_CheckList.Interval = 1800000.0;// 30 Min
                Timer_Create_Repair_CheckList.Enabled = true;

                #endregion

                MainClass.key_Value_List = objMain.Key_Value_Get();
                TimerChargeRate.Enabled = MainClass.key_Value_List.Select("KeyName ='Enable_Charge_Rate'")[0]["Value"].ToString().ToLower() == "true";

            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
            }
        }

        private void TimerChargeRate_Tick(object sender, EventArgs e)
        {
            try
            {
                if (DateTime.Now.Minute != 0)
                    return;
                objGames.Charge_Rate_GetAll(objMain.ID_GameCenter_Local_Get());
                clsSender.ManualChargeRate();
                TimerChargeRate.Interval = 60000.0;// 1 Min
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
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
                objMain.ErrorLog(ex);
            }
        }

        private void TimerChargeRate_SetNonRecive_Tick(object sender, EventArgs e)
        {
            try
            {
                objSwiper.Swiper_StateUpdateToNotReciveForChargeRate();
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
            }
        }

        //

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
    }
}
