using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 _target;
    private float _rotationSpeed = 2.0f;

    private void Start()
    {
        _target = MainTower.Instance.transform.position;
    }
    public virtual void MoveToTarget()
    {
        
    }

    public virtual void SetMovementArea(Collider movementArea)
    {
        
    }

    public virtual Vector3 GetRandomPointInArea(Bounds bounds)
    {
        return new Vector3(
            UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
            bounds.center.y,
            UnityEngine.Random.Range(bounds.min.z, bounds.max.z));
    }

    public virtual void LookAtTower()
    {
        transform.LookAt(_target);
    }
}
