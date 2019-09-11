using System.Collections.Generic;
using Assets.Scripts.Concepts.Cosmic.Space;
using Assets.Scripts.Concepts.Gameplay.Map.Components;
using Assets.Scripts.Concepts.Gameplay.Map.TileType;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IMap
    {
        KeyValuePair<Tile, GameObject> this[Vector2 position] { get; }

        Camera Camera { get; set; }
        Vector2 Dimensions { get; set; }
        GameObject[,] TileGameObjects2D { get; }
        Tile[,] Tiles2D { get; }

        void CalculateTileConfigurations();
        void CalculateTileHeights();
        Dictionary<GameObject, List<GameObject>> GenerateTileGameObjects(ITileBiome biome);
        GameObject GetGameObjectAtPosition(Vector2 position, bool throwIfOverflow);
        Tile GetNeighboringTile(Tile tile, CompassOrientation offset, bool throwIfOverflow);
        AdjoiningTilesGrid9 GetNeighboringTiles(Vector2 position);
        Vector2 GetPosition(Tile tile);
        Tile GetTileAtPosition(Vector2 position, bool throwIfOverflow);
        TileConfiguration GetTileConfiguration(Vector2 position, out CompassAxisOrientation? orientation);
        bool IsValidPosition(Vector2 position);
    }
}