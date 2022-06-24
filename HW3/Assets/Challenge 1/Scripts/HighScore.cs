using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{

    public Text highScoretxt;

    // Start is called before the first frame update
    void Awake()
    {
     // Get HighScore date 
      highScoretxt.text = PlayerPrefs.GetInt("HighScore",0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
     //set the highscore; 
     highScoretxt.text = PlayerPrefs.GetInt("HighScore").ToString();

    }
        
        

    public void Reset()
    {
       PlayerPrefs.DeleteKey("HighScore");      
    }


}

