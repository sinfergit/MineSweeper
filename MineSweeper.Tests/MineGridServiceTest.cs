using Microsoft.Extensions.Options;
using MineSweeper.Domain.Contracts;
using MineSweeper.Domain.Entities;
using MineSweeper.Rules.Services;
using Moq;

namespace MineSweeper.Tests;

public class MineGridServiceTest
{
    private readonly Mock<IOptions<GridConstraints>> gridSettings = new Mock<IOptions<GridConstraints>>();
    private readonly IMineGridService _mineGridService;
    public MineGridServiceTest()
    {
        gridSettings.Setup(o => o.Value).Returns(new GridConstraints()
        {
            MaximumSize = 10, MinimumSize = 2, MinePercentage = 35
        });
        _mineGridService = new MineGridService(gridSettings.Object);
    }

    [InlineData(10)]
    [Theory]
    public void TestPositionMines_ReturnsCorrectCount(int minesCount)
    {
        var grid = new MineGrid(5) { };
        grid.InitializeCells();
        
       int positionedMinesCount =  _mineGridService.PositionMines(grid, minesCount);
       Assert.Equal(positionedMinesCount,minesCount);
    }
    
    [InlineData(6)]
    [Theory]
    public void TestPositionMines_ReturnsInCorrectCount(int minesCount)
    {
        var grid = new MineGrid(2) { };
        grid.InitializeCells();
        
        int positionedMinesCount =  _mineGridService.PositionMines(grid, minesCount);
        Assert.NotEqual(positionedMinesCount,minesCount);
    }
}