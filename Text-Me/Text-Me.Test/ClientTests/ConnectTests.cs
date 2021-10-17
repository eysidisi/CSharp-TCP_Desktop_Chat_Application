using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Text_Me.Service;
using Xunit;
using static Text_Me.Service.Client;

namespace Text_Me.Test.ClientTests
{
    public class ConnectTests
    {

        private static readonly IPEndPoint DefaultLoopbackEndpoint = new IPEndPoint(IPAddress.Loopback, port: 0);

        public static int GetAvailablePort()
        {
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Bind(DefaultLoopbackEndpoint);
                return ((IPEndPoint)socket.LocalEndPoint).Port;
            }
        }


        [Fact]
        public void ClientShouldConnectToAServer()
        {
            ManualResetEvent receiveDone = new ManualResetEvent(false);

            TcpListener server = new TcpListener(IPAddress.Loopback, 0);
            server.Start();
            int serverPortNum = ((IPEndPoint)server.LocalEndpoint).Port;

            Client client = new Client();
            client.OnConnection += ResultFunc;
            ConnectionResult connectionResult = ConnectionResult.UNKNOWN;
            client.Connect(IPAddress.Loopback.ToString(), serverPortNum);
            void ResultFunc(ConnectionResult result)
            {
                connectionResult = result;
                receiveDone.Set();
            }

            receiveDone.WaitOne();

            Assert.True(connectionResult == ConnectionResult.SUCCESS);
        }


        [Fact]
        public void ClientShouldNOTConnectToAServer()
        {
            ManualResetEvent receiveDone = new ManualResetEvent(false);

            TcpListener server = new TcpListener(IPAddress.Loopback, 0);
            server.Start();
            int serverPortNum = ((IPEndPoint)server.LocalEndpoint).Port;

            Client client = new Client();
            client.OnConnection += ResultFunc;
            ConnectionResult connectionResult = ConnectionResult.UNKNOWN;
            
            client.Connect(IPAddress.Loopback.ToString(), GetAvailablePort());
            void ResultFunc(ConnectionResult result)
            {
                connectionResult = result;
                receiveDone.Set();
            }

            receiveDone.WaitOne();

            Assert.True(connectionResult == ConnectionResult.FAILURE);
        }

    }
}
