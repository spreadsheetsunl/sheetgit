using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LibGit2Sharp;
using Microsoft.Office.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace GitExcelAddIn
{
    class Bitbucket
    {
        private string authCode;
        private static Commit lastCommit;

        public Bitbucket()
        {
            authCode = "";
        }

        public static string StartAuthentication()
        {
            RestClient client;

            if (ThisAddIn.Info["refresh_token"] == null)
            {
                client = new RestClient("https://bitbucket.org/site/oauth2/authorize");
                var request = new RestRequest(Method.POST);
                request.AddQueryParameter("client_id", ThisAddIn.Info["consumerKey"].ToString());
                request.AddQueryParameter("response_type", "code");
                ThisAddIn.OnlineFunctionsEnabled = true;
                return client.BuildUri(request).ToString();
            }
            else
            {
                client = new RestClient("https://bitbucket.org/site/oauth2/access_token");
                client.Authenticator = new HttpBasicAuthenticator(ThisAddIn.Info["consumerKey"].ToString(),
                    ThisAddIn.Info["consumerSecretKey"].ToString());
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("grant_type", "refresh_token");
                request.AddParameter("refresh_token", ThisAddIn.Info["refresh_token"].ToString());
                IRestResponse response = client.Execute(request);
                var content = JObject.Parse(response.Content);
                ThisAddIn.Info["access_token"] = content["access_token"];
                string json = JsonConvert.SerializeObject(ThisAddIn.Info, Formatting.Indented);
                File.WriteAllText($"{ThisAddIn.SheetGitPath}/Info.json", json);

                return "";
            }

        }

        public static void AuthenticateWithPin(string pin)
        {
            var client = new RestClient("https://bitbucket.org/site/oauth2/access_token");
            client.Authenticator = new HttpBasicAuthenticator(ThisAddIn.Info["consumerKey"].ToString(),
                ThisAddIn.Info["consumerSecretKey"].ToString());
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("code", pin);
            IRestResponse response = client.Execute(request);
            var content = JObject.Parse(response.Content);
            System.Diagnostics.Debug.WriteLine($"access:{content["access_token"]}^^refresh:{content["refresh_token"]}");
            ThisAddIn.Info["access_token"] = content["access_token"];
            ThisAddIn.Info["refresh_token"] = content["refresh_token"];
            string json = JsonConvert.SerializeObject(ThisAddIn.Info, Formatting.Indented);
            File.WriteAllText($"{ThisAddIn.SheetGitPath}/Info.json", json);
        }

        public void GetRepositories()
        {

        }

        private static Tuple<RestClient, RestRequest> PrepareRequest()
        {
            var client = new RestClient("https://api.bitbucket.org/2.0");
            client.Authenticator = new HttpBasicAuthenticator(ThisAddIn.Info["consumerKey"].ToString(),
                ThisAddIn.Info["consumerSecretKey"].ToString());
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", $"Bearer {ThisAddIn.Info["access_token"]}");
            return new Tuple<RestClient, RestRequest>(client, request);
        }

        public static string RepoExists(string name)
        {
            Tuple<RestClient, RestRequest> t = PrepareRequest();
            t.Item2.Resource = "{name}";
            t.Item2.AddUrlSegment("name", "repositories/Raikon");
            var content = ExecuteRequest(t);
            var repoExists = "";
            foreach (JObject value in content["values"].Children<JObject>())
            {
                if (value["name"].ToString() == name + "-SheetGit")
                {
                    repoExists = value["links"]["clone"][0]["href"].ToString();
                }
            }
            return repoExists;
        }

        public static string CreateRepo(string name)
        {
            Tuple<RestClient, RestRequest> t = PrepareRequest();
            t.Item2.Method = Method.POST;
            t.Item2.Resource = "{name}/{slug}";
            t.Item2.AddParameter("is_private", "true");
            t.Item2.AddUrlSegment("name", "repositories/Raikon");
            t.Item2.AddUrlSegment("slug", GenerateSlug("SheetGit " + name));
            var content = ExecuteRequest(t);
            return content["links"]["clone"][0]["href"].ToString();
        }

        private static string GenerateSlug(string phrase)
        {
            string str = RemoveAccent(phrase).ToLower().Split('.')[0];
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            str = Regex.Replace(str, @"\s+", " ").Trim();
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-");
            return str;
        }

        private static string RemoveAccent(string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        private static JObject ExecuteRequest(Tuple<RestClient, RestRequest> t)
        {
            for (var i = 0; i <= 1; i++)
            {
                IRestResponse response = t.Item1.Execute(t.Item2);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return JObject.Parse(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    if(i == 1) ThisAddIn.sheetGitPane.UpdateInfoLabel("Unauthorized. Please grant permission once more.");
                    if (ThisAddIn.OnlineFunctionsEnabled) Bitbucket.StartAuthentication();
                    else ThisAddIn.sheetGitPane.UpdateInfoLabel("Please grant permission in Settings if you wish to enable the online features.");
                }
            }
            return null;
        }

        public static void Logout()
        {
            ThisAddIn.Info.Remove("access_token");
            ThisAddIn.Info.Remove("refresh_token");
            ThisAddIn.OnlineFunctionsEnabled = false;
            string json = JsonConvert.SerializeObject(ThisAddIn.Info, Formatting.Indented);
            File.WriteAllText($"{ThisAddIn.SheetGitPath}/Info.json", json);
        }

        public static string GetGitLog()
        {
            var RFC2822Format = "ddd dd MMM HH:mm:ss yyyy K";

            JArray allcommits = new JArray();

            ICommitLog commits;

            if (lastCommit != null)
            {
                var filter = new CommitFilter { IncludeReachableFrom = lastCommit, SortBy = CommitSortStrategies.Time };
                commits = ThisAddIn.Repo.Commits.QueryBy(filter);
            }
            else
            {
                commits = ThisAddIn.Repo.Commits;
            }

            foreach (Commit c in commits)
            {
                dynamic jsonObject = new JObject();
                jsonObject.id = c.Id.ToString();
                jsonObject.author = c.Author.Name;
                jsonObject.email = c.Author.Email;
                jsonObject.date = c.Author.When.ToString(RFC2822Format, CultureInfo.InvariantCulture);
                jsonObject.message = c.Message;
                //jsonObject.notes = new JArray(c.Notes.GetEnumerator());
                lastCommit = c;
                allcommits.Add(jsonObject);
            }
            return JsonConvert.SerializeObject(allcommits, Formatting.Indented);
        }

    }
}
