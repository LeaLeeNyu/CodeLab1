using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public float amplitude;
    public float frequncy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = transform.position.x;
        float y = Mathf.Sin(Time.time* frequncy) *amplitude;
        float z = transform.position.x;

        transform.position = new Vector3(x,y,z);
    }
}
