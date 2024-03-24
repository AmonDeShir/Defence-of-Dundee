using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float RotationSpeed = 10;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, RotationSpeed * Time.deltaTime));
    }
}
