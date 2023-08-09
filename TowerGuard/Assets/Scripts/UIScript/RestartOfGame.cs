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


    public int sceneCount;

    private float _timeToRestart = 0.5f;

    private void Update()
    {
        if(MainTower.Instance == null)
        {
            _cameraAnimator.SetTrigger(CAMERA_DEFEAT_MOD);
            _restartButton.SetActive(true);
            _pauseButton.SetActive(false);
            _coinsScore.SetActive(false);
            _interactionPanel.SetActive(false);
            _scorePanel.SetActive(true);
        }
    }

    public void Restart()
    {
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
}
