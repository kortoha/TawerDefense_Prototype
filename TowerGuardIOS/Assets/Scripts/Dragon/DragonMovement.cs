using System;
using UnityEngine;
using UnityEngine.AI;

public class DragonMovement : EnemyMovement
{
    [SerializeField] private float _arrivalDistance = 0.1f;
    [NonSerialized] public bool isMoving = false;

    private DragonDamage _dragonDamage;
    private NavMeshAgent _dragonNavMeshAgent;
    private bool _hasSetInitialDestination = false;
    private Vector3 _initialDestination;
    private Vector3 _currentPosition;
    private Vector3 _targetPosition;
    private bool _hasReachedTarget = false;

    public event Action ReachedTargetEvent;
    public event Action RestingState;
    public event Action<Vector3> BlockedWay;

    private void Awake()
    {
        _dragonNavMeshAgent = GetComponent<NavMeshAgent>();
        _dragonDamage = GetComponent<DragonDamage>();
    }

    private void Update()
    {
        _currentPosition = transform.position;

        if (!_hasReachedTarget)
        {
            MoveToTarget();
        }

        if (_dragonDamage.health <= 0)
        {
            _dragonNavMeshAgent.destination = _currentPosition;
        }


        if (MainTower.Instance == null)
        {
            _dragonNavMeshAgent.destination = _currentPosition;
            RestingState();
        }
        else
        {
            if (Vector3.Distance(_targetPosition, _currentPosition) <= _arrivalDistance)
            {
                if (!_hasReachedTarget)
                {
                    ReachedTargetEvent();
                    _hasReachedTarget = true;
                }
            }
        }

        if (!_hasReachedTarget && _dragonNavMeshAgent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            RaycastHit hit;
            if (Physics.Raycast(_currentPosition, _dragonNavMeshAgent.steeringTarget - _currentPosition, out hit, _dragonNavMeshAgent.radius, NavMesh.AllAreas))
            {
                var obstacleDragon = hit.collider.GetComponent<MinotaurusMovement>();
                if (obstacleDragon != null)
                {
                    BlockedWay?.Invoke(hit.point);
                }
            }
        }
    }

    private void OnEnable()
    {
        _dragonNavMeshAgent.updatePosition = true;
        _dragonNavMeshAgent.updateRotation = true;
    }

    private void OnDisable()
    {
        _dragonNavMeshAgent.updatePosition = false;
        _dragonNavMeshAgent.updateRotation = false;
    }

    public override void MoveToTarget()
    {
        if (_targetPosition != null && !_hasSetInitialDestination)
        {
            _dragonNavMeshAgent.SetDestination(_targetPosition);
            _hasSetInitialDestination = true;
        }

        _targetPosition = _dragonNavMeshAgent.destination;

        if (_hasSetInitialDestination && _dragonNavMeshAgent.remainingDistance <= _arrivalDistance)
        {
            isMoving = false;
            _hasReachedTarget = false;
        }
        else
        {
            isMoving = true;
        }
    }

    public void SetMovementArea(Vector3 movementWay)
    {
        _targetPosition = movementWay;
    }
}
