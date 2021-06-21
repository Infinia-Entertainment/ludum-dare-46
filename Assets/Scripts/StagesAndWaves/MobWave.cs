using UnityEngine;
using System;
using Sirenix.Serialization;
using GameData;
using System.Collections.Generic;

[Serializable]
public struct MobWave
{

    [Serializable]
    public struct Mob
    {
        [OdinSerialize] public MonsterData monsterData;
        [OdinSerialize] public GameObject monsterPrefab;
        [OdinSerialize] [Range(0, 4)] public int spawnPointIndex;
        [OdinSerialize] public float delayAfterSpawn;
    }

    [OdinSerialize] public List<Mob> mobsInTheWave;
    [OdinSerialize] public float delayAfterWave;
}
