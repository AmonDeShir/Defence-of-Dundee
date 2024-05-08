using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DroneEnemyController : MonoBehaviour
{
    public LayerMask GroundLayer;
    protected Animator animator;
    protected Rigidbody2D rb; 

    protected float groundScan = 1f;

    [SerializeField]
    protected Vector3 targetPoint;
    public Transform target;

    public AStar pathfinder;

    protected Stack<Vector3> path;

    public float Speed = 0.5f;
    public float LongTravelSpeed = 4f;
    public float CloseTravelSpeed = 3f;

    protected float progress = 0f;

    protected Vector3 margin;

    public Tilemap ground;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        path = new();
        animator.Play("Idle");
    }

    public void ScanPath() {
        animator.Play("Scan");
        Debug.Log("RB 1");

        var times = 10;

        do {
            targetPoint = target.transform.position + new Vector3(Random.Range(-4, 4), Random.Range(1, 5), 0);
            times -= 1;
            
            if (times <= 0) {
                targetPoint = target.transform.position;
                break;
            }

        } while(ground.HasTile(ground.WorldToCell(targetPoint)));

        Debug.Log("RB 2");
        if (!Linecast()) {
            Debug.Log("RB 3");
            path = new Stack<Vector3>(new []{ targetPoint });
            Speed = CloseTravelSpeed;
        }
        else {
            Debug.Log("RB 4");
            path = pathfinder.Find(transform.position, targetPoint, 100);
            margin = transform.position - path.First();
            Speed = LongTravelSpeed;
            Debug.Log("RB 5");
        }
    }

    protected bool Linecast() {
        return Physics2D.Linecast(transform.position, targetPoint, GroundLayer);
    }

    void Update() {
        if (path.Count > 0) {
            animator.Play("Walk");
            progress = Mathf.Clamp01(progress + (Speed * Time.deltaTime));
            rb.MovePosition(Vector3.Lerp(transform.position, path.First() + margin, progress));

            if (progress >= 1f || Vector3.Distance(this.transform.position, path.First() + margin) <= 0.1f) {
                progress = 0f;
                path.Pop();
            }
        }
        else {
            ScanPath();
        }
    }
}
