using UnityEngine;

public class DragonVisual : MonoBehaviour
{
    private const string MOVEING_NAME = "IsMove", ATTACK_NAME = "IsAttacking", DEAD_NAME = "Dead";

    private DragonMovement _dragonMovement;
    private DragonDamage _dragonDamage;
    private Animator _animator;

    private void Start()
    {
        _dragonMovement = transform.GetComponentInParent<DragonMovement>();
        _dragonDamage = transform.GetComponentInParent<DragonDamage>();
        _animator = GetComponent<Animator>();

        _dragonMovement.ReachedTargetEvent += OnReachedTarget;
        _dragonMovement.RestingState += OnRestingState;

    }

    private void OnRestingState()
    {
        _animator.SetBool(ATTACK_NAME, false);
        _animator.SetBool(MOVEING_NAME, false);
        _dragonMovement.ReachedTargetEvent -= OnReachedTarget;

    }

    private void OnReachedTarget()
    {
        _animator.SetBool(ATTACK_NAME, true);
    }

    private void Update()
    {
        if (_dragonMovement.isMoving)
        {
            _animator.SetBool(MOVEING_NAME, true);
        }
        else
        {
            _animator.SetBool(MOVEING_NAME, false);
        }

        if (_dragonDamage.health <= 0)
        {
            _animator.SetBool(ATTACK_NAME, false);
            _animator.SetTrigger(DEAD_NAME);
        }
    }

    private void OnDestroy()
    {
        _dragonMovement.ReachedTargetEvent -= OnReachedTarget;
        _dragonMovement.RestingState -= OnRestingState;
    }
}
