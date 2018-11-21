using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TogglSharpAPI.V8;

namespace TogglSharpAPI
{
    class ProgramV8
    {
        public static string APIToken = "";
        public static TogglWebClient TogglWebClient = new TogglWebClient();
        public static Me MeV8;
        // V8

        public static async void GetDashboard()
        {
            var dashboard = await TogglSharpAPI.V8.Dashboard.Retrieve(MeV8.DefaultWId);
            Debugger.Break();
        }

        public static async void GetStandardClientV8()
        {
            TogglWebClient.SetAuthentication(APIToken);
            TogglWebClient.SetDomain("https://www.toggl.com/");
            MeV8 = await TogglSharpAPI.V8.Me.Retrieve(true);
            var x = MeV8;
            //Debugger.Break();
        }

        public static async void ClientTest()
        {
            string clientName = String.Format("Very Big Company {0} {1}", DateTime.Now.ToString("h:mm:ss tt"), DateTime.Now.ToString("dd/MM/yyyy"));
            string newClientName = String.Format("A really Very Big Company {0} {1}", DateTime.Now.ToString("h:mm:ss tt"), DateTime.Now.ToString("dd/MM/yyyy"));
            string notes = "Notes on a really very big company.";

            TogglSharpAPI.V8.Client client = await TogglSharpAPI.V8.Client.Create(clientName, MeV8.DefaultWId);
            TogglSharpAPI.V8.Client clientTwo = await TogglSharpAPI.V8.Client.Retrieve(client.Id);
            if (client.Id != clientTwo.Id)
            {
                throw new Exception("Client ID's don't match.");
            }

            client = await client.Update(newClientName, notes);
            List<TogglSharpAPI.V8.Project> projects = await client.RetrieveProjects();
            if (projects == null)
            {
                Debug.WriteLine("Projects is NULL but it could be that there are no projects.");
            }
            List<TogglSharpAPI.V8.Client> myClients = await TogglSharpAPI.V8.Client.Retrieve();
            if (myClients == null)
            {
                throw new Exception("No clients found, expected to find at the least the client created above.");
            }
            Debugger.Break();
            await client.Delete();
            Debugger.Break();
        }

        public static async void ListClientProjects()
        {
            if (MeV8.Clients.Count > 0)
            {
                TogglSharpAPI.V8.Client client = MeV8.Clients.First();
                var projects = await client.RetrieveProjects(TogglSharpAPI.V8.Client.Active.Both);
                Debugger.Break();
            }
            else
            {
                throw new Exception("No clients to retrieve");
            }
        }

        public static async void ListClients()
        {
            var clients = await TogglSharpAPI.V8.Client.Retrieve();
            Debugger.Break();
        }

        public static async void DeleteClient()
        {
            string clientName = String.Format("TogglSharpAPI {0} {1}", DateTime.Now.ToString("h:mm:ss tt"), DateTime.Now.ToString("dd/MM/yyyy"));
            TogglSharpAPI.V8.Client client = await TogglSharpAPI.V8.Client.Create(clientName, MeV8.DefaultWId);
            Debugger.Break();
            var deleted = await client.Delete();
            Debugger.Break();
        }

        public static async void UpdateClient()
        {
            if (MeV8.Clients.Count > 0)
            {
                TogglSharpAPI.V8.Client client = MeV8.Clients.First();
                string clientName = String.Format("TogglSharpAPI {0} {1}", DateTime.Now.ToString("h:mm:ss tt"), DateTime.Now.ToString("dd/MM/yyyy"));
                var clientTwo = await client.Update(clientName);
                Debugger.Break();
            }
            else
            {
                throw new Exception("No clients to retrieve");
            }
        }

        public static async void RetrieveClient()
        {
            if (MeV8.Clients.Count > 0)
            {
                TogglSharpAPI.V8.Client client = MeV8.Clients.First();
                var clientTwo = await TogglSharpAPI.V8.Client.Retrieve(client.Id);
                Debugger.Break();
            }
            else
            {
                throw new Exception("No clients to retrieve");
            }
        }

        public static async void CreateClient()
        {
            string clientName = String.Format("TogglSharpAPI {0} {1}", DateTime.Now.ToString("h:mm:ss tt"), DateTime.Now.ToString("dd/MM/yyyy"));
            TogglSharpAPI.V8.Client client = await TogglSharpAPI.V8.Client.Create(clientName, MeV8.DefaultWId);
            Debugger.Break();
        }

        public static async void RunThree()
        {
            TogglWebClient TogglWebClient = new TogglWebClient();
            TogglWebClient.SetAuthentication(APIToken);
            TogglWebClient.SetDomain("https://www.toggl.com/");
            //var me = await TogglWebClient.CreateSession();
            var secondMe = await TogglSharpAPI.V8.Me.Retrieve(TogglWebClient, true);
            Debugger.Break();
        }

        public static async void RunTwo()
        {
            TogglWebClient TogglWebClient = new TogglWebClient();
            TogglWebClient.SetAuthentication(APIToken);
            TogglWebClient.SetDomain("https://www.toggl.com/");

            TogglSharpAPI.V8.Me me = await TogglSharpAPI.V8.Me.Retrieve(true);
            Debugger.Break();
        }

        public static async void Run()
        {
            TogglWebClient TogglWebClient = new TogglWebClient();
            TogglWebClient.SetAuthentication(APIToken);
            TogglWebClient.SetDomain("https://www.toggl.com/");


            var request = TogglWebClient.CreateGetRequest("api/v8/me");
            JToken response = await TogglWebClient.ExecuteRequest(request);
            Debug.WriteLine(response);
            TogglSharpAPI.V8.Me Me = response.ToObject<TogglSharpAPI.V8.Me>();//JsonConvert.DeserializeObject<Me>(response);

            Debugger.Break();
        }
    }
}
