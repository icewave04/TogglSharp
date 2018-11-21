using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TogglSharpAPI.JsonConverters;

namespace TogglSharpAPI.V9
{
    public class TimeEntry
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("workspace_id")]
        public int WorkspaceId { get; set; }

        [JsonProperty("project_id")]
        [JsonConverter(typeof(NullIntsToNegativeOne))]
        public int ProjectId { get; set; }

        [JsonProperty("task_id")]
        [JsonConverter(typeof(NullIntsToNegativeOne))]
        public int TaskId { get; set; }

        [JsonProperty("billable")]
        public bool Billable { get; set; }

        [JsonProperty("start")]
        public string Start { get; set; }

        [JsonProperty("stop")]
        public string Stop { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("tag_ids")]
        public int[] TagIds { get; set; }

        [JsonProperty("duronly")]
        public bool DurOnly { get; set; }

        [JsonProperty("at")]
        public string At { get; set; }

        [JsonProperty("server_deleted_at")]
        [JsonConverter(typeof(NullIntsToNegativeOne))]
        public object ServerDeletedAt { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("uid")]
        public int UId { get; set; }

        [JsonProperty("wid")]
        public int WId { get; set; }

        [JsonProperty("pid")]
        [JsonConverter(typeof(NullIntsToNegativeOne))]
        public int PId { get; set; }
    }

}
