using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MinotaurusMovement : EnemyMovement
{
    [SerializeField] private float _arrivalDistance = 0.1f;
    [NonSerialized] public bool isMoving = false;

    private MinotaurusDamage _minotaurusDamage;
    private NavMeshAgent _minotaurusNavMeshAgent;
    private Collider _movementArea;
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
        _minotaurusNavMeshAgent = GetComponent<NavMeshAgent>();
        _minotaurusDamage = GetComponent<MinotaurusDamage>();
    }

    private void Update()
    {
        _currentPosition = transform.position;

        if (!_hasReachedTarget)
        {
            MoveToTarget();
        }

        if (_minotaurusDamage.health <= 0)
        {
            _minotaurusNavMeshAgent.destination = transform.position;
        }


        if (MainTower.Instance == null)
        {
            _minotaurusNavMeshAgent.destination = transform.position;
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

        if (!_hasReachedTarget && _minotaurusNavMeshAgent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, _minotaurusNavMeshAgent.steeringTarget - transform.position, out hit, _minotaurusNavMeshAgent.radius, NavMesh.AllAreas))
            {
                var obstacleMinotaurus = hit.collider.GetComponent<MinotaurusMovement>();
                if (obstacleMinotaurus != null)
                {
                    BlockedWay?.Invoke(hit.point);
                }
            }
        }
    }

    private void OnEnable()
    {
        _minotaurusNavMeshAgent.updatePosition = true;
        _minotaurusNavMeshAgent.updateRotation = true;
    }

    private void OnDisable()
    {
        _minotaurusNavMeshAgent.updatePosition = false;
        _minotaurusNavMeshAgent.updateRotation = false;
    }

    public override void MoveToTarget()
    {
        if (_movementArea != null && !_hasSetInitialDestination)
        {
            _initialDestination = GetRandomPointInArea(_movementArea.bounds);
            _minotaurusNavMeshAgent.SetDestination(_initialDestination);
            _hasSetInitialDestination = true;
        }

        _targetPosition = _minotaurusNavMeshAgent.destination;

        if (_hasSetInitialDestination && _minotaurusNavMeshAgent.remainingDistance <= _arrivalDistance)
        {
            isMoving = false;
            _hasReachedTarget = false;
        }
        else
        {
            isMoving = true;
        }
    }

    public override void SetMovementArea(Collider movementArea)
    {
        _movementArea = movementArea;
    }

    public override Vector3 GetRandomPointInArea(Bounds bounds)
    {
        return new Vector3(
            UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
            bounds.center.y,
            UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}