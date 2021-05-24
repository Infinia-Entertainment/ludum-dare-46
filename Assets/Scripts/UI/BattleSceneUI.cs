using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleSceneUI : MonoBehaviour
{
    [Header("Wave Count UI")]
    [SerializeField] private GameObject waveCounterObj;
    [SerializeField] private TMP_Text waveCounterText;

    [Space]
    [Header("Game Result UI")]
    [SerializeField] private float timeToWriteEntireText;
    [SerializeField] private GameObject gameResultPanel;
    [SerializeField] private TMP_Text gameResultText;
    [SerializeField] private TMP_Text gameResultButtonText;
    [SerializeField] private TMP_Text monstersKilledCounterText;
    [SerializeField] private TMP_Text damageDoneCounterText;
    [SerializeField] private TMP_Text monstersPassedCounterText;
    [SerializeField] private TMP_Text damageReceivedCounterText;
    [SerializeField] private TMP_Text goldEarnedCounterText;

    private bool isAnimatingGameResultText = false;
    private float animTimer = 0;

    bool hasWon;
    int monstersKilled;
    int damageDone;
    int monstersPassed;
    int damageReceived;
    int goldEarned;

    void Update()
    {
        AnimateGameResultUI();
        UpdateWaveCounterUI();

        if (Input.GetKeyDown(KeyCode.N)) GameStateManager.Instance.TransitionToShop();
    }

    private void AnimateGameResultUI()
    {
        if (isAnimatingGameResultText)
        {
            gameResultText.text = hasWon ? "Victory!" : "Defeat!";
            gameResultButtonText.text = hasWon ? "Next Stage" : "Restart";


            if (animTimer <= timeToWriteEntireText)
            {
                float timeProgress = animTimer / timeToWriteEntireText;

                monstersKilledCounterText.text = $"{Mathf.RoundToInt(monstersKilled * timeProgress)}";
                damageDoneCounterText.text = $"{Mathf.RoundToInt(damageDone * timeProgress)}";
                monstersPassedCounterText.text = $"{Mathf.RoundToInt(monstersPassed * timeProgress)}";
                damageReceivedCounterText.text = $"{Mathf.RoundToInt(damageReceived * timeProgress)}";
                goldEarnedCounterText.text = $"{Mathf.RoundToInt(goldEarned * timeProgress)}";

                animTimer += Time.deltaTime;
            }
            else
            {
                monstersKilledCounterText.text = $"{monstersKilled}";
                damageDoneCounterText.text = $"{damageDone}";
                monstersPassedCounterText.text = $"{monstersPassed}";
                damageReceivedCounterText.text = $"{damageReceived}";
                goldEarnedCounterText.text = $"{goldEarned}";

                isAnimatingGameResultText = false;
                animTimer = 0;
            }



        }
    }

    [ContextMenu("Test Game Result UI")]
    public void TestGameResultUI()
    {
        InitializResultUI(System.Convert.ToBoolean(UnityEngine.Random.Range(0, 2)), Random.Range(1, 30), Random.Range(1, 500), Random.Range(1, 10), Random.Range(1, 250), Random.Range(1, 1000));
    }

    public void InitializResultUI(bool hasWon, int monstersKilled, int damageDone, int monstersPassed, int damageReceived, int goldEarned)
    {
        this.hasWon = hasWon;
        this.monstersKilled = monstersKilled;
        this.damageDone = damageDone;
        this.monstersPassed = monstersPassed;
        this.damageReceived = damageReceived;
        this.goldEarned = goldEarned;

        isAnimatingGameResultText = true;
    }

    private void UpdateWaveCounterUI()
    {
        waveCounterText.text = $"Waves {WaveManager.Instance.WaveCount}/{WaveManager.Instance.currentStage?.Waves.Count}";
    }


    public void EnableUI()
    {
        waveCounterObj.SetActive(true);
    }

    public void EnableResultUI()
    {
        gameResultPanel.SetActive(true);
    }

    public void GameResultButton()
    {
        if (hasWon) GameStateManager.Instance.TransitionToShop();
        else GameStateManager.Instance.RestartGame();
    }
}
