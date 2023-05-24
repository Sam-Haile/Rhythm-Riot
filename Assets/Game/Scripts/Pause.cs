using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    bool isActive = false;
    public MMFeedbacks pauseFeedback;
    private void Start()
    {
        pauseMenu.SetActive(false); // Initially deactivate the pause menu
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isActive = true;
        pauseFeedback?.PlayFeedbacks();
    }

    public void Resume()
    {
        Debug.Log("RESUMED");
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        isActive = false;
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;

    }

    public void Settings()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isActive)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
    }


}
