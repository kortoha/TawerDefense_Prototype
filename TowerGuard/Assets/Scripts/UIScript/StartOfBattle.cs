using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartOfBattle : MonoBehaviour
{
    private const string CAMERA_BATTLE_MOD = "BattleMod";

    private int _timeToStart = 1;

    [SerializeField] private GameObject _towersHealthBar;
    [SerializeField] private GameObject _gameInteraction;
    [SerializeField] private GameObject[] _enemySpawner;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Animator _camerasAnimator;
    [SerializeField] private GameObject _soundButton;
    [SerializeField] private GameObject _interactionButton;
    [SerializeField] private GameObject _coinsScore;


    public void StartOfGame()
    {
        _camerasAnimator.SetTrigger(CAMERA_BATTLE_MOD);
        
        _towersHealthBar.SetActive(true);
        _gameInteraction.SetActive(true);
        _coinsScore.SetActive(true);
        _soundButton.SetActive(true);
        _interactionButton.SetActive(true);

        foreach (var item in _enemySpawner)
        {
            item.SetActive(true);
        }

        _startButton.SetActive(false);
        _pauseButton.SetActive(true);
        _panel.SetActive(false);
    }
}
