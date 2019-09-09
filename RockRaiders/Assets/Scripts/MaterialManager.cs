using Assets.Scripts.Concepts.Gameplay.Map.Components;
using Assets.Scripts.Concepts.Gameplay.Map.TileType;
using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Concepts.Gameplay.Map.TileType.Wall;
using Assets.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts
{
    public static class MaterialManager
    {
        public static class Constants
        {
            public static class Gameplay
            {
                public static class UI
                {
                    public static Texture2D CursorDefault;
                }

                public static class Map
                {
                    public static Material TintSelected;
                    public static Material TintMine;
                    public static Material TintReinforce;
                    public static Material TintDynamite;
                    public static Material TintBuildingFoundationPlacementMaterial;
                    public static Material TintBuildingHoverPlacementMaterial;
                    public static Material TintBuildingPlacementDeniedMaterial;
                }

                public static class Debug
                {
                    public static bool ShowDebugAnnotations;
                }
            }
        }

        public static Dictionary<TileBiome, Dictionary<string, Material>> TileBiomeMaterialMap { get; set; }

        static MaterialManager()
        {
            LoadData();
        }

        public static void LoadData()
        {
            LoadBiomeMaterials();
            LoadTextures();
            LoadMiscellaneousMaterials();
        }

        private static void LoadTextures()
        {
            Constants.Gameplay.UI.CursorDefault = Resources.Load("Textures/Interface/Pointers/Aclosed") as Texture2D;
        }

        private static void LoadMiscellaneousMaterials()
        {
            if (Constants.Gameplay.Map.TintBuildingFoundationPlacementMaterial != null) UnloadMiscellaneousMaterialData();
            Constants.Gameplay.Map.TintBuildingFoundationPlacementMaterial = Resources.Load("Materials/BuildingFoundationPlacementHover") as Material;
            Constants.Gameplay.Map.TintBuildingHoverPlacementMaterial = Resources.Load("Materials/BuildingPlacementHover") as Material;
            Constants.Gameplay.Map.TintBuildingPlacementDeniedMaterial = Resources.Load("Materials/BuildingPlacementDeniedHover") as Material;
            Constants.Gameplay.Map.TintSelected = Resources.Load("Materials/TintColorSelected") as Material;
            Constants.Gameplay.Map.TintMine = Resources.Load("Materials/TintColorMineQueue") as Material;
            Constants.Gameplay.Map.TintReinforce = Resources.Load("Materials/TintColorReinforce") as Material;
            Constants.Gameplay.Map.TintDynamite = Resources.Load("Materials/TintColorDynamite") as Material;
        }

        private static void UnloadMiscellaneousMaterialData()
        {
            new[]
            {
                Constants.Gameplay.Map.TintBuildingFoundationPlacementMaterial,
                Constants.Gameplay.Map.TintBuildingHoverPlacementMaterial,
                Constants.Gameplay.Map.TintBuildingPlacementDeniedMaterial,
                Constants.Gameplay.Map.TintSelected,
                Constants.Gameplay.Map.TintMine,
                Constants.Gameplay.Map.TintReinforce,
                Constants.Gameplay.Map.TintDynamite
            }.Where(m => m != null).ForEach(Resources.UnloadAsset);
        }

        private static void LoadBiomeMaterials()
        {
            if (TileBiomeMaterialMap != null) UnloadBiomeMaterialData();

            TileBiomeMaterialMap = new Dictionary<TileBiome, Dictionary<string, Material>>();
            foreach (var tileBiome in Enum.GetValues(typeof(TileBiome)).OfType<TileBiome>())
            {
                TileBiomeMaterialMap.Add(tileBiome, Resources.LoadAll($"Materials/Biomes/{tileBiome.ToString().ToUpper()}/").OfType<Material>().ToDictionary(m => m.name, m => m));
            }
        }

        private static void UnloadBiomeMaterialData()
        {
            foreach (var map in TileBiomeMaterialMap) foreach (var material in map.Value.Values) Resources.UnloadAsset(material);
            TileBiomeMaterialMap = null;
        }

        public static Material GetMaterialForTile(Tile tile)
        {
            var materialName = string.Empty;
            if (tile.TileType is ITileTypeGround tileTypeGround)
            {
                materialName = $"{tileTypeGround.Biome.BiomeNameReference}{tileTypeGround.MaterialBaseName}";
            }
            if (tile.TileType is ITileTypeWall tileTypeWall)
            {
                var tileWallState = tile.Configuration;
                switch (tileWallState)
                {
                    case TileConfiguration.Wall:
                        {
                            // TODO: DETERMINE IF WALL IS ACTUALLY REINFORCED OR NOT.
                            if (tile.TileType is ITileTypeWallReinforcable tileTypeWallReinforcable && false)
                            {
                                materialName = $"{tileTypeWall.Biome.BiomeNameReference}{tileTypeWallReinforcable.ReinforcementMaterialName}{tileTypeWall.MaterialBaseName}";
                            }
                            else
                            {
                                materialName = $"{tileTypeWall.Biome.BiomeNameReference}{tileTypeWall.WallMaterialName}{tileTypeWall.MaterialBaseName}";
                            }
                            break;
                        }
                    case TileConfiguration.InternalCorner:
                        materialName = $"{tileTypeWall.Biome.BiomeNameReference}{tileTypeWall.InternalCornerMaterialName}{tileTypeWall.MaterialBaseName}";
                        break;

                    case TileConfiguration.ExternalCorner:
                        materialName = $"{tileTypeWall.Biome.BiomeNameReference}{tileTypeWall.ExternalCornerMaterialName}{tileTypeWall.MaterialBaseName}";
                        break;
                    case TileConfiguration.Ceiling:
                        materialName = $"{tileTypeWall.Biome.BiomeNameReference}{tileTypeWall.CeilingMaterialName}";
                        break;
                    case TileConfiguration.Ground:
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            if(!TileBiomeMaterialMap.TryGetValue(tile.TileType.Biome.BiomeReference, out var materialDictionary))
                throw new KeyNotFoundException($"Unable to find biome \"{tile.TileType?.Biome?.BiomeReference}\" in biome material map.");
            if (string.IsNullOrEmpty(materialName))
                throw new ArgumentException("Unable to derive material name from tile.");
            if (string.IsNullOrEmpty(materialName) || !materialDictionary.TryGetValue(materialName, out var material))
                throw new KeyNotFoundException($"Unable to find matching material for name \"{materialName}\"");
            return material;
        }

        public static Material GetMaterial(this Tile tile)
        {
            return MaterialManager.GetMaterialForTile(tile);
        }
    }
}