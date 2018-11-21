using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TogglSharpAPI.Responses
{
    class DataResponse<T>
    {
        [JsonProperty("since")]
        public int Since { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
