using FluentValidation.Results;
using MineSweeper.Domain.Contracts;
using MineSweeper.Domain.Entities;
using MineSweeper.Shared;

namespace MineSweeper.Rules.UseCases;

public class InitializeGridUseCase
{
    private readonly IMineGridService _mineGridService;
    
    public InitializeGridUseCase(IMineGridService mineGridService)
    {
        _mineGridService = mineGridService;
    }

    public UseCaseResponse<MineGrid> Execute(string size, string minesCount)
    {
        MineGrid? grid = null;
        var result =  _mineGridService.ValidateGridInitInputs(new GridInitializeParams(){MineCount = minesCount,Size = size});
       
       if (!result.IsValid)
       {
           return UseCaseResponseMapper.Map<MineGrid>(false, result.Errors.Select(e => e.ErrorMessage).ToList());
       }
       grid = new MineGrid(int.Parse(size));
       return UseCaseResponseMapper.Map(grid);
    }
}