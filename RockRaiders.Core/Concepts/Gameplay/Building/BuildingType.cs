using System;
using System.Collections.Generic;
using Assets.Scripts.Concepts.Cosmic.Time;
using Assets.Scripts.Concepts.Gameplay.Building.Components;
using Assets.Scripts.Concepts.Gameplay.Resource;
using UnityEngine;

namespace Assets.Scripts.Concepts.Gameplay.Building
{
    public class BuildingTypeToolStore : IBuildingType
    {
        public string TooltipText { get; set; } = "Tool Store";
        public bool IsTooltipVocalized { get; set; } = true;
        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int>();
        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }

        private static IEnumerable<BuildingNode> ToolstoreX0Y0Nodes = new[] { BuildingNodes.ToolstoreBuildingX0Y0 };

        public Vector3 BuildingPivotCoordinates { get; set; } = new Vector3(0, 0);

        public IBuildingTileLayout[,] DefaultTileLayout { get; set; } =
        {
            {new BuildingTileLayout<BuildingTileTypeFoundation>{ Nodes = ToolstoreX0Y0Nodes } },
            {new BuildingTileLayout<BuildingTileTypeFoundation>()}
        };
    }

    public class BuildingTypeTeleportPad : IBuildingType
    {
        public string TooltipText { get; set; } = "Teleport Pad";
        public bool IsTooltipVocalized { get; set; } = true;
        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int>() { { typeof(ResourceTypeOre), 8 } };
        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }

        private static IEnumerable<BuildingNode> TeleportPadX0Y0Nodes = new[] { BuildingNodes.BasePlate1x1, BuildingNodes.TeleportPadBuildingX0Y0 };

        public Vector3 BuildingPivotCoordinates { get; set; } = new Vector3(0, 0);

        public IBuildingTileLayout[,] DefaultTileLayout { get; set; } =
        {
            {new BuildingTileLayout<BuildingTileTypeFoundation>{ Nodes = TeleportPadX0Y0Nodes } },
            {new BuildingTileLayout<BuildingTileTypeFoundation>()}
        };
    }

    public class BuildingTypeDocks : IBuildingType
    {
        public string TooltipText { get; set; } = "Docks";
        public bool IsTooltipVocalized { get; set; } = true;

        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int>()
        {
            { typeof(ResourceTypeOre), 8 },
            { typeof(ResourceTypeCrystal), 1 }
        };

        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }

        private static IEnumerable<BuildingNode> DocksX0Y0Nodes = new[] { BuildingNodes.BasePlate1x1, BuildingNodes.DocksBuildingX0Y0 };

        public Vector3 BuildingPivotCoordinates { get; set; } = new Vector3(0, 1);

        public IBuildingTileLayout[,] DefaultTileLayout { get; set; } =
        {
            {new BuildingTileLayout<BuildingTileTypeFoundation>()},
            {new BuildingTileLayout<BuildingTileTypeFoundation>{ Nodes = DocksX0Y0Nodes } },
            {new BuildingTileLayout<BuildingTileTypeEmpty>()}
        };
    }

    public class BuildingTypePowerStation : IBuildingType
    {
        public string TooltipText { get; set; } = "Power Station";
        public bool IsTooltipVocalized { get; set; } = true;

        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int>()
        {
            { typeof(ResourceTypeOre), 12 },
            { typeof(ResourceTypeCrystal), 2 }
        };

        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }

        private static IEnumerable<BuildingNode> PowerStationX0Y0Nodes = new[] { BuildingNodes.BasePlate1x2, BuildingNodes.DocksBuildingX0Y0 };

        public Vector3 BuildingPivotCoordinates { get; set; } = new Vector3(0, 0);

        public IBuildingTileLayout[,] DefaultTileLayout { get; set; } =
        {
            {new BuildingTileLayout<BuildingTileTypeFoundation>()},
            {new BuildingTileLayout<BuildingTileTypeFoundation>{ Nodes = PowerStationX0Y0Nodes } },
            {new BuildingTileLayout<BuildingTileTypeEmpty>()}
        };
    }
    /*
    public class BuildingTypeSupportStation : IBuildingType
    {
        public string TooltipText { get; set; } = "Support Station";
        public bool IsTooltipVocalized { get; set; } = true;

        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int>()
        {
            { typeof(ResourceTypeOre), 15 },
            { typeof(ResourceTypeCrystal), 3 }
        };

        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }
    }

    public class BuildingTypeUpgradeStation : IBuildingType
    {
        public string TooltipText { get; set; } = "Upgrade Station";
        public bool IsTooltipVocalized { get; set; } = true;

        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int>()
        {
            { typeof(ResourceTypeOre), 20 },
            { typeof(ResourceTypeCrystal), 3 }
        };

        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }
    }

    public class BuildingTypeGeologicalCenter : IBuildingType
    {
        public string TooltipText { get; set; } = "Geological Center";
        public bool IsTooltipVocalized { get; set; } = true;

        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int>()
        {
            { typeof(ResourceTypeOre), 15 },
            { typeof(ResourceTypeCrystal), 3 }
        };

        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }
    }

    public class BuildingTypeOreRefinery : IBuildingType
    {
        public string TooltipText { get; set; } = "Ore Refinery";
        public bool IsTooltipVocalized { get; set; } = true;

        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int>()
        {
            { typeof(ResourceTypeOre), 20 },
            { typeof(ResourceTypeCrystal), 3 }
        };

        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }
    }

    public class BuildingTypeMiningLaser : IBuildingType
    {
        public string TooltipText { get; set; } = "Mining Laser";
        public bool IsTooltipVocalized { get; set; } = true;

        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int>()
        {
            { typeof(ResourceTypeOre), 15 },
            { typeof(ResourceTypeCrystal), 1 }
        };

        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }
    }

    public class BuildingTypeSuperTeleport : IBuildingType
    {
        public string TooltipText { get; set; } = "Super Teleport";
        public bool IsTooltipVocalized { get; set; } = true;

        public Dictionary<Type, int> Cost { get; set; } = new Dictionary<Type, int>()
        {
            { typeof(ResourceTypeOre), 20 },
            { typeof(ResourceTypeCrystal), 2 }
        };

        public Seconds TimeToCreate { get; set; }
        public int DefaultHitpoints { get; set; }
    }
    */
}