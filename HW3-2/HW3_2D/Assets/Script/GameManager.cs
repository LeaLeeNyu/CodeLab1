using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{

    private PlayerController playerController;
    private GameObject player;

    private SaveCherry saveCherry;

    private GameObject cherry;
    private PolygonCollider2D[] cherryColliders;
    private GameObject[] cherryGameObjects;
    private bool[] cherryStatement;

    private void Awake()
    {
        //find platerController script on player 
        playerController = GameObject.Find("player").GetComponent<PlayerController>();
        player = GameObject.Find("player");

        saveCherry = GameObject.Find("cherry").GetComponent<SaveCherry>();

        cherry = GameObject.Find("cherry");

        cherryColliders = cherry.GetComponentsInChildren<PolygonCollider2D>(true);
        

    }

    public void SavePlayer()
    {
        //save data
        SaveSystem.SavePlayer(playerController, saveCherry);

        //Aviod the save button Onclick by space button
        EventSystem.current.SetSelectedGameObject(null);

    }

    public void loadPlayer()
    {
        //call the loadSystem function
        PlayerData data = SaveSystem.LoadData();

        // get the saved score
        playerController.score = data.score;

        // get the saved position
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        player.transform.position = position;

        cherryStatement = data.cherryS;

        //get cherry statement
        for (int i = 0; i < saveCherry.cherry.Length; i++)
        {
            cherryStatement[i] = data.cherryS[i];
            cherryColliders[i].gameObject.SetActive(cherryStatement[i]);

        }

        //Aviod the load button Onclick by space button
        EventSystem.current.SetSelectedGameObject(null);

    }
}
