using UnityEngine;

public interface ICharacter
{
    bool alive { get; }
    void Die();
}