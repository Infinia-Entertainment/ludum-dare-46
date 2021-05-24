using GameData;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class AliveUnit : MonoBehaviour
{
    public float defence = 0;
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
        health -= Mathf.FloorToInt((damage * (100 / (100 + defence)))); //pretty basic, but works 
        CheckForHealth();
    }

}
