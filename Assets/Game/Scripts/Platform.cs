using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public Rigidbody playersRb;
    public Collider playersCollider;
    public float timer = .5f;

    private bool canDrop;
    private bool startTimer;

    [Tooltip("The original collider. Will always be present thanks to the RequireComponent attribute.")]
    private new BoxCollider collider = null;


    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // if player has positive Y velocity
        // Disable collision
        if (playersRb.velocity.y > 0)
        {
            DisableCollision();
        }
        // if players has negative Y velocity
        // enable collision
        else if (playersRb.velocity.y < 0 && !canDrop)
        {
            EnableCollision();
        }
        else if(playersRb.velocity.y == 0 && Input.GetKey(KeyCode.S))
        {
            canDrop = true;
            timer = .5f;
            startTimer = true;
            DisableCollision();
        }

        if (startTimer)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0f)
        {
            canDrop = false;
        }







        /*
        if (playersRb.velocity.y > 0)
        {
            //Debug.Log("Positive");
            Physics.IgnoreCollision(this.collider, playersCollider, true);
        }
        else if (playersRb.velocity.y < 0 && !canDrop)
        {
            //Debug.Log("Negative");
            Physics.IgnoreCollision(this.collider, playersCollider, false);
        }
        else if (Input.GetKey(KeyCode.S) && playersRb.velocity.y == 0 && )
        {
            Physics.IgnoreCollision(this.collider, playersCollider, true);
        }*/

    }



    private void DisableCollisionTimer()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Physics.IgnoreCollision(this.collider, playersCollider, true);
            timer = 0f;
        }

    }

    private void EnableCollision()
    {
        Physics.IgnoreCollision(this.collider, playersCollider, false); ;
    }

    private void DisableCollision()
    {
        Physics.IgnoreCollision(this.collider, playersCollider, true); ;
    }

    /*
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            canDrop = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            timerStart = true;
        }
    }

    */

}
