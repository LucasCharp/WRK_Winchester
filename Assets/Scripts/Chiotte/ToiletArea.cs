using UnityEngine;

public class ToiletArea : MonoBehaviour
{
    public Toilet[] toilets; // Tableau des toilettes dans la zone
    public GameManager gameManager;

    public bool IsToiletAvailable()
    {
        // Vérifier si au moins un toilette est disponible
        foreach (var toilet in toilets)
        {
            Debug.Log("oui" + toilet.isOccupied);
            Debug.Log("oui" + toilet.isDirty);
            if (!toilet.isOccupied && !toilet.isDirty)
            {
                Debug.Log("toilettesOK");
                return true;
            }
        }
        Debug.Log("toilettesPasOK");
        return false;
    }

    public Toilet GetAvailableToilet()
    {
        // Récupérer un toilette disponible au hasard
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