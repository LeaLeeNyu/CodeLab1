using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuAni : MonoBehaviour
{
    private float speed = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.up * Time.deltaTime * speed);

        if (transform.position.y > 75f || transform.position.y < 45f)
        {
            speed *= -1;
        }
    }
}
