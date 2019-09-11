using System;
using Assets.Scripts.Concepts.Cosmic.Space;
using Assets.Scripts.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Concepts.Cosmic.Array
{
    public class AdjoiningGrid4<T> : Dictionary<CornerOrientation, T>
    {
        public AdjoiningGrid4()
        {
        }

        public AdjoiningGrid4(IEnumerable<KeyValuePair<CornerOrientation, T>> adjoiningTiles)
        {
            foreach (var tile in adjoiningTiles) this[tile.Key] = tile.Value;
        }

        public AdjoiningGrid4(IEnumerable<T> adjoiningTiles)
        {
            foreach (var tile in adjoiningTiles.Select((tile, index) => new KeyValuePair<CornerOrientation, T>(((CornerOrientation)index), tile))) this[tile.Key] = tile.Value;
        }
    }
}