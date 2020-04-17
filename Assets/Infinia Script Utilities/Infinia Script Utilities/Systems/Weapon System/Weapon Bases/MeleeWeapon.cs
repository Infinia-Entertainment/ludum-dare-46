using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

public class MeleeWeapon : IWeapon
{
    
    public int BaseAttackDamage { get; set; }
    public float WeaponKnockback { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public WeaponState WeaponState { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    int IWeapon.BaseAttackDamage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    float IWeapon.WeaponKnockback { get; set; }
    WeaponState IWeapon.WeaponState { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    List<WeaponAction> IWeapon.WeaponActions { get; set; }

    public float attackRange;
    public int attackRate;

    public void Initialize()
    {
        throw new System.NotImplementedException();
    }

    public virtual void DoAttack()
    {
    }


    public virtual void GuardThing()
    {
    }

    public void Reload()
    {
        throw new System.NotImplementedException();
    }

    void IWeapon.Initialize()
    {
        throw new NotImplementedException();
    }

    void IWeapon.Reload()
    {
        throw new NotImplementedException();
    }
    
}
