using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Text_Me.Service
{

    public class ClientSocket : ParentSocket
    {
        public ClientSocket()
        {
        }
        public void Connect(string remoteIPAddress, int remotePortNum)
        {
            if (_clientSocket != null && _clientSocket.Connected)
            {
                OnLog?.Invoke(_alreadyConnectionExistsMessage);
                return;
            }

            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 0);
            _clientSocket = new TcpClient(localEndPoint);
            _clientSocket.BeginConnect(remoteIPAddress, remotePortNum, new AsyncCallback(ConnectCallback), null);
        }
        protected override void CloseConnection()
        {
            //_clientSocket.Close();
        }
        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                _clientSocket.EndConnect(ar);
            }
            catch (SocketException)
            {

            }

            if (_clientSocket.Connected == true)
            {
                OnConnectionStatusChanged?.Invoke(ConnectionResult.SUCCESS);
                _checkConnectionTimer = new Timer(HearthBeatFunc, null, 250, 1000);
                StartReceivingMessage();
            }

            else
            {
                OnConnectionStatusChanged?.Invoke(ConnectionResult.FAILURE);
            }
        }
    }
}
