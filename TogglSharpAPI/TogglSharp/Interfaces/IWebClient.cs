using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace TogglSharpAPI.Interfaces
{
    public interface IWebClient
    {
        void SetAuthentication(string token);
        void SetAuthentication(string email, string password);
        string GetAuthentication();
        void SetDomain(string domain);
        string GetDomain();
        string BuildUrl(string urlPart);
        HttpWebRequest CreateWebRequest(string url, string method, bool requireAuthentication = true);
        HttpWebRequest CreateGetRequest(string url, bool requireAuthentication = true);
        HttpWebRequest CreatePostRequest(string url, bool requireAuthentication = true);
        void WriteRequestBody(Stream stream, object data);
        Task<string> GetResponse(Stream stream);
        Task<JToken> ExecuteRequest(HttpWebRequest request);
        string AddQueryParameters(string baseUrl, Dictionary<string, string> queryParams);
        void SetCookieContainer(CookieContainer cookieContainer);
        CookieContainer GetCookieContainer();
    }
}
