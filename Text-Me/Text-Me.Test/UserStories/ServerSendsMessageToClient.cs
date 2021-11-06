using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Text_Me.Service;
using Xunit;

namespace Text_Me.Test.UserStories
{
    public class ServerSendsMessageToClient
    {
        string receivedMessage = "";
        ManualResetEvent connectionEstablished = new ManualResetEvent(false);
        ManualResetEvent messageReceived = new ManualResetEvent(false);

        [Fact]
        public void ServerSendsMessageToConnectedClient()
        {
            int serverPortNum = Helpers.GetAvailablePort();
            string iPAddress = IPAddress.Loopback.ToString();
            string messageToSend = "Motörhead Rulz!";

            // Create server
            ServerSocket server = new ServerSocket(iPAddress, serverPortNum);

            // Wait connection
            server.StartAcceptingConnection();
            server.OnConnectionStatusChanged += ClientConnected;

            // Create client
            ClientSocket client = new ClientSocket();
            client.OnMessageReceived += MessageReceived;

            // Connect to server
            client.Connect(iPAddress, serverPortNum);
            connectionEstablished.WaitOne();

            // Send message
            server.SendMessage(messageToSend);
            messageReceived.WaitOne();

            // Check if message is received
            Assert.Equal(messageToSend, receivedMessage);
        }

        private void MessageReceived(string receivedMessage)
        {
            this.receivedMessage = receivedMessage;
            messageReceived.Set();
        }

        private void ClientConnected(ConnectionResult result)
        {
            connectionEstablished.Set();
        }

    }
}
