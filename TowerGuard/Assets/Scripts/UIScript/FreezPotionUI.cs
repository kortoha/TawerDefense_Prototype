using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FreezPotionUI : MonoBehaviour
{
    private const string NO_MONEY = "NoMoney";

    [SerializeField] private Animator _coinsAnimator;
    [SerializeField] private Sprite _haveMoney;
    [SerializeField] private Sprite _haveNoMoney;
    [SerializeField] private Image _freezImage;
    [SerializeField] private AudioSource _freezSound;

    private float _freezingTime = 4f;
    private int _costOfFreez = 5;
    

    private void Update()
    {
        SpriteChangeOver();
    }

    public IEnumerator FreezingTime()
    {
        if (MainTower.Instance != null)
        {
            if (EarningMoney.Instance.scoreOfCoins >= _costOfFreez)
            {
                EarningMoney.Instance.Buing(_costOfFreez);
                if (_freezSound.enabled)
                {
                    _freezSound.Play();
                }
                Time.timeScale = 0.5f;

                yield return new WaitForSeconds(_freezingTime);

                Time.timeScale = 1;
            }
            else
            {
                _coinsAnimator.SetTrigger(NO_MONEY);
            }
        }
    }

    private void SpriteChangeOver()
    {
        if (EarningMoney.Instance.scoreOfCoins < _costOfFreez)
        {
            _freezImage.sprite = _haveNoMoney;
        }
        else
        {
            _freezImage.sprite = _haveMoney;
        }
    }

    public void StartOfFreez()
    {
        StartCoroutine(FreezingTime());
    }
}
