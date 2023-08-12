using System.Collections;
using UnityEngine;

public class EnemyDamage : MonoBehaviour 
{
    private float _health; 

    public IEnumerator SetDamage(float attackInterval, MainTower mainTower)
    {
        yield return new WaitForSeconds(attackInterval);
    }
    public virtual float TakeDamage(float damage)
    {
        return _health;
    }
    public virtual void StopAttack()
    {

    }
    public IEnumerator Death()
    {
        yield return null;
    }
}
