using FluentValidation;
using MineSweeper.Domain.Entities;

namespace MineSweeper.Domain.Validators;

public class RevealParamValidator:AbstractValidator<SquareRevealParams>
{
    public RevealParamValidator()
    {
        RuleFor(o => o).NotEmpty().WithMessage("Please enter a cell to reveal")
            .Custom((o, context) =>
            {
                if (!string.IsNullOrEmpty(o.CellText))
                {
                    var cells = o.CellText.ToCharArray();
                    if(cells.Length<2)
                        context.AddFailure("Invalid cell!");
                    if(!char.IsNumber(cells[1]))
                        context.AddFailure($"{cells[1]} is not a number");
                    if(!char.IsLetter(cells[0]))
                        context.AddFailure($"{cells[0]} is not a letter");

                    if (char.IsNumber(cells[1]) && char.IsLetter(cells[0]))
                    {
                        int row = cells[0] - 'A';
                        int col = int.Parse(cells[1].ToString()) - 1;
                        if (row < 0 || row >= o.GridSize || col < 0 || col >= o.GridSize)
                            context.AddFailure($"Input out of bounds. Please try again.");
                    }
                }
            });
        
    }
}