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

    public static bool gameOverBool = false;

    public void GameOver()
    {
        //make gameover interface shows up and guide sentence disappear
        gameOver.SetActive(true);
        guide.SetActive(false);

        gameOverBool = true;
    }


    public void Restart()
    {
        //reset the score and scene
        ScoreManager.score = 0;
        gameOverBool = false;

        SceneManager.LoadScene("Game");
        gameOver.SetActive(false);
        guide.SetActive(true);
    }

    public void BacktoManu()
    {
        SceneManager.LoadScene("Menu");
        gameOverBool = false;
        ManuManager.gameIsLoad = false;
    }

}
