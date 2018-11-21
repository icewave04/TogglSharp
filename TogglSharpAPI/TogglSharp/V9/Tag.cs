using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TogglSharpAPI.V9
{
    public class Tag
    {
        [JsonProperty("at")]
        public string At { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("workspace_id")]
        public int WorkspaceId { get; set; }
    }
}
