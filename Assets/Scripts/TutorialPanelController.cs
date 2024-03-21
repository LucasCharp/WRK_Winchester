using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanelController : MonoBehaviour
{
    // Méthode appelée lorsque le bouton est cliqué
    public void CloseTutorialPanel()
    {
        // Désactiver le menu de tutoriel
        gameObject.SetActive(false);

        // Reprendre le jeu
        Time.timeScale = 1f;
    }
}
