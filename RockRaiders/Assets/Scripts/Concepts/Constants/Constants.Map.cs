using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Concepts.Cosmic.Space;

namespace Assets.Scripts.Concepts.Constants
{
    public static partial class Constants
    {
        public static int TileScale = 1;

        public static readonly Dictionary<CornerOrientation, Vector3> DefaultTileVertexDictionary = new Dictionary<CornerOrientation, Vector3>
        {
            { CornerOrientation.SouthWest, new Vector3(0, 0, 0) },
            { CornerOrientation.NorthWest, new Vector3(0, 0, TileScale) },
            { CornerOrientation.NorthEast, new Vector3(TileScale, 0, TileScale) },
            { CornerOrientation.SouthEast, new Vector3(TileScale, 0, 0) }
        };

        public static readonly List<Vector3> DefaultTileVerticies = DefaultTileVertexDictionary.Values.ToList();
        //{
        //    new Vector3(0, 0, 0),
        //    new Vector3(0, 0, TileScale),
        //    new Vector3(TileScale, 0, TileScale),
        //    new Vector3(TileScale, 0, 0)
        //};

        public static readonly int[] TileIndicies = { 0, 1, 2, 2, 3, 0 };

        //public static readonly List<Vector3> DefaultTileVerticies = new List<Vector3>
        //{
        //    new Vector3(0, 0, 0),
        //    new Vector3(-TileScale, 0, 0),
        //    new Vector3(-TileScale, 0, TileScale),
        //    new Vector3(0, 0, TileScale)
        //};

        //public static readonly int[] TileIndicies = { 3, 0, 2, 1, 2, 0 };//0, 2, 1, 2, 0, 3 };
    }
}