using ClickServerService.Improved;
using System.Collections.Generic;
using ClickServerService.Models;
using System.Threading.Tasks;
using System.ServiceProcess;
using System.Net.Sockets;
using System.Threading;
using System.Data;
using System.Linq;
using System;

namespace ClickServerService
{
    internal static class Program
    {
        public static List<MyTCPClient> tCPClientList = new List<MyTCPClient>();
        public static List<Access_Point> accessPoints = null;

        private static void Main()
        {
            MainClass objMain = new MainClass();
            try
            {
                Console.Title = "Click Server Service";
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
                        objMain.MyPrint("+accessPoints : " + item.AP_ID.ToString(), ConsoleColor.White);
                    }
                    Task.Run(() => new ClsSender().Start());
                    Task.Run(() => new ClsStarter());
                }
                Task a = Task.Run(() => ForBeConteneud());
                a.Wait();
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
            }
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
                if (r.Key == ConsoleKey.C)
                    Console.Clear();
            }
        }
    }
}
