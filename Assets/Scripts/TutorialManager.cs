using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorialImages;
    private int currentIndex = 0;

    public TutorialTrigger tutorialTrigger;
    public GameObject[] interractables;

    void Update()
    {
        if (tutorialTrigger.tutorialActive && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseTutorial();
        }
    }

    public void ShowTutorial()
    {
        if (!tutorialTrigger.tutorialActive)
        {
            Debug.Log("vas te faire enculer sale pute");
            Time.timeScale = 0f; // Met le jeu en pause
            tutorialImages[currentIndex].SetActive(true);
            
        }
    }

    public void CloseTutorial()
    {
        Debug.Log("ntm gros fdp");
        Time.timeScale = 1f; // Reprend le jeu
        tutorialImages[currentIndex].SetActive(false);
        currentIndex++;

        if (currentIndex >= tutorialImages.Length)
        {
            currentIndex = 0;
            tutorialTrigger.tutorialActive = false;
        }
    }
}