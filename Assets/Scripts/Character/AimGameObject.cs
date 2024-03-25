using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AimGameObject : Aim
{
    protected static Vector3 DEFAULT_TARGET = new(0.428f, 0.0282f, 0);

    public GameObject Target;

    protected override Vector3 GetDistanceToTarget() {
        if (Target == null) {
            return DEFAULT_TARGET - transform.position;
        }

        return Target.transform.position - transform.position;
    }
}