using Assets.Scripts.Concepts.Gameplay.Map.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Scripts.Concepts.Gameplay.Map.TileType;
using Assets.Scripts.Extensions;
using UnityEditor;
using UnityEngine;
using Assets.Scripts.Concepts.Constants;
using Assets.Scripts.Concepts.Gameplay.UI.Camera;

namespace Assets.Scripts
{
    [InitializeOnLoad]
    public class MapLoader : MonoBehaviour
    {
        public Camera Camera;

        private string MapFilePath = "F:\\Program Files (x86)\\Lego Rock Raiders\\LegoRR0\\Levels\\level12_waterworks.mcm";

        private const byte IndexTileType = 1;
        private const byte IndexTileHeight = 4;
        public float TileHeightScaleFactor { get; private set; } = 0.1f;

        public MapLoader()
        {
        }

        IEnumerator Start()
        {
            var loadscreen = GameObject.Find("LoadingScreen");
            var loadrect = loadscreen.GetComponent<RectTransform>();
            loadrect.anchoredPosition = Vector2.zero;
            loadscreen.SetActive(true);

            MaterialManager.LoadData();
            PrefabManager.LoadData();
            AudioManager.LoadData();

            try
            {
                LoadMap(MapFilePath);
            }
            catch (Exception e)
            {
                Debug.Log("Failed to load map.");
            }

            loadscreen.SetActive(false);
            return null;

        }

        public Map LoadMap(string path)
        {
            var rawMapData = !string.IsNullOrEmpty(path) ? LoadMapDataFromFile(path) : GenerateBlankMap();
            var newMap = ParseRawMapData(rawMapData);
            return newMap;
        }

        private string[] GenerateBlankMap()
        {
            throw new NotImplementedException();
        }

        private Map ParseRawMapData(string[] rawMapData)
        {
            var newMap = Map.GetInstance();
            InitializeObjective(newMap, rawMapData);
            ParseBasicMapData(newMap, rawMapData);
            InitializeCamera(newMap, rawMapData);
            return newMap;
        }

        private void InitializeObjective(Map newMap, string[] rawMapData)
        {
            var mapObjectiveLine = rawMapData.FirstOrDefault(line => line.Contains("OBJECTIVE"));
            if (string.IsNullOrEmpty(mapObjectiveLine)) throw new MissingFieldException("Map objective not defined.");
        }

        private void ParseBasicMapData(Map newMap, string[] rawMapData)
        {
            var mapDimensionsLine = rawMapData.FirstOrDefault(line => line.Contains("MAP"));
            if (string.IsNullOrEmpty(mapDimensionsLine)) throw new MissingFieldException("Map dimensions not defined.");
            var mapWidth = Convert.ToInt32(mapDimensionsLine.Split('|')[2]);
            var mapHeight = Convert.ToInt32(rawMapData.First().Split('|')[3]);
            newMap.Dimensions = new Vector2(mapWidth, mapHeight);

            var mapTileData = rawMapData.Where(line => line.StartsWith("BLOCK")).Select(line => line.Split('|')).ToList();
            if (mapTileData.Count != newMap.Dimensions.x * newMap.Dimensions.y) throw new MissingFieldException("Insufficient tiles for map dimensions.");
            var minimumHeight = mapTileData.Min(tile => int.Parse(tile[4]));

            InitializeMapTiles(newMap, mapTileData);
        }

        private void InitializeMapTiles(Map newMap, List<string[]> mapTileData)
        {
            int x = 0, y = (int)newMap.Dimensions.y - 1;
            var biome = TileBiomeRock.GetInstance();

            foreach (var tileData in mapTileData)
            {
                try
                {
                    var tile = InitializeMapTile(tileData);
                    tile.SetTileLocation(new Vector2(x, y));

                    // TODO: REMOVE AND READ FROM FILE
                    tile.TileType.Biome = biome;
                    newMap.Tiles2D[x, y] = tile;
                }
                catch (Exception exception)
                {
                    throw new MapImportException($"Map import error at tile ({x}, {y})", exception);
                }
                x++;
                if (x != newMap.Dimensions.x) continue;
                x = 0;
                y--;
            }

            newMap.CalculateTileConfigurations();
            newMap.CalculateTileHeights();
            
            var tileDictionary = newMap.GenerateTileGameObjects(biome);

            Debug.Log($"Loaded {tileDictionary.Values.Sum(list => list.Count)} tiles.");
        }

        private Tile InitializeMapTile(string[] tileData)
        {
            var tile = new Tile();
            try
            {
                int encodedTileType;
                if(!int.TryParse(tileData[IndexTileType], out encodedTileType)) throw new MapImportException("Invalid tile type: failed to parse encoded tile type representation. Encoded as {tileData[IndexTileType]}");
                var tileType = (TileTypeImportMap)encodedTileType;
                tile.TileType = tileType.ToTileType();

                float encodedTileHeight;
                if (!float.TryParse(tileData[IndexTileHeight], out encodedTileHeight)) throw new MapImportException("Invalid tile height: failed to parse encoded tile height representation. Encoded as {tileData[IndexTileHeight]}");
                encodedTileHeight = encodedTileHeight * TileHeightScaleFactor;
                tile.OriginalTileHeight = encodedTileHeight;

                return tile;
            }
            catch (Exception exception)
            {
                throw new MapImportException($"Invalid tile definition.", exception);
            }
        }

        private void InitializeCamera(Map mapData, IEnumerable<string> rawMapData)
        {
            var cameraLine = rawMapData.FirstOrDefault(line => line.Contains("Camera"));
            if (string.IsNullOrEmpty(cameraLine)) return;

            var camData = cameraLine.Split('|');
            var xpos = Convert.ToInt32(camData[2]) / 32f;
            var zpos = (mapData.Dimensions.y * Constants.TileScale) - (Convert.ToInt32(camData[3]) / 32f);       // RR y pos starts from top of map.
            var angle = Convert.ToInt32(camData.Last()) - 90;   // RR angles start 90 degrees away.
            var campos = new Vector3(xpos, Map.DefaultCameraHeight, zpos);

            // If the camera isn't overridden, look it up.
            if (Camera.main != null && Camera == null) Camera = Camera.main;
            if (Camera != null)
            {
                Camera.transform.localPosition = campos;
                CameraManager.GetInstance().YRotationAngle = angle;
            }
            else Debug.Log("Error: Unable to get handle to main camera!");
        }

        public string[] LoadMapDataFromFile(string path)
        {
            var lines = File.ReadAllLines(path);
            return lines.Where(line => !string.IsNullOrEmpty(line)).ToArray();
        }
    }
}