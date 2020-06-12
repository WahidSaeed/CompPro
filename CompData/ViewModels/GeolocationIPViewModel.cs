using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompData.ViewModels
{
    public class GeolocationIPViewModel
    {
        [JsonProperty("ip_address")]
        public string IPAddress { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }
        [JsonProperty("continent")]
        public string Continent { get; set; }
        [JsonProperty("continent_code")]
        public string ContinentCode { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("county")]
        public string County { get; set; }
        [JsonProperty("region")]
        public string Region { get; set; }
        [JsonProperty("region_code")]
        public string RegionCode { get; set; }
        [JsonProperty("timezone")]
        public string TimeZone { get; set; }
        [JsonProperty("owner")]
        public string Owner { get; set; }
        [JsonProperty("longitude")]
        public string Longitude { get; set; }
        [JsonProperty("latitude")]
        public string Latitude { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("languages")]
        public List<string> Languages { get; set; }
    }
}
