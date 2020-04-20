using GameData;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : AliveUnit
{
    [SerializeField] GameObject weaponPrefab;
    private StaffWeapon weapon;

    float lastAttack;

    private void Awake()
    {
        health = 20;
        weaponPrefab = Instantiate(weaponPrefab,transform);

        weapon = weaponPrefab.GetComponent<StaffWeapon>();
        weapon.TestInitialize();

        lastAttack = Time.time;
    }


    private void Attack()
    {
        weapon.DoAttack();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForHealth();
        CheckForAttackRange();

        //Debug.Log(weapon.WeaponType);
    }

    //!!!!!!!!!!!Do anims here!!!!!!!!!
    private void CheckForAttackRange()
    {
        RaycastHit hitInfo;

        if (!Physics.Raycast(transform.position, Vector3.right, out hitInfo, 10, 1 << 12)) // 1 << 12 Monster Layer
        {
            return;
        }

        //Check for attack Rate
        if (Time.time - lastAttack >= weapon.AttackRate)
        {

            //Just checks if attack is within range
            if (weapon.WeaponType == WeaponType.Melee && hitInfo.distance <= 2)
            {
                weapon.DoAttack();
            }
            if (weapon.WeaponType == WeaponType.Ranged && hitInfo.distance <= 8)
            {
                weapon.DoAttack();
            }
            lastAttack = Time.time;
        }
    }
}
