using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TogglSharpAPI
{
    public class TogglAPIError
    {
        public HttpStatusCode StatusCode { get => Response.StatusCode; }
        public string Message { get; private set; }
        public HttpWebResponse Response { get; private set; }

        public TogglAPIError(HttpWebResponse response, string message)
        {
            Message = message;
            Response = response;
        }
    }
}
