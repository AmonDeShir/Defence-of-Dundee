using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform Target;
    public FollowCamera Camera;
    public string TargetTag = "Player";

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag(TargetTag))
        {
            collider.transform.position = Target.position;
            Camera.TeleportCameraToTarget();
        }
    }
}
