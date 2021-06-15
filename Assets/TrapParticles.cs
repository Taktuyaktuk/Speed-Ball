using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapParticles : MonoBehaviour
{
    public GameObject ParticlesEffect;
    public Transform SpawnPosition;

     void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Dead"))
        {
            Instantiate(ParticlesEffect, SpawnPosition);
            Debug.Log("kolizja");
            
        }
    }

}
