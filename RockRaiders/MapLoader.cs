//using Assets.Scripts;
//using Assets.Scripts.Concepts.Gameplay.Map.Components;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using UnityEditor;
//using UnityEngine;

//[InitializeOnLoad]
//public class MapLoader : MonoBehaviour
//{
//    public MapLoader()
//    {
//    }

//    public Map LoadMap(string path)
//    {
//        var rawMapData = LoadMapDataFromFile(path);
//        var newMap = ParseRawMapData(rawMapData);
//    }

//    private Map ParseRawMapData(string[] rawMapData)
//    {
//        var newMap = new Map();
//        ParseCameraInformation(newMap, rawMapData);
//        ParseBasicMapData(newMap, rawMapData);
//    }

//    private void ParseBasicMapData(Map newMap, string[] rawMapData)
//    {
//        var mapDimensionsLine = rawMapData.FirstOrDefault(line => line.Contains("MAP"));
//        if (string.IsNullOrEmpty(mapDimensionsLine)) throw new MissingFieldException();
//        var mapWidth = Convert.ToInt32(mapDimensionsLine.Split('|')[2]);
//        var mapHeight = Convert.ToInt32(rawMapData.First().Split('|')[3]);
//        newMap.Tiles = new Tile[mapWidth, mapHeight];
//    }

//    private void ParseCameraInformation(Map mapData, IEnumerable<string> rawMapData)
//    {
//        if (mapData.Camera == null) mapData.Camera = new Camera();

//        var cameraLine = rawMapData.FirstOrDefault(line => line.Contains("Camera"));
//        if (string.IsNullOrEmpty(cameraLine)) return;

//        var camData = cameraLine.Split('|');
//        var xpos = Convert.ToInt32(camData[2]) / 32f;
//        var ypos = Convert.ToInt32(camData[3]) / 32f;
//        var angle = Convert.ToInt32(camData.Last());
//        var campos = new Vector3(xpos, Map.DefaultCameraHeight, ypos);

//        mapData.Camera.transform.RotateAround(campos, new Vector3(0, 1, 0), angle + 90);
//        mapData.Camera.transform.localPosition = campos;
//    }

//    public string[] LoadMapDataFromFile(string path)
//    {
//        var lines = File.ReadAllLines(path);
//        return lines.Where(line => !string.IsNullOrEmpty(line)).ToArray();
//    }
//}
