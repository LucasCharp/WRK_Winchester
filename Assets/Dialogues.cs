using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogues : MonoBehaviour
{
    public string[] dialogueLines; // Tableau de r�pliques pour ce PNJ
    public float minTimeBetweenDialogues = 5f; // Temps minimum entre chaque dialogue
    public float maxTimeBetweenDialogues = 10f; // Temps maximum entre chaque dialogue

    void Start()
    {
        // Lancer la coroutine pour afficher les dialogues al�atoirement
        StartCoroutine(DisplayRandomDialogue());
    }

    IEnumerator DisplayRandomDialogue()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeBetweenDialogues, maxTimeBetweenDialogues));

            // V�rifier s'il y a des r�pliques disponibles
            if (dialogueLines.Length > 0)
            {
                // S�lectionner un index al�atoire dans le tableau de r�pliques
                int randomIndex = Random.Range(0, dialogueLines.Length);

                // Afficher la r�plique s�lectionn�e
                Debug.Log(dialogueLines[randomIndex]);
            }
            else
            {
                Debug.LogError("Aucune r�plique n'est disponible pour ce PNJ !");
            }
        }
    }
}
