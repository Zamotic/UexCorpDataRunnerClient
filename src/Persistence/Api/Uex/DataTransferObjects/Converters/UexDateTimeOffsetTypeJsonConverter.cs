using System.Text.Json;
using System.Text.Json.Serialization;

namespace UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects.Converters;
public class UexDateTimeOffsetTypeJsonConverter : JsonConverter<DateTimeOffset>
{
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TryGetInt64(out long l) ? new DateTimeOffset(DateTimeOffset.FromUnixTimeSeconds(l).DateTime, TimeSpan.FromHours(3)) : DateTimeOffset.MinValue;
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
