using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Controler1 : MonoBehaviour
{

    private Vector2 startPos, stopPos;

    
    private Rigidbody2D rigidbody;
    private SpriteRenderer sprite;
    public bool barrelJump = false;
    public bool inBarrel = false;
    public bool oneJump;
    public bool isStoped;
    
    

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

    public float speed;
    public float maxSpeed;
    public float maxSpeedMinus;

    public float nextSlowTime = 2;
    private float slowTime;

    public bool rightDirection = false;

    public GameObject grassActivator;
    public GameObject player;

    //zaczyna sie robi? burdel... ponizej skok w powietrzu przy swipe
    float touchTimeStart, touchTimeFinish, timeInterval;
    Vector2 direction;
    
    private void Awake()
    {
        barrelJump = false;
        inBarrel = false;
        
    }
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        PlayerController();
        SwipeTest();
        RotationSlowing();
        _grassActivator();
        jump1();
        noDoubleJump();
        
        
        
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 1.3f, groundLayer);
        rigidbody.AddTorque(speed, ForceMode2D.Force);

        

    }
  

   public void PlayerController()
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
                //rigidbody.AddForce(Vector2.left * tapPower, ForceMode2D.Force);
                //if (speed <= 0)
                //{
                //    speed = 0;
                //    var brakePosition = rigidbody.position;
                //    rigidbody.transform.position = brakePosition;
                //}
                if (speed < maxSpeed && speed >= 0)
                {
                    speed += 1;
                    rightDirection = true;
                    Debug.Log(speed);
                }
                
                else if (rigidbody.angularVelocity < 0 && isGrounded)
                {
                    rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
                    rigidbody.constraints = RigidbodyConstraints2D.None;
                    speed = 0;
                }

                    FlipAndMove();

            }
            if ((stopPos.x > startPos.x) && facingRight == true)
            {
                
                if ( speed > maxSpeedMinus && speed <= 0)
                {
                    speed -= 1;
                    rightDirection = false;
                    Debug.Log(speed);
                }
              
                else if (rigidbody.angularVelocity > 0 && isGrounded)
                {
                    rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
                    rigidbody.constraints = RigidbodyConstraints2D.None;
                    speed = 0;
                }

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
            rigidbody.AddForce( Vector2.up * 100f);
        }
        else if( other.gameObject.CompareTag("RightSpeedPad"))
        {
            rigidbody.AddForce(Vector2.right * 500f);
        }
        else if (other.gameObject.CompareTag("LeftSpeedPad"))
        {
            rigidbody.AddForce(Vector2.left * 500f);
        }
    }
    public void EnterBarrel(GameObject Barrel)
    {
        
        rigidbody.velocity = new Vector2 (0,0);
        rigidbody.isKinematic = true;
        sprite.enabled = false;
        transform.parent = Barrel.transform;
        inBarrel = true;
        
    }

    public void ExitBarrel (Vector2 launchDir, float launchForce)
    {
        transform.parent = null;
        rigidbody.isKinematic = false;
        sprite.enabled = true;
        rigidbody.AddForce(launchDir * launchForce);
        barrelJump = false;
        inBarrel = false;
    }
    public void RotationSlowing()
    {
        slowTime += Time.deltaTime;

        if ( speed >=1  && speed <= 11 && slowTime >= nextSlowTime)
        {

            slowTime = 0;
            speed -= 1;
        }

        else if(speed <= -1 && speed >= -11 && slowTime >= nextSlowTime)
        {
            slowTime = 0;
            speed += 1;
        }
    }

    public void _grassActivator()
    {
        if(speed <0 || speed >0)
        {
            grassActivator.SetActive(true);
        }
        else
        {
            grassActivator.SetActive(false);
        }
    }
        public void jump1 ()
    {
        if (Input.touchCount > 0)
        {
            touch1 = Input.GetTouch(0);

            switch (touch1.phase)
            {
                case TouchPhase.Began:
                    beginTouchPosition = touch1.position;
                    touchTimeStart = Time.time;

                    break;

                case TouchPhase.Ended:

                    endTouchPosition = touch1.position;
                    touchTimeFinish = Time.time;

                    if (beginTouchPosition == endTouchPosition)
                    {

                        if (isGrounded)
                        {
                            jumpSound.Play();
                            rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
                        }
                        else if (inBarrel == true && isGrounded == false)
                        {
                            barrelJump = true;
                        }
                        //else if(!isGrounded)
                        //{
                        //    var touchPosJump = Camera.main.ScreenToWorldPoint(touch1.position);
                        //    var touchDir = touchPosJump - gameObject.transform.position;
                        //    touchDir.z = 0.0f;
                        //    touchDir = touchDir.normalized;
                        //    jumpSound.Play();
                        //    rigidbody.AddForce(touchDir * tapPower);
                        //}
                    }
                    else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !isGrounded && oneJump == true)
                    {
                        //touchTimeFinish = Time.deltaTime;
                        timeInterval = touchTimeFinish - touchTimeStart;
                        direction = beginTouchPosition - endTouchPosition;
                        rigidbody.AddForce(-direction * tapPower);
                        oneJump = false;

                    }
                    break;
            }

            
        }

        
    }

    public void jump2()
    {
        if (Input.touchCount >0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            beginTouchPosition = Input.GetTouch(0).position;
            touchTimeStart = Time.time;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;
            touchTimeFinish = Time.time;

            if (beginTouchPosition == endTouchPosition)
            {

                if (isGrounded)
                {
                    jumpSound.Play();
                    rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
                }
                else if (inBarrel == true && isGrounded == false)
                {
                    barrelJump = true;
                }
                
            }

            if(!isGrounded)
            {
                timeInterval = touchTimeFinish - touchTimeStart;
                endTouchPosition = Input.GetTouch(0).position;
                direction = beginTouchPosition - endTouchPosition;
                rigidbody.AddForce(-direction * tapPower);
            }
        }
    }

    public void noDoubleJump()
    {
        if(isGrounded == true)
        {
            oneJump = true;
        }
    }

   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            
        }

        if (other.gameObject.CompareTag("Platform"))
        {
            player.transform.SetParent(other.transform);
            
           
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Platform"))
        {
            player.transform.SetParent(null);
           
        }
    }

    

}
