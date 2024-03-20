using UnityEngine;

public class ClientChiotte : MonoBehaviour
{
    private bool inChiotteZone = false;
    public MoneyManager moneyManager;
    public Toilet[] toilets; // Tableau des toilettes dans la zone
    public GameManager gameManager;
    public ToiletArea toiletArea;
    private int ChiotteCost = 15;
    private Coroutine waitingCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ChiotteZone"))
        {
            inChiotteZone = true;
            // V�rifier s'il y a un toilette disponible imm�diatement
            bool toiletAvailable = toiletArea.IsToiletAvailable();
            Debug.Log(toiletArea.IsToiletAvailable());
            if (toiletAvailable)
            {
                UseToilet();
                Debug.Log("toilette utilis�es");
            }
            else
            {
                // Si aucun toilette n'est disponible imm�diatement, d�marrer la coroutine d'attente
                waitingCoroutine = StartCoroutine(WaitInToiletZone());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ChiotteZone"))
        {
            inChiotteZone = false;
            // Arr�ter la coroutine si le PNJ quitte la zone des toilettes
            if (waitingCoroutine != null)
            {
                StopCoroutine(waitingCoroutine);
            }
        }
    }

    // Coroutine pour attendre dans la zone des toilettes
    private System.Collections.IEnumerator WaitInToiletZone()
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
        // Aucun toilette disponible apr�s 17 secondes, le PNJ part
        gameManager.AugmenterScore(-20); // Perdre 20 points de score
    }

    // M�thode pour g�rer l'utilisation des toilettes par le PNJ
    public void UseToilet()
    {
        Toilet availableToilet = toiletArea.GetAvailableToilet();
        if (availableToilet != null)
        {
            moneyManager.moneyChange = ChiotteCost;
            moneyManager.OnMoneyChange();
            availableToilet.UseToilet(); // Utiliser le toilette disponible
        }
    }
}