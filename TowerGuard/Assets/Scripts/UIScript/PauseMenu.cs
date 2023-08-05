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



    private bool _isPaused = false;
    private float _timeToRestart = 0.5f;

    public void TogglePause()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            _menuPanel.SetActive(true);
            _restartButton.SetActive(true);
            _backgroundFade.SetActive(true);
            _gameInteraction.SetActive(false);
            Time.timeScale = 0;
        }
        
         if(!_isPaused)
        {
            _menuPanel.SetActive(false);
            _restartButton.SetActive(false);
            _backgroundFade.SetActive(false);
            _gameInteraction.SetActive(true);
            Time.timeScale = 1;
        }
    }

    public void Restart()
    {
        if(MainTower.Instance != null)
        {
            _restartButton.SetActive(false);
            _menuPanel.SetActive(false);
            _backgroundFade.SetActive(false);
            _towersHealthBar.SetActive(false);
            _soundButton.SetActive(false);
            _pauseButton.SetActive(false);
            MainTower.Instance.TowerDestroy();
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