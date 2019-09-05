// ReSharper disable RedundantUsingDirective
using Assets.Scripts.Concepts.Cosmic.Space;
using Assets.Scripts.Concepts.Gameplay.Map.Components;
using Assets.Scripts.Concepts.Gameplay.Map.TileType.Wall;
using Assets.Scripts.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace RockRaiders.Tests.Concepts.Gameplay.Map.Components
{
    [TestClass]
    public class AdjoiningTilesGrid9Tests
    {
        [TestMethod]
        public void TileGrid9_Get_ShouldGet()
        {
            var grid = new AdjoiningTilesGrid9(new[] { new Tile(), null, null, null, null, null, null, null, null });

            Assert.IsNotNull(grid[CompassOrientation.NorthWest]);
            Enum.GetValues(typeof(CompassOrientation)).OfType<CompassOrientation>().Except(CompassOrientation.NorthWest).ForEach(o => Assert.IsNull(grid[o]));
        }

        [TestMethod]
        public void TileGrid9_Rotate_ShouldGet()
        {
            var grid = new AdjoiningTilesGrid9(new[] { new Tile(), null, null, null, null, new Tile() { TileType = TileWallSolidRock.GetInstance() }, null, null, new Tile() { TileType = TileWallSolidRock.GetInstance() } });

            var original = grid.Clone();
            grid = grid.Rotate(RotationalOrientation.Clockwise);

            Assert.IsNotNull(grid[CompassOrientation.NorthEast]);
            Assert.IsNotNull(grid[CompassOrientation.South]);
            Assert.IsNotNull(grid[CompassOrientation.SouthWest]);
            Enum.GetValues(typeof(CompassOrientation)).OfType<CompassOrientation>().Except(
                new[]{ CompassOrientation.NorthEast,
                    CompassOrientation.South,
                    CompassOrientation.SouthWest }).ForEach(o => Assert.IsNull(grid[o]));

            grid = grid.Rotate(RotationalOrientation.Anticlockwise);

            Enum.GetValues(typeof(CompassOrientation)).OfType<CompassOrientation>().ForEach(o => Assert.IsTrue(grid[o] == original[o]));
        }

        [TestMethod]
        public void TileGrid9_GetQuad_ShouldGet()
        {
            var t1 = new Tile() { TileType = TileWallSolidRock.GetInstance() };
            var t2 = new Tile() { TileType = TileWallHardRock.GetInstance() };
            var t3 = new Tile() { TileType = TileWallDirt.GetInstance() };
            var t4 = new Tile() { TileType = TileWallLooseRock.GetInstance() };
            var t5 = new Tile() { TileType = TileWallLooseRock.GetInstance() };
            var t6 = new Tile() { TileType = TileWallLooseRock.GetInstance() };
            var t7 = new Tile() { TileType = TileWallLooseRock.GetInstance() };
            var t8 = new Tile() { TileType = TileWallLooseRock.GetInstance() };
            var t9 = new Tile() { TileType = TileWallLooseRock.GetInstance() };

            var grid = new AdjoiningTilesGrid9(new[] { t1, t2, t3, t4, t5, t6, t7, t8, t9 });

            var quad = grid[CornerOrientation.NorthWest];
            Assert.AreEqual(t1, quad.Tiles[0]);
            Assert.AreEqual(t2, quad.Tiles[1]);
            Assert.AreEqual(t4, quad.Tiles[2]);
            Assert.AreEqual(t5, quad.Tiles[3]);

            quad = grid[CornerOrientation.NorthEast];
            Assert.AreEqual(t2, quad.Tiles[0]);
            Assert.AreEqual(t3, quad.Tiles[1]);
            Assert.AreEqual(t5, quad.Tiles[2]);
            Assert.AreEqual(t6, quad.Tiles[3]);

            quad = grid[CornerOrientation.SouthEast];
            Assert.AreEqual(t5, quad.Tiles[0]);
            Assert.AreEqual(t6, quad.Tiles[1]);
            Assert.AreEqual(t8, quad.Tiles[2]);
            Assert.AreEqual(t9, quad.Tiles[3]);

            quad = grid[CornerOrientation.SouthWest];
            Assert.AreEqual(t4, quad.Tiles[0]);
            Assert.AreEqual(t5, quad.Tiles[1]);
            Assert.AreEqual(t7, quad.Tiles[2]);
            Assert.AreEqual(t8, quad.Tiles[3]);
        }
    }
}
