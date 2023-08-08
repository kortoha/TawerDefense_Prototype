using System.Collections;
using UnityEngine;

public class ArrowsDamage : MonoBehaviour
{
    private const string ENEMY_LAYER = "Enemy";

    [SerializeField] private float _damage = 1f;

    private float _attackInterval = 0.1f;
    private Coroutine _setDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(ENEMY_LAYER))
        {
            GoblinsDamage goblinDamage = other.gameObject.GetComponent<GoblinsDamage>();
            SpiderDamage spiderDamage = other.gameObject.GetComponent<SpiderDamage>();
            MinotaurusDamage minotaurusDamage = other.gameObject.GetComponent<MinotaurusDamage>();

            if (goblinDamage != null)
            {
                _setDamage = StartCoroutine(SetDamage(_attackInterval, goblinDamage));
            }
            else if (spiderDamage != null)
            {
                _setDamage = StartCoroutine(SetDamage(_attackInterval, spiderDamage));
            }
            else if (minotaurusDamage != null)
            {
                _setDamage = StartCoroutine(SetDamage(_attackInterval, minotaurusDamage));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(ENEMY_LAYER))
        {
            StopCoroutine(_setDamage);
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