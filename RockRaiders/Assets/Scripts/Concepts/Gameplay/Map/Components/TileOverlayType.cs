using System;

namespace Assets.Scripts.Concepts.Gameplay.Map.Components
{
    [Flags]
    public enum TileOverlayType
    {
        Reinforce = 1 << 0,
        Dynamite = 1 << 1,
        PowerPath = 1 << 2,
        Foundation = 1 << 3,
        PlaceBuilding = 1 << 4,
        PlaceBuildingFoundation = 1 << 5,
        InvalidBuildingPlacement = 1 << 6,
        Selected = 1 << 7
    }
}
