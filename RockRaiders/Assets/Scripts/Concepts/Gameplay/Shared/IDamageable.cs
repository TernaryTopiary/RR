namespace Assets.Scripts.Concepts.Gameplay.Shared
{
    public interface IDamageable
    {
        int MaxHitpoints { get; set; }
        int CurrentHitpoints { get; set; }
    }
}