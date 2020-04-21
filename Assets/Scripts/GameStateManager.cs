using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameStateManager : MonoBehaviour
{

    [SerializeField] List<GameObject> currentWeapons = new List<GameObject>();
    [SerializeField] List<GameObject> currentHeroes = new List<GameObject>();

    [SerializeField] List<GameObject> currentUnusedHeroes = new List<GameObject>();

    [SerializeField] GameObject heroPrefab;

    [SerializeField] private int currentgold;
    [SerializeField] private int totalGoldFromStage;

    [SerializeField] private int blacksmithMaxHealth = 100;
    [SerializeField] private int blacksmithCurrentHealth;

    [SerializeField] private int currentStageIndex = 0;

    private static GameStateManager _gameStateManager;

    [SerializeField] private List<Stage> gameStages;

    public static GameStateManager Instance { get => _gameStateManager;}

    public float spacingBetweenHeroes = 2.5f;
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
        currentgold = 100;

        GameObject hero1 = Instantiate(heroPrefab);
        GameObject hero2 = Instantiate(heroPrefab);
        GameObject hero3 = Instantiate(heroPrefab);

        AddHero(hero1);
        AddHero(hero2);
        AddHero(hero3);
    }

    private void transitionToBattle()
    {
        StoreWeapons();
        StoreHeores();

        MoveUnusedHeroes();



        StartCoroutine(LoadBattleScene());

        
        //Lul because we're such good programmers lmao
        

        

        


        
        //waveManager.currentStage = gameStages[currentStageIndex];
    }

    private IEnumerator LoadBattleScene()
    {
        // Start loading the scene
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(1);
        // Wait until the level finish loading
        while (!asyncLoadLevel.isDone)
            yield return null;
        // Wait a frame so every Awake and Start method is called

        HideHeroes();
        HideWeapons();

        for (int i = 0; i < currentHeroes.Count; i++)
        {
            HeroController heroController = currentHeroes[i].GetComponent<HeroController>();
            currentWeapons[i].GetComponent<Animator>().enabled = false;
            heroController.weaponObject = currentWeapons[i];
            heroController.InitializeWeaponPosition();
        }

        InstantiateHeroUI();


        WaveManager waveManager = FindObjectOfType<WaveManager>();
        Debug.Log(SceneManager.GetActiveScene().name);

        Debug.Log(waveManager);
        yield return new WaitForEndOfFrame();
    }
    private IEnumerator LoadShopScene()
    {
        // Start loading the scene
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(0);
        // Wait until the level finish loading
        while (!asyncLoadLevel.isDone)
            yield return null;
        // Wait a frame so every Awake and Start method is called

        CalculateGold();

        blacksmithCurrentHealth = blacksmithMaxHealth;

        yield return new WaitForEndOfFrame();
    }
    private void MoveUnusedHeroes()
    {
        int numberOfunusedHeroes = currentHeroes.Count - currentWeapons.Count;

        for (int i = 0; i < numberOfunusedHeroes; i++)
        {
            int movedHeroIndex = currentHeroes.Count - 1;

            currentUnusedHeroes.Add(currentHeroes[movedHeroIndex]);
            currentHeroes.RemoveAt(movedHeroIndex);

        }
        
    }

    public bool CanBuyWeapon(int price)
    {
        //returns true if there are enough heroes to buy it

        if (currentgold <= price)
        {
            return true;
        }
        else return false;
    }

    public bool HasEnougHeroes()
    {
        //returns true if there are enough heroes to buy it

        if (currentWeapons.Count < currentHeroes.Count)
        {
            return true;
        }
        else return false;
    }

    private void transitionToShop()
    {
        DeleteDeadHeroes();
        StoreHeores();
        DeleteWeaponData();

        StartCoroutine(LoadShopScene());

       
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

    public void BuyWeapon(int price, GameObject weapon)
    {
        AddWeapon(weapon);
        currentgold -= price;
    }

    private void AddWeapon(GameObject weapon)
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
        Debug.Log("Func runs..");
        Debug.Log(currentHeroes);
        //Vector3 heroPosition = FindObjectOfType<HeroPlacement>().transform.position;
        //for (int i = 0; i < currentHeroes.Count; i++)
        //{
        //    Debug.Log("Moving.." + currentHeroes[i].name);
        //   //Move heroes to a specific position in a line
        //    currentHeroes[i].transform.position = heroPosition;
        //    heroPosition = new Vector3(heroPosition.x + spacingBetweenHeroes, heroPosition.y, heroPosition.z);
        //}
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

        currentgold = baseGold + totalGoldFromStage;

        totalGoldFromStage = 0;
    }

    #endregion
}