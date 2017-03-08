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
        private JEnumerable<JToken> _currentChanges;
        private Range _lastChangeRange;
        private int _lastChangeValue;
        private IKeyboardMouseEvents _m_GlobalHook;
        private JProperty _lastChangeProperty;
        private Range _colorToDelete;

        delegate void SetChangesCallback(JObject changes, bool forwardInTime = false);

        public TaskPane()
        {
            _currentChanges = new JEnumerable<JToken>();
            InitializeComponent();
            string appPath = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            System.Diagnostics.Debug.WriteLine(Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName));
            appPath = appPath.Replace(@"file:\", "");
            Uri uri = new Uri(appPath + @"\PanePage.html");
            webBrowser1.Url = uri;
            webBrowser1.Update();
            metricsCombobox.SelectedIndex = 0;
            //JS calls c#
            webBrowser1.ObjectForScripting = new JavaScript();
            object[] o = new object[1];
            o[0] = "aaaaa";
            webBrowser1.Document.InvokeScript("ConsoleMessage", o);

            webBrowser1.DocumentCompleted += webBrowser1_DocumentCompleted;

            macTrackBar1.ValueChanged += changesTrackbar_ValueChanged;

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
            Task.Delay(0);
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

        public void MovetoDiffTab(JObject changes, bool forwardInTimeDiff = false)
        {
            if (macTrackBar1.InvokeRequired)
            {
                SetChangesCallback d = MovetoDiffTab;
                Invoke(d, changes, forwardInTimeDiff);
            }
            else
            {
                ThisAddIn.editMode = false;
                JProperty first = (JProperty)changes.First;
                macTrackBar1.Maximum = changes.Count;
                macTrackBar1.Value = macTrackBar1.Maximum;
                _lastChangeValue = macTrackBar1.Maximum;
                changeInfoText.Text = $"Cell Range: {first.Name}\nDiff. Values:\n{first.Value["Value"] ?? "Nothing"}\n\nDiff. Formulae:\n{first.Value["Formula"] ?? "Nothing"}";
                /*Worksheet sheet = ThisAddIn.ExcelApplication.ActiveSheet;
                sheet.(first.Name);*/
                var sheet = (Worksheet)ThisAddIn.ExcelApplication.ActiveSheet;
                _lastChangeRange = sheet.Range[first.Name, first.Name];
                //_lastChangeRange.Interior.Color = XlRgbColor.rgbYellowGreen;
                _lastChangeProperty = first;
                _colorToDelete = _lastChangeRange;
                _currentChanges = changes.Children();
                tabControl1.SelectTab("tabPage3");
            }
        }

        private void changesTrackbar_ValueChanged(object sender, decimal value)
        {
            if (value != _lastChangeValue) { 
            Debug.WriteLine($"trackbar: {value}");

            int size = macTrackBar1.Location.Y+macTrackBar1.Size.Height / macTrackBar1.Maximum*(macTrackBar1.Maximum-macTrackBar1.Value)-8*(macTrackBar1.Maximum - macTrackBar1.Value)+4;
            changeInfoText.Location = new System.Drawing.Point(changeInfoText.Location.X,decimal.ToInt32(size));

            _colorToDelete.Interior.ColorIndex = 0;
            JProperty c;
            int skipValue = macTrackBar1.Maximum - macTrackBar1.Value;
            if (macTrackBar1.Value < _lastChangeValue)
            {
                if (skipValue == macTrackBar1.Maximum)
                {
                    skipValue--;
                    _lastChangeRange.Interior.ColorIndex = 0;
                }
            }
            else
            {
                if (skipValue != 0)
                {
                    skipValue--;
                    _lastChangeRange.Interior.ColorIndex = 0;
                }
            }
            
            c = (JProperty)_currentChanges.Skip(skipValue).Take(1).Single();
            
            var sheet = (Worksheet)ThisAddIn.ExcelApplication.ActiveSheet;
            ;
            var currentRange = sheet.Range[c.Name, c.Name];
            changeInfoText.Text = $"Cell Range: {c.Name}\nDiff. Values:\n{(_lastChangeProperty.Value["Value"]) ?? (_lastChangeProperty.Value["Value2"] ?? "Nothing")}" +
                                  $"\n\nDiff. Formulae:\n{_lastChangeProperty.Value["Formula"] ?? (_lastChangeProperty.Value["Formula2"] ?? "Nothing")}";
            if (macTrackBar1.Value < _lastChangeValue)
            {
                var last = ((JProperty) _lastChangeProperty.Value.Last);
                var val = (last.Name.EndsWith("2")) ? last.Value : null;
                _lastChangeRange.Value2 = val;
                if (last.Name.Contains("Value"))
                    _lastChangeRange.Interior.Color = XlRgbColor.rgbYellowGreen;
                else _lastChangeRange.Interior.ColorIndex = 39;

            }
            else
            {
                var first = ((JProperty)_lastChangeProperty.Value.First);
                var val = (first.Name.EndsWith("2")) ? null : first.Value;
                _lastChangeRange.Value2 = val;
                if (first.Name.Contains("Value"))
                    _lastChangeRange.Interior.Color = XlRgbColor.rgbYellowGreen;
                else _lastChangeRange.Interior.ColorIndex = 39;
            }

            _lastChangeValue = macTrackBar1.Value;
            _lastChangeProperty = c;
            _colorToDelete = _lastChangeRange;
            _lastChangeRange = currentRange;
            }
        }

        public void logout()
        {
            Bitbucket.Logout();
            bitbucketButton.Text = "Grant permission";
        }

        private void mergebutton_Click(object sender, EventArgs e)
        {
            if (!ThisAddIn.Repo.Commits.Any() || ThisAddIn.Repo.Head.FriendlyName == "master" ||
                ThisAddIn.Repo.Head.FriendlyName == "(no branch)")
            {
                ThisAddIn.sheetGitPane.UpdateInfoLabel("You must be at the tip of a branch to merge.");
            } else if(ThisAddIn.Repo.Index.Conflicts.Any()) ThisAddIn.sheetGitPane.UpdateInfoLabel("Please resolve all conflicts before merging.");
            else ThisAddIn.Merge();
        }

        private void exitDiffTab_Click(object sender, EventArgs e)
        {
            _lastChangeRange.Interior.ColorIndex = 0;
            var sheet = (Worksheet)ThisAddIn.ExcelApplication.ActiveSheet;
            foreach (JProperty ch in _currentChanges)
            {
                var currentRange = sheet.Range[ch.Name, ch.Name];
                var val = (JProperty) ch.Value.First;
                if(!val.Name.EndsWith("2")) currentRange.Value2 = val.Value;

            }
            ThisAddIn.editMode = true;
            tabControl1.SelectTab("tabPage1");
        }

        public void DecreaseConflictLabel(int number)
        {
            var current = int.Parse(conflictNumberLabel.Text);
            UpdateConflictLabel(current-number);

        }

        public void UpdateConflictLabel(int number)
        {
            if(number > 0) { 
            tabControl1.SelectTab("tabPage4");
            this.conflictNumberLabel.Text = $"{number}";
            }
            else tabControl1.SelectTab("tabPage1");

        }

        private void downDiffButton_Click(object sender, EventArgs e)
        {
            macTrackBar1.Value--;
        }

        private void upDiffButton_Click(object sender, EventArgs e)
        {
            macTrackBar1.Value++;
        }


        /*       public void Unsubscribe()
               {
                   ThisAddIn.MGlobalHook.MouseMove -= GlobalHookMouseMove;

                   //It is recommened to dispose it
                   ThisAddIn.MGlobalHook.Dispose();
               }*/

    }
}
