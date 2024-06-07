using System;
using UnityEngine;

public class PatrolAction : BaseAction
{
    [SerializeField]
    protected Transform from;

    [SerializeField]
    protected Transform to;

    [SerializeField]
    protected Transform target;

    protected EnemyController controller;

    [SerializeField]
    protected CircleCollider2D scanner;

    [SerializeField]
    protected BaseAction attack;

    protected bool active = false;

    [SerializeField]
    protected string targetTag;

    [SerializeField]
    protected float scanRadius = 1f;

    public override void Init(GameObject parentObject, ActionController actions) {
        base.Init(parentObject, actions);
        this.controller = parentObject.GetComponent<EnemyController>();
    }

    public override void Enter()
    {
        active = true;
        scanner.radius = this.scanRadius;

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
            this.controller.Stop();
            this.actions.Select(attack);
        }
    }
}