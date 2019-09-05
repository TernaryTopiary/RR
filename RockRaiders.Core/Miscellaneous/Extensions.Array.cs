using Assets.Scripts.Concepts.Cosmic.Array;

namespace Assets.Scripts.Miscellaneous
{
    public static partial class Extensions
    {
        public static Index2 GetItemPositionIn<TArrayType>(this TArrayType member, TArrayType[,] array)
        {
            var w = array.GetLength(0); // width
            var h = array.GetLength(1); // height
            for (var x = 0; x < w; ++x)
            {
                for (var y = 0; y < h; ++y)
                {
                    if (array[x, y].Equals(member)) return new Index2(x, y);
                }
            }
            return new Index2(-1, -1);
        }

        public static bool IsValidPositionInArray<TArrayType>(this TArrayType[,] array, Index2 position) =>
            position.X >= 0 && position.X < array.GetLength(0) && position.Y >= 0 && position.Y < array.GetLength(1);

        public static TArrayType GetOffsetCell<TArrayType>(this TArrayType[,] array, TArrayType member, Index2 offset)
            where TArrayType : class
        {
            var position = member.GetItemPositionIn(array);
            var newPosition = new Index2(position.X - offset.X, position.Y - offset.Y);
            return array.IsValidPositionInArray(newPosition) ? array[newPosition.X, newPosition.Y] : null;
        }
    }
}