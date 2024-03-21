using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogues : MonoBehaviour
{
    public Image[] dialogueImages;
    private Dictionary<string, Image> humanDialoguesImages = new Dictionary<string, Image>();
    public string dialogue = "";
    private void Start()
    {
        humanDialoguesImages.Add("1",dialogueImages[0]);
        humanDialoguesImages.Add("2",dialogueImages[1]);
        humanDialoguesImages.Add("3",dialogueImages[2]);
        humanDialoguesImages.Add("4",dialogueImages[3]);
        humanDialoguesImages.Add("5",dialogueImages[4]);
        humanDialoguesImages.Add("6",dialogueImages[5]);
        humanDialoguesImages.Add("7",dialogueImages[6]);
        humanDialoguesImages.Add("8",dialogueImages[7]);
        humanDialoguesImages.Add("9",dialogueImages[8]);
        humanDialoguesImages.Add("10",dialogueImages[9]);
        humanDialoguesImages.Add("11",dialogueImages[10]);
        humanDialoguesImages.Add("12",dialogueImages[11]);
        humanDialoguesImages.Add("13",dialogueImages[12]);
        humanDialoguesImages.Add("14",dialogueImages[13]);
        humanDialoguesImages.Add("15",dialogueImages[14]);
    }
    public void SayDialogueHumain()
    {
        // Lancer la coroutine pour afficher les dialogues aléatoirement
        StartCoroutine(DisplayRandomDialogue());
        Debug.Log("Je suis la fonction qui lance la coroutine, et je l'ai lancée, n'est elle pas chouette ?");
    }

    IEnumerator DisplayRandomDialogue()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            Debug.Log("Je suis la coroutine qui vient définir le texte, n'est-ce pas fabuleux ? ?");
            // Vérifier s'il y a des répliques disponibles
            //if (dialogueLines.Length > 0)
            //{
               
                string[] dialogueLines = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15" };
                // Sélectionner un index aléatoire dans le tableau de répliques
                dialogue = dialogueLines[Random.Range(0, dialogueLines.Length)]; 
                Image imageDialogue = humanDialoguesImages[dialogue];
                imageDialogue.gameObject.SetActive(true);
                Debug.Log(imageDialogue.ToString());
            //}
            //else
            //{
                //Debug.LogError("Aucune réplique n'est disponible pour ce PNJ !");
            //}
        }
    }
}
