using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillable : Killable
{
    public PlayerData data;
    public FollowCamera Camera;

    public void Start() {
        this.SetCheckPoint(this.transform.position);
    }

    public void SetCheckPoint(Vector3 checkPoint) {
        data.checkpoint = checkPoint;
    }

    public override void Kill() {
        this.transform.position = data.checkpoint;
        Camera.TeleportCameraToTarget();
    }
}
