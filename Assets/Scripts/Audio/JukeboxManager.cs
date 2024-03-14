using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeboxManager : MonoBehaviour
{
    public AudioClip[] audioTracks;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    public void PlayTrack(int trackIndex)
    {
        if (trackIndex >= 0 && trackIndex < audioTracks.Length)
        {
            audioSource.clip = audioTracks[trackIndex];
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Invalid track index");
        }
    }

    public void TogglePause()
    {
        // V�rifiez si la musique est en pause ou en lecture et inversez son �tat
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }
    }
}