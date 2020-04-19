using GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : AliveUnit
{
    GameObject weaponPrefab;
    StaffWeapon weapon;

    private void Initialize()
    {
        weapon = weaponPrefab.GetComponent<StaffWeapon>();
    }

    private void Attack()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckForHealth();
    }
}
