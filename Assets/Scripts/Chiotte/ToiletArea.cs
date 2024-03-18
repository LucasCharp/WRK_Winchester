using UnityEngine;

public class ToiletArea : MonoBehaviour
{
    public Toilet[] toilets; // Tableau des toilettes dans la zone
    public GameObject[] npcs; // Tableau des PNJ qui peuvent utiliser les toilettes
    public MoneyManager moneyManager;

    public void OnTriggerEnter(Collider other)
    {
        // V�rifier si le PNJ entre dans la zone des toilettes
        if (IsNPC(other.gameObject))
        {
            Debug.Log("il est entr�");
            // V�rifier si au moins un toilette est disponible
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
                Debug.Log("toilette avaible");
                // Choisir un toilette disponible au hasard
                Toilet availableToilet = GetAvailableToilet();
                if (availableToilet != null)
                {
                    moneyManager.moneyChange = (5);
                    moneyManager.OnMoneyChange();
                    // Utiliser le toilette
                    availableToilet.UseToilet();
                    Debug.Log("toilette use");
                }
            }
            else
            {
                Debug.Log("Tous les toilettes sont occup�s ou sales.");
            }
        }
    }

    private bool IsNPC(GameObject obj)
    {
        // V�rifier si l'objet est un PNJ
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