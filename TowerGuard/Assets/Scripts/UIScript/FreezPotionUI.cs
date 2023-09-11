using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FreezPotionUI : BaseItemUI
{
    private const string NO_MONEY = "NoMoney";

    private float _freezingTime = 4f;
    

    private void Update()
    {
        SpriteChangeOver(itemImage);
    }

    public IEnumerator FreezingTime()
    {
        if (MainTower.Instance != null)
        {
            if (EarningMoney.Instance.scoreOfCoins >= itemSO.cost)
            {
                EarningMoney.Instance.Buing(itemSO.cost);
                if (itemSound.enabled)
                {
                    itemSound.Play();
                }
                Time.timeScale = 0.5f;

                yield return new WaitForSeconds(_freezingTime);

                Time.timeScale = 1;
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
