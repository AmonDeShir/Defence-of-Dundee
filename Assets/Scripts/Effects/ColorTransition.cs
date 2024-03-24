using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTransition : MonoBehaviour
{
    public Color Target;
    public float Speed = 1;

    private SpriteRenderer sprite;
    private Color start; 
    private int direction = 1;
    private float progress = 0;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        start = sprite.color;
    }

    void Update()
    {
        progress = Mathf.Clamp01(progress + (Speed * direction * Time.deltaTime));
        sprite.color = Color.Lerp(start, Target, progress);

        if (progress >= 1.0f) {
            direction = -1;
        }

        else if (progress <= 0f) {
            direction = 1;
        }
    }
}
