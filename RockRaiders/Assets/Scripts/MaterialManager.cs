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
        public static Material HighlightBuildingFoundationPlacementMaterial { get; set; }

        public static Material HighlightBuildingHoverPlacementMaterial { get; set; }

        public static Material HighlightBuildingPlacementDeniedMaterial { get; set; }

        public static Dictionary<TileBiome, Dictionary<string, Material>> TileBiomeMaterialMap { get; set; }

        static MaterialManager()
        {
            LoadData();
        }

        public static void LoadData()
        {
            LoadBiomeMaterials();
            LoadMiscellaneousMaterials();
        }

        private static void LoadMiscellaneousMaterials()
        {
            if (HighlightBuildingFoundationPlacementMaterial != null) UnloadMiscellaneousMaterialData();

            HighlightBuildingFoundationPlacementMaterial = Resources.Load("Materials/BuildingFoundationPlacementHover") as Material;
            HighlightBuildingHoverPlacementMaterial = Resources.Load("Materials/BuildingPlacementHover") as Material;
            HighlightBuildingPlacementDeniedMaterial = Resources.Load("Materials/BuildingPlacementDeniedHover") as Material;
        }

        private static void UnloadMiscellaneousMaterialData()
        {
            new[]
            {
                HighlightBuildingFoundationPlacementMaterial,
                HighlightBuildingHoverPlacementMaterial,
                HighlightBuildingPlacementDeniedMaterial
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
    }
}