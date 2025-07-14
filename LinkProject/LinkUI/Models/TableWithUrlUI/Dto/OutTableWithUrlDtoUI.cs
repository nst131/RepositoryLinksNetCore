using Newtonsoft.Json;

namespace LinkUI.Models.TableWithUrlUI.Dto
{
    public class OutTableWithUrlDtoUI
    {
        [JsonProperty(PropertyName = "id", Order = 0,
             NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "originalUrl", Order = 2,
            NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string OriginalUrl { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "shortUrl", Order = 3,
            NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string ShortUrl { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "createdAt", Order = 4,
            NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string CreatedAt { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "countRedirection", Order = 5,
             NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public int CountRedirection { get; set; } = 0;
    }
}
