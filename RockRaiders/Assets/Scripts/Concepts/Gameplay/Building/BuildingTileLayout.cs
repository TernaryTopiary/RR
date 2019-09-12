using System.Collections.Generic;
using Assets.Scripts.Concepts.Gameplay.Building.Components;
using Assets.Scripts.Concepts.Gameplay.Map.TileType;

namespace Assets.Scripts.Concepts.Gameplay.Building
{
    public interface IBuildingTileLayout
    {
        IBuildingTileType BuildingTileType { get; set; }
        BuildingNode Node { get; set; }

        List<ITileType> ValidTargetTileTypes { get; set; }
    }

    public class BuildingTileLayout<T> : IBuildingTileLayout 
        where T : IBuildingTileType, new()
    {
        public IBuildingTileType BuildingTileType { get; set; }
        public BuildingNode Node { get; set; }
        public List<ITileType> ValidTargetTileTypes { get; set; } = new List<ITileType>();

        public BuildingTileLayout()
        {
            BuildingTileType = new T();
        }

        public BuildingTileLayout(BuildingNode node)
        {
            BuildingTileType = new T();
            Node = node;
        }
    }
}
