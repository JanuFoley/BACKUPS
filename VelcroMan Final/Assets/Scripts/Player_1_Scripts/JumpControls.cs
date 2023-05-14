using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpControls : MonoBehaviour
{
    public float torsoForce = 300f;
    public KeyCode torsoForceButton = KeyCode.Space;
    public Rigidbody2D torsoRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        // This is used to find the Torso's Rigidbody2D and set it
        // to a variable 
        torsoRigidbody = GameObject.Find("Torso").GetComponent<Rigidbody2D>();

    }

    void ApplyTorsoForce()
    {
        // This method is used to addd force to the torso in the direction of
        // the mouse by using the ScreenToWorldPoint feature 
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - torsoRigidbody.transform.position;
        direction.z = 0f;
        direction.Normalize();
        torsoRigidbody.AddForce(direction * torsoForce, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        // This if statment is used to activeate the ApplyTorsoForce
        // method when the torsoForceButton is pressed 
        if (Input.GetKeyDown(torsoForceButton))
        {
            ApplyTorsoForce();
        }
    }
}
