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

        StartCoroutine(spawn());

    }

    // Update is called once per frame
    void Update()
    {   
        Debug.Log(currentStage.Waves.IndexOf(_currentWave) + 1);
        Debug.Log(currentStage.Waves.Count);
        Debug.Log(lastMonsterObject);

        if (currentStage.Waves.IndexOf(_currentWave) + 1 == currentStage.Waves.Count && lastMonsterObject == null && !hasWon)
        {
            GameStateManager.Instance.WinStage(currentStage);
            hasWon = true;
        }
    }
    public IEnumerator spawn()
    {
        foreach (MobWave currentWave in currentStage.Waves)
        {
            _currentWave = currentWave;
            for (int i = 0; i < currentWave.Count; i++)
            {
                int x = Random.Range(0, numSpawnPoints);
                lastMonsterObject = Instantiate(currentWave.Prefab, spawnPointsToUse[x].position, Quaternion.LookRotation(Vector3.right));
                yield return new WaitForSeconds(1 + currentWave.DelayInBetween);
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
