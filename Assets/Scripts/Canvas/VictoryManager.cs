using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{

    public AudioClip[] audioClips;

    public void PlayVictorySound()
    {
        SFXManager.instance.PlaySoundFXClip(audioClips[0], transform, 1f);
    }

    public void PlayLooseSound()
    {
        SFXManager.instance.PlaySoundFXClip(audioClips[1], transform, 1f);
    }

    public void OnQuitCliqued()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
