namespace Text_Me_Server.UI
{
    partial class ServerForm
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
            this.serverConnectionUserControl = new Text_Me_Client.UserControls.ServerConnectionUserControl();
            this.messageWindowUserControl = new Text_Me_Client.UserControls.MessageWindowUserControl();
            this.SuspendLayout();
            // 
            // serverConnectionUserControl
            // 
            this.serverConnectionUserControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.serverConnectionUserControl.Location = new System.Drawing.Point(370, 42);
            this.serverConnectionUserControl.Name = "serverConnectionUserControl";
            this.serverConnectionUserControl.Size = new System.Drawing.Size(411, 461);
            this.serverConnectionUserControl.TabIndex = 2;
            // 
            // messageWindowUserControl
            // 
            this.messageWindowUserControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.messageWindowUserControl.Location = new System.Drawing.Point(12, 42);
            this.messageWindowUserControl.Name = "messageWindowUserControl";
            this.messageWindowUserControl.Size = new System.Drawing.Size(343, 461);
            this.messageWindowUserControl.TabIndex = 1;
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.serverConnectionUserControl);
            this.Controls.Add(this.messageWindowUserControl);
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "ServerForm";
            this.Text = "Server";
            this.ResumeLayout(false);

        }

        #endregion
        private Text_Me_Client.UserControls.MessageWindowUserControl messageWindowUserControl;
        private Text_Me_Client.UserControls.ServerConnectionUserControl serverConnectionUserControl;
    }
}

