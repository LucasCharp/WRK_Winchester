using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogues : MonoBehaviour
{
    public Image[] dialogueImages;
    public Button retourButton;
    public Button dialogueButton;
    public MainSceneManager mainSceneManager;
    private bool doOnce = false;

    private void Update()
    {
        if (mainSceneManager.startGame == true && doOnce == false)
        {
            StartCoroutine(ActivateDialogueButtonAfterDelay());
            doOnce = true;
        }
    }

    IEnumerator ActivateDialogueButtonAfterDelay()
    {
        float delay = Random.Range(30f, 60f);
        yield return new WaitForSeconds(delay);

        dialogueButton.gameObject.SetActive(true);
        yield return new WaitForSeconds(7f); // Attendre 7 secondes

        if (!dialogueButton.GetComponent<DialogueButton>().clicked) // V�rifier si le bouton n'a pas �t� cliqu�
        {
            dialogueButton.gameObject.SetActive(false);
            dialogueButton.GetComponent<DialogueButton>().clicked = false;
            StartCoroutine(ActivateDialogueButtonAfterDelay()); // Relancer la coroutine
        }
    }


    public void SayDialogueHumain()
    {
        dialogueButton.gameObject.SetActive(false);
        retourButton.gameObject.SetActive(true);

        // Choisir un index de dialogue al�atoire
        int randomIndex = Random.Range(0, dialogueImages.Length);

        // D�sactiver toutes les images de dialogue
        foreach (Image image in dialogueImages)
        {
            image.gameObject.SetActive(false);
        }

        // Activer l'image de dialogue al�atoire
        dialogueImages[randomIndex].gameObject.SetActive(true);
    }

    public void OnRetourCliqued()
    {
        retourButton.gameObject.SetActive(false);
        foreach (Image image in dialogueImages)
        {
            image.gameObject.SetActive(false);
        }
        StartCoroutine(ActivateDialogueButtonAfterDelay());
    }
}
