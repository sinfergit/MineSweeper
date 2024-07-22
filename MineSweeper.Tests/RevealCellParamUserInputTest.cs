using MineSweeper.Domain.Entities;
using MineSweeper.Domain.Validators;
using MineSweeper.Shared;

namespace MineSweeper.Tests;

public class RevealCellParamUserInputTest
{
    private RevealParamValidator RevealParamValidator {get;}
    
    public RevealCellParamUserInputTest()
    {
        RevealParamValidator = new RevealParamValidator();
    }
    
    [Theory]
    [InlineData("A1",5)]
    public void IsValid_TestRevealCellPosition_ReturnsTrue(string input,int size)
    {
        var result = RevealParamValidator.Validate(new SquareRevealParams() { CellText = input, GridSize = size });
        Assert.True(result.IsValid);
    }
    
    [Theory]
    [InlineData("a",5)]
    public void IsValid_TestRevealCellPosition_ReturnsFalse(string input,int size)
    {
        var result = RevealParamValidator.Validate(new SquareRevealParams() { CellText = input, GridSize = size });
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, o=> o.ErrorMessage.Equals("Invalid cell!"));
    }
    
    [Theory]
    [InlineData("B6",5)]
    public void IsValid_TestRevealCellPosition_ReturnsInputOutofBounds(string input,int size)
    {
        var result = RevealParamValidator.Validate(new SquareRevealParams() { CellText = input, GridSize = size });
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, o=> o.ErrorMessage.Equals("Input out of bounds. Please try again."));
    }
}