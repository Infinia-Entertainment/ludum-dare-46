using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static WeaponData;

public class WeaponCreationSystem : MonoBehaviour
{

    /* We're creating a system for staff creation (the weapon in this case)
     *
     * It will have different components
     * 
     * first either it's ranged or melee
     * 
     * then you add an element to that
     * 
     * Fire
     * Wind
     * Earth
     * Ice
     * Electricity
     * 
     * + other attachements
     * 
     *  attachements that add more buffs in some way (defence, attackRate etc)
     */


    /*
     * How do i do visual combination?
     * I have the enums for the type so now what?
     * 
     */

    GameObject weaponStructurePrefab;

    public GameObject CreateWeaponPrefab()
    {
        //for now just spawns here, later at a specific place
        GameObject weaponPrefab = Instantiate(weaponStructurePrefab,transform.position,Quaternion.identity);
        return weaponPrefab;
    }

    public StaffWeapon CreateWeapon(WeaponType weaponType, WeaponElement weaponElement, params WeaponBuffAttachement[] weaponBuffAttachement)
    {
        StaffWeapon weapon = new StaffWeapon(weaponType, weaponElement, weaponBuffAttachement);

        return weapon;

    }

    


}
