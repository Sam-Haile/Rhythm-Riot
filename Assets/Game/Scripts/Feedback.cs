using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feedback : MonoBehaviour
{

    public PlayerMovement player;

    public MMFeedbacks jumpFeedback;
    public MMFeedbacks landingFeedback;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            landingFeedback?.PlayFeedbacks();
        }
    }

}
