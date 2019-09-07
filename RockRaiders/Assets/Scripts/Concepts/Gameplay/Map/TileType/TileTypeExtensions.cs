using System;
using System.Collections.Generic;
using Assets.Scripts.Concepts.Gameplay.Map.TileType.Ground;
using Assets.Scripts.Concepts.Gameplay.Map.TileType.Wall;
using Assets.Scripts.Concepts.Gameplay.Shared;

namespace Assets.Scripts.Concepts.Gameplay.Map.TileType
{
    public interface ITileType : ITooltipInformationDisplayable
    {
        ITileBiome Biome { get; set; }
    }

    public interface ITileTypeWall : ITileTypeSemiTraversable
    {
        /// <summary>
        /// Base name for the material. If the material name is ROCK02, this would be "2".
        /// </summary>
        string MaterialBaseName { get; }
        
        /// <summary>
        /// Naming prefix for wall texture. If the material name is ROCK02, this would be "0".
        /// </summary>
        string WallMaterialName { get; }

        /// <summary>
        /// Naming prefix for internal corner texture (three vertexes at top, one at bottom). If the material name is ROCK32, this would be "3".
        /// </summary>
        string InternalCornerMaterialName { get; }

        /// <summary>
        /// Naming prefix for external corner texture (three vertexes at bottom, one at top). If the material name is ROCK22, this would be "5".
        /// </summary>
        string ExternalCornerMaterialName { get; }

        /// <summary>
        /// Naming prefix for ceiling texture.
        /// </summary>
        string CeilingMaterialName { get; }
    }

    public interface ITileTypeWallReinforcable : ITileTypeWall
    {
        /// <summary>
        /// Naming prefix for reinforcement texture. If the material name is ROCK21, this would be "2".
        /// </summary>
        string ReinforcementMaterialName { get; }
    }

    public interface ITileTypeWallDamageable : ITileTypeWall, ISelectable, IDamageable
    {
    }

    public interface ITileTypeWallLaserable : ITileTypeWallDamageable
    {
    }

    public interface ITileTypeWallDynamitable : ITileTypeWallDamageable
    {
    }

    public interface ITileTypeTraversable : ITileType
    {
    }

    public interface ITileTypeSemiTraversable : ITileType
    {
    }

    public interface ITileTypeBuildable : ITileType
    {
    }

    public interface ITileTypeGround : ITileType
    {
        string MaterialBaseName { get; }
    }

    public interface ITileTypeSolidGround : ITileTypeGround, ITileTypeTraversable
    {
        bool CanBeEroded { get; set; }
    }

    public interface ITileTypeLiquidGround : ITileTypeGround
    {
    }

    public interface ITileTypeDamagingGround : ITileTypeGround
    {
    }

    public interface ITileTypeWallDrillable : ITileTypeWallReinforcable, ITileTypeWallDamageable
    {
    }

    public interface ITileTypeWallLightDrillable : ITileTypeTypeWallHeavyDrillable
    {
    }

    public interface ITileTypeTypeWallHeavyDrillable : ITileTypeWallDrillable, ITileTypeWallLaserable, ITileTypeWallDynamitable
    {
    }

    public static class TileTypeExtensions
    {
        public static Dictionary<TileTypeImportMap, ITileType> TileTypeMap = new Dictionary<TileTypeImportMap, ITileType>
        {
            { TileTypeImportMap.Soil, new TileTypeGroundSoil()},
            { TileTypeImportMap.Dirt, new TileWallDirt()},
            { TileTypeImportMap.Loose, new TileWallLooseRock()},
            { TileTypeImportMap.Hard, new TileWallHardRock()},
            { TileTypeImportMap.Solid, new TileWallSolidRock()},
            { TileTypeImportMap.EnergySeam, new TileWallEnergyCrystalSeam()},
            { TileTypeImportMap.RegeneratorSeam, new TileWallEnergyCrystalRegeneratorSeam()},
            { TileTypeImportMap.Lava, new TileGroundLava()},
            { TileTypeImportMap.Water, new TileGroundWater()},
            { TileTypeImportMap.SlugHole, new TileGroundSlimySlugHole()},
            { TileTypeImportMap.OreSeam, new TileWallOreSeam()}
        };

        public static ITileType ToTileType(this TileTypeImportMap value) => TileTypeMap.ContainsKey(value) ? TileTypeMap[value] : new TileTypeGroundSoil();
    }
}