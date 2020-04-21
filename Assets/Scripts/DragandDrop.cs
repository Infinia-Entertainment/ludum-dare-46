using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragandDrop : MonoBehaviour
{
    float followSpeed = 17f;
    // Start is called before the first frame update
    Rigidbody rb;
    Vector3 target;
    Vector3 initialPos;
    public Platform associatedPlatform;

    public static bool isRepositioning = false;
    bool isSelected = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Ray ray;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (isRepositioning == false)
            {
                initialPos = transform.position;
                if (Physics.Raycast(ray, out hit) &&  transform.gameObject == hit.transform.gameObject)
                {
                    isSelected = true;
                    isRepositioning = true;
                }
            }

            if (isSelected)
            {

                Ray ray1;
                ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit1;

                if (Physics.Raycast(ray1, out hit1, 50, 1 << 8, QueryTriggerInteraction.Ignore) && hit1.transform.CompareTag("Platform"))
                {
                    target = hit1.point;
                }
                transform.position = Vector3.Lerp(transform.position, target, followSpeed * Time.deltaTime);
            }        
        }

        if (Input.GetMouseButtonUp(1) && isRepositioning == true && isSelected)
        {
           
            isRepositioning = false;
            int layerMask = 1 << 8;
            Ray ray;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 50, layerMask, QueryTriggerInteraction.Ignore) && hit.transform.CompareTag("Platform") )
            {
                Platform platform = hit.transform.gameObject.GetComponent<Platform>();
                if (!platform.hasObject)
                {
                    transform.position = hit.transform.position;
                    platform.hasObject = true;
                    if (associatedPlatform != null)
                        associatedPlatform.hasObject = false;

                    associatedPlatform = platform;
                }
                else
                    transform.position = initialPos;
            }
            else
                transform.position = initialPos;
            isSelected = false;
        }
    }
}
