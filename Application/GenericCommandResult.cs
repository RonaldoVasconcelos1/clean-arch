namespace Application;

public class GenericCommandResult
{
    public GenericCommandResult() { }

    public GenericCommandResult(bool success, string message, object data, bool isCreated = false)
    {
        Success = success;
        Message = message;
        Data = data;
        IsCreated = isCreated;
    }

    public bool Success { get; set; }
    public string Message { get; set; }
    public object Data { get; set; }
    public bool IsCreated { get; set; }
}