using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TogglSharpAPI.V9
{
    public class Project
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("workspace_id")]
        public int WorkspaceId { get; set; }

        [JsonProperty("client_id")]
        public int ClientId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("is_private")]
        public bool IsPrivate { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("at")]
        public string At { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("server_deleted_at")]
        public object ServerDeletedAt { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("billable")]
        public object Billable { get; set; }

        [JsonProperty("template")]
        public object Template { get; set; }

        [JsonProperty("auto_estimates")]
        public object AutoEstimates { get; set; }

        [JsonProperty("estimated_hours")]
        public object EstimatedHours { get; set; }

        [JsonProperty("rate")]
        public object Rate { get; set; }

        [JsonProperty("currency")]
        public object Currency { get; set; }

        [JsonProperty("actual_hours")]
        public int ActualHours { get; set; }

        [JsonProperty("wid")]
        public int WId { get; set; }

        [JsonProperty("cid")]
        public int CId { get; set; }

    }
}
