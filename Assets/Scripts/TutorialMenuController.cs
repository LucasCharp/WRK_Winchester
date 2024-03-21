using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenuController : MonoBehaviour
{
    public GameObject tutorialMenu;
    public GameObject tutoImage;

    // M�thode appel�e lorsque le bouton est cliqu�
    public void OpenTutorialMenu(GameObject whichImage)
    {
        tutoImage = whichImage;
        // Activer le menu du tutoriel
        tutoImage.SetActive(true);

        // Mettre le jeu en pause
        Time.timeScale = 0f;
    }

    public void QuitTutorialMenu(GameObject whichImage)
    {
        tutoImage = whichImage;
        // D�sactiver le menu du tutoriel
        tutoImage.SetActive(false);

        // Enlever la pause
        Time.timeScale = 1f;
    }
}
