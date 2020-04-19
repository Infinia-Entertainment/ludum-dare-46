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

    Time lastAttack;

    private void Awake()
    {
        weapon = weaponPrefab.GetComponent<StaffWeapon>();
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

        Debug.Log();
    }

    //!!!!!!!!!!!Do anims here!!!!!!!!!
    private void CheckForAttackRange()
    {
        float distanceToEnemy;

        RaycastHit hitInfo;
        Physics.Raycast( transform.position,Vector3.right,out hitInfo, 10, 1 << 8); //8 is Monster layer

        if (!Physics.Raycast(transform.position, Vector3.right, out hitInfo, 10, 1 << 8))
        {
            return;
        }

        //Just checks if attack is within range
        switch (weapon.WeaponType)
        {
            case WeaponType.Melee:

                if (hitInfo.distance <= 3)
                {
                    weapon.DoAttack();
                }

                break;
            case WeaponType.Ranged:

                if (hitInfo.distance <= 8)
                {
                    weapon.DoAttack();
                }
                break;
            default:
                break;
        }
    }
}
