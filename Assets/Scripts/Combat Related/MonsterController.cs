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
        Physics.Raycast(transform.position, Vector3.left, out hitInfo, 10, 1 << 13); //13 is Hero layer
        Debug.DrawRay(transform.position, Vector3.left * hitInfo.distance,Color.red,5);


        if (!Physics.Raycast(transform.position, Vector3.left, out hitInfo, 10, 1 << 13))
        {
            Debug.Log("returned");
            return;
        }
        Debug.Log("didn't return");

        //Check for attack Rate
        if (Time.time - lastAttack >= monsterData.attackRate)
        {
            Debug.Log("attack cooldown thing");

            //Just checks if attack is within range
            if (hitInfo.distance <= monsterData.attackDistance && isFrontOccupied)
            {
                Debug.Log("monster attacked");

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
