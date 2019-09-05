// ReSharper disable RedundantUsingDirective
using Assets.Scripts.Concepts.Cosmic.Space;
using Assets.Scripts.Concepts.Gameplay.Map.Components;
using System;
using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.Concepts.Gameplay.Map.TileType;
using System.Collections.Generic;

namespace RockRaiders.Tests.Concepts.Gameplay.Map
{
    public class FakeMap : IMap
    {
        public KeyValuePair<Tile, GameObject> this[Vector2 position] => throw new NotImplementedException();

        public Camera Camera { get; set; }
        public Vector2 Dimensions { get; set; }

        public GameObject[,] TileGameObjects2D { get; }

        public Tile[,] Tiles2D { get; }

        public void CalculateTileConfigurations()
        {
            throw new NotImplementedException();
        }

        public void CalculateTileHeights()
        {
            throw new NotImplementedException();
        }

        public Dictionary<GameObject, List<GameObject>> GenerateTileGameObjects()
        {
            throw new NotImplementedException();
        }

        public GameObject GetGameObjectAtPosition(Vector2 position, bool throwIfOverflow)
        {
            throw new NotImplementedException();
        }

        public Tile GetNeighboringTile(Tile tile, CompassOrientation offset, bool throwIfOverflow)
        {
            throw new NotImplementedException();
        }

        public AdjoiningTilesGrid9 GetNeighboringTiles(Vector2 position)
        {
            throw new NotImplementedException();
        }

        public Vector2 GetPosition(Tile tile)
        {
            throw new NotImplementedException();
        }

        public Tile GetTileAtPosition(Vector2 position, bool throwIfOverflow)
        {
            throw new NotImplementedException();
        }

        public TileConfiguration GetTileConfiguration(Vector2 position, out CornerOrientation? orientation)
        {
            throw new NotImplementedException();
        }

        public bool IsValidPosition(Vector2 position)
        {
            throw new NotImplementedException();
        }
    }
}