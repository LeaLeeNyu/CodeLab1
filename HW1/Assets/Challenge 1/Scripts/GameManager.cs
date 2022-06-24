using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static bool Button;
    public GameObject canvas;

   public void GameOver()
    {
       // Debug.Log("Game Over");
        canvas.SetActive(true);
    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        canvas.SetActive(false);
    }

}
