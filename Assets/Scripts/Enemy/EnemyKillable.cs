using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Enemy))]
public class EnemyKillable : Killable
{
    protected Enemy data;
    protected Rigidbody2D rb;

    public void Start() {
        this.rb = GetComponent<Rigidbody2D>();
        this.data = GetComponent<Enemy>();
    }

    public override void Kill() {
        Destroy(this.gameObject);
    }

    public override void Hit(int damage, GameObject attacker) {
        data.HP -= damage;

        if (data.HP == 0) {
            Kill();
            return;
        }
    }
}
