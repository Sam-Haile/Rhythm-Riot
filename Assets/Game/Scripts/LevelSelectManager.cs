using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{

    public MMFeedbacks changeTrackForward;
    public MMFeedbacks changeTrackBackward;


    private GameObject currentTrack;
    public GameObject[] tracks;

    public AudioSource hummingTheBaseline;
    public AudioSource okHouse;


    // Start is called before the first frame update
    void Start()
    {
        hummingTheBaseline.Play();
        currentTrack = tracks[0];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }


    public void ChangeTrackBackward()
    {
        if (currentTrack == tracks[1])
        {
            changeTrackBackward?.PlayFeedbacks();
            currentTrack = tracks[0];
            okHouse.Stop();
            hummingTheBaseline.Play();
        }

    }


    public void ChangeTrackForward()
    {
        if (currentTrack == tracks[0])
        {
            changeTrackForward?.PlayFeedbacks();
            currentTrack = tracks[1];
            hummingTheBaseline.Stop();
            okHouse.Play();
        }
    }
}
