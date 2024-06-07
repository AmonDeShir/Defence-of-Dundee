using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DroneEnemyController : EnemyController
{
    public LayerMask GroundLayer;
    protected Animator animator;
    protected Rigidbody2D rb; 

    protected float groundScan = 1f;

    [SerializeField]
    protected Vector3 targetPosition;

    public AStar pathfinder;

    protected Stack<Vector3> path;

    public float Speed = 0.5f;
    public float LongTravelSpeed = 4f;
    public float CloseTravelSpeed = 3f;

    protected float progress = 0f;

    protected Vector3 margin;

    private Vector2 lastCollision;
    private float collisionsDistance;

    private FlagTimer flyOffTImer;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        flyOffTImer = new FlagTimer(0.5f);

        path = new();
        animator.Play("Idle");
        lastCollision = Vector2.zero;
        collisionsDistance = 1f;
    }

    public override void PlanPath(Vector3 targetPosition) {
        this.targetPosition = targetPosition;

        if (!Linecast()) {
            path = new Stack<Vector3>(new []{ targetPosition });
            Speed = CloseTravelSpeed;
        }
        else {
            path = pathfinder.Find(transform.position, targetPosition, 100);
            margin = transform.position - path.First();
            Speed = LongTravelSpeed;
        }
    }

    public override void Stop() {
        this.path.Clear();
        this.rb.velocity = Vector2.zero;
    }

    protected float DistanceToTarget(Vector3 target) {
        return Vector2.Distance(this.transform.position, target);
    }

    public override bool IsOnTarget() {
        return path.Count == 0;
    }

    protected bool Linecast() {
        return Physics2D.Linecast(transform.position, targetPosition, GroundLayer);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var contact = collision.contacts[0];

        collisionsDistance = Vector2.Distance(lastCollision, contact.point);
        lastCollision = contact.point;

        rb.velocity /= 4;
        rb.AddForce(contact.normal * 10000);
        flyOffTImer.Start();
    }

    void Update() {
        flyOffTImer.Update();

        if (!flyOffTImer.IsStopped) {
            return;
        }

        if (path.Count > 0) {
            animator.Play("Walk");

            var target = path.First() + margin;
            var delta = transform.position - target;
            rb.velocity = delta.normalized * -1 * Time.fixedDeltaTime * Speed; 

            if (Vector2.Distance(target, transform.position) < 0.1 || IsStuck()) {
                collisionsDistance = 1f;
                path.Pop();
            }
        }
        else {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")) {
                animator.Play("Idle");
            }
        }
    }

    bool IsStuck() {
        return collisionsDistance < 0.5f;
    }
}
