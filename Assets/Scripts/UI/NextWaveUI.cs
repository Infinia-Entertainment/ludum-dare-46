using System.Collections;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Serialization;
using GameData;

public class NextWaveUI : MonoBehaviour
{
    [SerializeField] private GameObject _nextMonsterUICellPrefab;
    [SerializeField] private GameObject _contentGameObject;
    [SerializeField] private Sprite fireElementSprite;
    [SerializeField] private Sprite earthElementSprite;
    [SerializeField] private Sprite lightningElementSprite;
    [SerializeField] private Sprite iceElementSprite;
    [SerializeField] private Sprite voidElementSprite;
    [SerializeField] public List<MonsterFilterData> filteredMonsters = new List<MonsterFilterData>();

    public struct MonsterFilterData
    {
        public MonsterFilterData(MonsterType monsterType, ElementAttribute elementAttribute, bool isABossMonster, Sprite monsterImage)
        {
            this.monsterType = monsterType;
            this.elementAttribute = elementAttribute;
            this.isABossMonster = isABossMonster;
            this.monsterImage = monsterImage;
        }

        public MonsterType monsterType;
        public ElementAttribute elementAttribute;
        public bool isABossMonster;
        public Sprite monsterImage;
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

        foreach (MonsterFilterData mobData in filteredMonsters)
        {
            Sprite spriteToAssign;

            switch (mobData.elementAttribute)
            {
                case ElementAttribute.Void:
                    spriteToAssign = voidElementSprite;
                    break;
                case ElementAttribute.Earth:
                    spriteToAssign = earthElementSprite;
                    break;
                case ElementAttribute.Ice:
                    spriteToAssign = iceElementSprite;
                    break;
                case ElementAttribute.Fire:
                    spriteToAssign = fireElementSprite;
                    break;
                case ElementAttribute.Lightning:
                    spriteToAssign = lightningElementSprite;
                    break;
                default:
                    spriteToAssign = fireElementSprite;
                    break;

            }

            GameObject cellUI = Instantiate(_nextMonsterUICellPrefab, _contentGameObject.transform);
            cellUI.GetComponent<NextWaveMonsterUICell>().InitializeCell(mobData.monsterImage, spriteToAssign);
        }
    }

    private void AddMobToFilteredList(MobWave.Mob mob)
    {
        filteredMonsters.Add(
            new MonsterFilterData(
                mob.monsterData.monsterType,
                mob.monsterData.elementAttribute,
                mob.monsterData.isABossMonster,
                mob.monsterData.monsterImage)
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
