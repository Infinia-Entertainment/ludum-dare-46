using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GameData;
using Sirenix.OdinInspector;

public class StaffWeapon : MonoBehaviour
{

    //Prefab references
    [SerializeField] public GameObject rodAttachment, attackTypeAttachment, elementTypeAttachment;
    [SerializeField] public List<GameObject> buffAttachments;

    //Damage info
    [SerializeField] private int _baseDamage = 10; //for now
    [SerializeField] private int _adjustedToWeaponDamage;

    [Range(0.0f,2.0f)]
    [SerializeField] private float elementDamageModifier = 0.5f;

    //Other weapon info
    [SerializeField] private float attackRate = 1f; //in seconds

    //[SerializeField] private float knockback; ??

    private WeaponType _weaponType;
    private ElementAttribute _weaponElement;
    //private WeaponBuffs[] _weaponBuffs = new WeaponBuffs[3];

    public void InitializeWeapon(WeaponType weaponType, ElementAttribute weaponElement)
    {
        _weaponType = weaponType;
        _weaponElement = weaponElement;
        //_weaponBuffs = weaponBuffs;


        switch (_weaponType)
        {
            case WeaponType.Melee:

                _adjustedToWeaponDamage = _baseDamage + 20;

                break;
            case WeaponType.Ranged:

                _adjustedToWeaponDamage = _baseDamage + 5;

                break;
            default:
                break;
        }

        //ProcessBuffEffects();
    }

    //private void ProcessBuffEffects()
    //{
        //Debug.Log("Processed Buff effects");
    //}

    public void DoAttack()
    {
        switch (_weaponType)
        {
            case WeaponType.Melee:

                //Do melee stuff

                //Does more damage but less speed and no range

                CalculateDamage(_weaponElement );

                break;
            case WeaponType.Ranged:

                //Do ranged stuff, spawn projectile n' stuff

                //Does less damage but more speed and has range

                CalculateDamage(_weaponElement);

                break;
            default:
                break;
        }
    }

    public int CalculateDamage(ElementAttribute monsterElementType)
    {
        //Compare weapon type and monster type
        float adjustedDamage = _adjustedToWeaponDamage;

        switch (_weaponElement)
        {
            case ElementAttribute.Void:

                // Neutral to all
                // So do nothing for now

                //adjustedDamage = _baseDamage;

                break;
            case ElementAttribute.Fire:

                //Strong against Earth (or plants)

                if (_weaponElement == ElementAttribute.Earth)
                {
                    adjustedDamage *= (1 - elementDamageModifier);
                }

                break;
            case ElementAttribute.Earth:

                //Strong against Lightning

                if (_weaponElement == ElementAttribute.Lightning)
                {
                    adjustedDamage *= (1 - elementDamageModifier);
                }

                break;
            case ElementAttribute.Ice:

                //Strong against Fire

                if (_weaponElement == ElementAttribute.Fire)
                {
                    adjustedDamage *= (1 - elementDamageModifier);
                }

                break;
            case ElementAttribute.Lightning:

                //Strong against Ice

                if (_weaponElement == ElementAttribute.Ice)
                {
                    adjustedDamage *= (1 - elementDamageModifier);
                }

                break;
            default:
                break;
        }

        return Mathf.RoundToInt(adjustedDamage);
    }
}
