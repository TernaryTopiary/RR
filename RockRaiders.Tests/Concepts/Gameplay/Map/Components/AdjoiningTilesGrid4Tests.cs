// ReSharper disable RedundantUsingDirective
using Assets.Scripts.Concepts.Cosmic.Space;
using Assets.Scripts.Concepts.Gameplay.Map.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RockRaiders.Tests.Concepts.Gameplay.Map.Components
{
    [TestClass]
    public class AdjoiningTilesGrid4Tests
    {
        [TestMethod]
        public void TileGrid4_Get_ShouldGet()
        {
            var grid = new AdjoiningTilesGrid4(new[] { new Tile(), null, null, null });

            Assert.IsNotNull(grid[CornerOrientation.NorthWest]);
            Assert.IsNull(grid[CornerOrientation.NorthEast]);
            Assert.IsNull(grid[CornerOrientation.SouthWest]);
            Assert.IsNull(grid[CornerOrientation.SouthEast]);
        }

        [TestMethod]
        public void TileGrid4_Rotate_ShouldGet()
        {
            var grid = new AdjoiningTilesGrid4(new[] { new Tile(), null, null, null });

            grid = grid.Rotate(RotationalOrientation.Clockwise);

            Assert.IsNull(grid[CornerOrientation.NorthWest]);
            Assert.IsNotNull(grid[CornerOrientation.NorthEast]);
            Assert.IsNull(grid[CornerOrientation.SouthWest]);
            Assert.IsNull(grid[CornerOrientation.SouthEast]);

            grid = grid.Rotate(RotationalOrientation.Anticlockwise);
            grid = grid.Rotate(RotationalOrientation.Anticlockwise);

            Assert.IsNull(grid[CornerOrientation.NorthWest]);
            Assert.IsNull(grid[CornerOrientation.NorthEast]);
            Assert.IsNotNull(grid[CornerOrientation.SouthWest]);
            Assert.IsNull(grid[CornerOrientation.SouthEast]);
        }
    }
}