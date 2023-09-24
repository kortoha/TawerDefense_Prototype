using UnityEngine;

public class CatapultVisual : MonoBehaviour
{
    private const string MOVEING_NAME = "IsMove", ATTACK_NAME = "IsAttacking", DEAD_NAME = "Dead";

    private CatapultMovement _catapultMovement;
    private CatapultDamage _catapultDamage;
    private Animator _animator;

    private void Start()
    {
        _catapultMovement = transform.GetComponentInParent<CatapultMovement>();
        _catapultDamage = transform.GetComponentInParent<CatapultDamage>();
        _animator = GetComponent<Animator>();

        _catapultMovement.ReachedTargetEvent += OnReachedTarget;
        _catapultMovement.RestingState += OnRestingState;
    }

    private void OnRestingState()
    {
        _animator.SetBool(ATTACK_NAME, false);
        _animator.SetBool(MOVEING_NAME, false);
        _catapultMovement.ReachedTargetEvent -= OnReachedTarget;

    }

    private void OnReachedTarget()
    {
        _animator.SetBool(ATTACK_NAME, true);
    }

    private void Update()
    {
        if (_catapultMovement.isMoving)
        {
            _animator.SetBool(MOVEING_NAME, true);
        }
        else
        {
            _animator.SetBool(MOVEING_NAME, false);
        }

        if (_catapultDamage.health <= 0)
        {
            _animator.SetBool(ATTACK_NAME, false);
            _animator.SetTrigger(DEAD_NAME);
        }
    }

    private void OnDestroy()
    {
        _catapultMovement.ReachedTargetEvent -= OnReachedTarget;
        _catapultMovement.RestingState -= OnRestingState;
    }
}
