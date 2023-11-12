

using System;
using System.Text.Json.Serialization;

namespace FunctionApp3.Models
{


    public class CapteurIfo
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("timeInfo")]
        public DateTime TimeInfo { get; set; }
        [JsonPropertyName("info")]
        public string Info { get; set; }
    }
}
