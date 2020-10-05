using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    [SerializeField] public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public Stat armor;
    public Stat damage;


    void Start()
    {
        currentHealth = maxHealth;
    }

    public void Heal(int heal)
    {
        if (heal >= 0)
        {
            currentHealth += heal;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        ICharacter character = GetComponent<ICharacter>();
        character?.Die();
    }
}