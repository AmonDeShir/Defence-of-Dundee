using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class DroneAttackAction : BaseAction
{
    protected new Drone parent;
    protected DroneEnemyController controller;

    [SerializeField]
    protected string targetTag;

    [SerializeField]
    protected float AttackZone = 1.5f;

    [SerializeField]
    protected Tilemap ground;


    [SerializeField]
    protected BaseAction failed;

    public override void Init(GameObject parent, ActionController actions) {
        this.actions = actions;
        this.parent = parent.GetComponent<Drone>();
        this.controller = parent.GetComponent<DroneEnemyController>();
    }

    public override void Exit() {}

    public override void Play()
    {
        if (!controller.IsOnTarget()) {
            return;
        }
        
        var player = ScanForTarget();
            
        if (player == null) {
            this.actions.Select(failed);
            return;
        }

        this.ReachPlayer(player.transform.position);
    }
    
    protected GameObject ScanForTarget() {
        var colliders = Physics2D.OverlapCircleAll(parent.transform.position, AttackZone);

        foreach (var collider in colliders) {
            if (collider.CompareTag(targetTag)) {
                return collider.gameObject;
            }
        }

        return null;
    }

    protected void ReachPlayer(Vector3 position) {
        int times = 10;

        Vector3 target;

        do {
            target = position + new Vector3(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1), 0);
            times -= 1;
            
            if (times <= 0) {
                target = position;
                break;
            }

        } while(ground.HasTile(ground.WorldToCell(target)));

        controller.PlanPath(target);
    }

    public override void Enter() { }
}