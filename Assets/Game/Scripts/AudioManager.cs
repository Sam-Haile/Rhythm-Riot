using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource music;
    public AudioSource spraySound;
    public MMFeedbacks sprayCanFX;


    public void PlayMusic()
    {
        music.Play();
    }

    public void CanSoundEffect()
    {
        sprayCanFX?.PlayFeedbacks();
    }
}
