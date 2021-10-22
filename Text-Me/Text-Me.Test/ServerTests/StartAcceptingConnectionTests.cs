using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Text_Me.Service;
using System.Net;
using System.Threading;
using System.Net.Sockets;

namespace Text_Me.Test.ServerTests
{
    public class StartAcceptingConnectionTests
    {
        [Fact]
        public void ServerShouldAcceptConnection()
        {
            ManualResetEvent receiveDone = new ManualResetEvent(false);

            TcpClient tcpClient = new TcpClient();

            Server server = new Server(IPAddress.Loopback.ToString());

            server.OnConnection += ResultFunc;
            ConnectionResult connectionResult = ConnectionResult.UNKNOWN;
            void ResultFunc(ConnectionResult result)
            {
                connectionResult = result;
                receiveDone.Set();
            }

            server.StartAcceptingConnection();
            tcpClient.Connect(IPAddress.Loopback.ToString(), 3838);

            receiveDone.WaitOne();
            //tcpClient.Close();

            Assert.True(connectionResult == ConnectionResult.SUCCESS);
        }
    }
}
