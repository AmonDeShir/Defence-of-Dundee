using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TriggerEventArgument {

    public Collider2D collider;
    public GameObject gameObject;
    public Transform transform;

    public TriggerEventArgument(Collider2D collider) {
        this.gameObject = collider.gameObject;
        this.collider = collider;
        this.transform = collider.gameObject.transform;
    }

    public bool CompareTag(string tag) {
        return this.collider.CompareTag(tag);
    }
}

[System.Serializable]
public class TriggerEvent : UnityEvent<TriggerEventArgument>
{
}

public class EventArea : MonoBehaviour
{
    public TriggerEvent onEnter;
    public TriggerEvent onStay;
    public TriggerEvent onExit;

    void OnTriggerEnter2D(Collider2D collider) {
        onEnter.Invoke(new TriggerEventArgument(collider));
    }

    void OnTriggerStay2D(Collider2D collider) {
        onStay.Invoke(new TriggerEventArgument(collider));
    }

    void OnTriggerExit2D(Collider2D collider) {
        onExit.Invoke(new TriggerEventArgument(collider));    
    }
}
