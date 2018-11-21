using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TogglSharpAPI.Interfaces;

namespace TogglSharpAPI
{
    public abstract class WebClient : IWebClient
    {
        private static string Authentication { get; set; }
        private static string Domain { get; set; }
        private static CookieContainer CookieContainer { get; set; }

        public static bool RateLimit { get; set; } = true;
        private static DateTime _lastRequest;


        public void SetAuthentication(string token)
        {
            var bytes = Encoding.UTF8.GetBytes(String.Format("{0}:api_token", token));
            Authentication = Convert.ToBase64String(bytes);
        }

        public void SetAuthentication(string email, string password)
        {
            var bytes = Encoding.UTF8.GetBytes(String.Format("{0}:{1}", email, password));
            Authentication = Convert.ToBase64String(bytes);
        }

        public string GetAuthentication()
        {
            return Authentication;
        }

        public void SetDomain(string domain)
        {
            Domain = domain;
        }

        public string GetDomain()
        {
            return Domain;
        }

        public string BuildUrl(string urlPart)
        {
            return String.Format("{0}{1}", GetDomain(), urlPart);
        }

        public virtual string AddQueryParameters(string baseUrl, Dictionary<string, string> queryParams)
        {
            string queryAsString = String.Empty;
            foreach (KeyValuePair<string, string> entry in queryParams)
            {
                queryAsString = String.Format("{0}&{1}={2}", queryAsString, entry.Key, entry.Value);
            }
            return String.Format("{0}?{1}", baseUrl, queryAsString.TrimStart('&')).TrimEnd('?');
        }

        public virtual void SetCookieContainer(CookieContainer cookieContainer)
        {
            CookieContainer = cookieContainer;
        }

        public virtual CookieContainer GetCookieContainer()
        {
            return CookieContainer;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual void LimitRequestRate()
        {
            while ((DateTime.UtcNow - _lastRequest).TotalSeconds < 1)
            {
                Thread.Sleep(250);
            };
            _lastRequest = DateTime.UtcNow;
        }

        public abstract HttpWebRequest CreateWebRequest(string url, string method, bool requireAuthentication = true);
        public abstract HttpWebRequest CreateGetRequest(string url, bool requireAuthentication = true);
        public abstract HttpWebRequest CreatePostRequest(string url, bool requireAuthentication = true);
        public abstract void WriteRequestBody(Stream stream, object data);
        public abstract Task<string> GetResponse(Stream stream);
        public abstract Task<JToken> ExecuteRequest(HttpWebRequest request);
    }
}
