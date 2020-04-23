using GameData;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HeroController : AliveUnit
{
    [SerializeField] public GameObject weaponObject;
    [SerializeField] private GameObject handHoldingWeapon;

    private StaffWeapon _weapon;

    float lastAttack;
    RaycastHit attackHitInfo;

    Animator animator;

    public bool isSelected;
    public bool isInitialized;

    public StaffWeapon Weapon { get => _weapon; }

    private void Awake()
    {
        animator = GetComponent<Animator>();

        handHoldingWeapon = GetComponentInChildren<WeaponHolder>().gameObject;

        health = maxHealth;

        lastAttack = Time.time;


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
        weaponObject.transform.parent = handHoldingWeapon.transform;
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


        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out attackHitInfo, 10, 1 << 12)) // 1 << 12 Monster Layer
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 10, Color.red);
            return;
        }

        //Debug.Log(attackHitInfo.collider.gameObject);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * attackHitInfo.distance, Color.green);

        //Check for attack Rate
        if (Time.time - lastAttack >= _weapon.AttackRate)
        {

            //Just checks if attack is within range
            if (attackHitInfo.distance <= _weapon.AttackRange)
            {
                DoMeleeAttackAnimation();
            }
            
            lastAttack = Time.time;
        }
    }

    private void DoRangedAttackAnimation( )
    {
        Debug.Log("ranged animation");
        animator.SetTrigger("Range attack");

    }

    public void CarryOutAttack()
    {
        _weapon.DoAttack(attackHitInfo);
    }

    private void DoMeleeAttackAnimation( )
    {
        animator.SetTrigger("Melee attack");
    }

    public override void ReceiveDamage(int damage)
    {
        base.ReceiveDamage(damage);
        animator.SetTrigger("IsHit");

    }

    protected override void CheckForHealth()
    {
        if (health <= 0)
        {
            animator.SetTrigger("Death");
        }

    }

    public void FinishDeath()
    {
        Destroy(gameObject);
    }

    private void OnMouseOver()
    {
        Debug.Log(gameObject.name + " has been selected");
    }
}
