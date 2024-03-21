using UnityEngine;

public class ToiletArea : MonoBehaviour
{
    public Toilet[] toilets; // Tableau des toilettes dans la zone
    public GameManager gameManager;
    private Coroutine waitingCoroutine;
    public ClientChiotte lesClientsDesChiottes;
    public bool inChiotteZone = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PNJ"))
        {
            inChiotteZone = true;
            // Récupérer le script ClientChiotte attaché au PNJ
            ClientChiotte clientChiotte = other.GetComponent<ClientChiotte>();
            lesClientsDesChiottes = clientChiotte;
            if (clientChiotte != null)
            {
                lesClientsDesChiottes.UseToilet();
            }
            else
            {
                Debug.LogWarning("Script ClientChiotte non trouvé sur le PNJ !");
                // Si aucun toilette n'est disponible immédiatement, démarrer la coroutine d'attente
                waitingCoroutine = StartCoroutine(lesClientsDesChiottes.WaitInToiletZone());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PNJ"))
        {
            inChiotteZone = false;
            // Arrêter la coroutine si le PNJ quitte la zone des toilettes
            if (waitingCoroutine != null)
            {
                StopCoroutine(waitingCoroutine);
            }
        }
    }
    public bool IsToiletAvailable()
    {
        // Vérifier si au moins un toilette est disponible
        foreach (var toilet in toilets)
        {
            if (!Toilet.instance.isOccupied && !Toilet.instance.isDirty)
            {
                return true;
            }
        }
        return false;
    }

    public Toilet GetAvailableToilet()
    {
        Debug.Log("oui3");
        // Récupérer un toilette disponible au hasard
        foreach (var toilet in toilets)
        {
            print("ouiouiuiuiui");
            print(Toilet.instance.isOccupied + "e");
            print(Toilet.instance.isDirty + "o");
            if (!Toilet.instance.isOccupied && !Toilet.instance.isDirty)
            {
                return toilet;
            }
        }
        return null;
    }
}