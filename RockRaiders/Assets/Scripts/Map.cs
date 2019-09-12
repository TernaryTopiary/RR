using Assets.Scripts.Concepts.Cosmic.Space;
using Assets.Scripts.Concepts.Gameplay.Map.Components;
using Assets.Scripts.Concepts.Gameplay.Map.TileType;
using Assets.Scripts.Concepts.Gameplay.Map.TileType.Wall;
using System;
using Assets.Scripts.Extensions;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Concepts.Constants;
using Assets.Scripts.Concepts.Cosmic.Array;
using Assets.Scripts.Concepts.Gameplay.Building.BuildingType;
using Assets.Scripts.Concepts.Gameplay.Building.Components;
using Assets.Scripts.Miscellaneous;
using UnityEngine;

namespace Assets.Scripts
{
    public class Map : Singleton<Map>, IMap
    {
        private Vector2 _dimensions;
        public MapInteractor MapInteractor { get; set; }
        public Camera Camera { get; set; }
        public static float DefaultCameraHeight { get; set; } = 3.0f;
        public Tile[,] Tiles2D { get; private set; }
        public GameObject[,] TileGameObjects2D { get; private set; }
        public BuildingNodeInstantiation[,] BuildingMap { get; private set; }
        
        public Vector2 Dimensions
        {
            get { return _dimensions; }
            set
            {
                _dimensions = value;
                Tiles2D = new Tile[(int)value.x, (int)value.y];
                TileGameObjects2D = new GameObject[(int)value.x, (int)value.y];
                BuildingMap = new BuildingNodeInstantiation[(int)value.x, (int)value.y];
            }
        }

        public Vector2 GetPosition(Tile tile)
        {
            for (var x = 0; x < _dimensions.x; ++x)
            {
                for (var y = 0; y < _dimensions.y; ++y)
                {
                    if (Tiles2D[x, y]?.Equals(tile) == true) return new Vector2(x, y);
                }
            }

            throw new KeyNotFoundException("Could not find tile in tile map.");
        }

        public Vector2 GetPosition(GameObject gameObject)
        {
            for (var x = 0; x < _dimensions.x; ++x)
            {
                for (var y = 0; y < _dimensions.y; ++y)
                {
                    if (TileGameObjects2D[x, y]?.Equals(gameObject) == true) return new Vector2(x, y);
                }
            }

            throw new KeyNotFoundException("Could not find tile in tile map.");
        }

        public bool IsValidPosition(Vector2 position)
        {
            return _dimensions.x > position.x &&
                   _dimensions.y > position.y &&
                   position.x >= 0 &&
                   position.y >= 0;
        }

        public TileConfiguration GetTileConfiguration(Vector2 position, out CompassAxisOrientation? orientation)
        {
            if (!IsValidPosition(position)) throw new ArgumentException("Invalid position on map.");
            var neighboringTiles = GetNeighboringTiles(position);

            var primaryCompassPointTiles = new[]
            {
                neighboringTiles[CompassOrientation.North],
                neighboringTiles[CompassOrientation.East],
                neighboringTiles[CompassOrientation.South],
                neighboringTiles[CompassOrientation.West]
            };

            orientation = null;
            if (neighboringTiles.Center.IsGround) return TileConfiguration.Ground;
            if (neighboringTiles.Adjoining.All(t => t.IsWall)) return TileConfiguration.Ceiling;

            var formations = WellKnownTileFormations.WallConfigurationLayoutMap.SelectMany(kv =>
                kv.Value.Select(formation => new { TileConfiguration = kv.Key, Formation = formation.Clone() as CompassOrientation[] })).OrderByDescending(kv => kv.Formation.Length).ToArray();

            var bestMatchingFormation = formations.FirstOrDefault(); // take the type.
            bestMatchingFormation = null;
            var bestMatchingOrientation = CompassAxisOrientation.South;
            var currentOrientation = CompassAxisOrientation.South;

            for (var i = 0; i < 4; i++)
            {
                var matchingFormation = formations.FirstOrDefault(formation => neighboringTiles.SubsetMeetsCriteria(tile => tile.IsGround, formation.Formation));
                if (matchingFormation != null && matchingFormation.Formation.Length >= WellKnownTileFormations.UnambiguityLimit)
                {
                    orientation = bestMatchingOrientation = currentOrientation;
                    return matchingFormation.TileConfiguration;
                }
                if (matchingFormation != null && (bestMatchingFormation == null || matchingFormation.Formation.Length > bestMatchingFormation.Formation.Length))
                {
                    bestMatchingFormation = matchingFormation;
                    bestMatchingOrientation = currentOrientation;
                }

                foreach(var pair in formations)
                {
                    for (int index = 0; index < pair.Formation.Length; index++)
                    {
                        pair.Formation[index] = pair.Formation[index].Rotate(RotationalOrientation.Clockwise).Rotate(RotationalOrientation.Clockwise);
                    }
                }

                currentOrientation = currentOrientation.Rotate(RotationalOrientation.Clockwise);
            }

            orientation = bestMatchingOrientation;
            return bestMatchingFormation?.TileConfiguration ?? throw new ArgumentException();
        }

        public KeyValuePair<Tile, GameObject> this[Vector2 position]
        {
            get
            {
                return new KeyValuePair<Tile, GameObject>(GetTileAtPosition(position, false), GetGameObjectAtPosition(position, false));
            }
        }

        public Tile GetTileAtPosition(Vector2 position, bool throwIfOverflow)
        {
            if (IsValidPosition(position)) return Tiles2D[(int)position.x, (int)position.y];
            if (throwIfOverflow) throw new ArgumentException("Invalid position on map.");
            else return new Tile { TileType = TileWallSolidRock.GetInstance() }; //null; Changed to make safe.
        }

        public GameObject GetGameObjectAtPosition(Vector2 position, bool throwIfOverflow)
        {
            if (IsValidPosition(position)) return TileGameObjects2D[(int)position.x, (int)position.y];
            if (throwIfOverflow) throw new ArgumentException("Invalid position on map.");
            else return null;
        }

        public Tile GetNeighboringTile(Tile tile, CompassOrientation offset, bool throwIfOverflow)
        {
            var position = GetPosition(tile);
            var positionOffset = offset.ToOffsetVector2();
            var adjustedPosition = position + positionOffset;
            return GetTileAtPosition(adjustedPosition, throwIfOverflow);
        }

        public AdjoiningTilesGrid9 GetNeighboringTiles(Vector2 position)
        {
            var orientations = Enum.GetValues(typeof(CompassOrientation)).OfType<CompassOrientation>().ToList();
            var offsets = orientations.Select(o => new {Orientation = o, Offset = o.ToOffsetVector2()}).ToList();
            return new AdjoiningTilesGrid9(offsets.ToDictionary(pair => pair.Orientation, pair => GetTileAtPosition(position + pair.Offset, false)));
        }

        public void CalculateTileConfigurations()
        {
            for (var x = 0; x < _dimensions.x; ++x)
            {
                for (var y = 0; y < _dimensions.y; ++y)
                {
                    Tiles2D[x, y].Configuration = GetTileConfiguration(new Vector2(x, y), out var orientation);
                    Tiles2D[x, y].Orientation = orientation;
                }
            }
        }

        public Dictionary<GameObject, List<GameObject>> GenerateTileGameObjects(ITileBiome biome)
        {
            // Keep the hierarchy tidy.
            var map = GameObject.Find("Map");
            MapInteractor = map.GetComponent<MapInteractor>();
            var animator = map.AddComponent<MaterialAnimator>();
            animator.Biome = biome;
            var mapTiles = new GameObject() { name = "Tiles" };
            mapTiles.transform.parent = map.transform;

            var tileDictionary = new Dictionary<GameObject, List<GameObject>>();

            for (var x = 0; x < _dimensions.x; ++x)
            {
                var row = new GameObject() { name = $"Row_({x})" };
                row.transform.parent = mapTiles.transform;
                var rowTiles = new List<GameObject>();
                tileDictionary[row] = rowTiles;

                for (var y = 0; y < _dimensions.y; ++y)
                {
                    var gameObj = Tiles2D[x, y].ToGameObject(MapInteractor);
                    gameObj.name += $"({x},{y})";
                    gameObj.transform.parent = row.transform;
                    TileGameObjects2D[x, y] = gameObj;
                    rowTiles.Add(TileGameObjects2D[x, y]);
                }
            }

            Extents = new Bounds()
            {
                min = Tiles2D[0,0].GetVertexAt(CornerOrientation.NorthWest),
                max = Tiles2D[(int)Dimensions.x - 1, (int)Dimensions.y - 1].GetVertexAt(CornerOrientation.SouthEast)
            };

            return tileDictionary;
        }

        public Bounds Extents { get; set; }

        public void CalculateTileHeights()
        {
            // Mesh the tiles.
            for (var x = 0; x < _dimensions.x; ++x)
            {
                for (var y = 0; y < _dimensions.y; ++y)
                {
                    var tile = Tiles2D[x, y];
                    var neighbors = GetNeighboringTiles(new Vector2(x, y));

                    tile.SetVertexHeightsFromNeighbors(neighbors);
                }
            }
        }

        public static void DrawTextAtLocation(string str, Vector3 position, Color? color = null)
        {
            var textObject = new GameObject();
            textObject.transform.position = position;

            var textMesh = textObject.AddComponent<TextMesh>();
            textMesh.text = str;
            textMesh.characterSize = .05f;
            if (color.HasValue) textMesh.color = color.Value;
        }
    }
}