using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Be able to save this as a file
[System.Serializable]
public class PlayerData
{
    public int score;
    public float[] position;
   // public bool[] cherryS; 

    public PlayerData(PlayerController playerController)
    {
        // To get the score in PlayerController script, the data type of score in playercontroller could not be static -- why? 
        score = playerController.score;

        position = new float[3];
        position[0] = playerController.transform.position.x;
        position[1] = playerController.transform.position.y;
        position[2] = playerController.transform.position.z;

       // cherryS = saveCherry.cherryState;

    }



}
