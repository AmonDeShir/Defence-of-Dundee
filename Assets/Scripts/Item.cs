using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Item : MonoBehaviour
{
    protected new SpriteRenderer renderer;
    
    [SerializeField]
    protected ParticleSystem destroyParticles;

    [SerializeField]
    protected ParticleSystem normalParticles;

    [SerializeField]
    protected string onlyForTag = "Player";

    [SerializeField]
    protected AudioSource pickUpSound;

    protected bool disabled = false;

    protected virtual void Start()
    {
        renderer = this.GetComponent<SpriteRenderer>();
    }

    public abstract void Effect(Killable target);

    void OnTriggerEnter2D(Collider2D collider) {
        if (!disabled && collider.TryGetComponent(out Killable entity)) {
            if (onlyForTag != null && !entity.CompareTag(onlyForTag)) {
                return;
            }

            this.Effect(entity);
            renderer.enabled = false;
            normalParticles.Stop();
            destroyParticles.Play();
            pickUpSound.Play();
            Destroy(transform.parent.gameObject, 2);
            disabled = true;
        }     
    }
}
