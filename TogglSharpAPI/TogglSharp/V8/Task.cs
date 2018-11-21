using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TogglSharpAPI.Interfaces;
using TogglSharpAPI.JsonConverters;

namespace TogglSharpAPI.V8
{
    class Task
    {
        protected static string _endpoint = "api/v8/clients";
        public static string Endpoint { get => _endpoint; }
        protected IWebClient WebClient { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("id")]
        public string Id { get; private set; }

        [JsonProperty("wid")]
        public int WId { get; private set; }

        [JsonProperty("pid")]
        public int PId { get; private set; }

        [JsonProperty("active")]
        public bool Active { get; private set; }

        [JsonProperty("at")]
        public string At { get; private set; }

        [JsonConverter(typeof(NullIntsToNegativeOne))]
        [JsonProperty("estimated_seconds")]
        public int EstimatedSeconds { get; private set; }
    }
}
