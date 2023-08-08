using UnityEngine;
using System.Collections;

public class MinotaurusDamage : EnemyDamage
{
    [SerializeField] private float _minotaurusDamage = 50;
    [SerializeField] private GameObject _revardCoin;

    private MinotaurusMovement _minotaurusMovement;
    private Animator _animator;
    private Collider _collider;

    private Coroutine _enemysAttackCoroutine;

    public bool isAttacking { get; private set; }
    public float health = 500;
    public float maxMinotaurusHealt = 500;

    private float _attackInterval = 2f;
    private float _timeToDestroySelf = 5f;
    private bool _isScoreUpOnce = false;
    private int _deathMoney = 30;
    private int _deathScoreCount = 30;


    private void Start()
    {
        _minotaurusMovement = GetComponent<MinotaurusMovement>();
        _collider = GetComponent<Collider>();
        _minotaurusMovement.ReachedTargetEvent += OnReachedTarget;
        _minotaurusMovement.RestingState += OnRestingState;
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
        _minotaurusMovement.ReachedTargetEvent -= OnReachedTarget;
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
            mainTower.TakeDamage(_minotaurusDamage);
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
                    Revard();

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
        _minotaurusMovement.ReachedTargetEvent -= OnReachedTarget;
        StopAttack();
        yield return new WaitForSeconds(_timeToDestroySelf);
        Destroy(gameObject);

    }

    private void OnDestroy()
    {
        _minotaurusMovement.ReachedTargetEvent -= OnReachedTarget;
        _minotaurusMovement.RestingState -= OnRestingState;
    }

    private void Revard()
    {
        Instantiate(_revardCoin, transform.position, Quaternion.identity);
    }
}
