namespace GitExcelAddIn
{
    partial class BrowserForm
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
            this.browserInWindow = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // browserInWindow
            // 
            this.browserInWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserInWindow.Location = new System.Drawing.Point(0, 0);
            this.browserInWindow.MinimumSize = new System.Drawing.Size(20, 20);
            this.browserInWindow.Name = "browserInWindow";
            this.browserInWindow.Size = new System.Drawing.Size(747, 571);
            this.browserInWindow.TabIndex = 0;
            this.browserInWindow.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.browserInWindow_DocumentCompleted);
            // 
            // BrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 571);
            this.Controls.Add(this.browserInWindow);
            this.Name = "BrowserForm";
            this.Text = "SheetGit";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser browserInWindow;
    }
}