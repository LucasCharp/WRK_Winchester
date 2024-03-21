using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanelController : MonoBehaviour
{
    // M�thode appel�e lorsque le bouton est cliqu�
    public void CloseTutorialPanel()
    {
        // D�sactiver le menu de tutoriel
        gameObject.SetActive(false);

        // Reprendre le jeu
        Time.timeScale = 1f;
    }
}
