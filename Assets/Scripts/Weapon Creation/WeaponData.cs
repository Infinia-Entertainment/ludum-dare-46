using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WeaponData
{
    public enum WeaponType
    {
        Melee,
        Ranged
    }

    //I'll just reuse this for enemy as well
    public enum WeaponElement
    {
        None,
        Fire,
        Wind,
        Eearth,
        Ice,
        Electricity
    }

    //will get added later just for examples
    public enum WeaponBuffs
    {
        Damage,
        AttackRate,
        ElementModifier
    }
}
