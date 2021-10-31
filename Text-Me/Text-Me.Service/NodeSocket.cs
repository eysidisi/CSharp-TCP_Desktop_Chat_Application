using System;
using System.Collections.Generic;
using System.Linq;
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

    public abstract class NodeSocket
    {
        protected TcpClient _clientSocket;
        private const int BufferSize = 1024;
        public Action<string> OnMessageReceived;
        public Action<ConnectionResult> OnConnectionStatusChanged;
        public Action<string> OnLog;
        protected Timer _checkConnectionTimer;

        protected readonly string _heartBeatMessage = "<I'm Alive>";
        protected readonly string _noConnectionMessage = "No Connection Avaible!";
        protected readonly string _alreadyConnectionExistsMessage = "Already a connection exists!";

        protected void StartReceivingMessage()
        {
            NetworkStream stream = _clientSocket.GetStream();

            byte[] receivedBytes = new byte[BufferSize];
            int numberOfBytesReceived;

            while (_clientSocket.Connected)
            { // Loop to receive all the data sent by the client.
                while (stream.DataAvailable && (numberOfBytesReceived = stream.Read(receivedBytes, 0, receivedBytes.Length)) != 0)
                {
                    // Translate data bytes to a ASCII string.
                    string receivedMessage = Encoding.Default.GetString(receivedBytes, 0, numberOfBytesReceived);

                    // Don't log heartbeat message to the user
                    if (receivedMessage.Equals(_heartBeatMessage))
                    {
                        continue;
                    }

                    // Heartbeat message and other messages can concatenate
                    if (receivedMessage.Contains(_heartBeatMessage))
                    {
                        receivedMessage = receivedMessage.Replace(_heartBeatMessage, "");
                    }

                    // Send back a response.
                    OnMessageReceived?.Invoke(receivedMessage);
                }
            }
        }

        public void SendMessage(string message)
        {
            if (_clientSocket == null || _clientSocket.Connected == false)
            {
                OnLog?.Invoke(_noConnectionMessage);
                return;
            }

            if (CheckConnection() == false)
            {
                OnConnectionStatusChanged?.Invoke(ConnectionResult.DISCONNECTED);
                CloseConnection();
                return;
            }

            byte[] bytesToSend = Encoding.Default.GetBytes(message);
            _clientSocket.GetStream().Write(bytesToSend, 0, bytesToSend.Length);
        }

        abstract protected void CloseConnection();

        protected bool CheckConnection()
        {
            try
            {
                _clientSocket.Client.Send(Encoding.UTF8.GetBytes(_heartBeatMessage));
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
            catch (Exception)
            {
                return false;
            }
        }

        protected void HearthBeatFunc(object state)
        {
            // Already disconnected
            if (_clientSocket.Connected == false)
            {
                _checkConnectionTimer.Dispose();
                return;
            }

            if (CheckConnection() == false)
            {
                _checkConnectionTimer.Dispose();
                OnConnectionStatusChanged.Invoke(ConnectionResult.DISCONNECTED);
                CloseConnection();
            }
        }

    }
}
