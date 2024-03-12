using System.Collections.Generic;
using UnityEngine;

public class BarmanController : MonoBehaviour
{
    public GameObject menuPanel;
    private Queue<ClientController> commandesEnAttente = new Queue<ClientController>();
    private ClientController commandePrioritaire;

    private void Start()
    {
        menuPanel.SetActive(false);
    }

    private void OnMouseDown()
    {
        menuPanel.SetActive(true);
        Debug.Log("true");
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
        if (commandePrioritaire != null && !commandePrioritaire.EstCommandeFulfilled())
        {
            if (commandePrioritaire.commande == nomBoisson)
            {
                Debug.Log("Bonne commande !");
                commandePrioritaire.LivrerBoisson(true);
            }
            else
            {
                Debug.Log("Mauvaise réponse !");
                commandePrioritaire.LivrerBoisson(false);
            }
            commandePrioritaire = null;
            menuPanel.SetActive(false);
        }
    }
}