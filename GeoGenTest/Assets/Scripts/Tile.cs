using System;
using System.Linq;
using Assets.Scripts;
using UnityEngine;

public class Tile
{
    public GameObject TileGameObject;

    public float v0
    {
        get { return TileGameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh.vertices[0].y; }
    }

    public float v1
    {
        get { return TileGameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh.vertices[1].y; }
    }

    public float v2
    {
        get { return TileGameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh.vertices[2].y; }
    }

    public float v3
    {
        get { return TileGameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh.vertices[3].y; }
    }

    public enum RockClass
    {
        Soil = 0,
        Dirt = 2,
        Loose = 3,
        Hard = 4,
        Solid = 5,
        OreSeam = 26,
        EnergySeam = 13,
        RegeneratorSeam = 41,
        SlugHole = 19,
        Water = 27,
        Lava = 28,
        FoundationPlacement,
        BuildingPlacement,
    }

    public bool IsSelectable()
    {
        return ((int)TileClass != 5 && (int)TileClass != 27 && (int)TileClass != 28);
    }

    public static Material GetWallMaterial(RockClass rClass, MaterialManager.Biome biome)
    {
        switch (rClass)
        {
            case RockClass.Soil:
                return MaterialManager.GetGroundTile(biome);
            case RockClass.Dirt:
                return MaterialManager.GetWallTile(MaterialManager.RockType.DIRT, biome);
            case RockClass.Loose:
                return MaterialManager.GetWallTile(MaterialManager.RockType.LOOSE, biome);
            case RockClass.Hard:
                return MaterialManager.GetWallTile(MaterialManager.RockType.HARD, biome);
            case RockClass.Solid:
                return MaterialManager.GetWallTile(MaterialManager.RockType.SOLID, biome);
            case RockClass.OreSeam:
                return MaterialManager.GetOreSeamTile(biome);
            case RockClass.EnergySeam:
                return MaterialManager.GetEnergyCrystalSeamTile(biome);
            case RockClass.RegeneratorSeam:
                return MaterialManager.GetRechargeSeamTile(biome);
            case RockClass.SlugHole:
                return MaterialManager.GetSlimySlugHoleTile(biome);
            case RockClass.Water:
                return MaterialManager.GetWaterTile(biome);
            case RockClass.Lava:
                return MaterialManager.GetLavaTile(biome);
        }
        return null;
    }

    public static Material GetExternalCornerMaterial(RockClass rClass, MaterialManager.Biome biome)
    {
        switch (rClass)
        {
            case RockClass.Soil:
                return MaterialManager.GetGroundTile(biome);
            case RockClass.Dirt:
                return MaterialManager.GetExternalCornerWallTile(MaterialManager.RockType.DIRT, biome);
            case RockClass.Loose:
                return MaterialManager.GetExternalCornerWallTile(MaterialManager.RockType.LOOSE, biome);
            case RockClass.Hard:
                return MaterialManager.GetExternalCornerWallTile(MaterialManager.RockType.HARD, biome);
            case RockClass.Solid:
                return MaterialManager.GetExternalCornerWallTile(MaterialManager.RockType.SOLID, biome);
            case RockClass.OreSeam:
                return MaterialManager.GetOreSeamTile(biome);
            case RockClass.EnergySeam:
                return MaterialManager.GetEnergyCrystalSeamTile(biome);
            case RockClass.RegeneratorSeam:
                return MaterialManager.GetRechargeSeamTile(biome);
            case RockClass.SlugHole:
                return MaterialManager.GetSlimySlugHoleTile(biome);
            case RockClass.Water:
                return MaterialManager.GetWaterTile(biome);
            case RockClass.Lava:
                return MaterialManager.GetLavaTile(biome);
        }
        return null;
    }

    public static Material GetInternalCornerMaterial(RockClass rClass, MaterialManager.Biome biome)
    {
        switch (rClass)
        {
            case RockClass.Soil:
                return MaterialManager.GetGroundTile(biome);
            case RockClass.Dirt:
                return MaterialManager.GetInternalCornerWallTile(MaterialManager.RockType.DIRT, biome);
            case RockClass.Loose:
                return MaterialManager.GetInternalCornerWallTile(MaterialManager.RockType.LOOSE, biome);
            case RockClass.Hard:
                return MaterialManager.GetInternalCornerWallTile(MaterialManager.RockType.HARD, biome);
            case RockClass.Solid:
                return MaterialManager.GetInternalCornerWallTile(MaterialManager.RockType.SOLID, biome);
            case RockClass.OreSeam:
                return MaterialManager.GetOreSeamTile(biome);
            case RockClass.EnergySeam:
                return MaterialManager.GetEnergyCrystalSeamTile(biome);
            case RockClass.RegeneratorSeam:
                return MaterialManager.GetRechargeSeamTile(biome);
            case RockClass.SlugHole:
                return MaterialManager.GetSlimySlugHoleTile(biome);
            case RockClass.Water:
                return MaterialManager.GetWaterTile(biome);
            case RockClass.Lava:
                return MaterialManager.GetLavaTile(biome);
        }
        return null;
    }

    public enum Augmentation
    {
        None,
        PathFoundation,
        PowerPath,
        Reinforcement
    }

    public MaterialManager.Biome TileBiome;
    public RockClass TileClass;
    public Augmentation TileAugmentation;
    public float TileHeight;
    private Material[] _oldMat;

    public Tile(MaterialManager.Biome biome, RockClass rockClass, Augmentation tileAug = Augmentation.None, float tileHeight = 0)
    {
        TileBiome = biome;
        TileClass = rockClass;
        TileAugmentation = tileAug;
        TileHeight = tileHeight;
    }

    public override string ToString()
    {
        return TileClass.ToString();
    }

    public bool IsSoil
    {
        get { return TileClass == RockClass.Soil; }
    }

    public bool IsWater
    {
        get { return TileClass == RockClass.Water; }
    }

    public bool IsLava
    {
        get { return TileClass == RockClass.Lava; }
    }

    public bool IsLowTileClass()
    {
        return TileClass == RockClass.Soil || TileClass == RockClass.Water || TileClass == RockClass.Lava;
    }

    public bool LowTileClass
    {
        get { return TileClass == RockClass.Soil || TileClass == RockClass.Water || TileClass == RockClass.Lava; }
    }

    public bool IsFoundation
    {
        get
        {
            // TODO: THIS
            throw new NotImplementedException();
        }
        set
        {
            if (value && !IsLava && !IsWater)
            {
                _oldMat = TileGameObject.GetComponent<SkinnedMeshRenderer>().sharedMaterials;
                TileGameObject.GetComponent<SkinnedMeshRenderer>().sharedMaterial = MaterialManager.GetFoundationTile(TileBiome);
            }
            else
            {
                if (_oldMat != null)
                {
                    TileGameObject.GetComponent<SkinnedMeshRenderer>().sharedMaterials = _oldMat;
                    _oldMat = null;
                }
            }
        }
    }

    public bool IsPowerPath
    {
        get
        {
            // TODO: THIS
            throw new NotImplementedException();
        }
        set
        {
            if (value && !IsLava && !IsWater)
            {
                _oldMat = TileGameObject.GetComponent<SkinnedMeshRenderer>().sharedMaterials;
                var tiles = Map.GetSurroundingBuildingTiles(Map.GetTileCoords(this));
                var neighborCount = 0;
                neighborCount += tiles[1, 0] != null ? 1 : 0; // NORTH
                neighborCount += tiles[2, 1] != null ? 1 : 0; // EAST
                neighborCount += tiles[0, 1] != null ? 1 : 0; // WEST
                neighborCount += tiles[1, 2] != null ? 1 : 0; // SARTH

                if (neighborCount == 0)
                {
                    TileGameObject.GetComponent<SkinnedMeshRenderer>().sharedMaterial =
                        MaterialManager.GetPowerPathTile(TileBiome, MaterialManager.PathType.SOLO);
                }
                else if (neighborCount == 4)
                {
                    TileGameObject.GetComponent<SkinnedMeshRenderer>().sharedMaterial =
                        MaterialManager.GetPowerPathTile(TileBiome, MaterialManager.PathType.CROSSROADS);
                }
                else
                {
                    if (neighborCount == 1)
                    {
                        
                    }
                }
                if (tiles[1, 0] != null)
                {
                    
                }
                //TileGameObject.GetComponent<SkinnedMeshRenderer>().sharedMaterial = MaterialManager.GetPowerPathTile(TileBiome, );
            }
            else
            {
                if (_oldMat != null)
                {
                    TileGameObject.GetComponent<SkinnedMeshRenderer>().sharedMaterials = _oldMat;
                    _oldMat = null;
                }
            }
        }
    }
}
