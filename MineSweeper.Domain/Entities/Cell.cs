namespace MineSweeper.Domain.Entities;

public class Cell
{
    public Position Position { get; set; }
    public bool IsMine { get; set; }
    public bool IsUncovered{ get; set; }
    public int AdjacentMines { get; set; }
}