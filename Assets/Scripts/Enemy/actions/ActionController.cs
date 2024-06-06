
using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour {
    protected Enemy parent;

    [SerializeField]
    protected BaseAction current;

    public void Start() {
        for(int i = 0; i < this.transform.childCount; i++) {
            var action = this.transform.GetChild(i).GetComponent<BaseAction>();
            action.Init(this.transform.parent.gameObject, this);
        }

        current.Enter();
    }

    public void Select(BaseAction action) {
        current.Exit();
        this.current = action;
        this.current.Enter();
    }

    public void Update() {
        current.Play();
    }
}
