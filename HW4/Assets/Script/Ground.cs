using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Ground : MonoBehaviour
{
    [MenuItem("Custom/Snap To Ground %g")]
    // Create a functon that pull up the resources object to the ground
      public static void Hit()
    {
        //get the clone resources' transform
        //GameObject[] resources = GameObject.FindGameObjectsWithTag("Resources");
        //Transform[] transforms = new Transform[resources.Length];

        //for (int i = 0; i < resources.Length; i++)
        //{
        //    transforms[i] = resources[i].transform;
        //}

        foreach (var transform in Selection.transforms)
        {
            //RaycastHit hitInfo;
            // set the raycast to detect the collider
            var hits = Physics.RaycastAll(transform.position + Vector3.up*10, Vector3.down, 10f);

            foreach (var hit in hits)
            {
                Debug.Log(hit.point);
                //if the raycast hit the gameObject itself, continue
                if (hit.collider.gameObject == transform.gameObject)
                 continue;

                 transform.position = hit.point;
                 break;
             
               
            }


        }
    }
}
