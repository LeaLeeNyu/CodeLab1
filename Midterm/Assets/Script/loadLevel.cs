using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadLevel : MonoBehaviour
{
    private static int nextSceneToLoad;

    public int levelAdd;

    void Start()
    {
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + levelAdd; 
    }

    public static void CollideHouse()
    {
        SceneManager.LoadScene(nextSceneToLoad);
    }

}
