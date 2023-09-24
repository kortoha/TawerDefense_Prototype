using UnityEngine;
using System.Collections;

public class CatapultDamage : EnemyDamage
{
    [SerializeField] private float _catapultDamage = 50;

    private CatapultMovement _catapultMovement;
    private Collider _collider;

    private Coroutine _enemysAttackCoroutine;

    public bool isAttacking { get; private set; }
    public float health = 300;
    public float maxHealth = 300;

    private float _attackInterval = 7f;
    private float _timeToDestroySelf = 5f;
    private bool _isScoreUpOnce = false;
    private int _deathMoney = 20;
    private int _deathScoreCount = 20;


    private void Start()
    {
        _catapultMovement = GetComponent<CatapultMovement>();
        _collider = GetComponent<Collider>();
        _catapultMovement.ReachedTargetEvent += OnReachedTarget;
        _catapultMovement.RestingState += OnRestingState;
    }

    private void Update()
    {
        if (MainTower.Instance == null)
        {
            StopAttack();
        }
    }

    private void OnRestingState()
    {
        StopAttack();
        _catapultMovement.ReachedTargetEvent -= OnReachedTarget;
    }

    private void OnReachedTarget()
    {
        StartCoroutine(SetDamage(_attackInterval, MainTower.Instance));
    }

    public new IEnumerator SetDamage(float attackInterval, MainTower mainTower)
    {
        _catapultMovement.LookAtTower();

        isAttacking = true;
        while (mainTower != null && health > 0)
        {
            yield return new WaitForSeconds(attackInterval);
            mainTower.TakeDamage(_catapultDamage);
        }
        isAttacking = false;
        _enemysAttackCoroutine = null;
    }

    public override float TakeDamage(float damage)
    {
        if (MainTower.Instance != null)
        {
            health -= damage;

            if (health <= 0)
            {
                _collider.enabled = false;

                if (!_isScoreUpOnce)
                {
                    ScoreManager.Instance.IncreaseScore(_deathScoreCount);
                    _isScoreUpOnce = true;

                    EarningMoney.Instance.MakeMoney(_deathMoney);
                }
                StartCoroutine(Death());
            }
        }
        return health;
    }

    public override void StopAttack()
    {
        if (_enemysAttackCoroutine != null)
        {
            StopCoroutine(_enemysAttackCoroutine);
        }
    }

    public new IEnumerator Death()
    {
        _catapultMovement.ReachedTargetEvent -= OnReachedTarget;
        StopAttack();
        yield return new WaitForSeconds(_timeToDestroySelf);
        Destroy(gameObject);

    }

    private void OnDestroy()
    {
        _catapultMovement.ReachedTargetEvent -= OnReachedTarget;
        _catapultMovement.RestingState -= OnRestingState;
    }
}
