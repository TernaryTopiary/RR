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
                    return rotationalOrientation == RotationalOrientation.Clockwise ? CompassAxisOrientation.South: CompassAxisOrientation.North;
                case CompassAxisOrientation.South:                                    
                    return rotationalOrientation == RotationalOrientation.Clockwise ? CompassAxisOrientation.West : CompassAxisOrientation.East;
                case CompassAxisOrientation.West:                                     
                    return rotationalOrientation == RotationalOrientation.Clockwise ? CompassAxisOrientation.North : CompassAxisOrientation.South;
            }
            throw new ArgumentException();
        }
    }
}