namespace MineSweeper.Shared;

public class UseCaseResponse<T>
{
    public bool Success { get; }
    public T Data { get; }
    public List<string> Errors { get; }

    public UseCaseResponse(bool success, T data, List<string> errors = null)
    {
        Success = success;
        Data = data;
        Errors = errors ?? new List<string>();
    }

    public UseCaseResponse(bool success, List<string> errors)
    {
        Success = success;
        Data = default;
        Errors = errors ?? new List<string>();
    }
}