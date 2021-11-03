using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Text_Me.Service;

namespace Text_Me_Client.UI
{
    public partial class ConnectionUserControl : UserControl
    {
        Client client;
        StringBuilder logStringBuilder = new StringBuilder();

        public ConnectionUserControl()
        {
            InitializeComponent();
        }

        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            if (client != null && client.IsConnected)
            {
                return;
            }

            string ipAddressStr = textBoxIP.Text;
            string portNumStr = textBoxPortNum.Text;

            try
            {
                client = new Client();
                client.OnConnectionStatusChanged += LogReceived;
                int portNum = int.Parse(portNumStr);
                client.Connect(ipAddressStr, portNum);

            }

            catch (Exception)
            {

                throw;
            }
        }

        private void LogReceived(ConnectionResult log)
        {
            logStringBuilder.AppendLine("Connection Result: " + log.ToString());
            UpdateLogText();
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
            }
        }
    }
}
