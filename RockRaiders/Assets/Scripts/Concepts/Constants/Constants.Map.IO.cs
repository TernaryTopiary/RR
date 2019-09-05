using System.Collections.Generic;
using Assets.Scripts.Concepts.Gameplay.Map.TileType;
using Assets.Scripts.Concepts.Gameplay.Map.TileType.Ground;
using Assets.Scripts.Concepts.Gameplay.Map.TileType.Wall;

namespace Assets.Scripts.Concepts.Constants
{
    public static partial class Constants
    {
        public static readonly byte MapWidthIndex = 2;
        public static readonly byte MapHeightIndex = 3;
        public static readonly byte MapTileTypeIndex = 1;
        public static readonly byte MapTileAverageHeightIndex = 4;

        private static readonly Dictionary<int, ITileType> TileClassLookup = new Dictionary<int, ITileType>
        {
            {0, TileWallSoil.GetInstance() },
            {2, TileWallDirt.GetInstance() },
            {3, TileWallLooseRock.GetInstance() },
            {4, TileWallHardRock.GetInstance() },
            {5, TileWallSolidRock.GetInstance() },
            {26, TileWallOreSeam.GetInstance() },
            {13, TileWallEnergyCrystalSeam.GetInstance() },
            {41, TileWallEnergyCrystalRegeneratorSeam.GetInstance() },
            {19, TileGroundSlimySlugHole.GetInstance() },
            {27, TileGroundWater.GetInstance() },
            {28, TileGroundLava.GetInstance() }
        };

        public static ITileType GetTileType(int tileTypeId) => TileClassLookup[tileTypeId];
    }
}