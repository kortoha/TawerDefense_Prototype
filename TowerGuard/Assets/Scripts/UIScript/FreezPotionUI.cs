using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FreezPotionUI : BaseItemUI
{
    private const string NO_MONEY = "NoMoney";

    private float _freezingTime = 4f;

    public bool isFreez { get; private set; } = false;

    private void Update()
    {
        SpriteChangeOver(itemImage);
    }

    public IEnumerator FreezingTime()
    {
        if (MainTower.Instance != null)
        {
            if (EarningMoney.Instance.scoreOfCoins >= itemSO.cost && !isFreez)
            {
                EarningMoney.Instance.Buing(itemSO.cost);
                if (itemSound.enabled)
                {
                    itemSound.Play();
                }

                Time.timeScale = 0.5f;
                ArrowsDamage.Instance.FreezDamageUp();
                isFreez = true;

                yield return new WaitForSeconds(_freezingTime);

                Time.timeScale = 1;

                isFreez = false;
                ArrowsDamage.Instance.FreezDamageDown();
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

    public void StartOfFreez()
    {
        StartCoroutine(FreezingTime());
    }
}
