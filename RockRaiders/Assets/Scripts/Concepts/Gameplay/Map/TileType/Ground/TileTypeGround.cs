using Assets.Scripts.Concepts.Gameplay.Shared;
using Assets.Scripts.Miscellaneous;

namespace Assets.Scripts.Concepts.Gameplay.Map.TileType.Ground
{
    public class TileTypeGroundSoil : Singleton<TileTypeGroundSoil>, ITileTypeSolidGround, ITileTypeBuildable, ISelectable
    {
        public ITileBiome Biome { get; set; }
        public bool CanBeEroded { get; set; } = true;
        public string TooltipText { get; set; } = "Soil";
        public bool IsTooltipVocalized { get; set; } = false;
        public bool IsHighlightedToBuild { get; set; }
        public string MaterialBaseName { get; } = "00";
        public bool PreventSelection { get; set; }
    }

    public class TileGroundSlimySlugHole : Singleton<TileGroundSlimySlugHole>, ITileTypeSolidGround, ISelectable
    {
        public ITileBiome Biome { get; set; }
        public bool CanBeEroded { get; set; } = true;
        public string TooltipText { get; set; } = "Slimy Slug Hole";
        public bool IsTooltipVocalized { get; set; } = true;
        public string MaterialBaseName { get; } = "30";
        public bool PreventSelection { get; set; }
    }

    public class TileGroundWater : Singleton<TileGroundWater>, ITileTypeLiquidGround
    {
        public ITileBiome Biome { get; set; }
        public string TooltipText { get; set; } = "Water";
        public bool IsTooltipVocalized { get; set; } = true;
        public string MaterialBaseName { get; } = "45";
    }

    public class TileGroundLava : Singleton<TileGroundLava>, ITileTypeLiquidGround, ITileTypeDamagingGround
    {
        public ITileBiome Biome { get; set; }
        public string TooltipText { get; set; } = "Lava";
        public bool IsTooltipVocalized { get; set; } = true;
        public string MaterialBaseName { get; } = "46";
    }
}