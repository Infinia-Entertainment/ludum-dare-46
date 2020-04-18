using UnityEngine;
using System;

[Serializable]
public struct MobWave
{
    public enum WaveType
    {
        Spider,
        Boss
    }

    public WaveType Type;
    public GameObject Prefab;
    public int Count;
    public float Delay;
}
