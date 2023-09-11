using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartOfGame : MonoBehaviour
{
    private const string NAME_OF_FAIDING_TRIGER = "Brightening";
    private const string CAMERA_DEFEAT_MOD = "DefeatMod";

    [SerializeField] private Animator _cameraAnimator;
    [SerializeField] private Animator _brighteningAnimator;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _brightening;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _soundButton;
    [SerializeField] private GameObject _scorePanel;
    [SerializeField] private GameObject _interactionPanel;
    [SerializeField] private GameObject _coinsScore;
    [SerializeField] private AudioSource _selectSound;
    [SerializeField] private AudioSource _towerDestroySound;

    public int sceneCount;

    private float _timeToRestart = 0.6f;
    private bool _isSoundPlay = false;

    private void Update()
    {
        if(MainTower.Instance == null)
        {
            TowerDestroySound();
            _cameraAnimator.SetTrigger(CAMERA_DEFEAT_MOD);
            _restartButton.SetActive(true);
            _pauseButton.SetActive(false);
            _coinsScore.SetActive(false);
            _interactionPanel.SetActive(false);
            if(_scorePanel != null)
            {
                _scorePanel.SetActive(true);
            }
        }
    }

    public void Restart()
    {
        if (_selectSound.enabled)
        {
            _selectSound.Play();
        }
        _soundButton.SetActive(false);
        _interactionPanel.SetActive(false);
        _brightening.SetActive(true);
        _scorePanel.SetActive(false);
        _brighteningAnimator.SetTrigger(NAME_OF_FAIDING_TRIGER);

        Invoke("LoadScene", _timeToRestart);
        _restartButton.SetActive(false);

        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneCount);
    }

    private void TowerDestroySound()
    {
        if (!_isSoundPlay && _towerDestroySound.enabled)
        {
            _towerDestroySound.Play();
            _isSoundPlay = true;
        }
    }
}
