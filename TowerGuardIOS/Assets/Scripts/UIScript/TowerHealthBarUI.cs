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
        float healthLevel = _mainTower.towersHealth / MainTower.Instance.maxHealth;

        if (healthLevel <= 0.5f)
        {
            _barImage.color = Color.red;
        }
        else if (healthLevel <= 0.7f)
        {
            _barImage.color = Color.yellow;
        }
        else
        {
            _barImage.color = Color.green;
        }

        _barImage.fillAmount = healthLevel;
    }
}
