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

    public GameObject ParentCanvas;

    
    private void Awake()
    {
        pos.x = x;
        pos.y = y;
        pos.z = 0;
        foreach (MobWave wave in nextStage.Waves)
        {
            if (!types.Contains(wave.Type))
            {
                types.Add(wave.Type);
                GameObject NewObj = new GameObject(); //Create the GameObject
                Image NewImage = NewObj.AddComponent<Image>(); //Add the Image Component script
                NewImage.sprite = wave.Artwork; //Set the Sprite of the Image Component on the new GameObject
                NewObj.GetComponent<RectTransform>().SetParent(ParentCanvas.transform); //Assign the newly created Image GameObject as a Child of the Parent Panel.
                NewObj.GetComponent<RectTransform>().SetPositionAndRotation(pos, Quaternion.identity);//Set position
                NewObj.SetActive(true); //Activate the GameObject
                pos.y -= 100;
            }
        }
    }
}
