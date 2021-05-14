using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Sirenix.Utilities;

public class GameStateManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> _currentWeapons = new List<GameObject>();
    [SerializeField] private List<GameObject> _currentHeroes = new List<GameObject>();

    [SerializeField] private List<GameObject> _currentUnusedHeroes = new List<GameObject>();

    [SerializeField] private GameObject _heroPrefab;

    [SerializeField] private int _currentGold;
    [SerializeField] private int _totalGoldFromStage;

    [SerializeField] private int _blacksmithMaxHealth = 100;
    [SerializeField] private int _blacksmithCurrentHealth;

    [SerializeField] private int _currentStageIndex = 0;
    [SerializeField] private int _currentEmptyDisplayHero = 0;

    private static GameStateManager _gameStateManager;

    [SerializeField] private List<Stage> _gameStages;

    public List<Transform> _spawnPoints = new List<Transform>();

    public static GameStateManager Instance { get => _gameStateManager; }
    public int CurrentStageIndex { get => _currentStageIndex; }
    public List<Stage> GameStages { get => _gameStages; }
    public List<GameObject> CurrentWeapons { get => _currentWeapons; }
    public List<GameObject> CurrentHeroes { get => _currentHeroes; }

    public float spacingBetweenHeroes = 0.75f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddWeapon(FindObjectOfType<WeaponCreationSystem>().CreateTestWeapon());
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
            return;
        }
        else
        {
            _gameStateManager = this;
        }

        _spawnPoints = FindObjectOfType<SpawnPointsData>().spawnPoints;

        DontDestroyOnLoad(gameObject);
        InitializeFirstTime();

        InitializeHeroDisplayPositions();
    }

    private void InitializeFirstTime()
    {
        _blacksmithCurrentHealth = _blacksmithMaxHealth;
        _currentGold = 100;

        GameObject firstHero = Instantiate(_heroPrefab);
        AddHero(firstHero);
    }

    private void InitializeHeroDisplayPositions()
    {
        _spawnPoints = FindObjectOfType<SpawnPointsData>().spawnPoints;


        for (int i = 0; i < _currentHeroes.Count; i++)
        {
            _currentHeroes[i].transform.position = _spawnPoints[i].position; //set position
            _currentHeroes[i].transform.eulerAngles = _spawnPoints[i].eulerAngles; //set rotation
        }
    }

    private void TransitionToBattle()
    {
        StoreWeapons();
        StoreHeroes();
        MoveUnusedHeroes();
        StartCoroutine(LoadBattleScene());
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

        AssignWeaponsToHeroes();

        InstantiateHeroUI();

        //If null reference add the test stage to game manager stage list stuff
        WaveManager.Instance.currentStage = _gameStages[_currentStageIndex];

        FindObjectOfType<BattleSceneUI>().EnableUI();

        WaveManager.Instance.StartCoroutine(WaveManager.Instance.StartSpawning());

        yield return new WaitForEndOfFrame();
    }

    private void AssignWeaponsToHeroes()
    {
        for (int i = 0; i < _currentHeroes.Count; i++)
        {
            HeroController heroController = _currentHeroes[i].GetComponent<HeroController>();
            _currentWeapons[i].GetComponent<Animator>().enabled = false;

            heroController.weaponObject = _currentWeapons[i];
            heroController.InitializeWeaponPosition();
            heroController.InitalizeWeaponData();
        }
    }

    private IEnumerator LoadShopScene()
    {
        // Start loading the scene
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(0);
        // Wait until the level finish loading
        while (!asyncLoadLevel.isDone)
            yield return null;
        // Wait a frame so every Awake and Start method is called

        InitializeHeroDisplayPositions();
        CalculateGold();

        _blacksmithCurrentHealth = _blacksmithMaxHealth;

        yield return new WaitForEndOfFrame();
    }

    private void MoveUnusedHeroes()
    {
        int numberOfunusedHeroes = _currentHeroes.Count - _currentWeapons.Count;

        for (int i = 0; i < numberOfunusedHeroes; i++)
        {
            int movedHeroIndex = _currentHeroes.Count - 1;

            _currentUnusedHeroes.Add(_currentHeroes[movedHeroIndex]);
            _currentHeroes.RemoveAt(movedHeroIndex);

        }

    }

    public bool CanBuyWeapon(int price)
    {
        //returns true if there are enough heroes to buy it

        if (_currentGold <= price)
        {
            return true;
        }
        else return false;
    }

    public bool HasEnougHeroes()
    {
        //returns true if there are enough heroes to buy it

        if (_currentWeapons.Count < _currentHeroes.Count)
        {
            return true;
        }
        else return false;
    }

    private void transitionToShop()
    {
        DeleteDeadHeroes();
        StoreHeroes();
        DeleteWeaponData();

        StartCoroutine(LoadShopScene());
    }

    public void WinStage(Stage wonStage)
    {
        WaveManager.Instance.StopCoroutine(WaveManager.Instance.StartSpawning());
        for (int i = 0; i < wonStage.heroReward; i++)
        {
            GameObject heroObj = Instantiate(_heroPrefab);

            AddHero(heroObj);


        }

        _currentStageIndex++;

        transitionToShop();
    }

    public void StartBattle()
    {
        TransitionToBattle();
    }

    #region Data_Manipulation
    public void AddHero(GameObject heroObject)
    {
        _currentHeroes.Add(heroObject);
    }



    private void AddWeapon(GameObject weapon)
    {
        _currentWeapons.Add(weapon);

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //UpdateWeaponDisplay();
        }
    }

    public void UpdateWeaponDisplay()
    {
        HeroController heroController = _currentHeroes[_currentEmptyDisplayHero].GetComponent<HeroController>();
        _currentWeapons[_currentWeapons.Count - 1].GetComponent<Animator>().enabled = false;
        _currentEmptyDisplayHero++;

        heroController.weaponObject = _currentWeapons[_currentWeapons.Count - 1];
        heroController.InitializeWeaponPosition();
    }

    private void StoreWeapons()
    {
        //we make sure they don't destroy
        foreach (GameObject weaponObject in _currentWeapons)
        {
            DontDestroyOnLoad(weaponObject);
        }
    }

    private void StoreHeroes()
    {
        foreach (GameObject hero in _currentHeroes)
        {
            DontDestroyOnLoad(hero);
        }
    }

    private void DeleteDeadHeroes()
    {
        //Heroes get destroyed when they die otherwise we keep them
        for (int i = 0; i < _currentHeroes.Count; i++)
        {
            //check if they are destroyed and clean up the node
            if (_currentHeroes[i] == null)
            {
                _currentHeroes.RemoveAt(i);
            }
        }
    }

    private void DeleteWeaponData()
    {
        foreach (GameObject weaponObject in _currentWeapons)
        {
            Destroy(weaponObject);
        }
        _currentWeapons = new List<GameObject>();
    }
    #endregion

    #region Battle_Related
    private void HideHeroes()
    {
        for (int i = 0; i < _currentHeroes.Count; i++)
        {
            _currentHeroes[i].transform.position = new Vector3((i + 1), 0, -25);
        }

    }

    private void HideWeapons()
    {
        for (int i = 0; i < _currentWeapons.Count; i++)
        {
            _currentWeapons[i].transform.position = new Vector3(-(i + 1), 0, -10);
        }
    }
    private void InstantiateHeroUI()
    {
        Vector3 heroPosition = new Vector3(-4, 0, 8);
        for (int i = 0; i < _currentHeroes.Count; i++)
        {
            _currentHeroes[i].transform.position = heroPosition;
            _currentHeroes[i].transform.eulerAngles = new Vector3(0, -90, 0);
            heroPosition = new Vector3(heroPosition.x + spacingBetweenHeroes, heroPosition.y, heroPosition.z);
        }
    }

    public void DamageBlacksmith(int damage)
    {
        _blacksmithCurrentHealth -= damage;

        if (_blacksmithCurrentHealth <= 0)
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

    public bool IsAffordable(int price)
    {
        return (price <= _currentGold) ? true : false;
    }
    public void BuyWeapon(int price, GameObject weapon)
    {
        if (IsAffordable(price))
        {
            AddWeapon(weapon);
            _currentGold -= price;
        }
        else
        {
            Debug.LogError("Not enough gold to buy (You shouldn't be able to buy if so)");
        }
    }

    private void CalculateGold()
    {
        int baseGold = 100 * (_currentStageIndex + 1);
        _currentGold = baseGold + _totalGoldFromStage;
        _totalGoldFromStage = 0;
    }

    #endregion


}