using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeboxManager : MonoBehaviour
{
    public AudioClip[] audioTracks;
    public AudioClip[] afroAudioTracks;
    public AudioClip[] brazilAudioTracks;
    public AudioClip[] countryAudioTracks;
    public AudioClip[] discoAudioTracks;
    public AudioClip[] drillAudioTracks;
    public AudioClip[] jazzAudioTracks;
    public AudioClip[] metalAudioTracks;
    public AudioClip[] rockAudioTracks;
    public AudioClip[] transitions;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    //public void PlayTrack(int trackIndex)
    //{
    //    if (trackIndex >= 0 && trackIndex < audioTracks.Length)
    //    {
    //        audioSource.clip = audioTracks[trackIndex];
    //        audioSource.Play();
    //    }
    //    else
    //    {
    //        Debug.LogError("Invalid track index");
    //    }
    //}

    public void PlayTrackAfro()
    {
        audioSource.clip = afroAudioTracks[Random.Range(0, afroAudioTracks.Length)];
        audioSource.Play();
    }
    public void PlayTrackBrazil()
    {
        audioSource.clip = brazilAudioTracks[Random.Range(0, brazilAudioTracks.Length)];
        audioSource.Play();
    }
    public void PlayTrackCountry()
    {
        audioSource.clip = countryAudioTracks[Random.Range(0, countryAudioTracks.Length)];
        audioSource.Play();
    }
    public void PlayTrackDisco()
    {
        audioSource.clip = discoAudioTracks[Random.Range(0, discoAudioTracks.Length)];
        audioSource.Play();
    }
    public void PlayTrackDrill()
    {
        audioSource.clip = drillAudioTracks[Random.Range(0, drillAudioTracks.Length)];
        audioSource.Play();
    }
    public void PlayTrackJazz()
    {
        audioSource.clip = jazzAudioTracks[Random.Range(0, jazzAudioTracks.Length)];
        audioSource.Play();
    }
    public void PlayTrackMetal()
    {
        audioSource.clip = metalAudioTracks[Random.Range(0, metalAudioTracks.Length)];
        audioSource.Play();
    }
    public void PlayTrackRock()
    {
        audioSource.clip = rockAudioTracks[Random.Range(0, rockAudioTracks.Length)];
        audioSource.Play();
    }

    public void TogglePause()
    {
        // Vérifiez si la musique est en pause ou en lecture et inversez son état
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }
    }

    public void TransitionNoise()
    {
        SFXManager.instance.PlaySoundFXClip(transitions[Random.Range(0, transitions.Length)], transform, 1f);
    }
}