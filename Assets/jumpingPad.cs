using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpingPad : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpForcePower;

    public void OnCollisionEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            rb.velocity = Vector2.up * jumpForcePower;
        }
    }
}
