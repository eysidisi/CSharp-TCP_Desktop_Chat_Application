using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Text_Me.Service
{
    public class ServerSocket : ParentSocket
    {
        TcpListener _listenerSocket;
        const int _portNum = 3838;

        /// <summary>
        /// If no ip address provided uses first IPV4 address it finds in ip address list of device
        /// </summary>
        /// <param name="ipAddressStr"></param>
        public ServerSocket(string ipAddressStr = null, int portNum = _portNum)
        {
            if (ipAddressStr == null)
            {
                ipAddressStr = GetLocalIPAddress();
            }

            IPAddress ipAddress = IPAddress.Parse(ipAddressStr);
            _listenerSocket = new TcpListener(ipAddress, portNum);
        }

        public void StartAcceptingConnection()
        {
            if (_listenerSocket.Server.IsBound == true)
            {
                OnLog?.Invoke("Server is aldready listening!");
                return;
            }

            OnLog?.Invoke("Server started listening!");

            _listenerSocket.Start();
            _listenerSocket.BeginAcceptTcpClient(new AsyncCallback(AcceptTcpClientCallback), null);
        }

        private void AcceptTcpClientCallback(IAsyncResult ar)
        {
            _clientSocket = _listenerSocket.EndAcceptTcpClient(ar);
            OnConnectionStatusChanged?.Invoke(ConnectionResult.SUCCESS);
            _checkConnectionTimer = new Timer(HearthBeatFunc, null, 250, 1000);
            StartReceivingMessage();
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

        protected override void CloseConnection()
        {
            _listenerSocket.Stop();
            _clientSocket.Close();
        }
    }
}
