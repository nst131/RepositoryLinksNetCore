using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LinkUI.Models.TableWithUrlUI.Dto
{
    public class InputRedirectByShortUrlDtoUI
    {
        [JsonProperty(PropertyName = "shortUrl", Required = Required.Always, DefaultValueHandling = DefaultValueHandling.Include)]
        public string ShortUrl { get; set; } = string.Empty;
    }
}
