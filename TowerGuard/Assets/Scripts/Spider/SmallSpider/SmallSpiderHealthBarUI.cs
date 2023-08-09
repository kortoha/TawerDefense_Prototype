using UnityEngine;
using UnityEngine.UI;

public class SmallSpiderHealthBarUI : MonoBehaviour
{
    [SerializeField] private Image _barImage;
    [SerializeField] private SmallSpiderDamage _smallSpiderDamage;

    private void Update()
    {
        UpdateHealthBar(_smallSpiderDamage);
    }

    private void UpdateHealthBar(SmallSpiderDamage smallSpiderDamage)
    {
        float healthLavel = smallSpiderDamage.health / smallSpiderDamage.maxSpiderHealt;

        _barImage.fillAmount = healthLavel;

        if (healthLavel <= 0)
        {
            Destroy(gameObject);
        }
    }
}
