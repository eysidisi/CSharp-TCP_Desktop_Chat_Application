using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Text_Me.Service
{
    public enum ConnectionResult
    {
        SUCCESS,
        FAILURE,
        UNKNOWN
    };

    public class Client
    {
        TcpClient _tcpClient;

        public Action<ConnectionResult> OnConnection;
        public Action<string> OnMessageReceived;

        public Client()
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 0);
            _tcpClient = new TcpClient(localEndPoint);
        }

        /// <summary>
        /// Async connection function
        /// </summary>
        public void Connect(string remoteIPAddress, int remotePortNum)
        {
            _tcpClient.BeginConnect(remoteIPAddress, remotePortNum, new AsyncCallback(ConnectCallback), null);
        }
        public void SendMessage(string message)
        {
            byte[] bytesToSend = Encoding.UTF8.GetBytes(message);

            _tcpClient.Client.Send(bytesToSend);
        }
        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                _tcpClient.EndConnect(ar);
            }
            catch (SocketException)
            {

            }

            if (_tcpClient.Connected == true)
            {
                OnConnection?.Invoke(ConnectionResult.SUCCESS);
            }

            else
            {
                OnConnection?.Invoke(ConnectionResult.FAILURE);
            }
        }
    }
}
