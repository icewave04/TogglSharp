using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TogglSharpAPI.Interfaces;
using TogglSharpAPI.JsonConverters;

namespace TogglSharpAPI.V8
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class Workspace
    {
        protected static string _endpoint = "api/v8/workspace";
        public static string Endpoint { get => _endpoint; }
        protected IWebClient WebClient { get; private set; }

        [JsonProperty("id")]
        public int Id { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("at")]
        public string At { get; private set; }

        [JsonProperty("default_hourly_rate")]
        public int DefaultHourlyRate { get; private set; }

        [JsonProperty("default_currency")]
        public string DefaultCurrency { get; private set; }

        [JsonProperty("projects_billable_by_default")]
        public bool ProjectsBillableByDefault { get; private set; }

        [JsonProperty("rounding")]
        public int Rounding { get; private set; }

        [JsonProperty("rounding_minutes")]
        public int RoundingMinutes{ get; private set; }

        [JsonProperty("api_token")]
        public string ApiToken { get; private set; }

        public Workspace()
        {
            WebClient = new TogglWebClient();
        }

        public Workspace(IWebClient webClient)
        {
            WebClient = webClient;
        }
    }
}
