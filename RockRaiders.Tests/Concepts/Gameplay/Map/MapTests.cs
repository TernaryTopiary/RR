// ReSharper disable RedundantUsingDirective
using Assets.Scripts.Concepts.Cosmic.Space;
using Assets.Scripts.Concepts.Gameplay.Map.Components;
using Assets.Scripts.Concepts.Gameplay.Map.TileType.Ground;
using Assets.Scripts.Concepts.Gameplay.Map.TileType.Wall;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RockRaiders.Tests.Concepts.Gameplay.Map.Components;
using Assets.Scripts.Concepts.Gameplay.Map.Components;
using UnityEngine;
using Assets.Scripts;

namespace RockRaiders.Tests.Concepts.Gameplay.Map
{

    [TestClass]
    public class MapTests
    {
        public IMap Map { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Map = new FakeMap
            {
                Dimensions = new Vector2(6, 6)
            };

            Map.Tiles2D[2, 2] = new Tile { TileType = TileWallSoil.GetInstance() };
            Map.Tiles2D[3, 2] = new Tile { TileType = TileWallSolidRock.GetInstance() };
            Map.Tiles2D[5, 5] = new Tile { TileType = TileWallSolidRock.GetInstance() };
        }

        [TestMethod]
        public void Map_IsValidPosition_ShouldBeTrue()
        {
            Assert.IsTrue(Map.IsValidPosition(new Vector2(2, 2)));
        }

        [TestMethod]
        public void Map_IsValidPosition_ShouldBeFalse()
        {
            Assert.IsFalse(Map.IsValidPosition(new Vector2(-1, 2)));
            Assert.IsFalse(Map.IsValidPosition(new Vector2(6, 2)));
        }

        [TestMethod]
        public void Map_GetTile_ShouldGet()
        {
            var tile = Map.GetTileAtPosition(new Vector2(2, 2), true);
            Assert.IsNotNull(tile);
        }

        [TestMethod]
        public void Map_GetTilePosition_ShouldGet()
        {
            var sourcePosition = new Vector2(2, 2);
            var tile = Map.GetTileAtPosition(sourcePosition, true);
            var position = Map.GetPosition(tile);
            Assert.AreEqual(sourcePosition, position);
        }

        [TestMethod]
        public void Map_GetTile_ShouldOverflowAndFail()
        {
            try
            {
                Assert.ThrowsException<ArgumentException>(() =>
                {
                    return Map.GetTileAtPosition(new Vector2(12, 12), true);
                });
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void Map_GetTile_ShouldOverflowAndPass()
        {
            Assert.IsNull(Map.GetTileAtPosition(new Vector2(12, 12), false));
        }

        [TestMethod]
        public void Map_GetNeighboringTile_ShouldGet()
        {
            var tile = Map.GetTileAtPosition(new Vector2(2, 2), true);
            var neighbor = Map.GetNeighboringTile(tile, CompassOrientation.East, true);
            Assert.IsNotNull(neighbor);
        }

        [TestMethod]
        public void Map_GetNeighboringTile_ShouldOverflowAndFail()
        {
            var tile = Map.GetTileAtPosition(new Vector2(5, 5), true);
            try
            {
                Assert.ThrowsException<ArgumentException>(() =>
                {
                    return Map.GetNeighboringTile(tile, CompassOrientation.East, true);
                });
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void Map_GetNeighboringTile_ShouldOverflowAndPass()
        {
            var tile = Map.GetTileAtPosition(new Vector2(5, 5), true);
            var neighbor = Map.GetNeighboringTile(tile, CompassOrientation.East, false);
        }

        [TestMethod]
        public void Map_GetTileConfiguration_ShouldBeValid()
        {
            Tile GetGroundTile()
            {
                return new Tile() { TileType = TileTypeGroundSoil.GetInstance() };
            }

            Tile GetWallTile()
            {
                return new Tile() { TileType = TileWallSolidRock.GetInstance() };
            }

            void EraseMap()
            {
                for (var x = 0; x < Map.Dimensions.x; x++)
                {
                    for (var y = 0; y < Map.Dimensions.y; y++)
                    {
                        Map.Tiles2D[x, y] = GetWallTile();
                        Assert.IsTrue(Map.Tiles2D[x, y].TileType == TileWallSolidRock.GetInstance());
                    }
                }
            }

            var target = new Vector2(1, 1);
            foreach (var kv in WellKnownTileFormations.WallConfigurationLayoutMap)
            {
                EraseMap();
                foreach (var orientationList in kv.Value)
                {
                    foreach (var orientation in orientationList)
                    {
                        var offset = orientation.ToOffsetVector2();
                        var location = target + offset;
                        Map.Tiles2D[(int)location.x, (int)location.y] = GetGroundTile();
                        Assert.IsTrue(Map.Tiles2D[(int)location.x, (int)location.y].TileType == TileTypeGroundSoil.GetInstance());
                    }
                    var config = Map.GetTileConfiguration(target, out var QuadOrientationasf);
                }
            }

            // TODO: Generate some real-world examples and test the algorithm with it, not just the training set.
        }

        [TestMethod]
        public void Map_SetTileVertexHeights_ShouldBeValid()
        {
            var t1 = Map.Tiles2D[0, 0] = new Tile { OriginalTileHeight = 2 };
            var t2 = Map.Tiles2D[1, 0] = new Tile { OriginalTileHeight = 3 };
            var t3 = Map.Tiles2D[2, 0] = new Tile { OriginalTileHeight = 3 };
            var t4 = Map.Tiles2D[0, 1] = new Tile { OriginalTileHeight = 1 };
            var t5 = Map.Tiles2D[1, 1] = new Tile { OriginalTileHeight = 1 };
            var t6 = Map.Tiles2D[2, 1] = new Tile { OriginalTileHeight = 1 };
            var t7 = Map.Tiles2D[0, 2] = new Tile { OriginalTileHeight = 2 };
            var t8 = Map.Tiles2D[1, 2] = new Tile { OriginalTileHeight = 3 };
            var t9 = Map.Tiles2D[2, 2] = new Tile { OriginalTileHeight = 3 };
            var neighbors = Map.GetNeighboringTiles(new Vector2(1, 1));
            neighbors.Center.SetVertexHeightsFromNeighbors(neighbors);

            var heights = new[] {t1.OriginalTileHeight, t2.OriginalTileHeight, t4.OriginalTileHeight, t5.OriginalTileHeight};
            var sum = heights[0] + heights[1] + heights[2] + heights[3];
            var avg = sum / 4f;
            var expectedQuadHeightAverage = avg;
            Assert.Equals(t5.GetVertexAt(CornerOrientation.NorthWest).y, expectedQuadHeightAverage);


            //neighbors = Map.GetNeighboringTiles(new Vector2(1, 0));
            //neighbors.Center.SetVertexHeightsFromNeighbors(neighbors);
            //neighbors = Map.GetNeighboringTiles(new Vector2(0, 1));
            //neighbors.Center.SetVertexHeightsFromNeighbors(neighbors);
            //neighbors = Map.GetNeighboringTiles(new Vector2(1, 1));
            //neighbors.Center.SetVertexHeightsFromNeighbors(neighbors);

            //var cornerQuad = new AdjoiningTilesGrid4(new Tile[] { t1, t2, t4, t5 });
            //var averageCornerTileHeight = cornerQuad.Tiles.Select(t => t?.OriginalTileHeight).Where(h => h.HasValue).Average(h => h.Value);

            //cornerQuad = new AdjoiningTilesGrid4(new Tile[] { t5, t6, t8, t9 });
            //averageCornerTileHeight = cornerQuad.Tiles.Select(t => t?.OriginalTileHeight).Where(h => h.HasValue).Average(h => h.Value);
            //// TODO: WHY DO THEY NOT AGREE
        }
    }
}