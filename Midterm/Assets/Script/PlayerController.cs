using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D playerRB;

    //for invisible state, the player and frogs will not collide
    public Collider2D[] enemyCollider;
    public GameObject enemy;

    //horizontal speed
    private float speed = 400.0f;
    public float normalSpped = 400.0f;
    public float sprintSpeed = 600.0f;

    private float jumpForece = 5.0f;
    public float normalJump = 500.0f;
    public float sprintJump = 700.0f;
    public float bounceForce = 3.0f;

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

    public GameObject sprint;
    public ParticleSystem sprintPartical;
    public GameObject invisibleShield;
    public static bool invisibleOn = false;

    //Health related paramter
    public Health healthScript;

    public enum State
    {
        normal,
        sprint,
        invisible
    };

    public static State currentState;


    private void Awake()
    {
        //find the partical effect of sprint state
        sprint = GameObject.Find("sprintPartical");
        sprintPartical = sprint.GetComponent<ParticleSystem>();

        //find the light effect of invisible state
        invisibleShield = GameObject.Find("invisibleShield");

        speed = normalSpped;
        jumpForece = normalJump;

        //find the player collider for power up state
        playerCollider = gameObject.GetComponent<Collider2D>();

        //for invisible mode, the player and frogs will not collide
        enemy = GameObject.Find("enemy");
        enemyCollider = enemy.GetComponentsInChildren<CircleCollider2D>();

        //find health system
        healthScript = GameObject.Find("healthSystem").GetComponent<Health>();
    }

    void Start()
    {
        TransStatement(State.normal);
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
        if (!DialogueSystem.noDialogue)
        {
            playerAni.SetBool("idle", true);
            playerAni.SetFloat("running", 0);
        }
        else if (horizontalMove != 0 && DialogueSystem.noDialogue)
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

        if (collision.tag == "cherry")
        {
            //score += 1;
            //Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);

            //When collide the cherry, player turn to sprint state
            TransStatement(State.sprint);

        }else if(collision.tag == "dimond")
        {
            collision.gameObject.SetActive(false);
            TransStatement(State.invisible);
        }
        // if collider with the house and press space button
        else if(collision.tag == "house")
        {
            loadLevel.CollideHouse();
        }else if(collision.tag == "deadLine")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy" && currentState == State.normal)
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
                //reduce health value
                healthScript.health -= 1; 
                healthScript.changeHealth();
                if(healthScript.health == 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                playerRB.AddForce(Vector2.right * bounceForce * Time.deltaTime, ForceMode2D.Impulse);
               isHurt = true;
                playerAni.SetBool("isHurt", isHurt);
            }
        }else if (collision.gameObject.tag == "enemy" && currentState == State.sprint)
        {
            collision.gameObject.SetActive(false);
        }
    }

    //Switch the statement of player
    void TransStatement(State newState)
    {
        currentState = newState;
        switch (newState)
        {
            case State.normal:
                playerSprit.color = playerNormalC;
                speed = normalSpped;
                jumpForece = normalJump;
                sprint.SetActive(false);
                invisibleShield.SetActive(false);
                invisibleOn = false;
                for (int i = 0; i < enemyCollider.Length; i++)
                {
                    Physics2D.IgnoreCollision(playerCollider, enemyCollider[i],false);
                }
                break;

            case State.sprint:
                playerSprit.color = playerSprintC;
                sprintPartical.Play();
                speed = sprintSpeed;
                jumpForece = sprintJump;
                sprint.SetActive(true);
                invisibleOn = false;
                StartCoroutine(PowerUpCountDown());
                break;

            case State.invisible:
                playerSprit.color = playerInvisC;
                invisibleShield.SetActive(true);
                StartCoroutine(PowerUpCountDown());
                invisibleOn = true;
                for(int i = 0; i < enemyCollider.Length; i++)
                {
                    Physics2D.IgnoreCollision(playerCollider, enemyCollider[i],true);
                }                        
                break;
        }
    }

    // The sprint and invisible effect have time limitation
    IEnumerator PowerUpCountDown()
    {
        yield return new WaitForSeconds(5);
        TransStatement(State.normal);
        
        Debug.Log("Sprint Time Out!");
    }




}
