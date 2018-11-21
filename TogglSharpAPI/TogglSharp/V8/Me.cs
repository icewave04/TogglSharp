using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TogglSharpAPI;
using TogglSharpAPI.Interfaces;
using TogglSharpAPI.JsonConverters;
using TogglSharpAPI.Responses;

namespace TogglSharpAPI.V8
{
    public class Me
    {
        protected static string _endpoint = "api/v8/me";
        public static string Endpoint {get => _endpoint; }
        protected IWebClient WebClient { get; private set; }

        [JsonProperty("since")]
        public int Since { get; private set; }

        [JsonProperty("id")]
        public int Id { get; private set; }

        [JsonProperty("api_token")]
        public string APIToken { get; private set; }

        [JsonProperty("default_wid")]
        public int DefaultWId { get; private set; }

        [JsonProperty("email")]
        public string Email { get; private set; }

        [JsonProperty("fullname")]
        public string Fullname { get; private set; }

        [JsonProperty("jquery_timeofday_format")]
        public string JQueryTimeOfDayFormat { get; private set; }

        [JsonProperty("jquery_date_format")]
        public string JqueryDateFormat { get; private set; }

        [JsonProperty("timeofday_format")]
        public string TimeOfDayFormat { get; private set; }

        [JsonProperty("date_format")]
        public string DateFormat { get; private set; }

        [JsonProperty("store_start_and_stop_time")]
        public bool StoreStartAndStopTime { get; private set; }

        [JsonProperty("beginning_of_week")]
        public int BeginningOfWeek { get; private set; }

        [JsonProperty("language")]
        public string Language { get; private set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; private set; }

        [JsonProperty("sidebar_piechart")]
        public string SidebarPieChart { get; private set; }

        [JsonProperty("at")]
        public string At { get; private set; }

        [JsonProperty("retention")]
        public int Retention { get; private set; }

        [JsonProperty("record_timeline")]
        public bool RecordTimeline { get; private set; }

        [JsonProperty("render_timeline")]
        public bool RenderTimeline { get; private set; }

        [JsonProperty("timeline_enabled")]
        public bool TimelineEnabled { get; private set; }

        [JsonProperty("timeline_experiment")]
        public bool TimelineExperiment { get; private set; }

        [JsonProperty("new_blog_post")]
        public IReadOnlyDictionary<string, string> NewBlogPost { get; private set; }

        [JsonProperty("projects")]
        public IReadOnlyCollection<Project> Projects { get; private set; }

        [JsonProperty("tags")]
        public IReadOnlyCollection<Tag> Tags { get; private set; }

        [JsonProperty("workspaces")]
        public IReadOnlyCollection<Workspace> Workspaces { get; private set; }

        [JsonProperty("clients")]
        public IReadOnlyCollection<Client> Clients { get; private set; }

        [JsonProperty("time_entries")]
        public IReadOnlyCollection<TimeEntry> TimeEntries { get; private set; }

        public Me()
        {
            WebClient = new TogglWebClient();
        }

        public Me(IWebClient webClient)
        {
            WebClient = webClient;
        }

        public static async Task<Me> Retrieve(bool withRelatedData = false)
        {
            return await Retrieve(new TogglWebClient(), withRelatedData);
        }

        public static async Task<Me> Retrieve(IWebClient webClient, bool withRelatedData = false)
        {
            var url = _endpoint;
            Dictionary<string, string> queryParams = new Dictionary<string, string>();

            if(withRelatedData)
            {
                queryParams.Add("with_related_data", "true");
            }

            url = webClient.AddQueryParameters(url, queryParams);

            var request = webClient.CreateGetRequest(url);
            JToken response = await webClient.ExecuteRequest(request);
            DataResponse<Me> Me = response.ToObject<DataResponse<Me>>();
            return Me.Data;
        }

        public async void CreateSession()
        {
            var url = "/api/v8/sessions";

            var request = WebClient.CreateGetRequest(url);
            JToken jtoken = await WebClient.ExecuteRequest(request);

        }
    }
}
