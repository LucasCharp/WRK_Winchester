using UnityEngine;

public class ToiletArea : MonoBehaviour
{
    public Toilet[] toilets; // Tableau des toilettes dans la zone
    public GameManager gameManager;

    public bool IsToiletAvailable()
    {
        // V�rifier si au moins un toilette est disponible
        foreach (var toilet in toilets)
        {
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