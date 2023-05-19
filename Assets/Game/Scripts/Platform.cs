using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public Rigidbody playersRb;
    public Collider playersCollider;
    public float timer = .5f;

    private bool touchingPlatform;

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
        else if (playersRb.velocity.y < 0)
        {
            EnableCollision();
        }
        if (Input.GetKey(KeyCode.S) && touchingPlatform)
        {
            DisableCollider(.5f);
        }

    }

    public void DisableCollider(float duration)
    {
        collider.enabled = false;
        Invoke("EnableCollider", duration);
    }


    private void EnableCollider()
    {
        collider.enabled = true;
    }

    private void EnableCollision()
    {
        Physics.IgnoreCollision(this.collider, playersCollider, false); ;
    }

    private void DisableCollision()
    {
        Physics.IgnoreCollision(this.collider, playersCollider, true); ;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            touchingPlatform = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            touchingPlatform = false;
        }
    }

}
