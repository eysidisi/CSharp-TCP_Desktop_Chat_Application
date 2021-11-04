using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Text_Me_Client.UI.UserControls
{
    public partial class MessageWindowUserControl : UserControl
    {
        public Action<string> OnSendMessage;
        string _messageBoxString;
        public MessageWindowUserControl()
        {
            InitializeComponent();
        }

        private void ButtonSend_Click(object sender, EventArgs e)
        {
            string messageToSend = textBoxMessage.Text;
            _messageBoxString += $"Send: {messageToSend}\r\n";
            OnSendMessage?.Invoke(messageToSend);
            UpdateMessageText();
        }

        public void MessageReceived(string receivedMessage)
        {
            _messageBoxString += $"Received: {receivedMessage}\r\n";
            UpdateMessageText();
        }

        public void UpdateMessageText()
        {
            if (textBoxMessageDisplay.InvokeRequired)
            {
                textBoxMessageDisplay.Invoke(new MethodInvoker(UpdateMessageText));
            }

            else
            {
                textBoxMessageDisplay.Text = _messageBoxString;
            }

        }
    }
}
