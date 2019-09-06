using Assets.Scripts.Concepts.Cosmic.Time;

namespace Assets.Scripts.Concepts.Gameplay.Shared
{
    public interface ITakeTimeToCreateable : ICreatable
    {
        Seconds TimeToCreate { get; set; }
    }
}