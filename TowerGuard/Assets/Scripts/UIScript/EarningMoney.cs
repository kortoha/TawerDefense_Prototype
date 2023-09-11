using UnityEngine;
using UnityEngine.UI;

public class EarningMoney : MonoBehaviour
{
    [SerializeField] private Text _coinsText;

    public static EarningMoney Instance { get; private set; }

    public int scoreOfCoins { get; private set; } = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        ShowCoinsScore();
    }

    public int MakeMoney(int money)
    {
        scoreOfCoins += money;
        return scoreOfCoins;
    }

    public int Buing(int price)
    {
        scoreOfCoins -= price;
        return scoreOfCoins;
    }

    private void ShowCoinsScore()
    {
        _coinsText.text = scoreOfCoins.ToString();
    }
}
