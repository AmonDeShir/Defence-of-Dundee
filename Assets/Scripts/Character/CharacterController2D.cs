using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterBody))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterBody))]
public class CharacterController2D : MonoBehaviour
{
    public NPCData statistics;

    public LayerMask GroundLayer;
    protected Animator animator;
    protected Rigidbody2D rb; 
    protected CharacterBody body;
    protected Vector2 direction;
    
    protected bool crouch;
    protected bool jump;
    protected bool ultraJump;
    protected bool run;
    protected bool instantFall;

    public Vector2 Direction { get { return direction; }}

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        body = GetComponent<CharacterBody>();
        direction = Vector2.zero;
    }

    protected void Update() {
        animator.SetBool("run", direction.x != 0);
        animator.SetBool("jump", jump);
        animator.SetBool("crouch", crouch);
        animator.SetInteger("direction", Math.Sign(direction.x * body.GetFrontSide()));
    }

    void FixedUpdate()
    {
        float movement_speed = statistics.speed;
        
        if (run) {
            movement_speed *= statistics.run;
        }

        if (crouch) {
            movement_speed = 0;
        }

        rb.velocity = new Vector2(direction.x * movement_speed  * Time.deltaTime, rb.velocity.y);

        if (jump && IsOnGround()) {
            var power = ultraJump ? statistics.ultraJump : statistics.jump;

            rb.velocity = new Vector2(rb.velocity.x, power * Time.deltaTime);
        }

        if (rb.velocity.y < 2 || instantFall) {
            var fallSpeed = instantFall ? statistics.fallSpeed * 10 : statistics.fallSpeed;
            rb.velocity -= fallSpeed * Time.deltaTime * new Vector2(0, -Physics2D.gravity.y);

            if (instantFall) {
                rb.velocityX = 0;
            }
        }

        jump = false;
        ultraJump = false;
        instantFall = false;
    }

    protected bool IsOnGround() {
        return Physics2D.OverlapCapsule(body.GroundCheck.position, new Vector2(0.2f, 0.02f), CapsuleDirection2D.Horizontal, 0, GroundLayer);
    }
}
