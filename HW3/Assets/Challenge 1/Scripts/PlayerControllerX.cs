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

    

    public GameManager gameManager;

    void Start()
    {
        GameManager.gameOverBool = false;
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
           // Debug.Log("PRESS!");
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
        }

        //game over if plane out of the boundary
        if (transform.position.y < -110f & !GameManager.gameOverBool)
        {
            FindObjectOfType<GameManager>().GameOver();
           // transform.position = new Vector3(transform.position.x, -110f, transform.position.z);
        }
        

    }

    //If plane collide with others, end and restart the game
    private void OnCollisionEnter(Collision collision)
    {
        FindObjectOfType<GameManager>().GameOver();

        //compare the score with HighScore
        if (ScoreManager.score > PlayerPrefs.GetInt("HighScore"))
        {
            //Use playerprefs to store the HighScore
            PlayerPrefs.SetInt("HighScore", ScoreManager.score);
        }
    }

}
