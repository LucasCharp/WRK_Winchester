using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialMenuController : MonoBehaviour
{
    public GameObject tutorialMenu;
    public GameObject tutoImage;

    public AudioClip son;

    public MainSceneManager mainSceneManager;
    public GameObject startCanvas;
    public GameObject playCanvas;

    // M�thode appel�e lorsque le bouton est cliqu�
    public void OpenTutorialMenu(GameObject whichImage)
    {
        SFXManager.instance.PlaySoundFXClip(son, transform, 1f);
        tutoImage = whichImage;
        // Activer le menu du tutoriel
        tutoImage.SetActive(true);

        // Mettre le jeu en pause
        Time.timeScale = 0f;

        if (mainSceneManager.startGame == false)
        {
            startCanvas.SetActive(false);
        }
        else
        {
            playCanvas.SetActive(false);
        }
    }

    public void QuitTutorialMenu(GameObject whichImage)
    {
        SFXManager.instance.PlaySoundFXClip(son, transform, 1f);
        tutoImage = whichImage;
        // D�sactiver le menu du tutoriel
        tutoImage.SetActive(false);

        // Enlever la pause
        Time.timeScale = 1f;

        if (mainSceneManager.startGame==false)
        {
            startCanvas.SetActive(true);
        }
        else
        {
            playCanvas.SetActive(true);
        }
    }

    public void OpenTutorialSlide(GameObject whichImage)
    {
        Time.timeScale = 0f;
        SFXManager.instance.PlaySoundFXClip(son, transform, 1f);
        tutoImage = whichImage;
        // Activer le menu du tutoriel
        tutoImage.SetActive(true);
    }
    public void QuitTutorialSlide(GameObject whichImage)
    {
        Time.timeScale = 1f;
        SFXManager.instance.PlaySoundFXClip(son, transform, 1f);
        tutoImage = whichImage;
        // D�sactiver le menu du tutoriel
        tutoImage.SetActive(false);

    }
}
