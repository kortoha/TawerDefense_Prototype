using System.Collections;
using UnityEngine;

public class DragonDamage : EnemyDamage
{
    [SerializeField] private float _dragonDamage = 200;
    [SerializeField] private GameObject _revardCoin;
    [SerializeField] private GameObject _fireFlow;

    private DragonMovement _dragonMovement;
    private Collider _collider;

    private Coroutine _enemysAttackCoroutine;

    public bool isAttacking { get; private set; }
    public float health = 2000;
    public float maxHealth = 2000;

    private float _attackInterval = 2f;
    private float _timeToDestroySelf = 5f;
    private bool _isScoreUpOnce = false;
    private int _deathMoney = 100;
    private int _deathScoreCount = 100;


    private void Start()
    {
        _dragonMovement = GetComponent<DragonMovement>();
        _collider = GetComponent<Collider>();
        _dragonMovement.ReachedTargetEvent += OnReachedTarget;
        _dragonMovement.RestingState += OnRestingState;
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
        _fireFlow.SetActive(false);
        _dragonMovement.ReachedTargetEvent -= OnReachedTarget;
    }

    private void OnReachedTarget()
    {
        StartCoroutine(SetDamage(_attackInterval, MainTower.Instance));
        _fireFlow.SetActive(true);
    }

    public new IEnumerator SetDamage(float attackInterval, MainTower mainTower)
    {
        _dragonMovement.LookAtTower();

        isAttacking = true;
        while (mainTower != null && health > 0)
        {
            mainTower.TakeDamage(_dragonDamage);
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
        _fireFlow.SetActive(false);
        _dragonMovement.ReachedTargetEvent -= OnReachedTarget;
        StopAttack();
        yield return new WaitForSeconds(_timeToDestroySelf);
        Destroy(gameObject);

    }

    private void OnDestroy()
    {
        _dragonMovement.ReachedTargetEvent -= OnReachedTarget;
        _dragonMovement.RestingState -= OnRestingState;
    }

    private void Revard()
    {
        Instantiate(_revardCoin, transform.position, Quaternion.identity);
    }
}
