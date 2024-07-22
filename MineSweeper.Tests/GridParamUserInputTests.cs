
using GIC_Minesweeper.Helpers;
using MineSweeper.Domain.Entities;
using MineSweeper.Domain.Validators;
using MineSweeper.Shared;

namespace MineSweeper.Tests;

public class GridParamUserInputTests
{
    private GridInitializeParamValidator GridInitializeParamValidator {get;}
   
    public GridParamUserInputTests()
    {
        GridInitializeParamValidator = new GridInitializeParamValidator(new GridConstraints()
            { MaximumSize = 10, MinePercentage = 35, MinimumSize = 2 });
    }
    
    [Theory]
    [InlineData("1")]
    public void IsNumber_TestExtensionMethod_ReturnsTrue(string input)
    {
        var result = input.IsNumber();
        Assert.True(result);
    }
    
    [Theory]
    [InlineData("k")]
    public void IsNumber_TestExtensionMethod_ReturnsFalse(string input)
    {
        var result = input.IsNumber();
        Assert.False(result);
    }
    
    [InlineData("","")]
    [Theory]
    public void IsEmpty_AsksGridSizeAndMineCount_ReturnsFalse(string gridSize,string minesCount)
    {
        var result = GridInitializeParamValidator.Validate(new GridInitializeParams() {Size = gridSize,MineCount = minesCount});
        Assert.Contains(result.Errors, o =>o.ErrorMessage.Equals("Please enter the grid size"));
        Assert.Contains(result.Errors, o =>o.ErrorMessage.Equals("Please enter the number of mines to be positioned!"));
    }
    
    [InlineData("1","3")]
    [Theory]
    public void IsNumber_AsksGridSize_ReturnsTrue(string gridSize,string minesCount)
    {
        var result = GridInitializeParamValidator.Validate(new GridInitializeParams() {Size = gridSize,MineCount = minesCount});
        Assert.DoesNotContain(result.Errors, o =>o.ErrorMessage.Equals($"{gridSize} is not a valid number!"));
    }
    
  
    [InlineData("q","3")]
    [Theory]
    public void IsNumber_AsksGridSize_ReturnsFalse(string gridSize,string minesCount)
    {
        var result = GridInitializeParamValidator.Validate(new GridInitializeParams() {Size = gridSize,MineCount = minesCount});
        Assert.Contains(result.Errors, o =>o.ErrorMessage.Equals($"{gridSize} is not a valid number!"));
    }
    
    [InlineData("5","4")]
    [Theory]
    public void IsNumber_AsksNumberOfMines_ReturnsTrue(string gridSize,string minesCount)
    {
        var result = GridInitializeParamValidator.Validate(new GridInitializeParams() {Size = gridSize,MineCount = minesCount});
        Assert.DoesNotContain(result.Errors, o =>o.ErrorMessage.Equals($"{minesCount} is not a valid number!"));
    }
    
    [InlineData("5","e")]
    [Theory]
    public void IsNumber_AsksNumberOfMines_ReturnsFalse(string gridSize,string minesCount)
    {
        var result = GridInitializeParamValidator.Validate(new GridInitializeParams() {Size = gridSize,MineCount = minesCount});
        Assert.Contains(result.Errors, o =>o.ErrorMessage.Equals($"{minesCount} is not a valid number!"));
    }
    
    [InlineData("5","4")]
    [Theory]
    public void IsNumberOfMinesAllowed_AsksNumberOfMines_ReturnsTrue(string gridSize,string minesCount)
    {
        var result = GridInitializeParamValidator.Validate(new GridInitializeParams() {Size = gridSize,MineCount = minesCount});
        Assert.DoesNotContain(result.Errors, o =>o.ErrorMessage.Equals($"Maximum number is 35% of total squares."));
    }
    
    [InlineData("5","20")]
    [Theory]
    public void IsNumberOfMinesAllowed_AsksNumberOfMines_ReturnsFalse(string gridSize,string minesCount)
    {
        var result = GridInitializeParamValidator.Validate(new GridInitializeParams() {Size = gridSize,MineCount = minesCount});
        Assert.Contains(result.Errors, o =>o.ErrorMessage.Equals($"Maximum number is 35% of total squares."));
    }
    
    [InlineData("5","6")]
    [Theory]
    public void IsGridParamsValid_AsksGridParams_ReturnsTrue(string gridSize,string minesCount)
    {
        var result = GridInitializeParamValidator.Validate(new GridInitializeParams() {Size = gridSize,MineCount = minesCount});
        Assert.True(result.IsValid);
    }
    
    
}