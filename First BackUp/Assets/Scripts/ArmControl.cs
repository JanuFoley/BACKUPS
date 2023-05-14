using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmControl : MonoBehaviour
{
    public float movespeed = 5f;
    private Rigidbody2D objectRigidBody;
    Vector2 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
         // This finds and gets the Rigidbody Component of the object it is attached to 
        objectRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // These two lines of code set the two variables moveX and move Y to the preset controls 
        // unity has for the X and Y axis which are up, down, left and right   
        float moveX = Input.GetAxisRaw("HorizontalArrowKeys");      
        float moveY = Input.GetAxisRaw("VerticalArrowKeys");
       
        // This code sets a Vector2 called movment to a new Vector2 wich is the data from The inputs 
        // of moveX and moveY and then normalizes them
        Vector2 movement = new Vector2(moveX, moveY).normalized;
        
        // This code Gets the objectRigidBody and adds force to it by multiplying the movement 
        // variable and the preset movespeed, it then uses a ForcMode2D to apply this Force to the 
        //Rigid Body 
        objectRigidBody.AddForce(movement * movespeed, ForceMode2D.Force);
    }
}
