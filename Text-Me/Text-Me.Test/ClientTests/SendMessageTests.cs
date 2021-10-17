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

namespace Text_Me.Test.ClientTests
{
    public class SendMessageTests
    {
        [Fact]

        public void ClientShouldSendMessage()
        {

            TcpListener server = new TcpListener(IPAddress.Loopback, 0);
            server.Start();
            int serverPortNum = ((IPEndPoint)server.LocalEndpoint).Port;


            var client = new Client();
            client.Connect(IPAddress.Loopback.ToString(), serverPortNum);

            string messageToSend = "Hellö Wörld! şİç";
            client.SendMessage(messageToSend);

            var connectedClient = server.AcceptTcpClient();
            NetworkStream stream = connectedClient.GetStream();

            int i;
            byte[] buffer = new byte[1024];
            // Loop to receive all the data sent by the client.
            i = stream.Read(buffer, 0, buffer.Length);
            var receivedStr = Encoding.UTF8.GetString(buffer, 0, i);

            server.Stop();
            Assert.Equal(messageToSend, receivedStr);
        }
    }
}
