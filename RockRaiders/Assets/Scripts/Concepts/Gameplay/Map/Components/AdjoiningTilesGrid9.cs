using System;
using Assets.Scripts.Concepts.Cosmic.Space;
using Assets.Scripts.Extensions;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Concepts.Cosmic.Array;

namespace Assets.Scripts.Concepts.Gameplay.Map.Components
{
    public class AdjoiningTilesGrid9 : AdjoiningGrid9<Tile>
    {
        public AdjoiningTilesGrid9() : base()
        {
        }

        public AdjoiningTilesGrid9(IEnumerable<KeyValuePair<CompassOrientation, Tile>> adjoiningTiles) : base(adjoiningTiles)
        {
        }

        public bool SubsetMeetsCriteria(Func<Tile, bool> filter, params CompassOrientation[] orientationsToCheck)
        {
            if (!orientationsToCheck.Any()) throw new ArgumentException("No orientations to check.");
            return orientationsToCheck.Distinct().All(orientation => filter(this[orientation]));
        }
    }
}