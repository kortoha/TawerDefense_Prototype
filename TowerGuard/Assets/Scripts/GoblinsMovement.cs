using System;
using UnityEngine;
using UnityEngine.AI;

public class GoblinsMovement : MonoBehaviour
{
    [SerializeField] private float _arrivalDistance = 0.1f;
    [NonSerialized] public bool isMoving = false;

    private GoblinsDamage _goblinsDamage;
    private NavMeshAgent _goblinsNavMeshAgent;
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
        _goblinsNavMeshAgent = GetComponent<NavMeshAgent>();
        _goblinsDamage = GetComponent<GoblinsDamage>();
    }

    private void Update()
    {
        _currentPosition = transform.position;

        if (!_hasReachedTarget)
        {
            MoveToTarget();
        }

        if(_goblinsDamage.goblinsHealth <= 0)
        {
            _goblinsNavMeshAgent.destination = transform.position;
        }


        if (MainTower.Instance == null)
        {
            _goblinsNavMeshAgent.destination = transform.position;
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

        if (!_hasReachedTarget && _goblinsNavMeshAgent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, _goblinsNavMeshAgent.steeringTarget - transform.position, out hit, _goblinsNavMeshAgent.radius, NavMesh.AllAreas))
            {
                var obstacleGoblin = hit.collider.GetComponent<GoblinsMovement>();
                if (obstacleGoblin != null)
                {
                    BlockedWay?.Invoke(hit.point);
                }
            }
        }
    }

    private void OnEnable()
    {
        _goblinsNavMeshAgent.updatePosition = true;
        _goblinsNavMeshAgent.updateRotation = true;
    }

    private void OnDisable()
    {
        _goblinsNavMeshAgent.updatePosition = false;
        _goblinsNavMeshAgent.updateRotation = false;
    }

    private void MoveToTarget()
    {
        if (_movementArea != null && !_hasSetInitialDestination)
        {
            _initialDestination = GetRandomPointInArea(_movementArea.bounds);
            _goblinsNavMeshAgent.SetDestination(_initialDestination);
            _hasSetInitialDestination = true;
        }

        _targetPosition = _goblinsNavMeshAgent.destination;

        if (_hasSetInitialDestination && _goblinsNavMeshAgent.remainingDistance <= _arrivalDistance)
        {
            isMoving = false;
            _hasReachedTarget = false;
        }
        else
        {
            isMoving = true;
        }
    }

    public void SetMovementArea(Collider movementArea)
    {
        _movementArea = movementArea;
    }

    private Vector3 GetRandomPointInArea(Bounds bounds)
    {
        return new Vector3(
            UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
            bounds.center.y,
            UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}