using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InstantFallState {
    DISABLED,
    START,
    FREEZE,
    FALL,
}

[RequireComponent(typeof(CharacterBody))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterBody))]
public class CharacterController2D : MonoBehaviour
{
    public MovementStats statistics;

    public LayerMask GroundLayer;
    protected Animator animator;
    protected Rigidbody2D rb; 
    protected CharacterBody body;
    protected Vector2 direction;
    
    protected bool crouch;
    protected bool jump;
    protected bool ultraJump;
    protected bool run;
    protected InstantFallState instantFall;
    public Timer instantFallTimer;
    public ParticleSystem dustParticles;
    public ParticleSystem dubleJumpParticles;

    protected int jumpCount;

    public Vector2 Direction { get { return direction; }}

    public Timer stoppedTimer;
    public FlagTimer playGroundSoundTimer;

    protected bool stopped = false;
    protected bool isTouchingWall = false;
    protected FlagTimer wallBounceTimer;
    protected ContactPoint2D lastGroundContact;

    public AudioSource runningSound;
    public AudioSource contactGroundSound;
    public AudioSource doubleJumpSound;
    public AudioSource jumpSound;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        body = GetComponent<CharacterBody>();
        direction = Vector2.zero;
        stoppedTimer = new Timer(0.5f, StartMovement, false);
        instantFallTimer = new Timer(0.1f, StartInstantFall, false);
        playGroundSoundTimer = new FlagTimer(0.5f, true);
    }

    protected void Update() {
        animator.SetBool("run", direction.x != 0);
        animator.SetBool("jump", jump);
        animator.SetBool("crouch", crouch);
        animator.SetInteger("direction", Math.Sign(direction.x * body.GetFrontSide()));
        animator.SetFloat("velocityY", rb.velocityY);

        instantFallTimer.Update();
        stoppedTimer.Update();
        playGroundSoundTimer.Update();
    }

    void FixedUpdate()
    {
        if (stopped) {
            return;
        }

        if (instantFall == InstantFallState.START) {
            instantFall = InstantFallState.FREEZE;
            instantFallTimer.Start();
            return;
        }

        if (instantFall == InstantFallState.FREEZE) {
            rb.velocity = Vector2.zero;
            return;
        }

        float movement_speed = statistics.speed;
        
        if (run) {
            movement_speed *= statistics.run;
        }

        if (crouch) {
            movement_speed = 0;
        }

        if (isTouchingWall && !(jump && CanJump())) {
            movement_speed = 0;
        }

        rb.velocity = new Vector2(direction.x * movement_speed  * Time.deltaTime, rb.velocity.y);

        if (isTouchingWall) {
            dustParticles.Play();

            // Prevent wall clipping
            if (IsOnGround()) {
                rb.velocityX = lastGroundContact.normal.x * 2f;
            }
        }

        if (jump && CanJump()) {
            var power = statistics.jump;

            if (crouch && ultraJump && jumpCount == 0) {
                power = statistics.ultraJump;
            }

            rb.velocity = new Vector2(rb.velocity.x, power * Time.deltaTime);

            if (!isTouchingWall) {
                jumpCount += 1;
            }

            if (jumpCount > 1 || ultraJump) {
                dubleJumpParticles.Play();
                doubleJumpSound.Play();
            }
            else {
                dustParticles.Play();
                jumpSound.Play();
            }

        }

        if (rb.velocity.y < 2 || instantFall == InstantFallState.FALL) {
            var fallSpeed = instantFall == InstantFallState.FALL ? statistics.fallSpeed * 10 : statistics.fallSpeed;
            rb.velocity -= fallSpeed * Time.deltaTime * new Vector2(0, -Physics2D.gravity.y);

            if (instantFall == InstantFallState.FALL) {
                dustParticles.Play();
                rb.velocityX = 0;
            }
        }

        jump = false;
        ultraJump = false;
        instantFall = InstantFallState.DISABLED;
    }

    protected bool IsOnGround() {
        return Physics2D.OverlapCapsule(body.GroundCheck.position, new Vector2(0.2f, 0.02f), CapsuleDirection2D.Horizontal, 0, GroundLayer);
    }

    protected bool CanJump() {
        if (IsOnGround()) {
            jumpCount = 0;

            return true;
        }

        return jumpCount < statistics.maxJumpCount;
    }

    public void StopMovement(float time = 0.5f) {
        stopped = true;
        stoppedTimer.Time = time;
        stoppedTimer.Start();
    }

    public void StartMovement() {
        stopped = false;
        stoppedTimer.IsStopped = true;
    }

    public void StartInstantFall() {
        instantFall = InstantFallState.FALL;
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("ground") && playGroundSoundTimer.HasFinishedCounting) {
            dustParticles.Play();
            contactGroundSound.Play();
        }
    }

    public void OnCollisionStay2D(Collision2D collision) {
        if (collision.collider.CompareTag("ground")) {
            lastGroundContact = collision.contacts[0];
            isTouchingWall = collision.contacts[0].normal.y == 0;

            if (direction.x != 0) {
                if (runningSound.time == 0) {
                    runningSound.Play();
                }
                else {
                    runningSound.UnPause();
                }
            }
            else {
                runningSound.Pause();
            }
        }
    }

    public void OnCollisionExit2D(Collision2D collision) {

        if (collision.collider.CompareTag("jumpbooster")) {
            dustParticles.Play();
        }

        if (collision.collider.CompareTag("ground")) {
            isTouchingWall = false;
            runningSound.Pause();
        }

        playGroundSoundTimer.Start();
    }
}
