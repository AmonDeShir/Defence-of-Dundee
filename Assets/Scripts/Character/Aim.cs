using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(CharacterBody))]
public abstract class Aim : MonoBehaviour
{
    private CharacterBody body;
    private float targetCorrection = 1;

    void Start()
    {
        this.body = GetComponent<CharacterBody>();
    }

    void Update()
    {
        FaceCharacterToTarget();
        this.body.LeftArm.rotation = Quaternion.Euler(0, this.body.LeftArm.rotation.eulerAngles.y, CalculateAngleToTargetPoint());
    }

    protected void FaceCharacterToTarget() {
        bool isAimingLeft = GetDistanceToTarget().normalized.x < 0;
        
        body.Flip(isAimingLeft);
        targetCorrection = body.GetFrontSide();
    }

    protected abstract Vector3 GetDistanceToTarget();

    protected float CalculateAngleToTargetPoint() {
        var distance = GetDistanceToTarget().normalized;
        distance.x *= targetCorrection;

        return Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
    }
}

