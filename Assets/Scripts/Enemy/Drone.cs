using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(DroneEnemyController))]
public class Drone : Enemy
{
    protected DroneEnemyController controller;
    protected GameObject player;
    public Tilemap ground;
    public String PlayerTag;
    public CircleCollider2D scanner;

    void Start()
    {
        controller = GetComponent<DroneEnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTargetEnter(TriggerEventArgument target) {
        scanner.radius = 1;
        ReachPlayer(target.position);
    }
     
    public void OnTargetExist(TriggerEventArgument target) {
        scanner.radius = 2;
    }

    void ReachPlayer(Vector3 position) {
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

}
