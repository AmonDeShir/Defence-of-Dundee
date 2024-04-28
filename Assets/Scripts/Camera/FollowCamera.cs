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
        var offset = new Vector3(margin.x * Target.GetFrontSide(), margin.y, margin.z);
        var target = Target.transform.position + offset;

        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * Speed);
    }
}
