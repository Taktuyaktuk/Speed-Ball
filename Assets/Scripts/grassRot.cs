using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class grassRot : MonoBehaviour
{
    Quaternion OrgRotation;
    Vector3 OrgPosition;
    public Controler1 controler;
   
    public Transform transformGrass;
    void Start()
    {
        OrgRotation = transform.rotation;
        OrgPosition = transform.parent.transform.position - transform.position;
        
    }

    private void Update()
    {
       // Direction();
    }

    void LateUpdate()
    {
        transform.rotation = OrgRotation;
        transform.position = transform.parent.position - OrgPosition;
        Direction();
    }

    void Direction()
    {


        if (controler.speed > 0)
        {
            
           
            transformGrass.Rotate(0f, 0f, 0);
            
        }
        if (controler.speed <0 )
        {
            
           
            transformGrass.Rotate(0f, 180f, 0f);
            
        }
    }
}





