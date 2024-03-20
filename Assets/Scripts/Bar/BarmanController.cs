using System.Collections.Generic;
using UnityEngine;

public class BarmanController : MonoBehaviour
{
    public GameObject menuPanel;
    private Queue<ClientController> commandesEnAttente = new Queue<ClientController>();
    private ClientController commandePrioritaire;
    public GameManager gameManager; // Assurez-vous de définir cette référence dans l'inspecteur Unity-

    public void AjouterCommande(ClientController client, string commande)
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
            Debug.Log("Commande prioritaire : " + commandePrioritaire.commande);
        }
        else if (commandesEnAttente.Count > 0)
        {
            commandePrioritaire = commandesEnAttente.Dequeue();
            Debug.Log("Commande secondaire en attente de la priorité : " + commandePrioritaire.commande);
        }
    }

    public void ServirBoisson(string nomBoisson)
    {
        Debug.Log("Boisson vendue : " + nomBoisson);

        if (commandePrioritaire != null)
        {
            // Vérification si la boisson est correcte ou non
            if (nomBoisson == commandePrioritaire.commande)
            {
                Debug.Log("Bonne commande !");
            }
            else
            {
                Debug.Log("Mauvaise réponse !");
            }
            // Modifier l'appel à LivrerBoisson pour passer un booléen
        commandePrioritaire.LivrerBoisson(nomBoisson == commandePrioritaire.commande);
            Debug.Log("ouiu");
        }
        else
        {
            Debug.Log("Aucune commande prioritaire en attente !");
        }

        if (commandePrioritaire != null)
            commandePrioritaire.QuitterZoneBarman();
        // Fait sortir le client de la zone du barman

        // Réinitialiser la commande prioritaire et passer à la suivante dans la file d'attente
        commandePrioritaire = null;
        TraiterCommandes();
    }
}