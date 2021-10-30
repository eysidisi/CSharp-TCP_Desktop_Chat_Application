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
    public enum ConnectionResult
    {
        SUCCESS,
        FAILURE,
        DISCONNECTED,
        UNKNOWN
    };

    public class Client
    {
        private const int BufferSize = 256;
        TcpClient _tcpClient;
        Timer _checkConnectionTimer;


        public Action<ConnectionResult> OnConnectionStatusChanged;
        public Action<string> OnMessageReceived;
        public Action<string> OnLog;

        public Client()
        {
        }

        public void Connect(string remoteIPAddress, int remotePortNum)
        {
            if (_tcpClient != null && _tcpClient.Connected)
            {
                OnLog?.Invoke(CommonMessages._alreadyConnectionExistsMessage);
                return;
            }

            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 0);
            _tcpClient = new TcpClient(localEndPoint);
            _tcpClient.BeginConnect(remoteIPAddress, remotePortNum, new AsyncCallback(ConnectCallback), null);
        }
        public void SendMessage(string message)
        {
            if (_tcpClient.Connected == false)
            {
                OnLog?.Invoke(CommonMessages._noConnectionMessage);
                return;
            }

            if (CheckConnection() == false)
            {
                OnConnectionStatusChanged?.Invoke(ConnectionResult.DISCONNECTED);
                return;
            }

            byte[] bytesToSend = Encoding.Default.GetBytes(message);
            _tcpClient.GetStream().Write(bytesToSend, 0, bytesToSend.Length);
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
                OnConnectionStatusChanged?.Invoke(ConnectionResult.SUCCESS);
                _checkConnectionTimer = new Timer(HearthBeatFunc, null, 250, 1000);
                StartReceivingMessage();
            }

            else
            {
                OnConnectionStatusChanged?.Invoke(ConnectionResult.FAILURE);
            }
        }
        private void HearthBeatFunc(object state)
        {
            // Already disconnected
            if (_tcpClient.Connected == false)
            {
                _checkConnectionTimer.Dispose();
                return;
            }

            if (CheckConnection() == false)
            {
                _checkConnectionTimer.Dispose();
                OnConnectionStatusChanged.Invoke(ConnectionResult.DISCONNECTED);
            }
        }
        private bool CheckConnection()
        {
            try
            {
                _tcpClient.Client.Send(Encoding.UTF8.GetBytes(CommonMessages._heartBeatMessage));
                return true;
            }
            catch (SocketException e)
            {
                // 10035 == WSAEWOULDBLOCK
                if (e.NativeErrorCode.Equals(10035))
                    return true;
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        private void StartReceivingMessage()
        {
            NetworkStream stream = _tcpClient.GetStream();

            byte[] receivedBytes = new byte[BufferSize];
            int numberOfBytesReceived;

            while (_tcpClient.Connected)
            {
                // Loop to receive all the data sent by the client.
                while (stream.DataAvailable && (numberOfBytesReceived = stream.Read(receivedBytes, 0, receivedBytes.Length)) != 0)
                {
                    // Translate data bytes to a ASCII string.
                    string receivedMessage = Encoding.Default.GetString(receivedBytes, 0, numberOfBytesReceived);

                    // Don't log heartbeat message to the user
                    if (receivedMessage.Equals(CommonMessages._heartBeatMessage))
                    {
                        continue;
                    }

                    // Heartbeat message and other messages can concatenate
                    if (receivedMessage.Contains(CommonMessages._heartBeatMessage))
                    {
                        receivedMessage = receivedMessage.Replace(CommonMessages._heartBeatMessage, "");
                    }

                    // Send back a response.
                    OnMessageReceived?.Invoke(receivedMessage);
                }
            }
        }
    }
}
