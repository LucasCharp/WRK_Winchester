using UnityEngine;

public class ClientChiotte : MonoBehaviour
{
    public Toilet[] toilets; // Tableau des toilettes dans la zone
    public ToiletArea toiletArea;
    public static ClientChiotte instance;
    private MoneyManager moneyManager = new MoneyManager();
    private bool unique;

    // Coroutine pour attendre dans la zone des toilettes
    public System.Collections.IEnumerator WaitInToiletZone()
    {
        if (toiletArea.inChiotteZone == true)
        {
            float waitTime = 17f;
            while (waitTime > 0)
            {
                // V�rifier p�riodiquement s'il y a un toilette disponible
                bool toiletAvailable = toiletArea.IsToiletAvailable();
                if (toiletAvailable)
                {
                    // Si un toilette est disponible, utiliser les toilettes et arr�ter la coroutine
                    UseToilet();
                    yield break;
                }
                else
                {
                    // Attendre une petite p�riode avant de v�rifier � nouveau
                    yield return new WaitForSeconds(1f);
                    waitTime -= 1f;
                }
            }
        }
        // Aucun toilette disponible apr�s 17 secondes, le PNJ part
        GameManager.instance.AugmenterScore(-12); // Perdre 20 points de score
    }

    // M�thode pour g�rer l'utilisation des toilettes par le PNJ
    public void UseToilet()
    {
        if (unique == false)
        {
            unique = true;
            ToiletArea toiletArea = FindObjectOfType<ToiletArea>(); // Trouver l'instance de ToiletArea dans la sc�ne
            if (toiletArea != null)
            {
                Debug.Log("check toil");
                Toilet availableToilet = toiletArea.GetAvailableToilet(); // Obtenir un toilette disponible
                if (availableToilet != null)
                {
                    moneyManager.moneyChange = 15;
                    moneyManager.OnMoneyChange();
                    availableToilet.UseToilet(); // Utiliser le toilette disponible
                }
            }
        }
        else
        {
            Debug.LogWarning("ToiletArea non trouv�e dans la sc�ne !");
        }
    }
}