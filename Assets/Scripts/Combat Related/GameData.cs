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
        None,
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

    public static class GameFunctions
    {
        public static int CalculateDamage(int basedamage, float elementDamageModifier, ElementAttribute damagingElement, ElementAttribute monsterElementType)
        {
            //Compare weapon type and monster type
            float adjustedDamage = basedamage;

            if (damagingElement == ElementAttribute.Void) { }// nothing since it's neutral
            else if (damagingElement == ElementAttribute.Fire)
            {
                //Strong against Earth (or plants)
                if (monsterElementType == ElementAttribute.Earth)
                {
                    adjustedDamage *= (1 + elementDamageModifier);
                }
            }
            else if (damagingElement == ElementAttribute.Earth)
            {
                if (monsterElementType == ElementAttribute.Lightning)
                {
                    adjustedDamage *= (1 + elementDamageModifier);
                }
            }
            else if (damagingElement == ElementAttribute.Ice)
            {
                if (monsterElementType == ElementAttribute.Fire)
                {
                    adjustedDamage *= (1 + elementDamageModifier);
                }
            }
            else if (damagingElement == ElementAttribute.Lightning)
            {
                if (monsterElementType == ElementAttribute.Ice)
                {
                    adjustedDamage *= (1 + elementDamageModifier);
                }
            }
            else if (damagingElement == ElementAttribute.Fire)
            {
                if (monsterElementType == ElementAttribute.Earth)
                {
                    adjustedDamage *= (1 + elementDamageModifier);
                }
            }

            return Mathf.RoundToInt(adjustedDamage);
        }
    }
}