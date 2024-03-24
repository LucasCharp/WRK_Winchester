using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BarmanController : MonoBehaviour
{
    public GameObject menuPanel;
    private Queue<ClientController> commandesEnAttente = new Queue<ClientController>();
    private ClientController commandePrioritaire;
    public GameManager gameManager; // Assurez-vous de définir cette référence dans l'inspecteur Unity-
    public MainSceneManager mainSceneManager;
    public TextMeshProUGUI boissprio;
    string resultString = "Aucun";

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
    TraiterCommandes();
    }

    public void TraiterCommandes()
    {
        if (commandePrioritaire != null && !commandePrioritaire.EstCommandeFulfilled())
        {
            Debug.Log("Commande prioritaire : " + commandePrioritaire.commande);
            boissprio.text = commandePrioritaire.commande;
            boissprio.ForceMeshUpdate();
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
        Debug.Log(mainSceneManager.barLevel);

        if (commandePrioritaire != null && mainSceneManager.barLevel != 0)
        {
            // Vérification si la boisson est correcte ou non
            if (nomBoisson == commandePrioritaire.commande)
            {
                Debug.Log("Bonne commande !");
                mainSceneManager.RemoveDrink();
            }
            else
            {
                Debug.Log("Mauvaise réponse !");
            }
            // Modifier l'appel à LivrerBoisson pour passer un booléen
            commandePrioritaire.LivrerBoisson(nomBoisson == commandePrioritaire.commande);
        }
        else
        {
            Debug.Log("Aucune commande prioritaire en attente !");
        }

        if (commandePrioritaire != null)
            commandePrioritaire.QuitterZoneBarman();
        commandePrioritaire = null;
        boissprio.text = resultString;
        boissprio.ForceMeshUpdate();
        TraiterCommandes();
    }
}