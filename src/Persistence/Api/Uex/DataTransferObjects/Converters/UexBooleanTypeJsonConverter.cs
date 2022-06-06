using System.Text.Json;
using System.Text.Json.Serialization;

namespace UexCorpDataRunner.Persistence.Api.Uex.DataTransferObjects.Converters;
public class UexBooleanTypeJsonConverter : JsonConverter<bool>
{
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.ValueSpan[0];
        char valueChar = (char)value;
        if(byte.TryParse(valueChar.ToString(), out byte b))
        {
            if (b.Equals(0))
            {
                return false;
            }
            if (b.Equals(1))
            {
                return true;
            }
            throw new Exception("Unknown Boolean Value Encountered");
        }
        return false;
    }

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
