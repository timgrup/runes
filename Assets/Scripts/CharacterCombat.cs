using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    public IWeapon equippedWeapon { get; private set; }
    public Stat damage { get; private set; }
    private bool isAttacking;

    private void Start()
    {
        damage = new Stat();
        IWeapon weapon = GetComponentInChildren<IWeapon>();
        if (weapon != null)
        {
            EquipWeapon(weapon);
            Debug.Log("Weapon found");
        }
        else
        {
            Debug.Log(transform.name + ": no weapon found");
        }
    }

    public void Attack()
    {
        if (isAttacking) return; //If Animator already triggered attacking event return
        int damage = Mathf.FloorToInt(this.damage.GetValue());
        equippedWeapon.Attack(damage);
    }

    public void EquipWeapon(IWeapon weapon)
    {
        if (weapon != null && equippedWeapon != weapon)
        {
            equippedWeapon = weapon;
            damage.baseValue = weapon.baseDamage;
            Debug.Log("Weapon Base Damage: " + weapon.baseDamage);
        }
    }

    public void UnequiupWeapon()
    {
        equippedWeapon = null;
        damage.baseValue = 0;
    }
    
    public void SetAttacking()
    {
        this.isAttacking = true;
    }

    public void SetNotAttacking()
    {
        this.isAttacking = false;
    }

    public void AllowWeaponDamage()
    {
        equippedWeapon.AllowDamage(true);
    }
    
    public void AllowNoWeaponDamage()
    {
        equippedWeapon.AllowDamage(false);
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }
}
