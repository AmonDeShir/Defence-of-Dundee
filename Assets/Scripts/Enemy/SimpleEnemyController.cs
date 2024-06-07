using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleEnemyController : EnemyController
{
    protected Animator animator;
    protected Rigidbody2D rb; 

    [SerializeField]
    protected Vector3 targetPosition;

    public float Speed = 0.5f;

    protected float progress = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        animator.Play("Idle");
    }

    public override void PlanPath(Vector3 targetPosition) {
        this.targetPosition = targetPosition;
    }

    public override void Stop() {
        this.targetPosition = gameObject.transform.position;
        this.rb.velocity = Vector2.zero;
    }

    protected float DistanceToTarget(Vector3 target) {
        return Vector2.Distance(this.transform.position, target);
    }

    void Update() {
        // if (IsOnTarget()) {
        //     if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")) {
        //         animator.Play("Idle");
        //     }
        // }
        //else {
          //  animator.Play("Walk");

            var delta = transform.position - targetPosition;
            rb.velocityX = -1 * Speed * Time.deltaTime * delta.normalized.x;
        //}
    }

    public override bool IsOnTarget()
    {
        return DistanceToTarget(targetPosition) < 0.1f;
    }
}
