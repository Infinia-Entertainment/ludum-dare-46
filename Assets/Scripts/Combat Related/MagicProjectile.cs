using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    public int damage;

    Vector3 translation = new Vector3(0, 2.5f, 0);

    private void Update()
    {
        transform.Translate(translation * Time.deltaTime);
    }
        
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            other.gameObject.GetComponent<AliveUnit>().ReceiveDamage(damage);

            Destroy(gameObject);
        }
    }
}
