using UnityEngine;

public class ClientController : MonoBehaviour
{
    private bool inBarZone = false;
    public string commande = "";
    private bool commandeFulfilled = false;
    private BarmanController barmanController;
    private bool satisfait = false;

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
    public void QuitterZoneBarman()
    {
        // Sortir de la zone du barman
        // Par exemple, désactiver le gameObject du client
        Destroy(gameObject);
        //gameObject.SetActive(false);
        // Arrêter la simulation d'attente si elle est en cours
        StopCoroutine("SimulerAttente");
        satisfait = true;
        Destroy(gameObject);
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
            Invoke("SimulerAttente", 40f);
        }
    }

    private void SimulerAttente()
    {
        if (!commandeFulfilled)
        {
            if (satisfait == false)
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