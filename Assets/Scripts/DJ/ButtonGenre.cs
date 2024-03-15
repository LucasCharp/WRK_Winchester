using UnityEngine;
using UnityEngine.UI;

public class ButtonGenre : MonoBehaviour
{
    public string genreName; // Le nom du genre musical associé à ce bouton
    public MusicGenrePointCollector pointCollector; // Référence au script de collecte de points
    public GameManager gameManager; // Référence au gestionnaire de jeu pour le score

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

        // Trouver la référence du MusicGenrePointCollector si elle n'est pas déjà définie
        if (pointCollector == null)
        {
            pointCollector = FindObjectOfType<MusicGenrePointCollector>();
            if (pointCollector == null)
            {
                Debug.LogError("MusicGenrePointCollector non trouvé !");
            }
        }

        // Trouver la référence du GameManager si elle n'est pas déjà définie
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
            if (gameManager == null)
            {
                Debug.LogError("GameManager non trouvé !");
            }
        }
    }

    private void OnButtonClick()
    {
        // Récupérer le genre préféré des PNJ
        string favoriteGenre = pointCollector.GetFavoriteGenre();

        // Comparer le genre de musique du bouton avec le genre préféré des PNJ
        if (genreName == favoriteGenre)
        {
            // Le genre de musique du bouton correspond au genre préféré des PNJ
            gameManager.IncreaseScoreContinuous(10);
        }
        else
        {
            // Le genre de musique du bouton ne correspond pas au genre préféré des PNJ
            gameManager.DecreaseScoreContinuous(5);
        }
    }
}