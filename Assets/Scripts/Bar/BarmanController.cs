using System.Collections.Generic;
using UnityEngine;

public class BarmanController : MonoBehaviour
{
    public GameObject menuPanel;
    private Queue<ClientController> commandesEnAttente = new Queue<ClientController>();
    private ClientController commandePrioritaire;
    public GameManager gameManager; // Assurez-vous de définir cette référence dans l'inspecteur Unity

    public void Start()
    {
        menuPanel.SetActive(false);
    }

    public void OnMouseDown()
    {
        menuPanel.SetActive(true);
        Debug.Log("Menu de boissons ouvert");
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
                gameManager.AugmenterScore(10); // Augmente le score de 10 points pour une bonne commande
            }
            else
            {
                Debug.Log("Mauvaise réponse !");
                gameManager.DiminuerScore(5); // Diminue le score de 5 points pour une mauvaise commande
            }
        }
        else
        {
            Debug.Log("Aucune commande prioritaire en attente !");
        }

        // Fermer le menu de boissons
        menuPanel.SetActive(false);
        if (commandePrioritaire != null)
            commandePrioritaire.QuitterZoneBarman();
        // Fait sortir le client de la zone du barman

        // Réinitialiser la commande prioritaire et passer à la suivante dans la file d'attente
        commandePrioritaire = null;
        TraiterCommandes();
    }
}