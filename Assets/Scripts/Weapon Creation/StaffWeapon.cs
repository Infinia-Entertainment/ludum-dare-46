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
    [SerializeField] private int _baseDamage; //for now
    [SerializeField] private int _baseDefence; //for now

    //[Range(0.0f, 2.0f)]
    [SerializeField] private float _elementDamageModifier = 0.5f;

    //Other weapon info
    [SerializeField] private float _attackRate; //in seconds
    [SerializeField] private float _attackRange;

    //[SerializeField] private float knockback; ??

    private WeaponType _weaponType;
    private ElementAttribute _weaponElement;

    public WeaponType WeaponType { get => _weaponType; }
    public ElementAttribute WeaponElement { get => _weaponElement; }
    public float AttackRate { get => _attackRate; }
    public float AttackRange { get => _attackRange; }
    public int Basedefence { get => _baseDefence; }

    //private WeaponBuffs[] _weaponBuffs = new WeaponBuffs[3];

    #region Test Code Only

    public void TestInitialize()
    {
        _weaponType = WeaponType.Melee;
        _weaponElement = ElementAttribute.Ice;
    }

    #endregion Test Code Only

    public void InitializeWeapon(WeaponType weaponType, ElementAttribute weaponElement, int damage, int weaponDefence, float fireRate, float range, float elementDamageModifier)
    {
        _weaponType = weaponType;
        _weaponElement = weaponElement;
        //_weaponBuffs = weaponBuffs;

        _baseDamage = damage;
        _baseDefence = weaponDefence;
        _attackRate = fireRate;
        _attackRange = range;
        _elementDamageModifier = elementDamageModifier;

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

                MonsterController monster = monsterHitInfo.collider.gameObject.GetComponent<MonsterController>();
                monster.ReceiveDamage(CalculateDamage(_baseDamage, _elementDamageModifier, _weaponElement, monster.monsterData.elementAttribute));

                break;

            case WeaponType.Ranged:

                MagicProjectile projectile = Instantiate(projectilePrefab, elementTypeAttachment.transform.position, projectilePrefab.transform.rotation);
                projectile.InitilizeProjectile(_baseDamage, _elementDamageModifier, _weaponElement);

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