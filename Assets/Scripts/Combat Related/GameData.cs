using UnityEngine;

namespace GameData
{
    public enum WeaponType
    {
        Melee,
        Ranged
    }

    public enum MonsterType
    {
        Imp,
        //Spider,//might be added later xd
    }
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

            if (damagingElement == ElementAttribute.Void)
            {
                //Affects all types but less
                adjustedDamage *= (1 + elementDamageModifier);
            }
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
                //Strong against Lightning
                if (monsterElementType == ElementAttribute.Lightning)
                {
                    adjustedDamage *= (1 + elementDamageModifier);
                }
            }
            else if (damagingElement == ElementAttribute.Ice)
            {
                //Strong against Fire
                if (monsterElementType == ElementAttribute.Fire)
                {
                    adjustedDamage *= (1 + elementDamageModifier);
                }
            }
            else if (damagingElement == ElementAttribute.Lightning)
            {
                //Strong against Ice
                if (monsterElementType == ElementAttribute.Ice)
                {
                    adjustedDamage *= (1 + elementDamageModifier);
                }
            }
           

            return Mathf.RoundToInt(adjustedDamage);
        }


    }
}