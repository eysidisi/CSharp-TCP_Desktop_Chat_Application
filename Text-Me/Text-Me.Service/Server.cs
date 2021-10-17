using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace Text_Me.Service
{
    public class Server
    {
        TcpListener _listenerSocket;

        const int _portNum = 3838;
        private const int BufferSize = 256;

        public Action<ConnectionResult> OnConnection;
        public Action<string> OnMessageReceived;

        /// <summary>
        /// If no ip address provided uses first IPV4 address it finds in ip address list of device
        /// </summary>
        /// <param name="ipAddressStr"></param>
        public Server(string ipAddressStr = null)
        {
            if (ipAddressStr == null)
            {
                ipAddressStr = GetLocalIPAddress();
            }

            IPAddress ipAddress = IPAddress.Parse(ipAddressStr);

            _listenerSocket = new TcpListener(ipAddress, _portNum);
        }

        public void StartAcceptingConnection()
        {
            _listenerSocket.Start();
            _listenerSocket.BeginAcceptTcpClient(new AsyncCallback(AcceptTcpClientCallback), null);
        }

        private void AcceptTcpClientCallback(IAsyncResult ar)
        {
            TcpClient _clientSocket = _listenerSocket.EndAcceptTcpClient(ar);
            OnConnection(ConnectionResult.SUCCESS);
            StartReceivingMessage(_clientSocket.GetStream());
        }

        private void StartReceivingMessage(NetworkStream stream)
        {
            byte[] receivedBytes = new byte[BufferSize];
            int numberOfBytesReceived;

            // Loop to receive all the data sent by the client.
            while ((numberOfBytesReceived = stream.Read(receivedBytes, 0, receivedBytes.Length)) != 0)
            {
                // Translate data bytes to a ASCII string.
                string receivedMessage = Encoding.UTF8.GetString(receivedBytes, 0, numberOfBytesReceived);
                Console.WriteLine("Received: {0}", receivedMessage);

                // Send back a response.
                OnMessageReceived(receivedMessage);
            }

        }

        private string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
