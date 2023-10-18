using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UexCorpDataRunner.Persistence.Api.Converters;

namespace UexCorpDataRunner.Persistence.Api.UexV2.DataTransferObjects;
public class BaseDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("date_added")]
    [JsonConverter(typeof(UexDateTimeOffsetTypeJsonConverter))]
    public DateTimeOffset DateAdded { get; set; }

    [JsonPropertyName("date_modified")]
    [JsonConverter(typeof(UexDateTimeOffsetTypeJsonConverter))]
    public DateTimeOffset DateModified { get; set; }
}
