using System;
using UnityEngine;

public class DronePatrolAction : PatrolAction
{
    [SerializeField]
    protected DroneScanAction scan;

    public override void Play()
    {
        if (DistanceToTarget() < 0.1 || this.controller.IsOnTarget()) {
            this.controller.PlanPath(this.target.position);
            this.ToggleTarget();
            this.actions.Select(scan);
        }
    }
}