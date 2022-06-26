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
using System.Net.Sockets;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;


namespace ClickServerService
{
    internal static class Program
    {
        public static List<MyTCPClient> tCPClientList = new List<MyTCPClient>();

        private static void Main()
        {
            Console.WriteLine("q");
            MainClass mainClass = new MainClass();
            List<Access_Point> accessPoints = mainClass.GetAccessPoints();
            //accessPoints.Reverse();
            Thread.Sleep(0);
            if (accessPoints.Any())
            {
                foreach (var item in accessPoints)
                {
                    //Task.Run(() => new ClsClickService(item.AP_ID));
                    // Thread.Sleep(1);
                    TcpClient tcp = new TcpClient();
                    tCPClientList.Add(new MyTCPClient(item.AP_ID, tcp));
                    tCPClientList.SingleOrDefault(i => i.AP_ID == item.AP_ID).TCPClient.Connect(item.AP_IP, 1000);
                    Thread.Sleep(1);

                    // Thread.Sleep(1);
                    Task.Run(() => new ClsSender(item.AP_ID).Start(ref tcp));
                    Thread.Sleep(1);
                    // Thread.Sleep(1);
                    Task.Run(() => new ClsReceiver(item.AP_ID).Start(ref tcp));
                    //  Thread.Sleep(1);
                    Thread.Sleep(1);

                    Console.WriteLine("+accessPoints : " + item.AP_ID.ToString());
                    //Thread.Sleep(10);
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
