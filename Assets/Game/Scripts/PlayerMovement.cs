using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

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
    public GameObject jumpParticles;

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

        //jumpParticles.transform.position = new Vector3(transform.position.x, transform.position.y -1.1f, transform.position.z);
    }

    private void JumpInput()
    {
        // Check for jump input
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            jumpFeedback.transform.position = new Vector3(transform.position.x, transform.position.y -1.1f, transform.position.z);
            jumpFeedback?.PlayFeedbacks();

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
            UnityEngine.Debug.Log("Slidin");
        }
    }

    /// <summary>
    /// Moves the player to the right at constant speed
    /// </summary>
    private void Move()
    {
        Vector3 startPos = transform.position;
        transform.Translate(transform.right * moveSpeed * Time.deltaTime);
        Vector3 endPos = transform.position;
        UnityEngine.Debug.DrawLine(startPos, endPos, Color.green, 2f);
    }


    // Check if the player is colliding with a platform
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Platform"))
        {
            isGrounded = true;
            hasJumped = false;

            if (collision.collider.CompareTag("Ground"))
            {
                landingFeedback?.PlayFeedbacks();
                canSlide = true;
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
        }
    }


}
