public interface IDamageable {
    float MaxHealth { get; }
    float Health { get; }

    void TakeDamage(Damage damage);
}