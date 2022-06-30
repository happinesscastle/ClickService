using ClickServerService.Improved;
using System.Threading.Tasks;
using System;

namespace ClickServerService
{
    internal static class Program
    {

        private static void Main()
        {
            MainClass objMain = new MainClass();
            try
            {
                Console.Title = "Click Server Service";
                
                Task.Run(() => new ClsStarter());

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
