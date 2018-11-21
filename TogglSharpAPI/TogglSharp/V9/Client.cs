using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TogglSharpAPI.Interfaces;

namespace TogglSharpAPI.V9
{
    public class Client
    {
        protected static string _endpoint = "api/v9/me";
        public static string Endpoint { get => _endpoint; }
        protected IWebClient WebClient { get;  set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("wid")]
        public int WId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("at")]
        public string At { get; set; }

        public Client()
        {
            WebClient = new TogglWebClient();
        }

        public Client(IWebClient webClient)
        {
            WebClient = webClient;
        }
    }
}
