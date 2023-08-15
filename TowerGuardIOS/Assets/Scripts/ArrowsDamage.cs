using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsDamage : MonoBehaviour
{
    private const string ENEMY_LAYER = "Enemy";

    [SerializeField] private float _damage = 5f;

    private float _attackInterval = 0.1f;
    private Dictionary<EnemyDamage, Coroutine> _activeDamageCoroutines = new Dictionary<EnemyDamage, Coroutine>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(ENEMY_LAYER))
        {
            EnemyDamage enemyDamage = other.gameObject.GetComponent<EnemyDamage>();

            if (enemyDamage != null)
            {
                StartDamageCoroutine(enemyDamage);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(ENEMY_LAYER))
        {
            EnemyDamage enemyDamage = other.gameObject.GetComponent<EnemyDamage>();

            if (enemyDamage != null)
            {
                StopActiveDamageCoroutine(enemyDamage);
            }
        }
    }

    private void StartDamageCoroutine(EnemyDamage enemy)
    {
        if (_activeDamageCoroutines.ContainsKey(enemy))
        {
            StopCoroutine(_activeDamageCoroutines[enemy]);
        }

        _activeDamageCoroutines[enemy] = StartCoroutine(SetDamage(_attackInterval, enemy));
    }

    private void StopActiveDamageCoroutine(EnemyDamage enemy)
    {
        if (_activeDamageCoroutines.ContainsKey(enemy))
        {
            StopCoroutine(_activeDamageCoroutines[enemy]);
            _activeDamageCoroutines.Remove(enemy);
        }
    }

    public IEnumerator SetDamage(float attackInterval, EnemyDamage enemy)
    {
        while (MainTower.Instance != null)
        {
            if (enemy != null)
            {
                enemy.TakeDamage(_damage);
            }
            yield return new WaitForSeconds(attackInterval);
        }
    }
}