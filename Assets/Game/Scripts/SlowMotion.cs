using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    public float slowMotionTimescale;
    private float startTimescale;
    private float startFixedDeltaTime;
    private float timer = .03f;
    private bool restore = false;

    // Start is called before the first frame update
    void Start()
    {
        startTimescale = Time.timeScale;
        startFixedDeltaTime = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (restore)
        {
            StopSlowMotion();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartSlowMotion();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        restore = true;
    }

    private void StartSlowMotion()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            slowMotionTimescale -= .01f;
            timer = .03f;
        }

        Time.timeScale = slowMotionTimescale;
        Time.fixedDeltaTime = startFixedDeltaTime * slowMotionTimescale;
    }

    private void StopSlowMotion()
    {
        Time.timeScale = startTimescale;
        Time.fixedDeltaTime = startFixedDeltaTime;
    }

}
