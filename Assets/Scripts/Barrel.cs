using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{

    Controler1 controler1;
    public Transform launchDirection;
    public float launchForce = 400f;
    // Start is called before the first frame update
    void Start()
    {
        controler1 = GameObject.FindObjectOfType<Controler1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controler1.barrelJump == true && controler1.inBarrel == true)
        {
            LaunchPlayer();
        }
    
       //if (Input.GetKeyDown (KeyCode.Space) || Input.GetTouch(0))
       // {
       //   LaunchPlayer();
       //}
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Controler1>())
        {
            controler1.EnterBarrel(gameObject);
        }
    }

    void LaunchPlayer() 
    {
        float launchDirX = launchDirection.position.x;
        float launchDirY = launchDirection.position.y;
        float posX = transform.position.x;
        float posY = transform.position.y;

        // Launch direction is the difference between our destination and our current position
        Vector2 launchDir = new Vector2(launchDirX - posX, launchDirY - posY);
        controler1.ExitBarrel (launchDir, launchForce);
    }
}
