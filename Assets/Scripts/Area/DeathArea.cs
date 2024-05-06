using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathArea : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.TryGetComponent(out Killable entity)) {
            entity.Kill();
        }     
    }
}
