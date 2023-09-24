using UnityEngine;
using UnityEngine.UI;

public class CatapultHealthBarUI : BaseHealthBar
{
    [SerializeField] private Image _barImage;
    [SerializeField] private CatapultDamage _catapultDamage;

    private void Update()
    {
        UpdateHealthBar(_catapultDamage.health, _catapultDamage.maxHealth, _barImage);
    }

    public override void UpdateHealthBar(float currentHealth, float maxHealth, Image image)
    {
        base.UpdateHealthBar(currentHealth, maxHealth, image);
    }
}
