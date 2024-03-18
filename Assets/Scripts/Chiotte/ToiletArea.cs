using UnityEngine;

public class ToiletArea : MonoBehaviour
{
    public Toilet[] toilets; // Tableau des toilettes dans la zone
    public GameObject[] npcs; // Tableau des PNJ qui peuvent utiliser les toilettes

    public void OnTriggerEnter(Collider other)
    {
        // Vérifier si le PNJ entre dans la zone des toilettes
        if (IsNPC(other.gameObject))
        {
            // Vérifier si au moins un toilette est disponible
            bool isAnyToiletAvailable = false;
            foreach (var toilet in toilets)
            {
                if (!toilet.isOccupied && !toilet.isDirty)
                {
                    isAnyToiletAvailable = true;
                    break;
                }
            }

            if (isAnyToiletAvailable)
            {
                // Choisir un toilette disponible au hasard
                Toilet availableToilet = GetAvailableToilet();
                if (availableToilet != null)
                {
                    // Utiliser le toilette
                    availableToilet.UseToilet();
                }
            }
            else
            {
                Debug.Log("Tous les toilettes sont occupés ou sales.");
            }
        }
    }

    private bool IsNPC(GameObject obj)
    {
        // Vérifier si l'objet est un PNJ
        foreach (var npc in npcs)
        {
            if (obj == npc)
            {
                return true;
            }
        }
        return false;
    }

    private Toilet GetAvailableToilet()
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