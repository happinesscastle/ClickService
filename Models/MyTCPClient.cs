using System.Net.Sockets;

namespace ClickServerService.Models
{
    public class MyTCPClient
    {
        public MyTCPClient() { }

        public MyTCPClient(int aP_ID, TcpClient tCPClient)
        {
            AP_ID = aP_ID;
            TCPClient = tCPClient;
        }

        public int AP_ID { get; set; }
        public TcpClient TCPClient { get; set; }
    }
}
