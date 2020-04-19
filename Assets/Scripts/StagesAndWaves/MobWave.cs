using UnityEngine;
using System;
using Sirenix.Serialization;

public struct MobWave
{

    public enum WaveType
    {
        Monster,
        Boss
    }


    [OdinSerialize] public Sprite Artwork;
    [OdinSerialize] public WaveType Type;
    [OdinSerialize] public GameObject Prefab;
    [OdinSerialize] public int Count;
    [OdinSerialize] public float DelayInBetween;
    [OdinSerialize] public float DelayAfterWave;
}
