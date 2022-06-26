using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
