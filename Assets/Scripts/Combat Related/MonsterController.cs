using System;
using UnityEngine;

public class MonsterController : AliveUnit
{
    private float _lastAttack;
    private float _raycastLength;
    private bool _isFrontOccupied;
    private bool _isMonsterDying;
    private bool _isLastDamageFromHero;
    private LayerMask _layerMask = 1 << 12 | 1 << 13; // Hero and Monster layer combined
    private Collider _monsterCollider;
    private Animator _animator;
    private RaycastHit _hitInfo;
    [SerializeField] private MonsterController _monsterInFront;

    [SerializeField] private MonsterElementMaterialData _monsterElementMaterialData;
    [SerializeField] private GameObject hero;

    public MonsterData monsterData;

    private bool _isMonsterStopped = false;
    private void Awake()
    {
        _isFrontOccupied = false;
        _animator = GetComponent<Animator>();
        _monsterCollider = GetComponent<Collider>();
        _raycastLength = _monsterCollider.bounds.size.x / 2;
    }

    public void InitalizeMonsterData()
    {
        maxHealth = monsterData.health;
        health = maxHealth;
        _lastAttack = Time.time;

        Debug.Log(health);
    }

    private void Start()
    {
        InitalizeMonsterData();
    }

    private void Update()
    {
        if (!_isMonsterDying)
        {
            Move();
            CheckForAttack();
            CheckForHealth();
        }
    }

    private void PickMonsterMaterial()
    {
        switch (monsterData.elementAttribute)
        {
            case GameData.ElementAttribute.None:
                break;
            case GameData.ElementAttribute.Void:
                GetComponent<MeshRenderer>().material = _monsterElementMaterialData.voidMonsterMaterial;
                break;
            case GameData.ElementAttribute.Fire:
                GetComponent<MeshRenderer>().material = _monsterElementMaterialData.fireMonsterMaterial;
                break;
            case GameData.ElementAttribute.Earth:
                GetComponent<MeshRenderer>().material = _monsterElementMaterialData.earthMonsterMaterial;
                break;
            case GameData.ElementAttribute.Ice:
                GetComponent<MeshRenderer>().material = _monsterElementMaterialData.iceMonsterMaterial;
                break;
            case GameData.ElementAttribute.Lightning:
                GetComponent<MeshRenderer>().material = _monsterElementMaterialData.lightningMonsterMaterial;
                break;
            default:
                break;
        }
    }


    private void CheckForAttack()
    {

        Debug.DrawRay(_monsterCollider.bounds.center, Vector3.left * _raycastLength, Color.magenta);

        if (!Physics.Raycast(_monsterCollider.bounds.center, Vector3.left, out _hitInfo, _raycastLength, _layerMask))
        {
            Debug.DrawRay(_monsterCollider.bounds.center, Vector3.left * _hitInfo.distance, Color.cyan);
            //Didn't collide with either hero or monster so you can move forward
            _isFrontOccupied = false;
            return;
        }

        //If did collide, check which one has is the first collision

        if (_hitInfo.collider.gameObject.CompareTag("Monster")) //It's a monster
        {
            _monsterInFront = _hitInfo.collider.gameObject.GetComponent<MonsterController>();
            if (!_monsterInFront._isMonsterDying) _isFrontOccupied = true;
        }
        else if (_hitInfo.collider.gameObject.CompareTag("Hero"))//It's a hero
        {
            hero = _hitInfo.collider.gameObject;
            _isFrontOccupied = true;
        }



        //Check for attack Rate
        if (Time.time - _lastAttack > monsterData.attackRate)
        {
            //Just checks if attack is within range
            if (_hitInfo.distance <= monsterData.attackDistance && _isFrontOccupied && hero != null)
            {
                //attack anims etc
                DoAttackAnimation();
            }
            _lastAttack = Time.time;
        }
    }

    private void Move()
    {
        Vector3 translation;

        if (_isFrontOccupied || _isMonsterStopped)
        {
            translation = new Vector3(0, 0, 0);
            _animator.SetBool("isWalking", false);

        }
        else
        {
            translation = new Vector3(-monsterData.speed, 0, 0);
            _animator.SetBool("isWalking", true);
        }

        transform.Translate(translation * Time.deltaTime);
    }

    public override void ReceiveDamage(int damage)
    {
        _isLastDamageFromHero = false;
        base.ReceiveDamage(damage);
        _animator.SetTrigger("Hit");
    }
    public void ReceiveDamageFromHero(int damage)
    {
        _isLastDamageFromHero = true;
        GameStateManager.Instance.AddMonsterDamageDone(damage);
        base.ReceiveDamage(damage);
        _animator.SetTrigger("Hit");
    }

    protected override void CheckForHealth()
    {
        if (health <= 0)
        {
            Debug.Log($"dying   {health}");
            _animator.SetTrigger("Death");
            _isMonsterDying = true;
        }

    }

    public void FinishDeath()
    {
        WaveManager.Instance.RemoveMonsterFromList(gameObject, monsterData, _isLastDamageFromHero);

        Destroy(gameObject);
    }


    public void CarryOutAttack()
    {
        if (_hitInfo.collider)
        {
            _hitInfo.collider.gameObject.GetComponent<AliveUnit>().ReceiveDamage(monsterData.damage);
        }
    }

    private void DoAttackAnimation()
    {
        _animator.SetTrigger("Attack");
    }

    [ContextMenu("Stop Monster")]
    private void StopMonster()
    {
        _isMonsterStopped = true;
    }
}


