using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "new stage",menuName = "Stage")]
public class Stage : SerializedScriptableObject
{
    public List<MobWave> Waves = new List<MobWave>();

}
