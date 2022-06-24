using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    private InputAction leftMouseClick;
    public Camera cameraM;
    public ParticleSystem shootEffect;

    [SerializeField] float range = 100f;

    public static float score;
    public Text scoreText;

    private void Awake()
    {
        leftMouseClick = new InputAction(binding: "<Mouse>/leftButton");
        leftMouseClick.performed += ctx => LeftMouseClicked();
        leftMouseClick.Enable();
     }

    void LeftMouseClicked()
    {
        // Debug.Log("Click");

        Shoot();
        scoreText.text = score.ToString();        
    }

    void Shoot()
    {
        shootEffect.Play();

        RaycastHit hit;
        if (Physics.Raycast(cameraM.transform.position, cameraM.transform.forward,out hit, range))
        {
            Debug.Log(hit.collider.gameObject.name);

            if(hit.collider.gameObject.tag == "Rabbit")
            {
                score += 1;
            }else if (hit.collider.gameObject.tag == "Deer")
            {
                score += 5;
            }
            else if (hit.collider.gameObject.tag == "Bear")
            {
                score += 10;
            }

        }
    }
}
