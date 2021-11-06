using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Text_Me_Server.UI
{
    public partial class ServerForm : Form
    {
        public ServerForm()
        {
            InitializeComponent();
            messageWindowUserControl.OnSendMessage += SendMessage;
            serverConnectionUserControl.OnMessageReceived += MessageReceived;
        }

        private void MessageReceived(string receivedMessage)
        {
            messageWindowUserControl.LogReceivedMessage(receivedMessage);
        }

        private bool SendMessage(string messageToSend)
        {
            return serverConnectionUserControl.SendMessage(messageToSend);
        }
    }
}
