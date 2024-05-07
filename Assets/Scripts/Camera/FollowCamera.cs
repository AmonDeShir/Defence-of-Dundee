using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine; 

public class FollowCamera : MonoBehaviour
{
    public CharacterBody Target;

    public float Speed;
    private Vector3 margin;

    void Start()
    {
        margin = transform.position - Target.transform.position;
    }

    void Update()
    {
        var offset = new Vector3(margin.x , margin.y, margin.z);
        var target = Target.transform.position + offset;

        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * Speed);
    }

    public void TeleportCameraToTarget() {
        var delta = Vector3.Normalize(Target.transform.position - transform.position);
        var animationMargin = new Vector3(-delta.x * 10, -delta.y * 10, 0);

        transform.position = Target.transform.position + animationMargin;
    }
}
