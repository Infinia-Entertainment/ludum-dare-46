using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Sirenix.Utilities;

public class WaveManager : MonoBehaviour
{


    public Stage currentStage;
    public MobWave _currentWave;
    public int numSpawnPoints;
    public Transform spawn1;
    public Transform spawn2;
    public Transform spawn3;
    public Transform spawn4;
    public Transform spawn5;
    public List<Transform> spawnPoints;

    private List<GameObject> spawnedMonsters = new List<GameObject>();
    private bool hasWon = false;
    private bool lastMonsterSpawned = false;

    void Start()
    {
        //lastMonsterSpawned

        spawnPoints = new List<Transform>();

        spawnPoints.Add(spawn1);
        spawnPoints.Add(spawn2);
        spawnPoints.Add(spawn3);
        spawnPoints.Add(spawn4);
        spawnPoints.Add(spawn5);
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(currentStage);
        //Debug.Log(currentStage.Waves);
        //Debug.Log(currentStage.Waves.IndexOf(_currentWave));


        //Add check for if the monster is 
        if (currentStage)
        {
            if (lastMonsterSpawned && currentStage.Waves.IndexOf(_currentWave) == currentStage.Waves.Count - 1 && spawnedMonsters.IsNullOrEmpty() && !hasWon)
            {
                hasWon = true;
                GameStateManager.Instance.WinStage(currentStage);
            }
        }

    }
    public IEnumerator StartSpawning()
    {
        foreach (MobWave currentWave in currentStage.Waves)
        {
            _currentWave = currentWave;
            foreach (MobWave.Mob mob in currentWave.mobsInTheWave)
            {
                int spawnPointIndex = mob.spawnPointIndex;
                GameObject spawnedMonster = Instantiate(mob.monsterPrefab, spawnPoints[spawnPointIndex].position, Quaternion.identity);
                spawnedMonster.GetComponent<MonsterController>().monsterData = mob.monsterData;
                spawnedMonsters.Add(spawnedMonster);
                yield return new WaitForSeconds(mob.delayAfterSpawn);

            }
            lastMonsterSpawned = true;
            yield return new WaitForSeconds(currentWave.delayAfterWave);
        }


        /*if(monsterNum > 0)
        {
            int x = Random.Range(0, numSpawnPoints);
            Instantiate(monster, spawnPoints[x].position, Quaternion.LookRotation(Vector3.right));
            monsterNum -= 1;
        }*/

    }
}
