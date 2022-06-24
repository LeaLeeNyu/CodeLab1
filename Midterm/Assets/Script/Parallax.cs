using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cam;
    public float rate;

    private float startPoint;


    void Start()
    {
        startPoint = transform.position.x;
    }

    void Update()
    {
        transform.position = new Vector2(startPoint + cam.position.x * rate, transform.position.y);
    }
}
