using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTouch : MonoBehaviour
{
    public string TargetTag = "Player";

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag(TargetTag)) {
            Destroy(this.gameObject);
        }       
    }
}
