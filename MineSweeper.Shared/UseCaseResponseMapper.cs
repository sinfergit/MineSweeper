namespace MineSweeper.Shared;

public class UseCaseResponseMapper
{
    public static UseCaseResponse<T> Map<T>(T data, bool success = true, List<string> errors = null)
    {
        return new UseCaseResponse<T>(success, data, errors);
    }

    public static UseCaseResponse<T> Map<T>(bool success, List<string> errors)
    {
        return new UseCaseResponse<T>(success, errors);
    }
}