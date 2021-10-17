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

namespace Text_Me.Test.ServerTests
{
    public class ReceiveMessageTest
    {
        [Fact]
        public void ServerShouldReceiveMessage()
        {
            ManualResetEvent receiveDone = new ManualResetEvent(false);

            TcpClient tcpClient = new TcpClient();

            string sentMessage = "Hellö Wörld";
            string receivedMessage = "";

            int serverPortNum=Helpers.GetAvailablePort();   
            Server server = new Server(IPAddress.Loopback.ToString(), serverPortNum);
            
            server.OnMessageReceived += MessageReceivedFunc;
            void MessageReceivedFunc(string message)
            {
                receivedMessage = message;
                receiveDone.Set();
            }

            server.StartAcceptingConnection();
            tcpClient.Connect(IPAddress.Loopback.ToString(), serverPortNum);

            byte[] bytesToSend = Encoding.UTF8.GetBytes(sentMessage);
            tcpClient.GetStream().Write(bytesToSend, 0, bytesToSend.Length);

            receiveDone.WaitOne();

            Assert.True(receivedMessage == sentMessage);
        }
    }
}
