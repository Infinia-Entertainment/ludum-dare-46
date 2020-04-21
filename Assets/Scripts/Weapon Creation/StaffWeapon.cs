using GameData;
using System.Collections.Generic;
using UnityEngine;
using static GameData.GameFunctions;

public class StaffWeapon : MonoBehaviour
{
    
    //[SerializeField] public GameObject heroOwner;

    //Prefab references
    [SerializeField] public GameObject rodAttachment, attackTypeAttachment, elementTypeAttachment;

    [SerializeField] public List<GameObject> buffAttachments;

    //prefab for the projectile of ranged weapons
    [SerializeField] private MagicProjectile projectilePrefab;

    //Damage info
    [SerializeField] private int _baseDamage = 10; //for now

    [SerializeField] private int _adjustedToWeaponDamage;

    //[Range(0.0f, 2.0f)]
    [SerializeField] private float _elementDamageModifier = 0.5f;

    //Other weapon info
    [SerializeField] private float _attackRate = 2f; //in seconds
    [SerializeField] private float _attackRange;

    [SerializeField] private int defenceModifier; //for now

    //[SerializeField] private float knockback; ??

    private WeaponType _weaponType;
    private ElementAttribute _weaponElement;

    public WeaponType WeaponType { get => _weaponType; }
    public ElementAttribute WeaponElement { get => _weaponElement; }
    public float AttackRate { get => _attackRate; }

    //private WeaponBuffs[] _weaponBuffs = new WeaponBuffs[3];

    #region Test Code Only

    public void TestInitialize()
    {
        _weaponType = WeaponType.Melee;
        _weaponElement = ElementAttribute.Ice;
        Debug.Log("initialized");
    }

    #endregion Test Code Only

    public void InitializeWeapon(WeaponType weaponType, ElementAttribute weaponElement,int damage, int defence, float fireRate,float range,float elementDamageModifier)
    {
        _weaponType = weaponType;
        _weaponElement = weaponElement;
        //_weaponBuffs = weaponBuffs;

        _baseDamage = damage;
        //int defence;
        _attackRate =  fireRate;
        _attackRange = range;
        _elementDamageModifier = elementDamageModifier;

        switch (_weaponType)
        {
            case WeaponType.Melee:

                _adjustedToWeaponDamage = _baseDamage * 2;

                break;

            case WeaponType.Ranged:

                _adjustedToWeaponDamage = Mathf.RoundToInt(_baseDamage /2);

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

    public void DoAttack(RaycastHit monsterHitInfo)
    {
        switch (_weaponType)
        {
            case WeaponType.Melee:

                //    Not timed with animation rn
                //   Then enable/disable collider or something with the animation

                MonsterController monster = monsterHitInfo.collider.gameObject.GetComponent<MonsterController>();

                monster.ReceiveDamage(CalculateDamage(_adjustedToWeaponDamage, _elementDamageModifier, _weaponElement, monster.monsterData.elementAttribute));

                break;

            case WeaponType.Ranged:

                //Not timed with animation rn,
                //Need an animation value to trigger the attack or something
                MagicProjectile projectile = Instantiate(projectilePrefab, elementTypeAttachment.transform.position, projectilePrefab.transform.rotation);
                projectile.InitilizeProjectile(_adjustedToWeaponDamage, _elementDamageModifier, _weaponElement);

                break;

            default:
                break;
        }
    }

    public void IdleWeaponAnimation()
    {
        GetComponent<Animator>().SetTrigger("Idle");
    }
}