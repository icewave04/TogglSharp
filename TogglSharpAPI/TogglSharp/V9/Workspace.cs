using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TogglSharpAPI.Interfaces;
using TogglSharpAPI.JsonConverters;
using TogglSharpAPI.Responses;

namespace TogglSharpAPI.V9
{

    public class Workspace
    {
        protected static string _endpoint = "api/v9/workspaces";
        public static string Endpoint { get => _endpoint; }
        private IWebClient _webClient;
        protected IWebClient WebClient { get => _webClient ?? new TogglWebClient(); set => _webClient = value; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("profile")]
        public int Profile { get; set; }

        [JsonProperty("premium")]
        public bool Premium { get; set; }

        [JsonProperty("business_ws")]
        public bool Business_ws { get; set; }

        [JsonProperty("admin")]
        public bool Admin { get; set; }

        [JsonProperty("suspended_at")]
        [JsonConverter(typeof(NullIntsToNegativeOne))]
        public int SuspendedAt { get; set; }

        [JsonProperty("server_deleted_at")]
        [JsonConverter(typeof(NullIntsToNegativeOne))]
        public int ServerDeletedAt { get; set; }

        [JsonProperty("default_hourly_rate")]
        public int DefaultHourlyRate { get; set; }

        [JsonProperty("default_currency")]
        public string DefaultCurrency { get; set; }

        [JsonProperty("only_admins_may_create_projects")]
        public bool OnlyAdminsMayCreateProjects { get; set; }

        [JsonProperty("only_admins_see_billable_rates")]
        public bool OnlyAdminsSeeBillableRates { get; set; }

        [JsonProperty("only_admins_see_team_dashboard")]
        public bool OnlyAdminsSeeTeamDashboard { get; set; }

        [JsonProperty("projects_billable_by_default")]
        public bool ProjectsBillableByDefault { get; set; }

        [JsonProperty("rounding")]
        public int Rounding { get; set; }

        [JsonProperty("rounding_minutes")]
        public int RoundingMinutes { get; set; }

        [JsonProperty("api_token")]
        public string APIToken { get; set; }

        [JsonProperty("at")]
        public string At { get; set; }

        [JsonProperty("logo_url")]
        public string LogoUrl { get; set; }

        [JsonProperty("ical_url")]
        public string ICalUrl { get; set; }

        [JsonProperty("ical_enabled")]
        public bool ICalEnabled { get; set; }

        [JsonProperty("csv_upload")]
        public object CSVUpload { get; set; }

        [JsonProperty("subscription")]
        public object Subscription { get; set; }
        
        public async Task<PagedDataResponse<List<Project>>> GetProjects(
            int page = 1, 
            bool active = true, 
            string sort_field = null, 
            bool actual_hours = true, 
            int[] client_ids = null, 
            int[] group_ids = null, 
            Billable billable = Billable.Both, 
            bool force=false)
        {
            // We're just going to take care of billable first
            string billableString = "both";
            if (billable == Billable.False)
                billableString = "false";
            if (billable == Billable.True)
                billableString = "true";

            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            queryParams.Add("page", page.ToString());
            queryParams.Add("active", active.ToString());
            queryParams.Add("sort_field", sort_field ?? String.Empty);
            queryParams.Add("actual_hours", actual_hours.ToString());
            queryParams.Add("client_ids", client_ids != null ? string.Join(",", client_ids) : String.Empty);
            queryParams.Add("group_ids", group_ids != null ? string.Join(",", group_ids) : String.Empty);
            queryParams.Add("billable", billableString);
            queryParams.Add("force", force.ToString());

            var request = WebClient.CreateGetRequest(WebClient.AddQueryParameters(String.Format("{0}/{1}/{2}", _endpoint, Id, "projects"), queryParams));
            JToken response = await WebClient.ExecuteRequest(request);
            PagedDataResponse<List<Project>> result = response.ToObject<PagedDataResponse<List<Project>>>();
            return result;
        }

        public async Task<List<WorkspaceActivity>> GetAllActivity()
        {
            var request = WebClient.CreateGetRequest(String.Format("{0}/{1}/{2}/{3}", _endpoint, Id, "dashboard", "all_activity"));
            JToken response = await WebClient.ExecuteRequest(request);
            List<WorkspaceActivity> result = response.ToObject<List<WorkspaceActivity>>();
            return result;
        }

        public async Task<List<WorkspaceActivity>> GetTopActivity()
        {
            var request = WebClient.CreateGetRequest(String.Format("{0}/{1}/{2}/{3}", _endpoint, Id, "dashboard", "top_activity"));
            JToken response = await WebClient.ExecuteRequest(request);
            List<WorkspaceActivity> result = response.ToObject<List<WorkspaceActivity>>();
            return result;
        }

        public async Task<List<Tag>> GetTags()
        {
            var request = WebClient.CreateGetRequest(String.Format("{0}/{1}/{2}", _endpoint, Id, "tags"));
            JToken response = await WebClient.ExecuteRequest(request);
            List<Tag> result = response.ToObject<List<Tag>>();
            return result;
        }

        public async Task<PagedDataResponse<List<Task>>> GetTasks()
        {
            var request = WebClient.CreateGetRequest(String.Format("{0}/{1}/{2}", _endpoint, Id, "tasks"));
            JToken response = await WebClient.ExecuteRequest(request);
            PagedDataResponse<List<Task>> result = response.ToObject<PagedDataResponse<List<Task>>>();
            return result;
        }

        public async Task<List<WorkspaceUser>> GetWorkspaceUsers()
        {
            var request = WebClient.CreateGetRequest(String.Format("{0}/{1}/{2}", _endpoint, Id, "tasks"));
            JToken response = await WebClient.ExecuteRequest(request);
            List<WorkspaceUser> result = response.ToObject<List<WorkspaceUser>>();
            return result;
        }

        public static async Task<List<Workspace>> Get()
        {
            return await Get(new TogglWebClient());
        }

        public static async Task<List<Workspace>> Get(IWebClient webClient)
        {
            var request = webClient.CreateGetRequest(_endpoint);
            JToken response = await webClient.ExecuteRequest(request);
            List<Workspace> workspaces = response.ToObject<List<Workspace>>();
            return workspaces;
        }

        public static async Task<Workspace> Get(int workspaceId)
        {
            return await Get(new TogglWebClient(), workspaceId);
        }

        public static async Task<Workspace> Get(IWebClient webClient, int workspaceId)
        {
            var request = webClient.CreateGetRequest(String.Format("{0}/{1}", _endpoint, workspaceId.ToString()));
            JToken response = await webClient.ExecuteRequest(request);
            Workspace workspace = response.ToObject<Workspace>();
            return workspace;
        }
    }

    public class WorkspaceActivity
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("duration")]
        public int Duration{ get; set; }

        [JsonProperty("project_id")]
        [JsonConverter(typeof(NullIntsToNegativeOne))]
        public int ProjectId { get; set; }

        [JsonProperty("stop")]
        public string Stop { get; set; }

        [JsonProperty("tid")]
        [JsonConverter(typeof(NullIntsToNegativeOne))]
        public int TId { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }
    }

    public enum Billable
    {
        Both,
        True,
        False
    }
}
