using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Start is called before the first frame update
    public bool hasObject;
    void Start()
    {
        hasObject = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AssociateHero(GameObject hero)
    {
        if (!hasObject)
        {        
            hero.GetComponent<DragandDrop>().associatedPlatform = this;
            hasObject = true;
        }
    }
}
