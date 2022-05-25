using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace UexCorpDataRunner.Application.DataTransferObjects;
public class UexResponseDto<T> where T : class
{
    [JsonPropertyName("status")]
    public string? Status { get; set; }
    [JsonPropertyName("code")]
    public int Code { get; set; }
    [JsonPropertyName("data")]
    public IList<T>? Data { get; set; }
}
