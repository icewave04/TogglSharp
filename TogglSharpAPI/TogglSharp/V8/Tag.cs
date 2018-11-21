using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TogglSharpAPI.JsonConverters;

namespace TogglSharpAPI.V8
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class Tag
    {
        [JsonProperty("id")]
        public int Id { get; private set; }

        [JsonProperty("wid")]
        public int WId { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }
    }
}
