using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eagleController: MonoBehaviour
{
    // set the enemy target
    public GameObject target;

    public GameObject Target
    {
        get => target;
        private set
        {
            target = value;
            // if detect the player enter the cube, set the playerDetected to true
            playerDetected = target != null;
        }
    }
    //set the start point of enemy
    public Rigidbody2D enemyRB;
    public float speed = 5.0f;
    public bool faceLeft = true;

    public Transform detectorOrigin;
    private Vector3 directionToTarget => target.transform.position - enemyRB.gameObject.transform.position;
    private Vector3 enemyStartPoint;
    private Vector3 directionToStart => enemyStartPoint - enemyRB.gameObject.transform.position;
    public float distanceToStart;

    //OverlapBox parameters
    public bool playerDetected;

    public Vector3 detectorSize = Vector3.one;
    public Vector3 detectorOffset = Vector3.zero;

    //detect if there is a player every 0.3 second
    public float detectionDelay = 0.3f;

    // the layer have player
    public LayerMask detectorLayer;

    //Gizmo parameters
    public Color gizmoIdleColor = Color.green;
    public Color gizmoDetectedColor = Color.red;
    public bool showGizmos = true;

    private void Start()
    {
        //enemyRB = GetComponent<Rigidbody2D>();
        StartCoroutine(DetectionCoroutine());

        enemyStartPoint = enemyRB.gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        distanceToStart = Mathf.Abs(Vector3.Distance(enemyStartPoint, enemyRB.gameObject.transform.position));

        if (playerDetected)
        {
            enemyRB.velocity = directionToTarget.normalized * speed ;
        }
        else if (!playerDetected & distanceToStart > 0.05f)
        {
            enemyRB.velocity = directionToStart.normalized * speed;
        }
        else
        {
            enemyRB.velocity = Vector3.zero;
        }

        if (faceLeft)
        {           
            if (enemyRB.velocity.x > 0)
            {
                faceLeft = false;
                enemyRB.gameObject.transform.localScale = new Vector3(-1 * enemyRB.gameObject.transform.localScale.x,
                                                               enemyRB.gameObject.transform.localScale.y,
                                                               enemyRB.gameObject.transform.localScale.z);
            }
                
        }
        else
        {
            if (enemyRB.velocity.x < 0)
            {
                faceLeft = true;
                enemyRB.gameObject.transform.localScale = new Vector3(-1 * enemyRB.gameObject.transform.localScale.x,
                                                               enemyRB.gameObject.transform.localScale.y,
                                                               enemyRB.gameObject.transform.localScale.z);
            }
        }         

    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionDelay);
        if (!PlayerController.invisibleOn)
        {
            PerformDetaction();
        }
        

        //loop the dectection
        StartCoroutine(DetectionCoroutine());
    }

    public void PerformDetaction()
    {
        //Detect if the player enter the box area
        Collider2D collider = Physics2D.OverlapBox(
                              (Vector3)detectorOrigin.position + detectorOffset,
                              detectorSize, 0, detectorLayer);

        if (collider != null)
        {
            Target = collider.gameObject;
        }
        else
        {
            Target = null;
        }
    }

    private void OnDrawGizmos()
    {
        if(showGizmos && detectorOrigin != null)
        {
            Gizmos.color = gizmoIdleColor;
            if (playerDetected)
                Gizmos.color = gizmoDetectedColor;

            Gizmos.DrawCube((Vector3)detectorOrigin.position + detectorOffset, detectorSize);
        }
    }

}
