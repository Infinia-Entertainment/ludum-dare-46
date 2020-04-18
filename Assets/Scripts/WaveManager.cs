using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveManager : MonoBehaviour
{
    public int numSpawnPoints;
    public Transform spawn1;
    public Transform spawn2;
    public Transform spawn3;
    public Transform spawn4;
    public Transform spawn5;
    public List<Transform> spawnPoints;
    public List<Transform> spawnPointsToUse;
    public List<MobWave> Waves = new List<MobWave>();
    // Start is called before the first frame update
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
        
    }
    public IEnumerator spawn()
    {
        foreach (MobWave wave in Waves)
        {
            for (int i = 0; i < wave.Count; i++)
            {
                int x = Random.Range(0, numSpawnPoints);
                Instantiate(wave.Prefab, spawnPointsToUse[x].position, Quaternion.LookRotation(Vector3.right));
                yield return new WaitForSeconds(wave.DelayInBetween);
            }
            yield return new WaitForSeconds(wave.DelayAfterWave);
            
        }
        /*if(monsterNum > 0)
        {
            int x = Random.Range(0, numSpawnPoints);
            Instantiate(monster, spawnPointsToUse[x].position, Quaternion.LookRotation(Vector3.right));
            monsterNum -= 1;
        }*/
        
    }
}
