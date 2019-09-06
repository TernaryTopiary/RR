using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Assets.Scripts.Concepts.Cosmic.Space;
using Assets.Scripts.Concepts.Gameplay.Map.TileType;
using Assets.Scripts.Concepts.Gameplay.Map.TileType.Ground;
using Assets.Scripts.Concepts.Gameplay.Map.TileType.Wall;
using Assets.Scripts.Extensions;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.Concepts.Gameplay.Map.Components
{
    [DebuggerDisplay("{ToString()}")]
    public class Tile : ITile
    {
        public const byte
            IndexNorthWest = 0,
            IndexNorthEast = 1,
            IndexSouthEast = 2,
            IndexSouthWest = 3;
        //IndexNorthWest = 0,
        //IndexNorthEast = 1,
        //IndexSouthEast = 2,
        //IndexSouthWest = 3;

        public event Action<Tile> VerticiesChanged;

        public ITileType TileType { get; set; }
        
        private List<Vector3> _verticies = new List<Vector3>(Constants.Constants.DefaultTileVerticies);

        public List<Vector3> Verticies
        {
            get { return _verticies; }
            set
            {
                var old = _verticies;
                _verticies = value;
                VerticiesChanged?.Invoke(this);
            }
        }

        public int[] Indicies { get; set; } = Constants.Constants.TileIndicies;

        public void SetVertexAt(int index, Vector2 value)
        {
            _verticies[index] = _verticies[index] + new Vector3(value.x, 0, value.y);
            VerticiesChanged?.Invoke(this);
        }

        public void SetVertexAt(int index, Vector3 value)
        {
            _verticies[index] = value;
            VerticiesChanged?.Invoke(this);
        }

        public void SetVertexAt(CornerOrientation orientation, Vector3 value)
        {
            _verticies[orientation.ToVertexIndex()] = value;
            VerticiesChanged?.Invoke(this);
        }

        public Vector3 GetVertexAt(CornerOrientation orientation)
        {
            return _verticies[orientation.ToVertexIndex()];
        }

        public void SetTileLocation(Vector2 location)
        {
            Verticies = Verticies.Select(v => v + new Vector3(location.x, 0, location.y)).ToList();
        }

        public Vector3 Vertex0
        {
            get { return _verticies[IndexNorthWest]; }
            set { SetVertexAt(0, value); }
        }

        public Vector3 Vertex1
        {
            get { return _verticies[IndexNorthEast]; }
            set { SetVertexAt(IndexNorthEast, value); }
        }

        public Vector3 Vertex2
        {
            get { return _verticies[IndexSouthEast]; }
            set { SetVertexAt(IndexSouthEast, value); }
        }

        public Vector3 Vertex3
        {
            get { return _verticies[IndexSouthWest]; }
            set { SetVertexAt(IndexSouthWest, value); }
        }

        public Vector3 VertexNorthWest => Vertex0;

        public Vector3 VertexNorthEast => Vertex1;

        public Vector3 VertexSouthEast => Vertex2;

        public Vector3 VertexSouthWest => Vertex3;

        public Vector3 Average() => Verticies.Average();

        public float AverageTileHeight => (Vertex0.y + Vertex1.y + Vertex2.y + Vertex3.y) / 4;

        public static float DefaultTileVerticalHeight { get; set; } = 2f;

        public bool IsGround => TileType is ITileTypeGround;
        public bool IsWall => TileType is ITileTypeWall;
        public bool IsSoil => TileType is TileTypeGroundSoil;
        public bool IsWater => TileType is TileGroundWater;
        public bool IsLava => TileType is TileGroundLava;
        public bool IsCeiling => Configuration == TileConfiguration.Ceiling;

        /// <summary>
        /// Is this currently a wall?
        /// </summary>
        public bool IsActiveWall => !IsCeiling && !IsGround;

        public TileConfiguration Configuration { get; set; } = TileConfiguration.Ceiling;
        public float OriginalTileHeight { get; set; }

        public new string ToString() => $"{string.Join(", ", Verticies)}";
    }

    public static class TileExtensions
    {
    }
}