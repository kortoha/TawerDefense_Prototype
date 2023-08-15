using UnityEngine;
using UnityEngine.UI;

public class MinotaurusHealthBarUI : BaseHealthBar
{
    [SerializeField] private Image _barImage;
    [SerializeField] private MinotaurusDamage _minotaurusDamage;

    private void Update()
    {
        UpdateHealthBar(_minotaurusDamage.health, _minotaurusDamage.maxHealth, _barImage);
    }

    public override void UpdateHealthBar(float currentHealth, float maxHealth, Image image)
    {
        base.UpdateHealthBar(currentHealth, maxHealth, image);
    }
}
