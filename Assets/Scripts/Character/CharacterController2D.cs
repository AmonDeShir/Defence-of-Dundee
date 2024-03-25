using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterBody))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterBody))]
public class CharacterController2D : MonoBehaviour
{
    public LayerMask GroundLayer;
    protected Animator animator;
    protected Rigidbody2D rb; 
    protected CharacterBody body;
    protected Vector2 direction;
    
    public float speed = 100f;
    public float jumpPower = 400f;
    public float ultraJumpPower = 800f;
    public float fallSpeed = 5f;

    protected bool jump;
    protected bool ultraJump;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        body = GetComponent<CharacterBody>();
        direction = Vector2.zero;
    }


    void FixedUpdate()
    {
        rb.velocity = new Vector2(direction.x * speed * Time.deltaTime, rb.velocity.y);

        if (jump && IsOnGround()) {
            var power = ultraJump ? ultraJumpPower : jumpPower;

            rb.velocity = new Vector2(rb.velocity.x, power * Time.deltaTime);
        }

        if (rb.velocity.y < 0) {
            rb.velocity -= fallSpeed * Time.deltaTime * new Vector2(0, -Physics2D.gravity.y);
        }

        jump = false;
        ultraJump = false;
    }

    protected bool IsOnGround() {
        return Physics2D.OverlapCapsule(body.GroundCheck.position, new Vector2(0.2f, 0.02f), CapsuleDirection2D.Horizontal, 0, GroundLayer);
    }
}
