using UnityEngine;
using System.Collections;

public class SpiderDamage : EnemyDamage
{
    [SerializeField] private float _spiderDamage = 50;

    private SpiderMovement _spiderMovement;
    private Animator _animator;
    private Collider _collider;

    private Coroutine _enemysAttackCoroutine;

    public bool isAttacking { get; private set; }
    public float health = 300;
    public float maxSpiderHealt = 300;

    private float _attackInterval = 2f;
    private float _timeToDestroySelf = 5f;
    private bool _isScoreUpOnce = false;
    private int _deathMoney = 5;
    private int _deathScoreCount = 5;


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
        isAttacking = true;
        while (mainTower != null && health > 0)
        {
            mainTower.TakeDamage(_spiderDamage);
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
