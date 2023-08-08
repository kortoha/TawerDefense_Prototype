using UnityEngine;
using UnityEngine.UI;

public class SpiderHealthBarUI : MonoBehaviour
{
    [SerializeField] private Image _barImage;
    [SerializeField] private SpiderDamage _spiderDamage;

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float healthLavel = _spiderDamage.health / _spiderDamage.maxSpiderHealt;

        _barImage.fillAmount = healthLavel;

        if (healthLavel <= 0)
        {
            Destroy(gameObject);
        }

    }
}
