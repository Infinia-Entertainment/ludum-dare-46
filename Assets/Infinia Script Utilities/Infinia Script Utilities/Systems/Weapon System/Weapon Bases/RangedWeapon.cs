using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Serialization;
using WeaponSystem.Projectile;
using WeaponSystem;

namespace Weapon.WeaponBases
{
    public class RangedWeapon : IWeapon
    {
        [OdinSerialize] public ProjectileBase Projectile { get; set; }
        [OdinSerialize] public int RateOfFire { get; set; }
        public int BaseAttackDamage { get; set; }
        public float WeaponKnockback { get; set; }
        public WeaponState WeaponState { get; set; }
        public int MagazineSize { get; set; }
        public float Velocity { get; set; }

        public Action OnShoot { get; set; }
        public Action OnReload { get; set; }

        public bool CanShoot => LastShot + 1f / RateOfFire <= Time.time && CurrentAmmoCount > 0;
        public bool CanReload => true;

        [OdinSerialize] public float ReloadTime { get; set; }
        [OdinSerialize] public Transform Position { get; set; }
        public List<WeaponAction> WeaponActions { get { return weaponActions; } set { weaponActions = value; } }

        protected List<WeaponAction> weaponActions = new List<WeaponAction>();

        [OdinSerialize] public bool hasInfiniteAmmo = false;

        [OdinSerialize] protected float LastShot;
        [OdinSerialize] protected int TotalAmmoCount;
        [OdinSerialize] protected int CurrentAmmoCount;

        public virtual void Initialize()
        {
            /**
            * the actionKey should be passed down from an Input class or something
            * It's quite awkward to do it here though tbh, so what the fuck should i do? 
            * (╯°□°）╯︵ ┻━┻
            * 
            * An alternative would be creating WeaponActions outside and just passing them down
            * Which would be cleaner here but would sometimes require longer names like LaserPreChargeGunAAction
            * Although that wouldn't happen most of the time tbh, more so it wouldn't be specific to a gun but who the fuck knows
            **/

            // TODO: Move the WeaponActions outside and just add them without creating them here

            WeaponAction reloadAction = new WeaponAction();
            reloadAction.actionKey = KeyCode.R;
            reloadAction.actionEvent += new WeaponActionEvent(Reload);

            WeaponAction ShootAction = new WeaponAction();
            ShootAction.actionKey = KeyCode.Mouse0;
            ShootAction.actionEvent += new WeaponActionEvent(Shoot);

            WeaponActions.Add(reloadAction);
            WeaponActions.Add(ShootAction);


            Debug.Log("Weapon: " + this.GetType().Name + " is Initialized");
        }

        public virtual void Reload()
        {
            //Don't reload more than we need to
            //(p.s in some other systems you throw out your whole magazine, so that'd be implemented somehow else lmao)
            int amountToReaload = MagazineSize - CurrentAmmoCount;

            if (TotalAmmoCount >= amountToReaload)
            {
                TotalAmmoCount -= amountToReaload;
                CurrentAmmoCount = MagazineSize;
            }
            else
            {
                CurrentAmmoCount += TotalAmmoCount;
                TotalAmmoCount = 0;
            }

            OnReload?.Invoke();
        }

        public virtual void Shoot()
        {
        }

        public virtual ProjectileBase GetProjectile()
        {
            return Projectile;
        }
    }
}