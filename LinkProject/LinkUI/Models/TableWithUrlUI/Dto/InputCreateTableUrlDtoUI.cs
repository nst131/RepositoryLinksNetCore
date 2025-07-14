using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LinkUI.Models.TableWithUrlUI.Dto
{
    public class InputCreateTableUrlDtoUI
    {
        [Required(ErrorMessage = "This field reqiered")]
        [Url(ErrorMessage = "Please enter correct URL")]
        [StringLength(256, ErrorMessage = "URL cannot exceed 256 symbols")]

        [JsonProperty(PropertyName = "originalUrl", Required = Required.Always, DefaultValueHandling = DefaultValueHandling.Include)]
        public string OriginalUrl { get; set; } = string.Empty;
    }
}
