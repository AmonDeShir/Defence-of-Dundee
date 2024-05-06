using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Checkpoint : MonoBehaviour
{
    public Color Active;

    private SpriteRenderer sprite;

    public void Start() {
        sprite = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.TryGetComponent(out PlayerKillable entity)) {
            entity.SetCheckPoint(this.transform.position);
            sprite.color = Active;
        }
    }
}
