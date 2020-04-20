using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    /*
     * 2 scenes
     *
     * blacksmith and battle
     *
     *  if in blacksmith scene control gold and weapon creation and movement
     *
     *  transitionToBattle()
     *  {
     *      save weapon data,
     *      instantiate heroes (into UI),
     *      //Add weapons position initialization
     *
     *  }
     *
     *  transitionToShop()
     *  {
     *      keep heroes, delete weapons
     *      gold stuff = base gold + monster gold
     *  }
     *
     *  if in the battle scene check if enemies pass through
     *  deduct from health
     *  blah blah dead
     *
     *
     */

    List<GameObject> weaponPrefabs = new List<GameObject>();
    List<GameObject> savedHeroes = new List<GameObject>();

    GameObject heroPrefab;

    private static int gold;
    private int totalGoldFromStage;

    private int blacksmithMaxHealth;
    private int blacksmithCurrentHealth;

    private int currentStageIndex;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void InitializeFirstTime()
    {
        gold = 100;

        GameObject firstHero = Instantiate(heroPrefab);

        AddHero(firstHero);
    }

    private void transitionToBattle()
    {
        //SceneManager.LoadScene();

        StoreWeaponData();
        InstantiateHeroUI();

        //Add weapons position initialization
        //In Each Hero maybe?
    }
    
   
    private void transitionToShop()
    {
        //SceneManager.LoadScene();

        DeleteDeadHeroes();
        StoreHeores();
        DeleteWeaponData();
        CalculateGold();

        blacksmithCurrentHealth = blacksmithMaxHealth;
    }

    
    

    #region Data_Manipulation
    public void AddHero(GameObject heroObject)
    {
        savedHeroes.Add(heroObject);
    }

    public void AddWeapon(GameObject weapon)
    {
        weaponPrefabs.Add(weapon);
    }

    private void StoreWeaponData()
    {
        //we make sure they don't destroy
        foreach (GameObject weaponObject in weaponPrefabs)
        {
            DontDestroyOnLoad(weaponObject);
        }
    }

    private void StoreHeores()
    {
        foreach (GameObject hero in savedHeroes)
        {
            DontDestroyOnLoad(hero);
        }
    }

    private void DeleteDeadHeroes()
    {
        //Heroes get destroyed when they die otherwise we keep them
        for (int i = 0; i < savedHeroes.Count; i++)
        {
            //check if they are destroyed and clean up the node
            if (savedHeroes[i] == null)
            {
                savedHeroes.RemoveAt(i);
            }
        }
    }

    private void DeleteWeaponData()
    {
        foreach (GameObject weaponObject in weaponPrefabs)
        {
            Destroy(weaponObject);
        }
        weaponPrefabs = new List<GameObject>();
    }
    #endregion

    #region Battle_Related

    private void InstantiateHeroUI()
    {
        // stuff here for UI in the battle if need be
        //Get blah blah
        //Do blah blah
    }

    public void DamageBlacksmith(int damage)
    {
        blacksmithCurrentHealth -= damage;

        if (blacksmithCurrentHealth <= 0)
        {
            LoseGame();
        }
    }

    private void LoseGame()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Shop_Related
    private void CalculateGold()
    {
        int baseGold = 100 * currentStageIndex;

        gold = baseGold + totalGoldFromStage;
    }

    #endregion
}