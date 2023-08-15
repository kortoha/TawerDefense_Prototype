using UnityEngine;
using UnityEngine.UI;

public class HealingPotionUI : MonoBehaviour
{
    private const string NO_MONEY = "NoMoney";
    private const string HEAL_ANIM = "IsHeal";

    [SerializeField] private Animator _coinsAnimator;
    [SerializeField] private Sprite _haveMoney;
    [SerializeField] private Sprite _haveNoMoney;
    [SerializeField] private Image _healImage;
    [SerializeField] private AudioSource _healSound;

    private float _healingOnce = 300;
    private int _costOfHeal = 20;

    private Animator _towerAnimator;

    private void Start()
    {
        _towerAnimator = MainTower.Instance.transform.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        SpriteChangeOver();
    }

    public void HealingOfTower()
    {
        if (MainTower.Instance != null)
        {
            if (EarningMoney.Instance.scoreOfCoins >= _costOfHeal)
            {
                EarningMoney.Instance.Buing(_costOfHeal);

                MainTower.Instance.towersHealth += _healingOnce;

                _towerAnimator.SetTrigger(HEAL_ANIM);
                _healSound.Play();

                if (MainTower.Instance.towersHealth > MainTower.Instance.maxHealth)
                {
                    MainTower.Instance.towersHealth = MainTower.Instance.maxHealth;
                }
            }
            else
            {
                _coinsAnimator.SetTrigger(NO_MONEY);
            }
        }
    }

    private void SpriteChangeOver()
    {
        if(EarningMoney.Instance.scoreOfCoins < _costOfHeal)
        {
            _healImage.sprite = _haveNoMoney;
        }
        else
        {
            _healImage.sprite = _haveMoney;
        }
    }
}
