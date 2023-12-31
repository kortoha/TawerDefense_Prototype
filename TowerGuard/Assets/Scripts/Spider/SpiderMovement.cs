using System;
using UnityEngine;
using UnityEngine.AI;

public class SpiderMovement : EnemyMovement
{
    [SerializeField] private float _arrivalDistance = 0.1f;

    [NonSerialized] public bool isMoving = false;

    private SmallSpiderDamage _smallSpiderDamage;
    private SpiderDamage _spiderDamage;
    private NavMeshAgent _spiderNavMeshAgent;
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
        _spiderNavMeshAgent = GetComponent<NavMeshAgent>();
        _spiderDamage = GetComponent<SpiderDamage>();
        _smallSpiderDamage = GetComponent<SmallSpiderDamage>();
    }

    private void Update()
    {
        _currentPosition = transform.position;

        if (!_hasReachedTarget)
        {
            MoveToTarget();
        }

        if (_smallSpiderDamage == null && _spiderDamage.health <= 0)
        {
            _spiderNavMeshAgent.destination = _currentPosition;
        }
        else if (_smallSpiderDamage != null && _smallSpiderDamage.health <= 0)
        {
            _spiderNavMeshAgent.destination = _currentPosition;
        }


        if (MainTower.Instance == null)
        {
            _spiderNavMeshAgent.destination = _currentPosition;
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

        if (!_hasReachedTarget && _spiderNavMeshAgent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            RaycastHit hit;
            if (Physics.Raycast(_currentPosition, _spiderNavMeshAgent.steeringTarget - _currentPosition, out hit, _spiderNavMeshAgent.radius, NavMesh.AllAreas))
            {
                var obstacleSpider = hit.collider.GetComponent<SpiderMovement>();
                if (obstacleSpider != null)
                {
                    BlockedWay?.Invoke(hit.point);
                }
            }
        }
    }

    private void OnEnable()
    {
        _spiderNavMeshAgent.updatePosition = true;
        _spiderNavMeshAgent.updateRotation = true;
    }

    private void OnDisable()
    {
        _spiderNavMeshAgent.updatePosition = false;
        _spiderNavMeshAgent.updateRotation = false;
    }

    public override void MoveToTarget()
    {
        if (_movementArea != null && !_hasSetInitialDestination)
        {
            _initialDestination = GetRandomPointInArea(_movementArea.bounds);
            _spiderNavMeshAgent.SetDestination(_initialDestination);
            _hasSetInitialDestination = true;
        }

        _targetPosition = _spiderNavMeshAgent.destination;

        if (_hasSetInitialDestination && _spiderNavMeshAgent.remainingDistance <= _arrivalDistance)
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
}
