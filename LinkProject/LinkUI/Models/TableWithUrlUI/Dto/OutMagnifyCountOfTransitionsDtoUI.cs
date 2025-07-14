using Newtonsoft.Json;

namespace LinkUI.Models.TableWithUrlUI.Dto
{
    public class OutMagnifyCountOfTransitionsDtoUI
    {
        [JsonProperty(PropertyName = "id", Order = 0,
           NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "originalUrl", Order = 1,
            NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public string OriginalUrl { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "countRedirection", Order = 2,
             NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include)]
        public int CountRedirection { get; set; } = 0;
    }
}
