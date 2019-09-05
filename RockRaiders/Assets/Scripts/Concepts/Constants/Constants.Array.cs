using Assets.Scripts.Concepts.Cosmic.Array;

namespace Assets.Scripts.Concepts.Constants
{
    public static partial class Constants
    {
        public static readonly Index2 OffsetNorth = new Index2(0, -1);
        public static readonly Index2 OffsetNorthEast = new Index2(1, -1);
        public static readonly Index2 OffsetEast = new Index2(1);
        public static readonly Index2 OffsetSouthEast = new Index2(1, 1);
        public static readonly Index2 OffsetSouth = new Index2(0, 1);
        public static readonly Index2 OffsetSouthWest = new Index2(-1, 1);
        public static readonly Index2 OffsetWest = new Index2(-1);
        public static readonly Index2 OffsetNorthWest = new Index2(-1, -1);
    }
}