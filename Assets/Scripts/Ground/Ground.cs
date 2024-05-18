using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    void Start()
    {
    
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var contact = collision.contacts[0];

        if (contact.normal.y == 0) {
            collision.rigidbody.velocity /= 4;
            collision.rigidbody.AddForce(contact.normal * 10000);
        } 
    }
}
