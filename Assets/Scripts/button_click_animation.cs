using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_click_animation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void button_click(){
	GetComponent<Animation>().Play("button_click");
}
}
