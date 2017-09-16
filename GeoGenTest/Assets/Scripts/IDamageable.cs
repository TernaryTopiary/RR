public interface IDamageable 
{
    int GetHealth { get; }

    int GetMaxHealth { get; }

    bool IsAlive { get; }

    bool IsDamageable { get; }

    void Damage(int amount);

    void Heal(int amount);

    void Kill();
}
