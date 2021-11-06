using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Text_Me_Client.UserControls
{
    public partial class MessageWindowUserControl : UserControl
    {
        public Func<string, bool> OnSendMessage;
        string _messageBoxString;
        public MessageWindowUserControl()
        {
            InitializeComponent();
        }

        private void SendMessage()
        {
            string messageToSend = textBoxMessage.Text;
            if (String.IsNullOrEmpty(messageToSend))
            {
                return;
            }
            if (OnSendMessage != null && OnSendMessage(messageToSend))
            {
                _messageBoxString += $"Sent: {messageToSend}\r\n";
                UpdateMessageText();
            }
        }
        public void LogReceivedMessage(string receivedMessage)
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
                textBoxMessageDisplay.SelectionStart = textBoxMessageDisplay.Text.Length;
                textBoxMessageDisplay.ScrollToCaret();
            }
        }

        private void TextBoxMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendMessage();
                textBoxMessage.Text = string.Empty;
                e.SuppressKeyPress = true;
            }

        }
    }
}
