using Xunit;

namespace RogueSheep.Tests
{
    public class GameGridTests
    {
        /// <summary>
        /// [ 1, 2 ]
        /// [ 3, 4 ]
        /// </summary>
        [Theory]
        [InlineData(0, 0, 1)]
        [InlineData(0, 1, 3)]
        [InlineData(1, 0, 2)]
        [InlineData(1, 1, 4)]
        public void GameGrid_IndexesCorrectly(int x, int y, int value)
        {
            // Arrange
            var grid = new GameGrid<int>((2, 2));
            for (var i = 0; i < grid.Length; i++)
            {
                grid[i] = i + 1;
            }

            // Act & Assert
            Assert.True(grid[(x, y)] == value);
        }
    }
}
