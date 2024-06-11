using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class DroneScanAction : BaseAction
{
    [SerializeField]    
    protected float scanTime;

    [SerializeField]
    protected BaseAction patrol;

    protected Animator animator;
    protected DroneEnemyController controller;

    [SerializeField]
    protected BaseAction attack;

    [SerializeField]
    protected string targetTag;

    protected bool active;

    [SerializeField]
    protected AudioSource scanSound;


    public override void Init(GameObject parent, ActionController actions) {
        base.Init(parent, actions);
        this.animator = parent.GetComponent<Animator>();
        this.controller = parent.GetComponent<DroneEnemyController>();
    }

    public override void Enter()
    {
        this.active = true;
        this.controller.Stop();
        StartCoroutine(Scan());
    }

    public override void Exit() 
    {
        this.active = false;
    }


    protected IEnumerator Scan() {
        this.scanSound.Play();
        yield return new WaitForSeconds(scanTime);
        this.actions.Select(patrol);
        this.scanSound.Stop();
    }

    public override void Play() {
        this.animator.Play("Scan");
    }

    public void ScanEnter(TriggerEventArgument collider) {
        if (active && collider.CompareTag(targetTag)) {
            this.actions.Select(attack);
        }
    }
}