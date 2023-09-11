using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TowerShealdUI : BaseItemUI
{
    private const string NO_MONEY = "NoMoney";

    [SerializeField] private GameObject _sheald;

    private Vector3 _target;
    private float _timeOfProtect = 5f;

    private void Start()
    {
        _target = MainTower.Instance.transform.position;
    }

    private void Update()
    {
        SpriteChangeOver(itemImage);
    }

    private IEnumerator TowerSheald()
    {
        if (MainTower.Instance != null)
        {
            if (EarningMoney.Instance.scoreOfCoins >= itemSO.cost)
            {
                EarningMoney.Instance.Buing(itemSO.cost);

                GameObject sheald = Instantiate(_sheald, _target, Quaternion.identity);
                MainTower.Instance.isTowerHasSheald = true;

                yield return new WaitForSeconds(_timeOfProtect);

                Destroy(sheald);
                MainTower.Instance.isTowerHasSheald = false;
            }
            else
            {
                coinsAnimator.SetTrigger(NO_MONEY);
            }
        }
    }

    public void TowerProtect()
    {
        if(!MainTower.Instance.isTowerHasSheald)
        {
            StartCoroutine(TowerSheald());

            if (itemSound.enabled)
            {
                itemSound.Play();
            }
        }
    }

    public override void SpriteChangeOver(Image image)
    {
        base.SpriteChangeOver(image);
    }
}
