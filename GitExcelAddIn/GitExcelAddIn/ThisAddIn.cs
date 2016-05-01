using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using Gma.System.MouseKeyHook;
using LibGit2Sharp;
using LibGit2Sharp.Handlers;
using Microsoft.Office.Tools;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;
using Newtonsoft.Json.Linq;

namespace GitExcelAddIn
{
    public partial class ThisAddIn
    {
        public static Repository Repo;
        public static string FilePath;
        public static JObject Info;
        public static string SheetGitPath;
        public static bool OnlineFunctionsEnabled;
        public static TaskPane sheetGitPane;


        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Plugin initiated");
            this.Application.WorkbookOpen += new Excel.AppEvents_WorkbookOpenEventHandler(Application_WorkbookStart);
            ((Excel.AppEvents_Event)this.Application).NewWorkbook += new Excel.AppEvents_NewWorkbookEventHandler(Application_WorkbookStart);

            SheetGitPath = Utils.GenerateFilePath();

            var infoText = File.ReadAllText($"{SheetGitPath}/info.json");

            Info = JObject.Parse(infoText);

            sheetGitPane = new TaskPane();
            var customPane = this.CustomTaskPanes.Add(sheetGitPane, "SheetGit");
            customPane.Visible = true;

        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Plugin Shutdown");
        }

        void Application_WorkbookStart(Excel.Workbook wb)
        {
            System.Diagnostics.Debug.WriteLine("Workbook Opened or Created");
            this.Application.SheetChange += new Excel.AppEvents_SheetChangeEventHandler(Application_SheetChange);
            this.Application.SheetCalculate += new Excel.AppEvents_SheetCalculateEventHandler(Application_SheetCalculate);
            FilePath = Utils.GenerateFilePath(wb.Name);
            System.Diagnostics.Debug.WriteLine(FilePath);
            System.Diagnostics.Debug.WriteLine(wb.Path + "\\" + wb.Name);
            OnlineFunctionsEnabled = Info["refresh_token"] != null;

        }

        void Application_SheetChange(object sh, Excel.Range target)
        {
            System.Diagnostics.Debug.WriteLine("Worksheet Changed");
            Excel.Worksheet sheet = (Excel.Worksheet)sh;
            var wb = this.Application.ActiveWorkbook;
            if (!string.IsNullOrEmpty(wb.Path)) //ficheiro já foi salvo
            {
                this.Application.ActiveWorkbook.Save();
                InitRepo();
                wb.SaveCopyAs(FilePath + "\\" + wb.Name);
                RepositoryStatus status = Repo.RetrieveStatus();

                if (status.IsDirty)
                {
                    if (Info["name"] == null || Info["email"] == null)
                    {
                        sheetGitPane.UpdateInfoLabel("Please fill in User Information in Settings");
                    }
                    else
                    {
                        Signature author = new Signature(Info["name"].ToString(), Info["email"].ToString(), DateTime.Now);
                        Signature committer = author;
                        Repo.Stage(wb.Name);
                        var commit = Repo.Commit("message" + Repo.Commits.Count(), author, committer);
                        /*Repo.Notes.Add(commit.Id, "One", commit.Author, commit.Committer, "");
                        Repo.Notes.Add(commit.Id, "Two", commit.Author, commit.Committer, "");*/
                        sheetGitPane.UpdateGitGraph(Bitbucket.GetGitLog());
                        if (Repo.Commits.Count() == 1) Repo.CreateBranch(Info["name"].ToString());
                        if (Repo.Network.Remotes.Any())
                        {
                            LibGit2Sharp.PushOptions options = new LibGit2Sharp.PushOptions();
                            options.CredentialsProvider = new CredentialsHandler(
                                (url, usernameFromUrl, types) =>
                                    new UsernamePasswordCredentials()
                                    {
                                        Username = Info["username"].ToString(),
                                        Password = Info["password"].ToString()
                                    });
                            try
                            {
                                Repo.Network.Push(Repo.Head, options);
                            }
                            catch(Exception e)
                            {
                                sheetGitPane.UpdateInfoLabel("Cannot push version online. Please confirm your Bitbucket data in Settings.");
                            }
                            
                        }
                        else if (Info["refreshToken"] != null)
                        {
                            //check if remote repos exist
                            //if not, create
                            //create push options
                        }
                    }
                    
                }
            }

            /*Globals.ThisAddIn.Application.ActiveWorkbook.SaveAs();*/


            /* string changedRange = target.get_Address(
                Excel.XlReferenceStyle.xlA1);

            MessageBox.Show("The value of " + sheet.Name + ":" +
                changedRange + " was changed.");*/
        }

        void Application_SheetCalculate(object sh)
        {
            System.Diagnostics.Debug.WriteLine("Worksheet Recalculated");
        }

        void InitRepo()
        {
            var wb = this.Application.ActiveWorkbook;

            FilePath = Utils.GenerateFilePath(wb.Name);
            if (!string.IsNullOrEmpty(wb.Path) && !Directory.Exists(FilePath)) //se foi salvo mas ainda não tem repo
            {
                Directory.CreateDirectory(FilePath);
                Thread.Sleep(2500);
                Repository.Init(FilePath);
                Repo = new Repository(FilePath);
                //Check if remote exists
                //Repo.CreateBranch("master");
                var url = Bitbucket.CreateRepo(wb.Name);
                Remote remote = Repo.Network.Remotes.Add("origin", url);
                Repo.Branches.Update(Repo.Head,
                    b => b.Remote = remote.Name,
                    b => b.UpstreamBranch = Repo.Head.CanonicalName);
                System.Diagnostics.Debug.WriteLine("Branches: " + Repo.Head.CanonicalName);

            }
            else
            {
                Repo = new Repository(FilePath);
            }
        }

        private int getExcelColumnNumber(string colAdress)
        {
            int[] digits = new int[colAdress.Length];
            for (int i = 0; i < colAdress.Length; ++i)
            {
                digits[i] = Convert.ToInt32(colAdress[i]) - 64;
            }
            int mul = 1; int res = 0;
            for (int pos = digits.Length - 1; pos >= 0; --pos)
            {
                res += digits[pos] * mul;
                mul *= 26;
            }
            return res;
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion
    }
}
