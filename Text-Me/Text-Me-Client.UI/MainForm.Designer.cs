using Text_Me_Client.UI.UserControls;
namespace Text_Me_Client.UI
{
    partial class MainForm
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
            this.messageWindowUserControl = new Text_Me_Client.UI.UserControls.MessageWindowUserControl();
            this.connectionUserControl = new Text_Me_Client.UI.UserControls.ConnectionUserControl();
            this.SuspendLayout();
            // 
            // messageWindowUserControl
            // 
            this.messageWindowUserControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.messageWindowUserControl.Location = new System.Drawing.Point(12, 45);
            this.messageWindowUserControl.Name = "messageWindowUserControl";
            this.messageWindowUserControl.Size = new System.Drawing.Size(557, 382);
            this.messageWindowUserControl.TabIndex = 1;
            // 
            // connectionUserControl
            // 
            this.connectionUserControl.AutoSize = true;
            this.connectionUserControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.connectionUserControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.connectionUserControl.Location = new System.Drawing.Point(636, 114);
            this.connectionUserControl.Name = "connectionUserControl";
            this.connectionUserControl.Size = new System.Drawing.Size(233, 241);
            this.connectionUserControl.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 495);
            this.Controls.Add(this.messageWindowUserControl);
            this.Controls.Add(this.connectionUserControl);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ConnectionUserControl connectionUserControl;
        private MessageWindowUserControl messageWindowUserControl;
    }
}

