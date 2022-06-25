// Decompiled with JetBrains decompiler
// Type: ClickServerService.Program
// Assembly: ClickServerService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6BDFD2F8-7BA8-4B8A-8EC1-401DFA893333
// Assembly location: C:\Users\Win10\Desktop\ClickServerService.exe

using ClickServerService.Improved;
using ClickServerService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;


namespace ClickServerService
{
    internal static class Program
    {
        private static void Main()
        {
            MainClass mainClass = new MainClass();
            List<Access_Point> accessPoints = mainClass.GetAccessPoints();
            if (accessPoints.Any())
            {
                foreach (var item in accessPoints.Where(q => q.AP_ID == 3))
                {
                    //Thread.Sleep(10);
                    //Task.Run(() => new ClsReceiver(item.AP_ID).Start());
                    Task.Run(() => new ClsClickService(item.AP_ID));

                    //Thread.Sleep(10);
                    //ClsSender cls = new ClsSender(item.AP_ID);
                    
                    //cls.StartTimer();

                     //new ClsReceiver(item.AP_ID).Start();
                    Console.WriteLine("+accessPoints : " + item.AP_ID.ToString());
                    Thread.Sleep(10);

                }


                //for (int i = 0; i < accessPoints.Count(); i++)
                //{

                //    Thread.Sleep(10);
                //    //Task.Run(() => new Improved.ClsReceiver(Convert.ToInt32(accessPoints.Rows[i]["AP_ID"].ToString())));

                //   // new Improved.ClsReceiver(Convert.ToInt32(accessPoints.Rows[i]["AP_ID"].ToString()));

                //    Task.Run(() => new ClsClickService(Convert.ToInt32(accessPoints.Rows[i]["AP_ID"].ToString())));
                //    Thread.Sleep(10);
                //    Console.WriteLine("+accessPoints : " + accessPoints.Rows[i]["AP_ID"].ToString());
                //    Thread.Sleep(10);
                //}
            }
            var a = Task.Run(() => ForBeConteneud());
            a.Wait();

            // cls.Start();
            //ServiceBase.Run(new ServiceBase[1] { (ServiceBase)new ClickService() });
        }

        static void ForBeConteneud()
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
    }
}
