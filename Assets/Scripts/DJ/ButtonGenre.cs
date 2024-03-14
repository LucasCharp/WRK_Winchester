using UnityEngine;
using UnityEngine.UI;

public class ButtonGenre : MonoBehaviour
{
    public string genreName; // Le nom du genre musical associ� � ce bouton
    public GameManager gameManager; // R�f�rence au script de gestion du score
    private bool isPlayingFavoriteMusic; // Indique si la musique correspond au genre pr�f�r�

    // M�thode pour d�finir la pr�f�rence de genre
    public void SetGenrePreference(int preference)
    {
        // Impl�mentez ici la logique pour d�finir la pr�f�rence de genre
        Debug.Log("Setting genre preference for " + genreName + " to " + preference);
    }

    void Start()
    {
        // Assurez-vous que le bouton est configur� pour appeler une m�thode lorsque cliqu�
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Le composant Button est manquant sur ce GameObject.");
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

    // M�thode appel�e lorsque le bouton est cliqu�
    void OnButtonClick()
    {
        // Appeler une m�thode ou d�clencher un �v�nement pour g�rer le clic du bouton
        Debug.Log("Bouton " + genreName + " cliqu� !");
        // Ajoutez ici la logique pour g�rer le clic du bouton
        // D�finir la pr�f�rence de genre pour ce bouton
        SetGenrePreference(10); // Exemple : attribuer une note de 10 au genre associ� � ce bouton
    }

    void Update()
    {
        // V�rifier si la musique est en train de jouer et si elle correspond au genre pr�f�r�
        if (IsMusicPlaying() && IsFavoriteMusic())
        {
            // Augmenter le score de fa�on continue
            gameManager.IncreaseScoreContinuous(10);
        }
        else
        {
            // Diminuer le score de fa�on continue
            gameManager.DecreaseScoreContinuous(5);
        }
    }

    // M�thode pour v�rifier si la musique est en train de jouer
    bool IsMusicPlaying()
    {
        // Impl�mentez ici la logique pour v�rifier si la musique est en train de jouer
        return true; // Par exemple, retourner vrai si la musique est en train de jouer, sinon faux
    }

    // M�thode pour v�rifier si la musique correspond au genre pr�f�r�
    bool IsFavoriteMusic()
    {
        // Impl�mentez ici la logique pour v�rifier si la musique correspond au genre pr�f�r�
        return true; // Par exemple, retourner vrai si la musique correspond au genre pr�f�r�, sinon faux
    }
}