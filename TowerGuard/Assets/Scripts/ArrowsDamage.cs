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
            _setDamage = StartCoroutine(SetDamage(_attackInterval, goblinDamage));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(ENEMY_LAYER))
        {
            StopCoroutine(_setDamage);
        }
    }

    public IEnumerator SetDamage(float attackInterval, GoblinsDamage goblin)
    {
        while (MainTower.Instance != null)
        {
            if (goblin != null)
            {
                goblin.TakeDamage(_damage);
            }
            yield return new WaitForSeconds(attackInterval);
        }
    }
}