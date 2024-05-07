using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class JumpPlatform : MonoBehaviour
{
    public float animationTime = 5f;
    public float jumpPower = 600f;

    [SerializeField]
    private Timer timer;
    private Timer deactivationTimer;


    [SerializeField]
    private Rigidbody2D target;
    [SerializeField]
    private Rigidbody2D previousTarget;

    private Animator animator;

    void Start() {
        timer = new Timer(animationTime, Jump, false);
        deactivationTimer = new Timer(2, Deactivate, false);

        animator = GetComponent<Animator>();
        previousTarget = null;
    }

    void Update() {
        deactivationTimer.Update();
        timer.Update();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (target == null) {
            target = collider.attachedRigidbody;
            animator.Play("Jump");
            timer.Start();
        }
    } 

    void OnTriggerExit2D(Collider2D collider) {
        var rb = collider.attachedRigidbody;  

        if (target == rb) {
            target = null;
            timer.IsStopped = true;
        }
    }

    private void Jump() {
        if (target != null) {
            var power = jumpPower;

            if (previousTarget == target) {
                power *= 1.25f;
            }

            target.velocity = new Vector2(target.velocity.x, power * Time.deltaTime);
            previousTarget = target;
            deactivationTimer.Start();
        }
    }

    private void Deactivate() {
        previousTarget = null;
    }
}
