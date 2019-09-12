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

    public interface ITileTypeWallDamageable : ITileTypeWall, ISelectable, IDamageableDefinition
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
            { TileTypeImportMap.Soil, TileTypeGroundSoil.GetInstance()},
            { TileTypeImportMap.Dirt, TileWallDirt.GetInstance()},
            { TileTypeImportMap.Loose, TileWallLooseRock.GetInstance()},
            { TileTypeImportMap.Hard, TileWallHardRock.GetInstance()},
            { TileTypeImportMap.Solid, TileWallSolidRock.GetInstance()},
            { TileTypeImportMap.EnergySeam, TileWallEnergyCrystalSeam.GetInstance()},
            { TileTypeImportMap.RegeneratorSeam, TileWallEnergyCrystalRegeneratorSeam.GetInstance()},
            { TileTypeImportMap.Lava, TileGroundLava.GetInstance()},
            { TileTypeImportMap.Water, TileGroundWater.GetInstance()},
            { TileTypeImportMap.SlugHole, TileGroundSlimySlugHole.GetInstance()},
            { TileTypeImportMap.OreSeam, TileWallOreSeam.GetInstance()}
        };

        public static ITileType ToTileType(this TileTypeImportMap value) => TileTypeMap.ContainsKey(value) ? TileTypeMap[value] : new TileTypeGroundSoil();
    }
}