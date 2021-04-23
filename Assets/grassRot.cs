using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grassRot : MonoBehaviour
{
    Quaternion rotation;
    Rigidbody2D rb;

    private void Awake()
    {
        rotation = transform.rotation;
    }

    private void LateUpdate()
    {
        transform.rotation = rotation;
        //transform.position = transform.localPosition;
        //transform.localPosition = new Vector3(0, -2, 0);
    }
}


