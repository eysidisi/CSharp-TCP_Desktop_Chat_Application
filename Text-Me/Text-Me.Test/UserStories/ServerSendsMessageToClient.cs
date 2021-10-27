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
        public void ServerCanSendMessageToConnectedClient()
        {
            int serverPortNum = Helpers.GetAvailablePort();
            string iPAddress = IPAddress.Loopback.ToString();
            string messageToSend = "Motörhead Rulz!";

            // Create server
            Server server = new Server(iPAddress, serverPortNum);

            // Wait connection
            server.StartAcceptingConnection();
            server.OnConnection += ClientConnected;

            // Create client
            Client client = new Client();
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
