using ClickServerService.ClassCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ClickServerService.Improved
{
    public class ClsStarter
    {
        readonly MainClass objMain = new MainClass();
        readonly GamesClass objGames = new GamesClass();
        readonly SwiperClass objSwiper = new SwiperClass();

        readonly System.Timers.Timer TimerChargeRate = new System.Timers.Timer();
        readonly System.Timers.Timer Timer_Create_Repair_CheckList = new System.Timers.Timer();
        readonly System.Timers.Timer TimerChargeRate_SetNonRecive = new System.Timers.Timer();

        public ClsStarter()
        {
            AppLoadMain();
        }

        public void AppLoadMain()
        {
            try
            {
                objMain.MyPrint("Starter - AppLoadMain", ConsoleColor.Blue);

                #region ' Timers '

                TimerChargeRate.Elapsed += new ElapsedEventHandler(TimerChargeRate_Tick);
                TimerChargeRate.Interval = 1000.0;
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
    }
}
