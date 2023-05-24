using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource music;


    private void Awake()
    {
       
    }


    private void Update()
    {

    }

    public void PlayMusic()
    {
        music.Play();

    }
}
