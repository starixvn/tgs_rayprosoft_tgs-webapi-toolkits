using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Tgs.WebApi.Toolkits.ViewModels
{
    [DisplayName("Success")]
    public class SuccessViewModel<TResult>
    {
        [JsonPropertyName("data")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TResult Data { get; init; } = default!;

        [JsonPropertyName("err")]
        public int Error { get; } = 0;

        [JsonPropertyName("errmsg")]
        public string ErrorMessage { get; } = string.Empty;
    }
}
