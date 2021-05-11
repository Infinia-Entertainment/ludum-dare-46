using GameData;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class AliveUnit : MonoBehaviour
{
    public float defence = 1;
    public int health;
    public int maxHealth;

    ElementAttribute UnitElementType;

    protected virtual void CheckForHealth()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            return;
        }
    }

    public virtual void ReceiveDamage(int damage)
    {
        //defence and damage
        //i want defence to **reduce** the damage not substract it

        //20 damage /40 defence = 0.5 * 20 = 10 damage
        //20 damage /50 defence = 0.4 * 20 = 8 damage
        //30 damage /40 defence = 0.75 * 30 = round(22.5) --> 22
        //30 damage /50 defence = 0.6 * 30 = 18 
        
        health -= Mathf.FloorToInt((damage/defence) * damage);
        CheckForHealth();
        Debug.Log(gameObject.name + " got damaged for " + damage + " damage");
    }

}
