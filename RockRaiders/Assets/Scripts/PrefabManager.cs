using Assets.Scripts.Concepts.Gameplay.Building.BuildingType;
using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Concepts.Cosmic.Array;
using Assets.Scripts.Concepts.Cosmic.Space;
using UnityEngine;

namespace Assets.Scripts
{
    public static class PrefabManager
    {
        public static Dictionary<BuildingType, AdjoiningGrid9<GameObject>> BuildingModelMap { get; set; } = new Dictionary<BuildingType,AdjoiningGrid9<GameObject>>();
        public static bool IsLoaded { get; private set; }

        static PrefabManager()
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
            BuildingModelMap.Add(BuildingType.ToolStore, new AdjoiningGrid9<GameObject>
            {
                {CompassOrientation.None, Resources.Load<GameObject>($"Prefabs/Buildings/{BuildingType.ToolStore}/{BuildingType.ToolStore}") }
            });
            BuildingModelMap.Add(BuildingType.TeleportPad, new AdjoiningGrid9<GameObject>
            {
                {CompassOrientation.None, Resources.Load<GameObject>($"Prefabs/Buildings/{BuildingType.TeleportPad}/{BuildingType.TeleportPad}") }
            });
        }
    }
}