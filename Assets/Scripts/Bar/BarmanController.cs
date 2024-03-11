using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarmanController : MonoBehaviour
{
    public GameObject menuPanel;
    public Text infoText; // Texte pour afficher le résultat
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
            // Traitement de la commande prioritaire si elle n'est pas encore complétée
            Debug.Log("Commande prioritaire : " + commandePrioritaire.commande);
        }
        else if (commandesEnAttente.Count > 0)
        {
            // Passage à la commande suivante en attente si la commande prioritaire est complétée
            commandePrioritaire = commandesEnAttente.Dequeue();
            Debug.Log("Commande secondaire en attente de la priorité : " + commandePrioritaire.commande);
        }
    }

    public void ServirBoisson(string nomBoisson)
    {
        // Vérifie si la boisson servie est correcte
        if (commandePrioritaire != null && !commandePrioritaire.EstCommandeFulfilled())
        {
            if (commandePrioritaire.commande == nomBoisson)
            {
                infoText.text = "Bonne commande !";
                Debug.Log("Bonne commande !");
            }
            else
            {
                infoText.text = "Mauvaise réponse !";
                Debug.Log("Mauvaise réponse !");
            }
            // Marquer la commande comme complétée
            commandePrioritaire.LivrerBoisson(nomBoisson);
            // Sortir le client de la zone du barman
            commandePrioritaire = null;
            menuPanel.SetActive(false);
        }
    }
}