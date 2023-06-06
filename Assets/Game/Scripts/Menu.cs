using MoreMountains.Feedbacks;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    // get logos to animate it
    public CameraController cam;
    public Camera cameraFOV;

    public GameObject pressAnyButton;
    public Transform parentTransform;

    public MMFeedbacks transition1;
    public MMFeedbacks transition2;

    public MMFeedbacks textFeedback;



    private void Start()
    {
        ToggleMenuIcons(false);

        // Initial Position
        cameraFOV.fieldOfView = 40;
        cam.transform.position = new Vector3(42.91f, 1.21f, -35.9f);
        cam.transform.rotation = Quaternion.Euler(new Vector3(-9.4f, -56.15f, 0f));
        textFeedback?.PlayFeedbacks();

    }

    private void Update()
    {
        if (Input.anyKeyDown && pressAnyButton != null)
        {
            Destroy(pressAnyButton);
            transition1?.PlayFeedbacks();
            ToggleMenuIcons(true);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    private void ToggleMenuIcons(bool activate)
    {
        // Turn off menu buttons initially
        for (int j = 0; j < parentTransform.childCount; j++)
        {
            parentTransform.GetChild(j).gameObject.SetActive(activate);
        }
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void Transition2()
    {
        transition2?.PlayFeedbacks();
    }

    public void Transition1()
    {
        transition1?.PlayFeedbacks();
    }



}
