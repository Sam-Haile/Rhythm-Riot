using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feedback : MonoBehaviour
{

    public PlayerMovement player;

    public MMFeedbacks jumpFeedback;
    public MMFeedbacks landingFeedback;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            landingFeedback?.PlayFeedbacks();
        }
    }

}
