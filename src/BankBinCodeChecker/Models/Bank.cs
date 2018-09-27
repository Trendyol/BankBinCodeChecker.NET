using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BankBinCodeChecker.Models
{
    public class Bank
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("url")]
        public string Url { get; set; }
        
        [JsonProperty("phone")]
        public string Phone { get; set; }
    }
}