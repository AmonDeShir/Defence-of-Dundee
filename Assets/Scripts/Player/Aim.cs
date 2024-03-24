using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    private CharacterBody body;
    public GameObject Target;

    void Start()
    {
        this.body = new CharacterBody(this.transform);
    }

    void Update()
    {
        Vector3 targetPosition = (Target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;

        this.body.LeftArm.rotation = Quaternion.Euler(0, 0, angle);
    }
}

