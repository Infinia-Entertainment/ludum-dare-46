using GameData;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster Data", menuName = "New Monster Data")]
public class MonsterData : SerializedScriptableObject
{

    public Sprite monsterImage;
    public ElementAttribute elementAttribute;
    public MonsterType monsterType;
    public bool isABossMonster; //This is used by some UI and may later affect monster behaviour or functions
    public int goldReward; //health of the monster
    public int health; //health of the monster
    public int damage; // damage to the heros
    public int playerDamage; //damage dealt if they pass through
    public float speed; //speed
    public float defence;
    public float attackRate;
    public float attackDistance;

}
