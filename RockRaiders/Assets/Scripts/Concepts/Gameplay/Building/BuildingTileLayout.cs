using System.Collections.Generic;
using Assets.Scripts.Concepts.Gameplay.Building.Components;

namespace Assets.Scripts.Concepts.Gameplay.Building
{
    public interface IBuildingTileLayout
    {
        IEnumerable<BuildingNode> Nodes { get; set; }
    }

    public class BuildingTileLayout<T> : IBuildingTileLayout 
        where T : IBuildingTileType, new()
    {
        public T BuildingTileType;
        public IEnumerable<BuildingNode> Nodes { get; set; }

        public BuildingTileLayout()
        {
            BuildingTileType = new T();
        }

        public BuildingTileLayout(params BuildingNode[] nodes)
        {
            BuildingTileType = new T();
            Nodes = nodes;
        }
    }
}
