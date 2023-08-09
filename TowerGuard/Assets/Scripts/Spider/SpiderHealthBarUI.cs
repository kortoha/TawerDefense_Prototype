using UnityEngine;
using UnityEngine.UI;

public class SpiderHealthBarUI : MonoBehaviour
{
    [SerializeField] private Image _barImage;
    [SerializeField] private SpiderDamage _spiderDamage;

    private void Update()
    {
        UpdateHealthBar(_spiderDamage);
    }

    private void UpdateHealthBar(SpiderDamage spiderDamage)
    {
        float healthLavel = spiderDamage.health / spiderDamage.maxSpiderHealt;

        _barImage.fillAmount = healthLavel;

        if (healthLavel <= 0)
        {
            Destroy(gameObject);
        }

    }
}
