using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public enum WeaponState
    {
        NoBullets,
        OnAttackCooldown,
        WeaponJammed,
    }

    public delegate void WeaponActionEvent();
    public struct WeaponAction
    {
        internal KeyCode actionKey;

        internal WeaponActionEvent actionEvent;
        //This would later be refactored to check the Input manager for the action key

    }

    public interface IWeapon
    {
        int BaseAttackDamage { get; set; }
        float WeaponKnockback { get; set; }
        WeaponState WeaponState { get; set; }
        List<WeaponAction> WeaponActions { get; set; }

        void Initialize();
        void Reload();
    }

}