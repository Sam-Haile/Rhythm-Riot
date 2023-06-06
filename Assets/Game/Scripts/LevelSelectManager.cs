using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{

    public MMFeedbacks[] trackTransitions;
    public AudioSource[] music;
    public GameObject[] tracks;
    private GameObject currentTrack;

    void Start()
    {
        currentTrack = tracks[0];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainMenu();
        }
    }

    /// <summary>
    /// Returns to the previous track in the list
    /// </summary>
    public void ChangeTrackBackward()
    {
        if (currentTrack == tracks[1])
        {
            trackTransitions[3]?.PlayFeedbacks();
            currentTrack = tracks[0];
            StopMusic();
            music[0].Play();
        }
        else if (currentTrack == tracks[2])
        {
            trackTransitions[2]?.PlayFeedbacks();
            currentTrack = tracks[1];
            StopMusic();
            music[1].Play();
        }
    }

    /// <summary>
    /// Displays the next track in the list
    /// </summary>
    public void ChangeTrackForward()
    {
        if (currentTrack == tracks[0])
        {
            trackTransitions[0]?.PlayFeedbacks();
            currentTrack = tracks[1];
            StopMusic();
            music[1].Play();
        }
        else if (currentTrack == tracks[1])
        {
            trackTransitions[1]?.PlayFeedbacks();
            currentTrack = tracks[2];
            StopMusic();
            music[2].Play();
        }
    }

    /// <summary>
    /// Stops all songs
    /// </summary>
    private void StopMusic()
    {
        foreach (AudioSource song in music)
        {
            song.Stop();
        }
    }

    /// <summary>
    /// Returns to the main menu
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
