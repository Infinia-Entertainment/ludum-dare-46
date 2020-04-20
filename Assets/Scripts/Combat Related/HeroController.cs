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

    public bool isSelected;
    public bool isInitialized;

    private void Awake()
    {
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
        RaycastHit hitInfo;

        if (!Physics.Raycast(transform.position, Vector3.right, out hitInfo, 10, 1 << 12)) // 1 << 12 Monster Layer
        {
            return;
        }

        //Check for attack Rate
        if (Time.time - lastAttack >= weapon.AttackRate)
        {

            //Just checks if attack is within range
            if (weapon.WeaponType == WeaponType.Melee && hitInfo.distance <= 2)
            {
                weapon.DoAttack();
            }
            if (weapon.WeaponType == WeaponType.Ranged && hitInfo.distance <= 8)
            {
                weapon.DoAttack();
            }
            lastAttack = Time.time;
        }
    }

    private void OnMouseOver()
    {
        Debug.Log(gameObject.name + " has been selected");
    }
}
