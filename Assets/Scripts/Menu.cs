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
    public Sprite[] titleScreenImages;
    public Image titleObject;

    public float duration = 1f;
    private int index = 0;
    private float timer = 0;
    private int numOfFrames;

    private void Start()
    {
        numOfFrames = titleScreenImages.Length;
    }


    private void Update()
    {
        
        if ((timer += Time.deltaTime) >= duration) 
        {
            timer = 0;
            
            if (index >= numOfFrames )
            {
                index = 0;
            }
            titleObject.sprite = titleScreenImages[index];
            index++;
        }

    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT GAME!");
    }




}
