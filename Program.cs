using ClickServerService.Improved;
using System.Threading.Tasks;
using System.ServiceProcess;
using System;

namespace ClickServerService
{
    internal static class Program
    {

        private static void Main()
        {
            try
            {
                ServiceBase.Run(new ServiceBase[1] { new ClsStarter() });

                //    Console.Title = "Click Server Service";
                //    Task.Run(() => new ClsStarter());
                //    Task fbc = Task.Run(() => ForBeConteneud());
                //    fbc.Wait();
            }
            catch
            { }
        }

        //static void ForBeConteneud()
        //{
        //    while (true)
        //    {
        //        Console.ForegroundColor = ConsoleColor.Cyan;
        //        Console.Write(" : ");
        //        Console.ForegroundColor = ConsoleColor.White;
        //        var r = Console.ReadKey();
        //        Console.ForegroundColor = ConsoleColor.Cyan;
        //        Console.WriteLine(" -_-> " + r.Key);
        //        Console.ForegroundColor = ConsoleColor.White;
        //        if (r.Key == ConsoleKey.C)
        //            Console.Clear();
        //    }
        //}
    }
}
