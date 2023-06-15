namespace Project_NGO.Utils;

public class CustomStatusResult<T>
{
    public CustomStatusResult()
    {
    }

    public CustomStatusResult(int status, string message, T? data, List<string>? error)
    {
        Status = status;
        Message = message;
        Data = data;
        Error = error;
    }

    public int Status { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }
    public List<string>? Error { get; set; }
}