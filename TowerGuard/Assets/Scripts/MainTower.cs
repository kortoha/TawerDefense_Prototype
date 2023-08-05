using System;
using UnityEngine;

public class MainTower : MonoBehaviour
{
    [SerializeField] private GameObject _exploud;

    [NonSerialized] public bool _isTowerStay = true;

    public static MainTower Instance { get; private set; }

    public float towersHealth = 2000, maxHealth = 2000;

    private GameObject _exploudeInstance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public float TakeDamage(float damage)
    {
        towersHealth -= damage;
        if (towersHealth <= 0)
        {
            TowerDestroy();
        }
        return towersHealth;
    }

    public void TowerDestroy()
    {
        Destroy(gameObject);
        _isTowerStay = false;

        if (_exploudeInstance == null)
        {
            _exploudeInstance = Instantiate(_exploud);
        }
    }
}