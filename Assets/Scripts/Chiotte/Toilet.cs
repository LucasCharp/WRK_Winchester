using UnityEngine;
using System.Collections;

public class Toilet : MonoBehaviour
{
    public bool isOccupied = false;
    public bool isDirty = false;
    private int cleaningClicksRequired;
    public GameManager gameManager;
    public AudioClip ventouse;

    public void UseToilet()
    {
        if (!isOccupied && !isDirty)
        {
            StartCoroutine(UseToiletRoutine());
        }
    }

    private IEnumerator UseToiletRoutine()
    {
        isOccupied = true;
        yield return new WaitForSeconds(8f); // Temps passé par le PNJ dans les toilettes
        isOccupied = false;
        isDirty = true;
        StartCleaning();
    }

    private void OnMouseDown()
    {
        CleanToilet();
    }

    public void CleanToilet()
    {
        if (isDirty)
        {
            SFXManager.instance.PlaySoundFXClip(ventouse, transform, 1f);
            cleaningClicksRequired--;
            Debug.Log(cleaningClicksRequired);
            if (cleaningClicksRequired <= 0)
            {
                isDirty = false;
                gameManager.AugmenterScore(25); // Gagner 25 points de score après avoir nettoyé le toilette
            }
        }
    }

    public void StartCleaning()
    {
        cleaningClicksRequired = Random.Range(5, 10); // Nombre aléatoire de clics nécessaires entre 5 et 9 pour nettoyer le toilette
    }
}