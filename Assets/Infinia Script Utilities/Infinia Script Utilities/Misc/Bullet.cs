using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed = 50;
    public float knockbackForce = 12;
    public Vector2 moveDir;
    private Rigidbody2D rb;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            Unit enemyUnit = collision.gameObject.GetComponent<Unit>();
            enemyUnit.ReceiveKnockBackFromMovementDirection(moveDir, knockbackForce);
            enemyUnit.ReceiveDamage(1);
        }

        Destroy(gameObject);
    }


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = moveDir * bulletSpeed;

        Destroy(gameObject, 2f);
    }
}
    
