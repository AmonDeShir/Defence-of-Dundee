using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TriggerEventArgument {
    public Vector3 position;
    public string tag;

    public TriggerEventArgument(Collider2D collider): this(collider.transform.position, collider.tag) {}

    public TriggerEventArgument(Vector3 position, string tag) {
        this.position = position;
        this.tag = tag;
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
