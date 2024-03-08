using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientController : MonoBehaviour
{
    private bool inBarZone = false;
    public string[] boissons;
    public string commande = new string("");
    private bool commandeFulfilled = false;
    private BarmanController barmanController;

    public string commandeClient;

    private void Start()
    {
        barmanController = FindObjectOfType<BarmanController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BarmanZone"))
        {
            inBarZone = true;
            if (barmanController != null)
                if (commandeFulfilled == false)
                {
                    string[] boissons = { "t'es qui la", "Rome", "abe-sainte", "Mot riz tôt", "vaux 2 k" };
                    string commande = boissons[Random.Range(0, boissons.Length)];
                    Debug.Log("Commande ajoutée : " + commande);
                    commandeClient = commande;
                    barmanController.AjouterCommande(this);
                }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BarmanZone"))
        {
            inBarZone = false;
        }
    }

    public void LivrerBoisson(string nomBoisson)
    {
        Debug.Log("leboutonfaitilvrer");
        if (inBarZone && !commandeFulfilled)
        {
            Debug.Log(nomBoisson + commandeClient);
            if (nomBoisson == commandeClient)
            {
                Debug.Log("Commande complétée : " + commande);
                barmanController.TraiterCommandes();
            }
        }
    }

    public bool EstCommandeFulfilled()
    {
        return commandeFulfilled;
    }
}
