using UnityEngine;

public class ShowResultOnClick : MonoBehaviour
{
    public GameObject resultCanvas; // Référence au canvas contenant le texte résultat
    public GameObject jobCanvas; // Référence au canvas contenant les boutons de genre musical
    public string[] favoriteGenres; // Genre musical préféré actuel
    public GameManager gameManager; // Référence au script de gestion du score
    private bool isPlayingFavoriteMusic; // Indique si la musique correspond au genre préféré

    void Start()
    {
        // Assurez-vous que le canvas est désactivé au démarrage du jeu
        resultCanvas.SetActive(false);
    }

    // Fonction appelée lorsqu'un bouton de genre musical est cliqué
    public void OnGenreButtonClicked(string genre)
    {
        if (IsGenreFavorite(genre) && isPlayingFavoriteMusic)
        {
            // Augmenter le score si le genre correspond au genre préféré et que la musique est en cours
            gameManager.IncreaseScoreContinuous(10);
        }
        else
        {
            // Réinitialiser le score continu si le genre ne correspond pas ou si la musique est différente
            gameManager.ResetContinuousScore();
        }
    }

    // Fonction appelée lors du changement de musique dans la zone de danse
    public void OnMusicChanged(bool isFavoriteMusic)
    {
        // Mettre à jour l'indicateur pour indiquer si la musique correspond au genre préféré
        isPlayingFavoriteMusic = isFavoriteMusic;
    }

    private bool IsGenreFavorite(string genre)
    {
        foreach (string favoriteGenre in favoriteGenres)
        {
            if (genre == favoriteGenre)
            {
                return true;
            }
        }
        return false;
    }

    private void OnMouseDown()
    {
        Canvas[] canvases = FindObjectsOfType<Canvas>();
        // Affiche le canvas avec le résultat
        resultCanvas.SetActive(true);
        if (resultCanvas != null)
        {
            foreach (Canvas canvas in canvases)
            {
                jobCanvas.SetActive(false);
            }
        }
    }
}
