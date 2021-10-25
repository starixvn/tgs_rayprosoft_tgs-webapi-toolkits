using System.Text.Json.Serialization;

namespace Tgs.WebApi.Toolkits.ViewModels
{
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
