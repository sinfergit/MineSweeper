using MineSweeper.Domain.Entities;

namespace GIC_Minesweeper.Helpers;

public static class UserInputHelper
{
    public static bool IsNumber(string text)
    {
        return int.TryParse(text, out _);
    }
}