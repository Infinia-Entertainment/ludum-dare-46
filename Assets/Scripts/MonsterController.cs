using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Rigidbody2D rb;

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
    }
    private void FixedUpdate()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
       
        rb.velocity = new Vector2(-speed, 0);

        if (attack)
        {
            rb.velocity = new Vector2(0, 0);
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            hero = collision.gameObject;
            attack = true;
        }
    }
}
