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

    [SerializeField]
    protected CircleCollider2D scanner;

    protected bool active = false;

    protected FlagTimer updateAttackPos;

    protected GameObject player = null;

    public override void Init(GameObject parent, ActionController actions) {
        this.actions = actions;
        this.parent = parent.GetComponent<Drone>();
        this.controller = parent.GetComponent<DroneEnemyController>();
    
        this.updateAttackPos = new FlagTimer(2, true);
        this.active = true;
        this.scanner.radius = AttackZone;
    }

    public override void Exit() {
    }

    public override void Play()
    {
        this.updateAttackPos.Update();

        if (player == null) {
            this.actions.Select(failed);
            this.updateAttackPos.Stop();
        }

        if(this.updateAttackPos.HasFinishedCounting) {
            this.updateAttackPos.Start();
            ReachPlayer(player.transform.position);
        }
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

    public override void Enter() {
        scanner.radius = AttackZone;
        this.updateAttackPos.Start();

        if (player != null) {
            this.updateAttackPos.Start();
            controller.PlanPath(player.transform.position);
        }
    }

    public void ScanEnter(TriggerEventArgument collider) {
        if (collider.CompareTag(targetTag)) {
            Debug.Log("Scan enter!! " + collider.gameObject.tag + " is attackgin " + this.active);
            player = collider.gameObject;

            if (active) {
                this.updateAttackPos.Start();
                controller.PlanPath(player.transform.position);
            }
        }
    }

    public void ScanExit(TriggerEventArgument collider) {
        if (collider.CompareTag(targetTag)) {
            player = null;
        }      
    }
}