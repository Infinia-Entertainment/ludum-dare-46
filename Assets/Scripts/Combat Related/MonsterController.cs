using System;
using UnityEngine;

public class MonsterController : AliveUnit
{
    private bool isFrontOccupied;
    [SerializeField] public GameObject hero;
    [SerializeField] private MonsterController monsterInFront;

    private float lastAttack;
    //Monster stats
    public MonsterData monsterData;

    LayerMask layerMask = 1 << 12 | 1 << 13; // Hero and Monster layer combined

    float raycastLength;

    private void Awake()
    {
        raycastLength = GetComponent<Collider>().bounds.size.x / 2;

        health = monsterData.health;

        lastAttack = Time.time;
    }

    private void Start()
    {
        isFrontOccupied = false;
    }

    private void Update()
    {
        

        Move();

        CheckForAttack();
    }

    private void CheckForAttack()
    {
        RaycastHit hitInfo;

        Debug.DrawRay(transform.position, Vector3.left * raycastLength, Color.magenta);

        if (!Physics.Raycast(transform.position, Vector3.left, out hitInfo, raycastLength, layerMask)) 
        {
            Debug.DrawRay(transform.position,Vector3.left * hitInfo.distance,Color.cyan);
            //Didn't collide with either hero or monster so you can move forward
            isFrontOccupied = false;
            return;
        }

        //If did collide, check which one has is the first collision

        if (hitInfo.collider.gameObject.CompareTag("Hero"))//It's a hero
        {
            hero = hitInfo.collider.gameObject;
            isFrontOccupied = true;
        }
        else if (hitInfo.collider.gameObject.CompareTag("Monster")) //It's a monster
        {
            monsterInFront = hitInfo.collider.gameObject.GetComponent<MonsterController>();
            isFrontOccupied = true;
        }
        

        //Check for attack Rate
        if (Time.time - lastAttack > monsterData.attackRate)
        {
            //Just checks if attack is within range
            if (hitInfo.distance <= monsterData.attackDistance && isFrontOccupied)
            {
                //attack anims etc
                hitInfo.collider.gameObject.GetComponent<AliveUnit>().ReceiveDamage(monsterData.damage);
            }
            lastAttack = Time.time;
        }
    }

    private void Move()
    {
        Vector3 translation;

        if (isFrontOccupied)
        {
            translation = new Vector3(0, 0, 0);
        }
        else
        {
            translation = new Vector3(0, 0, -monsterData.speed);
        }

        transform.Translate(translation * Time.deltaTime);
    }
}

/*
 * Refactoring to fix a bug:
 * Check whatever is infront with a raycast instead of collisions
 */

