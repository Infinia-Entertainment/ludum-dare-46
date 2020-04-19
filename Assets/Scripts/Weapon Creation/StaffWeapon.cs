using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GameData;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class StaffWeapon : MonoBehaviour
{

    //Prefab references
    [SerializeField] public GameObject rodAttachment, attackTypeAttachment, elementTypeAttachment;
    [SerializeField] public List<GameObject> buffAttachments;

    //prefab for the projectile of ranged weapons
    [SerializeField] private MagicProjectile projectilePrefab;

    //Damage info
    [SerializeField] private int _baseDamage = 10; //for now
    [SerializeField] private int _adjustedToWeaponDamage;

    [Range(0.0f,2.0f)]
    [SerializeField] private float elementDamageModifier = 0.5f;

    //Other weapon info
    [SerializeField] private float attackRate = 1f; //in seconds
    [SerializeField] private int defenceModifier; //for now

    //[SerializeField] private float knockback; ??

    private WeaponType _weaponType;
    private ElementAttribute _weaponElement;

    public WeaponType WeaponType { get => _weaponType;}
    public ElementAttribute WeaponElement { get => _weaponElement;}

    //private WeaponBuffs[] _weaponBuffs = new WeaponBuffs[3];


    private void Awake()
    {
        
    }


    #region Test Code Only
    public void TestInitialize()
    {
        InitializeWeapon(WeaponType.Melee, ElementAttribute.Ice);
        Debug.Log("initialized");
    }
    #endregion

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
        RaycastHit hitInfo;
        Physics.Raycast(transform.position, Vector3.right, out hitInfo, 10, 1 << 12); //12 is Monster layer

        switch (_weaponType)
        {
            case WeaponType.Melee:


                //Not timed with animation rn
                //Then enable/disable collider or something with the animation
                Debug.Log(hitInfo.collider);
                Debug.Log(hitInfo.collider.gameObject);
                Debug.Log(hitInfo.collider.gameObject.GetComponent<AliveUnit>());

                hitInfo.collider.gameObject.GetComponent<AliveUnit>().ReceiveDamage(CalculateDamage(_weaponElement));


                break;
            case WeaponType.Ranged:

                //Not timed with animation rn,
                //Need an animation value to trigger the attack or something
                MagicProjectile projectile = Instantiate(projectilePrefab,elementTypeAttachment.transform.position,projectilePrefab.transform.rotation);
                projectile.damage = CalculateDamage(_weaponElement);



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
