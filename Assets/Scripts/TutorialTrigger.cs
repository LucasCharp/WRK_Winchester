using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public TutorialManager tutorialManager;
    public bool tutorialActive = false;

    public void ActivateOrNot()
    {
        if (!tutorialActive)
        {
            tutorialManager.ShowTutorial();
            tutorialActive = true;
        }
    }
}
