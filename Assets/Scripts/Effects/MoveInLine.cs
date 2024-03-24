using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInLine : MonoBehaviour
{
    public GameObject FinalPos;
    public float Speed = 1f;
    private Vector3 startPos;
    private int direction = 1;
    private float progress = 0f;


    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        progress = Mathf.Clamp01(progress + (Speed * direction * Time.deltaTime));

        transform.position = Vector3.Lerp(startPos, FinalPos.transform.position, progress);

        if (progress >= 1.0f) {
            direction = -1;
        }

        else if (progress <= 0f) {
            direction = 1;
        }
    }
}
