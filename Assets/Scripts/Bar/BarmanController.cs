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
    }

    public void VendreBoisson(string nomBoisson)
    {
        // Code pour vendre la boisson et marquer des points
        Debug.Log("Boisson vendue : " + nomBoisson);
        menuPanel.SetActive(false);
        commandePrioritaire = null;
    }

    public void AjouterCommande(ClientController client)
    {
        if (commandePrioritaire == null)
        {
            commandePrioritaire = client;
            Debug.Log(client);
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
            VendreBoisson(commandePrioritaire.commande);
            Debug.Log("Commande prioritaire vendue : ");
        }
        else if (commandesEnAttente.Count > 0)
        {
            // Passage à la commande suivante en attente si la commande prioritaire est complétée
            commandePrioritaire = commandesEnAttente.Dequeue();
            Debug.Log("Commande secondaire en attente de la priorité : ");
        }
    }
}