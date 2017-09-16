using System;
using System.Collections.Generic;
using UnityEngine;

public static class MaterialManager
{
    #region Materials

    public static Material HighlightBuildingFoundationPlacement =
        Resources.Load("Materials/BuildingFoundationPlacementHover") as Material;

    public static Material HighlightBuildingHoverPlacement =
        Resources.Load("Materials/BuildingPlacementHover") as Material;

    public static Material HighlightBuildingPlacementDenied =
        Resources.Load("Materials/BuildingPlacementDeniedHover") as Material;

    #region Biomes

    #region Rock Biome

    public static List<Material> BiomeRockMaterials = new List<Material>
    {
        Resources.Load("Materials/Biomes/ROCK/ROCK00") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK01") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK02") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK03") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK04") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK05") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK06") as Material,
        null,
        null,
        null,
        Resources.Load("Materials/Biomes/ROCK/ROCK10") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK11") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK12") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK13") as Material,
        null,
        null,
        Resources.Load("Materials/Biomes/ROCK/ROCK16") as Material,
        null,
        null,
        null,
        Resources.Load("Materials/Biomes/ROCK/ROCK20") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK21") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK22") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK23") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK24") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK25") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK26") as Material,
        null,
        null,
        null,
        Resources.Load("Materials/Biomes/ROCK/ROCK30") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK31") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK32") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK33") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK34") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK35") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK36") as Material,
        null,
        null,
        null,
        Resources.Load("Materials/Biomes/ROCK/ROCK40") as Material,
        null,
        null,
        null,
        null,
        Resources.Load("Materials/Biomes/ROCK/ROCK45") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK46") as Material,
        null,
        null,
        null,
        null,
        Resources.Load("Materials/Biomes/ROCK/ROCK51") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK52") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK53") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK54") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK55") as Material,
        null,
        null,
        null,
        Resources.Load("Materials/Biomes/ROCK/ROCK59") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK60") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK61") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK62") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK63") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK64") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK65") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK66") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK67") as Material,
        null,
        null,
        Resources.Load("Materials/Biomes/ROCK/ROCK70") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK71") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK72") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK73") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK74") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK75") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK76") as Material,
        Resources.Load("Materials/Biomes/ROCK/ROCK77") as Material,
    };

    #endregion

    #region Ice Biome

    public static List<Material> BiomeIceMaterials = new List<Material>
    {
        Resources.Load("Materials/Biomes/ICE/ICE00") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE01") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE02") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE03") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE04") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE05") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE06") as Material,
        null,
        null,
        null,
        Resources.Load("Materials/Biomes/ICE/ICE10") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE11") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE12") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE13") as Material,
        null,
        null,
        Resources.Load("Materials/Biomes/ICE/ICE16") as Material,
        null,
        null,
        null,
        Resources.Load("Materials/Biomes/ICE/ICE20") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE21") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE22") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE23") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE24") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE25") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE26") as Material,
        null,
        null,
        null,
        Resources.Load("Materials/Biomes/ICE/ICE30") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE31") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE32") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE33") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE34") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE35") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE36") as Material,
        null,
        null,
        null,
        Resources.Load("Materials/Biomes/ICE/ICE40") as Material,
        null,
        null,
        null,
        null,
        Resources.Load("Materials/Biomes/ICE/ICE45") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE46") as Material,
        null,
        null,
        null,
        null,
        Resources.Load("Materials/Biomes/ICE/ICE51") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE52") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE53") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE54") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE55") as Material,
        null,
        null,
        null,
        null,
        Resources.Load("Materials/Biomes/ICE/ICE60") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE61") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE62") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE63") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE64") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE65") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE66") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE67") as Material,
        null,
        null,
        Resources.Load("Materials/Biomes/ICE/ICE70") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE71") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE72") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE73") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE74") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE75") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE76") as Material,
        Resources.Load("Materials/Biomes/ICE/ICE77") as Material,
    };

    #endregion

    #region Lava Biome

    public static List<Material> BiomeLavaMaterials = new List<Material>
    {
        Resources.Load("Materials/Biomes/LAVA/LAVA00") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA01") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA02") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA03") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA04") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA05") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA06") as Material,
        null,
        null,
        null,
        Resources.Load("Materials/Biomes/LAVA/LAVA10") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA11") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA12") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA13") as Material,
        null,
        null,
        Resources.Load("Materials/Biomes/LAVA/LAVA16") as Material,
        null,
        null,
        null,
        Resources.Load("Materials/Biomes/LAVA/LAVA20") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA21") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA22") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA23") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA24") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA25") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA26") as Material,
        null,
        null,
        null,
        Resources.Load("Materials/Biomes/LAVA/LAVA30") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA31") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA32") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA33") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA34") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA35") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA36") as Material,
        null,
        null,
        null,
        Resources.Load("Materials/Biomes/LAVA/LAVA40") as Material,
        null,
        null,
        null,
        null,
        Resources.Load("Materials/Biomes/LAVA/LAVA45") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA46") as Material,
        null,
        null,
        null,
        null,
        Resources.Load("Materials/Biomes/LAVA/LAVA51") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA52") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA53") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA54") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA55") as Material,
        null,
        null,
        null,
        null,
        Resources.Load("Materials/Biomes/LAVA/LAVA60") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA61") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA62") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA63") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA64") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA65") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA66") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA67") as Material,
        null,
        null,
        Resources.Load("Materials/Biomes/LAVA/LAVA70") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA71") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA72") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA73") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA74") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA75") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA76") as Material,
        Resources.Load("Materials/Biomes/LAVA/LAVA77") as Material,
    };

    #endregion

    #endregion

    #endregion

    #region Enums

    public enum Biome
    {
        LAVA,
        ICE,
        ROCK
    }

    public enum PathType
    {
        CORNER,
        TJUNCTION,
        ROAD,
        SOLO,
        CROSSROADS,
        FOUNDATION
    }

    public enum RockType
    {
        DIRT,
        LOOSE,
        HARD,
        SOLID
    }

    #endregion

    public static Material GetBuildingFoundationHoverTile()
    {
        return HighlightBuildingFoundationPlacement;
    }

    public static Material GetBuildingHoverTile()
    {
        return HighlightBuildingHoverPlacement;
    }

    public static Material GetBuildingDeniedHoverTile()
    {
        return HighlightBuildingPlacementDenied;
    }

    private static Material GetBiomeTile(Biome biome, int tileIndex)
    {
        if (BiomeRockMaterials[tileIndex] == null) throw new ArgumentOutOfRangeException("tileIndex");
        switch (biome)
        {
            case Biome.LAVA:
                return BiomeLavaMaterials[tileIndex];
            case Biome.ICE:
                return BiomeIceMaterials[tileIndex];
            case Biome.ROCK:
                return BiomeRockMaterials[tileIndex];
            default:
                throw new ArgumentOutOfRangeException("biome");
        }
    }

    public static Material GetGroundTile(Biome biome)
    {
        return GetBiomeTile(biome, 0);
    }

    public static Material GetEnergyCrystalSeamTile(Biome biome)
    {
        return GetBiomeTile(biome, 20);
    }

    public static Material GetOreSeamTile(Biome biome)
    {
        return GetBiomeTile(biome, 40);
    }

    public static Material GetRubbleTile(int level, Biome biome)
    {
        int rubbleIndex;
        switch (level)
        {
            case 4:
                rubbleIndex = 10;
                break;
            case 3:
                rubbleIndex = 11;
                break;
            case 2:
                rubbleIndex = 12;
                break;
            case 1:
                rubbleIndex = 13;
                break;
            default:
                Debug.Log("Rubble index invalid");
                throw new ArgumentOutOfRangeException("level");
        }
        return GetBiomeTile(biome, rubbleIndex);
    }

    public static Material GetErosionTile(int level, Biome biome)
    {
        int erosionIndex;
        switch (level)
        {
            case 4:
                erosionIndex = 06;
                break;
            case 3:
                erosionIndex = 16;
                break;
            case 2:
                erosionIndex = 26;
                break;
            case 1:
                erosionIndex = 36;
                break;
            default:
                Debug.Log("Erosion index invalid");
                throw new ArgumentOutOfRangeException("level");
        }
        return GetBiomeTile(biome, erosionIndex);
    }

    public static Material GetPowerPathTile(Biome biome, PathType pathType, bool powered = false)
    {
        int pathTypeIndex;
        switch (pathType)
        {
            case PathType.CORNER:
                pathTypeIndex = 63;
                break;
            case PathType.TJUNCTION:
                pathTypeIndex = 64;
                break;
            case PathType.ROAD:
                pathTypeIndex = 62;
                break;
            case PathType.SOLO:
                pathTypeIndex = 59;
                break;
            case PathType.CROSSROADS:
                pathTypeIndex = 60;
                break;
            case PathType.FOUNDATION:
                pathTypeIndex = 61;
                break;
            default:
                throw new ArgumentOutOfRangeException("pathType");
        }
        if (powered && pathType != PathType.SOLO)
        {
            pathTypeIndex += 10;
        }
        return GetBiomeTile(biome, pathTypeIndex);
    }

    public static Material GetFoundationTile(Biome biome, bool powered = false)
    {
        if(powered) return GetBiomeTile(biome, 66);
        else return GetBiomeTile(biome, 76);
    }

    public static Material GetSlimySlugHoleTile(Biome biome)
    {
        return GetBiomeTile(biome, 30);
    }

    public static Material GetRechargeSeamTile(Biome biome)
    {
        return GetBiomeTile(biome, 67);
    }

    public static Material GetLavaTile(Biome biome)
    {
        return GetBiomeTile(biome, 46);
    }

    public static Material GetWaterTile(Biome biome)
    {
        return GetBiomeTile(biome, 45);
    }

    public static Material GetRoofTile(Biome biome)
    {
        return GetBiomeTile(biome, 70);
    }

    public static Material GetWallTile(RockType type, Biome biome, bool isReinforced = false)
    {
        int tileWallIndex;
        switch (type)
        {
            case RockType.DIRT:
                tileWallIndex = 2;
                break;
            case RockType.LOOSE:
                tileWallIndex = 3;
                break;
            case RockType.HARD:
                tileWallIndex = 4;
                break;
            case RockType.SOLID:
                tileWallIndex = 5;
                break;
            default:
                throw new ArgumentOutOfRangeException("type");
        }
        if (isReinforced) tileWallIndex += 20;
        return GetBiomeTile(biome, tileWallIndex);
    }

    public static Material GetInternalCornerWallTile(RockType type, Biome biome)
    {
        int tileWallIndex;
        switch (type)
        {
            case RockType.DIRT:
                tileWallIndex = 32;
                break;
            case RockType.LOOSE:
                tileWallIndex = 33;
                break;
            case RockType.HARD:
                tileWallIndex = 34;
                break;
            case RockType.SOLID:
                tileWallIndex = 35;
                break;
            default:
                throw new ArgumentOutOfRangeException("type");
        }
        return GetBiomeTile(biome, tileWallIndex);
    }

    public static Material GetExternalCornerWallTile(RockType type, Biome biome)
    {
        int tileWallIndex;
        switch (type)
        {
            case RockType.DIRT:
                tileWallIndex = 52;
                break;
            case RockType.LOOSE:
                tileWallIndex = 53;
                break;
            case RockType.HARD:
                tileWallIndex = 54;
                break;
            case RockType.SOLID:
                tileWallIndex = 55;
                break;
            default:
                throw new ArgumentOutOfRangeException("type");
        }
        return GetBiomeTile(biome, tileWallIndex);
    }
}

