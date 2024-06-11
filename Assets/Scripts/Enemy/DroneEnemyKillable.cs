using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(DroneEnemyController))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(DamageArea))]

public class DroneEnemyKillable : EnemyKillable
{
    protected Animator animator;
    protected DroneEnemyController controller;
    protected DamageArea damageArea;

    protected SpriteRenderer spriteRenderer;

    protected bool isDeath;

    public ParticleSystem particlesMetal;
    public ParticleSystem particlesFire;

    public AudioSource boomSound;
    public AudioSource flyingSound;

    public new void Start() {
        base.Start();

        this.controller = GetComponent<DroneEnemyController>();
        this.animator = GetComponent<Animator>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.damageArea = GetComponent<DamageArea>();

        this.isDeath = false;
    }

    public override void Kill() {
        if (!this.isDeath) {
            this.isDeath = true;

            this.animator.Play("Death");
            this.controller.enabled = false;
            this.rb.gravityScale = 3;
            this.rb.mass = 100;
            this.rb.sharedMaterial.bounciness = 0;
            this.rb.sharedMaterial.friction = 10;
            this.rb.velocity = Vector2.zero;
            this.particlesFire.Play();
            this.damageArea.enabled = false;
            this.flyingSound.Stop();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var contact = collision.contacts[0];

        if (ShouldDestroyOnContact(contact.collider) && this.isDeath) {
            this.enabled = false;
            this.particlesMetal.Play();
            this.rb.simulated = false;
            this.spriteRenderer.enabled = false;
            this.boomSound.Play();
            Destroy(this.transform.parent.gameObject, 1f);
        }
    }

    protected bool ShouldDestroyOnContact(Collider2D collider) {
        return collider.CompareTag("ground") || collider.CompareTag("jumpbooster");
    }
}
