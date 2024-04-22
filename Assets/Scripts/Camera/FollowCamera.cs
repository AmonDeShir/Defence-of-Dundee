using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine; 

public class FollowCamera : MonoBehaviour
{
    public GameObject Target;
    public float Speed;
    private Vector3 margin;

    // Start is called before the first frame update
    void Start()
    {
        margin = transform.position - Target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var target = Target.transform.position + margin;

        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * Speed);
    }
}
