using UnityEngine;

public abstract class EnemyController: MonoBehaviour
{
    public abstract void PlanPath(Vector3 targetPosition);
    public abstract void Stop();
    public abstract bool IsOnTarget();
}
