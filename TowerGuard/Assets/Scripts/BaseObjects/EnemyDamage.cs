using System.Collections;
using UnityEngine;

public class EnemyDamage : MonoBehaviour 
{
    public float baseHealth;

    public IEnumerator SetDamage(float attackInterval, MainTower mainTower)
    {
        yield return new WaitForSeconds(attackInterval);
    }
    public virtual float TakeDamage(float damage)
    {
        return baseHealth;
    }
    public virtual void StopAttack()
    {

    }
    public IEnumerator Death()
    {
        yield return null;
    }
}
