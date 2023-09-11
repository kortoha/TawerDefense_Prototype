using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class ArrowUltUI : BaseItemUI
{
    private const string NO_MONEY = "NoMoney";

    [SerializeField] private GameObject _ult;
    [SerializeField] private GameObject _ultTarget;

    private bool _isUltOnScene = false;
    private float _timeToWait = 2f;
    private Vector3 _targetPosition;

    private void Start()
    {
        _targetPosition = _ultTarget.transform.position;
    }

    private void Update()
    {
        SpriteChangeOver(itemImage);
    }

    private IEnumerator ArrowUlt()
    {
        if (MainTower.Instance != null)
        {
            if (EarningMoney.Instance.scoreOfCoins >= itemSO.cost)
            {
                EarningMoney.Instance.Buing(itemSO.cost);
                _isUltOnScene = true;
                GameObject ult = Instantiate(_ult, _targetPosition, Quaternion.identity);
                if (itemSound.enabled)
                {
                    itemSound.Play();
                }
                yield return new WaitForSeconds(_timeToWait);
                Destroy(ult);
                _isUltOnScene = false;            
            }
            else
            {
                coinsAnimator.SetTrigger(NO_MONEY);
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

    public override void SpriteChangeOver(Image image)
    {
        base.SpriteChangeOver(image);
    }
}
