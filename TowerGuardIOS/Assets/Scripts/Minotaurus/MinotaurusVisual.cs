using UnityEngine;

public class MinotaurusVisual : MonoBehaviour
{
    private const string MOVEING_NAME = "IsMove", ATTACK_NAME = "IsAttacking", DEAD_NAME = "Dead";

    private MinotaurusMovement _minotaurusMovement;
    private MinotaurusDamage _minotaurusDamage;
    private Animator _animator;

    private void Start()
    {
        _minotaurusMovement = transform.GetComponentInParent<MinotaurusMovement>();
        _minotaurusDamage = transform.GetComponentInParent<MinotaurusDamage>();
        _animator = GetComponent<Animator>();

        _minotaurusMovement.ReachedTargetEvent += OnReachedTarget;
        _minotaurusMovement.RestingState += OnRestingState;

    }

    private void OnRestingState()
    {
        _animator.SetBool(ATTACK_NAME, false);
        _animator.SetBool(MOVEING_NAME, false);
        _minotaurusMovement.ReachedTargetEvent -= OnReachedTarget;

    }

    private void OnReachedTarget()
    {
        _animator.SetBool(ATTACK_NAME, true);
    }

    private void Update()
    {
        if (_minotaurusMovement.isMoving)
        {
            _animator.SetBool(MOVEING_NAME, true);
        }
        else
        {
            _animator.SetBool(MOVEING_NAME, false);
        }

        if (_minotaurusDamage.health <= 0)
        {
            _animator.SetBool(ATTACK_NAME, false);
            _animator.SetTrigger(DEAD_NAME);
        }
    }

    private void OnDestroy()
    {
        _minotaurusMovement.ReachedTargetEvent -= OnReachedTarget;
        _minotaurusMovement.RestingState -= OnRestingState;
    }
}
