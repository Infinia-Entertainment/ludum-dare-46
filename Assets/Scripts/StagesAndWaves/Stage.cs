using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New stage",menuName = "Stage")]
public class Stage : SerializedScriptableObject
{
    [SerializeField] public List<MobWave> Waves = new List<MobWave>();

    public int heroReward; // how many heroes do you get from winning
}
