using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int gridHeight = 7;
    public int gridWidth = 5;
    public Vector3 platformOffset;
    public GameObject platformPrefab;
    int color = 0;

    public Material[] gridMats;
    Inventory inventory;
    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int i = 0; i < gridHeight; i++)
        {
            for (int j = 0; j < gridWidth; j++)
            {
                GameObject platform = Instantiate(
                    platformPrefab,
                    new Vector3((float)j - gridHeight / 2, 0, (float)i - gridWidth / 2),
                    Quaternion.identity,
                    transform);
                platform.GetComponent<MeshRenderer>().material = color % 2 == 0 ? gridMats[0] : gridMats[1];
                color++;
            }
        }
        transform.position = platformOffset;
    }

   
}
