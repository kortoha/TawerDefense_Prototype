using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class ArrowUltUI : MonoBehaviour
{
    private const string NO_MONEY = "NoMoney";

    [SerializeField] private Animator _coinsAnimator;
    [SerializeField] private Sprite _haveMoney;
    [SerializeField] private Sprite _haveNoMoney;
    [SerializeField] private Image _ultImage;
    [SerializeField] private GameObject _ult;
    [SerializeField] private GameObject _ultTarget;

    private bool _isUltOnScene = false;
    private float _timeToWait = 2f;
    private int _costOfUlt = 40;
    private Vector3 _targetPosition;

    private void Start()
    {
        _targetPosition = _ultTarget.transform.position;
    }

    private void Update()
    {
        SpriteChangeOver();
    }

    private IEnumerator ArrowUlt()
    {
        if (MainTower.Instance != null)
        {
            if (EarningMoney.Instance.scoreOfCoins >= _costOfUlt)
            {
                EarningMoney.Instance.Buing(_costOfUlt);
                _isUltOnScene = true;
                GameObject ult = Instantiate(_ult, _targetPosition, Quaternion.identity);
                yield return new WaitForSeconds(_timeToWait);
                Destroy(ult);
                _isUltOnScene = false;            
            }
            else
            {
                _coinsAnimator.SetTrigger(NO_MONEY);
            }
        }
    }

    public void UseUlt()
    {
        if (!_isUltOnScene)
        {
            StartCoroutine(ArrowUlt());
        }
    }

    private void SpriteChangeOver()
    {
        if (EarningMoney.Instance.scoreOfCoins < _costOfUlt)
        {
            _ultImage.sprite = _haveNoMoney;
        }
        else
        {
            _ultImage.sprite = _haveMoney;
        }
    }
}
