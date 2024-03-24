using UnityEngine;

public class ClientChiotte : MonoBehaviour
{
    public Toilet[] toilets; // Tableau des toilettes dans la zone
    public ToiletArea toiletArea;
    public GameManager gameManager;
    private bool unique;

    private void Start()
    {
        GameObject gameManagerObject = GameObject.FindWithTag("GameManager");
        if (gameManagerObject != null)
        {
            gameManager = gameManagerObject.GetComponent<GameManager>();
        }
        GameObject moneyManagerObject = GameObject.FindWithTag("GameManager");
        if (gameManagerObject != null)
        {
            gameManager = moneyManagerObject.GetComponent<GameManager>();
        }
    }
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
        GameManager.instance.AugmenterScore(-12); // Perdre 20 points de score
    }

    // Méthode pour gérer l'utilisation des toilettes par le PNJ
    public void UseToilet()
    {
        unique = false; 
        if (unique == false)
        {
            Debug.Log("E1");
            unique = true;
            ToiletArea toiletArea = FindObjectOfType<ToiletArea>(); // Trouver l'instance de ToiletArea dans la scène
            if (toiletArea != null)
            {
                Debug.Log("E2");
                Toilet availableToilet = FindObjectOfType<Toilet>(); // Obtenir un toilette disponible
                if (availableToilet != null)
                {
                    Debug.Log("E3");
                    toiletArea.LanceInfo();
                    Debug.Log("E4");
                    availableToilet.UseToilet(); // Utiliser le toilette disponible
                }
            }
        }
        else
        {
            Debug.LogWarning("ToiletArea non trouvée dans la scène !");
        }
    }
}