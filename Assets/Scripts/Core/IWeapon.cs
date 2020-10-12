public interface IWeapon
{
    int baseDamage { get; }

    void Attack(int damage);
    void SpecialAttack();
    void AllowDamage(bool d);
}