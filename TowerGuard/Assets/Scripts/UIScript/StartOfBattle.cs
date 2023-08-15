using UnityEngine;

public class StartOfBattle : MonoBehaviour
{
    private const string CAMERA_BATTLE_MOD = "BattleMod";
    private const string BACK_OF_BUTTON = "Back";

    private int _timeToStart = 1;

    [SerializeField] private GameObject _towersHealthBar;
    [SerializeField] private GameObject _gameInteraction;
    [SerializeField] private GameObject _waveManager;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Animator _camerasAnimator;
    [SerializeField] private Animator _startButtonAnimator;
    [SerializeField] private Animator _logoAnimator;
    [SerializeField] private GameObject _soundButton;
    [SerializeField] private GameObject _interactionPanel;
    [SerializeField] private GameObject _coinsScore;
    [SerializeField] private GameObject _logo;
    [SerializeField] private AudioSource _selectSound;

    public void StartOfGame()
    {
        _selectSound.Play();
        _camerasAnimator.SetTrigger(CAMERA_BATTLE_MOD);
        
        _towersHealthBar.SetActive(true);
        _gameInteraction.SetActive(true);
        _coinsScore.SetActive(true);
        _soundButton.SetActive(true);
        _interactionPanel.SetActive(true);
        _waveManager.SetActive(true);
        _pauseButton.SetActive(true);

        _panel.SetActive(false);

        _startButtonAnimator.SetTrigger(BACK_OF_BUTTON);
        _logoAnimator.SetTrigger(BACK_OF_BUTTON);

        Invoke("ButtonOff", _timeToStart);
    }

    private void ButtonOff() => _startButton.SetActive(false);
}
