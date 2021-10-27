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
    public class ReceiveMessageTest
    {
        string sentMessage = "Hellö Wörld";
        string receivedMessage = "";
        ManualResetEvent receiveDone = new ManualResetEvent(false);

        [Fact]
        public void ClientShouldReceiveMessage()
        {
            int serverPortNum = Helpers.GetAvailablePort();

            Client client = new Client();
            client.OnMessageReceived += ClientReceivedMessage;

            TcpListener server = new TcpListener(IPAddress.Loopback, serverPortNum);
            server.Start();
            server.BeginAcceptTcpClient(new AsyncCallback(AcceptTcpClientCallback), server);

            client.Connect(IPAddress.Loopback.ToString(), serverPortNum);
            
            receiveDone.WaitOne();

            Assert.Equal( sentMessage, receivedMessage);
        }

        private void ClientReceivedMessage(string receivedMessage)
        {
            this.receivedMessage = receivedMessage;
            receiveDone.Set();
        }

        void AcceptTcpClientCallback(IAsyncResult ar)
        {
            TcpListener listener = (TcpListener)ar.AsyncState;
            var clientSocket = listener.EndAcceptTcpClient(ar);
            byte[] bytesToSend= ASCIIEncoding.UTF8.GetBytes(sentMessage);
            clientSocket.GetStream().Write(bytesToSend);
        }

    }
}
