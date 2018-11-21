using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;

namespace TogglSharpAPI
{
    public class TogglWebClient : WebClient
    {
        public static TogglAPIError APIError { get; set; }

        public override HttpWebRequest CreateGetRequest(string url, bool requireAuthentication = true)
        {
            return CreateWebRequest(url, "GET", requireAuthentication);
        }

        public override HttpWebRequest CreatePostRequest(string url, bool requireAuthentication = true)
        {
            return CreateWebRequest(url, "POST", requireAuthentication);
        }

        public override HttpWebRequest CreateWebRequest(string url, string method, bool requireAuthentication = true)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(BuildUrl(url));
            request.CookieContainer = GetCookieContainer();
            request.Method = method;
            request.AllowReadStreamBuffering = true;
            if (requireAuthentication)
            {
                request.Headers = new WebHeaderCollection
                {
                    "Authorization: Basic " + GetAuthentication()
                };
            }
            
            return request;

        }

        public override async Task<JToken> ExecuteRequest(HttpWebRequest request)
        {
            LimitRequestRate();
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException we)
            {
                response = (HttpWebResponse)we.Response;

                if (response.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    APIError = new TogglAPIError(response, "Too many requests.");
                }
                else if (response.StatusCode == HttpStatusCode.Gone)
                {
                    APIError = new TogglAPIError(response, "This endpoint is not accessible.");
                }
                else if (response.StatusCode == HttpStatusCode.PaymentRequired)
                {
                    APIError = new TogglAPIError(response, "This feature is only available to upgraded workspaces.");
                }
                else if(response.StatusCode == HttpStatusCode.NotFound)
                {
                    APIError = new TogglAPIError(response, "This endpoint does not exist.");
                }
                else if (((int)response.StatusCode >= 500) && ((int)response.StatusCode <= 599))
                {
                    APIError = new TogglAPIError(response, "An error has occured, please wait and try again.");
                }
                else if (((int)response.StatusCode >= 400) && ((int)response.StatusCode <= 499))
                {
                    APIError = new TogglAPIError(response, "An error has occured, please wait and try again.");
                }
                throw we;
            }

            var result = await GetResponse(response.GetResponseStream());
            JToken json;
            if (!string.IsNullOrEmpty(result))
            {
                try
                {
                    json = JToken.Parse(result);
                }
                catch(Newtonsoft.Json.JsonException e)
                {
                    json = null;
                }
            }
            else
            {
                json = null;
            }

            return json;
        }

        public override async Task<string> GetResponse(Stream stream)
        {
            string response = String.Empty;
            using (StreamReader reader = new StreamReader(stream))
            {
                response = await reader.ReadToEndAsync();
            }
            return response; 
        }

        public override void WriteRequestBody(Stream stream, object data)
        {
            var serializedData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var raw = Encoding.UTF8.GetBytes(serializedData);
            stream.Write(raw, 0, raw.Length);
            stream.Close();
        }

        public static long DateTimeToTimestamp(DateTime toConvert)
        {
            DateTime unixStart = DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc);
            long epoch = (long)Math.Floor((toConvert.ToUniversalTime() - unixStart).TotalSeconds);
            return epoch;

        }

        /* It appears the ability to create a session has been removed
        public async Task<JToken> CreateSession()
        {
            var url = "api/v8/sessions";

            var request = CreateGetRequest(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            foreach(Cookie cookieValue in response.Cookies)
            {
                if(cookieValue.Name == "toggl_api_session_new")
                {
                    SetAuthentication(cookieValue.Value);
                }
            }
            var result = await GetResponse(response.GetResponseStream());

            JToken json;
            if (!string.IsNullOrEmpty(result))
            {
                json = JToken.Parse(result);
            }
            else
            {
                // Return JToken with error/request information?
                json = JToken.Parse("");
            }

            return json;
        }
        */
    }
}
