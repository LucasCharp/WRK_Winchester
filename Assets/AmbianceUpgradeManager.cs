using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbianceUpgradeManager : MonoBehaviour
{
   public AudioSource ambianceAudioSource;
   public AudioClip music;

    public void TogglePause()
    {
        // V�rifiez si la musique est en pause ou en lecture et inversez son �tat
        if (ambianceAudioSource.isPlaying)
        {
            ambianceAudioSource.Pause();
        }
        else
        {
            ambianceAudioSource.UnPause();
        }
    }


    public void AutoDestroy()
    {
        gameObject.SetActive(false);
    }
}
