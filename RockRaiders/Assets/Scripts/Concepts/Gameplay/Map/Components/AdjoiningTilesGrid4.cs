using System;
using Assets.Scripts.Concepts.Cosmic.Space;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Concepts.Gameplay.Map.Components
{
    public class AdjoiningTilesGrid4
    {
        public List<Tile> Tiles { get; } = new List<Tile>(4);

        public AdjoiningTilesGrid4(IEnumerable<Tile> adjoiningTiles)
        {
            Tiles.AddRange(adjoiningTiles.Take(4));
        }

        public AdjoiningTilesGrid4()
        {
        }

        public Tile this[CornerOrientation orientation]
        {
            get { return Tiles[(int)orientation]; }
        }

        public AdjoiningTilesGrid4 Rotate(RotationalOrientation rotationalOrientation)
        {
            return new AdjoiningTilesGrid4(rotationalOrientation == RotationalOrientation.Clockwise ?
                new[]
                {
                    this[CornerOrientation.SouthWest],
                    this[CornerOrientation.NorthWest],
                    this[CornerOrientation.SouthEast],
                    this[CornerOrientation.NorthEast]
                } :
                new[]
                {
                    this[CornerOrientation.NorthEast],
                    this[CornerOrientation.SouthEast],
                    this[CornerOrientation.NorthWest],
                    this[CornerOrientation.SouthWest]
                });
        }
    }
}