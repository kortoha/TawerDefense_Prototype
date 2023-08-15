using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int score { get; private set; } = 0;
    public int bestScore { get; private set; } = 0;

    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _bestScoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        UpdateScoreText();
    }

    public void IncreaseScore(int scoreCount)
    {
        score += scoreCount;
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        _scoreText.text = "Score:\n" + score.ToString();
        _bestScoreText.text = "Best Score:\n" + bestScore.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }
}