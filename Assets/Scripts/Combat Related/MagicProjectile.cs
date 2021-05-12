using GameData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static GameData.GameFunctions;

public class MagicProjectile : MonoBehaviour
{
    private int _baseDamage;
    private float _elementDamageModifier;
    ElementAttribute _projectileElement;

    Vector3 translation = new Vector3(0, 2.5f, 0);

    private void Update()
    {
        transform.Translate(translation * Time.deltaTime);
    }

    public void InitilizeProjectile(int damage, float elementDamageModifier, ElementAttribute elementAttribute)
    {
        _baseDamage = damage;
        _elementDamageModifier = elementDamageModifier;
        _projectileElement = elementAttribute;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided " + other.tag);

        if (other.CompareTag("Monster"))
        {
            MonsterController monster = other.gameObject.GetComponent<MonsterController>();

            monster.ReceiveDamage(CalculateDamage(_baseDamage, _elementDamageModifier, _projectileElement, monster.monsterData.elementAttribute));

            //other.gameObject.GetComponent<AliveUnit>().ReceiveDamage(damage);

            Destroy(gameObject);
        }
    }
}
