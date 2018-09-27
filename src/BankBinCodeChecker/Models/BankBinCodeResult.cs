using Newtonsoft.Json;

namespace BankBinCodeChecker.Models
{
    public class BankBinCodeResult
    {
        [JsonProperty("number")]
        public Number Number { get; set; }
        
        [JsonProperty("scheme")]
        public string Scheme { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("brand")]
        public string Brand { get; set; }
        
        [JsonProperty("country")]
        public Country Country { get; set; }
        
        [JsonProperty("bank")]
        public Bank Bank { get; set; }
    }
}