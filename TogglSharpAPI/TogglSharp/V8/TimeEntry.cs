using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TogglSharpAPI.JsonConverters;

namespace TogglSharpAPI.V8
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class TimeEntry
    {
        [JsonProperty("id")]
        public int Id { get; private set; }

        [JsonProperty("wid")]
        public int WId { get; private set; }

        [JsonProperty("billable")]
        public bool Billable { get; private set; }

        [JsonProperty("start")]
        public string Start { get; private set; }

        [JsonProperty("stop")]
        public string Stop { get; private set; }

        [JsonProperty("duration")]
        public int Duration { get; private set; }

        [JsonProperty("description")]
        public string Description { get; private set; }

        [JsonProperty("tags")]
        public IReadOnlyCollection<string> Tags { get; private set; }

        [JsonProperty("at")]
        public string At { get; private set; }
    }
}
