using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstacle;
    private Vector3 obstaclePos;
    private int obstacleRandomY;

    public PlayerControllerX playerControllerX;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerX = GameObject.Find("Player").GetComponent<PlayerControllerX>();
        InvokeRepeating("SpawnCollider", 2.0f, 1.3f);
    }

    // Update is called once per frame
    void SpawnCollider()
    {
        // Only spawn the collider when game is not over
        if (playerControllerX.gameOver == false)
        {
            // Randomly spawn the collider at different y position
            obstacleRandomY = Random.Range(-20, 20);
            obstaclePos = new Vector3(0, obstacleRandomY, 25);

            Instantiate(obstacle, obstaclePos, obstacle.transform.rotation);
        }
        
    }
}
