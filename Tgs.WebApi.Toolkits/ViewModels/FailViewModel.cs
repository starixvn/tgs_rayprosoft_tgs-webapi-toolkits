using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Tgs.WebApi.Toolkits.ViewModels
{
    [DisplayName("Failure")]
    public class FailViewModel
    {
        [JsonPropertyName("err")]
        public int Error { get; init; }

        [JsonPropertyName("errmsg")]
        public string ErrorMessage { get; init; } = default!;

        [JsonPropertyName("errmsgid")]
        public string ErrorMessageKey { get; init; } = default!;
    }
}
