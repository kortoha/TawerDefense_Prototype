using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private const string NAME_OF_FAIDING_TRIGER = "Brightening";

    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _backgroundFade;
    [SerializeField] private GameObject _gameInteraction;
    [SerializeField] private GameObject _towersHealthBar;
    [SerializeField] private Animator _brighteningAnimator;
    [SerializeField] private GameObject _brightening;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _soundButton;
    [SerializeField] private GameObject _scorePanel;
    [SerializeField] private GameObject _interactionPanel;
    [SerializeField] private GameObject _coinsScore;
    [SerializeField] private AudioSource _selectSound;



    private bool _isPaused = false;
    private float _timeToRestart = 0.8f;

    public void TogglePause()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            if (_selectSound.enabled)
            {
                _selectSound.Play();
            }
            _menuPanel.SetActive(true);
            _restartButton.SetActive(true);
            _backgroundFade.SetActive(true);
            _gameInteraction.SetActive(false);
            _interactionPanel.SetActive(false);
            _coinsScore.SetActive(false);
            Time.timeScale = 0;
        }
        
         if(!_isPaused)
        {
            if (_selectSound.enabled)
            {
                _selectSound.Play();
            }
            _menuPanel.SetActive(false);
            _restartButton.SetActive(false);
            _backgroundFade.SetActive(false);
            _gameInteraction.SetActive(true);
            _interactionPanel.SetActive(true);
            _coinsScore.SetActive(true);
            Time.timeScale = 1;
        }
    }

    public void Restart()
    {
        if(MainTower.Instance != null)
        {
            if (_selectSound.enabled)
            {
                _selectSound.Play();
            };
            _restartButton.SetActive(false);
            _menuPanel.SetActive(false);
            _backgroundFade.SetActive(false);
            _towersHealthBar.SetActive(false);
            Destroy(_scorePanel);
            _soundButton.SetActive(false);
            _pauseButton.SetActive(false);
            _interactionPanel.SetActive(false);
            _coinsScore.SetActive(false);
            MainTower.Instance.TowerDestroy();
            ArrowsDamage.Instance.ArrowsDestroy();
            Invoke("LoadScene", _timeToRestart);
            _brightening.SetActive(true);
            _brighteningAnimator.SetTrigger(NAME_OF_FAIDING_TRIGER);

            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }

        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
}