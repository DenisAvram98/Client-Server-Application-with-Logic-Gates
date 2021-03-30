namespace Client
{
    partial class Form1
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
            this.sendTB = new System.Windows.Forms.TextBox();
            this.receiveTB = new System.Windows.Forms.TextBox();
            this.sendBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // sendTB
            // 
            this.sendTB.Location = new System.Drawing.Point(12, 12);
            this.sendTB.Multiline = true;
            this.sendTB.Name = "sendTB";
            this.sendTB.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.sendTB.Size = new System.Drawing.Size(374, 245);
            this.sendTB.TabIndex = 0;
            // 
            // receiveTB
            // 
            this.receiveTB.Location = new System.Drawing.Point(392, 12);
            this.receiveTB.Multiline = true;
            this.receiveTB.Name = "receiveTB";
            this.receiveTB.ReadOnly = true;
            this.receiveTB.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.receiveTB.Size = new System.Drawing.Size(396, 245);
            this.receiveTB.TabIndex = 1;
            // 
            // sendBtn
            // 
            this.sendBtn.AutoSize = true;
            this.sendBtn.Location = new System.Drawing.Point(303, 263);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(83, 27);
            this.sendBtn.TabIndex = 2;
            this.sendBtn.Text = "Send data";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 328);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.receiveTB);
            this.Controls.Add(this.sendTB);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox sendTB;
        private System.Windows.Forms.TextBox receiveTB;
        private System.Windows.Forms.Button sendBtn;
    }
}

