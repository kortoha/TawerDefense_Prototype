using UnityEngine;
using UnityEngine.UI;

public abstract class BaseItemUI : MonoBehaviour
{
    public ItemsSO itemSO;
    public Image itemImage;
    public Animator coinsAnimator;
    public AudioSource itemSound;

    [SerializeField] private Text _priceText;

    private void Awake()
    {
        ShowPrice();
    }

    public virtual void SpriteChangeOver(Image image)
    {
        if (EarningMoney.Instance.scoreOfCoins < itemSO.cost)
        {
            image.sprite = itemSO.noMoney;
        }
        else
        {
            image.sprite = itemSO.haveMoney;
        }
    }

    public void ShowPrice()
    {
        _priceText.text = itemSO.cost.ToString();
    }
}
