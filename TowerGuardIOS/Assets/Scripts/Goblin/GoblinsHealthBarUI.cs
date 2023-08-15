using UnityEngine;
using UnityEngine.UI;

public class GoblinsHealthBarUI : BaseHealthBar
{
    [SerializeField] private Image _barImage;
    [SerializeField] private GoblinsDamage _goblinsDamage; 

    private void Update()
    {
        UpdateHealthBar(_goblinsDamage.health, _goblinsDamage.maxHealth, _barImage);
    }

    public override void UpdateHealthBar(float currentHealth, float maxHealth, Image image)
    {
        base.UpdateHealthBar(currentHealth, maxHealth, image);
    }
}