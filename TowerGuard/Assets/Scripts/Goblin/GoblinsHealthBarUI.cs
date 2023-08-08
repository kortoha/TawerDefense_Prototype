using UnityEngine;
using UnityEngine.UI;

public class GoblinsHealthBarUI : MonoBehaviour
{
    [SerializeField] private Image _barImage;
    [SerializeField] private GoblinsDamage _goblinsDamage;

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float healthLavel = _goblinsDamage.health / _goblinsDamage.maxGoblinsHealt;

        _barImage.fillAmount = healthLavel;

        if (healthLavel <= 0)
        {
            Destroy(gameObject);
        }

    }
}
