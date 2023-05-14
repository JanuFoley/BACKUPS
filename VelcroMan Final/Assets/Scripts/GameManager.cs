using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button stopButton;
    public float fallHeight = -5f;
    public bool usingHands = true;
    private int numStuckHandsFeet = 0;
    void Start()
    {
        // This is used to reset and base the numstuck value so it is
        // always 0 at the begingin of the game 
        numStuckHandsFeet = 0;       
    }

    // Update is called once per frame
    void Update()
    {
        // This if statment sets he using hands bool to false and then back
        // to true if its triggered by space bar again
        if (Input.GetKeyDown(KeyCode.Space))
        {
            usingHands = !usingHands;
        }

        // This is used to cheack if the torsos position is less than the
        // "fallHeight" and if it is then it reloads the scene, aka you have
        // to restart the game 
        if (transform.position.y < fallHeight)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Stop()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // This public bool is used in the other scripts that need to have access
    // to this script and it gives them the imformation of whether usingHands is 
    //true or false
    public bool IsUsingHands()
    {
        return usingHands;
    }

    // This is used to add value to the numstuck value and is called when
    // hands or feet come in contact with the ground 
    public void IncrementStuckHandsFeet()
    {
        numStuckHandsFeet++;

    }

    // This is the opposite to the other method as
    // this one decreases the value when the hands and feet lose contact
    // with the ground 
    public void DecrementStuckHandsFeet()
    {

        numStuckHandsFeet = Mathf.Max(numStuckHandsFeet - 1, 0);

    }

    // This method is very sinple as it returns the numstuck value back
    // to the script which alows the player movment scripts to know what
    // the value of numstuck is 
    public int GetNumStuckHandsFeet()
    {
        return numStuckHandsFeet;
    }
}
