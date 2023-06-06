using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Burst.CompilerServices;
using UnityEngine.UIElements;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Main player physics fields
    public Rigidbody rb;
    public float moveSpeed = 10f;
    public float jumpForce = 3f;
    private float gravityScale = 1.0f;
    public float globalGravity = -90f;
    public GameObject pressEnter;

    //Used for tracking jumps
    private bool isGrounded = false;
    private bool hasJumped = false;


    //Used for tracking various abilities
    private bool canSlide = false;
    [HideInInspector]
    public bool airJump;
    [HideInInspector]
    public bool grindJump;
    [HideInInspector]
    public bool beginGame;
    [HideInInspector]
    public bool isGrinding;

    public MMFeedbacks jumpFeedback;
    public MMFeedbacks landingFeedback;
    public BoxCollider playerCollider;

    public AudioManager audioManager;
    public AnimationController scriptWithTrigger;

    void Start()
    {
        pressEnter.SetActive(true);
        beginGame = false;
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    void Update()
    {
        Move();
        JumpInput();
        SlideInput();
    }

    /// <summary>
    /// Physics logic when jumping
    /// </summary>
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

    /// <summary>
    /// Decrease the size of the players box colldier when sliding
    /// </summary>
    private void SlideInput()
    {
        if (canSlide && Input.GetKeyDown(KeyCode.S))
        {
            playerCollider.center = new Vector3(playerCollider.center.x, 0.75f, playerCollider.center.z);
            playerCollider.size = new Vector3(playerCollider.size.x, 1.4f, 3.67f);
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
        if (Input.GetKeyDown(KeyCode.Return) && !beginGame)
        {
            pressEnter.SetActive(false);
            beginGame = true;
            audioManager.PlayMusic();
        }

        if (beginGame)
        {
            Vector3 startPos = transform.position;
            transform.Translate(transform.right * moveSpeed * Time.deltaTime);
            Vector3 endPos = transform.position;
            UnityEngine.Debug.DrawLine(startPos, endPos, Color.green, 2f);
        }
    }


    /// <summary>
    /// Check if the player is colliding with a platform
    /// </summary>
    /// <param name="collision"></param>
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
        }
    }


    /// <summary>
    /// Check if the player is no longer colliding with a platform
    /// </summary>
    /// <param name="collision"></param>
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
        }
    }

}
