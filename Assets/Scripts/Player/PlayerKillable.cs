using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillable : Killable
{
    public FollowCamera Camera;
    protected Vector3 lastCheckPoint;

    public void Start() {
        lastCheckPoint = this.transform.position;
    }

    public void SetCheckPoint(Vector3 checkPoint) {
        this.lastCheckPoint = checkPoint;
    }

    public override void Kill() {
        this.transform.position = lastCheckPoint;
        Camera.TeleportCameraToTarget();
    }
}
