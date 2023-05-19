using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Rigidbody rb;

    [SerializeField] private float movementSpeed = 10f;

    [SerializeField] private float slopeForce;
    [SerializeField] private float slopeForceRayLength;

    private CharacterController charController;

    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey;


    public float jumpForce = 3f;
    //Used for tracking jumps
    private bool isGrounded = false;
    private bool hasJumped = false;
    //Used for tracking sliding ability
    private bool canSlide = false;

    private float gravityScale = 1.0f;
    public float globalGravity = 9.81f;
    
    private bool isJumping;


    private void Start()
    {
        rb.useGravity = false;
    }

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    private void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float horizInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = -transform.up;

        if (charController.isGrounded)
        {
            Vector3 moveDirection = rightMovement * movementSpeed;
            moveDirection.y = charController.velocity.y; // Preserve the current vertical velocity
            charController.Move(moveDirection * Time.deltaTime);
        }
        // Apply gravity to the player
        Vector3 gravityVector = Vector3.down * globalGravity;
        charController.Move(gravityVector * Time.deltaTime);

        if ((vertInput != 0 || horizInput != 0) && OnSlope())
            charController.Move(Vector3.down * charController.height / 2 * slopeForce * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {

                Debug.Log("JUMP");
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Zero out the horizontal velocity
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                UnityEngine.Debug.DrawLine(transform.position, transform.position + Vector3.up * jumpForce, Color.green, 2f);
            // If the player is in the air and hasn't jumped before, perform a double jump

        }

        //JumpInput();
    }

    private bool OnSlope()
    {
        if (isJumping)
            return false;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, charController.height / 2 * slopeForceRayLength))
            if (hit.normal != Vector3.up)
                return true;
        return false;
    }

    private void JumpInput()
    {
        if(Input.GetKeyDown(jumpKey) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }

    private IEnumerator JumpEvent()
    {

        Debug.Log("JUMP");

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Zero out the horizontal velocity
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
       
        
        yield return null;
        /*
        charController.slopeLimit = 90.0f;
        float timeInAir = 0.0f;
        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            charController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        } while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);

        charController.slopeLimit = 45.0f;
        isJumping = false;
    */
    }
}
