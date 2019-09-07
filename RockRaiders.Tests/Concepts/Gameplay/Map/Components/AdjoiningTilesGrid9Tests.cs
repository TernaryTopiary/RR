// ReSharper disable RedundantUsingDirective
using Assets.Scripts.Concepts.Cosmic.Space;
using Assets.Scripts.Concepts.Gameplay.Map.Components;
using Assets.Scripts.Concepts.Gameplay.Map.TileType.Wall;
using Assets.Scripts.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RockRaiders.Tests.Concepts.Gameplay.Map.Components
{
    [TestClass]
    public class AdjoiningTilesGrid9Tests
    {
        [TestMethod]
        public void TileGrid9_Get_ShouldGet()
        {
            var grid = new AdjoiningTilesGrid9(new List<KeyValuePair<CompassOrientation, Tile>> { new KeyValuePair<CompassOrientation, Tile>(CompassOrientation.NorthWest, new Tile()) });

            Assert.IsNotNull(grid[CompassOrientation.NorthWest]);
            Enum.GetValues(typeof(CompassOrientation)).OfType<CompassOrientation>().Except(CompassOrientation.NorthWest).ForEach(o => Assert.IsFalse(grid.ContainsKey(o)));
        }

        [TestMethod]
        public void TileGrid9_Rotate_ShouldntTouchOriginal()
        {
            // Arrange
            var grid = new AdjoiningTilesGrid9(new List<KeyValuePair<CompassOrientation, Tile>> {
                new KeyValuePair<CompassOrientation, Tile>(CompassOrientation.NorthWest, new Tile()),
                new KeyValuePair<CompassOrientation, Tile>(CompassOrientation.East, new Tile(){ TileType = TileWallSolidRock.GetInstance() }),
                new KeyValuePair<CompassOrientation, Tile>(CompassOrientation.SouthEast, new Tile(){ TileType = TileWallSolidRock.GetInstance() }),
            });
            var original = grid.Clone();

            // Act
            grid = grid.Rotate(RotationalOrientation.Clockwise);

            // Assert
            Assert.IsNotNull(original[CompassOrientation.NorthWest]);
            Assert.IsNotNull(original[CompassOrientation.East]);
            Assert.IsNotNull(original[CompassOrientation.SouthEast]);
        }

        [TestMethod]
        public void TileGrid9_RotateClockwise_ShouldGet()
        {
            // Arrange
            var grid = new AdjoiningTilesGrid9(new List<KeyValuePair<CompassOrientation, Tile>> {
                new KeyValuePair<CompassOrientation, Tile>(CompassOrientation.NorthWest, new Tile()),
                new KeyValuePair<CompassOrientation, Tile>(CompassOrientation.East, new Tile(){ TileType = TileWallSolidRock.GetInstance() }),
                new KeyValuePair<CompassOrientation, Tile>(CompassOrientation.SouthEast, new Tile(){ TileType = TileWallSolidRock.GetInstance() }),
            });
            var original = grid.Clone();

            // Act
            grid = grid.Rotate(RotationalOrientation.Clockwise);

            // Assert
            Assert.IsNotNull(grid[CompassOrientation.NorthEast]);
            Assert.IsNotNull(grid[CompassOrientation.South]);
            Assert.IsNotNull(grid[CompassOrientation.SouthWest]);
            Enum.GetValues(typeof(CompassOrientation)).OfType<CompassOrientation>().Except(
                new[]{ CompassOrientation.NorthEast,
                    CompassOrientation.South,
                    CompassOrientation.SouthWest }).ForEach(o => Assert.IsFalse(grid.ContainsKey(o)));
        }

        [TestMethod]
        public void TileGrid9_RotateAnticlockwise_ShouldGet()
        {
            // Arrange
            var grid = new AdjoiningTilesGrid9(new List<KeyValuePair<CompassOrientation, Tile>> {
                new KeyValuePair<CompassOrientation, Tile>(CompassOrientation.NorthWest, new Tile()),
                new KeyValuePair<CompassOrientation, Tile>(CompassOrientation.East, new Tile(){ TileType = TileWallSolidRock.GetInstance() }),
                new KeyValuePair<CompassOrientation, Tile>(CompassOrientation.SouthEast, new Tile(){ TileType = TileWallSolidRock.GetInstance() }),
            });
            var original = grid.Clone();

            // Act
            grid = grid.Rotate(RotationalOrientation.Clockwise);
            // Rotate back and test that that works too.
            grid = grid.Rotate(RotationalOrientation.Anticlockwise);

            // Assert
            Assert.IsTrue(original.All(kv => grid.TryGetValue(kv.Key, out var value) && kv.Value == value));
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

            var grid = new AdjoiningTilesGrid9(new List<KeyValuePair<CompassOrientation, Tile>> {
                new KeyValuePair<CompassOrientation, Tile>(CompassOrientation.NorthWest, t1),
                new KeyValuePair<CompassOrientation, Tile>(CompassOrientation.North,     t2),
                new KeyValuePair<CompassOrientation, Tile>(CompassOrientation.NorthEast, t3),
                new KeyValuePair<CompassOrientation, Tile>(CompassOrientation.West,      t4),
                new KeyValuePair<CompassOrientation, Tile>(CompassOrientation.None,      t5),
                new KeyValuePair<CompassOrientation, Tile>(CompassOrientation.East,      t6),
                new KeyValuePair<CompassOrientation, Tile>(CompassOrientation.SouthWest, t7),
                new KeyValuePair<CompassOrientation, Tile>(CompassOrientation.South,     t8),
                new KeyValuePair<CompassOrientation, Tile>(CompassOrientation.SouthEast, t9),
            });

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
