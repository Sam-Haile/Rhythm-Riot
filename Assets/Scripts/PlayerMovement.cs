using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed = 10f;
    public float jumpForce = 3f;

    //Used for tracking jumps
    private bool isGrounded = false;
    private bool hasJumped = false;
    //Used for tracking sliding ability
    private bool canSlide = false;

    public float timer = 5f;


    private float gravityScale = 1.0f;
    public float globalGravity = -90f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        timer -= Time.deltaTime;
       
        if (timer <= 0f)
        {
            timer = 0f;
        }

        // Move player right at constant speed
        Vector3 startPos = transform.position; 
        transform.Translate(transform.right * moveSpeed * Time.deltaTime);
        Vector3 endPos = transform.position; 

        Debug.DrawLine(startPos, endPos, Color.green, 2f);

        // Check for jump input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // If the player is on the ground, jump
            if (isGrounded)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Zero out the horizontal velocity
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                Debug.DrawLine(transform.position, transform.position + Vector3.up * jumpForce, Color.green, 2f);
            }
            // If the player is in the air and hasn't jumped before, perform a double jump
            else if (!hasJumped)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Zero out the horizontal velocity
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                hasJumped = true;
            }

        }
        if (canSlide && Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Slidin");
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left mouse button clicked!");
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

    // Check if the player is colliding with a platform
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Platform"))
        {
            isGrounded = true;
            hasJumped = false;
        }
        if (collision.collider.CompareTag("Ground"))
        {
            canSlide = true;
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
