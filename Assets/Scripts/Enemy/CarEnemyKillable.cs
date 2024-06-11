using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SimpleEnemyController))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(DamageArea))]

public class CarEnemyKillable : EnemyKillable
{
    protected EnemyController controller;
    protected DamageArea damageArea;

    protected SpriteRenderer spriteRenderer;

    protected bool isDeath;

    public ParticleSystem particlesMetal;

    public ActionController actions;

    public SpriteRenderer gun;

    public AudioSource boomSound;
    public AudioSource engineSound;

    public new void Start() {
        base.Start();

        this.controller = GetComponent<EnemyController>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.damageArea = GetComponent<DamageArea>();
        
        this.isDeath = false;
    }

    public override void Kill() {
        if (!this.isDeath) {
            this.isDeath = true;

            this.actions.enabled = false;
            this.controller.Stop();
            this.damageArea.enabled = false;
            this.enabled = false;
            this.particlesMetal.Play();
            this.spriteRenderer.enabled = false;
            this.gun.enabled = false;
            this.engineSound.Stop();
            this.boomSound.Play();

            Destroy(this.transform.parent.gameObject, 1f);
        }
    }
}
