using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamreaLook : MonoBehaviour
{
    public float sphereRadius = 0.1f;

    // The offset of grabed object
    [SerializeField]private float zOffset =3f;
    [SerializeField] private float xOffset = 0f;
    [SerializeField] private float yOffset = 0f;

    GameObject heldObj;
    Vector3 objOriginalPos;

    //Roate camera 
    public float rotateSpeed = 5f;

    //Camera Rotate
    public float mouseSense = 0.5f;
    public float clampAngle = 80f;
    float rotationX;
    float rotationY;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 startRot = transform.localRotation.eulerAngles;
        rotationX = startRot.x;
        rotationY = startRot.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 eyePosition = transform.position;
        Vector3 mousePos = Input.mousePosition;

        // The mouse move on the nearClip interface
        //We want the mouse's z direction toward the nearClipPlan
        //Set the mouse z position, or its defalut value is 0
        mousePos.z = Camera.main.nearClipPlane;

        //get the mouse world position
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 dir = mouseWorldPos - eyePosition;
        //trun the vector length to 1, get the direction
        dir.Normalize();

        RaycastHit hitter = new RaycastHit();

        Debug.DrawLine(eyePosition, dir*20f, Color.red);

        //out means return a value
        //SphereCast will chech the area with certain radius
        if (Physics.SphereCast(eyePosition,sphereRadius,dir, out hitter))
        {
            //Debug.Log("hit something!");
            //Debug.Log(hitter.collider.gameObject.name);
            
            //if I held some obj
            if(heldObj != null)
            {
                //if the ray hit the holded obj
                if(heldObj.name == hitter.collider.gameObject.name)
                {
                    Debug.Log("cursor on held object");
                    float xRotate = Input.GetAxis("Mouse X") * rotateSpeed;
                    float yRotate = Input.GetAxis("Mouse Y") * rotateSpeed;

                    heldObj.transform.Rotate(Vector3.down, xRotate);
                    heldObj.transform.Rotate(Vector3.right, yRotate);
                }
            }


            if(Input.GetMouseButton(0) && hitter.collider.gameObject.tag == "Pickable" && heldObj == null)
            {
                Debug.Log("can pick");
                PickUpObject(hitter.collider.gameObject);
            }
        }

        if(Input.GetMouseButton(1) &&heldObj != null)
        {
            //Debug.Log("drop");
            DropObject();
        }
        MoveCamera();
    }
    void PickUpObject(GameObject obj)
    {
        heldObj = obj;
        objOriginalPos = obj.transform.position;
        Debug.Log(objOriginalPos);

        //for rotate the object when it was chosen
        obj.transform.SetParent(gameObject.transform);

        Vector3 newPos = new Vector3(transform.position.x+xOffset, transform.position.y+yOffset, transform.position.z+zOffset);

        obj.transform.position = newPos;
    }

    void DropObject()
    {
        heldObj.transform.SetParent(null);
        heldObj.transform.position = objOriginalPos;

        objOriginalPos = Vector3.zero;
        heldObj = null;
    }

    void MoveCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //why add Time.deltaTime?
        //make the movement smooth
        rotationX += mouseY * mouseSense * Time.deltaTime;
        rotationY += mouseX * mouseSense * Time.deltaTime;

        rotationX = Mathf.Clamp(rotationX,-clampAngle,clampAngle);

        Quaternion newRotation = Quaternion.Euler(rotationX, rotationY,0.0f);
        transform.rotation = newRotation;
    }
}
