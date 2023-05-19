using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    public PlayerMovement playerMovement;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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

    }




}
