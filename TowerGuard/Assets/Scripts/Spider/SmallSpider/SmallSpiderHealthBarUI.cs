using UnityEngine;
using UnityEngine.UI;

public class SmallSpiderHealthBarUI : BaseHealthBar
{
    [SerializeField] private Image _barImage;
    [SerializeField] private SmallSpiderDamage _smallSpiderDamage;

    private void Update()
    {
        UpdateHealthBar(_smallSpiderDamage.health, _smallSpiderDamage.maxHealth, _barImage);
    }

    public override void UpdateHealthBar(float currentHealth, float maxHealth, Image image)
    {
        base.UpdateHealthBar(currentHealth, maxHealth, image);
    }
}
