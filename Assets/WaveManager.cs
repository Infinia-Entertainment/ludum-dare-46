using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int numSpawnPoints;
    public float respawnInterval;
    public int monsterNum;
    public GameObject monster;
    public Transform spawn1;
    public Transform spawn2;
    public Transform spawn3;
    public Transform spawn4;
    public Transform spawn5;
    public List<Transform> spawnPoints;
    public List<Transform> spawnPointsToUse;
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

        InvokeRepeating("spawn", 0, respawnInterval);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void spawn()
    {
        if(monsterNum > 0)
        {
            int x = Random.Range(0, numSpawnPoints);
            Instantiate(monster, spawnPointsToUse[x].position, Quaternion.identity);
            monsterNum -= 1;
        }
        
    }
}
