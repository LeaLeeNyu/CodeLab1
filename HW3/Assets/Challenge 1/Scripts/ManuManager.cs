using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManuManager : MonoBehaviour
{
    private static ManuManager manu;

    public static bool gameIsLoad = false;
    
    public static ManuManager FindInstance()
    {
        return manu;
    }

    private void Awake()
    {
        if (manu != null && manu != this)
        {
            Destroy(this);
        }
        else 
        {
            //make this the king game manager
            manu = this;
            //Dont destroy the scene when load another scene
            DontDestroyOnLoad(gameObject);
        }

    }

    void Update()
    {
        //if player press enter & game scene have not be loaded 
        if (Input.GetKeyDown(KeyCode.Return) && !gameIsLoad) 
        {
            //Reset the game score
            ScoreManager.score = 0;

            //load the game scene
            SceneManager.LoadScene("Game");
            gameIsLoad = true;
        }

    }




}
