using System.Collections.Generic;
using Assets.Scripts.Concepts.Gameplay.Building.Components;

namespace Assets.Scripts.Concepts.Gameplay.Building
{
    public interface IBuildingTileLayout
    {

    }

    public class BuildingTileLayout<T> : IBuildingTileLayout 
        where T : IBuildingTileType, new()
    {
        public T BuildingTileType;
        public IEnumerable<BuildingNode> Nodes;

        public BuildingTileLayout()
        {
            BuildingTileType = new T();
        }

        public BuildingTileLayout(IEnumerable<BuildingNode> nodes)
        {
            BuildingTileType = new T();
            Nodes = nodes;
        }
    }
}
