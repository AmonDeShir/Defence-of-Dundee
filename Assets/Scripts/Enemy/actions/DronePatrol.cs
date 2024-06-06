using System;
using UnityEngine;

[Serializable]
public class DronePatrolAction : BaseAction
{
    [SerializeField]
    protected Transform from;

    [SerializeField]
    protected Transform to;

    [SerializeField]
    protected Transform target;

    [SerializeField]
    protected DroneScanAction scan;

    protected new Drone parent;
    protected DroneEnemyController controller;

    protected bool active = false;

    public override void Init(GameObject parent, ActionController actions) {
        this.actions = actions;
        this.parent = parent.GetComponent<Drone>();
        this.controller = parent.GetComponent<DroneEnemyController>();
    }

    public override void Enter()
    {
        active = true;

        if (this.target == null) {
            this.target = GetClosedPoint();
        }

        this.controller.PlanPath(this.target.position);
    }

    protected Transform GetClosedPoint() {
        var fromDelta = Vector2.Distance(this.parent.transform.position, this.from.position);
        var toDelta = Vector2.Distance(this.parent.transform.position, this.to.position);

        return toDelta < fromDelta ? to : from;
    }

    public override void Exit()
    {
        active = false;
    }

    public override void Play()
    {
        if (this.controller.IsOnTarget()) {
            this.controller.PlanPath(this.target.position);
        }

        if (DistanceToTarget() < 0.1) {
            this.ToggleTarget();
            this.actions.Select(scan);
        }
    }

    protected float DistanceToTarget() {
        return Vector2.Distance(this.parent.transform.position, this.target.position);
    }

    protected void ToggleTarget() {
        if (this.target == to) {
            this.target = from;
        }
        else {
            this.target = to;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (active) {
            Debug.Log("Find " + collider.name);
        }

    }
}