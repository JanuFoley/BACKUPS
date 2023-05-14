using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightArmManager : MonoBehaviour
{
    public float speed = 100f;
    public GameManager controlSwitcher;
    Vector2 moveDirection;
    public bool isSticking = false;
    public KeyCode unstickyKey = KeyCode.Space;
    public Rigidbody2D handrigidBody;
    private Vector2 savedVelocity;
    private float cooldownTimer = 0f;
    private Vector3 screenPoint;
    private Vector3 offset;
    public Rigidbody2D rigidbody2D_2;

    // Start is called before the first frame update
    void Start()
    {
        // This sets the controlSwitcher to find the GameManger
        controlSwitcher = FindObjectOfType<GameManager>();

        // This finds the objects Rigidbody2D and sets it to a variable 
        rigidbody2D_2 = GetComponent<Rigidbody2D>();

        // This finds the objects Rigidbody
        handrigidBody = GetComponent<Rigidbody2D>();
    }

    // This method is used when the left mouse button is pressed down
    private void OnMouseDown()
    {
        // This if statment is called if isSticking is true it
        // then sets isSticking to false, lowers the StuckHandsFeet
        // bool and sets thwe rest of the variables to fasle or its old variables
        if (isSticking)
        {
            isSticking = false;
            controlSwitcher.DecrementStuckHandsFeet();
            handrigidBody.isKinematic = false;
            handrigidBody.velocity = savedVelocity;
            handrigidBody.freezeRotation = false;
            cooldownTimer = 0.5f;
        }

        // This else stament is used to set isSticking to false
        // and finds the mouse position on the
        // screen and saves it to a variable
        else
        {
            isSticking = false;
            screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }

    // This method is used to see what the numStuck
    // value is and then use that to move the object in
    // the direction of the mouse with that given force 
    void OnMouseDrag()
    {
        // This is used to find the numStuck value
        // and then assigns it to a certain preset speed
        int numStuck = controlSwitcher.GetNumStuckHandsFeet();

        if (numStuck == 0) speed = 80f;
        else if (numStuck == 1) speed = 100f;
        else if (numStuck == 2) speed = 150f;
        else if (numStuck == 3) speed = 250f;
        else if (numStuck == 4) speed = 350f;

        // This is used to find the direction of the
        // object and then move it towards the
        // mouse position with the preset speed 
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.z = 0f;
        direction.Normalize();

        GetComponent<Rigidbody2D>().AddForce(direction * speed);

    }

    // Update is called once per frame
    private void Update()
    {
        // This is used to consistanly update the
        // numStuck value and keep track of it 
        int numStuck = controlSwitcher.GetNumStuckHandsFeet();

        if (numStuck == 0) speed = 80f;
        else if (numStuck == 1) speed = 100f;
        else if (numStuck == 2) speed = 150f;
        else if (numStuck == 3) speed = 200f;
        else if (numStuck == 4) speed = 250f;

        // This code is used to ustick the hand/foot from the object
        if (Input.GetKeyDown(unstickyKey))
        {
            isSticking = false;
            controlSwitcher.DecrementStuckHandsFeet();
            handrigidBody.isKinematic = false;
            handrigidBody.velocity = savedVelocity;
            handrigidBody.freezeRotation = false;
            cooldownTimer = 0.5f;
        }

        // This code is used to keep track of what the cooldownTimer
        // is and if its higher than 0 then the hands/feet cant stick 
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime; // decrease cooldown timer
            if (cooldownTimer <= 0f)
            {
                cooldownTimer = 0f;
            }
        }
    }

    // This method is for if the object collides with another objects
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        // This code is used to stick the hand/foot to an object 
        // If the colllision is with an object with the tag TileMap and if the cooldownTimer is 0
        if (collision2D.gameObject.tag == "TileMap" && cooldownTimer <= 0f)
        {
            isSticking = true;
            controlSwitcher.IncrementStuckHandsFeet();
            savedVelocity = handrigidBody.velocity;
            handrigidBody.isKinematic = true;
            handrigidBody.velocity = Vector2.zero;
            handrigidBody.freezeRotation = true;
        }
    }
}
