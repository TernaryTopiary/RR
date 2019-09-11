using System;
using Assets.Scripts.Concepts.Cosmic.Space;
using Assets.Scripts.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Concepts.Cosmic.Array
{
    public class AdjoiningGrid9<T> : Dictionary<CompassOrientation, T>
    {
        public T Center
        {
            get { return this[CompassOrientation.None]; }
        }

        public IEnumerable<T> Adjoining
        {
            get { return Values.Except(Center); }
        }

        public AdjoiningGrid9()
        {
        }

        public AdjoiningGrid9(IEnumerable<KeyValuePair<CompassOrientation, T>> adjoiningTiles)
        {
            foreach (var tile in adjoiningTiles) this[tile.Key] = tile.Value;
        }

        public AdjoiningGrid4<T> this[CornerOrientation orientation]
        {
            get { return new AdjoiningGrid4<T>(orientation.ToCandidateOrientations().Select(subOrientation => this[subOrientation])); }
        }

        public IEnumerable<T> GetByOrientation(params CompassOrientation[] orientations)
        {
            var uniqueOrientations = orientations.Distinct();
            return uniqueOrientations.Select(o => this[o]);
        }

        public AdjoiningGrid9<T> Clone()
        {
            return new AdjoiningGrid9<T>(this);
        }

        public AdjoiningGrid9<T> Rotate(RotationalOrientation rotationalOrientation, byte amount = 90)
        {
            switch (amount)
            {
                case 90:
                {
                    return new AdjoiningGrid9<T>(this.Select(kv => new KeyValuePair<CompassOrientation, T>(kv.Key.Rotate(rotationalOrientation).Rotate(rotationalOrientation), kv.Value)));
                }
                default:
                    throw new NotImplementedException();
            }
        }

        public override string ToString()
        {
            return string.Join(",\n", this.Select(kv => $"{kv.Key}, {kv.Value.ToString()}").ToList());
        }
    }
}