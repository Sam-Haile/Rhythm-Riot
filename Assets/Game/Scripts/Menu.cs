using MoreMountains.Feedbacks;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // get logos to animate it
    public CameraController cam;
    public Camera cameraFOV;

    public GameObject pressAnyButton;
    public Transform parentTransform;

    public MMFeedbacks transition1;
    public MMFeedbacks transition2;
    CameraController[] cameraData = new CameraController[3];

    public MMFeedbacks textFeedback;

    private void Start()
    {
        ToggleMenuIcons(false);
        cameraData[0] = new CameraController { Position = new Vector3(42.91f, 1.21f, -35.9f), Rotation = new Vector3(-9.4f, -56.15f, 0f) };
        cameraData[1] = new CameraController { Position = new Vector3(39.33f, 1.11f, -32.89f), Rotation = new Vector3(-15.22f, -75.7f, 1.1f) };
        cameraData[2] = new CameraController { Position = new Vector3(40.20f, 1.30f, -28.66f), Rotation = new Vector3(-12.26f, -112.43f, 6f) };

        // Initial Position
        cameraFOV.fieldOfView = 40;
        cam.transform.position = cameraData[0].Position;
        cam.transform.rotation = Quaternion.Euler(cameraData[0].Rotation);
    }


    private void Update()
    {
        if (pressAnyButton.activeSelf)
        {
            textFeedback?.PlayFeedbacks();
        }

        if (Input.anyKeyDown)
        {
            transition1?.PlayFeedbacks();
            pressAnyButton.SetActive(false);
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
        Debug.Log("QUIT GAME!");
    }

    public void CamPos1()
    {
        cameraFOV.fieldOfView = 40;
        cam.transform.position = cameraData[0].Position;
        cam.transform.rotation = Quaternion.Euler(cameraData[0].Rotation);
    }

    public void CamPos2()
    {
        cameraFOV.fieldOfView = 80;
        cam.transform.position = cameraData[1].Position;
        cam.transform.rotation = Quaternion.Euler(cameraData[1].Rotation);
    }


    public void CamPos3()
    {
        cameraFOV.fieldOfView = 58;
        cam.transform.position = cameraData[2].Position;
        cam.transform.rotation = Quaternion.Euler(cameraData[2].Rotation);
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
