using System;
using UnityEngine;

public class MonsterController : AliveUnit
{
    private bool isFrontOccupied;
    public GameObject hero;
    private MonsterController monsterInFront;

    private float lastAttack;

    //Monster stats
    public MonsterData monsterData;

    private void Awake()
    {
        health = monsterData.health;

        lastAttack = Time.time;
    }

    private void Start()
    {
        isFrontOccupied = false;
    }

    private void Update()
    {

        if (monsterInFront == null && hero == null)
        {
            isFrontOccupied = false;
        }

        Move();

        CheckForAttack();
    }

    private void CheckForAttack()
    {
        RaycastHit hitInfo;
         //13 is Hero layer
        

        if (!Physics.Raycast(transform.position, Vector3.left, out hitInfo, 10, 1 << 13)) // 1 << 13 Hero layer
        {

            return;
        }

        Debug.DrawRay(transform.position, Vector3.left * hitInfo.distance, Color.red, 1);


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

    private void CheckNextMonster()
    {
        {
            if (monsterInFront.isFrontOccupied)
            {
                isFrontOccupied = true;
            }
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hero"))
        {
            hero = other.gameObject;
            isFrontOccupied = true;
        }
        else if(other.gameObject.CompareTag("Monster"))
        {
            monsterInFront = other.gameObject.GetComponent<MonsterController>();
            CheckNextMonster();
        }

    }

    private void Ontriggerstay(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            monsterInFront = other.gameObject.GetComponent<MonsterController>();
            CheckNextMonster();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Hero"))
        {
            isFrontOccupied = false;
        }
        else if (other.gameObject.CompareTag("Monster"))
        {
            isFrontOccupied = false;
        }
    }
}

/*
 * Bug: When they move and you put the hero back in the middle they stop again
 * 
 * What should happen: The ones behind the character continue going through, and infront stop (or snap back?)
 */
