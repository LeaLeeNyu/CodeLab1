using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;


public class LevelLoader : MonoBehaviour
{
    public string fileName;

    public float xOffset; 
    public float zOffset;

    void Start()
    {
        StreamReader reader = new StreamReader(fileName); 
        string contentOfFile = reader.ReadToEnd(); 
        reader.Close(); 


        //split the text to various rows
        char[] newLineChar = { '\n' }; 
        string[] level = contentOfFile.Split(newLineChar); 

        //create the gameOnject in different rows
        for (int i = 0; i < level.Length; i++) 
        {
            MakeRow(level[i], -i); 
        }
        Hit();
    }




    //create the gameObject in a row, rowStr is the string, the z use for set the z position
    void MakeRow(string rowStr, float z) 
    {

        //transform the string to single character
        char[] rowArray = rowStr.ToCharArray(); 

        //
        for (int x = 0; x < rowStr.Length; x++) 
        {
            //use c to store current char value
            char c = rowArray[x];

            //if the character is B, create a bush
            if (c == 'B') 
            {
                int bushNum = Random.Range(0, 5);
                string bushName = "Bush_" + bushNum.ToString();            

                //create a bush from the resources folder
                GameObject bush = Instantiate(Resources.Load(bushName)) as GameObject;

                Debug.Log(bush.transform.localScale.x);

                bush.transform.position = new Vector3(
                    x * xOffset - 10f,
                    0,
                    z * zOffset
                    );
            }

            //if the character is M, create a mushroom
            else if (c == 'R') 
            {
                int rockNum = Random.Range(0, 2);
                string rockName = "Rock_" + rockNum.ToString();

                GameObject mushroom = Instantiate(Resources.Load(rockName)) as GameObject;
                //set the position of the new game object
                mushroom.transform.position = new Vector3(
                    x *  xOffset - 10f,
                    0,
                    z *  zOffset
                    );
            }

            //if the character is T, create a tree
            else if (c == 'S') 
            {
                int stumpNum = Random.Range(0, 1);
                string stumpName = "Stump_" + stumpNum.ToString();

                GameObject tree = Instantiate(Resources.Load(stumpName)) as GameObject;
                //set the position of the new game object
                tree.transform.position = new Vector3(
                    x * xOffset - 10f,
                    0,
                    z * zOffset
                    );
            }
            else if (c == 'T')
            {
                int treeNum = Random.Range(0, 1);
                string treeName = "Tree_" + treeNum.ToString();

                GameObject tree = Instantiate(Resources.Load(treeName)) as GameObject;
                //set the position of the new game object
                tree.transform.position = new Vector3(
                    x * xOffset - 10f,
                    0,
                    z * zOffset
                    );
            }
        }
    }


    public static void Hit()
    {
        //get the clone resources' transform
        GameObject[] resources = GameObject.FindGameObjectsWithTag("Resources");
        Transform[] transforms = new Transform[resources.Length];

        for (int i = 0; i < resources.Length; i++)
        {
            transforms[i] = resources[i].transform;
        }

        foreach (var transform in transforms)
        {
            //RaycastHit hitInfo;
            // set the raycast to detect the collider
            var hits = Physics.RaycastAll(transform.position + Vector3.up * 10, Vector3.down, 10f);

            foreach (var hit in hits)
            {
                //Debug.Log(hit.point);
                //if the raycast hit the gameObject itself, continue
                if (hit.collider.gameObject == transform.gameObject || hit.collider.gameObject.tag == "Resources")
                    continue;

                transform.position = hit.point;
                break;


            }


        }
    }


}
