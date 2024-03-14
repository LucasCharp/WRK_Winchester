using UnityEngine;
using UnityEngine.UI;

public class ButtonGenre : MonoBehaviour
{
    public string genreName; // Le nom du genre musical associé à ce bouton
    public GameManager gameManager; // Référence au script de gestion du score
    private bool isPlayingFavoriteMusic; // Indique si la musique correspond au genre préféré

    // Méthode pour définir la préférence de genre
    public void SetGenrePreference(int preference)
    {
        // Implémentez ici la logique pour définir la préférence de genre
        Debug.Log("Setting genre preference for " + genreName + " to " + preference);
    }

    void Start()
    {
        // Assurez-vous que le bouton est configuré pour appeler une méthode lorsque cliqué
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Le composant Button est manquant sur ce GameObject.");
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

    // Méthode appelée lorsque le bouton est cliqué
    void OnButtonClick()
    {
        // Appeler une méthode ou déclencher un événement pour gérer le clic du bouton
        Debug.Log("Bouton " + genreName + " cliqué !");
        // Ajoutez ici la logique pour gérer le clic du bouton
        // Définir la préférence de genre pour ce bouton
        SetGenrePreference(10); // Exemple : attribuer une note de 10 au genre associé à ce bouton
    }

    void Update()
    {
        // Vérifier si la musique est en train de jouer et si elle correspond au genre préféré
        if (IsMusicPlaying() && IsFavoriteMusic())
        {
            // Augmenter le score de façon continue
            gameManager.IncreaseScoreContinuous(10);
        }
        else
        {
            // Diminuer le score de façon continue
            gameManager.DecreaseScoreContinuous(5);
        }
    }

    // Méthode pour vérifier si la musique est en train de jouer
    bool IsMusicPlaying()
    {
        // Implémentez ici la logique pour vérifier si la musique est en train de jouer
        return true; // Par exemple, retourner vrai si la musique est en train de jouer, sinon faux
    }

    // Méthode pour vérifier si la musique correspond au genre préféré
    bool IsFavoriteMusic()
    {
        // Implémentez ici la logique pour vérifier si la musique correspond au genre préféré
        return true; // Par exemple, retourner vrai si la musique correspond au genre préféré, sinon faux
    }
}