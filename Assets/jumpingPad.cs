using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpingPad : MonoBehaviour
{
   
    public float jumpForcePower;

    public void OnCollisionEnter2D(Collider2D other)
    {
        
            other.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForcePower);
        
    }
}
