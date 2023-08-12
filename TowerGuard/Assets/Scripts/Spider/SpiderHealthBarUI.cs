using UnityEngine;
using UnityEngine.UI;

public class SpiderHealthBarUI : BaseHealthBar
{
    [SerializeField] private Image _barImage;
    [SerializeField] private SpiderDamage _spiderDamage;

    private void Update()
    {
        UpdateHealthBar(_spiderDamage.health, _spiderDamage.maxHealth, _barImage);
    }

    public override void UpdateHealthBar(float currentHealth, float maxHealth, Image image)
    {
        base.UpdateHealthBar(currentHealth, maxHealth, image);
    }
}
