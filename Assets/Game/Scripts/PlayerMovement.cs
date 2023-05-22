using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Burst.CompilerServices;
using UnityEngine.UIElements;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 10f;
    public float jumpForce = 3f;

    //Used for tracking jumps
    private bool isGrounded = false;
    private bool hasJumped = false;

    //Used for tracking sliding ability
    private bool canSlide = false;

    private float gravityScale = 1.0f;
    public float globalGravity = -90f;

    public MMFeedbacks jumpFeedback;
    public MMFeedbacks landingFeedback;
    public MMFeedbacks timeFeedback;
    public GameObject jumpParticles;

    public BoxCollider boxCollider;

    public AudioSource music;

    public AnimationController scriptWithTrigger;

    private float num = 1f;

    [HideInInspector]
    public bool airJump;
    [HideInInspector]
    public bool grindJump;
    [HideInInspector]
    public bool beginGame = false;
    [HideInInspector]
    private void Awake()
    {
        music.Pause();
    }

    void Start()
    {
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    // Update is called once per frame
    void Update()
    {

        Move();

        JumpInput();

        LeftClickInput();

        SlideInput();

        if (Input.GetKeyDown(KeyCode.L))
        {
            music.Play();
            timeFeedback?.StopFeedbacks();
        }

        //jumpParticles.transform.position = new Vector3(transform.position.x, transform.position.y -1.1f, transform.position.z);
    }

    private void RestoreColliders()
    {
            if (num <= 0)
            {
                boxCollider.center = new Vector3(boxCollider.center.x, 1.36f, boxCollider.center.z);
                boxCollider.size = new Vector3(boxCollider.size.x, 2.82f, 1.42f);
            }
            num -= Time.deltaTime;
    }

    private void JumpInput()
    {
        // Check for jump input
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            jumpFeedback.transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            jumpFeedback?.PlayFeedbacks();
            airJump = true;

            // If the player is on the ground, jump
            if (isGrounded)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Zero out the horizontal velocity
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                UnityEngine.Debug.DrawLine(transform.position, transform.position + Vector3.up * jumpForce, Color.green, 2f);
            }
            // If the player is in the air and hasn't jumped before, perform a double jump
            else if (!hasJumped)
            {
                scriptWithTrigger.DoubleJumpTrigger();
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Zero out the horizontal velocity
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                hasJumped = true;
            }

        }

        if (rb.velocity.y > 0)
        {
            gravityScale = 2.5f;
        }
        else
        {
            gravityScale = 1f;
        }

    }

    private void LeftClickInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UnityEngine.Debug.Log("Left mouse button clicked!");
        }

    }

    private void SlideInput()
    {
        if (canSlide && Input.GetKeyDown(KeyCode.S))
        {
            boxCollider.center = new Vector3(boxCollider.center.x, 0.75f, boxCollider.center.z);
            boxCollider.size = new Vector3(boxCollider.size.x, 1.4f, 3.67f);
            scriptWithTrigger.SlideTrigger();
        }
        else if (!canSlide && Input.GetKeyDown(KeyCode.S))
        {
            grindJump = false;
        }


    }

    /// <summary>
    /// Moves the player to the right at constant speed
    /// </summary>
    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            music.Play();
            beginGame = true;
        }

        if (beginGame)
        {
            Vector3 startPos = transform.position;
            transform.Translate(transform.right * moveSpeed * Time.deltaTime);
            Vector3 endPos = transform.position;
            UnityEngine.Debug.DrawLine(startPos, endPos, Color.green, 2f);
        }
    }


    // Check if the player is colliding with a platform
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Platform"))
        {
            isGrounded = true;
            hasJumped = false;
            airJump = false;

            if (collision.collider.CompareTag("Ground"))
            {
                jumpForce = 40;
                globalGravity = -90;
                landingFeedback?.PlayFeedbacks();
                canSlide = true;
                grindJump = false;
            }
            if (collision.collider.CompareTag("Platform"))
            {
               // jumpForce = 10;
               // globalGravity = -15f;
            }
        }
    }


    // Check if the player is no longer colliding with a platform
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Platform"))
        {
            isGrounded = false;
            canSlide = false;

            if (collision.collider.CompareTag("Ground"))
            {
                grindJump = true;
                airJump = false;

            }
            else if (collision.collider.CompareTag("Platform"))
            {

            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SlowMotion"))
        {
            music.Pause();
            timeFeedback?.PlayFeedbacks();
        }
    }

}
