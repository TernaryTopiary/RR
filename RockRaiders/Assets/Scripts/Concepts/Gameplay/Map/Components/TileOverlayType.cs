using System;

namespace Assets.Scripts.Concepts.Gameplay.Map.Components
{
    [Flags]
    public enum TileOverlayType
    {
        Reinforce = 1 << 0,
        Dynamite = 1 << 1,
        PlaceBuilding = 1 << 2,
        PlaceBuildingFoundation = 1 << 3,
        Selected = 1 << 4
    }
}
