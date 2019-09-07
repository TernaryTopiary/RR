﻿using System;
using Assets.Scripts.Concepts.Cosmic.Space;
using Assets.Scripts.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Concepts.Gameplay.Map.Components
{
    public class AdjoiningTilesGrid9 : Dictionary<CompassOrientation, Tile>
    {
        public Tile Center
        {
            get { return this[CompassOrientation.None]; }
        }

        public IEnumerable<Tile> Adjoining
        {
            get { return Values.Except(Center); }
        }

        public AdjoiningTilesGrid9(IEnumerable<KeyValuePair<CompassOrientation, Tile>> adjoiningTiles)
        {
            foreach(var tile in adjoiningTiles) this[tile.Key] = tile.Value;
        }

        public AdjoiningTilesGrid4 this[CornerOrientation orientation]
        {
            get { return new AdjoiningTilesGrid4(orientation.ToCandidateOrientations().Select(subOrientation => this[subOrientation])); }
        }

        public IEnumerable<Tile> GetByOrientation(params CompassOrientation[] orientations)
        {
            var uniqueOrientations = orientations.Distinct();
            return uniqueOrientations.Select(o => this[o]);
        }

        public bool SubsetMeetsCriteria(Func<Tile, bool> filter, params CompassOrientation[] orientationsToCheck)
        {
            if (!orientationsToCheck.Any()) throw new ArgumentException("No orientations to check.");
            return orientationsToCheck.Distinct().All(orientation => filter(this[orientation]));
        }

        public AdjoiningTilesGrid9 Clone()
        {
            return new AdjoiningTilesGrid9(this);
        }

        public AdjoiningTilesGrid9 Rotate(RotationalOrientation rotationalOrientation, byte amount = 90)
        {
            switch (amount)
            {
                case 90:
                {
                    return new AdjoiningTilesGrid9(this.Select(kv => new KeyValuePair<CompassOrientation, Tile>(kv.Key.Rotate(rotationalOrientation).Rotate(rotationalOrientation), kv.Value)));
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