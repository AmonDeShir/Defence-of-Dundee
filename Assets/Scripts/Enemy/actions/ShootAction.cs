
using System;
using UnityEngine;

public class ShootAction : BaseAction {
    [SerializeField]
    protected Shoot gun;

    [SerializeField]
    protected SimpleAim aim;

    [SerializeField]
    protected LayerMask GroundLayer;

    [SerializeField]
    protected string targetTag;

    protected GameObject player = null;

    public BaseAction exitAction;

    public override void Enter() 
    {
    }

    public override void Play() 
    {
        aim.Target = player;

        if (player != null) {
            if (!Linecast(player.transform.position)) {
                gun.SpawnBullet();
            }
        }
        else {
            actions.Select(exitAction);
        } 
    }

    protected bool Linecast(Vector2 targetPosition) {
        return Physics2D.Linecast(transform.position, targetPosition, GroundLayer);
    }


    public override void Exit() 
    {
    }

    public void ScanEnter(TriggerEventArgument collider) {
        if (collider.CompareTag(targetTag)) {
            player = collider.gameObject;
        }
    }

    public void ScanExit(TriggerEventArgument collider) {
        if (collider.CompareTag(targetTag)) {
            player = null;
        }
    }
}