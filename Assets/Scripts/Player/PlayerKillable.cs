using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterController2D))]
public class PlayerKillable : Killable
{
    public PlayerHP data;
    public FollowCamera Camera;
    private Rigidbody2D rb;
    private CharacterController2D controller;
    public float hitForce;

    public void Start() {
        this.SetCheckPoint(this.transform.position);
        this.rb = GetComponent<Rigidbody2D>();
        this.controller = GetComponent<CharacterController2D>();
    }

    public void SetCheckPoint(Vector3 checkPoint) {
        data.checkpoint = checkPoint;
    }

    public override void Kill() {
        this.transform.position = data.checkpoint;
        data.ResetHP();
        data.Lives -= 1;
        
        if (data.Lives == 0) {
            SceneManager.LoadScene(1);
        }

        Camera.TeleportCameraToTarget();
    }

    public override void Hit(int damage, GameObject attacker) {
        data.HP -= damage;

        if (data.HP == 0) {
            Kill();
            return;
        }

        var direction = attacker.transform.position - transform.position;
        rb.velocity = -direction * hitForce;

        controller.StopMovement();
    }

    public override void Heal(int value) {
        this.data.HP += value;
    }
}
