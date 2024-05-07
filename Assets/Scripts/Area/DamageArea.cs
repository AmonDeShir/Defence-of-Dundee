using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public int Damage = 0;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.TryGetComponent(out Killable entity)) {
            entity.Hit(Damage, transform.gameObject);
        }     
    }
}
