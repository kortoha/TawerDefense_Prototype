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

    public int sceneCount;

    private float _timeToRestart = 0.5f;

    private void Update()
    {
        if(MainTower.Instance == null)
        {
            _cameraAnimator.SetTrigger(CAMERA_DEFEAT_MOD);
            _restartButton.SetActive(true);
            _pauseButton.SetActive(false);
        }
    }

    public virtual void Restart()
    {
        _soundButton.SetActive(false);
        _brightening.SetActive(true);
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
