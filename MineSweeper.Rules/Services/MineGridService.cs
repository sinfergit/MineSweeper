using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Options;
using MineSweeper.Domain.Contracts;
using MineSweeper.Domain.Entities;
using MineSweeper.Domain.Validators;

namespace MineSweeper.Rules.Services;

public class MineGridService:IMineGridService
{
    private readonly Random _random = new();
    private readonly IValidator<GridInitializeParams> _gridInitParamValidator;
    private readonly IValidator<SquareRevealParams> _revealingSquareParamValidator;
    private readonly GridConstraints GridSettings;

    public MineGridService(IOptions<GridConstraints> gridSettings)
    {
        GridSettings = gridSettings?.Value ?? throw new ArgumentNullException();
        _gridInitParamValidator = new GridInitializeParamValidator(GridSettings);
        _revealingSquareParamValidator = new RevealParamValidator();
    }

    public ValidationResult ValidateGridInitInputs(GridInitializeParams gridInitializeParams)
    {
        return _gridInitParamValidator.Validate(gridInitializeParams);
    }

    public ValidationResult ValidateRevealSquareCell(SquareRevealParams revealParams)
    {
        return _revealingSquareParamValidator.Validate(revealParams);
    }

    public int PositionMines(MineGrid grid, int minesCount)
    {
        int positionedMines = 0;
        if ((grid.Size * grid.Size) > minesCount)
        {
            while (positionedMines < minesCount)
            {
                int x = _random.Next(grid.Size);
                int y = _random.Next(grid.Size);

                if (!grid.Cells[x, y].IsMine)
                {
                    grid.Cells[x, y].IsMine = true;
                    positionedMines++;
                    UpdateAdjacentCells(grid, x, y);
                }
            }
        }
        return positionedMines;
    }

    public void UncoverCell(MineGrid grid, Position position)
    {
        if (grid.Cells[position.X, position.Y].IsUncovered || grid.Cells[position.X, position.Y].IsMine)
            return;

        grid.Cells[position.X, position.Y].IsUncovered = true;

        if (grid.Cells[position.X, position.Y].AdjacentMines == 0)
        {
            UncoverAdjacentCells(grid, position);
        }
    }
    
    public bool IsWon(MineGrid grid)
    {
        foreach (var cell in grid.Cells)
        {
            if (!cell.IsMine && !cell.IsUncovered)
            {
                return false;
            }
        }
        return true;
    }
    
    private static void UpdateAdjacentCells(MineGrid grid, int mineX, int mineY)
    {
        for (int x = mineX - 1; x <= mineX + 1; x++)
        {
            for (int y = mineY - 1; y <= mineY + 1; y++)
            {
                if (x >= 0 && x < grid.Size && y >= 0 && y < grid.Size && !grid.Cells[x, y].IsMine)
                {
                    grid.Cells[x, y].AdjacentMines++;
                }
            }
        }
    }
    
    private void UncoverAdjacentCells(MineGrid grid, Position position)
    {
        for (int x = position.X - 1; x <= position.X + 1; x++)
        {
            for (int y = position.Y - 1; y <= position.Y + 1; y++)
            {
                if (x >= 0 && x < grid.Size && y >= 0 && y < grid.Size)
                {
                    UncoverCell(grid, new Position { X = x, Y = y });
                }
            }
        }
    }
}