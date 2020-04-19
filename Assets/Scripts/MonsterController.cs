using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Rigidbody rb;

    public bool attack;

    public GameObject hero;

    public float health;

    public float armor;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        attack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject, 1f);
        }
        if (hero == null)
        {
            attack = false;
        }
    }
    private void FixedUpdate()
    {
        rb = gameObject.GetComponent<Rigidbody>();
       
        rb.velocity = new Vector3(-speed, 0,0);

        if (attack)
        {
            rb.velocity = new Vector3(0, 0,0);
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hero")
        {
            hero = other.gameObject;
            attack = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Hero")
        {
            attack = false;
        }
    }
}
