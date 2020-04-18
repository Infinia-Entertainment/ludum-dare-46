using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static WeaponData;

public class StaffWeapon : MonoBehaviour
{
    [SerializeField] public GameObject rodAttachment, attackTypeAttachment, elementTypeAttachment;

    [SerializeField] public List<GameObject> buffAttachments;

    private int baseDamage = 10; //for now
    private int adjustedDamage;

    private WeaponType _weaponType;
    private WeaponElement _weaponElement;
    //private WeaponBuffs[] _weaponBuffs = new WeaponBuffs[3];

    public void InitializeWeapon(WeaponType weaponType, WeaponElement weaponElement)
    {
        this._weaponType = weaponType;
        this._weaponElement = weaponElement;
        //_weaponBuffs = weaponBuffs;

        ProcessBuffEffects();
    }

    private void ProcessBuffEffects()
    {
        //Debug.Log("Processed Buff effects");
    }

    public void DoAttack()
    {
        switch (_weaponType)
        {
            case WeaponType.Melee:

                //Do melee stuff

                break;
            case WeaponType.Ranged:

                //Do ranged stuff, spawn projectile n' stuff

                break;
            default:
                break;
        }
    }

    public int CalculateDamage(WeaponElement monsterElementType)
    {
        //Compare weapon type and monster type


        switch (_weaponElement)
        {
            case WeaponElement.None:
                break;
            case WeaponElement.Fire:
                break;
            case WeaponElement.Wind:
                break;
            case WeaponElement.Eearth:
                break;
            case WeaponElement.Ice:
                break;
            case WeaponElement.Electricity:
                break;
            default:
                break;
        }

        return baseDamage;
    }
}
