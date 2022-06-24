using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D player;

    //horizontal speed
    public float speed = 5.0f;
    public float jumpForece = 5.0f;

    public float horizontalMove;
    public float faceDirection;

    // using in the Update to detect space button down
    private bool spacePressed;
    //player can only jump when the fox on the ground
    public bool isJump = false;

    public Animator playerAni;

    public Collider2D playerCollder;

    public LayerMask ground;

    public int score;

    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressed = true;
           // Debug.Log("SPACE");
        }
    }

    private void FixedUpdate()
    {
        Movement();
        jump();
        jumpSwitch();
    }


    void Movement()
    {
         horizontalMove = Input.GetAxis("Horizontal");
         faceDirection = Input.GetAxisRaw("Horizontal");

        // move player on horizontal direction
        player.velocity = new Vector2(horizontalMove * speed * Time.deltaTime, player.velocity.y);

        // start runing animation
        if (horizontalMove != 0)
        {                     
            playerAni.SetFloat("running", Mathf.Abs(faceDirection));
        }

        //turn the directon of player if she goes left
        if(faceDirection != 0)
        {
            transform.localScale = new Vector3(faceDirection * transform.localScale.y, transform.localScale.y, transform.localScale.z);
        }      
    }

    void jump()
    {
        if (spacePressed & !isJump)
        {
            // if space is pressed, the player jump
            player.AddForce(Vector2.up * jumpForece * Time.deltaTime, ForceMode2D.Impulse);
            
            isJump = true;

            // start jumping animation
            playerAni.SetBool("jumping", true);
            playerAni.SetBool("idle", false);
        }
    }



    void jumpSwitch()
    {

        //when player is jumping
        if (playerAni.GetBool("jumping"))
        {
            // if player's velocity less than 0. player start falling
            if(player.velocity.y < 0)
            {
                playerAni.SetBool("jumping", false);
                playerAni.SetBool("falling", true);
            }   
            //when player stand on the ground, end falling
        }else if (playerCollder.IsTouchingLayers(ground))
        {
            playerAni.SetBool("falling", false);
            playerAni.SetBool("idle", true);

            //spacePressed to balance the time interval between FixUpdate and Update
            //If put this line of code in jump() and player quickly press the space twice, the fox will re-jump as soon as it collides with the ground
            spacePressed = false;

            //Fox does not jump, so the fox could jump if player press the space later
            isJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "cherry")
        {
            score += 1;
          //  Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
           
        }
    }



}
