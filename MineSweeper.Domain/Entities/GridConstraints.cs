namespace MineSweeper.Domain.Entities;

public class GridConstraints
{
    public int MinimumSize { get; set; }
    public int MinePercentage { get; set; }
    
    public int MaximumSize { get; set; }
}