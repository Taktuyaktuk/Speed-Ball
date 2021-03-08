using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Controler1 : MonoBehaviour
{

    private Vector2 startPos, stopPos;

    Rigidbody2D rigidbody;

    public float tapPower = 10;
    public float jumpPower = 10;

    public float maxSwipetime;
    public float minSwipeDistance;

    private float swipeStartTime;
    private float swipeEndTime;
    private float swipeTime;


    private Vector2 startSwipePosition;
    private Vector2 swipeEndPosition;
    private float swipeLength;

    float dir = 1;
    bool facingRight = true;
    
    bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public AudioSource jumpSound;

    private Touch touch1;

    private Vector2 beginTouchPosition, endTouchPosition;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        //{
        //    startPos = Input.GetTouch(0).position;
        //}
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        //{
        //    stopPos = Input.GetTouch(0).position;
        //    if((stopPos.x < startPos.x) && transform.position.x > -2)
        //    {
        //        rigidbody.AddForce(Vector2.left * tapPower, ForceMode2D.Force);

        //    }
        //    if ((stopPos.x > startPos.x) && transform.position.x < 2)
        //    {
        //        rigidbody.AddForce(Vector2.right * tapPower, ForceMode2D.Force);
        //    }  
        //}
        PlayerController();
        SwipeTest();

        if (Input.touchCount >0)
        {
            touch1 = Input.GetTouch(0);

            switch (touch1.phase)
            {
                case TouchPhase.Began:
                    beginTouchPosition = touch1.position;
                    break;

                case TouchPhase.Ended:

                    endTouchPosition = touch1.position;

                    if(beginTouchPosition == endTouchPosition)
                    {
                        if (isGrounded)
                        {
                            jumpSound.Play();
                            rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
                        }
                    }
                    break;
            }

            //jumpSound.Play();
            //rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    void PlayerController()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startPos = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopPos = Input.GetTouch(0).position;
            if ((stopPos.x < startPos.x) && facingRight == false)
            {
                rigidbody.AddForce(Vector2.left * tapPower, ForceMode2D.Force);
                FlipAndMove();

            }
            if ((stopPos.x > startPos.x) && facingRight == true)
            {
                rigidbody.AddForce(Vector2.right * tapPower, ForceMode2D.Force);
                FlipAndMove();
            }
        }
    }


    void SwipeTest()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                swipeStartTime = Time.time;
                startSwipePosition = touch.position;
            }

            else if (touch.phase == TouchPhase.Ended)
            {
                swipeEndTime = Time.deltaTime;
                swipeEndPosition = touch.position;
                swipeTime = swipeEndTime - swipeStartTime;
                swipeLength = (swipeEndPosition - startSwipePosition).magnitude;
                if(swipeTime<maxSwipetime && swipeLength > minSwipeDistance)
                {
                    swipeControl();
                }
            }
        }
    }
    void swipeControl()
    {
        Vector2 Distance = swipeEndPosition - startSwipePosition;
        float xDistance = Math.Abs(Distance.x);
        float yDistance = Math.Abs(Distance.y);
        if(xDistance > yDistance)
        {
            if (Distance.x > 0 && !facingRight)
            {
                FlipAndMove();
            }
            else if(Distance.x<0 && facingRight)
            {
                FlipAndMove();
            }
            //else if(yDistance > xDistance)
            //{
            //    if(Distance.y>0)
            //    {
            //        jumpSound.Play();
            //        rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Force); 
            //    }
            //}
        }

    }
    void FlipAndMove()
    {
        dir = -dir;
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("JumpPad"))
        {
            rigidbody.velocity = Vector2.up * 15f;
        }
    }
}
