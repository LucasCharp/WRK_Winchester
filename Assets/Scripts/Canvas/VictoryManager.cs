using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    public AudioSource[] audioSources;
    public AudioClip[] audioClips;

    public void PlayVictorySound()
    {
        audioSources[0].clip = audioClips[0];
        audioSources[0].Play();
    }

    public void PlayLooseSound()
    {
        audioSources[1].clip = audioClips[1];
        audioSources[1].Play();
    }

    public void OnQuitCliqued()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
