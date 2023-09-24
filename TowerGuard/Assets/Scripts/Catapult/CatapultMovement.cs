using System;
using UnityEngine;
using UnityEngine.AI;

public class CatapultMovement : EnemyMovement
{
    [SerializeField] private float _arrivalDistance = 0.1f;
    [NonSerialized] public bool isMoving = false;

    private CatapultDamage _catapultDamage;
    private NavMeshAgent _catapultNavMeshAgent;
    private bool _hasSetInitialDestination = false;
    private Vector3 _currentPosition;
    private Vector3 _targetPosition;
    private bool _hasReachedTarget = false;

    public event Action ReachedTargetEvent;
    public event Action RestingState;
    public event Action<Vector3> BlockedWay;

    private void Awake()
    {
        _catapultNavMeshAgent = GetComponent<NavMeshAgent>();
        _catapultDamage = GetComponent<CatapultDamage>();
    }

    private void Update()
    {
        _currentPosition = transform.position;

        if (!_hasReachedTarget)
        {
            MoveToTarget();
        }

        if (_catapultDamage.health <= 0)
        {
            _catapultNavMeshAgent.destination = _currentPosition;
        }


        if (MainTower.Instance == null)
        {
            _catapultNavMeshAgent.destination = _currentPosition;
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

        if (!_hasReachedTarget && _catapultNavMeshAgent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            RaycastHit hit;
            if (Physics.Raycast(_currentPosition, _catapultNavMeshAgent.steeringTarget - _currentPosition, out hit, _catapultNavMeshAgent.radius, NavMesh.AllAreas))
            {
                var obstacleCatapult = hit.collider.GetComponent<CatapultMovement>();
                if (obstacleCatapult != null)
                {
                    BlockedWay?.Invoke(hit.point);
                }
            }
        }
    }

    private void OnEnable()
    {
        _catapultNavMeshAgent.updatePosition = true;
        _catapultNavMeshAgent.updateRotation = true;
    }

    private void OnDisable()
    {
        _catapultNavMeshAgent.updatePosition = false;
        _catapultNavMeshAgent.updateRotation = false;
    }

    public override void MoveToTarget()
    {
        if (_targetPosition != null && !_hasSetInitialDestination)
        {
            _catapultNavMeshAgent.SetDestination(_targetPosition);
            _hasSetInitialDestination = true;
        }

        _targetPosition = _catapultNavMeshAgent.destination;

        if (_hasSetInitialDestination && _catapultNavMeshAgent.remainingDistance <= _arrivalDistance)
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
