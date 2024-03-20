using UnityEngine;

public class ToiletArea : MonoBehaviour
{
    public Toilet[] toilets; // Tableau des toilettes dans la zone
    public GameManager gameManager;
    private Coroutine waitingCoroutine;
    private ClientChiotte lesClientsDesChiottes;
    public bool inChiotteZone = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PNJ"))
        {
            inChiotteZone = true;
            // V�rifier s'il y a un toilette disponible imm�diatement
            bool toiletAvailable = IsToiletAvailable();
            Debug.Log(IsToiletAvailable());
            if (toiletAvailable)
            {
                lesClientsDesChiottes.UseToilet();
            }
            else
            {
                // Si aucun toilette n'est disponible imm�diatement, d�marrer la coroutine d'attente
                waitingCoroutine = StartCoroutine(lesClientsDesChiottes.WaitInToiletZone());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PNJ"))
        {
            inChiotteZone = false;
            // Arr�ter la coroutine si le PNJ quitte la zone des toilettes
            if (waitingCoroutine != null)
            {
                StopCoroutine(waitingCoroutine);
            }
        }
    }
    public bool IsToiletAvailable()
    {
        // V�rifier si au moins un toilette est disponible
        foreach (var toilet in toilets)
        {
            if (!toilet.isOccupied && !toilet.isDirty)
            {
                return true;
            }
        }
        return false;
    }

    public Toilet GetAvailableToilet()
    {
        // R�cup�rer un toilette disponible au hasard
        foreach (var toilet in toilets)
        {
            if (!toilet.isOccupied && !toilet.isDirty)
            {
                return toilet;
            }
        }
        return null;
    }
}