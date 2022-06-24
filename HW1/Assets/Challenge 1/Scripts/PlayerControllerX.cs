using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public float playerForceZ;
    public float playerForceY;

    public Camera mainCamera;
    public Camera hoodCamera;
    public KeyCode changeCam;

    public Rigidbody plane;

    public bool gameOver = false;

    public GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // tilt the plane up based on space key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            plane.velocity = Vector3.up * playerForceY;
           // transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        }
       
        if (Input.GetKeyDown(changeCam))
        {
            Debug.Log("PRESS!");
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
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
