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

    private StaffWeapon weapon;

    float lastAttack;
    RaycastHit attackHitInfo;

    Animator animator;

    public bool isSelected;
    public bool isInitialized;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        handHoldingWeapon = GetComponentInChildren<WeaponHolder>().gameObject;

        health = 20;

        weapon = weaponObject.GetComponent<StaffWeapon>();

        //InitializeWeaponPosition();

        lastAttack = Time.time;


        //test only
        //weaponPrefab = Instantiate(weaponPrefab,transform);
        //weapon.TestInitialize();
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

        Debug.DrawRay(transform.position, Vector3.right * attackHitInfo.distance,Color.red);

        if (!Physics.Raycast(transform.position, Vector3.left, out RaycastHit hitInfo, 10, 1 << 12)) // 1 << 12 Monster Layer
        {
            return;
        }
        Debug.Log("before before ranged animation");

        //Check for attack Rate
        if (Time.time - lastAttack >= weapon.AttackRate)
        {

            //Just checks if attack is within range
            if (weapon.WeaponType == WeaponType.Melee && hitInfo.distance <= 2)
            {
                Debug.Log("before ranged animation");
                    
                DoMeleeAttackAnimation();
            }
            if (weapon.WeaponType == WeaponType.Ranged && hitInfo.distance <= 8)
            {
                Debug.Log("before ranged animation");

                DoRangedAttackAnimation();

            }
            lastAttack = Time.time;
        }
    }

    private void DoRangedAttackAnimation( )
    {
        Debug.Log("ranged animation");
        animator.SetBool("Ranged attack", true);
        animator.SetBool("Ranged attack", false);

    }

    public void CarryOutAttack()
    {
        weapon.DoAttack(attackHitInfo);
    }

    private void DoMeleeAttackAnimation( )
    {
        animator.SetBool("Melee attack",true);
        animator.SetBool("Melee attack",false);
    }

    public override void ReceiveDamage(int damage)
    {
        base.ReceiveDamage(damage);
        animator.SetBool("IsHit", true);
        animator.SetBool("IsHit", false);

    }

    protected override void CheckForHealth()
    {
        base.CheckForHealth();
        animator.SetBool("Death", true);
        animator.SetBool("Death", false);

    }

    private void OnMouseOver()
    {
        Debug.Log(gameObject.name + " has been selected");
    }
}
