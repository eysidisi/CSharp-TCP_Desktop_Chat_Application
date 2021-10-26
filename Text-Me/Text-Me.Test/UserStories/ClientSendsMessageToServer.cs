using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Text_Me.Service;
using System.Net;
using System.Threading;

namespace Text_Me.Test.UserStories
{
    public class ClientSendsMessageToServer
    {
        string receivedMessage = "";
        ManualResetEvent connectionEstablished = new ManualResetEvent(false);
        ManualResetEvent messageReceived= new ManualResetEvent(false);

        [Fact]
        public void ClientSendsMessageToAnAlreadyActiveServer()
        {
            int serverPortNum = 50000;
            string iPAddress = IPAddress.Loopback.ToString();
            string messageToSend = "Motörhead Rulz!";

            // Create server
            Server server = new Server(iPAddress, serverPortNum);
            server.OnMessageReceived += MessageReceived;

            // Wait connection
            server.StartAcceptingConnection();

            // Create client
            Client client = new Client();

            // Connect to server
            client.Connect(iPAddress, serverPortNum);
            client.OnConnection += ClientConnected;
            connectionEstablished.WaitOne();

            // Send message
            client.SendMessage(messageToSend);
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
