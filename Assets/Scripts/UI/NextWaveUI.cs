using System.Collections;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Serialization;
using GameData;

public class NextWaveUI : MonoBehaviour
{
    [SerializeField] private GameObject _nextMonsterUICellPrefab;

    [SerializeField] private Sprite fireElementSprite;
    [SerializeField] private Sprite earthElementSprite;
    [SerializeField] private Sprite lightningElementSprite;
    [SerializeField] private Sprite iceElementSprite;
    [SerializeField] private Sprite voidElementSprite;
    [SerializeField] public List<MonsterFilterData> filteredMonsters = new List<MonsterFilterData>();

    public struct MonsterFilterData
    {
        public MonsterFilterData(MonsterType monsterType, ElementAttribute elementAttribute, bool isABossMonster)
        {
            this.monsterType = monsterType;
            this.elementAttribute = elementAttribute;
            this.isABossMonster = isABossMonster;
        }

        public MonsterType monsterType;
        public ElementAttribute elementAttribute;
        public bool isABossMonster;
    }

    void Start()
    {
        Stage currentGameStage = GameStateManager.Instance.GameStages[GameStateManager.Instance.CurrentStageIndex];

        foreach (MobWave wave in currentGameStage.Waves)
        {
            foreach (MobWave.Mob mob in wave.mobsInTheWave)
            {

                bool isUniqueMob = false;

                //add and skip the fist mob as it's always unique
                if (filteredMonsters.Count == 0)
                {
                    AddMobToFilteredList(mob);
                    continue;
                }

                foreach (MonsterFilterData filterData in filteredMonsters)
                {
                    if (CompareMobs(filterData, mob)) goto monsterAlreadyStored;
                    else isUniqueMob = true;

                }
                if (isUniqueMob) AddMobToFilteredList(mob);

                monsterAlreadyStored:
                break;
            }
        }
    }

    private void AddMobToFilteredList(MobWave.Mob mob)
    {
        filteredMonsters.Add(
            new MonsterFilterData(
                mob.monsterData.monsterType,
                mob.monsterData.elementAttribute,
                mob.monsterData.isABossMonster)
        );
    }

    private bool CompareMobs(MonsterFilterData filterData, MobWave.Mob mob)
    {
        return (filterData.monsterType == mob.monsterData.monsterType
        && filterData.elementAttribute == mob.monsterData.elementAttribute
        && filterData.isABossMonster == mob.monsterData.isABossMonster)
         ? true : false;
    }
}
