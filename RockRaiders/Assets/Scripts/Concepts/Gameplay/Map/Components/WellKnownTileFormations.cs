using Assets.Scripts.Concepts.Cosmic.Space;
using Assets.Scripts.Concepts.Gameplay.Map.TileType;
using Assets.Scripts.Extensions;
using System.Collections.Generic;

namespace Assets.Scripts.Concepts.Gameplay.Map.Components
{
    public static class WellKnownTileFormations
    {
        /// <summary>
        /// The number of tiles matching a formation below that unambiguously guarantees a central tile is a certain configuration.
        /// </summary>
        public static byte UnambiguityLimit { get; } = 4;

        public static Dictionary<TileConfiguration, List<CompassOrientation[]>> WallConfigurationLayoutMap { get; } = new Dictionary<TileConfiguration, List<CompassOrientation[]>>
        {
            {
                TileConfiguration.ExternalCorner,
                new List<CompassOrientation[]>
                {
                    // X X X
                    // X _ _
                    // X _ _
                    new []
                    {
                        CompassOrientation.NorthWest,
                        CompassOrientation.North,
                        CompassOrientation.NorthEast,
                        CompassOrientation.West,
                        CompassOrientation.SouthWest
                    },
                    // X X _
                    // X _ _
                    // X _ _
                    new []
                    {
                        CompassOrientation.NorthWest,
                        CompassOrientation.North,
                        CompassOrientation.West,
                        CompassOrientation.SouthWest
                    },
                    // X X X
                    // X _ _
                    // _ _ _
                    new []
                    {
                        CompassOrientation.NorthWest,
                        CompassOrientation.North,
                        CompassOrientation.NorthEast,
                        CompassOrientation.West
                    },
                    // _ X _
                    // X _ _
                    // _ _ _
                    new []
                    {
                        //CompassOrientation.NorthWest, // Not actually required.
                        CompassOrientation.North,
                        CompassOrientation.West
                    }
                }
            },
            {
                TileConfiguration.Wall,
                new List<CompassOrientation[]>
                {
                    // X X X
                    // _ _ _
                    // _ _ _
                    new []
                    {
                        CompassOrientation.NorthWest,
                        CompassOrientation.North,
                        CompassOrientation.NorthEast
                    },
                    // X X _
                    // _ _ _
                    // _ _ _
                    new []
                    {
                        CompassOrientation.NorthWest,
                        CompassOrientation.North
                    },
                    // _ X X
                    // _ _ _
                    // _ _ _
                    new []
                    {
                        CompassOrientation.North,
                        CompassOrientation.East
                    },
                    // _ X _
                    // _ _ _
                    // _ _ _
                    new []
                    {
                        CompassOrientation.North
                    }
                }
            },
            {
                TileConfiguration.InternalCorner,
                // X _ _
                // _ _ _
                // _ _ _
                CompassOrientation.NorthWest.EnqueueInArray().EnqueueInList()
            }
        };
    }
}