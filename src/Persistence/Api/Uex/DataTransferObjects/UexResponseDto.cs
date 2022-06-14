using System.Text.Json.Serialization;

namespace UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;
public class UexResponseDto<T>
{
    [JsonPropertyName("status")]
    public string? Status { get; set; }
    [JsonPropertyName("code")]
    public int Code { get; set; }
    [JsonPropertyName("data")]
    public T? Data { get; set; }
}
