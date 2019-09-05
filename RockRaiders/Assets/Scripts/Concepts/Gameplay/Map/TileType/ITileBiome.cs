using Assets.Scripts.Miscellaneous;

namespace Assets.Scripts.Concepts.Gameplay.Map.TileType
{
    public interface ITileBiome
    {
        TileBiome BiomeReference { get; }
        string BiomeNameReference { get; }
    }

    public class TileBiomeRock : Singleton<TileBiomeRock>, ITileBiome
    {
        public TileBiome BiomeReference { get; } = TileBiome.Rock;
        public string BiomeNameReference { get; } = TileBiome.Rock.ToString().ToUpper();
    }

    public class TileBiomeIce : Singleton<TileBiomeIce>, ITileBiome
    {
        public TileBiome BiomeReference { get; } = TileBiome.Ice;
        public string BiomeNameReference { get; } = TileBiome.Ice.ToString().ToUpper();
    }

    public class TileBiomeLava : Singleton<TileBiomeLava>, ITileBiome
    {
        public TileBiome BiomeReference { get; } = TileBiome.Lava;
        public string BiomeNameReference { get; } = TileBiome.Lava.ToString().ToUpper();
    }

    public enum TileBiome
    {
        Lava,
        Ice,
        Rock
    }
}