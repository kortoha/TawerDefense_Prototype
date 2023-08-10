using UnityEngine;

public class UltDamage : MonoBehaviour
{
    private const string ENEMY_LAYER = "Enemy";

    private float _ultimateDamage = 150f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(ENEMY_LAYER))
        {
            GoblinsDamage goblinDamage = other.gameObject.GetComponent<GoblinsDamage>();
            SpiderDamage spiderDamage = other.gameObject.GetComponent<SpiderDamage>();
            SmallSpiderDamage smallSpiderDamage = other.gameObject.GetComponent<SmallSpiderDamage>();
            MinotaurusDamage minotaurusDamage = other.gameObject.GetComponent<MinotaurusDamage>();

            if (goblinDamage != null)
            {
                goblinDamage.TakeDamage(_ultimateDamage);
            }
            else if (spiderDamage != null)
            {
                spiderDamage.TakeDamage(_ultimateDamage);
            }
            else if (smallSpiderDamage != null)
            {
                smallSpiderDamage.TakeDamage(_ultimateDamage);
            }
            else if (minotaurusDamage != null)
            {
                minotaurusDamage.TakeDamage(_ultimateDamage);
            }
        }
    }
}