using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //heart container
    public int heartNum = 5;
    //current health value
    public int health = 5;

    //assign the hearts image in inspector
    public Image[] hearts;
    public Color heartFull;
    public Color heartTrans;

    private float hearyAlpha = 155;

    private void Start()
    {
        changeHealth();
    }

    public void changeHealth()
    {
        //For the all the heart container
        for (int i = 0; i< hearts.Length; i++)
        {
            //
            if (i < health)
            {
                hearts[i].color = heartFull;
            }
            else
            {
                hearts[i].color = heartTrans;
            }
        }
    }

}
