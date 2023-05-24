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

    public Collider gameEndCollider;

    private void Start()
    {
        fireFX1.Stop();        
        fireFX2.Stop();
        noteFX.Stop();
    }

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

    }

    public void SlideTrigger()
    {
        // Trigger activation logic...
        animator.SetTrigger("slide");
        onTriggerActivated.Invoke();
    }

    public void DoubleJumpTrigger()
    {
        // Trigger activation logic...
        animator.SetTrigger("doubleJump");
        onTriggerActivated.Invoke();
    }


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

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            fireFX1.Stop();
            fireFX2.Stop();
        }
    }
}
