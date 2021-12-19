using Text_Me_Client.UserControls;
namespace Text_Me_Client.UI
{
    partial class ClientForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.messageWindowUserControl = new Text_Me_Client.UserControls.MessageWindowUserControl();
            this.connectionUserControl = new Text_Me_Client.UserControls.ClientConnectionUserControl();
            this.SuspendLayout();
            // 
            // messageWindowUserControl
            // 
            this.messageWindowUserControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.messageWindowUserControl.Location = new System.Drawing.Point(5, 45);
            this.messageWindowUserControl.Name = "messageWindowUserControl";
            this.messageWindowUserControl.Size = new System.Drawing.Size(450, 456);
            this.messageWindowUserControl.TabIndex = 1;
            // 
            // connectionUserControl
            // 
            this.connectionUserControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.connectionUserControl.Location = new System.Drawing.Point(479, 45);
            this.connectionUserControl.Name = "connectionUserControl";
            this.connectionUserControl.Size = new System.Drawing.Size(300, 450);
            this.connectionUserControl.TabIndex = 0;
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.messageWindowUserControl);
            this.Controls.Add(this.connectionUserControl);
            this.Location = new System.Drawing.Point(800, 600);
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "ClientForm";
            this.Text = "Client";
            this.ResumeLayout(false);

        }

        #endregion

        private ClientConnectionUserControl connectionUserControl;
        private MessageWindowUserControl messageWindowUserControl;
    }
}

