using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Checkpoint : MonoBehaviour
{
    public Color Active;
    public AudioSource sound;

    private SpriteRenderer sprite;

    private bool activated;

    public void Start() {
        sprite = GetComponent<SpriteRenderer>();
        activated = false;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (!activated && collider.TryGetComponent(out PlayerKillable entity)) {
            entity.SetCheckPoint(this.transform.position);
            sprite.color = Active;
            sound.Play();
            activated = true;
        }
    }
}
