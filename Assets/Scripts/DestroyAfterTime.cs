using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
   
        public float lifeTime = 2;

        void Start()
        {
             Destroy(gameObject, lifeTime);
        }
}

