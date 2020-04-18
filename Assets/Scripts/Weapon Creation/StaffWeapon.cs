using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static WeaponData;

public class StaffWeapon : MonoBehaviour
{
    [SerializeField] public GameObject rodAttachment, attackTypeAttachment, ElementTypeAttachment;

    [SerializeField] public List<GameObject> buffAttachments;


    private int baseDamage = 10; //for now

    private WeaponType weaponType;
    private WeaponElement weaponElement;
    private WeaponBuffs[] weaponBuffs = new WeaponBuffs[3];

    public void InitializeWeapon(WeaponType weaponType, WeaponElement weaponElement, params WeaponBuffs[] weaponBuffs)
    {
        this.weaponType = weaponType;
        this.weaponElement = weaponElement;
        weaponBuffs = weaponBuffs;
    }

    public int CalculateDamage()
    {
        switch (weaponElement)
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
