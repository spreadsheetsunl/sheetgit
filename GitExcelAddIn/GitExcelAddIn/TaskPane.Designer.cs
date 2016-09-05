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
            this.mergebutton = new System.Windows.Forms.Button();
            this.infoLabel = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.metricsCombobox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
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
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.legendValues = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.changeInfoText = new System.Windows.Forms.Label();
            this.exitDiffTab = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.conflictNumberLabel = new System.Windows.Forms.Label();
            this.conflictCountLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.macTrackBar1 = new XComponent.SliderBar.MACTrackBar();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 32);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Create Version";
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
            this.label3.Visible = false;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.webBrowser1.Location = new System.Drawing.Point(-4, 126);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(198, 752);
            this.webBrowser1.TabIndex = 9;
            // 
            // buttonSettings
            // 
            this.buttonSettings.Location = new System.Drawing.Point(98, 32);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(88, 23);
            this.buttonSettings.TabIndex = 10;
            this.buttonSettings.Text = "Settings";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(198, 689);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tabPage1.Controls.Add(this.mergebutton);
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
            // 
            // mergebutton
            // 
            this.mergebutton.Location = new System.Drawing.Point(6, 58);
            this.mergebutton.Name = "mergebutton";
            this.mergebutton.Size = new System.Drawing.Size(178, 23);
            this.mergebutton.TabIndex = 12;
            this.mergebutton.Text = "Place versions in trunk";
            this.mergebutton.UseVisualStyleBackColor = true;
            this.mergebutton.Click += new System.EventHandler(this.mergebutton_Click);
            // 
            // infoLabel
            // 
            this.infoLabel.Location = new System.Drawing.Point(9, 84);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(177, 39);
            this.infoLabel.TabIndex = 11;
            this.infoLabel.Text = "Welcome to SheetGit";
            this.infoLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tabPage2.Controls.Add(this.metricsCombobox);
            this.tabPage2.Controls.Add(this.label6);
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
            // 
            // metricsCombobox
            // 
            this.metricsCombobox.FormattingEnabled = true;
            this.metricsCombobox.Items.AddRange(new object[] {
            "Every workbook change",
            "After 1 minute without changes",
            "Every minute",
            "Fully manual"});
            this.metricsCombobox.Location = new System.Drawing.Point(10, 320);
            this.metricsCombobox.Name = "metricsCombobox";
            this.metricsCombobox.Size = new System.Drawing.Size(174, 21);
            this.metricsCombobox.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 303);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Versioning metrics";
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
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tabPage3.Controls.Add(this.macTrackBar1);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.legendValues);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.changeInfoText);
            this.tabPage3.Controls.Add(this.exitDiffTab);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(190, 663);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Purple;
            this.label8.Location = new System.Drawing.Point(6, 637);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Formulas";
            // 
            // legendValues
            // 
            this.legendValues.AutoSize = true;
            this.legendValues.ForeColor = System.Drawing.Color.Green;
            this.legendValues.Location = new System.Drawing.Point(6, 624);
            this.legendValues.Name = "legendValues";
            this.legendValues.Size = new System.Drawing.Size(39, 13);
            this.legendValues.TabIndex = 4;
            this.legendValues.Text = "Values";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 602);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Cell color legend:";
            // 
            // changeInfoText
            // 
            this.changeInfoText.Location = new System.Drawing.Point(58, 55);
            this.changeInfoText.Name = "changeInfoText";
            this.changeInfoText.Size = new System.Drawing.Size(132, 130);
            this.changeInfoText.TabIndex = 2;
            this.changeInfoText.Text = "Change Info Text";
            // 
            // exitDiffTab
            // 
            this.exitDiffTab.Location = new System.Drawing.Point(7, 17);
            this.exitDiffTab.Name = "exitDiffTab";
            this.exitDiffTab.Size = new System.Drawing.Size(177, 23);
            this.exitDiffTab.TabIndex = 1;
            this.exitDiffTab.Text = "Exit Comparison";
            this.exitDiffTab.UseVisualStyleBackColor = true;
            this.exitDiffTab.Click += new System.EventHandler(this.exitDiffTab_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tabPage4.Controls.Add(this.conflictNumberLabel);
            this.tabPage4.Controls.Add(this.conflictCountLabel);
            this.tabPage4.Controls.Add(this.label9);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(190, 663);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "tabPage4";
            // 
            // conflictNumberLabel
            // 
            this.conflictNumberLabel.AutoSize = true;
            this.conflictNumberLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conflictNumberLabel.ForeColor = System.Drawing.Color.Maroon;
            this.conflictNumberLabel.Location = new System.Drawing.Point(111, 154);
            this.conflictNumberLabel.Name = "conflictNumberLabel";
            this.conflictNumberLabel.Size = new System.Drawing.Size(21, 13);
            this.conflictNumberLabel.TabIndex = 2;
            this.conflictNumberLabel.Text = "99";
            // 
            // conflictCountLabel
            // 
            this.conflictCountLabel.AutoSize = true;
            this.conflictCountLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.conflictCountLabel.Location = new System.Drawing.Point(7, 154);
            this.conflictCountLabel.Name = "conflictCountLabel";
            this.conflictCountLabel.Size = new System.Drawing.Size(98, 13);
            this.conflictCountLabel.TabIndex = 1;
            this.conflictCountLabel.Text = "Conflicts remaining:";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(7, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(177, 119);
            this.label9.TabIndex = 0;
            this.label9.Text = "Conflicts have been found between the trunk and your branch. \r\n\r\nYou will see the" +
    "m as cells in yellow saying <CONFLICT>. \r\n\r\nClick them and use the dropdown\r\nto " +
    "choose which version to use.";
            // 
            // macTrackBar1
            // 
            this.macTrackBar1.BackColor = System.Drawing.Color.Transparent;
            this.macTrackBar1.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.macTrackBar1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.macTrackBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(125)))), ((int)(((byte)(123)))));
            this.macTrackBar1.IndentHeight = 6;
            this.macTrackBar1.Location = new System.Drawing.Point(3, 46);
            this.macTrackBar1.Maximum = 10;
            this.macTrackBar1.Minimum = 0;
            this.macTrackBar1.Name = "macTrackBar1";
            this.macTrackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.macTrackBar1.Size = new System.Drawing.Size(52, 553);
            this.macTrackBar1.TabIndex = 6;
            this.macTrackBar1.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(146)))), ((int)(((byte)(148)))));
            this.macTrackBar1.TickHeight = 4;
            this.macTrackBar1.TrackerColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(130)))), ((int)(((byte)(198)))));
            this.macTrackBar1.TrackerSize = new System.Drawing.Size(16, 16);
            this.macTrackBar1.TrackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(93)))), ((int)(((byte)(90)))));
            this.macTrackBar1.TrackLineHeight = 3;
            this.macTrackBar1.Value = 10;
            // 
            // TaskPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.tabControl1);
            this.Name = "TaskPane";
            this.Size = new System.Drawing.Size(198, 689);
            this.Load += new System.EventHandler(this.TaskPane_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
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
        private System.Windows.Forms.Button mergebutton;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label changeInfoText;
        private System.Windows.Forms.Button exitDiffTab;
        private System.Windows.Forms.ComboBox metricsCombobox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label legendValues;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label conflictCountLabel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label conflictNumberLabel;
        private XComponent.SliderBar.MACTrackBar macTrackBar1;
    }
}
