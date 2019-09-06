using Assets.Scripts.Miscellaneous;

namespace Assets.Scripts.Concepts.Gameplay.Map.TileType.Wall
{
    public class TileWallSoil : Singleton<TileWallSoil>, ITileTypeWallLightDrillable
    {
        public ITileBiome Biome { get; set; }
        public string TooltipText { get; set; } = "Soil";
        public bool IsTooltipVocalized { get; set; } = false;
        public int DefaultHitpoints { get; set; }

        public string MaterialBaseName { get; } = "1";
        public string WallMaterialName { get; } = "0";
        public string InternalCornerMaterialName { get; } = "3";
        public string ExternalCornerMaterialName { get; } = "5";
        public string ReinforcementMaterialName { get; } = "2";
        public string CeilingMaterialName { get; } = "70";
    }

    public class TileWallDirt : Singleton<TileWallDirt>, ITileTypeWallLightDrillable
    {
        public ITileBiome Biome { get; set; }
        public string TooltipText { get; set; } = "Dirt";
        public bool IsTooltipVocalized { get; set; } = true;
        public int DefaultHitpoints { get; set; }

        public string MaterialBaseName { get; } = "2";
        public string WallMaterialName { get; } = "0";
        public string InternalCornerMaterialName { get; } = "3";
        public string ExternalCornerMaterialName { get; } = "5";
        public string ReinforcementMaterialName { get; } = "2";
        public string CeilingMaterialName { get; } = "70";
    }

    public class TileWallLooseRock : Singleton<TileWallLooseRock>, ITileTypeWallLightDrillable
    {
        public ITileBiome Biome { get; set; }
        public string TooltipText { get; set; } = "Loose Rock";
        public bool IsTooltipVocalized { get; set; } = true;
        public int DefaultHitpoints { get; set; }

        public string MaterialBaseName { get; } = "3";
        public string WallMaterialName { get; } = "0";
        public string InternalCornerMaterialName { get; } = "3";
        public string ExternalCornerMaterialName { get; } = "5";
        public string ReinforcementMaterialName { get; } = "2";
        public string CeilingMaterialName { get; } = "70";
    }

    public class TileWallHardRock : Singleton<TileWallHardRock>, ITileTypeTypeWallHeavyDrillable
    {
        public ITileBiome Biome { get; set; }
        public string TooltipText { get; set; } = "Hard Rock";
        public bool IsTooltipVocalized { get; set; } = true;
        public int DefaultHitpoints { get; set; }

        public string MaterialBaseName { get; } = "4";
        public string WallMaterialName { get; } = "0";
        public string InternalCornerMaterialName { get; } = "3";
        public string ExternalCornerMaterialName { get; } = "5";
        public string ReinforcementMaterialName { get; } = "2";
        public string CeilingMaterialName { get; } = "70";
    }

    public class TileWallSolidRock : Singleton<TileWallSolidRock>, ITileTypeWall
    {
        public ITileBiome Biome { get; set; }
        public string TooltipText { get; set; } = "Solid Rock";
        public bool IsTooltipVocalized { get; set; } = true;

        public string MaterialBaseName { get; } = "5";
        public string WallMaterialName { get; } = "0";
        public string InternalCornerMaterialName { get; } = "3";
        public string ExternalCornerMaterialName { get; } = "5";
        public string CeilingMaterialName { get; } = "70";
    }

    public class TileWallOreSeam : Singleton<TileWallOreSeam>, ITileTypeWallLightDrillable
    {
        public ITileBiome Biome { get; set; }
        public string TooltipText { get; set; } = "Ore Seam";
        public bool IsTooltipVocalized { get; set; } = true;
        public int DefaultHitpoints { get; set; }

        public string MaterialBaseName { get; } = "0";
        public string WallMaterialName { get; } = "4";
        public string InternalCornerMaterialName { get; } = "4";
        public string ExternalCornerMaterialName { get; } = "4";
        public string ReinforcementMaterialName { get; } = "4";
        public string CeilingMaterialName { get; } = "70";
    }

    public class TileWallEnergyCrystalSeam : Singleton<TileWallEnergyCrystalSeam>, ITileTypeWallLightDrillable
    {
        public ITileBiome Biome { get; set; }
        public string TooltipText { get; set; } = "Energy Crystal Seam";
        public bool IsTooltipVocalized { get; set; } = true;
        public int DefaultHitpoints { get; set; }

        public string MaterialBaseName { get; } = "0";
        public string WallMaterialName { get; } = "2";
        public string InternalCornerMaterialName { get; } = "2";
        public string ExternalCornerMaterialName { get; } = "2";
        public string ReinforcementMaterialName { get; } = "2";
        public string CeilingMaterialName { get; } = "70";
    }

    public class TileWallEnergyCrystalRegeneratorSeam : Singleton<TileWallEnergyCrystalRegeneratorSeam>, ITileTypeWall
    {
        public ITileBiome Biome { get; set; }
        public string TooltipText { get; set; } = "Recharge Seam";
        public bool IsTooltipVocalized { get; set; } = true;

        public string MaterialBaseName { get; } = "7";
        public string WallMaterialName { get; } = "6";
        public string InternalCornerMaterialName { get; } = "6";
        public string ExternalCornerMaterialName { get; } = "6";
        public string CeilingMaterialName { get; } = "70";
    }
}