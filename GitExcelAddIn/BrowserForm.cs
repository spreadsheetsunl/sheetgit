using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GitExcelAddIn
{
    public partial class BrowserForm : Form
    {
        TaskPane tPane;

        public BrowserForm(string uri, TaskPane taskPane)
        {
            InitializeComponent();
            tPane = taskPane;
            browserInWindow.Url = new Uri(uri);
            browserInWindow.ScriptErrorsSuppressed = true;
            browserInWindow.Update();
            browserInWindow.DocumentCompleted += browserInWindow_DocumentCompleted;
            browserInWindow.Navigated += browserInWindow_Navigated;
        }

        private void browserInWindow_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void browserInWindow_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (e.Url.AbsoluteUri.StartsWith("http://spreadsheetsunl.github.io/sheetgit/"))
            {
                tPane.readyForPin(e.Url.Query.Split('=')[1]);
                this.Close();
            }
        }
    }
}
