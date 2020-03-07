using System;
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
            Assert.True(grid[x, y] == value);
            Assert.True(grid[(x, y)] == value);
        }

        [Test]
        public void GameGrid_GetNeighborhood_ReturnDefaultValueWhenOutOfBounds_Inclusive()
        {
            // Arrange
            var grid = new GameGrid<int>((2, 2));
            for (var i = 0; i < grid.Length; i++)
            {
                grid[i] = i + 1;
            }

            // Act
            var value = grid.GetNeighborhood((0, 0), defaultValue: 5, inclusive: true);

            // Assert
            Assert.That(value, Has.Exactly(1).EqualTo(1));
            Assert.That(value, Has.Exactly(1).EqualTo(2));
            Assert.That(value, Has.Exactly(1).EqualTo(3));
            Assert.That(value, Has.Exactly(1).EqualTo(4));
            Assert.That(value, Has.Exactly(5).EqualTo(5));
        }

        [Test]
        public void GameGrid_GetNeighborhood_ReturnDefaultValueWhenOutOfBounds_Exclusive()
        {
            // Arrange
            var grid = TestGrid();

            // Act
            var value = grid.GetNeighborhood((0, 0), defaultValue: 5, inclusive: false);

            // Assert
            Assert.That(value, Has.Exactly(1).EqualTo(2));
            Assert.That(value, Has.Exactly(1).EqualTo(3));
            Assert.That(value, Has.Exactly(1).EqualTo(4));
            Assert.That(value, Has.Exactly(5).EqualTo(5));
        }

        [TestCase(-1, false)]
        [TestCase(0, true)]
        [TestCase(3, true)]
        [TestCase(4, false)]
        public void GameGrid_IsInBoundsForSingleIndex_ReturnsCorrectValue(int index, bool result)
        {
            // Arrange
            var grid = TestGrid();

            // Act & Assert
            Assert.True(grid.IsInBounds(index) == result);
        }

        [TestCase(-1, -1, false)]
        [TestCase(-1, 0, false)]
        [TestCase(0, -1, false)]
        [TestCase(0, 0, true)]
        [TestCase(1, 0, true)]
        [TestCase(0, 1, true)]
        [TestCase(1, 1, true)]
        [TestCase(-1, 1, false)]
        [TestCase(1, -1, false)]
        [TestCase(2, 1, false)]
        [TestCase(2, 0, false)]
        [TestCase(0, 2, false)]
        [TestCase(1, 2, false)]
        [TestCase(2, 2, false)]
        public void GameGrid_IsInBoundsForDoubleIndex_ReturnsCorrectValue(int x, int y, bool result)
        {
            // Arrange
            var grid = TestGrid();

            // Act & Assert
            Assert.True(grid.IsInBounds(x, y) == result);
            Assert.True(grid.IsInBounds((x, y)) == result);
            Assert.True(grid.IsInBounds(new Point2i(x, y)) == result);
        }

        [Test]
        public void GameGrid_ClosestPosition_ThrowsWhenFailsToFindLegalPosition()
        {
            // Assert
            var grid = TestGrid();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => grid.ClosestPosition((0, 0), (g, p) => g[p] == -1));
        }

        private GameGrid<int> TestGrid()
        {
            var grid = new GameGrid<int>((2, 2));
            for (var i = 0; i < grid.Length; i++)
            {
                grid[i] = i + 1;
            }
            return grid;
        }
    }
}
