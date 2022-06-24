using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public float playerForceY;

    public Camera mainCamera;
    public Camera hoodCamera;
    public KeyCode changeCam;

    public Rigidbody plane;

    public bool gameOver = false;

    public GameManager gameManager;

    void Start()
    {
       
    }

    void Update()
    {
        // tilt the plane up based on space key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            plane.velocity = Vector3.up * playerForceY;
        }
       
        // change to different camera
        if (Input.GetKeyDown(changeCam))
        {
            Debug.Log("PRESS!");
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
        }

        //game over if plane out of the boundary
        if (transform.position.y < -110f & !gameOver)
        {
            gameOver = true;
            FindObjectOfType<GameManager>().GameOver();
           // transform.position = new Vector3(transform.position.x, -110f, transform.position.z);
        }
        

    }

    //If plane collide with others, end and restart the game
    private void OnCollisionEnter(Collision collision)
    {
        gameOver = true;
        FindObjectOfType<GameManager>().GameOver();
       // Debug.Log("GAME OVER!");
    }

}
