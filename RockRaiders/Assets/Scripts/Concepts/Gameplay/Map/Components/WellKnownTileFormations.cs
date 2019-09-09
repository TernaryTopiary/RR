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
                    // _ _ X
                    // _ _ X
                    // X X X
                    new []
                    {
                        CompassOrientation.NorthEast,
                        CompassOrientation.East,
                        CompassOrientation.SouthEast,
                        CompassOrientation.South,
                        CompassOrientation.SouthWest
                    },
                    // _ _ X
                    // _ _ X
                    // _ X X
                    new []
                    {
                        CompassOrientation.NorthEast,
                        CompassOrientation.East,
                        CompassOrientation.SouthEast,
                        CompassOrientation.South
                    },
                    // _ _ _
                    // _ _ X
                    // X X X
                    new []
                    {
                        CompassOrientation.East,
                        CompassOrientation.SouthEast,
                        CompassOrientation.South,
                        CompassOrientation.SouthWest
                    },
                    // _ _ _
                    // _ _ X
                    // _ X X
                    new []
                    {
                        CompassOrientation.SouthEast, // Not actually required.
                        CompassOrientation.East,
                        CompassOrientation.South
                    },
                    // _ _ _
                    // _ _ X
                    // _ X _
                    new []
                    {
                        CompassOrientation.East,
                        CompassOrientation.South
                    }
                }
            },
            {
                TileConfiguration.Wall,
                new List<CompassOrientation[]>
                {
                    // _ _ _
                    // _ _ _
                    // X X X
                    new []
                    {
                        CompassOrientation.SouthWest,
                        CompassOrientation.South,
                        CompassOrientation.SouthEast
                    },
                    // _ _ _
                    // _ _ _
                    // _ X X
                    new []
                    {
                        CompassOrientation.SouthEast,
                        CompassOrientation.South
                    },
                    // _ _ _
                    // _ _ _
                    // X X _
                    new []
                    {
                        CompassOrientation.South,
                        CompassOrientation.SouthWest
                    },
                    // _ _ _
                    // _ _ _
                    // _ X _
                    new []
                    {
                        CompassOrientation.South
                    }
                }
            },
            {
                TileConfiguration.InternalCorner,
                // _ _ _
                // _ _ _
                // _ _ X
                CompassOrientation.SouthEast.EnqueueInArray().EnqueueInList()
            }
        };
    }
}