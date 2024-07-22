using FluentValidation.Results;
using MineSweeper.Domain.Contracts;
using MineSweeper.Domain.Entities;
using MineSweeper.Rules.UseCases;
using MineSweeper.Shared;
using Moq;

namespace MineSweeper.Tests;

public class InitializeGrisUseCaseTest
{
    private readonly Mock<IMineGridService> _mockMineGridService = new Mock<IMineGridService>();
    private readonly InitializeGridUseCase _initializeGridUseCase;
    public InitializeGrisUseCaseTest()
    {
        _initializeGridUseCase = new InitializeGridUseCase(_mockMineGridService.Object);
    }

    [InlineData("5","4")]
    [Theory]
    public void TestExecuteUseCaseReturnsSuccessWithMocking(string size, string minesCount)
    {
        var result = new ValidationResult() { Errors = new List<ValidationFailure>() };
        _mockMineGridService.Setup(x =>
                x.ValidateGridInitInputs(It.IsAny<GridInitializeParams>()))
            .Returns(result);
        
        UseCaseResponse<MineGrid> response = _initializeGridUseCase.Execute(size, minesCount);
        
        
        Assert.True(response is { Success: true, Data.Cells.Length: 5 });
    }
    
    [InlineData("100","6",10)]
    [Theory]
    public void TestExecuteUseCaseReturnsFailureWithMocking(string size, string minesCount,int maxiumGridSize)
    {
        var result = new ValidationResult() { Errors = new List<ValidationFailure>(new []{new ValidationFailure(){ErrorMessage = $"Maximum grid size must be {maxiumGridSize}!"}}) };
        _mockMineGridService.Setup(x =>
                x.ValidateGridInitInputs(It.IsAny<GridInitializeParams>()))
            .Returns(result);
        
        var response = _initializeGridUseCase.Execute(size, minesCount);
        
        Assert.True(response is { Success: false, Errors.Count: > 0 });
    }
}