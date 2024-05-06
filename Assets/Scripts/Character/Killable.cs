using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    public virtual void Kill() {
        Destroy(this.gameObject);
    }
}
