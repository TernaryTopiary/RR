using System;
using Assets.Scripts.Concepts.Cosmic.Space;
using Assets.Scripts.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Concepts.Gameplay.Map.Components
{
    public class AdjoiningTilesGrid9
    {
        public List<Tile> AdjoiningTiles { get; } = new List<Tile>(9);

        public Tile Center
        {
            get { return AdjoiningTiles[(int)CompassOrientation.None]; }
        }

        public IEnumerable<Tile> Adjoining
        {
            get { return AdjoiningTiles.Except(Center); }
        }

        public AdjoiningTilesGrid9(IEnumerable<Tile> adjoiningTiles)
        {
            AdjoiningTiles.AddRange(adjoiningTiles.Take(9));
        }

        public Tile this[CompassOrientation orientation]
        {
            get { return AdjoiningTiles[(int)orientation]; }
        }

        public AdjoiningTilesGrid4 this[CornerOrientation orientation]
        {
            get { return new AdjoiningTilesGrid4(orientation.ToCandidateOrientations().Select(o => AdjoiningTiles[(int)o])); }
        }

        public IEnumerable<Tile> GetByOrientation(params CompassOrientation[] orientations)
        {
            var uniqueOrientations = orientations.Distinct();
            return uniqueOrientations.Select(o => this[o]);
        }

        public bool SubsetMeetsCriteria(Func<Tile, bool> filter, params CompassOrientation[] orientationsToCheck)
        {
            var indicies = orientationsToCheck.Select(o => (int) o).Distinct();
            if (!indicies.Any()) throw new ArgumentException("No orientations to check.");
            return indicies.All(index => filter(AdjoiningTiles[index]));
        }

        public AdjoiningTilesGrid9 Clone()
        {
            return new AdjoiningTilesGrid9(AdjoiningTiles.ToArray());
        }

        public AdjoiningTilesGrid9 Rotate(RotationalOrientation rotationalOrientation, byte amount = 90)
        {
            switch (amount)
            {
                case 90:
                {
                    return new AdjoiningTilesGrid9(rotationalOrientation == RotationalOrientation.Clockwise ?
                        new[]
                        {
                            this[CompassOrientation.SouthWest],
                            this[CompassOrientation.West],
                            this[CompassOrientation.NorthWest],
                            this[CompassOrientation.South],
                            this[CompassOrientation.None],
                            this[CompassOrientation.North],
                            this[CompassOrientation.SouthEast],
                            this[CompassOrientation.East],
                            this[CompassOrientation.NorthEast]
                        } :
                        new[]
                        {
                            this[CompassOrientation.NorthEast],
                            this[CompassOrientation.East],
                            this[CompassOrientation.SouthEast],
                            this[CompassOrientation.North],
                            this[CompassOrientation.None],
                            this[CompassOrientation.South],
                            this[CompassOrientation.NorthWest],
                            this[CompassOrientation.West],
                            this[CompassOrientation.SouthWest]
                        });
                    }
                default:
                throw new NotImplementedException();
            }
        }
    }
}