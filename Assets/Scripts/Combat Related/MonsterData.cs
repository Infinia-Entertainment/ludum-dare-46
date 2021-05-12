using GameData;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster Data", menuName = "New Monster Data")]
public class MonsterData : SerializedScriptableObject
{

    public MonsterData(MonsterData data)
    {
        monsterImage = data.monsterImage;
        elementAttribute = data.elementAttribute;
        goldReward = data.goldReward;
        health = data.health;
        damage = data.damage;
        playerDamage = data.playerDamage;
        speed = data.speed;
        defence = data.defence;
        attackRate = data.attackRate;
        attackDistance = data.attackDistance;
    }

    public Sprite monsterImage;
    public ElementAttribute elementAttribute;
    public int goldReward; //health of the monster
    public int health; //health of the monster
    public int damage; // damage to the heros
    public int playerDamage; //damage dealt if they pass through
    public float speed; //speed
    public float defence;
    public float attackRate;
    public float attackDistance;
}
