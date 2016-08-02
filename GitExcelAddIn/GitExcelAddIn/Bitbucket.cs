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
        private static int lastCommit = 0;

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
            if (content == null) return null;
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
            return content?["links"]["clone"][0]["href"].ToString();
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

        public static string GetGitLog(bool full = false)
        {

            var RFC2822Format = "ddd dd MMM HH:mm:ss yyyy K";

            var masterObject = new JObject();
            var allcommits = new JArray();

            if (full)
            {
                lastCommit = 0;
                allcommits.Add(new JProperty("reset","all"));
            }


            var i = 0;
            Queue<JToken> toIterate = new Queue<JToken>();
            //1 0 -> 2 1

            JProperty latest = null;

            foreach (JProperty c in ThisAddIn.Tree.Children().OrderBy(r => DateTimeOffset.Parse(r.First["timestamp"].ToString())))
            {
                JToken result = c;

                if ( c.Name == "latest")
                {
                    Branch branch = ThisAddIn.Repo.Branches.Select(p => p).First(p => p.FriendlyName == (string) c.First["branch"]);
                    var key = branch.Tip.Sha;
                    var prop = new JProperty("sha",key);
                    latest = new JProperty(key,c.First);
                    i++;
                    continue;
                    /*result.First.Remove();
                    result = result.First;*/
                }
                if (i >= lastCommit)
                {
                    toIterate.Enqueue(result);
                }
                i++;
            }
            if(latest != null) toIterate.Enqueue(latest);
            lastCommit += i - lastCommit;

            //allcommits.Add(new JObject(ThisAddIn.Tree[""]));

            //allcommits.Add(ThisAddIn.Tree.Children().FirstOrDefault());
            foreach (var prop in toIterate)
            {
                allcommits.Add(new JObject(prop));
                //lastCommit++;
            }
            masterObject.Add(new JProperty("values",allcommits));
            string js = JsonConvert.SerializeObject(allcommits, Formatting.Indented);
            return js;
        }
    }
}
