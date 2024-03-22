using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialMenuController : MonoBehaviour
{
    public GameObject tutorialMenu;
    public GameObject tutoImage;

    public AudioClip son;

    // Méthode appelée lorsque le bouton est cliqué
    public void OpenTutorialMenu(GameObject whichImage)
    {
        SFXManager.instance.PlaySoundFXClip(son, transform, 1f);
        tutoImage = whichImage;
        // Activer le menu du tutoriel
        tutoImage.SetActive(true);

        // Mettre le jeu en pause
        Time.timeScale = 0f;
    }

    public void QuitTutorialMenu(GameObject whichImage)
    {
        SFXManager.instance.PlaySoundFXClip(son, transform, 1f);
        tutoImage = whichImage;
        // Désactiver le menu du tutoriel
        tutoImage.SetActive(false);

        // Enlever la pause
        Time.timeScale = 1f;
    }

    public void OpenTutorialSlide(GameObject whichImage)
    {
        SFXManager.instance.PlaySoundFXClip(son, transform, 1f);
        tutoImage = whichImage;
        // Activer le menu du tutoriel
        tutoImage.SetActive(true);
    }
    public void QuitTutorialSlide(GameObject whichImage)
    {
        SFXManager.instance.PlaySoundFXClip(son, transform, 1f);
        tutoImage = whichImage;
        // Désactiver le menu du tutoriel
        tutoImage.SetActive(false);

    }
}
