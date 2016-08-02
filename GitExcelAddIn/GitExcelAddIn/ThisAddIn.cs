using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using GitExcelAddIn.exceptions;
using Gma.System.MouseKeyHook;
using LibGit2Sharp;
using LibGit2Sharp.Handlers;
using Microsoft.Office.Tools;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GitExcelAddIn
{
    public partial class ThisAddIn
    {
        public static JObject Tree;
        public static Repository Repo;
        public static string FilePath;
        public static JObject Info;
        public static string SheetGitPath;
        public static bool OnlineFunctionsEnabled;
        public static TaskPane sheetGitPane;
        public static Excel.Application ExcelApplication;


        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            Debug.WriteLine("Plugin initiated");
            this.Application.WorkbookOpen += new Excel.AppEvents_WorkbookOpenEventHandler(Application_WorkbookStart);
            ((Excel.AppEvents_Event)this.Application).NewWorkbook += new Excel.AppEvents_NewWorkbookEventHandler(Application_WorkbookStart);
            SheetGitPath = Utils.GenerateFilePath();

            var infoText = File.ReadAllText($"{SheetGitPath}/info.json");
            ExcelApplication = this.Application;
            Info = JObject.Parse(infoText);
            sheetGitPane = new TaskPane();
            var customPane = this.CustomTaskPanes.Add(sheetGitPane, "SheetGit");
            customPane.Visible = true;

        }

        private void ThisAddIn_Shutdown(object sender, EventArgs e)
        {
            Debug.WriteLine("Plugin Shutdown");
        }

        void Application_WorkbookStart(Excel.Workbook wb)
        {
            Debug.WriteLine("Workbook Opened or Created");
            this.Application.SheetChange += new Excel.AppEvents_SheetChangeEventHandler(Application_SheetChange);
            this.Application.SheetCalculate += new Excel.AppEvents_SheetCalculateEventHandler(Application_SheetCalculate);

            FilePath = Utils.GenerateFilePath(wb.Name);
            Debug.WriteLine(FilePath);
            Debug.WriteLine(wb.Path + "\\" + wb.Name);
            OnlineFunctionsEnabled = Info["refresh_token"] != null && (Info["online"] == null || Info["online"].ToObject<bool>());

            if (Repo == null)
            {
                try
                {
                    InitRepo();
                    var head = Repo.Head;
                    string commits;
                    foreach (var branch in Repo.Branches)
                    {
                        Repo.Checkout(branch);
                        commits = File.ReadAllText($"{FilePath}/commits.json");
                        BuildTree(commits);
                    }
                    Repo.Checkout(head);
                    sheetGitPane.UpdateGitGraph(Bitbucket.GetGitLog());
                }
                catch (Exception)
                {
                    Tree = new JObject();
                }
            }

        }

        private static void BuildTree(string commits)
        {
            if (Tree == null) Tree = new JObject();
            var c = JObject.Parse(commits);
            foreach (var obj in c)
            {
                if (obj.Key == "latest")
                {
                    /* if(Tree["latest"] == null || (Tree["latest"] != null && 
                         DateTimeOffset.Parse(Tree["latest"]["timestamp"].ToString()) < DateTimeOffset.Parse(obj.Value["timestamp"].ToString()))) Tree["latest"] = obj.Value;*/

                    var trueId = Repo.Head.Tip.Sha;
                    Tree[trueId] = obj.Value;
                }
                else if (Tree[obj.Key] == null)
                {
                    Tree[obj.Key] = obj.Value;
                }
            }
        }

        void Application_SheetChange(object sh, Excel.Range target)
        {
            try
            {
                Debug.WriteLine("Worksheet Changed");
                Excel.Worksheet sheet = (Excel.Worksheet)sh;
                var wb = this.Application.ActiveWorkbook;
                var lastSheet = (Excel.Worksheet)wb.Sheets[wb.Sheets.Count];


                //
                JObject changes = null;

                if (lastSheet.Visible == Excel.XlSheetVisibility.xlSheetHidden)
                {
                    changes = DetectChanges(wb, target);
                    ThisAddIn.ExcelApplication.DisplayAlerts = false;
                    lastSheet.Delete();
                    ThisAddIn.ExcelApplication.DisplayAlerts = false;

                }
                sheet.Copy(After: sheet);
                lastSheet = (Excel.Worksheet)wb.Sheets[wb.Sheets.Count];
                lastSheet.Visible = Excel.XlSheetVisibility.xlSheetHidden;
                if (changes == null) return;
                //

                if (!String.IsNullOrEmpty(wb.Path) && changes != null) //ficheiro já foi salvo
                {


                    this.Application.ActiveWorkbook.Save();
                    if (Repo == null) InitRepo();
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

                            if (Repo.Commits.Any() && (Repo.Head.FriendlyName == "master" || Repo.Head.FriendlyName == "(no branch)"))
                            {
                                Repo.Checkout(Repo.CreateBranch(Info["name"].ToString() + Repo.Commits.Count()));
                                if (Repo.Network.Remotes.Any())
                                {
                                    Repo.Branches.Update(Repo.Head,
                                        b => b.Remote = Repo.Network.Remotes.FirstOrDefault().Name,
                                        b => b.UpstreamBranch = Repo.Head.CanonicalName);
                                }

                            }
                            Commit(changes);
                            sheetGitPane.UpdateGitGraph(Bitbucket.GetGitLog());
                            if (Repo.Network.Remotes.Any())
                            {
                                PushOptions options = new PushOptions();
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
                                catch (LibGit2SharpException e)
                                {
                                    sheetGitPane.UpdateInfoLabel("Cannot push version online. Please confirm your Bitbucket data in Settings.");
                                }

                            }
                            else if (OnlineFunctionsEnabled)
                            {
                                //check if remote repos exist
                                //if not, create
                                //create push options
                            }
                        }

                    }
                }
            }
            catch (NoChangesExistException e) { Debug.WriteLine("No changes found"); }
        }

        void Application_SheetCalculate(object sh)
        {
            Debug.WriteLine("Worksheet Recalculated");
        }

        void InitRepo()
        {
            var wb = this.Application.ActiveWorkbook;

            FilePath = Utils.GenerateFilePath(wb.Name);
            if (!String.IsNullOrEmpty(wb.Path) && !Directory.Exists(FilePath)) //se foi salvo mas ainda não tem repo
            {
                Directory.CreateDirectory(FilePath);
                Thread.Sleep(2500);
                Repository.Init(FilePath);
                Repo = new Repository(FilePath);
                //Check if remote exists
                //Repo.CreateBranch("master");
                if (ThisAddIn.OnlineFunctionsEnabled) generateOnlineRepo();

            }
            else
            {
                Repo = new Repository(FilePath);
            }
        }

        void generateOnlineRepo()
        {
            var url = Bitbucket.CreateRepo(this.Application.ActiveWorkbook.Name);
            if (url != null)
            {
                Remote remote = Repo.Network.Remotes.Add("origin", url);
                Repo.Branches.Update(Repo.Head,
                    b => b.Remote = remote.Name,
                    b => b.UpstreamBranch = Repo.Head.CanonicalName);
                Debug.WriteLine("Branches: " + Repo.Head.CanonicalName);
            }
            else
            {
                OnlineFunctionsEnabled = false;
            }
        }

        public static void Diff(string id)
        {
            var commitB = Repo.Commits.First(c => c.Sha == id); //Other commit
            //var branchA = Repo.Branches.First(b => b.Commits.Contains(commitA));
            var commitA = Repo.Head.Tip; //Current commit
            var ancestor = Utils.GetMergeBase(commitA, commitB);

            var changes = new JObject[2];
            changes[0] = new JObject();
            changes[1] = new JObject();

            for (var i = 0; i <= 1; i++)
            {
                var current = i == 0 ? commitA : commitB;
                var sha = current.Sha;

                while (current != null && current != ancestor)
                {
                    if (Tree[sha] == null) sha = "latest";

                    foreach (JProperty p in Tree[sha]["changes"])
                    {
                        if (changes[i][p.Name] == null)
                        {
                            changes[i][p.Name] = p.Value;
                        }
                    }
                    current = current.Parents.FirstOrDefault();
                    if (current != null) sha = current.Sha;
                }
            }
            JObject realChanges = new JObject();
            foreach (var p in changes[0].Properties())
            {
                if (changes[1][p.Name] == null) //Only first
                {
                    realChanges[p.Name] = p.Value;
                }
                else if (changes[1][p.Name] != p.Value) //On two
                {
                    realChanges[p.Name] = p.Value;
                    realChanges[p.Name]["Value2"] = changes[1][p.Name]["Value"];
                }
            }
            foreach (var p in changes[1].Properties())
            {
                if (changes[0][p.Name] == null) //Only second
                {
                    realChanges[p.Name] = new JProperty("Value2", p.Value["Value"]);
                }
            }
            ThisAddIn.sheetGitPane.MovetoDiffTab(realChanges);
        }

        static Signature GenerateSignature()
        {
            Signature author = new Signature(Info["name"].ToString(), Info["email"].ToString(), DateTime.Now);
            return author;
        }

        static void Commit(JObject changes)
        {
            Signature author = GenerateSignature();
            Signature committer = author;

            var id = "latest";

            /*var toadd = new JObject();
            toadd.Add("branch",Repo.Head.FriendlyName);
            toadd.Add("head", "true");
            var authors = new JObject();
            authors.Add("email",commit.Author.Email);
            authors.Add("name",commit.Author.Name);
            toadd.Add("author", authors);

            Tree.Add(id,toadd);*/

            if (Tree[id] != null)
            {
                var trueId =
                    Repo.Commits.QueryBy(new CommitFilter
                    {
                        IncludeReachableFrom = Repo.Refs,
                        SortBy = CommitSortStrategies.Time
                    }).First().Sha;
                Tree[trueId] = Tree[id].DeepClone();
            }

            //if (Tree["head"] == null) Tree["head"] = ""; //Guarantee it's first
            Tree[id] = new JObject();
            Tree[id]["author"] = new JObject();
            Tree[id]["author"]["email"] = author.Email;
            Tree[id]["author"]["name"] = author.Name;
            Tree[id]["branch"] = Repo.Head.FriendlyName;
            string parent = null;
            /*if ((string) Tree["head"] != "")
            {
                Tree[id]["parent"] = Tree["head"];
                parent = (string) Tree[id]["parent"];

            }*/
            if (Repo.Commits.Any())
            {
                Tree[id]["parent"] = Repo.Head.Tip.Sha;
                parent = (string)Tree[id]["parent"];
            }
            Tree[id]["timestamp"] = DateTimeOffset.UtcNow;
            Tree[id]["message"] = "message" + (Tree.Count - 1);
            Tree[id]["changes"] = changes;
            var branchChanges = new JObject();
            if (parent != null && Tree[parent] != null && Tree[parent]["branchChanges"] != null && Tree[parent]["branchChanges"].HasValues)
            {
                branchChanges = (JObject)Tree[parent]["branchChanges"];
                Tree[id]["branchChanges"] = CreateBranchChanges(changes, branchChanges);
            }
            else
            {
                Tree[id]["branchChanges"] = changes;
            }



            //Tree["head"] = commit.Id.Sha;
            string json = JsonConvert.SerializeObject(Tree, Formatting.Indented);
            File.WriteAllText(FilePath + @"/commits.json", json);
            //Utils.WaitForFile(FilePath + @"/commits.json");
            Task.Delay(5000);
            Repo.Stage("*");
            var commit = Repo.Commit("message" + Repo.Commits.Count(), author, committer);


        }

        public static void Merge()
        {
            var commitB = Repo.Head.Tip; //ToMerge
            CheckoutOptions opt = new CheckoutOptions();
            opt.CheckoutModifiers = CheckoutModifiers.None;
            Repo.Checkout(Repo.Branches["master"], opt);
            var masterCommits = "";
            //File.ReadAllText($"{FilePath}/commits.json");
            MergeOptions m = new MergeOptions { FileConflictStrategy = CheckoutFileConflictStrategy.Theirs };
            m.CommitOnSuccess = false;
            Repo.Merge(commitB, GenerateSignature(), m);
            bool free = Repo.Index.IsFullyMerged;
            var conflicts = Repo.Index.Conflicts;
            if (!Repo.Index.Conflicts.Any()) //Fast Forward merge
            {
                Dictionary<string, string> iterated = new Dictionary<string, string>();
                foreach (var branch in Repo.Branches)
                {
                    var commits = Repo.Commits.QueryBy(new CommitFilter { IncludeReachableFrom = branch });
                    foreach (var c in commits)
                    {
                        var toChange = c.Sha;
                        if (Tree[c.Sha] == null)
                        {
                            toChange = "latest";
                        }
                        if (!iterated.ContainsKey(c.Sha))
                        {
                            Tree[toChange]["branch"] = branch.FriendlyName;
                            iterated.Add(c.Sha, "");
                        }


                    }
                }
                /*     IEnumerable<Commit> parents = commitB.Parents;

                     if (backTree["latest"] != null)
                     {
                         backTree["latest"]["branch"] = "master";
                     }
                     else
                     {
                         backTree[commitB.Sha]["branch"] = "master";
                     }


                     var commit = parents.FirstOrDefault();
                     parents = commit.Parents;

                     while ((string)backTree[commit.Sha]["branch"] != "master")
                     {

                         backTree[commit.Sha]["branch"] = "master";
                         commit = parents.FirstOrDefault();
                         parents = commit.Parents;
                     }
                     Tree = (JObject)backTree;*/
                Commit(null);
                sheetGitPane.UpdateGitGraph(Bitbucket.GetGitLog(true));

            }
            else
            {
                Tree = null;
                BuildTree(masterCommits);
                var newCommits = File.ReadAllText($"{FilePath}/commits.json"); //Their commits
                var theirCommits = JObject.Parse(newCommits);

                var theirChanges = theirCommits["latest"]["branchChanges"];
                var ourChanges = Tree["latest"]["branchChanges"];

                JObject toChange = new JObject();

                foreach (JProperty change in theirChanges.Children())
                {
                    if (theirChanges.Children().Contains(change.Name))
                    {
                        foreach (JProperty whatHappened in change.Value)
                        {
                            var cur = ourChanges[change.Name][whatHappened.Name];
                            if (cur != change.Value[whatHappened.Name])
                            {
                                toChange[change.Name][whatHappened.Name] = new JArray(change.Value[whatHappened.Name], cur);
                            }
                        }
                    }
                }
                Excel.Worksheet ws = ThisAddIn.ExcelApplication.ActiveSheet;
                foreach (var toCombo in toChange)
                {
                    Excel.Range cell = ws.Range[toCombo.Key, toCombo.Key];

                    var flatList = string.Join(",", ((JArray)toCombo.Value).Cast<string>().ToArray());

                    cell.Validation.Delete();
                    cell.Validation.Add(
                       Excel.XlDVType.xlValidateList,
                       Excel.XlDVAlertStyle.xlValidAlertInformation,
                       Excel.XlFormatConditionOperator.xlBetween,
                       flatList,
                       Type.Missing);

                    cell.Validation.IgnoreBlank = true;
                    cell.Validation.InCellDropdown = true;
                    cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 217, 217, 0));
                    /*var confs = newCommits.Split(new[] { "<<<<<<< HEAD" }, StringSplitOptions.None);
                    foreach (var problem in confs)
                    {
                        var details = problem.Split(new[] { "=======" }, StringSplitOptions.None);


                    }*/
                    Repo.Index.Add("commits.json");
                    Repo.Index.Add(FilePath);
                }
            }

            foreach (var w in Repo.RetrieveStatus().Modified)
            {
                var g = w.FilePath;
            }
            //sheetGitPane.UpdateGitGraph(Bitbucket.GetGitLog(true));

            //Conflict will always happen. ???
            //Se nºao houver novo commit, foi mexer só branches. É necessário acertar todos os commits no grafo
        }

        public static void ReloadWorkbook(string id)
        {
            var filter = new CommitFilter { IncludeReachableFrom = id };
            Commit commit;
            commit = ThisAddIn.Repo.Commits.QueryBy(filter).First();
            /*else
            {
                commit = ThisAddIn.Repo.Commits.Skip(1).Take(1).First(); //First é o head
            }*/
            Branch futureBranch = ThisAddIn.Repo.Branches.FirstOrDefault(b => b.Tip.Sha == id);
            var chkOptions = new CheckoutOptions();
            chkOptions.CheckoutModifiers = CheckoutModifiers.Force;
            while (File.Exists(FilePath + "/.git/index.lock"))
            {
                Task.Delay(1000);
            }
            if (futureBranch == null) ThisAddIn.Repo.Checkout(commit, chkOptions);
            else ThisAddIn.Repo.Checkout(futureBranch, chkOptions);
            var path = ExcelApplication.ActiveWorkbook.FullName;
            var name = ExcelApplication.ActiveWorkbook.Name;
            //Tree["head"] = id;
            //File.WriteAllText(FilePath + @"/commits.json", JsonConvert.SerializeObject(Tree, Formatting.Indented));
            ExcelApplication.ActiveWorkbook.Close(true);
            File.Copy(FilePath + "\\" + name, path, true);
            ExcelApplication.Workbooks.Open(path);

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

        private JObject DetectChanges(Excel.Workbook wb, Excel.Range target)
        {
            JObject result = new JObject();
            Excel.Worksheet sheet2 = wb.Sheets[wb.Sheets.Count];
            var editedRange = target.Address[true, true, Excel.XlReferenceStyle.xlA1, missing, missing].Split(':');
            JObject token = new JObject();
            var reference = target.Address[true, true, Excel.XlReferenceStyle.xlA1, missing, missing];
            Excel.Range mirrorRange;
            if (editedRange.Length > 1)
                mirrorRange = sheet2.Range[editedRange[0], editedRange[1]];
            else
                mirrorRange = sheet2.Range[editedRange[0]];

            if (target.Value != mirrorRange.Value)
            {
                token.Add("Value", target.Value);
            }
            if (target.Formula != mirrorRange.Formula)
            {
                if (target.Formula != "" && target.Formula[0] == '=') token.Add("Formula", target.Formula);
                else if (mirrorRange.Formula != "" && mirrorRange.Formula[0] == '=') token.Add("Formula", "");
            }
            if (target.DisplayFormat.Font.Name != mirrorRange.DisplayFormat.Font.Name)
            {
                token.Add("Font", target.DisplayFormat.Font.Name.ToString());
            }
            if (token.HasValues)
            {
                result.Add(reference, token);
                return result;
            }
            return null;
        }

        private static JObject CreateBranchChanges(JObject changes, JObject branchChanges)
        {


            var result = (JObject)branchChanges.DeepClone();
            if (changes == null) return result;

            foreach (var x in changes)
            {
                if (branchChanges[x.Key] != null) //This value was changed before
                {
                    foreach (JProperty y in x.Value) //the array of the reference
                    {
                        result[x.Key][y.Name] = y.Value;
                    }
                }
                else
                {
                    result[x.Key] = x.Value;
                }
            }
            return result;
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new EventHandler(ThisAddIn_Startup);
            this.Shutdown += new EventHandler(ThisAddIn_Shutdown);
        }

        #endregion
    }
}
