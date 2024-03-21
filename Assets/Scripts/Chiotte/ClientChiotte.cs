using UnityEngine;

public class ClientChiotte : MonoBehaviour
{
    public MoneyManager moneyManager;
    public Toilet[] toilets; // Tableau des toilettes dans la zone
    public GameManager gameManager;
    public ToiletArea toiletArea;
    private int chiotteCost = 15;

    // Coroutine pour attendre dans la zone des toilettes
    public System.Collections.IEnumerator WaitInToiletZone()
    {
        if (toiletArea.inChiotteZone == true)
        {
            float waitTime = 17f;
            while (waitTime > 0)
            {
                // Vérifier périodiquement s'il y a un toilette disponible
                bool toiletAvailable = toiletArea.IsToiletAvailable();
                if (toiletAvailable)
                {
                    // Si un toilette est disponible, utiliser les toilettes et arrêter la coroutine
                    UseToilet();
                    yield break;
                }
                else
                {
                    // Attendre une petite période avant de vérifier à nouveau
                    yield return new WaitForSeconds(1f);
                    waitTime -= 1f;
                }
            }
        }
        // Aucun toilette disponible après 17 secondes, le PNJ part
        gameManager.AugmenterScore(-12); // Perdre 20 points de score
    }

    // Méthode pour gérer l'utilisation des toilettes par le PNJ
    public void UseToilet()
    {
        Toilet availableToilet = toiletArea.GetAvailableToilet();
        print(availableToilet + "33333333");
        if (availableToilet != null)
        {
            moneyManager.moneyChange = chiotteCost;
            moneyManager.OnMoneyChange();
            availableToilet.UseToilet(); // Utiliser le toilette disponible
        }
    }
}