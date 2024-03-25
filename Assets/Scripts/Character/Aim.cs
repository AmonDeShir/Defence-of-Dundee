using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterBody))]
public abstract class Aim : MonoBehaviour
{
    private CharacterBody body;

    void Start()
    {
        this.body = GetComponent<CharacterBody>();
    }

    void Update()
    {
        this.body.LeftArm.rotation = Quaternion.Euler(0, 0, CalculateAngleToTargetPoint());
    }

    protected abstract Vector3 GetDistanceToTarget();

    protected float CalculateAngleToTargetPoint() {
        var distance = GetDistanceToTarget().normalized;

        return Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
    }
}

