using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{

    public float speed = 5.0f;
    public PlayerControllerX playerControllerX;
    public bool isCross = false;


    // Start is called before the first frame update
    void Start()
    {
        playerControllerX = GameObject.Find("Player").GetComponent<PlayerControllerX>();
    }

    // Update is called once per frame
    void Update()
    {
        // Only move the collider when the game is not over
        if(playerControllerX.gameOver == false)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);


            // when collider move to the left of plane, add score
            if (transform.position.z < -66.7f && !isCross)
            {
                 FindObjectOfType<ScoreManager>().score += 1;
                 Debug.Log("Cross!");
                isCross = true;
            }

            // if collider move out of the left boundary, destroy the collider
            if (transform.position.z < -130.0f)
            {
                Destroy(gameObject);
            }


        }
        
    }
}
