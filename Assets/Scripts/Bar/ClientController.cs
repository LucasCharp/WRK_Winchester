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
                DemanderBoisson();
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

    private void DemanderBoisson()
    {
        if (!commandeFulfilled)
        {
            string[] boissons = { "t'es qui la", "vaux 2 k", "Rome", "Mot riz tôt", "abe-sainte" };
            commande = boissons[Random.Range(0, boissons.Length)];
            Debug.Log("Commande reçue : " + commande);
            Invoke("SimulerAttente", 10f);
        }
    }

    private void SimulerAttente()
    {
        if (!commandeFulfilled)
        {
            Debug.Log("Client insatisfait.");
            LivrerBoisson(false);
        }
    }

    public void LivrerBoisson(bool reponse)
    {
        if (inBarZone && !commandeFulfilled)
        {
            if (reponse)
            {
                Debug.Log("Merci !");
            }
            else
            {
                Debug.Log("Nul !");
            }
            commandeFulfilled = true;
        }
    }

    public bool EstCommandeFulfilled()
    {
        return commandeFulfilled;
    }
}