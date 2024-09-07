using System.Text.Json.Serialization;

namespace UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;
public class UexResponseDto<T>
{
    [JsonPropertyName("status")]
    public string? Status { get; set; }
    [JsonPropertyName("http_code")]
    public int Code { get; set; }
    [JsonPropertyName("data")]
    public T? Data { get; set; }
}
