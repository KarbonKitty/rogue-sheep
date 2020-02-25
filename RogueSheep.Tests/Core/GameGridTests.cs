using NUnit.Framework;

namespace RogueSheep.Tests
{
    public class GameGridTests
    {
        /// <summary>
        /// [ 1, 2 ]
        /// [ 3, 4 ]
        /// </summary>
        [TestCase(0, 0, 1)]
        [TestCase(0, 1, 3)]
        [TestCase(1, 0, 2)]
        [TestCase(1, 1, 4)]
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

        [Test]
        public void GameGrid_GetNeighborhood_ReturnDefaultValueWhenOutOfBounds()
        {
            // Arrange
            var grid = new GameGrid<int>((2, 2));
            for (var i = 0; i < grid.Length; i++)
            {
                grid[i] = i + 1;
            }

            // Act
            var value = grid.GetNeighborhood((0, 0), 5);

            // Assert
            Assert.That(value, Has.Exactly(1).EqualTo(1));
            Assert.That(value, Has.Exactly(1).EqualTo(2));
            Assert.That(value, Has.Exactly(1).EqualTo(3));
            Assert.That(value, Has.Exactly(1).EqualTo(4));
            Assert.That(value, Has.Exactly(5).EqualTo(5));
        }
    }
}
