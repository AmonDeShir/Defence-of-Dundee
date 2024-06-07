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

    protected new Drone parentObject;
    protected DroneEnemyController controller;

    [SerializeField]
    protected CircleCollider2D scanner;

    [SerializeField]
    protected BaseAction attack;

    protected bool active = false;

    [SerializeField]
    protected string targetTag;

    public override void Init(GameObject parentObject, ActionController actions) {
        this.actions = actions;
        this.parentObject = parentObject.GetComponent<Drone>();
        this.controller = parentObject.GetComponent<DroneEnemyController>();
    }

    public override void Enter()
    {
        active = true;
        scanner.radius = 0.8f;

        if (this.target == null) {
            this.target = GetClosedPoint();
        }

        this.controller.PlanPath(this.target.position);
    }

    protected Transform GetClosedPoint() {
        var fromDelta = Vector2.Distance(this.parentObject.transform.position, this.from.position);
        var toDelta = Vector2.Distance(this.parentObject.transform.position, this.to.position);

        return toDelta < fromDelta ? to : from;
    }

    public override void Exit()
    {
        active = false;
    }

    public override void Play()
    {
        if (DistanceToTarget() < 0.1 || this.controller.IsOnTarget()) {
            this.controller.PlanPath(this.target.position);
            this.ToggleTarget();
            this.actions.Select(scan);
        }
    }

    protected float DistanceToTarget() {
        return Vector2.Distance(this.parentObject.transform.position, this.target.position);
    }

    protected void ToggleTarget() {
        if (this.target == to) {
            this.target = from;
        }
        else {
            this.target = to;
        }
    }

    public void ScanEnter(TriggerEventArgument collider) {
        if (active && collider.CompareTag(targetTag)) {
            this.actions.Select(attack);
        }
    }
}