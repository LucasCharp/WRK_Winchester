using UnityEngine;
using UnityEngine.UI;

public class ButtonGenre : MonoBehaviour
{
    public string genreName; // Le nom du genre musical associ� � ce bouton
    public MusicGenrePointCollector pointCollector; // R�f�rence au script de collecte de points
    public GameManager gameManager; // R�f�rence au gestionnaire de jeu pour le score

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

    private void OnButtonClick()
    {
        // R�cup�rer le genre pr�f�r� des PNJ
        string favoriteGenre = pointCollector.GetFavoriteGenre();

        // Comparer le genre de musique du bouton avec le genre pr�f�r� des PNJ
        if (genreName == favoriteGenre)
        {
            // Le genre de musique du bouton correspond au genre pr�f�r� des PNJ
            gameManager.IncreaseScoreContinuous(10);
        }
        else
        {
            // Le genre de musique du bouton ne correspond pas au genre pr�f�r� des PNJ
            gameManager.DecreaseScoreContinuous(5);
        }
    }
}