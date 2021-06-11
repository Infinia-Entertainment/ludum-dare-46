using GameData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.VFX;
using static GameData.GameFunctions;

public class MagicProjectile : MonoBehaviour
{
    private int _baseDamage;
    private float _elementDamageModifier;
    [SerializeField] private VisualEffect _projectileVFX;

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
        _projectileVFX.SetGradient("Particle Gradient", GameStateManager.Instance.GetGradientFromElement(_projectileElement));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            MonsterController monster = other.gameObject.GetComponent<MonsterController>();

            monster.ReceiveDamageFromHero(CalculateDamage(_baseDamage, _elementDamageModifier, _projectileElement, monster.monsterData.elementAttribute));

            //other.gameObject.GetComponent<AliveUnit>().ReceiveDamage(damage);

            Destroy(gameObject);
        }
    }
}
