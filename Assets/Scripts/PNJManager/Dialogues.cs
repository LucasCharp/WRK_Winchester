using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogues : MonoBehaviour
{
    public string[] dialogueLines; // Tableau de répliques pour ce PNJ
    public float minTimeBetweenDialogues = 5f; // Temps minimum entre chaque dialogue
    public float maxTimeBetweenDialogues = 10f; // Temps maximum entre chaque dialogue

    void Start()
    {
        // Lancer la coroutine pour afficher les dialogues aléatoirement
        StartCoroutine(DisplayRandomDialogue());
    }

    IEnumerator DisplayRandomDialogue()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeBetweenDialogues, maxTimeBetweenDialogues));

            // Vérifier s'il y a des répliques disponibles
            if (dialogueLines.Length > 0)
            {
                // Sélectionner un index aléatoire dans le tableau de répliques
                int randomIndex = Random.Range(0, dialogueLines.Length);

                // Afficher la réplique sélectionnée
                Debug.Log(dialogueLines[randomIndex]);
            }
            else
            {
                Debug.LogError("Aucune réplique n'est disponible pour ce PNJ !");
            }
        }
    }
}
