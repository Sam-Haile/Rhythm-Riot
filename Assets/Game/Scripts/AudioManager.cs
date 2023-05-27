using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource music;

    public AudioSource spraySound;

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
