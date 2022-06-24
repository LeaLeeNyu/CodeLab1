using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    public Rigidbody2D frogRB;
    public float speed = 5.0f;
    private bool faceLeft = true;

    public Transform leftPoint, rightPoint;

    void Start()
    {
        frogRB = GetComponent<Rigidbody2D>();

        // detach the chidren to aviod the children's transform move with parent
        transform.DetachChildren();
    }

    // Update is called once per frame
    void Update()
    {
        if (faceLeft)
        {
           //Debug.Log("left");
            frogRB.velocity = new Vector2(-speed,frogRB.velocity.y);

            if (transform.position.x < leftPoint.position.x)
            {
                faceLeft = false;
                transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
            }
        }
        else
        {
            frogRB.velocity = new Vector2(speed, frogRB.velocity.y);

            if (transform.position.x > rightPoint.position.x)
            {
                faceLeft = true;
                transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
            }
        }

    }


}

