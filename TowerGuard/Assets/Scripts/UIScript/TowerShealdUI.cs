using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TowerShealdUI : MonoBehaviour
{
    private const string NO_MONEY = "NoMoney";

    [SerializeField] private Animator _coinsAnimator;
    [SerializeField] private Sprite _haveMoney;
    [SerializeField] private Sprite _haveNoMoney;
    [SerializeField] private Image _shealdImage;
    [SerializeField] private GameObject _sheald;
    [SerializeField] private AudioSource _shealdAudio;


    private Vector3 _target;
    private float _timeOfProtect = 5f;
    private int _costOfSheald = 10;

    private void Start()
    {
        _target = MainTower.Instance.transform.position;
    }

    private void Update()
    {
        SpriteChangeOver();
    }

    private IEnumerator TowerSheald()
    {
        if (MainTower.Instance != null)
        {
            if (EarningMoney.Instance.scoreOfCoins >= _costOfSheald)
            {
                EarningMoney.Instance.Buing(_costOfSheald);

                GameObject sheald = Instantiate(_sheald, _target, Quaternion.identity);
                MainTower.Instance.isTowerHasSheald = true;

                yield return new WaitForSeconds(_timeOfProtect);

                Destroy(sheald);
                MainTower.Instance.isTowerHasSheald = false;
            }
            else
            {
                _coinsAnimator.SetTrigger(NO_MONEY);
            }
        }
    }

    public void TowerProtect()
    {
        if(!MainTower.Instance.isTowerHasSheald)
        {
            StartCoroutine(TowerSheald());

            if (_shealdAudio.enabled)
            {
                _shealdAudio.Play();
            }
        }
    }

    private void SpriteChangeOver()
    {
        if (EarningMoney.Instance.scoreOfCoins < _costOfSheald)
        {
            _shealdImage.sprite = _haveNoMoney;
        }
        else
        {
            _shealdImage.sprite = _haveMoney;
        }
    }
}
