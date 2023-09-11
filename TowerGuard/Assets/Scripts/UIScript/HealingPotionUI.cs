using UnityEngine;
using UnityEngine.UI;

public class HealingPotionUI : BaseItemUI
{
    private const string NO_MONEY = "NoMoney";
    private const string HEAL_ANIM = "IsHeal";


    private float _healingOnce = 300;

    private Animator _towerAnimator;

    private void Start()
    {
        _towerAnimator = MainTower.Instance.transform.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        SpriteChangeOver(itemImage);
    }

    public void HealingOfTower()
    {
        if (MainTower.Instance != null)
        {
            if (EarningMoney.Instance.scoreOfCoins >= itemSO.cost)
            {
                EarningMoney.Instance.Buing(itemSO.cost);

                MainTower.Instance.towersHealth += _healingOnce;

                _towerAnimator.SetTrigger(HEAL_ANIM);

                if (itemSound.enabled)
                {
                   itemSound.Play();
                }

                if (MainTower.Instance.towersHealth > MainTower.Instance.maxHealth)
                {
                    MainTower.Instance.towersHealth = MainTower.Instance.maxHealth;
                }
            }
            else
            {
                coinsAnimator.SetTrigger(NO_MONEY);
            }
        }
    }

    public override void SpriteChangeOver(Image image)
    {
        base.SpriteChangeOver(image);
    }
}
