using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using Object = UnityEngine.Object;

public static class BuildingTemplates
{

    public static Vector3 BuildingOffset = new Vector3(Map.Tilesize / 2f, -1, Map.Tilesize / 2f);

    public static IBuildingDefinition GetClassFromName(string name)
    {
        if (name == "toolstore") return new ToolStore();
        if (name == "smallteleport") return new SmallTeleport();
        if (name == "powerstation") return new PowerStation();
        if (name == "docks") return new Docks();
        if (name == "supportstation") return new SupportStation();
        if (name == "upgradestation") return new UpgradeStation();
        if (name == "geologicalcenter") return new GeologicalCenter();
        if (name == "orerefinery") return new OreRefinery();
        if (name == "mininglaser") return new MiningLaser();
        if (name == "superteleport") return new SuperTeleport();
        return null;
    }
}

public interface IBuildingDefinition
{
    int[,] GetTemplate();

    List<List<Model>> GetModels();

    int[] GetRequiredResources();

    int MaxHealth { get; }
}

public interface IBuilding
{
    bool IsPowered { get; }
    bool IsPowerOn { get; }
}

// This is just a reference for now. For the reader's benefit. 
public enum BuildingTile
{
    Empty = 0,
    SoilBuilding = 1,
    SoilFoundation = 2,
    WaterBuilding = 3,
    WaterFoundation = 4,
    LavaBuilding = 5,
    LavaFoundation = 6,
    SoilAndWaterBuilding = 7,
    SoilAndWaterFoundation = 8,
    SoilAndLavaBuilding = 9,
    SoilAndLavaFoundation = 10,
    SoilWaterLavaBuilding = 11,
    SoilWaterLavaFoundation = 12
}

/// <summary>
/// ADDING BUILDINGS?
/// MAKE SURE IT'S FACING DOWN!
/// </summary>

public class ToolStore : IBuildingDefinition
{

    private readonly int[,] _template = {
        {0,0,0,0,0},
        {0,0,0,0,0},
        {0,0,1,0,0},
        {0,0,2,0,0},
        {0,0,0,0,0}
    };

    private static readonly List<List<Model>> _models = new List<List<Model>>
    {
        new List<Model>{
            new Model ( AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Effects/TeleportFlames.prefab", typeof(GameObject)), BuildingTemplates.BuildingOffset, false)
        },
        new List<Model>{
            new Model ( AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Buildings/ToolStore/toolstore.prefab", typeof(GameObject)), BuildingTemplates.BuildingOffset)
        }
    };

    public List<List<Model>> GetModels()
    {
        return _models;
    }

    public int[,] GetTemplate()
    {
        return _template;
    }
    
    public int[] GetRequiredResources()
    {
        return new[] {Ore, Crystals};
    }

    public int MaxHealth
    {
        get { return 20; }
    }

    public int Ore = 0;
    public int Crystals = 0;
}

public class SmallTeleport : IBuildingDefinition
{
    private readonly int[,] _template = {
        {0,0,0,0,0},
        {0,0,0,0,0},
        {0,0,1,0,0},
        {0,0,2,0,0},
        {0,0,0,0,0}
    };

    private static readonly List<List<Model>> _models = new List<List<Model>>
    {
        new List<Model>{
            new Model ( AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Effects/TeleportFlames.prefab", typeof(GameObject)), BuildingTemplates.BuildingOffset, false),
            new Model ( AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Buildings/Foundation/Foundation.prefab", typeof(GameObject)), BuildingTemplates.BuildingOffset, false),
        },
        new List<Model>{new Model ( AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Buildings/Telepad/Telepad.prefab", typeof(GameObject)), BuildingTemplates.BuildingOffset)}
        
    };

    public List<List<Model>> GetModels()
    {
        return _models;
    }

    public int[,] GetTemplate()
    {
        return _template;
    }

    public int[] GetRequiredResources()
    {
        return new[] { Ore, Crystals };
    }

    public int MaxHealth
    {
        get { return 20; }
    }

    public int Ore = 8;
    public int Crystals = 0;
}

public class Docks : IBuildingDefinition
{
    private readonly int[,] _template = {
        {0,0,0,0,0},
        {0,0,2,0,0},
        {0,0,1,0,0},
        {0,0,4,0,0},
        {0,0,0,0,0}
    };

    private static readonly List<List<Model>> _models = new List<List<Model>>
    {
        new List<Model>{
            new Model ( AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Buildings/Docks/docks.prefab", typeof(GameObject)), BuildingTemplates.BuildingOffset)
        }
    };

    public List<List<Model>> GetModels()
    {
        return _models;
    }

    public int[,] GetTemplate()
    {
        return _template;
    }

    public int[] GetRequiredResources()
    {
        return new[] { Ore, Crystals };
    }

    public int MaxHealth
    {
        get { return 20; }
    }

    public int Ore = 8;
    public int Crystals = 1;
}

public struct Model
{
    public Object Asset;
    public Vector3 OffsetTransform;
    public bool Teleport;

    public Model(Object asset, Vector3 tf, bool teleport = true)
    {
        Asset = asset;
        OffsetTransform = tf;
        Teleport = teleport;
    }
}

public class PowerStation : IBuildingDefinition
{
    private readonly int[,] _template = {
        {0,0,0,0,0},
        {0,0,0,0,0},
        {0,0,1,1,0},
        {0,0,2,0,0},
        {0,0,0,0,0}
    };

    private static readonly Func<string, Type, Object> LoadAsset = (path, type) => AssetDatabase.LoadAssetAtPath(path, type);

    private static readonly List<List<Model>> _models = new List<List<Model>>
    {
        new List<Model>{
            new Model ( LoadAsset("Assets/Prefabs/Buildings/Effects/TeleportFlames2.prefab", typeof(GameObject)), BuildingTemplates.BuildingOffset + new Vector3(-.5f,0,0), false),
            new Model ( LoadAsset("Assets/Prefabs/Buildings/Foundation/Foundation2.prefab", typeof(GameObject)), BuildingTemplates.BuildingOffset + new Vector3(-.5f,0,0), false),
        },
        new List<Model>
        {
            new Model ( LoadAsset("Assets/Prefabs/Buildings/PowerStation/powerstation.prefab", typeof(GameObject)), BuildingTemplates.BuildingOffset),
            new Model ( LoadAsset("Assets/Prefabs/Buildings/PowerStation/powerstation2.prefab", typeof(GameObject)), BuildingTemplates.BuildingOffset + new Vector3(-1,0,0))
        },
        new List<Model>{
            new Model ( LoadAsset("Assets/Prefabs/Buildings/PowerStation/powerstation3.prefab", typeof(GameObject)), BuildingTemplates.BuildingOffset)
        },
    };

    public List<List<Model>> GetModels()
    {
        return _models;
    }

    public int[,] GetTemplate()
    {
        return _template;
    }

    public int[] GetRequiredResources()
    {
        return new[] { Ore, Crystals };
    }

    public int MaxHealth
    {
        get { return 20; }
    }

    public int Ore = 12;
    public int Crystals = 2;
}

public class SupportStation : IBuildingDefinition
{
    private readonly int[,] _template = {
        {0,0,0,0,0},
        {0,0,0,0,0},
        {0,0,1,0,0},
        {0,0,2,0,0},
        {0,0,0,0,0}
    };

    private static readonly List<List<Model>> _models = new List<List<Model>>
    {
        new List<Model>{
            new Model ( AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Buildings/SupportStation/supportstation.prefab", typeof(GameObject)), BuildingTemplates.BuildingOffset)
        }
    };

    public List<List<Model>> GetModels()
    {
        return _models;
    }

    public int[,] GetTemplate()
    {
        return _template;
    }

    public int[] GetRequiredResources()
    {
        return new[] { Ore, Crystals };
    }

    public int MaxHealth
    {
        get { return 20; }
    }

    public int Ore = 15;
    public int Crystals = 3;
}

public class UpgradeStation : IBuildingDefinition
{
    private readonly int[,] _template = {
        {0,0,0,0,0},
        {0,0,0,0,0},
        {0,0,1,0,0},
        {0,0,2,0,0},
        {0,0,0,0,0}
    };

    private static readonly List<List<Model>> _models = new List<List<Model>>
    {
        new List<Model>{
            new Model ( AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Buildings/ToolStore/toolstore.prefab", typeof(GameObject)), new Vector3(0.01f, 0, 0.1f))
        }
    };

    public List<List<Model>> GetModels()
    {
        return _models;
    }

    public int[,] GetTemplate()
    {
        return _template;
    }

    public int[] GetRequiredResources()
    {
        return new[] { Ore, Crystals };
    }

    public int MaxHealth
    {
        get { return 20; }
    }

    public int Ore = 20;
    public int Crystals = 3;
}

public class GeologicalCenter : IBuildingDefinition
{
    private readonly int[,] _template =
    {
        {0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0},
        {0, 0, 1, 0, 0},
        {0, 0, 2, 0, 0},
        {0, 0, 0, 0, 0}
    };

    private static readonly List<List<Model>> _models = new List<List<Model>>
    {
        new List<Model>{
            new Model ( AssetDatabase.LoadAssetAtPath("Assets/Prefabs/toolstore.prefab", typeof(GameObject)), new Vector3(0.01f, 0, 0.1f))
        }
    };

    public List<List<Model>> GetModels()
    {
        return _models;
    }

    public int[,] GetTemplate()
    {
        return _template;
    }


    public int[] GetRequiredResources()
    {
        return new[] {Ore, Crystals};
    }

    public int MaxHealth
    {
        get { return 20; }
    }

    public int Ore = 15;
    public int Crystals = 3;
}

public class OreRefinery : IBuildingDefinition
{
    private readonly int[,] _template = {
        {0,0,0,0,0},
        {0,0,0,0,0},
        {0,0,1,0,0},
        {0,0,1,0,0},
        {0,0,2,0,0}
    };

    private static readonly List<List<Model>> _models = new List<List<Model>>
    {
        new List<Model>{
            new Model ( AssetDatabase.LoadAssetAtPath("Assets/Prefabs/toolstore.prefab", typeof(GameObject)), new Vector3(0.01f, 0, 0.1f))
        }
    };

    public List<List<Model>> GetModels()
    {
        return _models;
    }

    public int[,] GetTemplate()
    {
        return _template;
    }

    public int[] GetRequiredResources()
    {
        return new[] { Ore, Crystals };
    }

    public int MaxHealth
    {
        get { return 20; }
    }

    public int Ore = 20;
    public int Crystals = 3;
}

public class MiningLaser : IBuildingDefinition
{
    private readonly int[,] _template = {
        {0,0,0,0,0},
        {0,0,0,0,0},
        {0,0,1,0,0},
        {0,0,0,0,0},
        {0,0,0,0,0}
    };

    private static readonly List<List<Model>> _models = new List<List<Model>>
    {
        new List<Model>{
            new Model ( AssetDatabase.LoadAssetAtPath("Assets/Prefabs/toolstore.prefab", typeof(GameObject)), new Vector3(0.01f, 0, 0.1f))
        }
    };

    public List<List<Model>> GetModels()
    {
        return _models;
    }

    public int[,] GetTemplate()
    {
        return _template;
    }

    public int[] GetRequiredResources()
    {
        return new[] { Ore, Crystals };
    }

    public int MaxHealth
    {
        get { return 20; }
    }

    public int Ore = 15;
    public int Crystals = 1;
}

public class SuperTeleport : IBuildingDefinition
{
    private readonly int[,] _template = {
        {0,0,0,0,0},
        {0,0,0,0,0},
        {0,0,1,1,0},
        {0,0,2,2,0},
        {0,0,0,0,0}
    };

    private static readonly List<List<Model>> _models = new List<List<Model>>
    {
        new List<Model>{
            new Model ( AssetDatabase.LoadAssetAtPath("Assets/Prefabs/toolstore.prefab", typeof(GameObject)), new Vector3(0.01f, 0, 0.1f))
        }
    };

    public List<List<Model>> GetModels()
    {
        return _models;
    }

    public int[,] GetTemplate()
    {
        return _template;
    }

    public int[] GetRequiredResources()
    {
        return new[] { Ore, Crystals };
    }

    public int MaxHealth
    {
        get { return 20; }
    }

    public int Ore = 20;
    public int Crystals = 3;
}

public class BuildingInstance : IDamageable
{
    public Vector2 Position;

    private IBuildingDefinition _template;
    public Map.Orientation TemplateOrientation = Map.Orientation.South;

    public List<GameObject> Models = new List<GameObject>();
    public List<BuildingTileInstance> Tiles = new List<BuildingTileInstance>();

    public bool IsBuilt = false;

    private int _currentHealth;

    public int[,] GetOrientedTemplate()
    {
        var templateCopy = _template.GetTemplate().Clone() as int[,];

        switch (TemplateOrientation)
        {
            case Map.Orientation.North:
                return templateCopy;
            case Map.Orientation.East:
                return HelperMethods.RotateMatrix(HelperMethods.RotateMatrix(HelperMethods.RotateMatrix(templateCopy, 5), 5), 5);
            case Map.Orientation.South:
                return HelperMethods.RotateMatrix(HelperMethods.RotateMatrix(templateCopy, 5), 5);
            case Map.Orientation.West:
                return HelperMethods.RotateMatrix(templateCopy, 5);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public IBuildingDefinition GetBuildingDefinition()
    {
        return _template;
    }

    public BuildingInstance(IBuildingDefinition building, Vector2 position, Map.Orientation orientation)
    {
        _template = building;
        TemplateOrientation = orientation;
        Position = position;
    }

    public int[] GetRemainingResourcesRequired()
    {
        var currentResourcesInBuildingSite = new int[2];
        var template = GetOrientedTemplate();

        #region Calculate Resources In Building Site

        for (var j = 0; j < 5; j++)
        {
            for (var i = 0; i < 5; i++)
            {
                var dx = j - 2;
                var dy = i - 2;

                var x = (int) Position.x + dx;
                var y = (int) Position.y + dy;

                if (x > 0 && y > 0 && x < Map.Tiles.GetLength(0) && y < Map.Tiles.GetLength(1))
                {
                    if (template[i, j] > 0)
                    {
                        var tileResources = Map.GetResourcesOnTile(new Vector2(x, y));
                        currentResourcesInBuildingSite =
                            currentResourcesInBuildingSite.Select((item, index) => item + tileResources[index]).ToArray();
                    }
                }
            }
        }

        #endregion

        var currentOre = currentResourcesInBuildingSite[0];
        var currentCrystals = currentResourcesInBuildingSite[1];
        var buildingCost = GetRequiredResources();
        var requiredOre = buildingCost[0];
        var requiredCrystals = buildingCost[1];

        return new[] {requiredOre - currentOre, requiredCrystals - currentCrystals};
    }

    public int[] GetRequiredResources()
    {
        return _template.GetRequiredResources();
    }

    public int GetHealth
    {
        get { throw new NotImplementedException(); }
    }

    public int GetMaxHealth
    {
        get { return _template.MaxHealth; }
    }

    public bool IsAlive
    {
        get { throw new NotImplementedException(); }
    }

    public bool IsDamageable
    {
        get { throw new NotImplementedException(); }
    }

    public void Damage(int amount)
    {
        throw new NotImplementedException();
    }

    public void Heal(int amount)
    {
        throw new NotImplementedException();
    }

    public void Kill()
    {
        throw new NotImplementedException();
    }
}

public class BuildingTileInstance
{
    public Vector2 Location;

    public GameObject BuildingModel;

    public BuildingInstance Parent;

    public BuildingTileInstance(int x, int y, BuildingInstance parent, GameObject model = null)
    {
        Location = new Vector2(x, y);
        Parent = parent;
        BuildingModel = model;
    }
}