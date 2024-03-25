using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AimMouse : Aim
{
    protected override Vector3 GetDistanceToTarget() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }
}