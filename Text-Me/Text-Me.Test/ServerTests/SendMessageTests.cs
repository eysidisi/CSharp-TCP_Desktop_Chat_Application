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
    public class SendMessageTests
    {
        [Fact]
        public async void ServerShouldSendMessage()
        {
            TcpClient client = new TcpClient();

            int serverPortNum= Helpers.GetAvailablePort();

            Server server = new Server(IPAddress.Loopback.ToString(),serverPortNum);

            ManualResetEvent receiveDone = new ManualResetEvent(false);

            server.StartAcceptingConnection();

            void OnConnect(ConnectionResult result)
            {
                receiveDone.Set();
            }

            server.OnConnection += OnConnect;

            client.Connect(IPAddress.Loopback, serverPortNum);

            receiveDone.WaitOne();

            byte[] buffer = new byte[1024];

            ValueTask<int> readTask = client.GetStream().ReadAsync(buffer);

            string messageToSend = "hellö wörld";

            server.SendMessage(messageToSend);

            int numberOfBytes = await readTask;

            byte[] receivedBytes = new byte[numberOfBytes];

            Array.Copy(buffer, receivedBytes, numberOfBytes);

            string receivedMessage = ASCIIEncoding.Default.GetString(receivedBytes);

            Assert.Equal(messageToSend, receivedMessage);
        }
    }
}
