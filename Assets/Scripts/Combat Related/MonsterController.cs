using System;
using UnityEngine;

public class MonsterController : AliveUnit
{
    public bool isFrontOccupied;
    public float speed;
    public GameObject hero;
    public MonsterController monsterInFront;
    // Start is called before the first frame update
    private void Start()
    {
        isFrontOccupied = false;
    }

    private void Update()
    {
        CheckForHealth();

        if (monsterInFront == null && hero == null)
        {
            isFrontOccupied = false;
        }

        Move();

        if (hero !=null)
        {
            Attack();
        }
    }

    private void Attack()
    {
        //do attack stuff
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
            translation = new Vector3(0, 0, -speed);
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
 * Move left
 * Check if hero infront
 * if yes stop otherwise continue
 * if you're infront of the monster, and that monster is stopped, stop as well
 * Wait till he's continues then continue too
 * 
 * If you're infront of the hero then attack (and anims etc)
 * 
 */
