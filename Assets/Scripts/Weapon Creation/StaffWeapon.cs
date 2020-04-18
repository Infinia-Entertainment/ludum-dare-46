using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static WeaponData;

public class StaffWeapon : MonoBehaviour
{
    [SerializeField] public GameObject Rod, AttackTypeAttachment, ElementTypeAttachment;

    private int baseDamage;

    public WeaponType weaponType;
    public WeaponElement weaponElement;

    private List<WeaponBuffAttachement> _weaponBuffAttachements = new List<WeaponBuffAttachement>(); //all other attachements

    public StaffWeapon(WeaponType weaponType, WeaponElement weaponElement, params WeaponBuffAttachement[] weaponBuffAttachements)
    {
        this.baseDamage = baseDamage;
        this.weaponType = weaponType;
        this.weaponElement = weaponElement;
        _weaponBuffAttachements = weaponBuffAttachements.ToList();
    }

    public int CalculateDamage()
    {

        return baseDamage;
    }
}
