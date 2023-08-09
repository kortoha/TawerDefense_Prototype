using UnityEngine;

public class SmallSpiderVisual : MonoBehaviour
{

    private const string MOVEING_NAME = "IsMove", ATTACK_NAME = "IsAttacking", DEAD_NAME = "Dead";

    private SpiderMovement _spiderMovement;
    private SmallSpiderDamage _smallSpiderDamage;
    private Animator _animator;

    private void Start()
    {
        _spiderMovement = transform.GetComponentInParent<SpiderMovement>();
        _smallSpiderDamage = transform.GetComponentInParent<SmallSpiderDamage>();
        _animator = GetComponent<Animator>();

        _spiderMovement.ReachedTargetEvent += OnReachedTarget;
        _spiderMovement.RestingState += OnRestingState;

    }

    private void OnRestingState()
    {
        _animator.SetBool(ATTACK_NAME, false);
        _animator.SetBool(MOVEING_NAME, false);
        _spiderMovement.ReachedTargetEvent -= OnReachedTarget;

    }

    private void OnReachedTarget()
    {
        _animator.SetBool(ATTACK_NAME, true);
    }

    private void Update()
    {
        if (_spiderMovement.isMoving)
        {
            _animator.SetBool(MOVEING_NAME, true);
        }
        else
        {
            _animator.SetBool(MOVEING_NAME, false);
        }

        if (_smallSpiderDamage.health <= 0)
        {
            _animator.SetBool(ATTACK_NAME, false);
            _animator.SetTrigger(DEAD_NAME);
        }
    }

    private void OnDestroy()
    {
        _spiderMovement.ReachedTargetEvent -= OnReachedTarget;
        _spiderMovement.RestingState -= OnRestingState;
    }
}
