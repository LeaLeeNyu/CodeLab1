using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerinCave : MonoBehaviour
{

    public Rigidbody2D playerRB;

    //horizontal speed
    private float speed;
    public float normalSpped;
    public float sprintSpeed;

    private float jumpForece;
    public float normalJump;
    public float sprintJump;
    public float bounceForce;

    public float horizontalMove;
    public float faceDirection;

    // using in the Update to detect space button down
    private bool spacePressed;
    //player can only jump when the fox on the ground
    public bool isJump = false;

    //detect whether the player is hurt
    private bool isHurt = false;

    public Animator playerAni;

    public Collider2D playerCollider;

    public LayerMask ground;

    public int score;

    //State related parameter
    public SpriteRenderer playerSprit;
    public Color playerNormalC;
    public Color playerSprintC;
    public Color playerInvisC;


    private void Awake()
    {
        speed = normalSpped;
        jumpForece = normalJump;

        //find the player collider for power up state
        playerCollider = gameObject.GetComponent<Collider2D>();      
    }

    void Start()
    {
       // TransStatement(State.normal);
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

        if (!isHurt && DialogueSystem.noDialogue)
        {
        Movement();
        }
        
        jump();
        aniSwitch();
    }


    void Movement()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        faceDirection = Input.GetAxisRaw("Horizontal");

        // move player on horizontal direction
        playerRB.velocity = new Vector2(horizontalMove * speed, playerRB.velocity.y);

        // start runing animation
        if (horizontalMove != 0 && DialogueSystem.noDialogue)
        {
            playerAni.SetFloat("running", Mathf.Abs(faceDirection));
        }

        //turn the directon of player if she goes left
        if (faceDirection != 0)
        {
            transform.localScale = new Vector3(faceDirection * transform.localScale.y, transform.localScale.y, transform.localScale.z);
        }
    }

    void jump()
    {
        if (spacePressed & !isJump)
        {
            // if space is pressed, the player jump
            playerRB.AddForce(Vector2.up * jumpForece, ForceMode2D.Impulse);

            isJump = true;

            // start jumping animation
            playerAni.SetBool("jumping", true);
            playerAni.SetBool("idle", false);
        }
    }

    void aniSwitch()
    {

        //when player is jumping
        if (playerAni.GetBool("jumping"))
        {
            // if player's velocity less than 0. player start falling
            if (playerRB.velocity.y < 0)
            {
                playerAni.SetBool("jumping", false);
                playerAni.SetBool("falling", true);
            }
            //when player stand on the ground, end falling
        }
        else if (isHurt)
        {
            // after player was hurted by enemy, and bounce back --> velocity =1, isHurt = false, player can movc
            if (Mathf.Abs(playerRB.velocity.x) < 0.1)
            {
                isHurt = false;
                playerAni.SetBool("idle", true);
                playerAni.SetBool("isHurt", isHurt);
                
            }
                
        }
        else if (playerCollider.IsTouchingLayers(ground))
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

    //if player collide cherry, player collect it and the cherry disappare
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);

         if(collision.tag == "house")
        {
            loadLevel.CollideHouse();
        }else if(collision.tag == "deadLine")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy" )
        {
            //if player hit the on the head of the enemy, the enemy dead
            if (playerAni.GetBool("falling"))
            {               
                collision.gameObject.SetActive(false);

                //bounce effect
                playerRB.AddForce(Vector3.up * jumpForece * Time.deltaTime, ForceMode2D.Impulse);
            }
            //if player collide the enemy, play hurt animation
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                Debug.Log("enemy!");           
                // bounce back the player
                playerRB.AddForce(Vector2.left * bounceForce * Time.deltaTime, ForceMode2D.Impulse);
                //play the hurt animation
                isHurt = true;
                playerAni.SetBool("isHurt", isHurt);               
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                playerRB.AddForce(Vector2.right * bounceForce * Time.deltaTime, ForceMode2D.Impulse);
               isHurt = true;
                playerAni.SetBool("isHurt", isHurt);
            }
        }
    }




}
