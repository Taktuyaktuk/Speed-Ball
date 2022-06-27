using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform1 : MonoBehaviour
{

    public Transform pos1, pos2;
    public float platformSpeed;
    public Transform starPos;

    Vector3 nextPos;
    // Start is called before the first frame update
    void Start()
    {
        nextPos = starPos.position;
    }

    // Update is called once per frame
    void Update()
    {
       if(transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }
       if(transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, platformSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
