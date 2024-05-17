using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int damage;

    [SerializeField]
    private float lifeTime;

    [SerializeField]
    private float maxSpeed;

    [SerializeField]
    public float acceleration = 5f;

    private float speed;
    
    private Rigidbody2D rb;

    private bool isStopped;

    private Timer autoDestroy;

    public SpriteRenderer sprite;
    public ParticleSystem particles;

    void Start()
    {
        this.speed = maxSpeed / 2;
        this.rb = GetComponent<Rigidbody2D>();
        this.isStopped = false;

        this.autoDestroy = new Timer(lifeTime, ()=>SelfDestroy(), true);

    }

    void FixedUpdate()
    {   
        if (!isStopped) {
            speed = Math.Min(maxSpeed, speed + acceleration);
            rb.velocity = transform.right * speed;    
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Bullet")) {
            return;
        }

        if (collision.collider.TryGetComponent(out Killable entity)) {
            entity.Hit(damage, this.gameObject);
        }

        SelfDestroy();
    }

    void SelfDestroy() {
        Destroy(this.gameObject, 1);
        this.particles.Play();
        this.sprite.gameObject.SetActive(false);
        isStopped = true;
    }
}
