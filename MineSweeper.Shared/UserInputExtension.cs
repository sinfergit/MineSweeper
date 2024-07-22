namespace MineSweeper.Shared;

public static class UserInputExtension
{
    public static bool IsNumber(this string  input)
    {
        return int.TryParse(input, out _);
    }
}