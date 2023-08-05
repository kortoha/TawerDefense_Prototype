using UnityEngine;

public class GoblinsVisual : MonoBehaviour
{
    private const string MOVEING_NAME = "IsMove", ATTACK_NAME = "IsAttacking", DEAD_NAME = "Dead";

    private GoblinsMovement _goblinsMovement;
    private GoblinsDamage _goblinsDamage;
    private Animator _animator;

    private void Start()
    {
        _goblinsMovement = transform.GetComponentInParent<GoblinsMovement>();
        _goblinsDamage = transform.GetComponentInParent<GoblinsDamage>();
        _animator = GetComponent<Animator>();

        _goblinsMovement.ReachedTargetEvent += OnReachedTarget;
        _goblinsMovement.RestingState += OnRestingState;

    }

    private void OnRestingState()
    {
        _animator.SetBool(ATTACK_NAME, false);
        _animator.SetBool(MOVEING_NAME, false);
        _goblinsMovement.ReachedTargetEvent -= OnReachedTarget;

    }

    private void OnReachedTarget()
    {
        _animator.SetBool(ATTACK_NAME, true);
    }

    private void Update()
    {
        if (_goblinsMovement.isMoving)
        {
            _animator.SetBool(MOVEING_NAME, true);
        }
        else
        {
            _animator.SetBool(MOVEING_NAME, false);
        }

        if (_goblinsDamage.goblinsHealth <= 0)
        {
            _animator.SetBool(ATTACK_NAME, false);
            _animator.SetTrigger(DEAD_NAME);
        }
    }

    private void OnDestroy()
    {
        _goblinsMovement.ReachedTargetEvent -= OnReachedTarget;
        _goblinsMovement.RestingState -= OnRestingState;
    }
}