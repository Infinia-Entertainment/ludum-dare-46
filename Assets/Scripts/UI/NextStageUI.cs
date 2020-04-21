using GameData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NextStageUI : MonoBehaviour
{
    public Stage nextStage;

    //private Vector3 pos;
    List<ElementAttribute> types = new List<ElementAttribute>();

    [SerializeField] private GameObject parentCanvas;
    [SerializeField] private List<GameObject> waveIcons = new List<GameObject>();

    [SerializeField] private Dictionary<Sprite, MonsterIconData> monsterWaveData = new Dictionary<Sprite, MonsterIconData>();

    private void Awake()
    {
        nextStage = GameStateManager.Instance.GameStages[GameStateManager.Instance.CurrentStageIndex];

        parentCanvas = GetComponent<Canvas>().gameObject;
        waveIcons =  GetComponentsInChildren<GameObject>().ToList();
    }

    public struct MonsterIconData
    {
        public List<ElementAttribute> allPresentMonsterElements;
    }

    private void Start()
    {

        //var uniqueMonsters = nextStage.MobsInTheWave.Where((x, i) => !x.Equals(nextStage.Waves[i - 1]));


        //foreach (MobWave wave in nextStage.Waves)
        //{
        //    foreach (MobWave.Mob mob in wave.MobsInTheWave)
        //    {
        //        if (!monsterWaveData.ContainsKey(mob.monsterData.monsterImage))
        //        {
        //            monsterWaveData.Add(mob.monsterData.monsterImage,mob.monsterData.elementAttribute);
        //        }
        //    }
        //    //Get all unique monsters


        //    //Assign elements of those monsters

            

        }
    }
}
