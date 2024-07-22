namespace MineSweeper.Domain.Entities;

public class UserInput
{
    public Cell Cell { get; }
    public bool IsMine { get; set; }
    public DateTime GameStartedTime { get; set; }
    public DateTime GameEndedTime { get; set; }
    
    public override string ToString()
    {
        return $"Row: {Cell.Position.X}, Col: {Cell.Position.Y}, IsMine: {IsMine}, AdjacentMines: {Cell.AdjacentMines}";
    }
}