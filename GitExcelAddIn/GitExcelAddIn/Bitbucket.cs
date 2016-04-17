using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using SharpBucket.V2.Pocos;

namespace GitExcelAddIn
{
    class Bitbucket
    {
        private string authCode;

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
                File.WriteAllText($"{ThisAddIn.CodeLocation}/Info.json", json);

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
            ThisAddIn.Info["access_token"] =  content["access_token"];
            ThisAddIn.Info["refresh_token"] = content["refresh_token"];
            string json = JsonConvert.SerializeObject(ThisAddIn.Info, Formatting.Indented);
            File.WriteAllText($"{ThisAddIn.CodeLocation}/Info.json", json);
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
            return new Tuple<RestClient, RestRequest>(client,request);
        }

        public static string RepoExists(string name)
        {
            Tuple<RestClient, RestRequest> t = PrepareRequest();
            t.Item2.Resource = "{name}";
            t.Item2.AddUrlSegment("name", "repositories/Raikon");
            IRestResponse response = t.Item1.Execute(t.Item2);
            var content = JObject.Parse(response.Content);
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
            t.Item2.AddUrlSegment("slug", GenerateSlug("SheetGit "+name));
            IRestResponse response = t.Item1.Execute(t.Item2);
            var content = JArray.Parse(response.Content);
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

    }
}
