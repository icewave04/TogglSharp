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
    public class ProjectUser
    {
        protected static string _endpoint = "api/v8/project_users";
        public static string Endpoint { get => _endpoint; }
        protected IWebClient WebClient { get; private set; }

        [JsonProperty("id")]
        public int Id { get; private set; }

        [JsonProperty("pid")]
        public int PId { get; private set; }

        [JsonProperty("wid")]
        public int WId { get; private set; }

        [JsonProperty("uid")]
        public int UId { get; private set; }

        [JsonProperty("manager")]
        public bool Manager { get; private set; }

        [JsonProperty("rate")]
        public float Rate { get; private set; }

        public async static Task<ProjectUser> Create(int pid, int uid, float rate, bool manager)
        {
            return await Create(new TogglWebClient(), pid, uid, rate, manager);
        }

        public async static Task<List<ProjectUser>> Create(int pid, List<int> uid, float rate, bool manager)
        {
            return await Create(new TogglWebClient(), pid, string.Join(",", uid), rate, manager);
        }

        public async static Task<List<ProjectUser>> Create(IWebClient webClient, int pid, List<int> uid, float rate, bool manager)
        {
            return await Create(webClient, pid, string.Join(",", uid), rate, manager);
        }

        public async static Task<List<ProjectUser>> Create(IWebClient webClient, int pid, string uid, float rate, bool manager)
        {
            try
            {
                var request = webClient.CreatePostRequest(_endpoint);
                var stream = request.GetRequestStream();
                webClient.WriteRequestBody(stream, new
                {
                    project_user = new
                    {
                        pid,
                        uid,
                        rate,
                        manager
                    }
                });
                JToken response = await webClient.ExecuteRequest(request);
                DataResponse<List<ProjectUser>> projectUsers = response.ToObject<DataResponse<List<ProjectUser>>>();
                return projectUsers.Data;
            }
            catch (Exception e)
            {
                //TODO: Add some kind of error handling? Come'on it's 2018 bro.
                Debugger.Break();
            }
            return null;
        }

        public async static Task<ProjectUser> Create(IWebClient webClient, int pid, int uid, float rate, bool manager)
        {
            try
            {
                var request = webClient.CreatePostRequest(_endpoint);
                var stream = request.GetRequestStream();
                webClient.WriteRequestBody(stream, new
                {
                    project_user = new
                    {
                        pid,
                        uid,
                        rate,
                        manager
                    }
                });
                JToken response = await webClient.ExecuteRequest(request);
                DataResponse<ProjectUser> projectUser = response.ToObject<DataResponse<ProjectUser>>();
                return projectUser.Data;
            }
            catch (Exception e)
            {
                //TODO: Add some kind of error handling? Come'on it's 2018 bro.
                Debugger.Break();
            }
            return null;
        }

        public async Task<ProjectUser> Update(bool manager, float rate, string fields = null)
        {
            return await Update(new TogglWebClient(), manager, rate, fields);
        }

        public async Task<ProjectUser> Update(IWebClient webClient, bool manager, float rate, string fields = null)
        {
            try
            {
                var request = webClient.CreateWebRequest(String.Format("{0}/{1}", _endpoint, Id), "PUT");
                var stream = request.GetRequestStream();
                webClient.WriteRequestBody(stream, new
                {
                    client = new
                    {
                        manager,
                        rate,
                        fields
                    }
                });
                JToken response = await webClient.ExecuteRequest(request);
                DataResponse<ProjectUser> projectUser = response.ToObject<DataResponse<ProjectUser>>();
                return projectUser.Data;
            }
            catch (Exception e)
            {
                //TODO: Add some kind of error handling? Come'on it's 2018 bro.
                Debugger.Break();
            }
            return null;
        }

        public static async Task<List<ProjectUser>> Update(bool manager, float rate, List<int> projectUserIds, string fields = null)
        {
            return await Update(new TogglWebClient(), manager, rate, projectUserIds, fields);
        }

        public static async Task<List<ProjectUser>> Update(IWebClient webClient, bool manager, float rate, List<int> projectUserIds, string fields = null)
        {
            try
            {
                var request = webClient.CreateWebRequest(String.Format("{0}/{1}", _endpoint, string.Join(",", projectUserIds)), "PUT");
                var stream = request.GetRequestStream();
                webClient.WriteRequestBody(stream, new
                {
                    client = new
                    {
                        manager,
                        rate,
                        fields
                    }
                });
                JToken response = await webClient.ExecuteRequest(request);
                DataResponse<List<ProjectUser>> projectUsers = response.ToObject<DataResponse<List<ProjectUser>>>();
                return projectUsers.Data;
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

        public static async Task<bool> Delete(List<int> projectUserIds)
        {
            return await Delete(new TogglWebClient(), projectUserIds);
        }

        public static async Task<bool> Delete(IWebClient webClient, List<int> projectUserIds)
        {
            var url = String.Format("{0}/{1}", _endpoint, string.Join(",", projectUserIds));

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
    }
}
