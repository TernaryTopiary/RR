using System;

namespace Assets.Scripts.Concepts.Cosmic.Space
{
    public enum CompassAxisOrientation
    {
        North = 0,
        East = 1,
        West = 2,
        South = 3
    }

    public static class CompassAxisOrientationExtensions
    {
        public static CompassAxisOrientation Rotate(this CompassAxisOrientation orientation, RotationalOrientation rotationalOrientation)
        {
            switch (orientation)
            {
                case CompassAxisOrientation.North:
                    return rotationalOrientation == RotationalOrientation.Clockwise ? CompassAxisOrientation.East : CompassAxisOrientation.West;

                case CompassAxisOrientation.East:
                    return rotationalOrientation == RotationalOrientation.Clockwise ? CompassAxisOrientation.South : CompassAxisOrientation.North;

                case CompassAxisOrientation.South:
                    return rotationalOrientation == RotationalOrientation.Clockwise ? CompassAxisOrientation.West : CompassAxisOrientation.East;

                case CompassAxisOrientation.West:
                    return rotationalOrientation == RotationalOrientation.Clockwise ? CompassAxisOrientation.North : CompassAxisOrientation.South;
            }
            throw new ArgumentException();
        }

        public static CompassOrientation ToCompassOrientation(this CompassAxisOrientation orientation)
        {
            switch (orientation)
            {
                case CompassAxisOrientation.North: return CompassOrientation.North;
                case CompassAxisOrientation.East: return CompassOrientation.East;
                case CompassAxisOrientation.South: return CompassOrientation.South;
                case CompassAxisOrientation.West: return CompassOrientation.West;
                default: throw new ArgumentOutOfRangeException(nameof(orientation), "Unsupported orientation.");
            }
        }

        public static CompassAxisOrientation Opposite(this CompassAxisOrientation orientation)
        {
            switch (orientation)
            {
                case CompassAxisOrientation.North: return CompassAxisOrientation.South;
                case CompassAxisOrientation.East: return  CompassAxisOrientation.West;
                case CompassAxisOrientation.South: return CompassAxisOrientation.North;
                case CompassAxisOrientation.West: return CompassAxisOrientation.East;
                default: throw new ArgumentOutOfRangeException(nameof(orientation), "Unsupported orientation.");
            }
        }

        public static CornerOrientation[] ToEdgeCorners(this CompassAxisOrientation orientation)
        {
            switch (orientation)
            {
                case CompassAxisOrientation.North: return new[] { CornerOrientation.NorthWest, CornerOrientation.NorthEast };
                case CompassAxisOrientation.East: return new[] { CornerOrientation.NorthEast, CornerOrientation.SouthEast };
                case CompassAxisOrientation.South: return new[] { CornerOrientation.SouthEast, CornerOrientation.SouthWest };
                case CompassAxisOrientation.West: return new[] { CornerOrientation.SouthWest, CornerOrientation.NorthWest };
                default: throw new ArgumentOutOfRangeException(nameof(orientation), "Unsupported orientation.");
            }
        }
    }
}