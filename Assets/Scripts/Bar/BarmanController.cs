using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarmanController : MonoBehaviour
{
    public GameObject menuPanel;
    public Text infoText; // Texte pour afficher le r�sultat
    private Queue<ClientController> commandesEnAttente = new Queue<ClientController>();
    private ClientController commandePrioritaire;

    private void Start()
    {
        menuPanel.SetActive(false);
    }

    private void OnMouseDown()
    {
        menuPanel.SetActive(true);
    }

    public void AjouterCommande(ClientController client)
    {
        if (commandePrioritaire == null)
        {
            commandePrioritaire = client;
        }
        else
        {
            commandesEnAttente.Enqueue(client);
        }
    }

    public void TraiterCommandes()
    {
        if (commandePrioritaire != null && !commandePrioritaire.EstCommandeFulfilled())
        {
            // Traitement de la commande prioritaire si elle n'est pas encore compl�t�e
            Debug.Log("Commande prioritaire : " + commandePrioritaire.commande);
        }
        else if (commandesEnAttente.Count > 0)
        {
            // Passage � la commande suivante en attente si la commande prioritaire est compl�t�e
            commandePrioritaire = commandesEnAttente.Dequeue();
            Debug.Log("Commande secondaire en attente de la priorit� : " + commandePrioritaire.commande);
        }
    }

    public void ServirBoisson(string nomBoisson)
    {
        // V�rifie si la boisson servie est correcte
        if (commandePrioritaire != null && !commandePrioritaire.EstCommandeFulfilled())
        {
            if (commandePrioritaire.commande == nomBoisson)
            {
                infoText.text = "Bonne commande !";
                Debug.Log("Bonne commande !");
            }
            else
            {
                infoText.text = "Mauvaise r�ponse !";
                Debug.Log("Mauvaise r�ponse !");
            }
            // Marquer la commande comme compl�t�e
            commandePrioritaire.LivrerBoisson(nomBoisson);
            // Sortir le client de la zone du barman
            commandePrioritaire = null;
            menuPanel.SetActive(false);
        }
    }
}