using UnityEngine;
using UnityEngine.UI;

public class BaseHealthBar : MonoBehaviour
{
    public virtual void UpdateHealthBar(float currentHealth, float maxHealth, Image barImage)
    {
        float healthLavel = currentHealth / maxHealth;

        barImage.fillAmount = healthLavel;

        if (healthLavel <= 0)
        {
            Destroy(gameObject);
        }
    }
}