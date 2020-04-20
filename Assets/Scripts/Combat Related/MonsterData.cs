﻿using GameData;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster Data", menuName = "New Monster Data")]
public class MonsterData : SerializedScriptableObject
{
    public ElementAttribute elementAttribute;
    public int health; //health of the monster
    public int damage; // damage to the heros
    public int playerDamage; //damage dealt if they pass through
    public float speed; //speed
    public float defence; 
    public float attackRate;
    public float attackDistance;
}