using System.Text.Json.Serialization;

namespace UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects;
public class UexResponseDto<T> where T : class
{
    [JsonPropertyName("status")]
    public string? Status { get; set; }
    [JsonPropertyName("code")]
    public int Code { get; set; }
    [JsonPropertyName("data")]
    public IList<T>? Data { get; set; }
}
