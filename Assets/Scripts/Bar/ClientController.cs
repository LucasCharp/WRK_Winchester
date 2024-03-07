using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientController : MonoBehaviour
{
    private bool inBarZone = false;
    private string commande = "";
    private bool commandeFulfilled = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BarmanZone"))
        {
            inBarZone = true;
            DemanderBoisson();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BarmanZone"))
        {
            inBarZone = false;
            commandeFulfilled = false;
        }
    }

    private void DemanderBoisson()
    {
        if (!commandeFulfilled)
        {
            BarmanController barman = FindObjectOfType<BarmanController>();
            if (barman != null)
            {
                string[] boissons = { "Cocktail", "Bière", "Vin", "Soda" };
                commande = boissons[Random.Range(0, boissons.Length)];
                Debug.Log("Commande reçue : " + commande);
            }
        }
    }

    public void CommanderBoisson(string nomBoisson)
    {
        if (inBarZone && !commandeFulfilled)
        {
            if (nomBoisson == commande)
            {
                Debug.Log("Commande complétée !");
                commandeFulfilled = true;
            }
        }
    }
}