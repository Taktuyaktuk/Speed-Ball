using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controler1 : MonoBehaviour
{

    private Vector2 startPos, stopPos;

    Rigidbody2D rigidbody;

    public float tapPower = 10;
    public float jumpPower = 10;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startPos = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopPos = Input.GetTouch(0).position;
            if((stopPos.x < startPos.x) && transform.position.x > -2)
            {
                rigidbody.AddForce(Vector2.left * tapPower, ForceMode2D.Force);
                Debug.Log("lalala");
            }
            if ((stopPos.x > startPos.x) && transform.position.x < -2)
            {
                rigidbody.AddForce(Vector2.right * tapPower, ForceMode2D.Force);
                Debug.Log("sralala");
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Force); 
        }
    }
    
}
