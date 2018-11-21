using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TogglSharpAPI.Interfaces;
using TogglSharpAPI.JsonConverters;
using TogglSharpAPI.Responses;

namespace TogglSharpAPI.V8
{
    public class Project
    {
        protected static string _endpoint = "api/v8/projects";
        public static string Endpoint { get => _endpoint; }
        protected IWebClient WebClient { get; private set; }

        [JsonProperty("id")]
        public int Id { get; private set; }

        [JsonProperty("wid")]
        public int WId { get; private set; }

        [JsonProperty("cid")]
        public int CId { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("billable")]
        public bool Billable { get; private set; }

        [JsonProperty("active")]
        public bool Active { get; private set; }

        [JsonProperty("at")]
        public string At { get; private set; }

        [JsonProperty("color")]
        public string Color { get; private set; }

        [JsonProperty("is_private")]
        public bool IsPrivate { get; private set; }

        [JsonProperty("template")]
        public bool Template { get; private set; }

        [JsonProperty("template_id")]
        public int TemplateId { get; private set; }

        [JsonProperty("auto_estimates")]
        public bool AutoEstimates { get; private set; }

        [JsonConverter(typeof(NullIntsToNegativeOne))]
        [JsonProperty("estimated_hours")]
        public int EstimatedHours { get; private set; }

        [JsonProperty("rate")]
        public float Rate { get; private set; }

        struct CreateParameters
        {
            public string Name;
            public int WId;
            public int TemplateId;
            public bool IsPrivate;
            public int CId;

            public object GetParameters()
            {
                if (CId == -1)
                {
                    return new
                    {
                        name = Name,
                        wid = WId,
                        template_id = TemplateId,
                        is_private = IsPrivate
                    };
                }
                return new
                {
                    name = Name,
                    wid = WId,
                    template_id = TemplateId,
                    is_private = IsPrivate,
                    cid = CId
                };
            }
        }

        struct UpdateParameters
        {
            public string Name;
            public bool IsPrivate;
            public int CId;
            public string color;

            public object GetParameters()
            {
                if (CId == -1)
                {
                    return new
                    {
                        name = Name,
                        is_private = IsPrivate,
                        color
                    };
                }
                return new
                {
                    name = Name,
                    is_private = IsPrivate,
                    cid = CId,
                    color
                };
            }
        }

        public async static Task<Project> Create(string name, int wid, int templateId, bool isPrivate = true, int cid = -1)
        {
            return await Create(new TogglWebClient(), name, wid, templateId, isPrivate, cid);
        }



        public async static Task<Project> Create(IWebClient webClient, string name, int wid, int templateId, bool IsPrivate = true, int cid = -1)
        {
            try
            {
                CreateParameters projectParameters;
                projectParameters.Name = name;
                projectParameters.WId = wid;
                projectParameters.IsPrivate = IsPrivate;
                projectParameters.TemplateId = templateId;
                projectParameters.CId = cid;

                var request = webClient.CreatePostRequest(_endpoint);
                var stream = request.GetRequestStream();
                webClient.WriteRequestBody(stream, new
                {
                    project = projectParameters.GetParameters()
                });
                JToken response = await webClient.ExecuteRequest(request);
                DataResponse<Project> project = response.ToObject<DataResponse<Project>>();
                return project.Data;
            }
            catch (Exception e)
            {
                //TODO: Add some kind of error handling? Come'on it's 2018 bro.
                Debugger.Break();
            }
            return null;
        }

        public async static Task<Project> Retrieve(IWebClient webClient, int projectId)
        {
            var url = String.Format("{0}/{1}", _endpoint, projectId);

            var request = webClient.CreateGetRequest(url);
            JToken response = await webClient.ExecuteRequest(request);
            DataResponse<Project> project = response.ToObject<DataResponse<Project>>();
            return project.Data;
        }

        public async static Task<Project> Retrieve(int clientId)
        {
            return await Retrieve(new TogglWebClient(), clientId);
        }

        public async Task<Project> Update(string name = null, bool IsPrivate = true, int cid = -1, string color = null)
        {
            return await Update(new TogglWebClient(), name, IsPrivate, cid, color);
        }

        public async Task<Project> Update(IWebClient webClient, string name = null, bool IsPrivate = true, int cid = -1, string color = null)
        {
            try
            {
                UpdateParameters updateParameters;
                updateParameters.color = color ?? Color;
                updateParameters.Name = name ?? Name;
                updateParameters.CId = cid;
                updateParameters.IsPrivate = IsPrivate;

                var request = webClient.CreateWebRequest(String.Format("{0}/{1}", _endpoint, Id), "PUT");
                var stream = request.GetRequestStream();
                webClient.WriteRequestBody(stream, new
                {
                    client = updateParameters.GetParameters()
                });
                JToken response = await webClient.ExecuteRequest(request);
                DataResponse<Project> project = response.ToObject<DataResponse<Project>>();
                return project.Data;
            }
            catch (Exception e)
            {
                //TODO: Add some kind of error handling? Come'on it's 2018 bro.
                Debugger.Break();
            }
            return null;
        }

        public async Task<bool> Delete()
        {
            return await Delete(new TogglWebClient());
        }

        public async Task<bool> Delete(IWebClient webClient)
        {
            var url = String.Format("{0}/{1}", _endpoint, Id);

            var request = webClient.CreateWebRequest(url, "DELETE");
            try
            {
                JToken response = await webClient.ExecuteRequest(request);
                return true;
            }
            catch (Exception e)
            {
                //TODO: Add some kind of error handling? Come'on it's 2018 bro.
                Debugger.Break();
            }
            return false;
        }

        public async Task<List<ProjectUser>> GetProjectUsers()
        {
            var url = String.Format("{0}/{1}/project_users", _endpoint, Id);

            var request = WebClient.CreateGetRequest(url);
            JToken response = await WebClient.ExecuteRequest(request);
            List<ProjectUser> projects = response.ToObject<List<ProjectUser>>();
            return projects;
        }
    }
}
