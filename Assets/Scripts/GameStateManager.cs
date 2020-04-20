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

    [SerializeField] List<GameObject> currentWeapons = new List<GameObject>();
    [SerializeField] List<GameObject> currentHeroes = new List<GameObject>();

    //[SerializeField] List<GameObject> currentStayedHeroes = new List<GameObject>();

    [SerializeField] GameObject heroPrefab;


    [SerializeField] private int gold;
    [SerializeField] private int totalGoldFromStage;

    [SerializeField] private int blacksmithMaxHealth = 100;
    [SerializeField] private int blacksmithCurrentHealth;

    [SerializeField] private int currentStageIndex;

    private static GameStateManager _gameStateManager;

    public static GameStateManager Instance { get => _gameStateManager;}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddWeapon(FindObjectOfType<WeaponCreationSystem>().CreateTestWeapon());
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            StartBattle();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            WinStage(FindObjectOfType<WaveManager>().currentStage);
        }
    }

    private void Awake()
    {

        //Singleton 
        if (_gameStateManager != null && _gameStateManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _gameStateManager = this;
        }


        DontDestroyOnLoad(gameObject);
        InitializeFirstTime();
    }

    private void InitializeFirstTime()
    {
        blacksmithCurrentHealth = blacksmithMaxHealth;
        gold = 100;

        GameObject firstHero = Instantiate(heroPrefab);
        GameObject secondHero = Instantiate(heroPrefab);

        AddHero(firstHero);
        AddHero(secondHero);
    }

    private void transitionToBattle()
    {
        StoreWeapons();
        StoreHeores();

        SceneManager.LoadScene(1);

        //Lul because we're such good programmers lmao
        HideHeroes();
        HideWeapons();

        InstantiateHeroUI();
        //Some heroes stay
        //code here

        //assign weapons to heros
        //fix their positions

        for (int i = 0; i < currentHeroes.Count; i++)
        {
            HeroController heroController = currentHeroes[i].GetComponent<HeroController>();
            heroController.weaponObject = currentWeapons[i];
            heroController.InitializeWeaponPosition();
        }

        //Add weapons position initialization
        //In Each Hero maybe?
    }

    private void transitionToShop()
    {

        DeleteDeadHeroes();
        StoreHeores();
        DeleteWeaponData();

        SceneManager.LoadScene(0);

        CalculateGold();

        blacksmithCurrentHealth = blacksmithMaxHealth;
    }

    public void WinStage(Stage wonStage)
    {
        for (int i = 0; i < wonStage.heroReward; i++)
        {
            GameObject heroObj = Instantiate(heroPrefab);

            AddHero(heroObj);
        }

        currentStageIndex++;

        transitionToShop();
    }

    public void StartBattle()
    {
        transitionToBattle();
    }

    #region Data_Manipulation
    public void AddHero(GameObject heroObject)
    {
        currentHeroes.Add(heroObject);
    }

    public void AddWeapon(GameObject weapon)
    {
        currentWeapons.Add(weapon);
    }

    private void StoreWeapons()
    {
        //we make sure they don't destroy
        foreach (GameObject weaponObject in currentWeapons)
        {
            DontDestroyOnLoad(weaponObject);
        }
    }

    private void StoreHeores()
    {
        foreach (GameObject hero in currentHeroes)
        {
            DontDestroyOnLoad(hero);
        }
    }

    private void DeleteDeadHeroes()
    {
        //Heroes get destroyed when they die otherwise we keep them
        for (int i = 0; i < currentHeroes.Count; i++)
        {
            //check if they are destroyed and clean up the node
            if (currentHeroes[i] == null)
            {
                currentHeroes.RemoveAt(i);
            }
        }
    }

    private void DeleteWeaponData()
    {
        foreach (GameObject weaponObject in currentWeapons)
        {
            Destroy(weaponObject);
        }
        currentWeapons = new List<GameObject>();
    }
    #endregion

    #region Battle_Related
    private void HideHeroes()
    {
        for (int i = 0; i < currentHeroes.Count; i++)
        {
            currentHeroes[i].transform.position = new Vector3((i + 1), 0, -10);
        }

    }

    private void HideWeapons()
    {
        for (int i = 0; i < currentWeapons.Count; i++)
        {
            currentWeapons[i].transform.position = new Vector3(-(i + 1), 0, -10);
        }
    }
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
        SceneManager.LoadScene(0);

        Debug.Log("You lost the game dingus");
    }

    #endregion

    #region Shop_Related
    private void CalculateGold()
    {
        int baseGold = 100 * currentStageIndex;

        gold = baseGold + totalGoldFromStage;

        totalGoldFromStage = 0;
    }

    #endregion
}