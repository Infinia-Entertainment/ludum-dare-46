using GameData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NextStageUI : MonoBehaviour
{
    public Stage nextStage;

    //private Vector3 pos;
    List<ElementAttribute> types = new List<ElementAttribute>();

    [SerializeField] private GameObject parentCanvas;
    [SerializeField] private List<GameObject> waveIcons = new List<GameObject>();

    [SerializeField] private Dictionary<Sprite, List<ElementAttribute>> monsterWaveData = new Dictionary<Sprite, List<ElementAttribute>>();

    private void Awake()
    {
        nextStage = GameStateManager.Instance.GameStages[GameStateManager.Instance.CurrentStageIndex];

        parentCanvas = GetComponent<Canvas>().gameObject;
        waveIcons = GetComponentsInChildren<GameObject>().ToList();
    }

    private void Start()
    {
        // Very Unpotmized Please for the love of god use bitmask later in the element types

        //get unique monsters

        List<Sprite> uniqueMonsters = new List<Sprite>();

        foreach (MobWave wave in nextStage.Waves)
        {
            foreach (MobWave.Mob mob in wave.mobsInTheWave)
            {
                if (uniqueMonsters.Contains(mob.monsterData.monsterImage))
                {
                    uniqueMonsters.Add(mob.monsterData.monsterImage);
                }
            }
        }


        //for each type check that type in the foreach and 
        foreach (Sprite monsterSprite in uniqueMonsters)
        {
            List<ElementAttribute> monsterElements = new List<ElementAttribute>();

            foreach (MobWave wave in nextStage.Waves)
            {
                foreach (MobWave.Mob mob in wave.mobsInTheWave)
                {
                    if (mob.monsterData.monsterImage = monsterSprite)
                    {
                        monsterElements.Add(mob.monsterData.elementAttribute);
                    }
                }
            }
            monsterWaveData.Add(monsterSprite, monsterElements);
        }
    }
}
