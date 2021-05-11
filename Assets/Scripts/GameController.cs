using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int gridHeight = 7;
    public int gridWidth = 5;
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
                    transform.position + new Vector3(j + 0.5f, 0, i + 0.5f),
                    Quaternion.identity,
                    transform);
                platform.GetComponent<MeshRenderer>().material = color % 2 == 0 ? gridMats[0] : gridMats[1];
                color++;
            }
        }
    }

   
}
