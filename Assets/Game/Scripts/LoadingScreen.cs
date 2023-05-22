using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;

public class LoadingScreen : MonoBehaviour
{
    public bool backGroundImageAndLoop;
    public float LoopTime;
    public GameObject[] backgroundImages;
    AsyncOperation async;
    public Image loadingBarFill;


    public void loadingScreen(int sceneNo)
    {
        this.gameObject.SetActive(true);
        StartCoroutine(Loading(sceneNo));
    }


    private void Start()
    {
        if (backGroundImageAndLoop)
            StartCoroutine(transitionImage());
    }


    // The pictures change according to the time of
    IEnumerator transitionImage()
    {
        for (int i = 0; i < backgroundImages.Length; i++)
        {
            yield return new WaitForSeconds(LoopTime);
            for (int j = 0; j < backgroundImages.Length; j++)
                backgroundImages[j].SetActive(false);
            backgroundImages[i].SetActive(true);
        }
    }

    // Activate the scene 
    IEnumerator Loading(int sceneNo)
    {
        async = SceneManager.LoadSceneAsync(sceneNo);
        async.allowSceneActivation = false;

        // Continue until the installation is completed
        while (async.isDone == false)
        {
            loadingBarFill.fillAmount = async.progress;

            if (async.progress == 0.9f)
            {
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
