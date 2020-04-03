using System;
using System.Linq;
using NUnit.Framework;

namespace RogueSheep.FieldOfView
{
    public class BeveledWallShadowcastingTests
    {
        [Test]
        [Ignore("Sandbox")]
        public void Sandbox()
        {
            var transparencyGrid = new GameGrid<bool>((50, 50));
            for (var i = 0; i < transparencyGrid.Length; i++)
            {
                transparencyGrid[i] = true;
            }
            var v = new BevelledWallShadowcasting(transparencyGrid);
            var c = v.Compute((25, 25), 1).Count(x => x);
            Assert.That(c == 9);

            c = v.Compute((25, 25), 8).Count(x => x);
            Assert.That(c == 249);

            for (var y = 0; y < 50; y++)
            {
                transparencyGrid[(25, y)] = false;
            }

            c = v.Compute((25, 25), 8).Count(x => x);
            Assert.That(c == 187);
        }
    }
}
