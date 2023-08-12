using UnityEngine;
using UnityEngine.UI;

public class DragonHealthBarUI : BaseHealthBar
{
    [SerializeField] private Image _barImage;
    [SerializeField] private DragonDamage _dragonDamage;

    private void Update()
    {
        UpdateHealthBar(_dragonDamage.health, _dragonDamage.maxHealth, _barImage);
    }

    public override void UpdateHealthBar(float currentHealth, float maxHealth, Image image)
    {
        base.UpdateHealthBar(currentHealth, maxHealth, image);
    }
}
