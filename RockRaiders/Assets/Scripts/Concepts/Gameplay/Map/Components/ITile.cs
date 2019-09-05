using Assets.Scripts.Concepts.Gameplay.Map.TileType;
using Assets.Scripts.Concepts.Gameplay.Shared;
using UnityEngine;

namespace Assets.Scripts.Concepts.Gameplay.Map.Components
{
    public interface ITile : IPositionable
    {
        ITileType TileType { get; set; }
        Vector3 Vertex0 { get; set; }
        Vector3 Vertex1 { get; set; }
        Vector3 Vertex2 { get; set; }
        Vector3 Vertex3 { get; set; }
    }
}