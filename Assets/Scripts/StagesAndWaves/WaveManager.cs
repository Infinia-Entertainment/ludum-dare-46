using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


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
    public List<Transform> spawnPointsToUse;
    // Start is called before the first frame update

    private GameObject lastMonsterObject;
    private bool hasWon = false;

    void Start()
    {

        spawnPoints = new List<Transform>();

        spawnPoints.Add(spawn1);
        spawnPoints.Add(spawn2);
        spawnPoints.Add(spawn3);
        spawnPoints.Add(spawn4);
        spawnPoints.Add(spawn5);

        for (int i = 0;i < numSpawnPoints;i++)
        {
            spawnPointsToUse.Add(spawnPoints[i]);
        }


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
            if (currentStage.Waves.IndexOf(_currentWave) + 1 == currentStage.Waves.Count && lastMonsterObject == null && !hasWon)
            {
                GameStateManager.Instance.WinStage(currentStage);
                hasWon = true;
            }
        }
        
    }
    public IEnumerator StartSpawning()
    {
        foreach (MobWave currentWave in currentStage.Waves)
        {
            _currentWave = currentWave;

            foreach (MobWave.Mob mob in currentWave.MobsInTheWave)
            {
                for (int i = 0; i < mob.count; i++)
                {
                    int spawnPointIndex = Random.Range(0, numSpawnPoints);
                    lastMonsterObject = Instantiate(mob.Prefab, spawnPointsToUse[spawnPointIndex].position, Quaternion.LookRotation(Vector3.right));
                    lastMonsterObject.GetComponent<MonsterController>().monsterData = mob.monsterData;
                    yield return new WaitForSeconds(1 + currentWave.DelayInBetween);
                }

            }
            yield return new WaitForSeconds(currentWave.DelayAfterWave);
        }


        /*if(monsterNum > 0)
        {
            int x = Random.Range(0, numSpawnPoints);
            Instantiate(monster, spawnPointsToUse[x].position, Quaternion.LookRotation(Vector3.right));
            monsterNum -= 1;
        }*/
        
    }
}
