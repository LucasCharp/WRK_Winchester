using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    [SerializeField] private AudioSource soundSFXObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //spawn in gameObject
        AudioSource audioSource = Instantiate(soundSFXObject, spawnTransform.position, Quaternion.identity);
            
        //assign the audioClip
        audioSource.clip = audioClip;

        //assign volume
        audioSource.volume = volume;

        //play sound
        audioSource.Play();

        //get lenght of sound FX clip
        float cliplenght = audioSource.clip.length;

        //destroy the clip after it is done playing
        Destroy(audioSource.gameObject, cliplenght );
    }
}
