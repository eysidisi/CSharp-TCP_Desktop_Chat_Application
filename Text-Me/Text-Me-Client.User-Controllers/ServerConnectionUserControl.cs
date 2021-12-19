using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Text_Me.Service;

namespace Text_Me_Client.UserControls
{
    public partial class ServerConnectionUserControl : UserControl
    {
        ServerSocket server = null;
        StringBuilder logStringBuilder = new StringBuilder();
        int logTextNum = 1;
        public Action<string> OnMessageReceived;

        public ServerConnectionUserControl()
        {
            InitializeComponent();
            FillIPAddressListBox();
        }

        private void FillIPAddressListBox()
        {
            try
            {
                string hostString = Dns.GetHostName();
                // Get 'IPHostEntry' object containing information like host name, IP addresses, aliases for a host.
                IPHostEntry hostInfo = Dns.GetHostEntry(hostString);
                Console.WriteLine("Host name : " + hostInfo.HostName);
                Console.WriteLine("IP address List : ");
                for (int index = 0; index < hostInfo.AddressList.Length; index++)
                {
                    if (hostInfo.AddressList[index].AddressFamily == AddressFamily.InterNetwork)
                    {
                        listBoxIPAddresses.Items.Add(hostInfo.AddressList[index].ToString());
                    }
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            }

        }
        private void ButtonWaitForConnection_Click(object sender, EventArgs e)
        {
            if (server != null)
            {
                LogText("A connection is present or already waiting for connection!");
                return;
            }

            string ipAddressStr = textBoxIP.Text;
            string portNumStr = textBoxPortNum.Text;

            if (IPAddress.TryParse(ipAddressStr, out _) == false)
            {
                LogText("Invalid IP Address");
                return;
            }

            int portNum;

            if (int.TryParse(portNumStr, out portNum) == false)
            {
                LogText("Invalid Port Num!");
                return;
            }
            try
            {
                server = new ServerSocket(ipAddressStr, portNum);
                server.OnLog += LogReceived;
                server.OnConnectionStatusChanged += ConnectionChanged;
                server.OnMessageReceived += MessageReceived;
                server.StartAcceptingConnection();
                SetConnectionStatusColor(Color.Yellow);
            }
            catch (Exception ex)
            {
                server = null;
                LogText(ex.Message);
            }
        }
        private void LogText(string text)
        {
            logStringBuilder.AppendLine($"{logTextNum}: {text}");
            UpdateLogText();
            logTextNum++;
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
        private void SetConnectionStatusColor(Color color)
        {
            buttonConnectionStatus.BackColor = color;
        }
        private void MessageReceived(string receivedMessage)
        {
            try
            {
                OnMessageReceived?.Invoke(receivedMessage);
            }
            catch (Exception ex)
            {
                LogText(ex.Message);
            }
        }
        private void ConnectionChanged(ConnectionResult connectionResult)
        {
            LogText($"Connection Result: {connectionResult}");

            if (connectionResult == ConnectionResult.SUCCESS)
            {
                SetConnectionStatusColor(Color.Green);
            }
            else
            {
                SetConnectionStatusColor(Color.Red);
                server = null;
            }
        }
        private void LogReceived(string logStr)
        {
            LogText(logStr);
        }
        public bool SendMessage(string messageToSend)
        {
            if (server == null || server.IsConnected == false)
            {
                LogText("No connection is available!");
                return false;
            }

            try
            {
                server.SendMessage(messageToSend);
                return true;
            }
            catch (Exception ex)
            {
                LogText(ex.Message);
                return false;
            }
        }
        private void ListBoxIPAddresses_SelectedValueChanged(object sender, EventArgs e)
        {
            string selectedIPAddress = listBoxIPAddresses.SelectedItem.ToString();
            textBoxIP.Text = selectedIPAddress;
        }
    }
}
