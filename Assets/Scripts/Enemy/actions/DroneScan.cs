using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class DroneScanAction : BaseAction
{
    [SerializeField]
    protected string targetTag;

    [SerializeField]
    protected float ScanRadius;

    [SerializeField]
    protected BaseAction scanSuccess;

    [SerializeField]    
    protected float scanTime;

    [SerializeField]
    protected BaseAction scanFailed;

    protected Animator animator;
    protected DroneEnemyController controller;

    public override void Init(GameObject parent, ActionController actions) {
        base.Init(parent, actions);
        this.animator = parent.GetComponent<Animator>();
        this.controller = parent.GetComponent<DroneEnemyController>();
    }

    public override void Enter()
    {
        StartCoroutine(Scan());
    }

    protected IEnumerator Scan() {
        this.animator.Play("Scan");
        yield return new WaitForSeconds(scanTime);
        this.animator.Play("Idle");

        var target = ScanForTarget();
        this.controller.PlanPath(this.parent.transform.position);

        if (target != null) {
            this.parent.target = target;
            Debug.Log($"scan success");
            this.actions.Select(scanSuccess);
        }
        else {
            Debug.Log($"scan failed");
            this.actions.Select(scanFailed);
        }
    }

    protected GameObject ScanForTarget() {
        var colliders = Physics2D.OverlapCircleAll(parent.transform.position, ScanRadius);

        foreach (var collider in colliders) {
            Debug.Log(collider.tag);

            if (collider.transform.gameObject.CompareTag(targetTag)) {
                return collider.transform.gameObject;
            }
        }

        return null;
    }

    public override void Exit() {}

    public override void Play() {}
}