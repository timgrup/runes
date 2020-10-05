using System;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    public Stat damage { get; }
    public int baseDamage { get; set; }

    private void Start()
    {
        baseDamage = 50;
    }

    public void Attack(int damage)
    {
        //ToDo: PlayAttackAnim
    }

    public void SpecialAttack()
    {
        //ToDo: Add Special Attack
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterStats enemy = other.GetComponent<CharacterStats>();
        if (enemy != null)
        {
            enemy.TakeDamage(baseDamage);
            Debug.Log(enemy.currentHealth);
        }
    }
}