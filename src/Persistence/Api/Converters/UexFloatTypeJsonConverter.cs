using System.Text.Json;
using System.Text.Json.Serialization;

namespace UexCorpDataRunner.Persistence.Api.Converters;
public class UexFloatTypeJsonConverter : JsonConverter<float>
{
    public override bool HandleNull => true;

    public override float Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if(reader.TokenType == JsonTokenType.Null)
        {
            return 0f;
        }

        if(reader.TryGetSingle(out float value) == false)
        {
            return 0f;
        }

        return value;
    }

    public override void Write(Utf8JsonWriter writer, float value, JsonSerializerOptions options)
    {
        string returnValue = value.ToString();
        writer.WriteStringValue(returnValue);
    }
}
