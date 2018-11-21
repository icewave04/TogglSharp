using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TogglSharpAPI.V9
{
    public class Location
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }
}
