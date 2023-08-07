using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class GoblinsDamage : MonoBehaviour
{
    [SerializeField] private float _goblinsDamage = 20;

    private GoblinsMovement _goblinsMovement;
    private Animator _animator;
    private Collider _collider;

    private Coroutine _enemysAttackCoroutine;

    public bool isAttacking { get; private set; }
    public float goblinsHealth = 100, maxGoblinsHealt = 100;

    private float _attackInterval = 1f;
    private float _timeToDestroySelf = 5f;
    private bool _isScoreUpOnce = false;
    private int _deathMoney = 1;


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

    public IEnumerator SetDamage(float attackInterval, MainTower mainTower)
    {
        isAttacking = true;
        while (mainTower != null && goblinsHealth > 0)
        {
            mainTower.TakeDamage(_goblinsDamage);
            yield return new WaitForSeconds(attackInterval);
        }
        isAttacking = false;
        _enemysAttackCoroutine = null;
    }

    public float TakeDamage(float damage)
    {
        if (MainTower.Instance != null)
        {
            goblinsHealth -= damage;

            if (goblinsHealth <= 0)
            {
                _collider.enabled = false;

                if (!_isScoreUpOnce)
                {
                    ScoreManager.Instance.IncreaseScore();
                    _isScoreUpOnce = true;

                    EarningMoney.Instance.MakeMoney(_deathMoney);
                }
                StartCoroutine(GoblinsDeath());
            }
        }
        return goblinsHealth;
    }

    private void StopAttack()
    {
        if (_enemysAttackCoroutine != null)
        {
            StopCoroutine(_enemysAttackCoroutine);
        }
    }

    private IEnumerator GoblinsDeath()
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