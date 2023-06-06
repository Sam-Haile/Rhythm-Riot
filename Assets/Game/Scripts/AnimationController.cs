using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationController : MonoBehaviour
{

    public PlayerMovement playerMovement;
    public Animator animator;


    public ParticleSystem fireFX1;
    public ParticleSystem fireFX2;
    public ParticleSystem noteFX;
    public UnityEvent onTriggerActivated;
    private int num = 0;

    public Collider gameEndCollider;

    private void Start()
    {
        fireFX1.Stop();        
        fireFX2.Stop();
        noteFX.Stop();
    }

    /// <summary>
    /// Display various animations depending on players actions
    /// </summary>
    void Update()
    {
        if (playerMovement.grindJump)
        {
            animator.SetBool("toPlatform", true);
        }   
        else if (!playerMovement.grindJump)
        {
            animator.SetBool("toPlatform", false);
        }
        if (playerMovement.airJump)
        {
            animator.SetBool("toAir", true);
        }
        else if (!playerMovement.airJump)
        {
            animator.SetBool("toAir", false);
        }
        if (playerMovement.beginGame)
        {
            animator.SetBool("beginGame", true);
        }

        if (Input.GetKeyDown(KeyCode.Return) && num == 0)
        {
            num++;
        }

    }

    /// <summary>
    /// Display Slide Animation on 'S' key press
    /// </summary>
    public void SlideTrigger()
    {
        // Trigger activation logic...
        animator.SetTrigger("slide");
        onTriggerActivated.Invoke();
    }

    /// <summary>
    /// Triggers jump animation a second time
    /// </summary>
    public void DoubleJumpTrigger()
    {
        // Trigger activation logic...
        animator.SetTrigger("doubleJump");
        onTriggerActivated.Invoke();
    }


    /// <summary>
    /// Display animations when touching a note/ending the game
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("gameEnd"))
        {
            animator.SetTrigger("gameEnd");
        }
        if (other.CompareTag("Note"))
        {
            noteFX.Play();
        }

    }

    /// <summary>
    /// Add a fire effect when touching the pipe
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            fireFX1.Play();
            fireFX2.Play();
        }
        else if (collision.collider.CompareTag("Ground"))
        {
            fireFX1.Stop();
            fireFX2.Stop();
        }
    }


    /// <summary>
    /// Stop fire effect 
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            fireFX1.Stop();
            fireFX2.Stop();
        }
    }
}
