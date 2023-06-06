using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pause : MonoBehaviour
{
    bool isActive = false;
    public MMFeedbacks pauseTime;
    public MMFeedbacks resumeTime;
    public GameObject pauseScreen;
    public GameObject settingsScreen;
    public AudioSource music;

    public void PauseGame()
    {
        music.Pause();
        pauseScreen.SetActive(true);
        pauseTime?.PlayFeedbacks();
        isActive = true;
    }

    public void Resume()
    {
        music.Play();
        pauseScreen.SetActive(false);
        settingsScreen.SetActive(false);
        resumeTime?.PlayFeedbacks();
        isActive = false;
    }

    public void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        Debug.Log(Time.timeScale);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isActive)
            {
                PauseGame();
            }
            else if(isActive)
            {
                Resume();
            }
        }
    }


}
