using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public virtual void MoveToTarget()
    {
        
    }

    public virtual void SetMovementArea(Collider movementArea)
    {
        
    }

    public virtual Vector3 GetRandomPointInArea(Bounds bounds)
    {
        return new Vector3();
    }
}
