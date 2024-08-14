using System.Text.Json.Serialization;

namespace Application;

public class CommandResult
{
    private CommandResult(object data)
    {
        Data = data;
    }

    private CommandResult(List<string> errors)
    {
        Errors = errors;
    }

    [JsonPropertyName("data")]
    public object Data { get; }

    [JsonPropertyName("erros")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string> Errors { get; }

    public static CommandResult SuccessResult(object data)
    {
        return new CommandResult(data);
    }

    public static CommandResult ErrorResult(List<string> errors)
    {
        return new CommandResult(errors);
    }
}