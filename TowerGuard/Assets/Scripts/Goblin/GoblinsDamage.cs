using System.Collections;
using UnityEngine;

public class GoblinsDamage : EnemyDamage
{
    [SerializeField] private float _goblinsDamage = 20;

    private GoblinsMovement _goblinsMovement;
    private Animator _animator;
    private Collider _collider;

    private Coroutine _enemysAttackCoroutine;

    public bool isAttacking { get; private set; }
    public float health = 100;
    public float maxHealth = 100;

    private float _attackInterval = 1f;
    private float _timeToDestroySelf = 5f;
    private bool _isScoreUpOnce = false;
    private int _deathMoney = 1;
    private int _deathScoreCount = 1;


    private void Start()
    {
        _goblinsMovement = GetComponent<GoblinsMovement>();
        _collider = GetComponent<Collider>();
        _goblinsMovement.ReachedTargetEvent += OnReachedTarget;
        _goblinsMovement.RestingState += OnRestingState;
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
        _goblinsMovement.ReachedTargetEvent -= OnReachedTarget;
    }

    private void OnReachedTarget()
    {
        StartCoroutine(SetDamage(_attackInterval, MainTower.Instance));
    }

    public new IEnumerator SetDamage(float attackInterval, MainTower mainTower)
    {
        _goblinsMovement.LookAtTower();

        isAttacking = true;
        while (mainTower != null && health > 0)
        {
            mainTower.TakeDamage(_goblinsDamage);
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
        _goblinsMovement.ReachedTargetEvent -= OnReachedTarget;
        StopAttack();
        yield return new WaitForSeconds(_timeToDestroySelf);
        Destroy(gameObject);

    }

    private void OnDestroy()
    {
        _goblinsMovement.ReachedTargetEvent -= OnReachedTarget;
        _goblinsMovement.RestingState -= OnRestingState;
    }
}