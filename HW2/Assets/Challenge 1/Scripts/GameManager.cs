using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static bool Button;
    public GameObject gameOver;
    public GameObject guide;

   public void GameOver()
    {
        //make gameover interface shows up and guide sentence disappear
        gameOver.SetActive(true);
        guide.SetActive(false);
    }


    public void Restart()
    {
        //reset the score and scene
        ScoreManager.score = 0;

        SceneManager.LoadScene("Game");
        gameOver.SetActive(false);
        guide.SetActive(true);
    }

    public void BacktoManu()
    {
        SceneManager.LoadScene("Menu");
        ManuManager.gameIsLoad = false;
    }

}
