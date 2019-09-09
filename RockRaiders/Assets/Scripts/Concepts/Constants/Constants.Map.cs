using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Concepts.Cosmic.Space;

namespace Assets.Scripts.Concepts.Constants
{
    public static partial class Constants
    {
        public static int TileScale = 1;

        public static readonly Dictionary<CompassOrientation, Vector3> DefaultTileVertexDictionary = new Dictionary<CompassOrientation, Vector3>
        {
            { CompassOrientation.None, new Vector3(TileScale/2f, 0, TileScale/2f) },
            { CompassOrientation.SouthWest, new Vector3(0, 0, 0) },
            { CompassOrientation.NorthWest, new Vector3(0, 0, TileScale) },
            { CompassOrientation.NorthEast, new Vector3(TileScale, 0, TileScale) },
            { CompassOrientation.SouthEast, new Vector3(TileScale, 0, 0) }
        };

        public static readonly List<Vector3> DefaultTileVerticies = DefaultTileVertexDictionary.Values.ToList();

        public static readonly int[] TileIndicies = {
            0, 1, 2,
            0, 2, 3,
            0, 3, 4,
            0, 4, 1
        };
    }
}