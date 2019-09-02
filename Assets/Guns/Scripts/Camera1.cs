using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera1 : MonoBehaviour
{
    public Vector3 trans;
    public float speed;                //Floating point variable to store the player's movement speed.


    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.

    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void Update() 

    {

        // Store the current horizontal input in the float moveHorizontal.
        // float moveHorizontal = Input.GetAxis ("Horizontal");

        // Store the current vertical input in the float moveVertical.
        // float moveVertical = Input.GetAxis ("Vertical");

        // Use the two store floats to create a new Vector2 variable movement.
        //  trans = new Vector3 (moveHorizontal, moveVertical, speed)* Time.deltaTime;



    if(Input.GetKey(KeyCode.RightArrow))
     {
         transform.Translate(new Vector3(speed * Time.deltaTime,0,0));
     }
     if(Input.GetKey(KeyCode.LeftArrow))
     {
         transform.Translate(new Vector3(-speed * Time.deltaTime,0,0));
     }
     if(Input.GetKey(KeyCode.DownArrow))
     {
         transform.Translate(new Vector3(0,-speed * Time.deltaTime,0));
     }
     if(Input.GetKey(KeyCode.UpArrow))
     {
         transform.Translate(new Vector3(0,speed * Time.deltaTime,0));
     }
       
    }
}

