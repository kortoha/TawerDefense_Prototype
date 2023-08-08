using UnityEngine;
using UnityEngine.UI;

public class MinotaurusHealthBarUI : MonoBehaviour
{
    [SerializeField] private Image _barImage;
    [SerializeField] private MinotaurusDamage _minotaurusDamage;

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float healthLavel = _minotaurusDamage.health / _minotaurusDamage.maxMinotaurusHealt;

        _barImage.fillAmount = healthLavel;

        if (healthLavel <= 0)
        {
            Destroy(gameObject);
        }

    }
}
