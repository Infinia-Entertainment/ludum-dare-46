using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster Data", menuName = "New Monster Data")]
public class MonsterData : SerializedScriptableObject
{
    public int damage;
    public float speed;
    public float defence;
    public float attackRate;
    public float attackDistance;
}
