using GameData;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HeroController : AliveUnit
{
    public bool isSelected;
    public bool isInitialized;
    public GameObject weaponObject;
    public StaffWeapon Weapon { get => _weapon; }

    [SerializeField] private GameObject _handHoldingWeapon;
    [SerializeField] private AudioClip _heroDamagedSound;

    private DragAndDrop _dragAndDrop;
    private Animator _animator;
    private AudioSource _audioSource;
    
    private float _lastAttack;
    private RaycastHit _attackHitInfo;
    private StaffWeapon _weapon;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _dragAndDrop = GetComponent<DragAndDrop>();

        _handHoldingWeapon = GetComponentInChildren<WeaponHolder>().gameObject;

        health = maxHealth;

        _lastAttack = Time.time;


        //test only
        //weaponPrefab = Instantiate(weaponPrefab,transform);
        //weapon.TestInitialize();
    }

    public void InitalizeWeaponData()
    {
        _weapon = weaponObject.GetComponent<StaffWeapon>();
        defence = _weapon.Basedefence;
    }

    public void InitializeWeaponPosition()
    {
        weaponObject.transform.parent = _handHoldingWeapon.transform;
        weaponObject.transform.localPosition = new Vector3(0.147f, 0.576f, 0.08f);
        weaponObject.transform.localEulerAngles = new Vector3(80f, -2.59f, 48f);
    }

    void Update()
    {
        CheckForHealth();
        CheckForAttackRange();

        //Debug.Log(weapon.WeaponType);
    }

    //!!!!!!!!!!!Do anims here!!!!!!!!!
    private void CheckForAttackRange()
    {
        if (!Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), transform.TransformDirection(Vector3.left), out _attackHitInfo, 10, 1 << 12)) // 1 << 12 Monster Layer
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 10, Color.red);
            return;
        }

        //Debug.Log(_attackHitInfo.collider.gameObject);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * _attackHitInfo.distance, Color.green);

        //Check for attack Rate
        if (Time.time - _lastAttack >= _weapon.AttackRate)
        {

            //Just checks if attack is within range
            if (_attackHitInfo.distance <= _weapon.AttackRange)
            {
                if (_weapon.WeaponType == WeaponType.Melee)
                {
                    DoMeleeAttackAnimation();
                }
                else if (_weapon.WeaponType == WeaponType.Ranged)
                {
                    DoRangedAttackAnimation();
                }
            }

            _lastAttack = Time.time;
        }
    }

    private void DoRangedAttackAnimation()
    {
        _animator.SetTrigger("Range attack");
    }

    public void CarryOutAttack()
    {
        if (_attackHitInfo.collider)
        {
            _weapon.DoAttack(_attackHitInfo);
        }
    }

    private void DoMeleeAttackAnimation()
    {
        _animator.SetTrigger("Melee attack");
    }

    public override void ReceiveDamage(int damage)
    {
        base.ReceiveDamage(damage);
        _audioSource.PlayOneShot(_heroDamagedSound);
        _animator.SetTrigger("IsHit");
    }

    protected override void CheckForHealth()
    {
        if (health <= 0)
        {
            _animator.SetTrigger("Death");
        }
    }

    public void FinishDeath()
    {
        Destroy(gameObject);
    }

    public void DisableDragger()
    {
        _dragAndDrop.enabled = false;
    }

    public void EnableDragger()
    {
        _dragAndDrop.enabled = true;
    }


}
