using System;
using UnityEngine;

namespace Assets.Scripts.Concepts.Cosmic.Space
{
    public enum CompassOrientation
    {
        NorthWest = 0,
        North = 1,
        NorthEast = 2,
        West = 3,
        None = 4,
        East = 5,
        SouthWest = 6,
        South = 7,
        SouthEast = 8,
    }

    public static class OrientationExtensions
    {
        public static Vector2 ToOffsetVector2(this CompassOrientation orientation)
        {
            switch (orientation)
            {
                case CompassOrientation.NorthWest:
                    return new Vector2(-1, 1);
                case CompassOrientation.North:
                    return new Vector2(0, 1);
                case CompassOrientation.NorthEast:
                    return new Vector2(1, 1);
                case CompassOrientation.West:
                    return new Vector2(-1, 0);
                case CompassOrientation.None:
                    return new Vector2(0, 0);
                case CompassOrientation.East:
                    return new Vector2(1, 0);
                case CompassOrientation.SouthWest:
                    return new Vector2(-1, -1);
                case CompassOrientation.South:
                    return new Vector2(0, -1);
                case CompassOrientation.SouthEast:
                    return new Vector2(1, -1);
                //case CompassOrientation.NorthWest:
                //    return new Vector2(-1, -1);
                //case CompassOrientation.North:
                //    return new Vector2(0, -1);
                //case CompassOrientation.NorthEast:
                //    return new Vector2(1, -1);
                //case CompassOrientation.West:
                //    return new Vector2(-1, 0);
                //case CompassOrientation.None:
                //    return new Vector2(0, 0);
                //case CompassOrientation.East:
                //    return new Vector2(1, 0);
                //case CompassOrientation.SouthWest:
                //    return new Vector2(-1, 1);
                //case CompassOrientation.South:
                //    return new Vector2(0, 1);
                //case CompassOrientation.SouthEast:
                //    return new Vector2(1, 1);
                default:
                    throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
            }
        }

        public static Vector3 ToOffsetVector3(this CompassOrientation orientation)
        {
            var vec = orientation.ToOffsetVector2();
            return new Vector3(vec.x, 0, vec.y);
        }

        public static CompassOrientation? Add(this CompassOrientation orientation,
            CompassAxisOrientation axisOrientation)
        {
            switch (orientation)
            {
                case CompassOrientation.NorthWest:
                    if (axisOrientation == CompassAxisOrientation.North) return null;
                    if (axisOrientation == CompassAxisOrientation.East) return CompassOrientation.North;
                    if (axisOrientation == CompassAxisOrientation.South) return CompassOrientation.West;
                    if (axisOrientation == CompassAxisOrientation.West) return null;
                    break;
                case CompassOrientation.North:
                    if (axisOrientation == CompassAxisOrientation.North) return null;
                    if (axisOrientation == CompassAxisOrientation.East) return CompassOrientation.NorthEast;
                    if (axisOrientation == CompassAxisOrientation.South) return CompassOrientation.None;
                    if (axisOrientation == CompassAxisOrientation.West) return CompassOrientation.NorthWest;
                    break;
                case CompassOrientation.NorthEast:
                    if (axisOrientation == CompassAxisOrientation.North) return null;
                    if (axisOrientation == CompassAxisOrientation.East) return null;
                    if (axisOrientation == CompassAxisOrientation.South) return CompassOrientation.East;
                    if (axisOrientation == CompassAxisOrientation.West) return CompassOrientation.North;
                    break;
                case CompassOrientation.West:
                    if (axisOrientation == CompassAxisOrientation.North) return CompassOrientation.NorthWest;
                    if (axisOrientation == CompassAxisOrientation.East) return CompassOrientation.None;
                    if (axisOrientation == CompassAxisOrientation.South) return CompassOrientation.SouthWest;
                    if (axisOrientation == CompassAxisOrientation.West) return null;
                    break;
                case CompassOrientation.None:
                    return axisOrientation.ToCompassOrientation();
                case CompassOrientation.East:
                    if (axisOrientation == CompassAxisOrientation.North) return CompassOrientation.NorthEast;
                    if (axisOrientation == CompassAxisOrientation.East) return null;
                    if (axisOrientation == CompassAxisOrientation.South) return CompassOrientation.SouthEast;
                    if (axisOrientation == CompassAxisOrientation.West) return CompassOrientation.None;
                    break;
                case CompassOrientation.SouthWest:
                    if (axisOrientation == CompassAxisOrientation.North) return CompassOrientation.West;
                    if (axisOrientation == CompassAxisOrientation.East) return CompassOrientation.South;
                    if (axisOrientation == CompassAxisOrientation.South) return null;
                    if (axisOrientation == CompassAxisOrientation.West) return null;
                    break;
                case CompassOrientation.South:
                    if (axisOrientation == CompassAxisOrientation.North) return CompassOrientation.None;
                    if (axisOrientation == CompassAxisOrientation.East) return CompassOrientation.North;
                    if (axisOrientation == CompassAxisOrientation.South) return null;
                    if (axisOrientation == CompassAxisOrientation.West) return CompassOrientation.SouthWest;
                    break;
                case CompassOrientation.SouthEast:
                    if (axisOrientation == CompassAxisOrientation.North) return CompassOrientation.East;
                    if (axisOrientation == CompassAxisOrientation.East) return null;
                    if (axisOrientation == CompassAxisOrientation.South) return null;
                    if (axisOrientation == CompassAxisOrientation.West) return CompassOrientation.South;
                    break;
                default: throw new ArgumentOutOfRangeException();
            }
            return null;
        }

        public static CompassOrientation Rotate(this CompassOrientation orientation, RotationalOrientation rotationalOrientation)
        {
            switch (orientation)
            {
                case CompassOrientation.NorthWest:
                    return rotationalOrientation == RotationalOrientation.Clockwise ? CompassOrientation.North : CompassOrientation.West;
                case CompassOrientation.North:
                    return rotationalOrientation == RotationalOrientation.Clockwise ? CompassOrientation.NorthEast : CompassOrientation.NorthWest;
                case CompassOrientation.NorthEast:
                    return rotationalOrientation == RotationalOrientation.Clockwise ? CompassOrientation.East : CompassOrientation.North;
                case CompassOrientation.West:
                    return rotationalOrientation == RotationalOrientation.Clockwise ? CompassOrientation.NorthWest : CompassOrientation.SouthWest;
                case CompassOrientation.None:
                    return CompassOrientation.None;
                case CompassOrientation.East:
                    return rotationalOrientation == RotationalOrientation.Clockwise ? CompassOrientation.SouthEast : CompassOrientation.NorthEast;
                case CompassOrientation.SouthWest:
                    return rotationalOrientation == RotationalOrientation.Clockwise ? CompassOrientation.West : CompassOrientation.South;
                case CompassOrientation.South:
                    return rotationalOrientation == RotationalOrientation.Clockwise ? CompassOrientation.SouthWest : CompassOrientation.SouthEast;
                case CompassOrientation.SouthEast:
                    return rotationalOrientation == RotationalOrientation.Clockwise ? CompassOrientation.South : CompassOrientation.East;
            }
            throw new ArgumentException();
        }

        public static CompassOrientation Opposite(this CompassOrientation orientation)
        {
            switch (orientation)
            {
                case CompassOrientation.NorthWest:
                    return CompassOrientation.SouthEast;
                case CompassOrientation.North:
                    return CompassOrientation.South;
                case CompassOrientation.NorthEast:
                    return CompassOrientation.SouthWest;
                case CompassOrientation.West:
                    return CompassOrientation.East;
                case CompassOrientation.East:
                    return CompassOrientation.West;
                case CompassOrientation.SouthWest:
                    return CompassOrientation.NorthEast;
                case CompassOrientation.South:
                    return CompassOrientation.North;
                case CompassOrientation.SouthEast:
                    return CompassOrientation.NorthWest;
            }
            throw new ArgumentException();
        }
    }
}