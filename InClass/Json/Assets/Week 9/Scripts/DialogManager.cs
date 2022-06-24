using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using System.IO;

public class DialogManager : MonoBehaviour
{

    public int score = 0;

    public Text scoreText;

    public Text dialogText;

    public Text choice1Text;
    public Text choice2Text;

    public GameObject choice1Button;
    public GameObject choice2Button;

    public GameObject continueButton;

    public string dialogFile;

    public string storyScene;

    public int storyIndex;

    JSONObject storyData;
    string storyText;


    // Start is called before the first frame update
    void Start()
    {
        choice1Button.SetActive(false);
        choice2Button.SetActive(false);

        scoreText.text = "Points: " + score;

        LoadJSONFormFile(dialogFile);
        StartaScene();

    }

    void LoadJSONFormFile(string fileName)
    {
        string jsonString = File.ReadAllText(fileName);
        //() helps trun the data type
        storyData = (JSONObject)JSON.Parse(jsonString);
    }

    void StartaScene()
    {
        dialogText.text = storyData[storyScene][storyIndex]["line"];
    }


    void ChoiceDialog(){
            continueButton.SetActive(false);
            choice1Button.SetActive(true);
            choice2Button.SetActive(true);

        //make the choice texts show on the button
            choice1Text.text = storyData[storyScene][storyIndex]["choice"][0];
            choice2Text.text = storyData[storyScene][storyIndex]["choice"][1];

        choice1Button.GetComponent<Buttons>().nextIndex = storyData[storyScene][storyIndex]["goto"][0];
        choice2Button.GetComponent<Buttons>().nextIndex = storyData[storyScene][storyIndex]["goto"][1];
    }

    void RegularDialog(){
            continueButton.SetActive(true);
            choice1Button.SetActive(false);
            choice2Button.SetActive(false);
            scoreText.text = "Points: " + score;
        //StartCoroutine(JSONServer.PostScore(scoreURL, newScore));
           continueButton.GetComponent<Buttons>().nextIndex = storyData[storyScene][storyIndex]["goto"];
    }

    public void ProgressStory()
    {
        dialogText.text = storyData[storyScene][storyIndex]["line"];

        // if there is a choice in the JASONObject, let the player choice the dialog
        if (storyData[storyScene][storyIndex]["choice"].Count > 0)
        {
            ChoiceDialog();
        }
        else
        {
            RegularDialog();
            Debug.Log("Regular");
        }
    }
}
