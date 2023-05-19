using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    bool isActive = false;

    private void Start()
    {
        pauseMenu.SetActive(false); // Initially deactivate the pause menu
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isActive = true;
    }

    public void Resume()
    {
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
                Debug.Log("ON");
                Resume();
            }
            else
            {
                Debug.Log("OFF");
                PauseGame();
            }
        }
    }


}
