using UnityEngine;

public class SimpleAim : MonoBehaviour
{
    protected static Vector3 DEFAULT_TARGET = new(0.428f, 0.0282f, 0);

    public GameObject Target;

    [SerializeField]
    private Transform gun;

    void Update()
    {
        this.gun.rotation = Quaternion.Euler(0, this.gun.rotation.eulerAngles.y, CalculateAngleToTargetPoint());
    }

    protected Vector3 GetDistanceToTarget() {
        if (Target == null) {
            return DEFAULT_TARGET - transform.position;
        }

        return (Target.transform.position - new Vector3(0, 1.5f, 0)) - transform.position;
    }
    protected float CalculateAngleToTargetPoint() {
        var distance = GetDistanceToTarget().normalized;

        return Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
    }
}