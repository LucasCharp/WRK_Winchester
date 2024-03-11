using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientController : MonoBehaviour
{
    private bool inBarZone = false;
    public string commande = "";
    private bool commandeFulfilled = false;
    private BarmanController barmanController;

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
            {
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
        if (inBarZone && !commandeFulfilled)
        {
            if (nomBoisson == commande)
            {
                Debug.Log("Commande complétée : " + commande);
                commandeFulfilled = true;
                barmanController.TraiterCommandes();
            }
        }
    }

    public void SetCommande(string nomBoisson)
    {
        commande = nomBoisson;
    }

    public bool EstCommandeFulfilled()
    {
        return commandeFulfilled;
    }
}