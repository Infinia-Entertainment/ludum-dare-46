using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
    public enum WeaponType
    {
        Melee,
        Ranged
    }

    //I'll just reuse this for enemy as well
    public enum ElementAttribute
    {
        Void,
        Fire,
        Earth,
        Ice,
        Lightning
    }

    //will get added later just for examples
    public enum WeaponBuffs
    {
        Damage,
        AttackRate,
        ElementModifier
    }
}
