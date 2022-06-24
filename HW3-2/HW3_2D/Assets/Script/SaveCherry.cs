using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCherry : MonoBehaviour
{

    public bool[] cherryState = new bool[6];

    //public GameObject cherryParent;
    public Component[] cherry;

    // Start is called before the first frame update
    void Awake()
    {
       cherry = GetComponentsInChildren<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {   
            for (int i = 0; i < cherryState.Length; i++)
            {
               cherryState[i] = cherry[i].gameObject.activeSelf;
               // Debug.Log(cherry.Length.ToString());
            }
    }
}
