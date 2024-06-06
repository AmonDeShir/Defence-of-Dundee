
using System;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour {
    protected Enemy parent;
    protected ActionController actions;

    public virtual void Init(GameObject parent, ActionController actions) {
        this.parent = parent.GetComponent<Enemy>();
        this.actions = actions;
    }

    public abstract void Enter();
    public abstract void Play();
    public abstract void Exit();
}