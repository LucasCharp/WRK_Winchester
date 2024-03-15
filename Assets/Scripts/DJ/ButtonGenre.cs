using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGenre : MonoBehaviour
{
    public string genreName; // Le nom du genre musical associ� � ce bouton
    public MusicGenrePointCollector pointCollector; // R�f�rence au script de collecte de points
    public GameManager gameManager; // R�f�rence au gestionnaire de jeu pour le score
    int maxPoints = 0;
    string favoriteGenre = "";

    private void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Le composant Button est manquant sur ce GameObject.");
        }

        // Trouver la r�f�rence du MusicGenrePointCollector si elle n'est pas d�j� d�finie
        if (pointCollector == null)
        {
            pointCollector = FindObjectOfType<MusicGenrePointCollector>();
            if (pointCollector == null)
            {
                Debug.LogError("MusicGenrePointCollector non trouv� !");
            }
        }

        // Trouver la r�f�rence du GameManager si elle n'est pas d�j� d�finie
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
            if (gameManager == null)
            {
                Debug.LogError("GameManager non trouv� !");
            }
        }
    }
    public string GetFavoriteGenre(Dictionary<string, int> totalPoints)
    {
        foreach (KeyValuePair<string, int> pair in totalPoints)
        {
            if (pair.Value > maxPoints)
            {
                maxPoints = pair.Value;
                favoriteGenre = pair.Key;
            }
        }

        return favoriteGenre;
    }
    private void OnButtonClick()
    {
        // V�rifier si le script de collecte de points est pr�sent
        if (pointCollector != null)
        {

            // Comparer le genre de musique du bouton avec le genre pr�f�r� des PNJ
            if (genreName == favoriteGenre)
            {
                // Le genre de musique du bouton correspond au genre pr�f�r� des PNJ
                gameManager.IncreaseScoreContinuous(10);
                Debug.Log("corespond");
            }
            else
            {
                // Le genre de musique du bouton ne correspond pas au genre pr�f�r� des PNJ
                gameManager.DecreaseScoreContinuous(5);
                Debug.Log("corespond pas");
            }
        }
        else
        {
            Debug.LogError("Le script MusicGenrePointCollector est manquant !");
        }
    }
}