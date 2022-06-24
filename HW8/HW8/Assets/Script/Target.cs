using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private bool faceLeft = true;

    public float amplitude = 10f;
    public float frequency;

    public Transform leftPoint, rightPoint;
    public Rigidbody target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (faceLeft)
        //{
        //    target.velocity = new Vector3(-speed, target.velocity.y,target.velocity.y);
        //    if (transform.position.x <= leftPoint.position.x)
        //    {
        //        faceLeft = false;
        //    }
        //}
        //else
        //{
        //    target.velocity = new Vector3(speed, target.velocity.y, target.velocity.y);
        //    if (transform.position.x > rightPoint.position.x)
        //    {
        //        faceLeft = true;
        //    }
        //}

        target.velocity = new Vector3(amplitude * Mathf.Cos(Time.time * frequency), target.velocity.y, target.velocity.y);


    }
}
