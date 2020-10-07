using System;
using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    private Animator animator;
    private Transform fighter; //Fighter is the character who carries this weapon
    private int currentDamage;
    private bool isDealingDamage; //True = Collider Trigger to Deal Damage
    [SerializeField] private int _baseDamage; //Unity Inspector Variable for Base Damage (Interface)
    public int baseDamage
    {
        get { return _baseDamage; }
    }

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        fighter = animator.transform;
    }


    public void Attack(int damage)
    {
        this.currentDamage = damage;
        animator.SetTrigger("Attack_normal");
    }

    public void SpecialAttack()
    {
        //ToDo: Add Special Attack
    }

    public void AllowDamage(bool d)
    {
        isDealingDamage = d;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDealingDamage) return;
        CharacterStats target = other.GetComponent<CharacterStats>();
        if (target != null && other.transform != fighter)
        {
            target.TakeDamage(currentDamage);
            AllowDamage(false);
        }
    }
}