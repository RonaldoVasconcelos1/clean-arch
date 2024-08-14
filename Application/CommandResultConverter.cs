using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application;

public class CommandResultConverter : JsonConverter<CommandResult>
{
    public override CommandResult? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, CommandResult value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        if (value.Errors != null)
        {
            writer.WritePropertyName("errors");
            JsonSerializer.Serialize(writer, value.Errors, options);
        }

        if (value.Errors == null)
        {
            writer.WritePropertyName("data");
            JsonSerializer.Serialize(writer, value.Data, options);
        }

        writer.WriteEndObject();
    }
}