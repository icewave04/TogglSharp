using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TogglSharpAPI.Responses
{
    public class PagedDataResponse<T>
    {
        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        [JsonProperty("sort_field")]
        public string SortField{ get; set; }

        [JsonProperty("sort_order")]
        public string SortOrder { get; set; }

        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }

        public bool HasMoreData()
        {
            return (PerPage * Page >= TotalCount);
        }
    }
}
