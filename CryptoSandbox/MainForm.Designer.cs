namespace CryptoSandbox
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
            this.siteKeyTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.encryptButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.encryptedPasswordTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.encryptUsernameTextBox = new System.Windows.Forms.TextBox();
            this.decryptedPasswordTextBox = new System.Windows.Forms.TextBox();
            this.decryptUsernameTextBox = new System.Windows.Forms.TextBox();
            this.decryptButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // siteKeyTextBox
            // 
            this.siteKeyTextBox.Location = new System.Drawing.Point(85, 9);
            this.siteKeyTextBox.Name = "siteKeyTextBox";
            this.siteKeyTextBox.Size = new System.Drawing.Size(190, 20);
            this.siteKeyTextBox.TabIndex = 0;
            this.siteKeyTextBox.TextChanged += new System.EventHandler(this.SiteKeyTextT_OnChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Key";
            // 
            // encryptButton
            // 
            this.encryptButton.Enabled = false;
            this.encryptButton.Location = new System.Drawing.Point(200, 98);
            this.encryptButton.Name = "encryptButton";
            this.encryptButton.Size = new System.Drawing.Size(75, 23);
            this.encryptButton.TabIndex = 9;
            this.encryptButton.Text = "Encrypt";
            this.encryptButton.UseVisualStyleBackColor = true;
            this.encryptButton.Click += new System.EventHandler(this.encryptButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Password";
            // 
            // encryptedPasswordTextBox
            // 
            this.encryptedPasswordTextBox.Location = new System.Drawing.Point(85, 72);
            this.encryptedPasswordTextBox.Name = "encryptedPasswordTextBox";
            this.encryptedPasswordTextBox.Size = new System.Drawing.Size(190, 20);
            this.encryptedPasswordTextBox.TabIndex = 7;
            this.encryptedPasswordTextBox.TextChanged += new System.EventHandler(this.Password_OnChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Username";
            // 
            // encryptUsernameTextBox
            // 
            this.encryptUsernameTextBox.Location = new System.Drawing.Point(85, 46);
            this.encryptUsernameTextBox.Name = "encryptUsernameTextBox";
            this.encryptUsernameTextBox.Size = new System.Drawing.Size(190, 20);
            this.encryptUsernameTextBox.TabIndex = 5;
            this.encryptUsernameTextBox.TextChanged += new System.EventHandler(this.Username_OnChanged);
            // 
            // decryptedPasswordTextBox
            // 
            this.decryptedPasswordTextBox.Location = new System.Drawing.Point(85, 169);
            this.decryptedPasswordTextBox.Name = "decryptedPasswordTextBox";
            this.decryptedPasswordTextBox.ReadOnly = true;
            this.decryptedPasswordTextBox.Size = new System.Drawing.Size(190, 20);
            this.decryptedPasswordTextBox.TabIndex = 11;
            // 
            // decryptUsernameTextBox
            // 
            this.decryptUsernameTextBox.Location = new System.Drawing.Point(85, 143);
            this.decryptUsernameTextBox.Name = "decryptUsernameTextBox";
            this.decryptUsernameTextBox.ReadOnly = true;
            this.decryptUsernameTextBox.Size = new System.Drawing.Size(190, 20);
            this.decryptUsernameTextBox.TabIndex = 10;
            // 
            // decryptButton
            // 
            this.decryptButton.Enabled = false;
            this.decryptButton.Location = new System.Drawing.Point(200, 195);
            this.decryptButton.Name = "decryptButton";
            this.decryptButton.Size = new System.Drawing.Size(75, 23);
            this.decryptButton.TabIndex = 12;
            this.decryptButton.Text = "Decrypt";
            this.decryptButton.UseVisualStyleBackColor = true;
            this.decryptButton.Click += new System.EventHandler(this.decryptButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 250);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(284, 22);
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(26, 17);
            this.statusLabel.Text = "Idle";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 272);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.decryptButton);
            this.Controls.Add(this.decryptedPasswordTextBox);
            this.Controls.Add(this.decryptUsernameTextBox);
            this.Controls.Add(this.encryptButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.encryptedPasswordTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.encryptUsernameTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.siteKeyTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Crypto Testing";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox siteKeyTextBox;
        private System.Windows.Forms.Button encryptButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox encryptedPasswordTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox encryptUsernameTextBox;
        private System.Windows.Forms.TextBox decryptedPasswordTextBox;
        private System.Windows.Forms.TextBox decryptUsernameTextBox;
        private System.Windows.Forms.Button decryptButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
    }
}

