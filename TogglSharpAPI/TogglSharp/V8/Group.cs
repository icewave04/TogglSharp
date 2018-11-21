using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TogglSharpAPI.Interfaces;

namespace TogglSharpAPI.V8
{
    public class Group
    {
        protected static string _endpoint = "api/v8/groups";
        public static string Endpoint { get => _endpoint; }
        protected IWebClient WebClient { get; private set; }

        [JsonProperty("id")]
        public int Id { get; private set; }

        [JsonProperty("wid")]
        public int WId { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("at")]
        public string At { get; private set; }

        [JsonProperty("notes")]
        public string Notes { get; private set; }

        public Group()
        {
            WebClient = new TogglWebClient();
        }

        public Group(IWebClient webClient)
        {
            WebClient = webClient;
        }

        public async static Task<Group> Create(string name, int wid)
        {
            return await Create(new TogglWebClient(), name, wid);
        }

        public async static Task<Group> Create(IWebClient webClient, string name, int wid)
        {
            try
            {
                var request = webClient.CreatePostRequest(_endpoint);
                var stream = request.GetRequestStream();
                webClient.WriteRequestBody(stream, new
                {
                    group = new
                    {
                        name,
                        wid
                    }
                });
                JToken response = await webClient.ExecuteRequest(request);
                Group group = response.ToObject<Group>();
                return group;
            }
            catch (Exception e)
            {
                //TODO: Add some kind of error handling? Come'on it's 2018 bro.
                Debugger.Break();
            }
            return null;
        }

        public async Task<Group> Update(string name = null, string notes = null)
        {
            return await Update(new TogglWebClient(), name, notes);
        }

        public async Task<Group> Update(IWebClient webClient, string name = null, string notes = null)
        {
            try
            {
                var request = webClient.CreateWebRequest(String.Format("{0}/{1}", _endpoint, Id), "PUT");
                var stream = request.GetRequestStream();
                webClient.WriteRequestBody(stream, new
                {
                    client = new
                    {
                        name = name ?? Name,
                        notes = notes ?? Notes
                    }
                });
                JToken response = await webClient.ExecuteRequest(request);
                Group client = response.ToObject<Group>();
                return client;
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
    }
}
