using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Text_Me.Service
{
    public class Client
    {
        public enum ConnectionResult
        {
            SUCCESS,
            FAILURE,
            UNKNOWN
        };

        TcpClient _tcpClient;

        public Client()
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 0);
            _tcpClient = new TcpClient(localEndPoint);
        }

        /// <summary>
        /// Async connection function
        /// </summary>
        public void Connect(string remoteIPAddress, int remotePortNum, Action<ConnectionResult> resultFunc)
        {
            _tcpClient.BeginConnect(remoteIPAddress, remotePortNum, new AsyncCallback(ConnectCallback), resultFunc);
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            var resultFunc = (Action<ConnectionResult>)ar.AsyncState;

            try
            {
                _tcpClient.EndConnect(ar);
            }
            catch (SocketException)
            {
                
            }

            if (_tcpClient.Connected == true)
            {
                resultFunc(ConnectionResult.SUCCESS);
            }

            else
            {
                resultFunc(ConnectionResult.FAILURE);
            }
        }
    }
}
