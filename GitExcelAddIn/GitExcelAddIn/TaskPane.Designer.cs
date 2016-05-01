namespace GitExcelAddIn
{
    partial class TaskPane
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.GitInfoTab2Button = new System.Windows.Forms.Button();
            this.GitEmailTextTab2 = new System.Windows.Forms.TextBox();
            this.GitNameTextTab2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.UserPassTab2Submit = new System.Windows.Forms.Button();
            this.PasswordTextTab2 = new System.Windows.Forms.TextBox();
            this.UsernameTextTab2 = new System.Windows.Forms.TextBox();
            this.PasswordTab2 = new System.Windows.Forms.Label();
            this.UsernameTab2 = new System.Windows.Forms.Label();
            this.backLabelTab2 = new System.Windows.Forms.LinkLabel();
            this.bitbucketButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.infoLabel = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 32);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Commit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Current Changes: 7";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.webBrowser1.Location = new System.Drawing.Point(-4, 99);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(198, 779);
            this.webBrowser1.TabIndex = 9;
            // 
            // buttonSettings
            // 
            this.buttonSettings.Location = new System.Drawing.Point(111, 32);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(75, 23);
            this.buttonSettings.TabIndex = 10;
            this.buttonSettings.Text = "Settings";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(198, 689);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.infoLabel);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.webBrowser1);
            this.tabPage1.Controls.Add(this.buttonSettings);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(190, 663);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.GitInfoTab2Button);
            this.tabPage2.Controls.Add(this.GitEmailTextTab2);
            this.tabPage2.Controls.Add(this.GitNameTextTab2);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.UserPassTab2Submit);
            this.tabPage2.Controls.Add(this.PasswordTextTab2);
            this.tabPage2.Controls.Add(this.UsernameTextTab2);
            this.tabPage2.Controls.Add(this.PasswordTab2);
            this.tabPage2.Controls.Add(this.UsernameTab2);
            this.tabPage2.Controls.Add(this.backLabelTab2);
            this.tabPage2.Controls.Add(this.bitbucketButton);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(190, 663);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // GitInfoTab2Button
            // 
            this.GitInfoTab2Button.Location = new System.Drawing.Point(51, 244);
            this.GitInfoTab2Button.Name = "GitInfoTab2Button";
            this.GitInfoTab2Button.Size = new System.Drawing.Size(101, 23);
            this.GitInfoTab2Button.TabIndex = 13;
            this.GitInfoTab2Button.Text = "Update";
            this.GitInfoTab2Button.UseVisualStyleBackColor = true;
            this.GitInfoTab2Button.Click += new System.EventHandler(this.GitInfoTab2Button_Click);
            // 
            // GitEmailTextTab2
            // 
            this.GitEmailTextTab2.Location = new System.Drawing.Point(71, 217);
            this.GitEmailTextTab2.Name = "GitEmailTextTab2";
            this.GitEmailTextTab2.Size = new System.Drawing.Size(113, 20);
            this.GitEmailTextTab2.TabIndex = 12;
            // 
            // GitNameTextTab2
            // 
            this.GitNameTextTab2.Location = new System.Drawing.Point(71, 190);
            this.GitNameTextTab2.Name = "GitNameTextTab2";
            this.GitNameTextTab2.Size = new System.Drawing.Size(113, 20);
            this.GitNameTextTab2.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 220);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Email";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "User information on your Versions";
            // 
            // UserPassTab2Submit
            // 
            this.UserPassTab2Submit.Location = new System.Drawing.Point(51, 136);
            this.UserPassTab2Submit.Name = "UserPassTab2Submit";
            this.UserPassTab2Submit.Size = new System.Drawing.Size(101, 23);
            this.UserPassTab2Submit.TabIndex = 7;
            this.UserPassTab2Submit.Text = "Update";
            this.UserPassTab2Submit.UseVisualStyleBackColor = true;
            this.UserPassTab2Submit.Click += new System.EventHandler(this.UserPassTab2Submit_Click);
            // 
            // PasswordTextTab2
            // 
            this.PasswordTextTab2.Location = new System.Drawing.Point(71, 110);
            this.PasswordTextTab2.Name = "PasswordTextTab2";
            this.PasswordTextTab2.PasswordChar = '*';
            this.PasswordTextTab2.Size = new System.Drawing.Size(113, 20);
            this.PasswordTextTab2.TabIndex = 6;
            // 
            // UsernameTextTab2
            // 
            this.UsernameTextTab2.Location = new System.Drawing.Point(71, 84);
            this.UsernameTextTab2.Name = "UsernameTextTab2";
            this.UsernameTextTab2.Size = new System.Drawing.Size(113, 20);
            this.UsernameTextTab2.TabIndex = 5;
            // 
            // PasswordTab2
            // 
            this.PasswordTab2.AutoSize = true;
            this.PasswordTab2.Location = new System.Drawing.Point(10, 113);
            this.PasswordTab2.Name = "PasswordTab2";
            this.PasswordTab2.Size = new System.Drawing.Size(53, 13);
            this.PasswordTab2.TabIndex = 4;
            this.PasswordTab2.Text = "Password";
            // 
            // UsernameTab2
            // 
            this.UsernameTab2.AutoSize = true;
            this.UsernameTab2.Location = new System.Drawing.Point(10, 86);
            this.UsernameTab2.Name = "UsernameTab2";
            this.UsernameTab2.Size = new System.Drawing.Size(55, 13);
            this.UsernameTab2.TabIndex = 3;
            this.UsernameTab2.Text = "Username";
            // 
            // backLabelTab2
            // 
            this.backLabelTab2.AutoSize = true;
            this.backLabelTab2.Location = new System.Drawing.Point(10, 7);
            this.backLabelTab2.Name = "backLabelTab2";
            this.backLabelTab2.Size = new System.Drawing.Size(41, 13);
            this.backLabelTab2.TabIndex = 2;
            this.backLabelTab2.TabStop = true;
            this.backLabelTab2.Text = "< Back";
            this.backLabelTab2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.backLabelTab2_LinkClicked);
            // 
            // bitbucketButton
            // 
            this.bitbucketButton.Location = new System.Drawing.Point(10, 53);
            this.bitbucketButton.Name = "bitbucketButton";
            this.bitbucketButton.Size = new System.Drawing.Size(174, 23);
            this.bitbucketButton.TabIndex = 1;
            this.bitbucketButton.Text = "Grant permission";
            this.bitbucketButton.UseVisualStyleBackColor = true;
            this.bitbucketButton.Click += new System.EventHandler(this.bitbucketButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "BitBucket account";
            // 
            // infoLabel
            // 
            this.infoLabel.Location = new System.Drawing.Point(9, 67);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(177, 39);
            this.infoLabel.TabIndex = 11;
            this.infoLabel.Text = "Welcome to SheetGit";
            this.infoLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // TaskPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.tabControl1);
            this.Name = "TaskPane";
            this.Size = new System.Drawing.Size(198, 689);
            this.Load += new System.EventHandler(this.TaskPane_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bitbucketButton;
        private System.Windows.Forms.LinkLabel backLabelTab2;
        private System.Windows.Forms.Button UserPassTab2Submit;
        private System.Windows.Forms.TextBox PasswordTextTab2;
        private System.Windows.Forms.TextBox UsernameTextTab2;
        private System.Windows.Forms.Label PasswordTab2;
        private System.Windows.Forms.Label UsernameTab2;
        private System.Windows.Forms.Button GitInfoTab2Button;
        private System.Windows.Forms.TextBox GitEmailTextTab2;
        private System.Windows.Forms.TextBox GitNameTextTab2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label infoLabel;
    }
}
