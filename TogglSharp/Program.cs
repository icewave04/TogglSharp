using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TogglSharpAPI;
using TogglSharpAPI.Responses;
using TogglSharpAPI.V9;

namespace Toggl
{
    class Program
    {
        public static TogglWebClient TogglWebClient = new TogglWebClient();
        public static Me Me;

        static void Main(string[] args)
        {
            Console.WriteLine("Please enter your API Token, or Email.");
            string emailOrToken = String.Empty;
            string password = String.Empty;

            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    emailOrToken += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && emailOrToken.Length > 0)
                    {
                        emailOrToken = emailOrToken.Substring(0, (emailOrToken.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);

            if (emailOrToken.Contains('@'))
            {
                Console.WriteLine("\nPlease enter your password.");
                do
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        password += key.KeyChar;
                        Console.Write("*");
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                        {
                            password = password.Substring(0, (password.Length - 1));
                            Console.Write("\b \b");
                        }
                        else if (key.Key == ConsoleKey.Enter)
                        {
                            break;
                        }
                    }
                } while (true);
                TogglWebClient.SetAuthentication(emailOrToken, password);
            }
            else
            {
                TogglWebClient.SetAuthentication(emailOrToken);
            }
            Console.WriteLine(String.Empty);
            GetStandardClient();
            GetMe();
        }

        public static async void GetMe()
        {
            // No error handling because YOLO
            Me me = await Me.Get();
            List<Client> clients = await Me.Instance.GetClients();
            List<Project> projects = await Me.Instance.GetProjects();
            Location location = await Me.Instance.GetLocation();
            List<Tag> tags = await Me.Instance.GetTags();
            List<Workspace> workspaces = await Me.Instance.GetWorkspaces();
            //DateTime since = DateTime.Now;
            //clients = await me.GetClients(since);
            //projects = await me.GetProjects(since);
            //location = await me.GetLocation(since);
            //tags = await me.GetTags(since);
            //workspaces = await me.GetWorkspaces(since);
            //workspaces = await me.GetWorkspaces();
            PagedDataResponse<List<Project>> projectList;
            PagedDataResponse<List<Project>> alwaysEmptyProjectList;
            List<WorkspaceActivity> topActivity;
            List<WorkspaceActivity> allActivity;
            if (workspaces.Count > 0)
            {
                projectList = await workspaces[0].GetProjects();
                // Unless you have 5001 deleted projects, and if you do that's awesome. Good job.
                alwaysEmptyProjectList = await workspaces[0].GetProjects(100, false);
                topActivity = await workspaces[0].GetTopActivity();
                allActivity = await workspaces[0].GetAllActivity();
            }
            List<TimeEntry> timeEntries = await Me.Instance.GetTimeEntries();
            Debugger.Break();
        }

        public static async void GetStandardClient()
        {
            TogglWebClient.SetDomain("https://www.toggl.com/");
            try
            {
                Me = await Me.Get();
            }
            catch(System.Net.WebException we)
            {
                Console.WriteLine(TogglWebClient.APIError.Message);
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
                System.Environment.Exit(0);
            }
        }

        
    }
}
