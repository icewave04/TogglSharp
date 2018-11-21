using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TogglSharpAPI.JsonConverters;

namespace TogglSharpAPI.V9
{
    public class WorkspaceUser
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("uid")]
        public int UserId { get; set; }

        [JsonProperty("wid")]
        public int WorkspaceId { get; set; }

        [JsonProperty("admin")]
        public bool Admin { get; set; }

        [JsonProperty("owner")]
        public bool Owner { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("inactive")]
        public bool Inactive { get; set; }

        [JsonProperty("at")]
        public DateTime At { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("group_ids")]
        public int[] GroupIds { get; set; }

        [JsonProperty("rate")]
        [JsonConverter(typeof(NullFloatsToNegativeOne))]
        public float Rate { get; set; }

        [JsonProperty("labour_cost")]
        [JsonConverter(typeof(NullFloatsToNegativeOne))]
        public float LabourCost { get; set; }

        [JsonProperty("invite_url")]
        public string InviteURL { get; set; }

        [JsonProperty("invitation_code")]
        public string InvitationCode { get; set; }

        [JsonProperty("avatar_file_name")]
        public string AvatarFileName { get; set; }
    }
}
