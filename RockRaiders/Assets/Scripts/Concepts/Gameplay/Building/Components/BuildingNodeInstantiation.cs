using UnityEngine;

namespace Assets.Scripts.Concepts.Gameplay.Building.Components
{
    public class BuildingNodeInstantiation
    {
        public BuildingNode Node { get; set; }

        public Building Parent { get; set; }

        public GameObject NodePhysicality { get; set; }
    }
}