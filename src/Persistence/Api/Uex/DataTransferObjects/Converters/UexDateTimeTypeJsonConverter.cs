using System.Text.Json;
using System.Text.Json.Serialization;

namespace UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects.Converters;
public class UexDateTimeTypeJsonConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TryGetInt64(out long l) ? new DateTime(l * 10000000) : DateTime.MinValue;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
