using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Concepts.Cosmic.Space;
using Assets.Scripts.Concepts.Gameplay.Map.TileType;
using Assets.Scripts.Concepts.Gameplay.Map.Components;
using Assets.Scripts.Concepts.Constants;
using Assets.Scripts.Concepts.Gameplay.Map.TileType;
using Assets.Scripts.Concepts.Gameplay.Map.TileType.Ground;
using Assets.Scripts.Concepts.Gameplay.Map.TileType.Wall;
using Assets.Scripts.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;

namespace RockRaiders.Tests.Concepts.Gameplay.Map.Components
{
    [TestClass]
    public class TileTests
    {
        [TestMethod]
        public void Tile_SetVertexes_ShouldSet()
        {
            var tile = new Tile() { TileType = TileWallSolidRock.GetInstance() };

            var v1 = tile.Verticies[Tile.IndexNorthWest];
            tile.SetVertexAt(CornerOrientation.NorthWest, new Vector3(v1.x, 12, v1.z));
            v1 = tile.GetVertexAt(CornerOrientation.NorthWest);
            Assert.IsTrue(v1.y == 12);
            Assert.IsTrue(v1.y == tile.Vertex0.y);
            Assert.IsTrue(v1.y == tile.VertexNorthWest.y);

            tile.Verticies = Constants.DefaultTileVerticies;

            v1 = tile.Verticies[Tile.IndexNorthEast];
            tile.SetVertexAt(CornerOrientation.NorthEast, new Vector3(v1.x, 12, v1.z));
            v1 = tile.GetVertexAt(CornerOrientation.NorthEast);
            Assert.IsTrue(v1.y == 12);
            Assert.IsTrue(v1.y == tile.Vertex1.y);
            Assert.IsTrue(v1.y == tile.VertexNorthEast.y);

            tile.Verticies = Constants.DefaultTileVerticies;

            v1 = tile.Verticies[Tile.IndexSouthEast];
            tile.SetVertexAt(CornerOrientation.SouthEast, new Vector3(v1.x, 12, v1.z));
            v1 = tile.GetVertexAt(CornerOrientation.SouthEast);
            Assert.IsTrue(v1.y == 12);
            Assert.IsTrue(v1.y == tile.Vertex2.y);
            Assert.IsTrue(v1.y == tile.VertexSouthEast.y);

            tile.Verticies = Constants.DefaultTileVerticies;

            v1 = tile.Verticies[Tile.IndexSouthWest];
            tile.SetVertexAt(CornerOrientation.SouthWest, new Vector3(v1.x, 12, v1.z));
            v1 = tile.GetVertexAt(CornerOrientation.SouthWest);
            Assert.IsTrue(v1.y == 12);
            Assert.IsTrue(v1.y == tile.Vertex3.y);
            Assert.IsTrue(v1.y == tile.VertexSouthWest.y);
        }

        [TestMethod]
        public void Tile_TestTileAverageHeight_ShouldBeCorrect()
        {
            var heights = new[] {10, 20, 30, 40};

            var tile = new Tile() { TileType = TileWallSolidRock.GetInstance() };

            tile.SetVertexAt(CornerOrientation.NorthWest, new Vector3(tile.VertexNorthWest.x, heights[0], tile.VertexNorthWest.z));
            tile.SetVertexAt(CornerOrientation.NorthEast, new Vector3(tile.VertexNorthEast.x, heights[1], tile.VertexNorthEast.z));
            tile.SetVertexAt(CornerOrientation.SouthEast, new Vector3(tile.VertexSouthEast.x, heights[2], tile.VertexSouthEast.z));
            tile.SetVertexAt(CornerOrientation.SouthWest, new Vector3(tile.VertexSouthWest.x, heights[3], tile.VertexSouthWest.z));

            Assert.AreEqual(heights.Average(), tile.AverageTileHeight);
        }
    }
}