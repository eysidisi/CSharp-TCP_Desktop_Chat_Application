using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Text_Me.Service;

namespace Text_Me_Client.UserControls
{
    public partial class ClientConnectionUserControl : UserControl
    {
        ClientSocket client;
        StringBuilder logStringBuilder = new StringBuilder();
        public Action<string> OnMessageReceived;
        private int logTextNum = 0;

        public ClientConnectionUserControl()
        {
            InitializeComponent();
        }
        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                LogText("Already connected or trying to connect!");
                return;
            }

            string ipAddressStr = textBoxIP.Text;
            string portNumStr = textBoxPortNum.Text;

            if (IPAddress.TryParse(ipAddressStr, out _) == false)
            {
                LogText("Invalid IP address!");
                return;
            }

            if (int.TryParse(portNumStr, out _) == false)
            {
                LogText("Invalid port number!");
                return;
            }
            try
            {
                client = new Text_Me.Service.ClientSocket();
                client.OnConnectionStatusChanged += ConnectionStatusChanged;
                client.OnMessageReceived += MessageReceived;
                int portNum = int.Parse(portNumStr);
                client.Connect(ipAddressStr, portNum);
                LogText("Trying to connect...");
                SetConnectionStatusColor(Color.Yellow);
            }

            catch (Exception ex)
            {
                client = null;
                LogText(ex.Message);
            }
        }
        private void MessageReceived(string receivedMessage)
        {
            OnMessageReceived?.Invoke(receivedMessage);
        }
        private void ConnectionStatusChanged(ConnectionResult connectionStatus)
        {
            LogText("Connection Result: " + connectionStatus.ToString());

            if (connectionStatus == ConnectionResult.SUCCESS)
            {
                SetConnectionStatusColor(Color.Green);
            }
            else
            {
                SetConnectionStatusColor(Color.Red);
                client = null;
            }
        }
        private void LogText(string text)
        {
            logTextNum++;
            logStringBuilder.AppendLine($"{logTextNum}: {text}");
            UpdateLogText();
        }
        private void SetConnectionStatusColor(Color color)
        {
            buttonConnectionStatus.BackColor = color;
        }
        private void UpdateLogText()
        {
            if (textBoxLog.InvokeRequired)
            {
                textBoxLog.Invoke(new MethodInvoker(UpdateLogText));
            }

            else
            {
                textBoxLog.Text = logStringBuilder.ToString();
                textBoxLog.SelectionStart = textBoxLog.Text.Length;
                textBoxLog.ScrollToCaret();
            }
        }
        public bool SendMessage(string messageToSend)
        {
            if (client == null || client.IsConnected == false)
            {
                LogText("No connection available!");
                return false;
            }

            try
            {
                client.SendMessage(messageToSend);
                return true;
            }
            catch (Exception e)
            {
                LogText(e.Message);
                return false;
            }
        }
    }
}
