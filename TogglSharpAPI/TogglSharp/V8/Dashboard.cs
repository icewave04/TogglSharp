using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TogglSharpAPI.Interfaces;
using TogglSharpAPI.JsonConverters;

namespace TogglSharpAPI.V8
{
    public class Dashboard
    {
        protected static string _endpoint = "api/v8/dashboard";
        public static string Endpoint { get => _endpoint; }
        protected IWebClient WebClient { get; private set; }

        [JsonProperty("most_active_user")]
        public List<MostActiveUser> MostActiveUsers { get; private set; }

        [JsonProperty("activity")]
        public List<Activity> Activities { get; private set; }

        public Dashboard()
        {
            WebClient = new TogglWebClient();
        }

        public Dashboard(IWebClient webClient)
        {
            WebClient = webClient;
        }


        public static async Task<Dashboard> Retrieve(int workspaceId)
        {
            return await Retrieve(new TogglWebClient(), workspaceId);
        }

        public static async Task<Dashboard> Retrieve(IWebClient webClient, int workspaceId)
        {
            string url = String.Format("{0}/{1}", _endpoint, workspaceId);

            var request = webClient.CreateGetRequest(url);
            JToken response = await webClient.ExecuteRequest(request);
            Dashboard dashboard = response.ToObject<Dashboard>();
            return dashboard;
        }
    }

    public class MostActiveUser
    {
        [JsonProperty("user_id")]
        public int UserId { get; private set; }

        [JsonConverter(typeof(NullIntsToNegativeOne))]
        [JsonProperty("duration")]
        public int Duration { get; private set; }
    }

    public class Activity
    {
        [JsonProperty("user_id")]
        public int UserId { get; private set; }

        [JsonConverter(typeof(NullIntsToNegativeOne))]
        [JsonProperty("project_id")]
        public int ProjectId { get; private set; }

        [JsonProperty("duration")]
        public int Duration { get; private set; }

        [JsonProperty("description")]
        public string Description { get; private set; }

        [JsonProperty("stop")]
        public string Stop { get; private set; }

        [JsonConverter(typeof(NullIntsToNegativeOne))]
        [JsonProperty("tid")]
        public int TaskId{ get; private set; }
    }

}
