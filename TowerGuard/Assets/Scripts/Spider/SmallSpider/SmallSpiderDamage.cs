using UnityEngine;
using System.Collections;
using System;

public class SmallSpiderDamage : EnemyDamage
{
    [SerializeField] private float _smallSpiderDamage = 5;

    [NonSerialized] public bool isScoreUpOnce = false;

    private SpiderMovement _spiderMovement;
    private Collider _collider;

    private Coroutine _enemysAttackCoroutine;

    public bool isAttacking { get; private set; }
    public float health = 50;
    public float maxSpiderHealt = 50;

    private float _attackInterval = 0.3f;
    private float _timeToDestroySelf = 5f;
    private int _deathMoney = 1;
    private int _deathScoreCount = 1;


    private void Start()
    {
        _spiderMovement = GetComponent<SpiderMovement>();
        _collider = GetComponent<Collider>();
        _spiderMovement.ReachedTargetEvent += OnReachedTarget;
        _spiderMovement.RestingState += OnRestingState;
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
        _spiderMovement.ReachedTargetEvent -= OnReachedTarget;
    }

    private void OnReachedTarget()
    {
        StartCoroutine(SetDamage(_attackInterval, MainTower.Instance));
    }

    public new IEnumerator SetDamage(float attackInterval, MainTower mainTower)
    {
        _spiderMovement.LookAtTower();

        isAttacking = true;
        while (mainTower != null && health > 0)
        {
            mainTower.TakeDamage(_smallSpiderDamage);
            yield return new WaitForSeconds(attackInterval);
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

                if (!isScoreUpOnce)
                {
                    ScoreManager.Instance.IncreaseScore(_deathScoreCount);
                    isScoreUpOnce = true;

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
        _spiderMovement.ReachedTargetEvent -= OnReachedTarget;
        StopAttack();
        yield return new WaitForSeconds(_timeToDestroySelf);
        Destroy(gameObject);

    }

    private void OnDestroy()
    {
        _spiderMovement.ReachedTargetEvent -= OnReachedTarget;
        _spiderMovement.RestingState -= OnRestingState;
    }
}
