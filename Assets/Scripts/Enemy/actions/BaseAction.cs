
using System;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour {
    protected Enemy parentObject;
    protected ActionController actions;

    public virtual void Init(GameObject parentObject, ActionController actions) {
        this.parentObject = parentObject.GetComponent<Enemy>();
        this.actions = actions;
    }

    public abstract void Enter();
    public abstract void Play();
    public abstract void Exit();
}