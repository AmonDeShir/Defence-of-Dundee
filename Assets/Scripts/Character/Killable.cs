using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    public virtual void Kill() {
        Destroy(this.gameObject);
    }

    public virtual void Hit(int damage, GameObject attacker) {
        Kill();
    }
}
