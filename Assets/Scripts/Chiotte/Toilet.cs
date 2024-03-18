using UnityEngine;
using System.Collections;

public class Toilet : MonoBehaviour
{
    public bool isOccupied = false;
    public bool isDirty = false;
    private int cleaningClicksRequired;
    public GameManager gameManager;

    // Méthode appelée lorsque le toilette est utilisé
    public void UseToilet()
    {
        if (!isOccupied && !isDirty)
        {
            isOccupied = true;
            StartCoroutine(UseToiletRoutine());
            Debug.Log("maintenant il  chie");
        }
    }

    // Coroutine pour simuler le temps passé par le PNJ devant le toilette
    private IEnumerator UseToiletRoutine()
    {
        yield return new WaitForSeconds(6f); // Attendre 6 secondes
        isOccupied = false; // Le PNJ quitte le toilette
        isDirty = true; // Le toilette devient sale après utilisation
        StartCleaning();
        Debug.Log("la il à bien fait caca");
    }

    private void OnMouseDown()
    {
        CleanToilet();
    }

    // Méthode appelée lorsqu'on tente de nettoyer le toilette
    public void CleanToilet()
    {
        Debug.Log("tu as lancé un clean");
        if (isDirty)
        {
            // Réduire le nombre de clics nécessaires pour le nettoyage
            cleaningClicksRequired--;
            if (cleaningClicksRequired <= 0)
            {
                isDirty = false; // Le toilette est maintenant propre
                Debug.Log("Le toilette est propre !");
                gameManager.AugmenterScore(30);
            }
            else
            {
                Debug.Log("Encore " + cleaningClicksRequired + " clics pour nettoyer le toilette.");
            }
        }
        else
        {
            Debug.Log("Le toilette n'est pas sale.");
        }
    }

    // Méthode pour initialiser le nettoyage du toilette avec un nombre aléatoire de clics requis
    public void StartCleaning()
    {
        cleaningClicksRequired = Random.Range(3, 8); // Nombre aléatoire de clics requis entre 3 et 7
        Debug.Log("Commencer le nettoyage du toilette. " + cleaningClicksRequired + " clics nécessaires.");
    }
}