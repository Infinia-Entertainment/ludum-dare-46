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
 * Bug: When they move and you put the hero back in the middle they stop again
 * 
 * What should happen: The ones behind the character continue going through, and infront stop (or snap back?)
 */
