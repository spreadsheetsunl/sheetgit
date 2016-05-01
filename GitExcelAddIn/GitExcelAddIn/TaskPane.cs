using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Range = Microsoft.Office.Interop.Excel.Range;

namespace GitExcelAddIn
{
    public partial class TaskPane : UserControl
    {

        private int _previousValue = 5;
        private IKeyboardMouseEvents _m_GlobalHook;

        public TaskPane()
        {
            InitializeComponent();
            string appPath = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            System.Diagnostics.Debug.WriteLine(Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName));
            appPath = appPath.Replace(@"file:\", "");
            Uri uri = new Uri(appPath + @"\PanePage.html");
            webBrowser1.Url = uri;
            webBrowser1.Update();
            //JS calls c#
            webBrowser1.ObjectForScripting = new JavaScript();
            object[] o = new object[1];
            o[0] = "aaaaa";
            webBrowser1.Document.InvokeScript("ConsoleMessage", o);

            webBrowser1.DocumentCompleted += webBrowser1_DocumentCompleted;

            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            Subscribe();
        }

        private void TaskPane_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Globals.ThisAddIn.Application.ActiveWorkbook.SaveCopyAs(@"elcopaay");
            System.Diagnostics.Debug.WriteLine("Saved Workbook");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Opened Workbook");
        }

        // Called after the document has been rendered
        private void webBrowser1_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            // Resize the window
            int height = webBrowser1.Document.Body.ScrollRectangle.Size.Height;
            height = Math.Max(height, webBrowser1.Height);
            /*            webBrowser1.Document.Body.Style = "overflow:hidden";*/

            webBrowser1.Size = new System.Drawing.Size(webBrowser1.Width, height);
            this.Update();
            /*
                        // Re-center the window
                        WindowStartupLocation = WindowStartupLocation.Manual;
                        Left = (SystemParameters.WorkArea.Width - ActualWidth) / 2 + SystemParameters.WorkArea.Left;
                        Top = (SystemParameters.WorkArea.Height - ActualHeight) / 2 + SystemParameters.WorkArea.Top;*/
        }

        /*private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Worksheet s = Globals.ThisAddIn.Application.ActiveSheet;
            //5 - insert column
            //4- Change value that affects formula
            //3 - Change cell formatting
            //2 - change formula
            //1 - add border

            if (trackBar1.Value == 5)
            {
                Range rng = s.Range["A2"];
                rng.EntireColumn.Insert(XlInsertShiftDirection.xlShiftToRight, XlInsertFormatOrigin.xlFormatFromLeftOrAbove);
                System.Diagnostics.Debug.WriteLine("- " + _previousValue + "->5");
            }
            if (trackBar1.Value == 4)
            {
                if (_previousValue > 4)
                {
                    Range rng = s.Range["A2"];
                    rng.EntireColumn.Delete();
                    System.Diagnostics.Debug.WriteLine("- " + _previousValue + "->4");
                }
                if (_previousValue < 4)
                {
                    Range rng = s.Range["C8"];
                    rng.Value2 = 30;
                    System.Diagnostics.Debug.WriteLine("- " + _previousValue + "->4");
                }
            }
            if (trackBar1.Value == 3)
            {
                if (_previousValue > 3)
                {
                    Range rng = s.Range["C8"];
                    rng.Value2 = 3;
                    System.Diagnostics.Debug.WriteLine("- " + _previousValue + "->3");
                }
                if (_previousValue < 3)
                {
                    Range rng = s.Range["F4"];
                    rng.Interior.Color = XlRgbColor.rgbRed;
                    System.Diagnostics.Debug.WriteLine("- " + _previousValue + "->3");
                }
            }
            if (trackBar1.Value == 2)
            {
                if (_previousValue > 2)
                {
                    Range rng = s.Range["F4"];
                    rng.Interior.ColorIndex = 0;
                    System.Diagnostics.Debug.WriteLine("- " + _previousValue + "->2");
                }
                if (_previousValue < 2)
                {
                    Range rng = s.Range["E4"];
                    rng.Formula = "=$C$9+$C$8";
                    System.Diagnostics.Debug.WriteLine("- " + _previousValue + "->2");
                }
            }
            if (trackBar1.Value == 1)
            {
                if (_previousValue > 1)
                {
                    Range rng = s.Range["E4"];
                    rng.Formula = "";
                    System.Diagnostics.Debug.WriteLine("- " + _previousValue + "->1");
                }
                if (_previousValue < 1)
                {
                    /*                   Range rng = s.Range["E4"];
                                       rng.Borders.LineStyle = XlLineStyle.xlContinuous;
                                       rng.Borders.Color = 0x000000;
                                       System.Diagnostics.Debug.WriteLine("- " + previousValue + "->1");
                }
            }
            if (trackBar1.Value == 0)
            {
                /*              Range rng = s.Range["E4"];
                              rng.Borders.LineStyle = XlLineStyle.xlContinuous;
                              rng.Borders
            }
            _previousValue = trackBar1.Value;
        }
    */
        private void button2_Click(object sender, EventArgs e)
        {
            Thread t = new System.Threading.Thread(delegate ()
            {
                VirtualMouse.MoveMouse(309, 388, 20);
                VirtualMouse.ClickLeftMouseButton();
                VirtualMouse.MoveMouse(92, 47, 15);
                VirtualMouse.ClickLeftMouseButton();
                VirtualMouse.MoveMouse(1479, 116, 20);
                VirtualMouse.ClickLeftMouseButton();
                VirtualMouse.MoveMouse(1492, 193, 10);
                VirtualMouse.ClickLeftMouseButton();
            });
            t.Start();
            /*object[] o = new object[1];
            o[0] = "aaaaa";
            webBrowser1.Document.InvokeScript("ConsoleMessage", o);*/

        }

        /*protected override void OnMouseMove(MouseEventArgs mouseEv)
        {
            this.label3.Text = Cursor.Position.X + "";
            this.label4.Text = Cursor.Position.Y + "";
        }*/

                public void Subscribe()
                {
                    // Note: for the application hook, use the Hook.AppEvents() instead
                    _m_GlobalHook = Hook.GlobalEvents();

                    _m_GlobalHook.MouseMove += GlobalHookMouseMove;
                }

        private void GlobalHookMouseMove(object sender, MouseEventArgs e)
        {
            //this.label3.Text = string.Format("x={0:0000}; y={1:0000}", e.X, e.Y);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("tabPage2");
        }

        private void bitbucketButton_Click(object sender, EventArgs e)
        {
            login();
        }

        protected internal void readyForPin(string code)
        {
            Bitbucket.AuthenticateWithPin(code);
            bitbucketButton.Text = "Log out";
            bitbucketButton.Visible = true;
        }

        private void backLabelTab2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tabControl1.SelectTab("tabPage1");
        }

        private void UserPassTab2Submit_Click(object sender, EventArgs e)
        {
            ThisAddIn.Info["username"] = UsernameTextTab2.Text;
            ThisAddIn.Info["password"] = PasswordTextTab2.Text;
            string json = JsonConvert.SerializeObject(ThisAddIn.Info, Formatting.Indented);
            File.WriteAllText($"{ThisAddIn.SheetGitPath}/Info.json", json);
        }

        private void GitInfoTab2Button_Click(object sender, EventArgs e)
        {
            ThisAddIn.Info["name"] = GitNameTextTab2.Text;
            ThisAddIn.Info["email"] = GitEmailTextTab2.Text;
            string json = JsonConvert.SerializeObject(ThisAddIn.Info, Formatting.Indented);
            File.WriteAllText($"{ThisAddIn.SheetGitPath}/Info.json", json);
        }

        public void UpdateGitGraph(string json)
        {
            object[] o = new object[1];
            o[0] = json;
            webBrowser1.Document.InvokeScript("refreshLog", o);
        }

        public void UpdateInfoLabel(string text)
        {
            infoLabel.Text = text;
        }

        public void login()
        {
            var uri = Bitbucket.StartAuthentication();
            if (uri != "")
            {
                //Process.Start(uri);
                BrowserForm bform = new BrowserForm(uri, this);
                bform.Show();
                //bitbucketPinText.Visible = true;
                //bitbucketButton.Visible = false;
                //bitbucketSubmitButton.Visible = true;
            }
            else
            {
                bitbucketButton.Text = "Log out";
            }
        }

        public void logout()
        {
            Bitbucket.Logout();
            bitbucketButton.Text = "Grant permission";
        }


        /*       public void Unsubscribe()
               {
                   ThisAddIn.MGlobalHook.MouseMove -= GlobalHookMouseMove;

                   //It is recommened to dispose it
                   ThisAddIn.MGlobalHook.Dispose();
               }*/

    }
}
