using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Next_Stage_Info : MonoBehaviour
{
    public Stage nextStage;

    public int x;
    public int y;

    private Vector3 pos;
    List<MobWave.WaveType> types = new List<MobWave.WaveType>();

    [SerializeField] private GameObject parentCanvas;
    [SerializeField] private GameObject prefab;

    private void Awake()
    {
        parentCanvas = GetComponent<Canvas>().gameObject;
    }

    private void Start()
    {
        foreach (MobWave wave in nextStage.Waves)
        {
            if (!types.Contains(wave.Type))
            {
                types.Add(wave.Type);
                GameObject icon = Instantiate(prefab,parentCanvas.transform);
                Image monsterImage = icon.GetComponent<Image>(); //Add the Image Component script
                monsterImage.sprite = wave.Artwork; //Set the Sprite of the Image Component on the new GameObject
            }
        }
    }
}
