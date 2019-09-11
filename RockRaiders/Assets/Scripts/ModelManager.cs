using Assets.Scripts.Concepts.Gameplay.Map.Components;
using Assets.Scripts.Concepts.Gameplay.Map.TileType;
using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Concepts.Gameplay.Building.BuildingType;
using Assets.Scripts.Concepts.Gameplay.Map.TileType.Wall;
using Assets.Scripts.Extensions;
using UnityEngine;
using Assets.Scripts.Concepts.Gameplay.Building.Effects;

namespace Assets.Scripts
{
    public static class ModelManager
    {
        public static Dictionary<BuildingType, GameObject> BuildingModelMap { get; set; }
        public static bool IsLoaded { get; private set; }

        static ModelManager()
        {
            LoadData();
        }

        public static void LoadData()
        {
            if (IsLoaded) return;
            IsLoaded = true;
            LoadModels();
        }

        private static void LoadModels()
        {
            LoadBuildingModels();
        }

        private static void LoadBuildingModels()
        {
            foreach (var buildingType in Enum.GetValues(typeof(BuildingType)).OfType<BuildingType>())
            {
                BuildingModelMap.Add(buildingType, Resources.Load<GameObject>($"Prefabs/Buildings/{buildingType.ToString()}/{buildingType.ToString()}.prefab"));
            }
        }
    }
}