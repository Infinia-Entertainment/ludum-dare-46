﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithDamager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            MonsterController monster = other.gameObject.GetComponent<MonsterController>();

            Debug.Log(GameStateManager.Instance);
            Debug.Log(monster.monsterData.playerDamage);
            GameStateManager.Instance.DamageBlacksmith(monster.monsterData.playerDamage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            Destroy(other.gameObject);
        }
    }
}
