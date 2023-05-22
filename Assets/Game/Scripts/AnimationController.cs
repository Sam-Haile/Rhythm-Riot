using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationController : MonoBehaviour
{

    public PlayerMovement playerMovement;
    public Animator animator;

    public UnityEvent onTriggerActivated;

    public Collider gameEndCollider;

    void Update()
    {
        if (playerMovement.grindJump == true)
        {
            animator.SetBool("toPlatform", true);
        }   
        else if (playerMovement.grindJump == false)
        {
            animator.SetBool("toPlatform", false);
        }
        
        if (playerMovement.airJump == true)
        {
            animator.SetBool("toAir", true);
        }
        else if (playerMovement.airJump== false)
        {
            animator.SetBool("toAir", false);
        }

        if (playerMovement.beginGame == true)
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
            playerMovement.beginGame = false;
            animator.SetTrigger("gameEnd");
        }
    }
}
