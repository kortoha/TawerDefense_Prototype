using UnityEngine.UI;
using UnityEngine;
using System;

public class TowerHealthBarUI : MonoBehaviour
{
    [SerializeField] private Image _barImage;
    [SerializeField] private MainTower _mainTower;

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float healthLavel = _mainTower.towersHealth / MainTower.Instance.maxHealth;
        _barImage.fillAmount = healthLavel;

        if (healthLavel <= 0.7f)
        {
            _barImage.color = Color.yellow;
        }

        if (healthLavel <= 0.5f)
        {
            _barImage.color = Color.red;
        }
    }
}
