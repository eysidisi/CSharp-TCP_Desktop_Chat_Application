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
            this.connectionUserControl1 = new Text_Me_Client.UI.ConnectionUserControl();
            this.SuspendLayout();
            // 
            // connectionUserControl1
            // 
            this.connectionUserControl1.AutoSize = true;
            this.connectionUserControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.connectionUserControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.connectionUserControl1.Location = new System.Drawing.Point(531, 122);
            this.connectionUserControl1.Name = "connectionUserControl1";
            this.connectionUserControl1.Size = new System.Drawing.Size(235, 212);
            this.connectionUserControl1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.connectionUserControl1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ConnectionUserControl connectionUserControl1;
    }
}

