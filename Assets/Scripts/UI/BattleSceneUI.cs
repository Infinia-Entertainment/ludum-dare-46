using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleSceneUI : MonoBehaviour
{
    [SerializeField] private GameObject waveCounterObj;
    [SerializeField] private TMP_Text waveCounterText;

    private void UpdateWaveCounterUI()
    {
        waveCounterText.text = $"Waves {WaveManager.Instance.WaveCount}/{WaveManager.Instance.currentStage?.Waves.Count}";
    }

    void Update()
    {
        UpdateWaveCounterUI();
    }

    public void EnableUI()
    {
        waveCounterObj.SetActive(true);
    }
}
