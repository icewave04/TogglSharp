using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TogglSharpAPI.Interfaces;
using TogglSharpAPI.JsonConverters;

namespace TogglSharpAPI.V9
{
    public class Me
    {
        protected static string _endpoint = "api/v9/me";
        public static string Endpoint { get => _endpoint; }
        protected IWebClient WebClient { get; set; }

        public static Me Instance { get; private set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("api_token")]
        public string ApiToken { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("fullname")]
        public string Fullname { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("default_workspace_id")]
        public int DefaultWorkspaceId { get; set; }

        [JsonProperty("beginning_of_week")]
        public int BeginningOfWeek { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("openid_email")]
        public object OpenIdEmail { get; set; }

        [JsonProperty("openid_enabled")]
        public bool OpenidEnabled { get; set; }

        [JsonProperty("country_id")]
        public object CountryId { get; set; }

        [JsonProperty("at")]
        public string At { get; set; }

        protected List<Client> _clients;
        public List<Client> Clients { get => _clients ?? new List<Client>(); set => _clients = value; }

        protected List<Project> _projects;
        public List<Project> Projects { get => _projects ?? new List<Project>(); set => _projects = value; }

        protected Location _location;
        public Location Location { get => _location ?? new Location(); set => _location = value; }

        protected List<Tag> _tags;
        public List<Tag> Tags { get => _tags ?? new List<Tag>(); set => _tags = value; }

        protected List<Workspace> _workspace;
        public List<Workspace> Workspaces { get => _workspace ?? new List<Workspace>(); set => _workspace = value; }

        protected List<TimeEntry> _timeEntries;
        public List<TimeEntry> TimeEntries { get => _timeEntries ?? new List<TimeEntry>(); set => _timeEntries = value; }

        protected Dictionary<MeSince, long> _sinceReference = new Dictionary<MeSince, long>();


        private Me()
        {
            WebClient = new TogglWebClient();
        }

        private Me(IWebClient webClient)
        {
            WebClient = webClient;
        }

        public static async Task<Me> Get()
        {
            return await Get(new TogglWebClient());
        }

        public static async Task<Me> Get(IWebClient webClient)
        {
            var request = webClient.CreateGetRequest(_endpoint);
            JToken response = await webClient.ExecuteRequest(request);
            if (response == null)
                return null;

            Instance = response.ToObject<Me>();
            return Instance;
        }

        public async Task<List<Client>> GetClients(string since = null)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            queryParams.Add("since", since ?? String.Empty);
            var request = WebClient.CreateGetRequest(WebClient.AddQueryParameters(String.Format("{0}/{1}", _endpoint, "clients"), queryParams));
            JToken response = await WebClient.ExecuteRequest(request);
            List<Client> clients = response.ToObject<List<Client>>();
            Clients = clients;
            _sinceReference[MeSince.Clients] = TogglWebClient.DateTimeToTimestamp(DateTime.Now);
            return Clients;
        }

        public async Task<List<Client>> GetClients(DateTime since)
        {
            return await GetClients(TogglWebClient.DateTimeToTimestamp(since).ToString());
        }

        public async Task<List<Project>> GetProjects(string since = null)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            queryParams.Add("since", since ?? String.Empty);
            var request = WebClient.CreateGetRequest(WebClient.AddQueryParameters(String.Format("{0}/{1}", _endpoint, "projects"), queryParams));
            JToken response = await WebClient.ExecuteRequest(request);
            List<Project> projects = response.ToObject<List<Project>>();
            Projects = projects;
            _sinceReference[MeSince.Projects] = TogglWebClient.DateTimeToTimestamp(DateTime.Now);
            return Projects;
        }

        public async Task<List<Project>> GetProjects(DateTime since)
        {
            return await GetProjects(TogglWebClient.DateTimeToTimestamp(since).ToString());
        }

        public async Task<Location> GetLocation(string since = null)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            queryParams.Add("since", since ?? String.Empty);
            var request = WebClient.CreateGetRequest(WebClient.AddQueryParameters(String.Format("{0}/{1}", _endpoint, "location"), queryParams));
            JToken response = await WebClient.ExecuteRequest(request);
            Location location = response.ToObject<Location>();
            Location = location;
            _sinceReference[MeSince.Location] = TogglWebClient.DateTimeToTimestamp(DateTime.Now);
            return Location;
        }

        public async Task<Location> GetLocation(DateTime since)
        {
            return await GetLocation(TogglWebClient.DateTimeToTimestamp(since).ToString());
        }

        public async Task<List<Tag>> GetTags(string since = null)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            queryParams.Add("since", since ?? String.Empty);
            var request = WebClient.CreateGetRequest(WebClient.AddQueryParameters(String.Format("{0}/{1}", _endpoint, "tags"), queryParams));
            JToken response = await WebClient.ExecuteRequest(request);
            List<Tag> tags = response.ToObject<List<Tag>>();
            Tags = tags;
            _sinceReference[MeSince.Tags] = TogglWebClient.DateTimeToTimestamp(DateTime.Now);
            return Tags;
        }

        public async Task<List<Tag>> GetTags(DateTime since)
        {
            return await GetTags(TogglWebClient.DateTimeToTimestamp(since).ToString());
        }

        public async Task<List<Workspace>> GetWorkspaces(string since = null)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            queryParams.Add("since", since ?? String.Empty);
            var request = WebClient.CreateGetRequest(WebClient.AddQueryParameters(String.Format("{0}/{1}", _endpoint, "workspaces"), queryParams));
            JToken response = await WebClient.ExecuteRequest(request);
            List<Workspace> workspaces = response.ToObject<List<Workspace>>();
            Workspaces = workspaces;
            _sinceReference[MeSince.Workspaces] = TogglWebClient.DateTimeToTimestamp(DateTime.Now);
            return Workspaces;
        }

        public async Task<List<Workspace>> GetWorkspaces(DateTime since)
        {
            return await GetWorkspaces(TogglWebClient.DateTimeToTimestamp(since).ToString());
        }

        public async Task<List<TimeEntry>> GetTimeEntries(string since = null)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            queryParams.Add("since", since ?? String.Empty);
            var request = WebClient.CreateGetRequest(WebClient.AddQueryParameters(String.Format("{0}/{1}", _endpoint, "time_entries"), queryParams));
            JToken response = await WebClient.ExecuteRequest(request);
            List<TimeEntry> timeEntries = response.ToObject<List<TimeEntry>>();
            TimeEntries = timeEntries;
            _sinceReference[MeSince.TimeEntries] = TogglWebClient.DateTimeToTimestamp(DateTime.Now);
            return TimeEntries;
        }

        public async Task<List<TimeEntry>> GetTimeEntries(DateTime since)
        {
            return await GetTimeEntries(TogglWebClient.DateTimeToTimestamp(since).ToString());
        }

        public string GetTimeSinceLast(MeSince meSince)
        {
            if (_sinceReference.ContainsKey(meSince))
            {
                return _sinceReference[meSince].ToString();
            }
            return null;
        }
    }

    public enum MeSince
    {
        Clients,
        Projects,
        Location,
        Tags,
        Workspaces,
        TimeEntries
    };
}
