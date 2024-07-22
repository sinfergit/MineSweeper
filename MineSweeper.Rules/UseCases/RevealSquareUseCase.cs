using FluentValidation.Results;
using MineSweeper.Domain.Contracts;
using MineSweeper.Domain.Entities;
using MineSweeper.Shared;

namespace MineSweeper.Rules.UseCases;

public class RevealSquareUseCase
{
    private readonly IMineGridService _mineGridService;
    
    public RevealSquareUseCase(IMineGridService mineGridService)
    {
        _mineGridService = mineGridService;
    }
    public UseCaseResponse<Position> Execute(SquareRevealParams revealParams)
    {
        Position cell = new Position();
        var result =  _mineGridService.ValidateRevealSquareCell(revealParams);
       
        if (!result.IsValid)
        {
            return UseCaseResponseMapper.Map<Position>(false, result.Errors.Select(e => e.ErrorMessage).ToList());
        }
     
        cell.X = revealParams.CellText.ToCharArray()[0] - 'A';
        cell.Y = int.Parse(revealParams.CellText.ToCharArray()[1].ToString()) - 1;
        return UseCaseResponseMapper.Map(cell);
    }
}