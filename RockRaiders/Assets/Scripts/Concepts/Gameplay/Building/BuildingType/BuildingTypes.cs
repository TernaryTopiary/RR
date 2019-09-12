using System;
using System.Collections.Generic;
using Assets.Scripts.Concepts.Cosmic.Array;
using Assets.Scripts.Concepts.Cosmic.Space;
using Assets.Scripts.Concepts.Cosmic.Time;
using Assets.Scripts.Concepts.Gameplay.Building.Components;
using Assets.Scripts.Concepts.Gameplay.Resource;
using System.Linq;
using Assets.Scripts.Concepts.Gameplay.Map.TileType;
using Assets.Scripts.Concepts.Gameplay.Map.TileType.Ground;
using Assets.Scripts.Miscellaneous;

namespace Assets.Scripts.Concepts.Gameplay.Building.BuildingType
{
    public static class BuildingTypeHelper
    {
        public static Dictionary<BuildingType, IBuildingType> BuildingTypeLookup =
            new Dictionary<BuildingType, IBuildingType>
            {
                {BuildingType.ToolStore, BuildingTypeToolStore.GetInstance()},
                {BuildingType.TeleportPad, BuildingTypeTeleportPad.GetInstance()},
                {BuildingType.Docks, BuildingTypeDocks.GetInstance()},
                {BuildingType.PowerStation, BuildingTypePowerStation.GetInstance()},
                {BuildingType.SupportStation, BuildingTypeSupportStation.GetInstance()},
                {BuildingType.UpgradeStation, BuildingTypeUpgradeStation.GetInstance()},
                {BuildingType.GeologicalCenter, BuildingTypeGeologicalCenter.GetInstance()},
                {BuildingType.SuperTeleport, BuildingTypeSuperTeleport.GetInstance()},
                {BuildingType.MiningLaser, BuildingTypeMiningLaser.GetInstance()},
                {BuildingType.OreRefinery, BuildingTypeOreRefinery.GetInstance()}
            };
    }

    public class BuildingTypeToolStore : Singleton<BuildingTypeToolStore>, IBuildingType
    {
        public BuildingType BuildingType { get; } = BuildingType.ToolStore;
        public string TooltipText { get; set; } = "Tool Store";
        public bool IsTooltipVocalized { get; set; } = true;
        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int>();
        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }

        public AdjoiningGrid9<IBuildingTileLayout> DefaultTileLayout { get; set; } = new AdjoiningGrid9<IBuildingTileLayout>
        {
            {CompassOrientation.None, new BuildingTileLayout<BuildingTileTypeFoundation>(BuildingNodes.ToolstoreBuildingX0Y0) { ValidTargetTileTypes = new List<ITileType> { TileTypeGroundSoil.GetInstance() } }},
            {CompassOrientation.South, new BuildingTileLayout<BuildingTileTypeFoundation>() { ValidTargetTileTypes = new List<ITileType> { TileTypeGroundSoil.GetInstance() } } },
        };
    }

    public class BuildingTypeTeleportPad : Singleton<BuildingTypeTeleportPad>, IBuildingType
    {
        public BuildingType BuildingType { get; } = BuildingType.TeleportPad;
        public string TooltipText { get; set; } = "Teleport Pad";
        public bool IsTooltipVocalized { get; set; } = true;
        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int>() { { typeof(ResourceTypeOre), 8 } };
        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }

        public AdjoiningGrid9<IBuildingTileLayout> DefaultTileLayout { get; set; } = new AdjoiningGrid9<IBuildingTileLayout>
        {
            {CompassOrientation.None, new BuildingTileLayout<BuildingTileTypeFoundation>(BuildingNodes.TeleportPadBuildingX0Y0) { ValidTargetTileTypes = new List<ITileType> { TileTypeGroundSoil.GetInstance() } }},
            {CompassOrientation.South, new BuildingTileLayout<BuildingTileTypeFoundation>() { ValidTargetTileTypes = new List<ITileType> { TileTypeGroundSoil.GetInstance() } } },
        };
    }

    public class BuildingTypeDocks : Singleton<BuildingTypeDocks>, IBuildingType
    {
        public BuildingType BuildingType { get; } = BuildingType.Docks;
        public string TooltipText { get; set; } = "Docks";
        public bool IsTooltipVocalized { get; set; } = true;

        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int>()
        {
            { typeof(ResourceTypeOre), 8 },
            { typeof(ResourceTypeCrystal), 1 }
        };

        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }

        public AdjoiningGrid9<IBuildingTileLayout> DefaultTileLayout { get; set; } = new AdjoiningGrid9<IBuildingTileLayout>
        {
            {CompassOrientation.North, new BuildingTileLayout<BuildingTileTypeFoundation>() { ValidTargetTileTypes = new List<ITileType> { TileTypeGroundSoil.GetInstance() } }},
            {CompassOrientation.None, new BuildingTileLayout<BuildingTileTypeFoundation>(BuildingNodes.DocksBuildingX0Y0) { ValidTargetTileTypes = new List<ITileType> { TileTypeGroundSoil.GetInstance() } }},
            {CompassOrientation.South, new BuildingTileLayout<BuildingTileTypeEmpty>() { ValidTargetTileTypes = new List<ITileType> { TileGroundWater.GetInstance() } } },
        };
    }

    public class BuildingTypePowerStation : Singleton<BuildingTypePowerStation>, IBuildingType
    {
        public BuildingType BuildingType { get; } = BuildingType.PowerStation;
        public string TooltipText { get; set; } = "Power Station";
        public bool IsTooltipVocalized { get; set; } = true;

        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int>()
        {
            { typeof(ResourceTypeOre), 12 },
            { typeof(ResourceTypeCrystal), 2 }
        };

        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }

        public AdjoiningGrid9<IBuildingTileLayout> DefaultTileLayout { get; set; } =
            new AdjoiningGrid9<IBuildingTileLayout>
            {
                {CompassOrientation.West, new BuildingTileLayout<BuildingTileTypeFoundation>(BuildingNodes.PowerStationBuildingX0Y0) { ValidTargetTileTypes = new List<ITileType> { TileTypeGroundSoil.GetInstance() } }},
                {CompassOrientation.None, new BuildingTileLayout<BuildingTileTypeFoundation>(BuildingNodes.PowerStationBuildingX1Y0) { ValidTargetTileTypes = new List<ITileType> { TileTypeGroundSoil.GetInstance() } }},
                {CompassOrientation.South, new BuildingTileLayout<BuildingTileTypeFoundation>() { ValidTargetTileTypes = new List<ITileType> { TileTypeGroundSoil.GetInstance() } } },
            };
    }

    public class BuildingTypeSupportStation : Singleton<BuildingTypeSupportStation>, IBuildingType
    {
        public BuildingType BuildingType { get; } = BuildingType.SupportStation;
        public string TooltipText { get; set; } = "Support Station";
        public bool IsTooltipVocalized { get; set; } = true;

        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int>()
        {
            { typeof(ResourceTypeOre), 15 },
            { typeof(ResourceTypeCrystal), 3 }
        };

        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }

        public AdjoiningGrid9<IBuildingTileLayout> DefaultTileLayout { get; set; } =
            new AdjoiningGrid9<IBuildingTileLayout>();
    }

    public class BuildingTypeUpgradeStation : Singleton<BuildingTypeUpgradeStation>, IBuildingType
    {
        public BuildingType BuildingType { get; } = BuildingType.UpgradeStation;
        public string TooltipText { get; set; } = "Upgrade Station";
        public bool IsTooltipVocalized { get; set; } = true;

        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int>()
        {
            { typeof(ResourceTypeOre), 20 },
            { typeof(ResourceTypeCrystal), 3 }
        };

        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }

        public AdjoiningGrid9<IBuildingTileLayout> DefaultTileLayout { get; set; } =
            new AdjoiningGrid9<IBuildingTileLayout>();
    }

    public class BuildingTypeGeologicalCenter : Singleton<BuildingTypeGeologicalCenter>, IBuildingType
    {
        public BuildingType BuildingType { get; } = BuildingType.GeologicalCenter;
        public string TooltipText { get; set; } = "Geological Center";
        public bool IsTooltipVocalized { get; set; } = true;

        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int>()
        {
            { typeof(ResourceTypeOre), 15 },
            { typeof(ResourceTypeCrystal), 3 }
        };

        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }

        public AdjoiningGrid9<IBuildingTileLayout> DefaultTileLayout { get; set; } =
            new AdjoiningGrid9<IBuildingTileLayout>();
    }

    public class BuildingTypeOreRefinery : Singleton<BuildingTypeOreRefinery>, IBuildingType
    {
        public BuildingType BuildingType { get; } = BuildingType.OreRefinery;
        public string TooltipText { get; set; } = "Ore Refinery";
        public bool IsTooltipVocalized { get; set; } = true;

        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int>()
        {
            { typeof(ResourceTypeOre), 20 },
            { typeof(ResourceTypeCrystal), 3 }
        };

        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }

        public AdjoiningGrid9<IBuildingTileLayout> DefaultTileLayout { get; set; } =
            new AdjoiningGrid9<IBuildingTileLayout>();
    }

    public class BuildingTypeMiningLaser : Singleton<BuildingTypeMiningLaser>, IBuildingType
    {
        public BuildingType BuildingType { get; } = BuildingType.MiningLaser;
        public string TooltipText { get; set; } = "Mining Laser";
        public bool IsTooltipVocalized { get; set; } = true;

        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int>()
        {
            { typeof(ResourceTypeOre), 15 },
            { typeof(ResourceTypeCrystal), 1 }
        };

        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }

        public AdjoiningGrid9<IBuildingTileLayout> DefaultTileLayout { get; set; } =
            new AdjoiningGrid9<IBuildingTileLayout>();
    }

    public class BuildingTypeSuperTeleport : Singleton<BuildingTypeSuperTeleport>, IBuildingType
    {
        public BuildingType BuildingType { get; } = BuildingType.SuperTeleport;
        public string TooltipText { get; set; } = "Super Teleport";
        public bool IsTooltipVocalized { get; set; } = true;

        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int>()
        {
            { typeof(ResourceTypeOre), 20 },
            { typeof(ResourceTypeCrystal), 2 }
        };

        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }

        public AdjoiningGrid9<IBuildingTileLayout> DefaultTileLayout { get; set; } =
            new AdjoiningGrid9<IBuildingTileLayout>();
    }
}