using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenuController : MonoBehaviour
{
    public GameObject tutorialMenu;
    public GameObject tutoImage;

    // Méthode appelée lorsque le bouton est cliqué
    public void OpenTutorialMenu()
    {
        // Activer le menu du tutoriel
        tutorialMenu.SetActive(true);

        // Mettre le jeu en pause
        Time.timeScale = 0f;
    }

    public void QuitTutorialMenu()
    {
        // Activer le menu du tutoriel
        tutorialMenu.SetActive(false);

        // Mettre le jeu en pause
        Time.timeScale = 1f;
    }
}
