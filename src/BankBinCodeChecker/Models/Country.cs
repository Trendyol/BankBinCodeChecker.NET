using Newtonsoft.Json;

namespace BankBinCodeChecker.Models
{
    public class Country
    {
        [JsonProperty("numeric")]
        public string Numeric { get; set; }
        
        [JsonProperty("alpha2")]
        public string Alpha2 { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("emoji")]
        public string Emoji { get; set; }
        
        [JsonProperty("currency")]
        public string Currency { get; set; }
        
        [JsonProperty("latitude")]
        public int Latitude { get; set; }
        
        [JsonProperty("longitude")]
        public int Longitude { get; set; }
    }
}