namespace MineSweeper.Domain.Entities;

public class MineGrid
{
    public int Size { get; private set; }
    public Cell[,] Cells { get; private set; }

    public MineGrid(int size)
    {
        Size = size;
        Cells = new Cell[size, size];
        InitializeCells();
    }
    
    public bool IsWithinBounds(int x, int y)
    {
        return x >= 0 && x < Size && y >= 0 && y < Size;
    }
    
    public void InitializeCells()
    {
        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                Cells[x, y] = new()
                {
                    Position = new() { X = x, Y = y },
                    IsMine = false,
                    IsUncovered = false,
                    AdjacentMines = 0
                };
            }
        }
    }
}