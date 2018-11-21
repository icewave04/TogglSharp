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
    public class Client
    {
        public enum Active
        {
            True,
            False,
            Both
        }

        protected static string _endpoint = "api/v8/clients";
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

        public Client()
        {
            WebClient = new TogglWebClient();
        }

        public Client(IWebClient webClient)
        {
            WebClient = webClient;
        }

        public async static Task<Client> Create(string name, int wid)
        {
            return await Create(new TogglWebClient(), name, wid);
        }

        public async static Task<Client> Create(IWebClient webClient, string name, int wid)
        {
            try
            {
                var request = webClient.CreatePostRequest(_endpoint);
                var stream = request.GetRequestStream();
                webClient.WriteRequestBody(stream, new
                {
                    client = new
                    {
                        name,
                        wid
                    }
                });
                JToken response = await webClient.ExecuteRequest(request);
                DataResponse<Client> client = response.ToObject<DataResponse<Client>>();
                return client.Data;
            }
            catch(Exception e)
            {
                //TODO: Add some kind of error handling? Come'on it's 2018 bro.
                Debugger.Break();
            }
            return null;
        }

        public async static Task<Client> Retrieve(IWebClient webClient, int clientId)
        {
            var url = String.Format("{0}/{1}", _endpoint, clientId);

            var request = webClient.CreateGetRequest(url);
            JToken response = await webClient.ExecuteRequest(request);
            DataResponse<Client> client = response.ToObject<DataResponse<Client>>();
            return client.Data;
        }

        public async static Task<Client> Retrieve(int clientId)
        {
            return await Retrieve(new TogglWebClient(), clientId);
        }

        public async Task<Client> Update(string name = null, string notes = null)
        {
            return await Update(new TogglWebClient(), name, notes);
        }

        public async Task<Client> Update(IWebClient webClient, string name = null, string notes = null)
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
                DataResponse<Client> client = response.ToObject<DataResponse<Client>>();
                return client.Data;
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
            catch(Exception e)
            {
                //TODO: Add some kind of error handling? Come'on it's 2018 bro.
                Debugger.Break();
            }
            return false;
        }

        public async static Task<List<Client>> Retrieve(IWebClient webClient)
        {
            var url = _endpoint;

            var request = webClient.CreateGetRequest(url);
            JToken response = await webClient.ExecuteRequest(request);
            List<Client> clients = response.ToObject<List<Client>>();
            return clients;
        }

        public async static Task<List<Client>> Retrieve()
        {
            return await Retrieve(new TogglWebClient());
        }

        public async Task<List<Project>> RetrieveProjects(IWebClient webClient, Active active = Active.True)
        {
            var url = String.Format("{0}/{1}/projects", _endpoint, Id);
            Dictionary<string, string> queryParams = new Dictionary<string, string>();

            if (active == Active.True)
            {
                queryParams.Add("active", "true");
            }
            else if(active == Active.False)
            {
                queryParams.Add("active", "false");
            }
            else if(active == Active.Both)
            {
                queryParams.Add("active", "both");
            }

            url = webClient.AddQueryParameters(url, queryParams);
            var request = webClient.CreateGetRequest(url);
            JToken response = await webClient.ExecuteRequest(request);
            List<Project> projects = response.ToObject<List<Project>>();
            return projects;
        }

        public async Task<List<Project>> RetrieveProjects(Active active = Active.True)
        {
            return await RetrieveProjects(new TogglWebClient(), active);
        }


    }
}
