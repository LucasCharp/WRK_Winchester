using UnityEngine;

public class ShowResultOnClick : MonoBehaviour
{
    public GameObject resultCanvas; // R�f�rence au canvas contenant le texte r�sultat
    public GameObject jobCanvas; // R�f�rence au canvas contenant les boutons de genre musical
    public string[] favoriteGenres; // Genre musical pr�f�r� actuel
    public GameManager gameManager; // R�f�rence au script de gestion du score
    private bool isPlayingFavoriteMusic; // Indique si la musique correspond au genre pr�f�r�

    void Start()
    {
        // Assurez-vous que le canvas est d�sactiv� au d�marrage du jeu
        resultCanvas.SetActive(false);
    }
    public void OnMusicChanged(bool isFavoriteMusic)
    {
        // Mettre � jour l'indicateur pour indiquer si la musique correspond au genre pr�f�r�
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
        // Affiche le canvas avec le r�sultat
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
