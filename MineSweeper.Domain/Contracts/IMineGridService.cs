using FluentValidation.Results;
using MineSweeper.Domain.Entities;

namespace MineSweeper.Domain.Contracts;

public interface IMineGridService
{
    int PositionMines(MineGrid grid, int minesCount);
    void UncoverCell(MineGrid grid, Position position);
    bool IsWon(MineGrid grid);
    ValidationResult ValidateGridInitInputs(GridInitializeParams gridInitializeParams);
    
    ValidationResult ValidateRevealSquareCell(SquareRevealParams revealParamss);

}