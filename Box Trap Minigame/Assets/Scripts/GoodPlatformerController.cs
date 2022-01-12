using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodPlatformerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private float moveInput;

    private bool isGrounded;
    //public Transform feetPos;
    //public float checkRadius;
    public Transform bottomRight;
    public Transform topLeft;
    public LayerMask whatIsGround;

    /*
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    */

    public float hangTime = .2f;
    private float hangCounter;

    public float jumpBufferLength = .1f;
    private float jumpBufferCount;

    public Animator playerAnim;
    private bool isCrouching;
    private bool crouchSound = false;
    public BoxCollider2D playerCol;

    public GameObject deathEffect;

    private ShakeBehavior shake;
    public GameManager gameManager;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        shake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShakeBehavior>();
    }

    private void FixedUpdate()
    {
        if(isCrouching == false)
        {
            moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
    }

    private void Update()
    {
        //isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        isGrounded = Physics2D.OverlapArea(topLeft.position, bottomRight.position, whatIsGround);

        //hangtime
        if (isGrounded)
        {
            hangCounter = hangTime;

            playerAnim.SetBool("isGrounded", true);
        }
        else
        {
            hangCounter -= Time.deltaTime;

            playerAnim.SetBool("isGrounded", false);
        }

        //jump buffer
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCount = jumpBufferLength;
        }
        else
        {
            jumpBufferCount -= Time.deltaTime;
        }

        //flipping sprite
        
        if(moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
       
        if(moveInput != 0)
        {
            playerAnim.SetBool("isRunning", true);
        }
        else
        {
            playerAnim.SetBool("isRunning", false);
        }

        //jumping
        if (hangCounter > 0f && jumpBufferCount >= 0 && isCrouching == false)
        {
            FindObjectOfType<AudioManager>().Play("jump");

            rb.velocity = Vector2.up * jumpForce;

            jumpBufferCount = 0;
        }

        //Crouching
        if (Input.GetKey(KeyCode.DownArrow) && isGrounded == true)
        {
            if(crouchSound == false)
            {
                FindObjectOfType<AudioManager>().Play("crouch");
                crouchSound = true;
            }

            moveInput = 0;

            isCrouching = true;

            rb.velocity = new Vector2(0, rb.velocity.y);

            playerAnim.SetBool("isCrouching", true);

            Collider2D platformCheck = Physics2D.OverlapArea(bottomRight.position, topLeft.position, whatIsGround);

            if (platformCheck != null && platformCheck.CompareTag("Platform"))
            {
                playerCol.enabled = false;

                Invoke("ColliderOn", .1f);
            }
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) || isGrounded == false)
        {
            isCrouching = false;

            crouchSound = false;

            playerAnim.SetBool("isCrouching", false);

            //playerCol.enabled = true;
        }

        //holding jump
        /*
        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if(jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        */
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 10)
        {
            Death(col.gameObject.tag.ToString());
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 10)
        {
            Death("Dippy got smooshed.");
        }
    }

    void Death(string e)
    {
        FindObjectOfType<AudioManager>().Play("hurt");
        FindObjectOfType<AudioManager>().Play("death");
        Destroy(gameObject);
        Instantiate(deathEffect, transform.position, deathEffect.transform.rotation);
        shake.TriggerShake(.3f, .2f);
        gameManager.GameOver(e);
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<Timer>().enabled = false;
    }

    void ColliderOn()
    {
        playerCol.enabled = true;
    }
}
