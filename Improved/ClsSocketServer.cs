using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System;

namespace ClickServerService.Improved
{
    public class ClsSocketServer
    {
        #region ' Variables '

        readonly MainClass objMain = new MainClass();

        private readonly int PortNumber = 2930;

        #endregion

        public ClsSocketServer()
        {
            try
            {
                PortNumber = Convert.ToInt32(objMain.GetData_SocketInterfaceConfig("SocketPort"));
            }
            catch (Exception ex)
            {
                objMain.ErrorLog(ex);
            }
        }

        public void Start()
        {
            try
            {
                Socket mainSocket;
                while (true)
                {
                    IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, PortNumber);
                    mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    try
                    {
                        mainSocket.Bind(iPEndPoint);
                        break;
                    }
                    catch (Exception ex)
                    {
                        objMain.MyPrint(ex.Message, ConsoleColor.DarkRed);
                        objMain.ErrorLog(ex);
                    }
                }
                mainSocket.Listen(5);
                var task = Task.Run(() => ClientReceive(mainSocket));
                task.Wait();
            }
            catch (Exception ex)
            {
                objMain.MyPrint(ex.Message, ConsoleColor.Red);
            }
        }

        void ClientReceive(Socket mainSocket)
        {
            while (true)
            {
                try
                {
                    Socket subSocket = mainSocket.Accept();
                    byte[] buffer = new byte[9999999];
                    int receiveCount = subSocket.Receive(buffer);
                    string receiveText = Encoding.ASCII.GetString(buffer, 0, receiveCount);

                    if (receiveText.StartsWith("#NeedForInformation?"))
                    {
                        List<string> tempDebugDataList = new List<string>();
                        tempDebugDataList.AddRange(ClsStarter.debugDataList);
                        ClsStarter.debugDataList.Clear();
                        Task.Run(() => SendMessage(subSocket, tempDebugDataList));
                    }
                }
                catch (Exception ex)
                {
                    objMain.MyPrint(ex.Message, ConsoleColor.Red);
                }
            }
        }

        void SendMessage(Socket socket, List<string> sendMessages)
        {
            try
            {
                string sendDataText = "";
                foreach (var item in sendMessages)
                {
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(item))
                            sendDataText += $"{item}~";
                    }
                    catch { }
                }
                sendDataText += "#EndData?";
                socket.Send(Encoding.ASCII.GetBytes(sendDataText));
            }
            catch (Exception ex)
            {
                objMain.MyPrint(ex.Message, ConsoleColor.Red);
            }
            finally
            {
                try
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                catch { }
            }
        }
    }
}
