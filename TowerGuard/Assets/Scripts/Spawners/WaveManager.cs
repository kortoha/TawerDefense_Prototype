using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Waves[] _waves;
    [SerializeField] private GameObject[] _spawnersArray;
    [SerializeField] private GameObject _interactPanel;

    [SerializeField] private GameObject _mainBackgroundMusic;
    [SerializeField] private AudioSource _deathMatchBackgroundMusic;

    private int _currentEnemyIndex;
    private int _currentWaveIndex;

    private void Start()
    {
        StartWave(0);
    }

    private void StartWave(int waveIndex)
    {
        _currentWaveIndex = waveIndex;
        _currentEnemyIndex = 0;

        foreach (var waveSetting in _waves[waveIndex].WaveSettings)
        {
            waveSetting.DeactivateSpawner();
        }

        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (_currentEnemyIndex < _waves[_currentWaveIndex].WaveSettings.Length)
        {
            WaveSettings waveSettings = _waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex];

            waveSettings.ActivateSpawner();

            yield return new WaitForSeconds(waveSettings.SpawnDelay);

            waveSettings.DeactivateSpawner();

            _currentEnemyIndex++;

            yield return new WaitForSeconds(2f);
        }

        _currentEnemyIndex = 0;


        _currentWaveIndex++;
        if (_currentWaveIndex < _waves.Length)
        {
            StartWave(_currentWaveIndex);
        }
        else
        {
            if (MainTower.Instance != null)
            {
                foreach (var item in _spawnersArray)
                {
                    item.SetActive(true);
                }
                _interactPanel.SetActive(false);
                _mainBackgroundMusic.SetActive(false);
                _deathMatchBackgroundMusic.Play();
                ArrowsDamage.Instance.DeathMod();
            }
            else
            {
                foreach (var item in _spawnersArray)
                {
                    item.SetActive(false);
                }
            }
        }
    }
}

[System.Serializable]
public class Waves
{
    [SerializeField] private WaveSettings[] _waveSettings;
    public WaveSettings[] WaveSettings { get => _waveSettings; }
}

[System.Serializable]
public class WaveSettings
{
    [SerializeField] private GameObject[] _spawner;
    public GameObject[] Spawner { get => _spawner; }
    [SerializeField] private float _spawnDelay;
    public float SpawnDelay { get => _spawnDelay; }

    public void ActivateSpawner()
    {
        foreach (var item in _spawner)
        {
            item.SetActive(true);
        }
    }

    public void DeactivateSpawner()
    {
        foreach (var item in _spawner)
        {
            item.SetActive(false);
        }
    }
}