using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Concepts.Cosmic.Space
{
    public enum CornerOrientation
    {
        NorthWest = 0,
        NorthEast = 1,
        SouthWest = 2,
        SouthEast = 3
    }

    public static class QuadOrientationExtensions
    {
        public static IEnumerable<CompassOrientation> ToCandidateOrientations(this CornerOrientation orientation)
        {
            switch (orientation)
            {
                case CornerOrientation.NorthWest:
                    return new[] { CompassOrientation.NorthWest, CompassOrientation.North, CompassOrientation.West, CompassOrientation.None };

                case CornerOrientation.NorthEast:
                    return new[] { CompassOrientation.North, CompassOrientation.NorthEast, CompassOrientation.None, CompassOrientation.East };

                case CornerOrientation.SouthWest:
                    return new[] { CompassOrientation.West, CompassOrientation.None, CompassOrientation.SouthWest, CompassOrientation.South };

                case CornerOrientation.SouthEast:
                    return new[] { CompassOrientation.None, CompassOrientation.East, CompassOrientation.South, CompassOrientation.SouthEast };

                default:
                    throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
            }
        }

        public static CornerOrientation Rotate(this CornerOrientation orientation, RotationalOrientation rotationalOrientation)
        {
            switch (orientation)
            {
                case CornerOrientation.NorthWest:
                    return rotationalOrientation == RotationalOrientation.Clockwise ? CornerOrientation.NorthEast : CornerOrientation.SouthWest;
                case CornerOrientation.NorthEast:
                    return rotationalOrientation == RotationalOrientation.Clockwise ? CornerOrientation.SouthEast : CornerOrientation.NorthWest;
                case CornerOrientation.SouthWest:
                    return rotationalOrientation == RotationalOrientation.Clockwise ? CornerOrientation.NorthWest : CornerOrientation.SouthEast;
                case CornerOrientation.SouthEast:
                    return rotationalOrientation == RotationalOrientation.Clockwise ? CornerOrientation.SouthWest : CornerOrientation.NorthEast;
            }
            throw new ArgumentException();
        }

        public static int ToVertexIndex(this CornerOrientation orientation)
        {
            switch (orientation)
            {
                case CornerOrientation.NorthWest:
                    return 0;
                case CornerOrientation.NorthEast:
                    return 1;
                case CornerOrientation.SouthWest:
                    return 3;
                case CornerOrientation.SouthEast:
                    return 2;
                default:
                    throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
            }
        }

        public static CompassOrientation ToCompassOrientation(this CornerOrientation orientation)
        {
            switch(orientation)
            {
                case CornerOrientation.NorthEast: return CompassOrientation.NorthEast;
                case CornerOrientation.NorthWest: return CompassOrientation.NorthWest;
                case CornerOrientation.SouthEast: return CompassOrientation.SouthEast;
                case CornerOrientation.SouthWest: return CompassOrientation.SouthWest;
                default: throw new ArgumentOutOfRangeException(nameof(orientation), "Unsupported orientation.");
            }
        }
        
        public static string ToPrefix(this CornerOrientation orientation)
        {
            return new string(orientation.ToString().Where(c => char.IsUpper(c)).ToArray());
        }
    }
}