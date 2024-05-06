using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(RandomBackground))]
public class ParallaxBackground : MonoBehaviour
{
    public Transform Camera;
    private Vector3 lastCameraPos;

    public Vector2[] layersSpeed;

    private RandomBackground background;

    // Start is called before the first frame update
    void Start()
    {
        lastCameraPos = Camera.position;
        background = GetComponent<RandomBackground>();
    }

    void LateUpdate()
    {
        var delta = Camera.position - lastCameraPos;
        lastCameraPos = Camera.position;

        for (int i = 0; i < this.transform.childCount; i++) {
            var layer = this.transform.GetChild(i);
            layer.transform.position += new Vector3(delta.x * layersSpeed[i].x, delta.y * layersSpeed[i].y, 0);

            if (Camera.position.x - layer.transform.position.x > background.GetLayerSize(i) / 4) {
                background.AddNew(i);
            }
        
        }
    }
}
