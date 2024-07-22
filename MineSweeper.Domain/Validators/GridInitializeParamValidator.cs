using FluentValidation;
using FluentValidation.Results;
using MineSweeper.Domain.Entities;
using MineSweeper.Shared;

namespace MineSweeper.Domain.Validators;

public class GridInitializeParamValidator : AbstractValidator<GridInitializeParams>
{
    public GridInitializeParamValidator(GridConstraints constraints)
    {
        RuleFor(p => p.Size).NotEmpty().WithErrorCode("GridSize").WithMessage("Please enter the grid size")
            .Custom((x, context) =>
            {
                if (!int.TryParse(x, out var parsedSize))
                {
                    context.AddFailure($"{x} is not a valid number!");
                }
                else if (parsedSize > constraints.MaximumSize)
                {
                    context.AddFailure($"Maximum grid size must be {constraints.MaximumSize}!");
                }
                else if (parsedSize < constraints.MinimumSize)
                {
                    context.AddFailure($"Minimum grid size must be {constraints.MinimumSize}!");
                }
            });

        RuleFor(o => o.MineCount).NotEmpty().WithMessage("Please enter the number of mines to be positioned!")
            .Custom((x, context) =>
            {
                if (!x.IsNumber())
                {
                    context.AddFailure($"{x} is not a valid number!");
                }
            });

        // best suitable for a separate validator class 
        RuleFor(p => p).Custom((x, context) =>
        {
            if (x.MineCount.IsNumber() && x.Size.IsNumber())
            {
                var mineCount = int.Parse(x.MineCount);
                var gridSize = int.Parse(x.Size);
                if (mineCount >= (int)(((gridSize*gridSize) * constraints.MinePercentage) / 100))
                {
                    context.AddFailure($"Maximum number is {constraints.MinePercentage}% of total squares.");
                }
            }
        });
    }
}