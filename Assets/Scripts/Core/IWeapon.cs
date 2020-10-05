public interface IWeapon
{
    Stat damage { get; }
    int baseDamage { get; set; }

    void Attack(int damage);
    void SpecialAttack();
}