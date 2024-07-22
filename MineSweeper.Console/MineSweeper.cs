using Microsoft.Extensions.Logging;
using MineSweeper.Domain.Contracts;
using MineSweeper.Domain.Entities;
using MineSweeper.Rules.UseCases;
using MineSweeper.Shared;
using Serilog;

namespace GIC_Minesweeper;

public class MineSweeper
{
    private readonly IMineGridService _mineGridService;
    private readonly InitializeGridUseCase _initializeGridUseCase;
    private readonly RevealSquareUseCase _revealSquareUseCase;
    private Dictionary<string, string> recordedUserInputs = new Dictionary<string, string>();

    public MineSweeper(IMineGridService mineGridService
        , InitializeGridUseCase initializeGridUseCase
        , RevealSquareUseCase revealSquareUseCase
        )
    {
        _mineGridService = mineGridService;
        _initializeGridUseCase = initializeGridUseCase;
        _revealSquareUseCase = revealSquareUseCase;
    }

    public void Start()
    {
        try
        {
            Console.WriteLine("Enter grid size:");
            string gridSize = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter number of mines:");
            string minesCount = Console.ReadLine() ?? string.Empty;

            // Execute the grid initializing use case with user input.
            UseCaseResponse<MineGrid> gridInitialized = _initializeGridUseCase.Execute(gridSize, minesCount);
            if (!gridInitialized.Success)
            {
                gridInitialized.Errors.ToList().ForEach(Console.WriteLine);
            }
            else
            {
                MineGrid grid = gridInitialized.Data;
                _mineGridService.PositionMines(grid, int.Parse(minesCount));

                while (true)
                {
                    PrintGrid(grid);

                    Console.WriteLine("Select a square to reveal (e.g. A1):");
                    
                    // Execute the grid initializing use case with user input
                    UseCaseResponse<Position> position = _revealSquareUseCase.Execute(new SquareRevealParams()
                        { CellText = Console.ReadLine(), GridSize = grid.Size });

                    if (!position.Success)
                    {
                        position.Errors.ForEach(Console.WriteLine);
                    }
                    else
                    {
                        if (grid.Cells[position.Data.X, position.Data.Y].IsMine)
                        {
                            Console.WriteLine("Oh no, you detonated a mine! Game over.");
                            PrintGrid(grid, revealAll: true);
                            break;
                        }

                        _mineGridService.UncoverCell(grid, new Position { X = position.Data.X, Y = position.Data.Y });

                        if (_mineGridService.IsWon(grid))
                        {
                            Console.WriteLine("Congratulations, you have won the game!");
                            PrintGrid(grid, revealAll: true);
                            break;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Log.Logger.Error(ex.ToString());
        }
    }

    private void PrintGrid(MineGrid grid, bool revealAll = false)
    {
        // Print column headers
        Console.Write("   ");
        for (int c = 1; c <= grid.Size; c++)
        {
            Console.Write($" {c} ");
        }

        Console.WriteLine();

        // Print rows
        for (int x = 0; x < grid.Size; x++)
        {
            Console.Write($" {(char)('A' + x)} ");
            for (int y = 0; y < grid.Size; y++)
            {
                var cell = grid.Cells[x, y];
                if (revealAll || cell.IsUncovered)
                {
                    Console.Write(cell.IsMine ? " * " : $" {cell.AdjacentMines} ");
                }
                else
                {
                    Console.Write(" _ ");
                }
            }

            Console.WriteLine();
        }
    }
}